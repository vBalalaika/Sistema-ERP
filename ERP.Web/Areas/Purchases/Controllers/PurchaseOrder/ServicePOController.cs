using ERP.Application.Constants;
using ERP.Application.DTOs.Entities.Purchases.PurchaseOrders;
using ERP.Application.Interfaces.ViewManagers.ProductMod;
using ERP.Application.Interfaces.ViewManagers.Purchases.Providers;
using ERP.Application.Interfaces.ViewManagers.Purchases.PurchaseOrders;
using ERP.Application.Specification.Purchases.Providers;
using ERP.Application.Specification.Purchases.PurchaseOrders;
using ERP.Language;
using ERP.Web.Abstractions;
using ERP.Web.Areas.ProductMod.Models;
using ERP.Web.Areas.Purchases.Models.Providers;
using ERP.Web.Areas.Purchases.Models.PurchaseOrder;
using ERP.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Rotativa.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Web.Areas.Purchases.Controllers.PurchaseOrder
{
    [Area("Purchases")]
    public class ServicePOController : BaseController<ServicePOController>
    {

        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly IServicePOHeaderViewManager _servicePOHeaderViewManager;
        private readonly IServicePODetailViewManager _servicePODetailViewManager;
        private readonly IProductViewManager _productViewManager;
        private readonly IStationViewManager _stationViewManager;
        private readonly IProviderViewManager _providerViewManager;
        private readonly IRelProviderStationViewManager _relProviderStationViewManager;

        public ServicePOController(IStringLocalizer<SharedResource> localizer, IServicePOHeaderViewManager servicePOHeaderViewManager, IServicePODetailViewManager servicePODetailViewManager, IProductViewManager productViewManager, IStationViewManager stationViewManager, IProviderViewManager providerViewManager, IRelProviderStationViewManager relProviderStationViewManager)
        {
            _localizer = localizer;
            _servicePOHeaderViewManager = servicePOHeaderViewManager;
            _servicePODetailViewManager = servicePODetailViewManager;
            _productViewManager = productViewManager;
            _stationViewManager = stationViewManager;
            _providerViewManager = providerViewManager;
            _relProviderStationViewManager = relProviderStationViewManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> _LoadAll(DatatableParamsViewModel dataTableParams,
            string sSearch_2, string sSearch_3, string sSearch_4, string sSearch_5, string sSearch_6, string sSearch_7, string sSearch_8, string sSearch_9, string sSearch_10,
            DateTime dateFromFilter, DateTime dateToFilter)
        {
            try
            {
                var response = await _servicePODetailViewManager.FindBySpecification(new ServicePODetailWithAllSpecifications());
                if (response.Succeeded)
                {
                    var servicePODetailVMs = _mapper.Map<List<ServicePODetailViewModel>>(response.Data);

                    // From date -> To date
                    servicePODetailVMs = servicePODetailVMs.Where(spod => spod.ServicePOHeader.Date.Date >= dateFromFilter && spod.ServicePOHeader.Date.Date <= dateToFilter).ToList();

                    if (!string.IsNullOrEmpty(sSearch_2))
                    {
                        servicePODetailVMs = servicePODetailVMs.Where(spod => spod.ServicePOHeader.Id.ToString().Contains(sSearch_2)).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_3))
                    {
                        servicePODetailVMs = servicePODetailVMs.Where(spod => spod.ServicePOHeader.Date.ToString("dd/MM/yyyy").Contains(sSearch_3)).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_4))
                    {
                        servicePODetailVMs = servicePODetailVMs.Where(spod => spod.Product != null && spod.Product.Code.ToString().ToLower().Contains(sSearch_4.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_5))
                    {
                        servicePODetailVMs = servicePODetailVMs.Where(spod => spod.Product != null && spod.Product.Description.ToString().ToLower().Contains(sSearch_5.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_6))
                    {
                        servicePODetailVMs = servicePODetailVMs.Where(spod => spod.Quantity.ToString().ToLower().Contains(sSearch_6.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_7))
                    {
                        servicePODetailVMs = servicePODetailVMs.Where(spod => spod.UnitMeasure.Description.ToString().ToLower().Contains(sSearch_7.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_8))
                    {
                        servicePODetailVMs = servicePODetailVMs.Where(spod => spod.UnitPrice.ToString().ToLower().Contains(sSearch_8.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_9))
                    {
                        servicePODetailVMs = servicePODetailVMs.Where(spod => spod.ServicePOHeader.Provider.getBussinessNameOrName.ToString().ToLower().Contains(sSearch_9.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_10))
                    {
                        servicePODetailVMs = servicePODetailVMs.Where(spod => spod.ServicePOHeader.Station.AbreviationDescription.ToString().ToLower().Contains(sSearch_10.ToLower())).ToList();
                    }

                    var sortColumnIndex = dataTableParams.iSortCol_0;
                    var sortDirection = dataTableParams.sSortDir_0;

                    if (sortColumnIndex == 2)
                    {
                        servicePODetailVMs = sortDirection == "asc" ? servicePODetailVMs.OrderBy(spod => spod.ServicePOHeader.Id).ToList() : servicePODetailVMs.OrderByDescending(spod => spod.ServicePOHeader.Id).ToList();
                    }
                    else if (sortColumnIndex == 3)
                    {
                        servicePODetailVMs = sortDirection == "asc" ? servicePODetailVMs.OrderBy(spod => spod.ServicePOHeader.Date).ToList() : servicePODetailVMs.OrderByDescending(spod => spod.ServicePOHeader.Date).ToList();
                    }
                    else if (sortColumnIndex == 4)
                    {
                        servicePODetailVMs = sortDirection == "asc" ? servicePODetailVMs.OrderBy(spod => spod.Product != null ? spod.Product.Code.ToString() : null).ToList() : servicePODetailVMs.OrderByDescending(spod => spod.Product != null ? spod.Product.Code.ToString() : null).ToList();
                    }
                    else if (sortColumnIndex == 5)
                    {
                        servicePODetailVMs = sortDirection == "asc" ? servicePODetailVMs.OrderBy(spod => spod.Product != null ? spod.Product.Description.ToString() : null).ToList() : servicePODetailVMs.OrderByDescending(spod => spod.Product != null ? spod.Product.Description.ToString() : null).ToList();
                    }
                    else if (sortColumnIndex == 6)
                    {
                        servicePODetailVMs = sortDirection == "asc" ? servicePODetailVMs.OrderBy(spod => spod.Quantity).ToList() : servicePODetailVMs.OrderByDescending(spod => spod.Quantity).ToList();
                    }
                    else if (sortColumnIndex == 7)
                    {
                        servicePODetailVMs = sortDirection == "asc" ? servicePODetailVMs.OrderBy(spod => spod.UnitMeasure.Description.ToString()).ToList() : servicePODetailVMs.OrderByDescending(spod => spod.UnitMeasure.Description.ToString()).ToList();
                    }
                    else if (sortColumnIndex == 8)
                    {
                        servicePODetailVMs = sortDirection == "asc" ? servicePODetailVMs.OrderBy(spod => spod.UnitPrice).ToList() : servicePODetailVMs.OrderByDescending(spod => spod.UnitPrice).ToList();
                    }
                    else if (sortColumnIndex == 9)
                    {
                        servicePODetailVMs = sortDirection == "asc" ? servicePODetailVMs.OrderBy(spod => spod.ServicePOHeader.Provider.getBussinessNameOrName.ToString()).ToList() : servicePODetailVMs.OrderByDescending(spod => spod.ServicePOHeader.Provider.getBussinessNameOrName.ToString()).ToList();
                    }
                    else if (sortColumnIndex == 10)
                    {
                        servicePODetailVMs = sortDirection == "asc" ? servicePODetailVMs.OrderBy(spod => spod.ServicePOHeader.Station.AbreviationDescription).ToList() : servicePODetailVMs.OrderByDescending(spod => spod.ServicePOHeader.Station.AbreviationDescription).ToList();
                    }

                    var displayResult = servicePODetailVMs.Skip(dataTableParams.iDisplayStart)
                        .Take(dataTableParams.iDisplayLength).ToList();

                    var totalRecords = servicePODetailVMs.Count();

                    return Json(new
                    {
                        dataTableParams.sEcho,
                        iTotalRecords = totalRecords,
                        iTotalDisplayRecords = totalRecords,
                        aaData = displayResult
                    });

                }
                else
                {
                    _notify.Error(_localizer["Service purchase orders data could not be loaded."]);
                    return null;
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return null;
            }
        }

        [Authorize(Policy = Permissions.PurchaseOrder.View)]
        public async Task<JsonResult> OnGetCreateOrEdit(int id = 0)
        {
            if (id == 0)
            {
                // For create
                var servicePOHeaderViewModel = new ServicePOHeaderViewModel();

                servicePOHeaderViewModel.Date = DateTime.Now.Date.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute);

                var servicePOHeaders = _servicePOHeaderViewManager.FindBySpecification(new ServicePOHeaderSpecification());
                if (servicePOHeaders.Result.Succeeded)
                {
                    var servicePOHeadersVMs = _mapper.Map<List<ServicePOHeaderViewModel>>(servicePOHeaders.Result.Data);
                    if (servicePOHeadersVMs.Count > 0)
                    {
                        servicePOHeaderViewModel.Number = servicePOHeadersVMs.Max(poh => poh.Id);
                        servicePOHeaderViewModel.Number += 1;
                    }
                    else
                    {
                        servicePOHeaderViewModel.Number = 1;
                    }
                }

                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("CreateOrEdit", servicePOHeaderViewModel) });
            }
            else
            {
                // For view and update
                var getSPOHeaderById = await _servicePOHeaderViewManager.FindBySpecification(new ServicePOHeaderWithAllSpecifications(id));
                if (getSPOHeaderById.Succeeded)
                {
                    var servicePOHeaderViewModel = _mapper.Map<ServicePOHeaderViewModel>(getSPOHeaderById.Data.First());

                    // Seteo la unidad de cotizacion en caso de que sea nula, y se haya seteado despues en la RelProviderStation.
                    if (servicePOHeaderViewModel.ServicePODetails.Any(spod => spod.UnitMeasureId == null))
                    {
                        var getRPS = await _relProviderStationViewManager.FindBySpecification(new RelProviderStationByProviderAndStationSpecification(servicePOHeaderViewModel.ProviderId, servicePOHeaderViewModel.StationId.Value));
                        if (getRPS.Succeeded)
                        {
                            if (getRPS.Data.Count > 0)
                            {
                                var relProviderStationVM = _mapper.Map<RelProviderStationViewModel>(getRPS.Data.First());
                                foreach (var spod in servicePOHeaderViewModel.ServicePODetails)
                                {
                                    if (spod.UnitMeasureId == null && relProviderStationVM.PriceUnitMeasureId != null)
                                    {
                                        spod.UnitMeasureId = relProviderStationVM.PriceUnitMeasureId;
                                        spod.UnitMeasure = relProviderStationVM.PriceUnitMeasure;
                                        spod.UnitPrice = relProviderStationVM.DollarPrice;
                                    }
                                }
                            }
                        }
                    }

                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("CreateOrEdit", servicePOHeaderViewModel) });
                }
            }
            return null;
        }

        public async Task<JsonResult> _LoadProvidersSelect2(string search)
        {
            var providersResponse = await _providerViewManager.FindBySpecification(new ProvidersGetAllSpecification());
            if (providersResponse.Succeeded)
            {
                List<Select2ViewModel> mapSelect2Data = new List<Select2ViewModel>();
                var providersViewModel = _mapper.Map<List<ProviderViewModel>>(providersResponse.Data.Where(pr => pr.ProviderType == "Servicios"));
                foreach (var provider in providersViewModel)
                {
                    mapSelect2Data.Add(new Select2ViewModel() { Id = provider.Id, Text = provider.getBussinessNameOrName });
                }

                if (!(string.IsNullOrEmpty(search) || string.IsNullOrWhiteSpace(search)))
                {
                    mapSelect2Data = mapSelect2Data.Where(x => x.Text.ToLower().Contains(search.ToLower())).ToList();
                }
                return new JsonResult(new { providers = mapSelect2Data.OrderBy(x => x.Text) });
            }
            else
            {
                return null;
            }
        }

        public async Task<JsonResult> _LoadProviderStationsSelect2(string search, int providerId)
        {
            if (providerId != 0)
            {
                var relStationProviderResponse = await _relProviderStationViewManager.FindBySpecification(new RelProviderStationByProviderIdSpecification(providerId));
                if (relStationProviderResponse.Succeeded)
                {
                    List<Select2ViewModel> mapSelect2Data = new List<Select2ViewModel>();
                    var stationsViewModel = new List<StationViewModel>();
                    foreach (var rps in relStationProviderResponse.Data)
                    {
                        var stVM = _mapper.Map<StationViewModel>(rps.Station);
                        stationsViewModel.Add(stVM);
                    }
                    foreach (var st in stationsViewModel)
                    {
                        mapSelect2Data.Add(new Select2ViewModel() { Id = st.Id, Text = st.AbreviationDescription });
                    }

                    if (!(string.IsNullOrEmpty(search) || string.IsNullOrWhiteSpace(search)))
                    {
                        mapSelect2Data = mapSelect2Data.Where(x => x.Text.ToLower().Contains(search.ToLower())).ToList();
                    }
                    return new JsonResult(new { stations = mapSelect2Data.OrderBy(x => x.Text) });
                }
                else
                {
                    return null;
                }
            }
            else
            {
                _notify.Warning(_localizer["You must select a provider."]);
                return null;
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreate(int id, ServicePOHeaderViewModel servicePOHeaderViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (id == 0)
                    {
                        if (servicePOHeaderViewModel.ProviderId <= 0)
                        {
                            _notify.Warning(_localizer["You must select a provider."]);
                            return new JsonResult(new { isValid = false, html = await _viewRenderer.RenderViewToStringAsync("CreateOrEdit", servicePOHeaderViewModel) });
                        }

                        // Create
                        var detailsServicePO = servicePOHeaderViewModel.ServicePODetails.Where(x => x != null).ToList();
                        if (detailsServicePO.Count() > 0)
                        {
                            servicePOHeaderViewModel.ServicePODetails = detailsServicePO;

                            var servicePOHeaderDTO = _mapper.Map<ServicePOHeaderDTO>(servicePOHeaderViewModel);
                            var result = await _servicePOHeaderViewManager.CreateAsync(servicePOHeaderDTO);
                            if (result.Succeeded)
                            {
                                id = result.Data;

                                _notify.Success(_localizer["Service purchase order created."]);
                                return new JsonResult(new { isValid = true, spohId = id });
                            }
                            else
                            {
                                _notify.Error(result.Message);
                                return new JsonResult(new { isValid = false, html = await _viewRenderer.RenderViewToStringAsync("CreateOrEdit", servicePOHeaderViewModel) });
                            }
                        }
                        else
                        {
                            _notify.Warning(_localizer["You must add detail for this purchase order."]);
                            return new JsonResult(new { isValid = false, html = await _viewRenderer.RenderViewToStringAsync("CreateOrEdit", servicePOHeaderViewModel) });
                        }
                    }
                    else
                    {
                        // Update
                        var servicePOHeaderDTO = _mapper.Map<ServicePOHeaderDTO>(servicePOHeaderViewModel);
                        var result = await _servicePOHeaderViewManager.UpdateAsync(servicePOHeaderDTO);
                        if (result.Succeeded)
                        {
                            _notify.Success(_localizer["Service purchase order updated."]);
                            return new JsonResult(new { isValid = true, spohId = id });
                        }
                        else
                        {
                            _notify.Error(result.Message);
                            return new JsonResult(new { isValid = false, html = await _viewRenderer.RenderViewToStringAsync("CreateOrEdit", servicePOHeaderViewModel) });
                        }
                    }
                }
                else
                {
                    return new JsonResult(new { isValid = false, html = await _viewRenderer.RenderViewToStringAsync("CreateOrEdit", servicePOHeaderViewModel) });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return new JsonResult(new { isValid = false, html = await _viewRenderer.RenderViewToStringAsync("CreateOrEdit", servicePOHeaderViewModel) });
            }
        }

        // Only for Superadmin user role (validate in view)
        [HttpPost]
        public async Task<JsonResult> OnPostDelete(int spodId, int spohId)
        {
            var deleteCommand = await _servicePODetailViewManager.DeleteAsync(spodId);
            if (deleteCommand.Succeeded)
            {
                var spodBySPOHId = await _servicePODetailViewManager.FindBySpecification(new ServicePODetailBySPOHIdSpecification(spohId));
                if (spodBySPOHId.Succeeded)
                {
                    if (spodBySPOHId.Data.Count == 0)
                    {
                        await _servicePOHeaderViewManager.DeleteAsync(spohId);
                    }

                    _notify.Information(_localizer["Service purchase order deleted."]);
                    return new JsonResult(new { isValid = true });
                }
                else
                {
                    return new JsonResult(new { isValid = false });
                }
            }
            else
            {
                _notify.Error(deleteCommand.Message);
                return new JsonResult(new { isValid = false });
            }
        }

        public async Task<IActionResult> ExportToPdf(int spohId)
        {
            try
            {
                var response = await _servicePOHeaderViewManager.FindBySpecification(new ServicePOHeaderWithAllSpecifications(spohId));
                if (response.Succeeded)
                {
                    var servicePOHeaderVM = _mapper.Map<ServicePOHeaderViewModel>(response.Data.FirstOrDefault());

                    servicePOHeaderVM.ServicePODetails = servicePOHeaderVM.ServicePODetails.OrderBy(spod => spod.Id).ToList();

                    return new ViewAsPdf("ServicePOPdfReport", servicePOHeaderVM)
                    {
                        PageSize = Rotativa.AspNetCore.Options.Size.A4,
                        PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                        PageMargins = new Rotativa.AspNetCore.Options.Margins(2, 4, 2, 4),
                        FileName = "Orden de compra de servicio_" + servicePOHeaderVM.Id.ToString("D9") + "_" + servicePOHeaderVM.Provider.getBussinessNameOrName.ToString() + ".pdf"
                    };
                }
                else
                {
                    _notify.Warning(_localizer["Could not export to PDF."].ToString());
                    return View("Index");
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return View("Index");
            }
        }


    }
}
