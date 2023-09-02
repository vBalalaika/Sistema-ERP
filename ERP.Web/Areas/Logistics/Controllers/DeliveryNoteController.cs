using ERP.Application.DTOs.Entities.CommercialDocuments.DeliveryNote;
using ERP.Application.DTOs.Entities.Logistics.Incomes;
using ERP.Application.DTOs.Entities.Production;
using ERP.Application.DTOs.Entities.Purchases.Providers;
using ERP.Application.DTOs.Entities.Purchases.PurchaseOrders;
using ERP.Application.Interfaces.ViewManagers.CommercialDocuments.DeliveryNote;
using ERP.Application.Interfaces.ViewManagers.Logistics.Incomes;
using ERP.Application.Interfaces.ViewManagers.Production;
using ERP.Application.Interfaces.ViewManagers.ProductMod;
using ERP.Application.Interfaces.ViewManagers.Purchases.Providers;
using ERP.Application.Interfaces.ViewManagers.Purchases.PurchaseOrders;
using ERP.Application.Specification.CommercialDocuments.DeliveryNote;
using ERP.Application.Specification.Production.WorkActivitySpecifications;
using ERP.Application.Specification.Production.WorkOrderItemSpecifications;
using ERP.Application.Specification.ProductMod;
using ERP.Application.Specification.Purchases.Providers;
using ERP.Application.Specification.Purchases.PurchaseOrders;
using ERP.Domain.Entities.Production;
using ERP.Language;
using ERP.Web.Abstractions;
using ERP.Web.Areas.Logistics.Models;
using ERP.Web.Areas.Logistics.Models.DeliveryNote;
using ERP.Web.Areas.Production.Models;
using ERP.Web.Areas.ProductMod.Models;
using ERP.Web.Areas.Purchases.Models.Providers;
using ERP.Web.Areas.Purchases.Models.PurchaseOrder;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Rotativa.AspNetCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Web.Areas.Logistics.Controllers
{
    [Area("Logistics")]
    public class DeliveryNoteController : BaseController<DeliveryNoteController>
    {
        private readonly IStringLocalizer<SharedResource> _localizer;

        private readonly IDeliveryNoteHeaderViewManager _deliveryNoteHeaderViewManager;
        private readonly IWorkActivityViewManager _workActivityViewManager;
        private readonly IProviderViewManager _providerViewManager;
        private readonly IWorkActionViewManager _workActionViewManager;
        private readonly IServicePOHeaderViewManager _servicePOHeaderViewManager;
        private readonly IIncomeHeaderViewManager _incomeHeaderViewManager;
        private readonly IProductViewManager _productViewManager;
        private readonly IWorkOrderItemViewManager _workOrderItemViewManager;
        private readonly IStationViewManager _stationViewManager;

        public DeliveryNoteController(IStringLocalizer<SharedResource> localizer, IDeliveryNoteHeaderViewManager deliveryNoteHeaderViewManager, IWorkActivityViewManager workActivityViewManager, IProviderViewManager providerViewManager, IWorkActionViewManager workActionViewManager, IServicePOHeaderViewManager servicePOHeaderViewManager, IIncomeHeaderViewManager incomeHeaderViewManager, IProductViewManager productViewManager, IWorkOrderItemViewManager workOrderItemViewManager, IStationViewManager stationViewManager)
        {
            _localizer = localizer;
            _deliveryNoteHeaderViewManager = deliveryNoteHeaderViewManager;
            _workActivityViewManager = workActivityViewManager;
            _providerViewManager = providerViewManager;
            _workActionViewManager = workActionViewManager;
            _servicePOHeaderViewManager = servicePOHeaderViewManager;
            _incomeHeaderViewManager = incomeHeaderViewManager;
            _productViewManager = productViewManager;
            _workOrderItemViewManager = workOrderItemViewManager;
            _stationViewManager = stationViewManager;
        }

        public async Task<JsonResult> CreateDeliveryNoteByWorkActivityIds(string[] workActivityIds, int stationId)
        {
            try
            {
                // Me traigo de la base de datos todas las actividades que se seleccionaron y las cargo en una lista.
                var waIds = workActivityIds.Select(x => x.Split("-").First()).ToList();
                var workActivityList = new List<WorkActivityViewModel>();
                foreach (var waId in waIds)
                {
                    var getWorkActivitiesById = await _workActivityViewManager.FindBySpecification(new WorkActivityWithAllSpecification(true, Convert.ToInt32(waId)));
                    if (getWorkActivitiesById.Succeeded)
                    {
                        workActivityList.Add(_mapper.Map<WorkActivityViewModel>(getWorkActivitiesById.Data.FirstOrDefault()));
                    }
                }

                // Compruebo que solo se genere un remito para actividades que tengan el mismo proveedor.
                if (workActivityList.GroupBy(wa => wa.OutsourcedProviderId).Count() > 1)
                {
                    _notify.Warning(_localizer["A delivery note cannot be generated for more than one supplier. Please select items that correspond to a single provider."].ToString());
                    return new JsonResult(new { isValid = false });
                }
                else
                {
                    // Genero el remito con su información.
                    var deliveryNoteHVM = new DeliveryNoteHeaderViewModel();

                    // Cargo los proveedores.
                    var providersResponse = await _providerViewManager.FindBySpecification(new ProvidersGetAllSpecification());
                    if (providersResponse.Succeeded)
                    {
                        var providerViewModel = _mapper.Map<List<ProviderViewModel>>(providersResponse.Data);
                        // Proveedores de servicios
                        deliveryNoteHVM.Providers = new SelectList(providerViewModel.Where(pv => pv.ProviderType == "Servicios").ToList(), nameof(ProviderViewModel.Id), nameof(ProviderViewModel.getBussinessNameOrName), null, null);
                        // Proveedores de transporte
                        // Agregar transporte propio (ver ingresos)
                        deliveryNoteHVM.TransportProviders = new SelectList(providerViewModel.Where(pv => pv.ProviderType == "Servicios" || pv.ProviderType == "Transporte").ToList(), nameof(ProviderViewModel.Id), nameof(ProviderViewModel.getBussinessNameOrName), null, null);
                    }

                    if (workActivityList.All(wa => wa.OutsourcedProviderId != null))
                    {
                        deliveryNoteHVM.ProviderId = workActivityList.Select(wa => wa.OutsourcedProviderId).FirstOrDefault().Value;
                    }

                    deliveryNoteHVM.DeliveryNoteDetails = new List<DeliveryNoteDetailViewModel>();
                    foreach (var waVM in workActivityList)
                    {
                        var deliveryNoteDVM = new DeliveryNoteDetailViewModel();

                        deliveryNoteDVM.WorkActivity = waVM;
                        foreach (var waId in workActivityIds)
                        {
                            if (waVM.Id == Convert.ToInt32(waId.Split("-")[0]))
                            {
                                deliveryNoteDVM.Quantity = Convert.ToInt32(waId.Split("-")[1]);
                            }
                        }

                        deliveryNoteHVM.DeliveryNoteDetails.Add(deliveryNoteDVM);
                    }

                    deliveryNoteHVM.stationId = stationId;

                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_OnGetCreate", deliveryNoteHVM) });

                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return new JsonResult(new { isValid = false });
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreate(int id, DeliveryNoteHeaderViewModel deliveryNoteHeaderViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    if (deliveryNoteHeaderViewModel.TransportProviderId == -1)
                    {
                        // Me traigo de la base de datos todas las actividades.
                        foreach (var detail in deliveryNoteHeaderViewModel.DeliveryNoteDetails)
                        {
                            var getWorkActivitiesById = await _workActivityViewManager.FindBySpecification(new WorkActivityWithAllSpecification(true, detail.WorkActivityId.Value));
                            if (getWorkActivitiesById.Succeeded)
                            {
                                detail.WorkActivity = _mapper.Map<WorkActivityViewModel>(getWorkActivitiesById.Data.FirstOrDefault());
                            }
                        }

                        _notify.Warning(_localizer["Please, you must select a transport provider."]);
                        return new JsonResult(new { isValid = false, html = await _viewRenderer.RenderViewToStringAsync("_OnGetCreate", deliveryNoteHeaderViewModel) });
                    }

                    if (id == 0)
                    {
                        // Create
                        var detailsDeliveryNote = deliveryNoteHeaderViewModel.DeliveryNoteDetails.Where(dnd => dnd != null).ToList();
                        if (detailsDeliveryNote.Count() > 0)
                        {
                            deliveryNoteHeaderViewModel.DeliveryNoteDetails = detailsDeliveryNote;

                            // Cuando creo un remito tengo que fijarme si no existe uno con el mismo numero de remito, si es asi, es porque se fusiono con otro:
                            // Lo que hago es borrar el anterior y guardar el nuevo.
                            var getDNHByDeliveryNoteNumber = await _deliveryNoteHeaderViewManager.FindBySpecification(new DNHByDeliveryNoteNumberProviderAndTransportSpecification(deliveryNoteHeaderViewModel.ProviderId, deliveryNoteHeaderViewModel.TransportProviderId, deliveryNoteHeaderViewModel.Number.Trim()));
                            if (getDNHByDeliveryNoteNumber.Succeeded)
                            {
                                if (getDNHByDeliveryNoteNumber.Data.Count > 0)
                                {
                                    await _deliveryNoteHeaderViewManager.DeleteAsync(getDNHByDeliveryNoteNumber.Data.FirstOrDefault().Id);
                                }
                            }

                            var deliveryNoteHeaderDTO = _mapper.Map<DeliveryNoteHeaderDTO>(deliveryNoteHeaderViewModel);
                            var result = await _deliveryNoteHeaderViewManager.CreateAsync(deliveryNoteHeaderDTO);
                            if (result.Succeeded)
                            {
                                id = result.Data;

                                // Una vez que se crea el remito, tengo que setear la actividad como en proceso.
                                //foreach (var detail in deliveryNoteHeaderViewModel.DeliveryNoteDetails)
                                //{
                                //    if (detail.WorkActivityId.HasValue)
                                //    {
                                //        var getWorkActivityById = await _workActivityViewManager.FindBySpecification(new WorkActivityWithAllSpecification(detail.WorkActivityId.Value, detail.ProductItemId, deliveryNoteHeaderViewModel.stationId));
                                //        if (getWorkActivityById.Succeeded)
                                //        {
                                //            var waVMs = _mapper.Map<List<WorkActivityViewModel>>(getWorkActivityById.Data);

                                //            if (waVMs.Count > 0)
                                //            {
                                //                foreach (var waVM in waVMs)
                                //                {
                                //                    // Tengo que iniciar la activadad para que quede en proceso
                                //                    await startAction(waVM.Id);
                                //                }
                                //            }
                                //        }
                                //    }
                                //}

                                _notify.Success(_localizer["Delivery note created."]);
                            }
                            else
                            {
                                _notify.Error(result.Message);
                            }

                        }
                        else
                        {
                            // Me traigo de la base de datos todas las actividades.
                            foreach (var detail in deliveryNoteHeaderViewModel.DeliveryNoteDetails)
                            {
                                var getWorkActivitiesById = await _workActivityViewManager.FindBySpecification(new WorkActivityWithAllSpecification(true, detail.WorkActivityId.Value));
                                if (getWorkActivitiesById.Succeeded)
                                {
                                    detail.WorkActivity = _mapper.Map<WorkActivityViewModel>(getWorkActivitiesById.Data.FirstOrDefault());
                                }
                            }

                            _notify.Warning(_localizer["You must add detail for this delivery note."]);
                            var html = await _viewRenderer.RenderViewToStringAsync("_OnGetCreate", deliveryNoteHeaderViewModel);
                            return new JsonResult(new { isValid = false, html = html });
                        }

                    }

                    return new JsonResult(new { isValid = true });
                }
                else
                {
                    // Me traigo de la base de datos todas las actividades.
                    foreach (var detail in deliveryNoteHeaderViewModel.DeliveryNoteDetails)
                    {
                        var getWorkActivitiesById = await _workActivityViewManager.FindBySpecification(new WorkActivityWithAllSpecification(true, detail.WorkActivityId.Value));
                        if (getWorkActivitiesById.Succeeded)
                        {
                            detail.WorkActivity = _mapper.Map<WorkActivityViewModel>(getWorkActivitiesById.Data.FirstOrDefault());
                        }
                    }

                    _notify.Warning(_localizer["Data is not complete."]);
                    var html = await _viewRenderer.RenderViewToStringAsync("_OnGetCreate", deliveryNoteHeaderViewModel);
                    return new JsonResult(new { isValid = false, html = html });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                var html = await _viewRenderer.RenderViewToStringAsync("_OnGetCreate", deliveryNoteHeaderViewModel);
                return new JsonResult(new { isValid = false, html = html });
            }
        }

        public async Task<bool> startAction(int workActivityId)
        {
            try
            {
                var viewModel = new WorkActionViewModel();
                viewModel.WorkActivityId = workActivityId;
                viewModel.ActionName = "play";
                viewModel.StartDate = DateTime.Now;
                var responseAction = await _workActionViewManager.CreateAsync(_mapper.Map<WorkActionDTO>(viewModel));
                if (responseAction.Succeeded)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _notify.Error($"{_localizer["Error"]}: {ex.Message}.");
                return false;
            }
        }

        public async Task<JsonResult> GetDeliveryNoteById(int dnhId)
        {
            try
            {
                var getDeliveryNote = await _deliveryNoteHeaderViewManager.FindBySpecification(new DNHSpecification(dnhId));
                if (getDeliveryNote.Succeeded)
                {
                    var deliveryNoteHVM = _mapper.Map<DeliveryNoteHeaderViewModel>(getDeliveryNote.Data.First());

                    // Cargo los proveedores.
                    var providersResponse = await _providerViewManager.FindBySpecification(new ProvidersGetAllSpecification());
                    if (providersResponse.Succeeded)
                    {
                        var providerViewModel = _mapper.Map<List<ProviderDTO>>(providersResponse.Data);
                        deliveryNoteHVM.Providers = new SelectList(providerViewModel, nameof(ProviderViewModel.Id), nameof(ProviderViewModel.BusinessName), null, null);
                        deliveryNoteHVM.TransportProviders = new SelectList(providerViewModel, nameof(ProviderViewModel.Id), nameof(ProviderViewModel.BusinessName), null, null);
                    }

                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_OnGet", deliveryNoteHVM) });

                }
                else
                {
                    return new JsonResult(new { isValid = false });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return new JsonResult(new { isValid = false });
            }
        }

        public async Task<IActionResult> ExportToPdf(int deliveryNoteHeaderId, bool wasExportedToPDF, int stationId)
        {
            try
            {
                var getDNHById = await _deliveryNoteHeaderViewManager.FindBySpecification(new DNHSpecification(deliveryNoteHeaderId));
                if (getDNHById.Succeeded)
                {
                    var dnhVM = _mapper.Map<DeliveryNoteHeaderViewModel>(getDNHById.Data.FirstOrDefault());

                    // Cuando se exporta el remito en PDF, se genera automaticamente una OC de servicio y el detalle de cada remito se corresponde con los detalles de la OC de servicios.
                    int servicePOHeaderId = await createServicePOWithDeliveryNoteDetails(dnhVM);
                    if (servicePOHeaderId == 0)
                    {
                        _notify.Error(_localizer["Could not create service purchase order."]);
                        return RedirectToAction("Index", "Shipping");
                    }

                    // Ademas, se hacen inserts en "Ingresos" para cada uno de estos detalles con estado "Pendiente de recepcion".
                    int createIncomes = await createIncomesWithDeliveryNoteDetails(dnhVM, servicePOHeaderId);
                    if (createIncomes == 0)
                    {
                        _notify.Error(_localizer["Could not create incomes."]);
                        return RedirectToAction("Index", "Shipping");
                    }

                    // Tengo que actualizar el atributo wasExportedToPdf = true para este dnhVM
                    dnhVM.wasExportedToPDF = wasExportedToPDF;
                    var dnhDTO = _mapper.Map<DeliveryNoteHeaderDTO>(dnhVM);
                    var updateResult = await _deliveryNoteHeaderViewManager.UpdateAsync(dnhDTO);
                    if (!updateResult.Succeeded)
                    {
                        _notify.Error(_localizer["Could not update delivery note status."]);
                        return RedirectToAction("Index", "Shipping");
                    }

                    // Una vez que se crea el remito, tengo que setear la actividad como en proceso.
                    bool resultStartAction = false;
                    foreach (var detail in dnhVM.DeliveryNoteDetails)
                    {
                        if (detail.WorkActivityId.HasValue)
                        {
                            var getWorkActivityById = await _workActivityViewManager.FindBySpecification(new WorkActivityWithAllSpecification(detail.WorkActivityId.Value, detail.ProductItemId, stationId));
                            if (getWorkActivityById.Succeeded)
                            {
                                var waVMs = _mapper.Map<List<WorkActivityViewModel>>(getWorkActivityById.Data);

                                if (waVMs.Count > 0)
                                {
                                    foreach (var waVM in waVMs)
                                    {
                                        // Tengo que iniciar la activadad para que quede en proceso
                                        bool result = await startAction(waVM.Id);
                                        if (!result)
                                        {
                                            resultStartAction = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (resultStartAction)
                    {
                        _notify.Error(_localizer["An error occurred while trying to set activities in progress."]);
                        return RedirectToAction("Index", "Shipping");
                    }

                    //return await GeneratePDF(dnhVM);

                    return new ViewAsPdf("_PdfReportBody", dnhVM)
                    {
                        PageSize = Rotativa.AspNetCore.Options.Size.A4,
                        PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                        PageMargins = new Rotativa.AspNetCore.Options.Margins(2, 4, 2, 4),
                        FileName = "Remito_" + dnhVM.Number + ".pdf"
                    };
                    //return View("_PdfReportBody", dnhVM);
                }
                else
                {
                    _notify.Warning(_localizer["Could not export to PDF."].ToString());
                    return RedirectToAction("Index", "Shipping");
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return RedirectToAction("Index", "Shipping");
            }
        }

        public async Task<IActionResult> GeneratePDF(DeliveryNoteHeaderViewModel dnhVM)
        {
            // Render the view to HTML
            var html = await _viewRenderer.RenderViewToStringAsync("_PdfReportBody", dnhVM);

            // Split the table into separate pages
            var numberOfRowsPerPage = 2;
            var rows = html.Split("<tr");
            List<string> tableRows = new List<string>();
            tableRows.Add(html.Split("<tr")[0]);
            tableRows.Add("<tr" + html.Split("<tr")[1]);
            var tableRowsAux = rows.Skip(2).Select(row => "<tr" + row).ToList();
            foreach (var tra in tableRowsAux)
            {
                tableRows.Add(tra);
            }

            int index = 1;
            var pages = new List<List<string>>();
            while (tableRows.Any())
            {
                List<string> pageRows = new List<string>();
                if (index == 1)
                {
                    // To add header and number of rows in first iteration
                    pageRows = tableRows.Take(numberOfRowsPerPage + 2).ToList();
                    tableRows = tableRows.Skip(numberOfRowsPerPage + 2).ToList();
                }
                else
                {
                    pageRows = tableRows.Take(numberOfRowsPerPage).ToList();
                    tableRows = tableRows.Skip(numberOfRowsPerPage).ToList();
                }

                pages.Add(pageRows);
                index++;
            }

            // Generate PDF from each page
            var pdfBytes = new List<byte[]>();

            foreach (var page in pages)
            {
                string pageHtml = string.Join("", page);
                var pdf = new ViewAsPdf
                {
                    ViewName = "_Blank", // Use a blank view as the template
                    IsGrayScale = false,
                    PageSize = Rotativa.AspNetCore.Options.Size.A4,
                    PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                    PageMargins = new Rotativa.AspNetCore.Options.Margins(2, 4, 2, 4),
                    Model = pageHtml
                };

                pdfBytes.Add(await pdf.BuildFile(ControllerContext));
            }

            // Combine all PDF pages into a single PDF file
            var mergedPdfBytes = MergePdfBytes(pdfBytes);

            // Return the PDF as a file download
            return File(mergedPdfBytes, "application/pdf", "Remito_" + dnhVM.Number + ".pdf");
        }

        private byte[] MergePdfBytes(List<byte[]> pdfBytes)
        {
            using (var memoryStream = new MemoryStream())
            {
                var document = new Document();
                var writer = new PdfCopy(document, memoryStream);
                document.Open();

                foreach (var pdfByte in pdfBytes)
                {
                    var reader = new PdfReader(pdfByte);
                    for (var i = 1; i <= reader.NumberOfPages; i++)
                    {
                        var importedPage = writer.GetImportedPage(reader, i);
                        writer.AddPage(importedPage);
                    }

                    reader.Close();
                }

                writer.Close();
                document.Close();

                return memoryStream.ToArray();
            }
        }

        // Cuando se exporta el remito en PDF, se genera automaticamente una OC de servicio y el detalle de cada remito se corresponde con los detalles de la OC de servicios.
        public async Task<int> createServicePOWithDeliveryNoteDetails(DeliveryNoteHeaderViewModel dnhVM)
        {
            var servicePOHeaderVM = new ServicePOHeaderViewModel();
            servicePOHeaderVM.Date = DateTime.Now.Date.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute);
            var servicePOHeaders = _servicePOHeaderViewManager.FindBySpecification(new ServicePOHeaderSpecification());
            if (servicePOHeaders.Result.Succeeded)
            {
                var servicePOHeadersVMs = _mapper.Map<List<ServicePOHeaderViewModel>>(servicePOHeaders.Result.Data);
                if (servicePOHeadersVMs.Count > 0)
                {
                    servicePOHeaderVM.Number = servicePOHeadersVMs.Max(poh => poh.Id);
                    servicePOHeaderVM.Number += 1;
                }
                else
                {
                    servicePOHeaderVM.Number = 1;
                }
            }
            servicePOHeaderVM.ProviderId = dnhVM.ProviderId;
            servicePOHeaderVM.Comments = dnhVM.Comments;
            servicePOHeaderVM.StationId = dnhVM.DeliveryNoteDetails.FirstOrDefault().WorkActivity.ProductStation.StationId;

            foreach (var deliveryNoteDetail in dnhVM.DeliveryNoteDetails)
            {
                var servicePODetailVM = new ServicePODetailViewModel();

                servicePODetailVM.DeliveryNoteDetailId = deliveryNoteDetail.Id;
                servicePODetailVM.ProductId = deliveryNoteDetail.ProductItemId;
                servicePODetailVM.Quantity = deliveryNoteDetail.Quantity;
                servicePODetailVM.UnitMeasureId = deliveryNoteDetail.ProductItem.UnitMeasureId;
                //foreach (var rps in dnhVM.Provider.RelProviderStations)
                //{
                //    if (rps.ProviderId == servicePOHeaderVM.ProviderId && rps.StationId == servicePOHeaderVM.StationId)
                //    {
                //        servicePODetailVM.UnitPrice = rps.DollarPrice;
                //        if (rps.PriceUnitMeasureId.HasValue)
                //        {
                //            servicePODetailVM.UnitMeasureId = rps.PriceUnitMeasureId.Value;
                //        }
                //    }
                //}
                //servicePOHeaderVM.Total += servicePODetailVM.UnitPrice;
                servicePOHeaderVM.ServicePODetails.Add(servicePODetailVM);
            }

            var servicePOHeaderDTO = _mapper.Map<ServicePOHeaderDTO>(servicePOHeaderVM);
            var createServicePO = await _servicePOHeaderViewManager.CreateAsync(servicePOHeaderDTO);
            if (createServicePO.Succeeded)
            {
                return createServicePO.Data;
            }
            else
            {
                return 0;
            }
        }

        // Ademas, se hacen inserts en "Ingresos" para cada uno de estos detalles con estado "Pendiente de recepcion".
        public async Task<int> createIncomesWithDeliveryNoteDetails(DeliveryNoteHeaderViewModel dnhVM, int servicePOHeaderId)
        {
            var incomeHeaderVM = new IncomeHeaderViewModel();
            incomeHeaderVM.IncomeDate = dnhVM.Date;
            incomeHeaderVM.ProviderId = dnhVM.ProviderId;
            incomeHeaderVM.TransportProviderId = dnhVM.TransportProviderId;
            if (incomeHeaderVM.ProviderId == incomeHeaderVM.TransportProviderId)
            {
                incomeHeaderVM.OwnTransport = true;
            }
            else
            {
                incomeHeaderVM.OwnTransport = false;
            }
            // El numero de remito es el de recepcion
            //incomeHeaderVM.DeliveryNoteNumber
            // Numero de factura    
            //incomeHeaderVM.InvoiceNumber
            incomeHeaderVM.ExternalProcessStationId = dnhVM.DeliveryNoteDetails.FirstOrDefault().WorkActivity.ProductStation.StationId;

            foreach (var deliveryNoteDetail in dnhVM.DeliveryNoteDetails)
            {
                var incomeDetailVM = new IncomeDetailViewModel();
                incomeDetailVM.IncomeProductId = deliveryNoteDetail.ProductItemId;
                incomeDetailVM.Quantity = deliveryNoteDetail.Quantity;
                if (deliveryNoteDetail.ProductItem.UnitMeasure == null)
                {
                    incomeDetailVM.UnitId = 19;
                }
                else
                {
                    incomeDetailVM.UnitId = deliveryNoteDetail.ProductItem.UnitMeasureId.Value;
                }
                //incomeDetailVM.BatchNumber
                incomeDetailVM.OTNumber = deliveryNoteDetail.WorkActivity.WorkOrderItem.WorkOrder.WorkOrderHeader.WorkOrderHeaderNumber;
                incomeDetailVM.ProductNumber = deliveryNoteDetail.WorkActivity.WorkOrderItem.WorkOrder.OrderDetail.ProductNumber;
                incomeDetailVM.FatherProductId = deliveryNoteDetail.ProductId;
                incomeDetailVM.FatherStructureId = deliveryNoteDetail.ConfigurationId;
                incomeDetailVM.OCNumber = servicePOHeaderId;
                // Estado: "Pendiente de recepcion"
                incomeDetailVM.IncomeStateId = 1;

                incomeDetailVM.DeliveryNoteHeaderId = dnhVM.Id;

                // Set "Reception" and "NextStation"
                var getRPSByProductId = await _productViewManager.FindBySpecification(new ProductWithRelProductStationsSpecification(incomeDetailVM.IncomeProductId));
                if (getRPSByProductId.Succeeded)
                {
                    var relProductStationVMs = _mapper.Map<List<RelProductStationViewModel>>(getRPSByProductId.Data.First().RelProductStations);
                    for (int i = 0; i < relProductStationVMs.Count(); i++)
                    {
                        if (incomeHeaderVM.ExternalProcessStationId.HasValue && incomeHeaderVM.ExternalProcessStationId != null)
                        {
                            var getStationById = await _stationViewManager.GetByIdAsync(incomeHeaderVM.ExternalProcessStationId.Value);
                            if (getStationById.Succeeded)
                            {
                                if (getStationById.Data.WorkOrderDisplayType == "Envios")
                                {
                                    if (incomeHeaderVM.ExternalProcessStationId == relProductStationVMs[i].StationId)
                                    {
                                        if ((i + 1) < (relProductStationVMs.Count()))
                                        {
                                            incomeDetailVM.Reception = relProductStationVMs[i + 1].Station.Abbreviation;
                                        }

                                        if ((i + 2) < (relProductStationVMs.Count()))
                                        {
                                            incomeDetailVM.NextStation = relProductStationVMs[i + 2].Station.Abbreviation;
                                        }
                                        else
                                        {
                                            // Busco la HDR del padre y se la asigno al NextStation
                                            var getWOIsByProductNumber = await _workOrderItemViewManager.FindBySpecification(new WOIByProductNumberSpecification(incomeDetailVM.ProductNumber.Trim()));
                                            if (getWOIsByProductNumber.Succeeded)
                                            {
                                                var workOrderItems = _mapper.Map<List<WorkOrderItem>>(getWOIsByProductNumber.Data);
                                                if (workOrderItems.Count > 0)
                                                {
                                                    foreach (var woi in workOrderItems)
                                                    {
                                                        // Si tiene padre
                                                        if (woi.ProductId == incomeDetailVM.IncomeProductId && woi.WorkOrderItemOf != null)
                                                        {
                                                            var responseProductStationsFather = await _productViewManager.FindBySpecification(new ProductWithRelProductStationsSpecification((int)woi.WorkOrderItemOf.ProductId));
                                                            var fatherProduct = _mapper.Map<ProductViewModel>(responseProductStationsFather.Data.FirstOrDefault());
                                                            // Si el padre tiene actividades, asignamos las estaciones como NextStation
                                                            if (fatherProduct.RelProductStations.Count > 0)
                                                            {
                                                                //for (int index = 0; index < fatherProduct.RelProductStations.Count(); index++)
                                                                //{
                                                                //    if (incomeDetailVM.NextStation != null)
                                                                //    {
                                                                //        if (!incomeDetailVM.NextStation.Contains(fatherProduct.RelProductStations[index].Station.Abbreviation))
                                                                //        {
                                                                //            incomeDetailVM.NextStation += fatherProduct.RelProductStations[index].Station.Abbreviation;
                                                                //        }
                                                                //    }
                                                                //    else
                                                                //    {
                                                                //        incomeDetailVM.NextStation += fatherProduct.RelProductStations[index].Station.Abbreviation;
                                                                //    }

                                                                //    if ((index + 1) != fatherProduct.RelProductStations.Count())
                                                                //    {
                                                                //        incomeDetailVM.NextStation += ", ";
                                                                //    }
                                                                //}

                                                                if (incomeDetailVM.NextStation != null)
                                                                {
                                                                    if (!incomeDetailVM.NextStation.Contains(fatherProduct.RelProductStations[0].Station.Abbreviation))
                                                                    {
                                                                        incomeDetailVM.NextStation += fatherProduct.RelProductStations[0].Station.Abbreviation;
                                                                        incomeDetailVM.NextStation += ", ";
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    incomeDetailVM.NextStation += fatherProduct.RelProductStations[0].Station.Abbreviation;
                                                                    incomeDetailVM.NextStation += ", ";
                                                                }

                                                                //incomeDetailVM.NextStation = fatherProduct.RelProductStations[0].Station.Abbreviation;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    // Cuando viene de planilla donde se enviaron a producir afuera.
                                    if (incomeHeaderVM.ExternalProcessStationId == relProductStationVMs[i].StationId)
                                    {
                                        if ((i + 1) < (relProductStationVMs.Count()))
                                        {
                                            if (relProductStationVMs[i + 1].Station.Abbreviation != "R01" && relProductStationVMs[i + 1].Station.Abbreviation != "R02")
                                            {
                                                incomeDetailVM.Reception = "R01";
                                                incomeDetailVM.NextStation = relProductStationVMs[i + 1].Station.Abbreviation;
                                            }
                                        }
                                        else
                                        {
                                            // Busco la HDR del padre y se la asigno al NextStation
                                            var getWOIsByProductNumber = await _workOrderItemViewManager.FindBySpecification(new WOIByProductNumberSpecification(incomeDetailVM.ProductNumber.Trim()));
                                            if (getWOIsByProductNumber.Succeeded)
                                            {
                                                var workOrderItems = _mapper.Map<List<WorkOrderItem>>(getWOIsByProductNumber.Data);
                                                if (workOrderItems.Count > 0)
                                                {
                                                    foreach (var woi in workOrderItems)
                                                    {
                                                        // Si tiene padre
                                                        if (woi.ProductId == incomeDetailVM.IncomeProductId && woi.WorkOrderItemOf != null)
                                                        {
                                                            var responseProductStationsFather = await _productViewManager.FindBySpecification(new ProductWithRelProductStationsSpecification((int)woi.WorkOrderItemOf.ProductId));
                                                            var fatherProduct = _mapper.Map<ProductViewModel>(responseProductStationsFather.Data.FirstOrDefault());
                                                            // Si el padre tiene actividades, asignamos las estaciones como NextStation
                                                            if (fatherProduct.RelProductStations.Count > 0)
                                                            {
                                                                //for (int index = 0; index < fatherProduct.RelProductStations.Count(); index++)
                                                                //{
                                                                //    if (incomeDetailVM.NextStation != null)
                                                                //    {
                                                                //        if (!incomeDetailVM.NextStation.Contains(fatherProduct.RelProductStations[index].Station.Abbreviation))
                                                                //        {
                                                                //            incomeDetailVM.NextStation += fatherProduct.RelProductStations[index].Station.Abbreviation;
                                                                //        }
                                                                //    }
                                                                //    else
                                                                //    {
                                                                //        incomeDetailVM.NextStation += fatherProduct.RelProductStations[index].Station.Abbreviation;
                                                                //    }

                                                                //    if ((index + 1) != fatherProduct.RelProductStations.Count())
                                                                //    {
                                                                //        incomeDetailVM.NextStation += ", ";
                                                                //    }
                                                                //}

                                                                if (incomeDetailVM.NextStation != null)
                                                                {
                                                                    if (!incomeDetailVM.NextStation.Contains(fatherProduct.RelProductStations[0].Station.Abbreviation))
                                                                    {
                                                                        incomeDetailVM.NextStation += fatherProduct.RelProductStations[0].Station.Abbreviation;
                                                                        incomeDetailVM.NextStation += ", ";
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    incomeDetailVM.NextStation += fatherProduct.RelProductStations[0].Station.Abbreviation;
                                                                    incomeDetailVM.NextStation += ", ";
                                                                }

                                                                //incomeDetailVM.NextStation = fatherProduct.RelProductStations[0].Station.Abbreviation;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                incomeHeaderVM.IncomeDetails.Add(incomeDetailVM);
            }

            var incomeHeaderDTO = _mapper.Map<IncomeHeaderDTO>(incomeHeaderVM);
            var createIncome = await _incomeHeaderViewManager.CreateAsync(incomeHeaderDTO);
            if (createIncome.Succeeded)
            {
                return createIncome.Data;
            }
            else
            {
                return 0;
            }
        }

    }
}
