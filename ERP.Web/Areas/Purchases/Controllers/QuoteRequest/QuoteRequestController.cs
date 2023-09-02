using ERP.Application.Constants;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Application.DTOs.Entities.Purchases.MissingProducts;
using ERP.Application.DTOs.Entities.Purchases.QuoteRequests;
using ERP.Application.Interfaces.ViewManagers.Commons;
using ERP.Application.Interfaces.ViewManagers.ProductMod;
using ERP.Application.Interfaces.ViewManagers.Purchases.MissingProducts;
using ERP.Application.Interfaces.ViewManagers.Purchases.Providers;
using ERP.Application.Interfaces.ViewManagers.Purchases.PurchaseOrders;
using ERP.Application.Interfaces.ViewManagers.Purchases.QuoteRequests;
using ERP.Application.Specification.Commons.UnitMeasureSpecifications;
using ERP.Application.Specification.ProductMod;
using ERP.Application.Specification.Purchases.MissingProducts;
using ERP.Application.Specification.Purchases.ProviderProduct;
using ERP.Application.Specification.Purchases.PurchaseOrders;
using ERP.Application.Specification.Purchases.QuoteRequests;
using ERP.Domain.Entities.Purchases.QuoteRequests;
using ERP.Language;
using ERP.Web.Abstractions;
using ERP.Web.Areas.ProductMod.Models;
using ERP.Web.Areas.Purchases.Models.MissingProduct;
using ERP.Web.Areas.Purchases.Models.Providers;
using ERP.Web.Areas.Purchases.Models.PurchaseOrder;
using ERP.Web.Areas.Purchases.Models.QuoteRequest;
using ERP.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using OfficeOpenXml;
using Rotativa.AspNetCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Web.Areas.Purchases.Controllers.QuoteRequest
{
    [Area("Purchases")]
    public class QuoteRequestController : BaseController<QuoteRequestController>
    {
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly IQuoteRequestHeaderViewManager _quoteRequestHeaderViewManager;
        private readonly IQuoteRequestDetailViewManager _quoteRequestDetailViewManager;
        private readonly IRelQuoteRequestProviderViewManager _relQuoteRequestProviderViewManager;
        private readonly IProductViewManager _productViewManager;
        private readonly IMissingProductViewManager _missingProductViewManager;
        private readonly IRelProviderProductViewManager _relProviderProductViewManager;
        private readonly IQuoteRequestResponseHeaderViewManager _quoteRequestResponseHeaderViewManager;
        private readonly IQuoteRequestResponseDetailViewManager _quoteRequestResponseDetailViewManager;
        private readonly IPurchaseOrderHeaderViewManager _purchaseOrderHeaderViewManager;
        private readonly IPurchaseOrderDetailViewManager _purchaseOrderDetailViewManager;
        private readonly IUnitMeasureViewManager _unitMeasureViewManager;

        public QuoteRequestController(IStringLocalizer<SharedResource> localizer, IQuoteRequestHeaderViewManager quoteRequestHeaderViewManager, IQuoteRequestDetailViewManager quoteRequestDetailViewManager, IRelQuoteRequestProviderViewManager relQuoteRequestProviderViewManager, IProductViewManager productViewManager, IMissingProductViewManager missingProductViewManager, IRelProviderProductViewManager relProviderProductViewManager, IQuoteRequestResponseHeaderViewManager quoteRequestResponseHeaderViewManager, IQuoteRequestResponseDetailViewManager quoteRequestResponseDetailViewManager, IPurchaseOrderHeaderViewManager purchaseOrderHeaderViewManager, IPurchaseOrderDetailViewManager purchaseOrderDetailViewManager, IUnitMeasureViewManager unitMeasureViewManager)
        {
            _localizer = localizer;
            _quoteRequestHeaderViewManager = quoteRequestHeaderViewManager;
            _quoteRequestDetailViewManager = quoteRequestDetailViewManager;
            _relQuoteRequestProviderViewManager = relQuoteRequestProviderViewManager;
            _productViewManager = productViewManager;
            _missingProductViewManager = missingProductViewManager;
            _relProviderProductViewManager = relProviderProductViewManager;
            _quoteRequestResponseHeaderViewManager = quoteRequestResponseHeaderViewManager;
            _quoteRequestResponseDetailViewManager = quoteRequestResponseDetailViewManager;
            _purchaseOrderHeaderViewManager = purchaseOrderHeaderViewManager;
            _purchaseOrderDetailViewManager = purchaseOrderDetailViewManager;
            _unitMeasureViewManager = unitMeasureViewManager;
        }

        #region QuoteRequests

        public IActionResult _Index()
        {
            var model = new QuoteRequestHeaderViewModel();
            return View(model);
        }

        public async Task<IActionResult> _LoadAll(DatatableParamsViewModel dataTableParams,
        string sSearch_2, string sSearch_3, string sSearch_4, string sSearch_5, string sSearch_6, string sSearch_7, string sSearch_8, string sSearch_9, string sSearch_10,
        DateTime dateFromFilter, DateTime dateToFilter)
        {
            try
            {
                var response = await _quoteRequestDetailViewManager.FindBySpecification(new QuoteRequestDetailWithAllSpecifications());
                if (response.Succeeded)
                {
                    var quoteRequestDetailViewModels = _mapper.Map<List<QuoteRequestDetailViewModel>>(response.Data);

                    // From date -> To date
                    quoteRequestDetailViewModels = quoteRequestDetailViewModels.Where(qrd => qrd.QuoteRequestHeader.Date.Date >= dateFromFilter && qrd.QuoteRequestHeader.Date.Date <= dateToFilter).ToList();

                    if (!string.IsNullOrEmpty(sSearch_2))
                    {
                        quoteRequestDetailViewModels = quoteRequestDetailViewModels.Where(qrd => qrd.QuoteRequestHeader.Id.ToString().Contains(sSearch_2)).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_3))
                    {
                        quoteRequestDetailViewModels = quoteRequestDetailViewModels.Where(qrd => qrd.QuoteRequestHeader.getQuoteRequestResponseNumbers.Contains(sSearch_3)).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_4))
                    {
                        quoteRequestDetailViewModels = quoteRequestDetailViewModels.Where(qrd => qrd.QuoteRequestHeader.Date.ToString("dd/MM/yyyy").Contains(sSearch_4)).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_5))
                    {
                        quoteRequestDetailViewModels = quoteRequestDetailViewModels.Where(qrd => qrd.Product != null && qrd.Product.Code.ToString().ToLower().Contains(sSearch_5.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_6))
                    {
                        quoteRequestDetailViewModels = quoteRequestDetailViewModels.Where(qrd => qrd.Product != null && qrd.Product.Description.ToString().ToLower().Contains(sSearch_6.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_7))
                    {
                        quoteRequestDetailViewModels = quoteRequestDetailViewModels.Where(qrd => qrd.QuantityToOrder.ToString().ToLower().Contains(sSearch_7.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_8))
                    {
                        quoteRequestDetailViewModels = quoteRequestDetailViewModels.Where(qrd => qrd.QuantityToOrderUnitMeasure != null && qrd.QuantityToOrderUnitMeasure.Description.ToString().ToLower().Contains(sSearch_8.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_9))
                    {
                        quoteRequestDetailViewModels = quoteRequestDetailViewModels.Where(qrd => qrd.QuoteRequestHeader.getRelQuoteRequestProviders.ToString().ToLower().Contains(sSearch_9.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_10))
                    {
                        quoteRequestDetailViewModels = quoteRequestDetailViewModels.Where(qrd => qrd.PurchaseState.Name.ToString().ToLower().Contains(sSearch_10.ToLower())).ToList();
                    }

                    var sortColumnIndex = dataTableParams.iSortCol_0;
                    var sortDirection = dataTableParams.sSortDir_0;

                    if (sortColumnIndex == 2)
                    {
                        quoteRequestDetailViewModels = sortDirection == "asc" ? quoteRequestDetailViewModels.OrderBy(qrd => qrd.Id).ToList() : quoteRequestDetailViewModels.OrderByDescending(qrd => qrd.Id).ToList();
                    }
                    else if (sortColumnIndex == 3)
                    {
                        quoteRequestDetailViewModels = sortDirection == "asc" ? quoteRequestDetailViewModels.OrderBy(qrd => qrd.QuoteRequestHeader.getQuoteRequestResponseNumbers).ToList() : quoteRequestDetailViewModels.OrderByDescending(qrd => qrd.QuoteRequestHeader.getQuoteRequestResponseNumbers).ToList();
                    }
                    else if (sortColumnIndex == 4)
                    {
                        quoteRequestDetailViewModels = sortDirection == "asc" ? quoteRequestDetailViewModels.OrderBy(qrd => qrd.QuoteRequestHeader.Date).ToList() : quoteRequestDetailViewModels.OrderByDescending(qrd => qrd.QuoteRequestHeader.Date).ToList();
                    }
                    else if (sortColumnIndex == 5)
                    {
                        quoteRequestDetailViewModels = sortDirection == "asc" ? quoteRequestDetailViewModels.OrderBy(qrd => qrd.Product != null ? qrd.Product.Code.ToString() : null).ToList() : quoteRequestDetailViewModels.OrderByDescending(qrd => qrd.Product != null ? qrd.Product.Code.ToString() : null).ToList();
                    }
                    else if (sortColumnIndex == 6)
                    {
                        quoteRequestDetailViewModels = sortDirection == "asc" ? quoteRequestDetailViewModels.OrderBy(qrd => qrd.Product != null ? qrd.Product.Description.ToString() : null).ToList() : quoteRequestDetailViewModels.OrderByDescending(qrd => qrd.Product != null ? qrd.Product.Description.ToString() : null).ToList();
                    }
                    else if (sortColumnIndex == 7)
                    {
                        quoteRequestDetailViewModels = sortDirection == "asc" ? quoteRequestDetailViewModels.OrderBy(qrd => qrd.QuantityToOrder).ToList() : quoteRequestDetailViewModels.OrderByDescending(qrd => qrd.QuantityToOrder).ToList();
                    }
                    else if (sortColumnIndex == 8)
                    {
                        quoteRequestDetailViewModels = sortDirection == "asc" ? quoteRequestDetailViewModels.OrderBy(qrd => qrd.QuantityToOrderUnitMeasure != null ? qrd.QuantityToOrderUnitMeasure.Description.ToString() : null).ToList() : quoteRequestDetailViewModels.OrderByDescending(qrd => qrd.QuantityToOrderUnitMeasure != null ? qrd.QuantityToOrderUnitMeasure.Description.ToString() : null).ToList();
                    }
                    else if (sortColumnIndex == 9)
                    {
                        quoteRequestDetailViewModels = sortDirection == "asc" ? quoteRequestDetailViewModels.OrderBy(qrd => qrd.QuoteRequestHeader.getRelQuoteRequestProviders).ToList() : quoteRequestDetailViewModels.OrderByDescending(qrd => qrd.QuoteRequestHeader.getRelQuoteRequestProviders).ToList();
                    }
                    else if (sortColumnIndex == 10)
                    {
                        quoteRequestDetailViewModels = sortDirection == "asc" ? quoteRequestDetailViewModels.OrderBy(qrd => qrd.PurchaseState.Name.ToString()).ToList() : quoteRequestDetailViewModels.OrderByDescending(qrd => qrd.PurchaseState.Name.ToString()).ToList();
                    }

                    var displayResult = quoteRequestDetailViewModels.Skip(dataTableParams.iDisplayStart)
                        .Take(dataTableParams.iDisplayLength).ToList();

                    var totalRecords = quoteRequestDetailViewModels.Count();

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
                    _notify.Error(_localizer["Quote requests data could not be loaded."]);
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
        [HttpPost]
        public async Task<JsonResult> CreateQuoteRequestFromMissingProducts(List<int> missingProductsIds)
        {
            var existsPurchasedMissingProduct = false;
            var quoteRequestHeaderViewModel = new QuoteRequestHeaderViewModel();
            if (missingProductsIds != null)
            {
                if (missingProductsIds.Count > 0)
                {
                    List<MissingProductViewModel> missingProductViewModels = new List<MissingProductViewModel>();
                    foreach (var id in missingProductsIds)
                    {
                        var missingProductDTO = await _missingProductViewManager.FindWithAllSpecification(new MissingProductByIdSpecification(id));
                        if (missingProductDTO.Succeeded)
                        {
                            var missingProductsVM = _mapper.Map<List<MissingProductViewModel>>(missingProductDTO.Data);
                            foreach (var mp in missingProductsVM)
                            {
                                //if (mp.StateMissingProductId != 4 && mp.StateMissingProductId != 5)
                                //if (mp.StateMissingProductId == 1)
                                if (mp.StateMissingProductId < 4)
                                {
                                    missingProductViewModels.Add(mp);
                                }
                                else
                                {
                                    existsPurchasedMissingProduct = true;
                                }
                            }
                        }
                    }

                    // For create
                    quoteRequestHeaderViewModel.Date = DateTime.Now.Date.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute);

                    var quoteRequestHeaders = _quoteRequestHeaderViewManager.FindBySpecification(new QuoteRequestHeaderSpecification());
                    if (quoteRequestHeaders.Result.Succeeded)
                    {
                        var quoteRequestHeadersVM = _mapper.Map<List<QuoteRequestHeaderViewModel>>(quoteRequestHeaders.Result.Data);
                        if (quoteRequestHeadersVM.Count > 0)
                        {
                            quoteRequestHeaderViewModel.Number = quoteRequestHeadersVM.Max(qrh => qrh.Id);
                            quoteRequestHeaderViewModel.Number += 1;
                        }
                        else
                        {
                            quoteRequestHeaderViewModel.Number = 1;
                        }
                    }

                    quoteRequestHeaderViewModel.isGenerateMissingProducts = true;

                    // Tengo que setear los detalles de acuerdo a cada producto faltante en missingProductViewModels
                    foreach (var mp in missingProductViewModels)
                    {
                        var quoteRequestDetailVM = new QuoteRequestDetailViewModel();

                        quoteRequestDetailVM.Product = mp.Product;
                        quoteRequestDetailVM.ProductId = mp.ProductId;
                        quoteRequestDetailVM.QuantityToOrder = mp.QuantityToOrder;
                        quoteRequestDetailVM.QuantityToOrderUnitMeasure = mp.QuantityToOrderUnitMeasure;
                        quoteRequestDetailVM.QuantityToOrderUnitMeasureId = mp.QuantityToOrderUnitMeasureId;

                        quoteRequestDetailVM.MissingProductId = mp.Id;

                        quoteRequestHeaderViewModel.QuoteRequestDetails.Add(quoteRequestDetailVM);
                    }

                    if (existsPurchasedMissingProduct)
                    {
                        //_notify.Warning(_localizer["Attention, some products may not have been added because their status not is \"Pending\"."]);
                        //_notify.Warning(_localizer["Attention, some products may not have been added because their status is \"Purchased\"."]);
                        _notify.Warning(_localizer["Attention, make sure to select products where their status is \"Pending\", \"To quote\" or \"Quoted\"."]);
                        return new JsonResult(new { isValid = false, html = await _viewRenderer.RenderViewToStringAsync("_Create", quoteRequestHeaderViewModel) });
                    }
                    else
                    {
                        return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_Create", quoteRequestHeaderViewModel) });
                    }
                }
                else
                {
                    var html = await _viewRenderer.RenderViewToStringAsync("_Create", quoteRequestHeaderViewModel);
                    return new JsonResult(new { isValid = false, html = html });
                }
            }
            else
            {
                var html = await _viewRenderer.RenderViewToStringAsync("_Create", quoteRequestHeaderViewModel);
                return new JsonResult(new { isValid = false, html = html });
            }
        }

        public async Task<IActionResult> ExportToPdf(int qrhId, int providerId)
        {
            try
            {
                var response = await _quoteRequestHeaderViewManager.FindBySpecification(new QuoteRequestHeaderWithAllSpecifications(qrhId));
                if (response.Succeeded)
                {
                    var quoteRequestHeaderViewModel = _mapper.Map<QuoteRequestHeaderViewModel>(response.Data.FirstOrDefault());

                    //quoteRequestHeaderViewModel.QuoteRequestDetails = quoteRequestHeaderViewModel.QuoteRequestDetails.OrderBy(qrd => qrd.Id).ToList();

                    var quoteRequestDetailAux = new List<QuoteRequestDetailViewModel>();
                    var getRelProviderProductByProviderId = await _relProviderProductViewManager.FindBySpecification(new RelProviderProductByProviderIdSpecification(providerId));
                    if (getRelProviderProductByProviderId.Succeeded)
                    {
                        var rpp = _mapper.Map<RelProviderProductViewModel>(getRelProviderProductByProviderId.Data.First());
                        quoteRequestHeaderViewModel.ProviderNameOrBussinessName = rpp.Provider.getBussinessNameOrName;
                        quoteRequestHeaderViewModel.ProviderDocumentTypeDescription = rpp.Provider.DocumentType == null ? "" : rpp.Provider.DocumentType.Description;
                        quoteRequestHeaderViewModel.ProviderDocumentNumber = rpp.Provider.DocumentNumber == null ? "" : rpp.Provider.DocumentNumber;
                        quoteRequestHeaderViewModel.ProviderIVAConditionDescription = rpp.Provider.IVACondition == null ? "" : rpp.Provider.IVACondition.Description;
                        quoteRequestHeaderViewModel.ProviderAddress = rpp.Provider.Address == null ? "" : rpp.Provider.Address;
                        quoteRequestHeaderViewModel.ProviderPostalCode = rpp.Provider.PostalCode == null ? "" : rpp.Provider.PostalCode;
                        quoteRequestHeaderViewModel.ProviderCity = rpp.Provider.City == null ? "" : rpp.Provider.City.Name;
                        quoteRequestHeaderViewModel.ProviderState = rpp.Provider.State == null ? "" : rpp.Provider.State.Name;
                        quoteRequestHeaderViewModel.ProviderCountry = rpp.Provider.Country == null ? "" : rpp.Provider.Country.Description;


                        var relProviderProductViewModels = _mapper.Map<List<RelProviderProductViewModel>>(getRelProviderProductByProviderId.Data);
                        foreach (var relProviderProductViewModel in relProviderProductViewModels)
                        {
                            foreach (var qrd in quoteRequestHeaderViewModel.QuoteRequestDetails)
                            {
                                if (relProviderProductViewModel.ProductId == qrd.ProductId)
                                {
                                    quoteRequestDetailAux.Add(qrd);
                                }
                            }
                        }
                    }

                    quoteRequestHeaderViewModel.QuoteRequestDetails = quoteRequestDetailAux.OrderBy(qrd => qrd.Id).ToList();

                    return new ViewAsPdf("_QuoteRequestPdfReport", quoteRequestHeaderViewModel)
                    {
                        PageSize = Rotativa.AspNetCore.Options.Size.A4,
                        PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                        PageMargins = new Rotativa.AspNetCore.Options.Margins(2, 4, 2, 4),
                        FileName = "Solicitud de cotización_" + quoteRequestHeaderViewModel.Id.ToString("D9") + "_" + quoteRequestHeaderViewModel.ProviderNameOrBussinessName + ".pdf"
                    };
                }
                else
                {
                    _notify.Warning(_localizer["Could not export to PDF."].ToString());
                    return View("_Index");
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return View("_Index");
            }
        }

        [Authorize(Policy = Permissions.QuoteRequest.View)]
        public async Task<JsonResult> OnGetCreate(int id = 0)
        {
            if (id == 0)
            {
                // For create
                var quoteRequestHeader = new QuoteRequestHeaderViewModel();

                quoteRequestHeader.Date = DateTime.Now.Date.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute);

                var quoteRequestHeaders = _quoteRequestHeaderViewManager.FindBySpecification(new QuoteRequestHeaderSpecification());
                if (quoteRequestHeaders.Result.Succeeded)
                {
                    var quoteRequestHeadersVM = _mapper.Map<List<QuoteRequestHeaderViewModel>>(quoteRequestHeaders.Result.Data);
                    if (quoteRequestHeadersVM.Count > 0)
                    {
                        quoteRequestHeader.Number = quoteRequestHeadersVM.Max(qrh => qrh.Id);
                        quoteRequestHeader.Number += 1;
                    }
                    else
                    {
                        quoteRequestHeader.Number = 1;
                    }
                }

                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_Create", quoteRequestHeader) });
            }
            else
            {
                // For view and update
                var getQRByHeaderId = await _quoteRequestHeaderViewManager.FindBySpecification(new QuoteRequestHeaderWithAllSpecifications(id));
                if (getQRByHeaderId.Succeeded)
                {
                    var quoteRequestViewModel = _mapper.Map<QuoteRequestHeaderViewModel>(getQRByHeaderId.Data.First());

                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_Create", quoteRequestViewModel) });
                }
            }
            return null;
        }

        // Método que se encarga de crear una solicitud de cotización y además actualizar el estado de los productos faltantes incluidos al estado "A cotizar".
        [HttpPost]
        public async Task<JsonResult> OnPostCreate(int id, int[] Providers, QuoteRequestHeaderViewModel quoteRequestHeaderViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (id == 0)
                    {
                        // Create
                        if (Providers.Length > 0)
                        {
                            var detailsQuoteRequest = quoteRequestHeaderViewModel.QuoteRequestDetails.Where(x => x != null).ToList();
                            if (detailsQuoteRequest.Count() > 0)
                            {
                                quoteRequestHeaderViewModel.QuoteRequestDetails = detailsQuoteRequest;

                                // RelQuoteRequestProvider
                                foreach (var providerId in Providers)
                                {
                                    var relQuoteRequestProviderViewModel = new RelQuoteRequestProviderViewModel();
                                    relQuoteRequestProviderViewModel.QuoteRequestHeaderId = id;
                                    relQuoteRequestProviderViewModel.ProviderId = providerId;
                                    quoteRequestHeaderViewModel.RelQuoteRequestProviders.Add(relQuoteRequestProviderViewModel);
                                }

                                var relQuoteRequestProviders = _mapper.Map<List<RelQuoteRequestProvider>>(quoteRequestHeaderViewModel.RelQuoteRequestProviders);

                                var quoteRequestHeaderDTO = _mapper.Map<QuoteRequestHeaderDTO>(quoteRequestHeaderViewModel);
                                quoteRequestHeaderDTO.RelQuoteRequestProviders = relQuoteRequestProviders;
                                var result = await _quoteRequestHeaderViewManager.CreateAsync(quoteRequestHeaderDTO);
                                if (result.Succeeded)
                                {
                                    id = result.Data;

                                    _notify.Success(_localizer["Quote request created.", result.Data]);
                                    return new JsonResult(new { isValid = true, qrhId = id });
                                }
                                else
                                {
                                    _notify.Error(result.Message);
                                    return new JsonResult(new { isValid = false });
                                }

                            }
                            else
                            {
                                _notify.Warning(_localizer["You must add detail for this quote request."]);
                                var html = await _viewRenderer.RenderViewToStringAsync("_Create", quoteRequestHeaderViewModel);
                                return new JsonResult(new { isValid = false, html = html });
                            }
                        }
                        else
                        {
                            _notify.Warning(_localizer["You must add providers for this quote request."]);
                            var html = await _viewRenderer.RenderViewToStringAsync("_Create", quoteRequestHeaderViewModel);
                            return new JsonResult(new { isValid = false, html = html });
                        }
                    }
                    else
                    {
                        // Update
                        if (Providers.Length > 0)
                        {
                            var detailsQuoteRequest = quoteRequestHeaderViewModel.QuoteRequestDetails.Where(x => x != null).ToList();
                            if (detailsQuoteRequest.Count() > 0)
                            {
                                quoteRequestHeaderViewModel.QuoteRequestDetails = detailsQuoteRequest;

                                // RelQuoteRequestProvider
                                foreach (var providerId in Providers)
                                {
                                    var relQuoteRequestProviderViewModel = new RelQuoteRequestProviderViewModel();
                                    relQuoteRequestProviderViewModel.QuoteRequestHeaderId = id;
                                    relQuoteRequestProviderViewModel.ProviderId = providerId;
                                    quoteRequestHeaderViewModel.RelQuoteRequestProviders.Add(relQuoteRequestProviderViewModel);
                                }

                                var relQuoteRequestProviders = _mapper.Map<List<RelQuoteRequestProvider>>(quoteRequestHeaderViewModel.RelQuoteRequestProviders);

                                var quoteRequestHeaderDTO = _mapper.Map<QuoteRequestHeaderDTO>(quoteRequestHeaderViewModel);
                                quoteRequestHeaderDTO.RelQuoteRequestProviders = relQuoteRequestProviders;
                                var result = await _quoteRequestHeaderViewManager.UpdateAsync(quoteRequestHeaderDTO);
                                if (result.Succeeded)
                                {
                                    _notify.Success(_localizer["Quote request updated."]);
                                    return new JsonResult(new { isValid = true, qrhId = id });
                                }
                                else
                                {
                                    _notify.Error(result.Message);
                                }

                            }
                            else
                            {
                                _notify.Warning(_localizer["You must add detail for this quote request."]);
                                var html = await _viewRenderer.RenderViewToStringAsync("_Create", quoteRequestHeaderViewModel);
                                return new JsonResult(new { isValid = false, html = html });
                            }
                        }
                        else
                        {
                            _notify.Warning(_localizer["You must add providers for this quote request."]);
                            var html = await _viewRenderer.RenderViewToStringAsync("_Create", quoteRequestHeaderViewModel);
                            return new JsonResult(new { isValid = false, html = html });
                        }
                    }

                    return new JsonResult(new { isValid = false });
                }
                else
                {
                    var html = await _viewRenderer.RenderViewToStringAsync("_Create", quoteRequestHeaderViewModel);
                    return new JsonResult(new { isValid = false, html = html });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                var html = await _viewRenderer.RenderViewToStringAsync("_Create", quoteRequestHeaderViewModel);
                return new JsonResult(new { isValid = false, html = html });
            }
        }

        // Only for Superadmin user role (validate in view)
        [HttpPost]
        public async Task<JsonResult> OnPostDelete(int qrdId, int qrhId)
        {
            var deleteCommand = await _quoteRequestDetailViewManager.DeleteAsync(qrdId);
            if (deleteCommand.Succeeded)
            {
                var qrdByQRHId = await _quoteRequestDetailViewManager.FindBySpecification(new QuoteRequestDetailByQRHIdSpecification(qrhId));
                if (qrdByQRHId.Succeeded)
                {
                    if (qrdByQRHId.Data.Count == 0)
                    {
                        // Tengo que borrar tambien todas sus respuestas de cotizacion antes de borrar toda la solicitud.
                        var getQRRByQRHId = await _quoteRequestResponseHeaderViewManager.FindBySpecification(new QuoteRequestResponseHeaderByQRHeaderIdSpecification(qrhId));
                        foreach (var qrrh in getQRRByQRHId.Data)
                        {
                            await _quoteRequestResponseHeaderViewManager.DeleteAsync(qrrh.Id);
                        }

                        // Borro la solicitud completa o encabezado
                        await _quoteRequestHeaderViewManager.DeleteAsync(qrhId);
                    }

                    _notify.Information(_localizer["Quote request deleted."]);
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

        public async Task<JsonResult> _LoadProvidersByMissingProductsSelect2(string search)
        {
            var missingProductsResponse = await _missingProductViewManager.FindWithAllSpecification(new MissingProductsStateDiscriminationSpecification());
            if (missingProductsResponse.Succeeded)
            {
                List<Select2ViewModel> mapSelect2Data = new List<Select2ViewModel>();
                HashSet<ProviderViewModel> providersViewModel = new HashSet<ProviderViewModel>();
                foreach (var missingProduct in missingProductsResponse.Data)
                {
                    var relProviderProductResponse = await _relProviderProductViewManager.FindBySpecification(new RelProviderProductGetProvidersByProductIdSpecification(missingProduct.ProductId));
                    if (relProviderProductResponse.Succeeded)
                    {
                        foreach (var rpp in relProviderProductResponse.Data)
                        {
                            if (!providersViewModel.Any(prov => prov.Id == rpp.ProviderId))
                            {
                                var providerVM = _mapper.Map<ProviderViewModel>(rpp.Provider);
                                providersViewModel.Add(providerVM);
                            }
                        }
                    }
                }
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

        public async Task<JsonResult> _LoadProvidersByProductIdsSelect2(string search, string productIds)
        {
            var productsIds = productIds.Replace('[', ' ').Replace(']', ' ').Replace('"', ' ').Trim().Split(',');
            List<Select2ViewModel> mapSelect2Data = new List<Select2ViewModel>();
            HashSet<ProviderViewModel> providersViewModel = new HashSet<ProviderViewModel>();
            foreach (var productId in productsIds)
            {
                var relProviderProductResponse = await _relProviderProductViewManager.FindBySpecification(new RelProviderProductGetProvidersByProductIdSpecification(Convert.ToInt32(productId)));
                if (relProviderProductResponse.Succeeded)
                {
                    foreach (var rpp in relProviderProductResponse.Data)
                    {
                        if (!providersViewModel.Any(pv => pv.Id == rpp.ProviderId))
                        {
                            var providerVM = _mapper.Map<ProviderViewModel>(rpp.Provider);
                            providersViewModel.Add(providerVM);
                        }
                    }
                }
            }
            //providersViewModel = providersViewModel.GroupBy(pv => pv.Id).Where(grp => grp.Count() == productsIds.Count()).Select(grp => grp.First()).ToHashSet();
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

        [HttpGet]
        public async Task<JsonResult> GetMissingProductByProductId(int id)
        {
            try
            {
                if (id != -1)
                {
                    var response = await _missingProductViewManager.FindWithAllSpecification(new MissingProductByProductIdSpecification(id));
                    if (response.Succeeded)
                    {
                        var missingProductVM = _mapper.Map<MissingProductViewModel>(response.Data.FirstOrDefault());
                        return new JsonResult(new { isValid = true, missingProductVM = missingProductVM });
                    }
                    else
                    {
                        return new JsonResult(new { isValid = false });
                    }
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

        [HttpGet]
        public async Task<JsonResult> GetProductsByProviderId(int id)
        {
            try
            {
                if (id != -1)
                {
                    var productViewModels = new HashSet<ProductViewModel>();
                    //Tengo que traerme los productos faltantes que NO tienen estado "Comprado" = 4 o "Finalizado" = 5 o "Ingreso parcial" = 6
                    var missingProductsResponse = await _missingProductViewManager.FindWithAllSpecification(new MissingProductsStateDiscriminationSpecification());
                    if (missingProductsResponse.Succeeded)
                    {
                        foreach (var mp in missingProductsResponse.Data)
                        {
                            foreach (var rpp in mp.Product.RelProviderProducts)
                            {
                                if (rpp.ProviderId == id && rpp.ProductId == mp.ProductId)
                                {
                                    var productViewModel = _mapper.Map<ProductViewModel>(mp.Product);
                                    productViewModels.Add(productViewModel);
                                }
                            }

                        }

                        if (productViewModels.Count > 0)
                        {
                            var selectList = new SelectList((from p in productViewModels.OrderBy(p => p.Description).ToList() select new { Id = p.Id, CodeAndDescription = p.Code + " - " + p.Description }), "Id", "CodeAndDescription", null);
                            return new JsonResult(new { isValid = true, data = selectList });
                        }
                        else
                        {
                            return new JsonResult(new { isValid = false, data = "" });
                        }
                    }
                    else
                    {
                        return new JsonResult(new { isValid = false, data = "" });
                    }
                }
                else
                {
                    return new JsonResult(new { isValid = false, data = "" });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return new JsonResult(new { isValid = false, data = "" });
            }
        }

        [HttpGet]
        public async Task<JsonResult> canBeDeletedProduct(int[] providerIds, int[] productIds)
        {
            // providerIds: son los ids de los proveedores que quedan en el select2.
            // productIds: son los ids de los productos que provee el proveedor que se deselecciona del select2.
            //bool canBeDeleted = false;
            List<int> productIdsToDelete = new List<int>();
            try
            {
                foreach (var providerId in providerIds)
                {
                    var getRPPByProviderId = await _relProviderProductViewManager.FindBySpecification(new RelProviderProductByProviderIdSpecification(providerId));
                    if (getRPPByProviderId.Succeeded)
                    {
                        var rppViewModel = _mapper.Map<List<RelProviderProductViewModel>>(getRPPByProviderId.Data);
                        //if (!rppViewModel.Any(rpp => productIds.Contains(rpp.ProductId)))
                        //{
                        //    canBeDeleted = true;
                        //}
                        foreach (var productId in productIds)
                        {
                            if (!rppViewModel.Any(rpp => rpp.ProductId == productId))
                            {
                                productIdsToDelete.Add(productId);
                            }
                        }
                    }
                    else
                    {
                        return new JsonResult(new { productIdsToDelete = productIdsToDelete });
                        //return new JsonResult(new { canBeDeleted = canBeDeleted, productIdsToDelete = productIdsToDelete });
                    }
                }

                return new JsonResult(new { productIdsToDelete = productIdsToDelete });
                //return new JsonResult(new { canBeDeleted = canBeDeleted, productIdsToDelete = productIdsToDelete });
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return new JsonResult(new { productIdsToDelete = productIdsToDelete });
                //return new JsonResult(new { canBeDeleted = canBeDeleted, productIdsToDelete = productIdsToDelete });
            }
        }

        // Fran: 4/5/2023: Se usa para cargar el select2 a la hora de modificar una solicitud de cotizacion.
        [HttpGet]
        public async Task<JsonResult> _LoadMissingProductsByProviderId(string search, int[] providerIds)
        {
            try
            {
                if (providerIds != null)
                {
                    List<Select2ViewModel> mapSelect2Data = new List<Select2ViewModel>();
                    HashSet<ProductViewModel> productViewModels = new HashSet<ProductViewModel>();
                    foreach (var pvId in providerIds)
                    {
                        //Tengo que traerme los productos faltantes que NO tienen estado "Comprado" = 4 o "Finalizado" = 5 o "Ingreso parcial" = 6
                        var missingProductsResponse = await _missingProductViewManager.FindWithAllSpecification(new MissingProductsStateDiscriminationSpecification());
                        if (missingProductsResponse.Succeeded)
                        {
                            foreach (var mp in missingProductsResponse.Data)
                            {
                                foreach (var rpp in mp.Product.RelProviderProducts)
                                {
                                    if (rpp.ProviderId == pvId && rpp.ProductId == mp.ProductId)
                                    {
                                        var productViewModel = _mapper.Map<ProductViewModel>(mp.Product);
                                        productViewModels.Add(productViewModel);
                                    }
                                }

                            }
                        }
                    }

                    foreach (var productVM in productViewModels.GroupBy(pr => pr.Id).Select(x => x.First()))
                    {
                        mapSelect2Data.Add(new Select2ViewModel() { Id = productVM.Id, Text = productVM.CodeAndDescription });
                    }

                    if (!(string.IsNullOrEmpty(search) || string.IsNullOrWhiteSpace(search)))
                    {
                        mapSelect2Data = mapSelect2Data.Where(x => x.Text.ToLower().Contains(search.ToLower())).ToList();
                    }
                    return new JsonResult(new { products = mapSelect2Data });

                }
                else
                {
                    return new JsonResult(new { products = new List<Select2ViewModel>() });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return new JsonResult(new { products = new List<Select2ViewModel>() });
            }
        }

        public async Task<IActionResult> ExportToExcel(string sSearch_2, string sSearch_3, string sSearch_4, string sSearch_5, string sSearch_6,
    string sSearch_7, string sSearch_8, string sSearch_9, string sSearch_10,
    int colIndexOrder, string colOrderDirection, string dateFromFilter, string dateToFilter)
        {
            try
            {
                var response = await _quoteRequestDetailViewManager.FindBySpecification(new QuoteRequestDetailWithAllSpecifications());
                if (response.Succeeded)
                {
                    var quoteRequestDetailViewModels = _mapper.Map<List<QuoteRequestDetailViewModel>>(response.Data);

                    // From date -> To date
                    quoteRequestDetailViewModels = quoteRequestDetailViewModels.Where(qrd => qrd.QuoteRequestHeader.Date >= Convert.ToDateTime(dateFromFilter) && qrd.QuoteRequestHeader.Date <= Convert.ToDateTime(dateToFilter)).ToList();

                    if (!string.IsNullOrEmpty(sSearch_2))
                    {
                        quoteRequestDetailViewModels = quoteRequestDetailViewModels.Where(qrd => qrd.QuoteRequestHeader.Id.ToString().Contains(sSearch_2)).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_3))
                    {
                        quoteRequestDetailViewModels = quoteRequestDetailViewModels.Where(qrd => qrd.QuoteRequestHeader.getQuoteRequestResponseNumbers.Contains(sSearch_3)).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_4))
                    {
                        quoteRequestDetailViewModels = quoteRequestDetailViewModels.Where(qrd => qrd.QuoteRequestHeader.Date.ToString("dd/MM/yyyy").Contains(sSearch_4)).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_5))
                    {
                        quoteRequestDetailViewModels = quoteRequestDetailViewModels.Where(qrd => qrd.Product != null && qrd.Product.Code.ToString().ToLower().Contains(sSearch_5.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_6))
                    {
                        quoteRequestDetailViewModels = quoteRequestDetailViewModels.Where(qrd => qrd.Product != null && qrd.Product.Description.ToString().ToLower().Contains(sSearch_6.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_7))
                    {
                        quoteRequestDetailViewModels = quoteRequestDetailViewModels.Where(qrd => qrd.QuantityToOrder.ToString().ToLower().Contains(sSearch_7.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_8))
                    {
                        quoteRequestDetailViewModels = quoteRequestDetailViewModels.Where(qrd => qrd.QuantityToOrderUnitMeasure != null && qrd.QuantityToOrderUnitMeasure.Description.ToString().ToLower().Contains(sSearch_8.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_9))
                    {
                        quoteRequestDetailViewModels = quoteRequestDetailViewModels.Where(qrd => qrd.QuoteRequestHeader.getRelQuoteRequestProviders.ToString().ToLower().Contains(sSearch_9.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_10))
                    {
                        quoteRequestDetailViewModels = quoteRequestDetailViewModels.Where(qrd => qrd.PurchaseState.Name.ToString().ToLower().Contains(sSearch_10.ToLower())).ToList();
                    }

                    if (colIndexOrder != 0 && colOrderDirection != "")
                    {
                        var sortColumnIndex = colIndexOrder;
                        var sortDirection = colOrderDirection;

                        if (sortColumnIndex == 2)
                        {
                            quoteRequestDetailViewModels = sortDirection == "asc" ? quoteRequestDetailViewModels.OrderBy(qrd => qrd.Id).ToList() : quoteRequestDetailViewModels.OrderByDescending(qrd => qrd.Id).ToList();
                        }
                        else if (sortColumnIndex == 3)
                        {
                            quoteRequestDetailViewModels = sortDirection == "asc" ? quoteRequestDetailViewModels.OrderBy(qrd => qrd.QuoteRequestHeader.getQuoteRequestResponseNumbers).ToList() : quoteRequestDetailViewModels.OrderByDescending(qrd => qrd.QuoteRequestHeader.getQuoteRequestResponseNumbers).ToList();
                        }
                        else if (sortColumnIndex == 4)
                        {
                            quoteRequestDetailViewModels = sortDirection == "asc" ? quoteRequestDetailViewModels.OrderBy(qrd => qrd.QuoteRequestHeader.Date).ToList() : quoteRequestDetailViewModels.OrderByDescending(qrd => qrd.QuoteRequestHeader.Date).ToList();
                        }
                        else if (sortColumnIndex == 5)
                        {
                            quoteRequestDetailViewModels = sortDirection == "asc" ? quoteRequestDetailViewModels.OrderBy(qrd => qrd.Product != null ? qrd.Product.Code.ToString() : null).ToList() : quoteRequestDetailViewModels.OrderByDescending(qrd => qrd.Product != null ? qrd.Product.Code.ToString() : null).ToList();
                        }
                        else if (sortColumnIndex == 6)
                        {
                            quoteRequestDetailViewModels = sortDirection == "asc" ? quoteRequestDetailViewModels.OrderBy(qrd => qrd.Product != null ? qrd.Product.Description.ToString() : null).ToList() : quoteRequestDetailViewModels.OrderByDescending(qrd => qrd.Product != null ? qrd.Product.Description.ToString() : null).ToList();
                        }
                        else if (sortColumnIndex == 7)
                        {
                            quoteRequestDetailViewModels = sortDirection == "asc" ? quoteRequestDetailViewModels.OrderBy(qrd => qrd.QuantityToOrder).ToList() : quoteRequestDetailViewModels.OrderByDescending(qrd => qrd.QuantityToOrder).ToList();
                        }
                        else if (sortColumnIndex == 8)
                        {
                            quoteRequestDetailViewModels = sortDirection == "asc" ? quoteRequestDetailViewModels.OrderBy(qrd => qrd.QuantityToOrderUnitMeasure != null ? qrd.QuantityToOrderUnitMeasure.Description.ToString() : null).ToList() : quoteRequestDetailViewModels.OrderByDescending(qrd => qrd.QuantityToOrderUnitMeasure != null ? qrd.QuantityToOrderUnitMeasure.Description.ToString() : null).ToList();
                        }
                        else if (sortColumnIndex == 9)
                        {
                            quoteRequestDetailViewModels = sortDirection == "asc" ? quoteRequestDetailViewModels.OrderBy(qrd => qrd.QuoteRequestHeader.getRelQuoteRequestProviders).ToList() : quoteRequestDetailViewModels.OrderByDescending(qrd => qrd.QuoteRequestHeader.getRelQuoteRequestProviders).ToList();
                        }
                        else if (sortColumnIndex == 10)
                        {
                            quoteRequestDetailViewModels = sortDirection == "asc" ? quoteRequestDetailViewModels.OrderBy(qrd => qrd.PurchaseState.Name.ToString()).ToList() : quoteRequestDetailViewModels.OrderByDescending(qrd => qrd.PurchaseState.Name.ToString()).ToList();
                        }
                    }

                    return ExportViewModelToExcel(quoteRequestDetailViewModels);
                }
                else
                {
                    _notify.Error(_localizer["Quote requests data could not be loaded."]);
                    return null;
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return null;
            }
        }

        public IActionResult ExportViewModelToExcel(IEnumerable<QuoteRequestDetailViewModel> viewModel)
        {
            try
            {
                var nameForExcelFile = "Solicitudes de cotización_";

                var stream = new MemoryStream();
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var xlPackage = new ExcelPackage(stream))
                {
                    var workSheet = xlPackage.Workbook.Worksheets.Add("Solicitudes de cotización");

                    workSheet.Cells["A1"].Value = _localizer["Nº SC"];
                    workSheet.Cells["B1"].Value = _localizer["Response number"];
                    workSheet.Cells["C1"].Value = _localizer["Date"];
                    workSheet.Cells["D1"].Value = _localizer["Code"];
                    workSheet.Cells["E1"].Value = _localizer["Description"];
                    workSheet.Cells["F1"].Value = _localizer["Quantity to order"];
                    workSheet.Cells["G1"].Value = _localizer["Unit"];
                    workSheet.Cells["H1"].Value = _localizer["Providers"];
                    workSheet.Cells["I1"].Value = _localizer["State"];

                    var row = 2;
                    foreach (var vm in viewModel)
                    {
                        workSheet.Cells[row, 1].Value = vm.QuoteRequestHeader.Id;
                        workSheet.Cells[row, 1].Style.Numberformat.Format = "0";

                        workSheet.Cells[row, 2].Value = vm.QuoteRequestHeader.getQuoteRequestResponseNumbers;

                        workSheet.Cells[row, 3].Value = vm.QuoteRequestHeader.Date;
                        workSheet.Cells[row, 3].Style.Numberformat.Format = "dd-MM-yyyy HH:mm";

                        if (vm.Product != null)
                        {
                            if (vm.Product.Code != null)
                            {
                                workSheet.Cells[row, 4].Value = vm.Product.Code.ToString();
                            }
                            else
                            {
                                workSheet.Cells[row, 4].Value = "";
                            }

                            if (vm.Product.Description != null)
                            {
                                workSheet.Cells[row, 5].Value = vm.Product.Description.ToString();
                            }
                            else
                            {
                                workSheet.Cells[row, 5].Value = "";
                            }
                        }
                        else
                        {
                            workSheet.Cells[row, 4].Value = "";
                            workSheet.Cells[row, 5].Value = "";
                        }

                        workSheet.Cells[row, 6].Value = vm.QuantityToOrder;
                        workSheet.Cells[row, 6].Style.Numberformat.Format = "0.00";

                        if (vm.QuantityToOrderUnitMeasure != null)
                        {
                            if (vm.QuantityToOrderUnitMeasure.Description != null)
                            {
                                workSheet.Cells[row, 7].Value = vm.QuantityToOrderUnitMeasure.Description.ToString();
                            }
                            else
                            {
                                workSheet.Cells[row, 7].Value = "";
                            }
                        }
                        else
                        {
                            workSheet.Cells[row, 7].Value = "";
                        }

                        workSheet.Cells[row, 8].Value = vm.QuoteRequestHeader.getRelQuoteRequestProviders;

                        if (vm.PurchaseState != null)
                        {

                            workSheet.Cells[row, 9].Value = vm.PurchaseState.Name.ToString();

                        }
                        else
                        {
                            workSheet.Cells[row, 9].Value = "";
                        }

                        row++;
                    }

                    workSheet.Cells["A1:I1"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                    workSheet.Cells["I1"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

                    workSheet.Row(1).Style.Font.Bold = true;
                    workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();

                    xlPackage.Workbook.Properties.Title = nameForExcelFile + DateTime.Now.Date.ToString("dd-MM-yyyy");
                    xlPackage.Save();
                }

                stream.Position = 0;
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nameForExcelFile + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xlsx");

            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return null;
            }
        }

        #endregion

        #region QuoteRequestResponses

        [HttpGet]
        public async Task<JsonResult> canGenerateQuoteRequestResponse(int qrhId)
        {
            try
            {
                var getRQRPByHeaderId = await _relQuoteRequestProviderViewManager.FindBySpecification(new RelQuoteRequestProviderGetProviderByHeaderIdSpecification(qrhId));
                if (getRQRPByHeaderId.Succeeded)
                {
                    var getQRRByHeaderId = await _quoteRequestResponseHeaderViewManager.FindBySpecification(new QuoteRequestResponseHeaderByQRHeaderIdSpecification(qrhId));
                    if (getQRRByHeaderId.Succeeded)
                    {
                        if (getQRRByHeaderId.Data.Count == 0)
                        {
                            // No se genero ninguna respuesta para la solicitud de cotizacion original
                            return new JsonResult(new { isValid = true });
                        }
                        else
                        {
                            int relQuoteRequestProviderQuantity = getRQRPByHeaderId.Data.Count();
                            int quoteRequestResponseProviderQuantity = getQRRByHeaderId.Data.GroupBy(x => x.ProviderId).Select(y => y.FirstOrDefault()).Count();
                            if (relQuoteRequestProviderQuantity == quoteRequestResponseProviderQuantity)
                            {
                                // Ya se generaron la misma cantidad de respuestas de cotizacion que la cantidad de proveedores asociados a un encabezado, entonces
                                // no dejo generar otra respuesta.
                                _notify.Information(_localizer["Responses for all providers has already generated."]);
                                return new JsonResult(new { isValid = false });
                            }
                            else
                            {
                                return new JsonResult(new { isValid = true });
                            }
                        }
                    }
                    else
                    {
                        return new JsonResult(new { isValid = false });
                    }
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

        [Authorize(Policy = Permissions.QuoteRequest.View)]
        public async Task<JsonResult> OnGetCreateQuoteRequestResponse(int qrhId, int id = 0)
        {
            if (id == 0)
            {
                // Create
                var quoteRequestResponseHeaderVM = new QuoteRequestResponseHeaderViewModel();

                var quoteRequestResponseHeaders = _quoteRequestResponseHeaderViewManager.FindBySpecification(new QuoteRequestResponseHeaderSpecification());
                if (quoteRequestResponseHeaders.Result.Succeeded)
                {
                    var quoteRequestResponseHeadersVM = _mapper.Map<List<QuoteRequestResponseHeaderViewModel>>(quoteRequestResponseHeaders.Result.Data);
                    if (quoteRequestResponseHeadersVM.Count > 0)
                    {
                        quoteRequestResponseHeaderVM.Number = quoteRequestResponseHeadersVM.Max(qrh => qrh.Id);
                        quoteRequestResponseHeaderVM.Number += 1;
                    }
                    else
                    {
                        quoteRequestResponseHeaderVM.Number = 1;
                    }
                }

                quoteRequestResponseHeaderVM.QuoteRequestHeaderId = qrhId;
                quoteRequestResponseHeaderVM.Date = DateTime.Now.Date.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute);

                // Tengo que setear los detalles a los detalles de respuesta
                var getQuoteRequestDetailsByHeaderId = await _quoteRequestHeaderViewManager.FindBySpecification(new QuoteRequestHeaderWithAllSpecifications(qrhId));
                if (getQuoteRequestDetailsByHeaderId.Succeeded)
                {
                    var quoteRequestHeadersVM = _mapper.Map<QuoteRequestHeaderViewModel>(getQuoteRequestDetailsByHeaderId.Data.FirstOrDefault());

                    foreach (var quoteRequestDetail in quoteRequestHeadersVM.QuoteRequestDetails)
                    {
                        var quoteRequestResponseDetailVM = new QuoteRequestResponseDetailViewModel();

                        if (quoteRequestDetail.MissingProductId.HasValue)
                        {
                            quoteRequestResponseDetailVM.MissingProduct = quoteRequestDetail.MissingProduct;
                            quoteRequestResponseDetailVM.MissingProductId = quoteRequestDetail.MissingProductId.Value;
                        }
                        else
                        {
                            return new JsonResult(new { isValid = false, html = await _viewRenderer.RenderViewToStringAsync("_CreateQuoteRequestResponse", quoteRequestResponseHeaderVM) });
                        }

                        quoteRequestResponseHeaderVM.QuoteRequestResponseDetails.Add(quoteRequestResponseDetailVM);
                    }

                }

                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateQuoteRequestResponse", quoteRequestResponseHeaderVM) });
            }
            else
            {
                // Update
                var getQRRByHeaderId = await _quoteRequestResponseHeaderViewManager.FindBySpecification(new QuoteRequestResponseHeaderWithAllSpecifications(id));
                if (getQRRByHeaderId.Succeeded)
                {
                    var quoteRequestResponseHeaderVM = _mapper.Map<QuoteRequestResponseHeaderViewModel>(getQRRByHeaderId.Data.First());

                    quoteRequestResponseHeaderVM.Number = quoteRequestResponseHeaderVM.Id;

                    HashSet<ProductViewModel> productsViewModel = new HashSet<ProductViewModel>();
                    var relProviderProductsResponse = await _relProviderProductViewManager.FindBySpecification(new RelProviderProductByProviderIdSpecification(quoteRequestResponseHeaderVM.ProviderId));
                    if (relProviderProductsResponse.Succeeded)
                    {
                        foreach (var relProviderProduct in relProviderProductsResponse.Data)
                        {
                            var productVM = _mapper.Map<ProductViewModel>(relProviderProduct.Product);
                            productsViewModel.Add(productVM);
                        }
                    }

                    quoteRequestResponseHeaderVM.AlternativeProducts = new SelectList(productsViewModel.OrderBy(x => x.CodeAndDescription), nameof(ProductViewModel.Id), nameof(ProductViewModel.CodeAndDescription), null, null);

                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateQuoteRequestResponse", quoteRequestResponseHeaderVM) });
                }
            }
            return null;
        }

        public async Task<JsonResult> getQuoteRequestDetailsByQRHId(int qrhId)
        {
            try
            {
                var getQuoteRequestDetailsByQRHId = await _quoteRequestDetailViewManager.FindBySpecification(new QuoteRequestDetailByQRHIdSpecification(qrhId));
                if (getQuoteRequestDetailsByQRHId.Succeeded)
                {
                    var quoteRequestDetailVMs = _mapper.Map<List<QuoteRequestDetailViewModel>>(getQuoteRequestDetailsByQRHId.Data);
                    return new JsonResult(new { isValid = true, quoteRequestDetails = quoteRequestDetailVMs });
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

        public async Task<JsonResult> getUnitsAndQuantity(int providerId, int missingProductId, int productId)
        {
            try
            {
                // Values by default
                decimal providerQuantity = 0;
                int providerUnitMeasureId = 19;
                var getUnitMeasureById = await _unitMeasureViewManager.GetByIdAsync(providerUnitMeasureId);
                string providerUnitMeasureDescription = getUnitMeasureById.Data.Description.ToString();

                decimal quotationQuantity = 0;
                int quotationUnitMeasureId = 19;
                string quotationUnitMeasureDescription = getUnitMeasureById.Data.Description.ToString();

                // Me traigo los datos de productos-proveedores por id de proveedor
                var getRPPByProviderId = await _relProviderProductViewManager.FindBySpecification(new RelProviderProductByProviderIdSpecification(providerId));
                if (getRPPByProviderId.Succeeded)
                {
                    foreach (var rpp in getRPPByProviderId.Data)
                    {
                        ProductDTO getProductById = null;
                        MissingProductDTO missingProductDTO = null;
                        if (missingProductId != 0)
                        {
                            var getMissingProductById = await _missingProductViewManager.GetByIdAsync(missingProductId);
                            getProductById = _productViewManager.FindBySpecification(new ProductByIdWithUnitMeasureSpecification(getMissingProductById.Data.ProductId)).Result.Data.First();
                            missingProductDTO = getMissingProductById.Data;
                        }
                        else
                        {
                            getProductById = _productViewManager.FindBySpecification(new ProductByIdWithUnitMeasureSpecification(productId)).Result.Data.First();
                        }
                        var productVM = _mapper.Map<ProductViewModel>(getProductById);
                        if (rpp.ProductId == productVM.Id)
                        {
                            // Get providerQuantity and providerUnitMeasure
                            if (rpp.UnitMeasureId.HasValue && productVM.QuantityToOrderUnitMeasureId.HasValue)
                            {
                                // Si la unidad del proveedor es igual que la unidad de la cantidad a pedir del producto:
                                if ((rpp.UnitMeasureId.Value == productVM.QuantityToOrderUnitMeasureId.Value) && (productVM.QuantityToOrder.HasValue && productVM.QuantityToOrderUnitMeasure != null && productVM.QuantityToOrderUnitMeasureId.HasValue))
                                {
                                    if (missingProductId != 0 && missingProductDTO != null)
                                    {
                                        providerQuantity = missingProductDTO.QuantityToOrder;
                                    }
                                    else
                                    {
                                        providerQuantity = productVM.QuantityToOrder.Value;
                                    }
                                    //providerQuantity = productVM.QuantityToOrder.Value;
                                    providerUnitMeasureId = rpp.UnitMeasureId.Value;
                                    providerUnitMeasureDescription = rpp.UnitMeasure.Description;
                                }
                                else
                                {
                                    // Si la unidad del proveedor no es igual que la unidad del producto, entonces tengo que convertir teniendo en cuenta la unidad del proveedor.
                                    if (missingProductId != 0 && missingProductDTO != null)
                                    {
                                        providerQuantity = missingProductDTO.QuantityToOrder;

                                        if (missingProductDTO.QuantityToOrder != 0)
                                        {
                                            // Barras o Rollos
                                            if (rpp.UnitMeasureId.Value == 22 || rpp.UnitMeasureId.Value == 23)
                                            {
                                                // Se completa el campo Lenght
                                                if (rpp.Lenght.HasValue && rpp.LengthUnitMeasure != null)
                                                {
                                                    providerQuantity = missingProductDTO.QuantityToOrder / rpp.Lenght.Value;
                                                }
                                                else
                                                {
                                                    _notify.Warning(_localizer["Atención, no se pudo convertir la cantidad utilizando la información almacenada."]);
                                                }

                                                // Si es Barras o Rollos o Bobinas redondeo al entero superior
                                                if (rpp.UnitMeasureId.Value == 22 || rpp.UnitMeasureId.Value == 23)
                                                {
                                                    providerQuantity = Math.Ceiling(providerQuantity);
                                                }
                                            }
                                            // Cajas
                                            else if (rpp.UnitMeasureId.Value == 33)
                                            {
                                                // Se completa el campo lenght
                                                if (rpp.Lenght.HasValue && rpp.LengthUnitMeasure != null)
                                                {
                                                    providerQuantity = missingProductDTO.QuantityToOrder / rpp.Lenght.Value;
                                                }
                                                // o el campo PresentationQuantity
                                                else if (rpp.PresentationQuantity.HasValue && rpp.PresentationUnitMeasure != null)
                                                {
                                                    providerQuantity = missingProductDTO.QuantityToOrder / rpp.PresentationQuantity.Value;
                                                }
                                                else
                                                {
                                                    _notify.Warning(_localizer["Atención, no se pudo convertir la cantidad utilizando la información almacenada."]);
                                                }

                                                providerQuantity = Math.Ceiling(providerQuantity);
                                            }
                                            // Hojas
                                            else if (rpp.UnitMeasureId.Value == 31)
                                            {
                                                // Se completa el campo lenght y width
                                                if (rpp.Lenght.HasValue && rpp.LengthUnitMeasure != null && rpp.Width.HasValue && rpp.WidthUnitMeasure != null)
                                                {
                                                    providerQuantity = missingProductDTO.QuantityToOrder / (rpp.Lenght.Value * rpp.Width.Value);
                                                }
                                                else
                                                {
                                                    _notify.Warning(_localizer["Atención, no se pudo convertir la cantidad utilizando la información almacenada."]);
                                                }

                                                providerQuantity = Math.Ceiling(providerQuantity);
                                            }
                                            // Bidones
                                            else if (rpp.UnitMeasureId.Value == 32)
                                            {
                                                // Se completa el campo PresentationQuantity
                                                if (rpp.PresentationQuantity.HasValue && rpp.PresentationUnitMeasure != null)
                                                {
                                                    providerQuantity = missingProductDTO.QuantityToOrder / rpp.PresentationQuantity.Value;
                                                }
                                                else
                                                {
                                                    _notify.Warning(_localizer["Atención, no se pudo convertir la cantidad utilizando la información almacenada."]);
                                                }

                                                providerQuantity = Math.Ceiling(providerQuantity);
                                            }
                                            // Latas
                                            else if (rpp.UnitMeasureId.Value == 37)
                                            {
                                                // Se completa el campo PresentationQuantity
                                                if (rpp.PresentationQuantity.HasValue && rpp.PresentationUnitMeasure != null)
                                                {
                                                    providerQuantity = missingProductDTO.QuantityToOrder / rpp.PresentationQuantity.Value;
                                                }
                                                else
                                                {
                                                    _notify.Warning(_localizer["Atención, no se pudo convertir la cantidad utilizando la información almacenada."]);
                                                }

                                                providerQuantity = Math.Ceiling(providerQuantity);
                                            }
                                            // Tubos
                                            else if (rpp.UnitMeasureId.Value == 38)
                                            {
                                                // Se completa el campo PresentationQuantity
                                                if (rpp.PresentationQuantity.HasValue && rpp.PresentationUnitMeasure != null)
                                                {
                                                    providerQuantity = missingProductDTO.QuantityToOrder / rpp.PresentationQuantity.Value;
                                                }
                                                else
                                                {
                                                    _notify.Warning(_localizer["Atención, no se pudo convertir la cantidad utilizando la información almacenada."]);
                                                }

                                                providerQuantity = Math.Ceiling(providerQuantity);
                                            }
                                            // Unidades
                                            else if (rpp.UnitMeasureId.Value == 38)
                                            {
                                                // Se completa el campo PresentationQuantity
                                                if (rpp.PresentationQuantity.HasValue && rpp.PresentationUnitMeasure != null)
                                                {
                                                    providerQuantity = missingProductDTO.QuantityToOrder / rpp.PresentationQuantity.Value;
                                                }
                                                else
                                                {
                                                    _notify.Warning(_localizer["Atención, no se pudo convertir la cantidad utilizando la información almacenada."]);
                                                }

                                                providerQuantity = Math.Ceiling(providerQuantity);
                                            }

                                            providerUnitMeasureId = rpp.UnitMeasureId.Value;
                                            providerUnitMeasureDescription = rpp.UnitMeasure.Description;
                                        }
                                    }
                                    else
                                    {
                                        if (productVM.QuantityToOrder.HasValue)
                                        {
                                            // Barras o Rollos
                                            if (rpp.UnitMeasureId.Value == 22 || rpp.UnitMeasureId.Value == 23)
                                            {
                                                // Se completa el campo Lenght
                                                if (rpp.Lenght.HasValue && rpp.LengthUnitMeasure != null)
                                                {
                                                    providerQuantity = productVM.QuantityToOrder.Value / rpp.Lenght.Value;
                                                }
                                                else
                                                {
                                                    _notify.Warning(_localizer["Atención, no se pudo convertir la cantidad utilizando la información almacenada."]);
                                                }

                                                // Si es Barras o Rollos o Bobinas redondeo al entero superior
                                                if (rpp.UnitMeasureId.Value == 22 || rpp.UnitMeasureId.Value == 23)
                                                {
                                                    providerQuantity = Math.Ceiling(providerQuantity);
                                                }
                                            }
                                            // Cajas
                                            else if (rpp.UnitMeasureId.Value == 33)
                                            {
                                                // Se completa el campo lenght
                                                if (rpp.Lenght.HasValue && rpp.LengthUnitMeasure != null)
                                                {
                                                    providerQuantity = productVM.QuantityToOrder.Value / rpp.Lenght.Value;
                                                }
                                                // o el campo PresentationQuantity
                                                else if (rpp.PresentationQuantity.HasValue && rpp.PresentationUnitMeasure != null)
                                                {
                                                    providerQuantity = productVM.QuantityToOrder.Value / rpp.PresentationQuantity.Value;
                                                }
                                                else
                                                {
                                                    _notify.Warning(_localizer["Atención, no se pudo convertir la cantidad utilizando la información almacenada."]);
                                                }

                                                providerQuantity = Math.Ceiling(providerQuantity);
                                            }
                                            // Hojas
                                            else if (rpp.UnitMeasureId.Value == 31)
                                            {
                                                // Se completa el campo lenght y width
                                                if (rpp.Lenght.HasValue && rpp.LengthUnitMeasure != null && rpp.Width.HasValue && rpp.WidthUnitMeasure != null)
                                                {
                                                    providerQuantity = productVM.QuantityToOrder.Value / (rpp.Lenght.Value * rpp.Width.Value);
                                                }
                                                else
                                                {
                                                    _notify.Warning(_localizer["Atención, no se pudo convertir la cantidad utilizando la información almacenada."]);
                                                }

                                                providerQuantity = Math.Ceiling(providerQuantity);
                                            }
                                            // Bidones
                                            else if (rpp.UnitMeasureId.Value == 32)
                                            {
                                                // Se completa el campo PresentationQuantity
                                                if (rpp.PresentationQuantity.HasValue && rpp.PresentationUnitMeasure != null)
                                                {
                                                    providerQuantity = productVM.QuantityToOrder.Value / rpp.PresentationQuantity.Value;
                                                }
                                                else
                                                {
                                                    _notify.Warning(_localizer["Atención, no se pudo convertir la cantidad utilizando la información almacenada."]);
                                                }

                                                providerQuantity = Math.Ceiling(providerQuantity);
                                            }
                                            // Latas
                                            else if (rpp.UnitMeasureId.Value == 37)
                                            {
                                                // Se completa el campo PresentationQuantity
                                                if (rpp.PresentationQuantity.HasValue && rpp.PresentationUnitMeasure != null)
                                                {
                                                    providerQuantity = productVM.QuantityToOrder.Value / rpp.PresentationQuantity.Value;
                                                }
                                                else
                                                {
                                                    _notify.Warning(_localizer["Atención, no se pudo convertir la cantidad utilizando la información almacenada."]);
                                                }

                                                providerQuantity = Math.Ceiling(providerQuantity);
                                            }
                                            // Tubos
                                            else if (rpp.UnitMeasureId.Value == 38)
                                            {
                                                // Se completa el campo PresentationQuantity
                                                if (rpp.PresentationQuantity.HasValue && rpp.PresentationUnitMeasure != null)
                                                {
                                                    providerQuantity = productVM.QuantityToOrder.Value / rpp.PresentationQuantity.Value;
                                                }
                                                else
                                                {
                                                    _notify.Warning(_localizer["Atención, no se pudo convertir la cantidad utilizando la información almacenada."]);
                                                }

                                                providerQuantity = Math.Ceiling(providerQuantity);
                                            }
                                            // Unidades
                                            else if (rpp.UnitMeasureId.Value == 38)
                                            {
                                                // Se completa el campo PresentationQuantity
                                                if (rpp.PresentationQuantity.HasValue && rpp.PresentationUnitMeasure != null)
                                                {
                                                    providerQuantity = productVM.QuantityToOrder.Value / rpp.PresentationQuantity.Value;
                                                }
                                                else
                                                {
                                                    _notify.Warning(_localizer["Atención, no se pudo convertir la cantidad utilizando la información almacenada."]);
                                                }

                                                providerQuantity = Math.Ceiling(providerQuantity);
                                            }

                                            providerUnitMeasureId = rpp.UnitMeasureId.Value;
                                            providerUnitMeasureDescription = rpp.UnitMeasure.Description;
                                        }
                                    }
                                }
                            }

                            // Get quotationQuantity and quotationUnitMeasure
                            if (rpp.UnitMeasureId.HasValue && rpp.PriceUnitMeasureId.HasValue)
                            {
                                // Si la unidad de cotizacion es igual que la unidad del proveedor no se convierte.
                                if (rpp.PriceUnitMeasureId.Value == rpp.UnitMeasureId.Value)
                                {
                                    quotationQuantity = providerQuantity;
                                }
                                // Metros
                                else if (rpp.PriceUnitMeasureId.Value == 2)
                                {
                                    if (rpp.LengthUnitMeasureId.HasValue)
                                    {
                                        if (rpp.Lenght.HasValue && rpp.Lenght != null)
                                        {
                                            quotationQuantity = rpp.Lenght.Value * providerQuantity;
                                        }
                                    }
                                }
                                // Kilogramos
                                else if (rpp.PriceUnitMeasureId.Value == 1)
                                {
                                    if (rpp.WeightUnitMeasureId.HasValue)
                                    {
                                        if (rpp.Weight.HasValue && rpp.Weight != null)
                                        {
                                            quotationQuantity = rpp.Weight.Value * providerQuantity;
                                        }
                                    }
                                }
                                // Metros cuadrados
                                else if (rpp.PriceUnitMeasureId.Value == 16)
                                {
                                    if (rpp.LengthUnitMeasureId.HasValue && rpp.WeightUnitMeasureId.HasValue)
                                    {
                                        if (rpp.Lenght.HasValue && rpp.Lenght != null && rpp.Width.HasValue && rpp.Width != null)
                                        {
                                            quotationQuantity = (rpp.Lenght.Value * rpp.Width.Value) * providerQuantity;
                                        }
                                    }
                                }
                                // Litros
                                else if (rpp.PriceUnitMeasureId.Value == 18)
                                {
                                    if (rpp.PresentationUnitMeasureId.HasValue)
                                    {
                                        if (rpp.PresentationQuantity.HasValue && rpp.PresentationQuantity != null)
                                        {
                                            quotationQuantity = rpp.PresentationQuantity.Value * providerQuantity;
                                        }
                                    }
                                }
                                // Mililitros
                                else if (rpp.PriceUnitMeasureId.Value == 26)
                                {
                                    if (rpp.PresentationUnitMeasureId.HasValue)
                                    {
                                        if (rpp.PresentationQuantity.HasValue && rpp.PresentationQuantity != null)
                                        {
                                            quotationQuantity = rpp.PresentationQuantity.Value * providerQuantity;
                                        }
                                    }
                                }
                                // Centimetros cubicos
                                else if (rpp.PriceUnitMeasureId.Value == 39)
                                {
                                    if (rpp.PresentationUnitMeasureId.HasValue)
                                    {
                                        if (rpp.PresentationQuantity.HasValue && rpp.PresentationQuantity != null)
                                        {
                                            quotationQuantity = rpp.PresentationQuantity.Value * providerQuantity;
                                        }
                                    }
                                }
                                // Metros cubicos
                                else if (rpp.PriceUnitMeasureId.Value == 17)
                                {
                                    if (rpp.PresentationUnitMeasureId.HasValue)
                                    {
                                        if (rpp.PresentationQuantity.HasValue && rpp.PresentationQuantity != null)
                                        {
                                            quotationQuantity = rpp.PresentationQuantity.Value * providerQuantity;
                                        }
                                    }
                                }
                                // Barras
                                else if (rpp.PriceUnitMeasureId.Value == 22)
                                {
                                    if (rpp.LengthUnitMeasureId.HasValue)
                                    {
                                        if (rpp.Lenght.HasValue && rpp.Lenght != null)
                                        {
                                            quotationQuantity = rpp.Lenght.Value * providerQuantity;
                                        }
                                    }
                                }
                                // Rollos
                                else if (rpp.PriceUnitMeasureId.Value == 23)
                                {
                                    if (rpp.LengthUnitMeasureId.HasValue)
                                    {
                                        if (rpp.Lenght.HasValue && rpp.Lenght != null)
                                        {
                                            quotationQuantity = rpp.Lenght.Value * providerQuantity;
                                        }
                                    }
                                }
                                // Hojas
                                else if (rpp.PriceUnitMeasureId.Value == 31)
                                {
                                    if (rpp.LengthUnitMeasureId.HasValue && rpp.WeightUnitMeasureId.HasValue)
                                    {
                                        if (rpp.Lenght.HasValue && rpp.Lenght != null && rpp.Width.HasValue && rpp.Width != null)
                                        {
                                            quotationQuantity = (rpp.Lenght.Value * rpp.Width.Value) * providerQuantity;
                                        }
                                    }
                                }
                                // Bidones
                                else if (rpp.PriceUnitMeasureId.Value == 36)
                                {
                                    if (rpp.PresentationUnitMeasureId.HasValue)
                                    {
                                        if (rpp.PresentationQuantity.HasValue && rpp.PresentationQuantity != null)
                                        {
                                            quotationQuantity = rpp.PresentationQuantity.Value * providerQuantity;
                                        }
                                    }
                                    else if (rpp.WeightUnitMeasureId.HasValue)
                                    {
                                        if (rpp.Weight.HasValue && rpp.Weight != null)
                                        {
                                            quotationQuantity = rpp.Weight.Value * providerQuantity;
                                        }
                                    }
                                }
                                // Latas
                                else if (rpp.PriceUnitMeasureId.Value == 37)
                                {
                                    if (rpp.PresentationUnitMeasureId.HasValue)
                                    {
                                        if (rpp.PresentationQuantity.HasValue && rpp.PresentationQuantity != null)
                                        {
                                            quotationQuantity = rpp.PresentationQuantity.Value * providerQuantity;
                                        }
                                    }
                                    else if (rpp.WeightUnitMeasureId.HasValue)
                                    {
                                        if (rpp.Weight.HasValue && rpp.Weight != null)
                                        {
                                            quotationQuantity = rpp.Weight.Value * providerQuantity;
                                        }
                                    }
                                }
                                // Cajas
                                else if (rpp.PriceUnitMeasureId.Value == 33)
                                {
                                    if (rpp.PresentationUnitMeasureId.HasValue)
                                    {
                                        if (rpp.PresentationQuantity.HasValue && rpp.PresentationQuantity != null)
                                        {
                                            quotationQuantity = rpp.PresentationQuantity.Value * providerQuantity;
                                        }
                                    }
                                    else if (rpp.LengthUnitMeasureId.HasValue)
                                    {
                                        if (rpp.Lenght.HasValue && rpp.Lenght != null)
                                        {
                                            quotationQuantity = rpp.Lenght.Value * providerQuantity;
                                        }
                                    }
                                    else if (rpp.WeightUnitMeasureId.HasValue)
                                    {
                                        if (rpp.Weight.HasValue && rpp.Weight != null)
                                        {
                                            quotationQuantity = rpp.Weight.Value * providerQuantity;
                                        }
                                    }
                                }
                                // Tubos
                                else if (rpp.PriceUnitMeasureId.Value == 38)
                                {
                                    if (rpp.PresentationUnitMeasureId.HasValue)
                                    {
                                        if (rpp.PresentationQuantity.HasValue && rpp.PresentationQuantity != null)
                                        {
                                            quotationQuantity = rpp.PresentationQuantity.Value * providerQuantity;
                                        }
                                    }
                                    else if (rpp.WeightUnitMeasureId.HasValue)
                                    {
                                        if (rpp.Weight.HasValue && rpp.Weight != null)
                                        {
                                            quotationQuantity = rpp.Weight.Value * providerQuantity;
                                        }
                                    }
                                }
                                // Unidades
                                else if (rpp.PriceUnitMeasureId.Value == 19)
                                {
                                    if (rpp.PresentationUnitMeasureId.HasValue)
                                    {
                                        if (rpp.PresentationQuantity.HasValue && rpp.PresentationQuantity != null)
                                        {
                                            quotationQuantity = rpp.PresentationQuantity.Value * providerQuantity;
                                        }
                                    }
                                }

                                quotationUnitMeasureId = rpp.PriceUnitMeasureId.Value;
                                quotationUnitMeasureDescription = rpp.PriceUnitMeasure.Description;
                            }
                        }
                    }

                    int productIdNotProvided = 0;

                    if (missingProductId != 0)
                    {
                        var getMPById = await _missingProductViewManager.GetByIdAsync(missingProductId);
                        var productById = await _productViewManager.FindBySpecification(new ProductByIdWithUnitMeasureSpecification(getMPById.Data.ProductId));
                        if (!getRPPByProviderId.Data.Select(rpp => rpp.ProductId).Contains(productById.Data.First().Id))
                        {
                            productIdNotProvided = productById.Data.First().Id;
                        }
                    }

                    return new JsonResult(new { isValid = true, providerQuantity = providerQuantity, providerUnitMeasureId = providerUnitMeasureId, providerUnitMeasureDescription = providerUnitMeasureDescription, quotationQuantity = quotationQuantity, quotationUnitMeasureId = quotationUnitMeasureId, quotationUnitMeasureDescription = quotationUnitMeasureDescription, productIdNotProvided = productIdNotProvided });
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

        public async Task<JsonResult> convertQuotationQuantityFromProviderQuantity(decimal providerQuantityValue, int providerId, int missingProductId, int alternativeProductId)
        {
            try
            {
                decimal quotationQuantity = 0;

                var relProviderProductsVM = new List<RelProviderProductViewModel>();
                if (missingProductId != 0)
                {
                    var getMPById = await _missingProductViewManager.FindWithAllSpecification(new MissingProductByIdSpecification(missingProductId));
                    if (getMPById.Succeeded)
                    {
                        var missingProductVM = _mapper.Map<MissingProductViewModel>(getMPById.Data.First());
                        relProviderProductsVM = missingProductVM.Product.RelProviderProducts.ToList();
                    }
                }
                else if (alternativeProductId != 0)
                {
                    var getProductById = await _productViewManager.FindBySpecification(new ProductByIdSpecification(alternativeProductId));
                    if (getProductById.Succeeded)
                    {
                        var productVM = _mapper.Map<ProductViewModel>(getProductById.Data.First());
                        relProviderProductsVM = productVM.RelProviderProducts.ToList();
                    }
                }

                foreach (var rpp in relProviderProductsVM.Where(x => x.ProviderId == providerId))
                {
                    if (rpp.PriceUnitMeasureId.HasValue)
                    {
                        if (rpp.PriceUnitMeasureId.Value == rpp.UnitMeasureId)
                        {
                            quotationQuantity = providerQuantityValue;
                        }
                        // Metros
                        else if (rpp.PriceUnitMeasureId.Value == 2)
                        {
                            if (rpp.LengthUnitMeasureId.HasValue)
                            {
                                if (rpp.Lenght.HasValue && rpp.Lenght != null)
                                {
                                    quotationQuantity = rpp.Lenght.Value * providerQuantityValue;
                                }
                            }
                        }
                        // Kilogramos
                        else if (rpp.PriceUnitMeasureId.Value == 1)
                        {
                            if (rpp.WeightUnitMeasureId.HasValue)
                            {
                                if (rpp.Weight.HasValue && rpp.Weight != null)
                                {
                                    quotationQuantity = rpp.Weight.Value * providerQuantityValue;
                                }
                            }
                        }
                        // Metros cuadrados
                        else if (rpp.PriceUnitMeasureId.Value == 16)
                        {
                            if (rpp.LengthUnitMeasureId.HasValue && rpp.WeightUnitMeasureId.HasValue)
                            {
                                if (rpp.Lenght.HasValue && rpp.Lenght != null && rpp.Width.HasValue && rpp.Width != null)
                                {
                                    quotationQuantity = (rpp.Lenght.Value * rpp.Width.Value) * providerQuantityValue;
                                }
                            }
                        }
                        // Litros
                        else if (rpp.PriceUnitMeasureId.Value == 18)
                        {
                            if (rpp.PresentationUnitMeasureId.HasValue)
                            {
                                if (rpp.PresentationQuantity.HasValue && rpp.PresentationQuantity != null)
                                {
                                    quotationQuantity = rpp.PresentationQuantity.Value * providerQuantityValue;
                                }
                            }
                        }
                        // Mililitros
                        else if (rpp.PriceUnitMeasureId.Value == 26)
                        {
                            if (rpp.PresentationUnitMeasureId.HasValue)
                            {
                                if (rpp.PresentationQuantity.HasValue && rpp.PresentationQuantity != null)
                                {
                                    quotationQuantity = rpp.PresentationQuantity.Value * providerQuantityValue;
                                }
                            }
                        }
                        // Centimetros cubicos
                        else if (rpp.PriceUnitMeasureId.Value == 39)
                        {
                            if (rpp.PresentationUnitMeasureId.HasValue)
                            {
                                if (rpp.PresentationQuantity.HasValue && rpp.PresentationQuantity != null)
                                {
                                    quotationQuantity = rpp.PresentationQuantity.Value * providerQuantityValue;
                                }
                            }
                        }
                        // Metros cubicos
                        else if (rpp.PriceUnitMeasureId.Value == 17)
                        {
                            if (rpp.PresentationUnitMeasureId.HasValue)
                            {
                                if (rpp.PresentationQuantity.HasValue && rpp.PresentationQuantity != null)
                                {
                                    quotationQuantity = rpp.PresentationQuantity.Value * providerQuantityValue;
                                }
                            }
                        }
                        // Barras
                        else if (rpp.PriceUnitMeasureId.Value == 22)
                        {
                            if (rpp.LengthUnitMeasureId.HasValue)
                            {
                                if (rpp.Lenght.HasValue && rpp.Lenght != null)
                                {
                                    quotationQuantity = rpp.Lenght.Value * providerQuantityValue;
                                }
                            }
                        }
                        // Rollos
                        else if (rpp.PriceUnitMeasureId.Value == 23)
                        {
                            if (rpp.LengthUnitMeasureId.HasValue)
                            {
                                if (rpp.Lenght.HasValue && rpp.Lenght != null)
                                {
                                    quotationQuantity = rpp.Lenght.Value * providerQuantityValue;
                                }
                            }
                        }
                        // Hojas
                        else if (rpp.PriceUnitMeasureId.Value == 31)
                        {
                            if (rpp.LengthUnitMeasureId.HasValue && rpp.WeightUnitMeasureId.HasValue)
                            {
                                if (rpp.Lenght.HasValue && rpp.Lenght != null && rpp.Width.HasValue && rpp.Width != null)
                                {
                                    quotationQuantity = (rpp.Lenght.Value * rpp.Width.Value) * providerQuantityValue;
                                }
                            }
                        }
                        // Bidones
                        else if (rpp.PriceUnitMeasureId.Value == 36)
                        {
                            if (rpp.PresentationUnitMeasureId.HasValue)
                            {
                                if (rpp.PresentationQuantity.HasValue && rpp.PresentationQuantity != null)
                                {
                                    quotationQuantity = rpp.PresentationQuantity.Value * providerQuantityValue;
                                }
                            }
                            else if (rpp.WeightUnitMeasureId.HasValue)
                            {
                                if (rpp.Weight.HasValue && rpp.Weight != null)
                                {
                                    quotationQuantity = rpp.Weight.Value * providerQuantityValue;
                                }
                            }
                        }
                        // Latas
                        else if (rpp.PriceUnitMeasureId.Value == 37)
                        {
                            if (rpp.PresentationUnitMeasureId.HasValue)
                            {
                                if (rpp.PresentationQuantity.HasValue && rpp.PresentationQuantity != null)
                                {
                                    quotationQuantity = rpp.PresentationQuantity.Value * providerQuantityValue;
                                }
                            }
                            else if (rpp.WeightUnitMeasureId.HasValue)
                            {
                                if (rpp.Weight.HasValue && rpp.Weight != null)
                                {
                                    quotationQuantity = rpp.Weight.Value * providerQuantityValue;
                                }
                            }
                        }
                        // Cajas
                        else if (rpp.PriceUnitMeasureId.Value == 33)
                        {
                            if (rpp.PresentationUnitMeasureId.HasValue)
                            {
                                if (rpp.PresentationQuantity.HasValue && rpp.PresentationQuantity != null)
                                {
                                    quotationQuantity = rpp.PresentationQuantity.Value * providerQuantityValue;
                                }
                            }
                            else if (rpp.LengthUnitMeasureId.HasValue)
                            {
                                if (rpp.Lenght.HasValue && rpp.Lenght != null)
                                {
                                    quotationQuantity = rpp.Lenght.Value * providerQuantityValue;
                                }
                            }
                            else if (rpp.WeightUnitMeasureId.HasValue)
                            {
                                if (rpp.Weight.HasValue && rpp.Weight != null)
                                {
                                    quotationQuantity = rpp.Weight.Value * providerQuantityValue;
                                }
                            }
                        }
                        // Tubos
                        else if (rpp.PriceUnitMeasureId.Value == 38)
                        {
                            if (rpp.PresentationUnitMeasureId.HasValue)
                            {
                                if (rpp.PresentationQuantity.HasValue && rpp.PresentationQuantity != null)
                                {
                                    quotationQuantity = rpp.PresentationQuantity.Value * providerQuantityValue;
                                }
                            }
                            else if (rpp.WeightUnitMeasureId.HasValue)
                            {
                                if (rpp.Weight.HasValue && rpp.Weight != null)
                                {
                                    quotationQuantity = rpp.Weight.Value * providerQuantityValue;
                                }
                            }
                        }
                        // Unidades
                        else if (rpp.PriceUnitMeasureId.Value == 19)
                        {
                            if (rpp.PresentationUnitMeasureId.HasValue)
                            {
                                if (rpp.PresentationQuantity.HasValue && rpp.PresentationQuantity != null)
                                {
                                    quotationQuantity = rpp.PresentationQuantity.Value * providerQuantityValue;
                                }
                            }
                        }
                    }
                }

                return new JsonResult(new { isValid = true, quotationQuantity = quotationQuantity });
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return new JsonResult(new { isValid = false });
            }
        }

        public async Task<JsonResult> convertProviderQuantityFromQuotationQuantity(decimal quotationQuantityValue, int providerId, int missingProductId, int alternativeProductId)
        {
            try
            {
                decimal providerQuantity = 0;

                var relProviderProductsVM = new List<RelProviderProductViewModel>();
                if (missingProductId != 0)
                {
                    var getMPById = await _missingProductViewManager.FindWithAllSpecification(new MissingProductByIdSpecification(missingProductId));
                    if (getMPById.Succeeded)
                    {
                        var missingProductVM = _mapper.Map<MissingProductViewModel>(getMPById.Data.First());
                        relProviderProductsVM = missingProductVM.Product.RelProviderProducts.ToList();
                    }
                }
                else if (alternativeProductId != 0)
                {
                    var getProductById = await _productViewManager.FindBySpecification(new ProductByIdSpecification(alternativeProductId));
                    if (getProductById.Succeeded)
                    {
                        var productVM = _mapper.Map<ProductViewModel>(getProductById.Data.First());
                        relProviderProductsVM = productVM.RelProviderProducts.ToList();
                    }
                }

                foreach (var rpp in relProviderProductsVM.Where(x => x.ProviderId == providerId))
                {
                    if (rpp.PriceUnitMeasureId.HasValue)
                    {
                        if (rpp.PriceUnitMeasureId.Value == rpp.UnitMeasureId)
                        {
                            providerQuantity = quotationQuantityValue;
                        }
                        // Metros
                        else if (rpp.PriceUnitMeasureId.Value == 2)
                        {
                            if (rpp.LengthUnitMeasureId.HasValue)
                            {
                                if (rpp.Lenght.HasValue && rpp.Lenght != null)
                                {
                                    providerQuantity = quotationQuantityValue / rpp.Lenght.Value;
                                }
                            }
                        }
                        // Kilogramos
                        else if (rpp.PriceUnitMeasureId.Value == 1)
                        {
                            if (rpp.WeightUnitMeasureId.HasValue)
                            {
                                if (rpp.Weight.HasValue && rpp.Weight != null)
                                {
                                    providerQuantity = quotationQuantityValue / rpp.Weight.Value;
                                }
                            }
                        }
                        // Metros cuadrados
                        else if (rpp.PriceUnitMeasureId.Value == 16)
                        {
                            if (rpp.LengthUnitMeasureId.HasValue && rpp.WeightUnitMeasureId.HasValue)
                            {
                                if (rpp.Lenght.HasValue && rpp.Lenght != null && rpp.Width.HasValue && rpp.Width != null)
                                {
                                    providerQuantity = quotationQuantityValue / (rpp.Lenght.Value * rpp.Width.Value);
                                }
                            }
                        }
                        // Litros
                        else if (rpp.PriceUnitMeasureId.Value == 18)
                        {
                            if (rpp.PresentationUnitMeasureId.HasValue)
                            {
                                if (rpp.PresentationQuantity.HasValue && rpp.PresentationQuantity != null)
                                {
                                    providerQuantity = quotationQuantityValue / rpp.PresentationQuantity.Value;
                                }
                            }
                        }
                        // Mililitros
                        else if (rpp.PriceUnitMeasureId.Value == 26)
                        {
                            if (rpp.PresentationUnitMeasureId.HasValue)
                            {
                                if (rpp.PresentationQuantity.HasValue && rpp.PresentationQuantity != null)
                                {
                                    providerQuantity = quotationQuantityValue / rpp.PresentationQuantity.Value;
                                }
                            }
                        }
                        // Centimetros cubicos
                        else if (rpp.PriceUnitMeasureId.Value == 39)
                        {
                            if (rpp.PresentationUnitMeasureId.HasValue)
                            {
                                if (rpp.PresentationQuantity.HasValue && rpp.PresentationQuantity != null)
                                {
                                    providerQuantity = quotationQuantityValue / rpp.PresentationQuantity.Value;
                                }
                            }
                        }
                        // Metros cubicos
                        else if (rpp.PriceUnitMeasureId.Value == 17)
                        {
                            if (rpp.PresentationUnitMeasureId.HasValue)
                            {
                                if (rpp.PresentationQuantity.HasValue && rpp.PresentationQuantity != null)
                                {
                                    providerQuantity = quotationQuantityValue / rpp.PresentationQuantity.Value;
                                }
                            }
                        }
                        // Barras
                        else if (rpp.PriceUnitMeasureId.Value == 22)
                        {
                            if (rpp.LengthUnitMeasureId.HasValue)
                            {
                                if (rpp.Lenght.HasValue && rpp.Lenght != null)
                                {
                                    providerQuantity = quotationQuantityValue / rpp.Lenght.Value;
                                }
                            }
                        }
                        // Rollos
                        else if (rpp.PriceUnitMeasureId.Value == 23)
                        {
                            if (rpp.LengthUnitMeasureId.HasValue)
                            {
                                if (rpp.Lenght.HasValue && rpp.Lenght != null)
                                {
                                    providerQuantity = quotationQuantityValue / rpp.Lenght.Value;
                                }
                            }
                        }
                        // Hojas
                        else if (rpp.PriceUnitMeasureId.Value == 31)
                        {
                            if (rpp.LengthUnitMeasureId.HasValue && rpp.WeightUnitMeasureId.HasValue)
                            {
                                if (rpp.Lenght.HasValue && rpp.Lenght != null && rpp.Width.HasValue && rpp.Width != null)
                                {
                                    providerQuantity = quotationQuantityValue / (rpp.Lenght.Value * rpp.Width.Value);
                                }
                            }
                        }
                        // Bidones
                        else if (rpp.PriceUnitMeasureId.Value == 36)
                        {
                            if (rpp.PresentationUnitMeasureId.HasValue)
                            {
                                if (rpp.PresentationQuantity.HasValue && rpp.PresentationQuantity != null)
                                {
                                    providerQuantity = quotationQuantityValue / rpp.PresentationQuantity.Value;
                                }
                            }
                            else if (rpp.WeightUnitMeasureId.HasValue)
                            {
                                if (rpp.Weight.HasValue && rpp.Weight != null)
                                {
                                    providerQuantity = quotationQuantityValue / rpp.Weight.Value;
                                }
                            }
                        }
                        // Latas
                        else if (rpp.PriceUnitMeasureId.Value == 37)
                        {
                            if (rpp.PresentationUnitMeasureId.HasValue)
                            {
                                if (rpp.PresentationQuantity.HasValue && rpp.PresentationQuantity != null)
                                {
                                    providerQuantity = quotationQuantityValue / rpp.PresentationQuantity.Value;
                                }
                            }
                            else if (rpp.WeightUnitMeasureId.HasValue)
                            {
                                if (rpp.Weight.HasValue && rpp.Weight != null)
                                {
                                    providerQuantity = quotationQuantityValue / rpp.Weight.Value;
                                }
                            }
                        }
                        // Cajas
                        else if (rpp.PriceUnitMeasureId.Value == 33)
                        {
                            if (rpp.PresentationUnitMeasureId.HasValue)
                            {
                                if (rpp.PresentationQuantity.HasValue && rpp.PresentationQuantity != null)
                                {
                                    providerQuantity = quotationQuantityValue / rpp.PresentationQuantity.Value;
                                }
                            }
                            else if (rpp.LengthUnitMeasureId.HasValue)
                            {
                                if (rpp.Lenght.HasValue && rpp.Lenght != null)
                                {
                                    providerQuantity = quotationQuantityValue / rpp.Lenght.Value;
                                }
                            }
                            else if (rpp.WeightUnitMeasureId.HasValue)
                            {
                                if (rpp.Weight.HasValue && rpp.Weight != null)
                                {
                                    providerQuantity = quotationQuantityValue / rpp.Weight.Value;
                                }
                            }
                        }
                        // Tubos
                        else if (rpp.PriceUnitMeasureId.Value == 38)
                        {
                            if (rpp.PresentationUnitMeasureId.HasValue)
                            {
                                if (rpp.PresentationQuantity.HasValue && rpp.PresentationQuantity != null)
                                {
                                    providerQuantity = quotationQuantityValue / rpp.PresentationQuantity.Value;
                                }
                            }
                            else if (rpp.WeightUnitMeasureId.HasValue)
                            {
                                if (rpp.Weight.HasValue && rpp.Weight != null)
                                {
                                    providerQuantity = quotationQuantityValue / rpp.Weight.Value;
                                }
                            }
                        }
                        // Unidades
                        else if (rpp.PriceUnitMeasureId.Value == 19)
                        {
                            if (rpp.PresentationUnitMeasureId.HasValue)
                            {
                                if (rpp.PresentationQuantity.HasValue && rpp.PresentationQuantity != null)
                                {
                                    providerQuantity = quotationQuantityValue / rpp.PresentationQuantity.Value;
                                }
                            }
                        }
                    }
                }

                return new JsonResult(new { isValid = true, providerQuantity = Math.Ceiling(providerQuantity) });
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return new JsonResult(new { isValid = false });
            }
        }

        public async Task<List<decimal>> getUnitsAndQuantityDiff(int providerId, int missingProductId, int productId)
        {
            try
            {
                // Values by default
                decimal providerQuantity = 0;
                decimal quotationQuantity = 0;

                // Me traigo los datos de productos-proveedores por id de proveedor
                var getRPPByProviderId = await _relProviderProductViewManager.FindBySpecification(new RelProviderProductByProviderIdSpecification(providerId));
                if (getRPPByProviderId.Succeeded)
                {
                    foreach (var rpp in getRPPByProviderId.Data)
                    {
                        ProductDTO getProductById = null;
                        MissingProductDTO missingProductDTO = null;
                        if (missingProductId != 0)
                        {
                            var getMissingProductById = await _missingProductViewManager.GetByIdAsync(missingProductId);
                            getProductById = _productViewManager.FindBySpecification(new ProductByIdWithUnitMeasureSpecification(getMissingProductById.Data.ProductId)).Result.Data.First();
                            missingProductDTO = getMissingProductById.Data;
                        }
                        else
                        {
                            getProductById = _productViewManager.FindBySpecification(new ProductByIdWithUnitMeasureSpecification(productId)).Result.Data.First();
                        }
                        var productVM = _mapper.Map<ProductViewModel>(getProductById);
                        if (rpp.ProductId == productVM.Id)
                        {
                            // Get providerQuantity and providerUnitMeasure
                            if (rpp.UnitMeasureId.HasValue && productVM.QuantityToOrderUnitMeasureId.HasValue)
                            {
                                // Si la unidad del proveedor es igual que la unidad de la cantidad a pedir del producto:
                                if ((rpp.UnitMeasureId.Value == productVM.QuantityToOrderUnitMeasureId.Value) && (productVM.QuantityToOrder.HasValue && productVM.QuantityToOrderUnitMeasure != null && productVM.QuantityToOrderUnitMeasureId.HasValue))
                                {
                                    if (missingProductId != 0 && missingProductDTO != null)
                                    {
                                        providerQuantity = missingProductDTO.QuantityToOrder;

                                    }
                                    else
                                    {
                                        providerQuantity = productVM.QuantityToOrder.Value;
                                    }
                                    //providerQuantity = productVM.QuantityToOrder.Value;
                                }
                                else
                                {
                                    // Si la unidad del proveedor no es igual que la unidad del producto, entonces tengo que convertir teniendo en cuenta la unidad del proveedor.
                                    if (missingProductId != 0 && missingProductDTO != null)
                                    {
                                        if (missingProductDTO.QuantityToOrder != 0)
                                        {
                                            // Barras o Rollos
                                            if (rpp.UnitMeasureId.Value == 22 || rpp.UnitMeasureId.Value == 23)
                                            {
                                                // Se completa el campo Lenght
                                                if (rpp.Lenght.HasValue && rpp.LengthUnitMeasure != null)
                                                {
                                                    providerQuantity = missingProductDTO.QuantityToOrder / rpp.Lenght.Value;
                                                }
                                                else
                                                {
                                                    _notify.Warning(_localizer["Atención, no se pudo convertir la cantidad utilizando la información almacenada."]);
                                                }

                                                // Si es Barras o Rollos o Bobinas redondeo al entero superior
                                                if (rpp.UnitMeasureId.Value == 22 || rpp.UnitMeasureId.Value == 23)
                                                {
                                                    providerQuantity = Math.Ceiling(providerQuantity);
                                                }
                                            }
                                            // Cajas
                                            else if (rpp.UnitMeasureId.Value == 33)
                                            {
                                                // Se completa el campo lenght
                                                if (rpp.Lenght.HasValue && rpp.LengthUnitMeasure != null)
                                                {
                                                    providerQuantity = missingProductDTO.QuantityToOrder / rpp.Lenght.Value;
                                                }
                                                // o el campo PresentationQuantity
                                                else if (rpp.PresentationQuantity.HasValue && rpp.PresentationUnitMeasure != null)
                                                {
                                                    providerQuantity = missingProductDTO.QuantityToOrder / rpp.PresentationQuantity.Value;
                                                }
                                                else
                                                {
                                                    _notify.Warning(_localizer["Atención, no se pudo convertir la cantidad utilizando la información almacenada."]);
                                                }

                                                providerQuantity = Math.Ceiling(providerQuantity);
                                            }
                                            // Hojas
                                            else if (rpp.UnitMeasureId.Value == 31)
                                            {
                                                // Se completa el campo lenght y width
                                                if (rpp.Lenght.HasValue && rpp.LengthUnitMeasure != null && rpp.Width.HasValue && rpp.WidthUnitMeasure != null)
                                                {
                                                    providerQuantity = missingProductDTO.QuantityToOrder / (rpp.Lenght.Value * rpp.Width.Value);
                                                }
                                                else
                                                {
                                                    _notify.Warning(_localizer["Atención, no se pudo convertir la cantidad utilizando la información almacenada."]);
                                                }

                                                providerQuantity = Math.Ceiling(providerQuantity);
                                            }
                                            // Bidones
                                            else if (rpp.UnitMeasureId.Value == 32)
                                            {
                                                // Se completa el campo PresentationQuantity
                                                if (rpp.PresentationQuantity.HasValue && rpp.PresentationUnitMeasure != null)
                                                {
                                                    providerQuantity = missingProductDTO.QuantityToOrder / rpp.PresentationQuantity.Value;
                                                }
                                                else
                                                {
                                                    _notify.Warning(_localizer["Atención, no se pudo convertir la cantidad utilizando la información almacenada."]);
                                                }

                                                providerQuantity = Math.Ceiling(providerQuantity);
                                            }
                                            // Latas
                                            else if (rpp.UnitMeasureId.Value == 37)
                                            {
                                                // Se completa el campo PresentationQuantity
                                                if (rpp.PresentationQuantity.HasValue && rpp.PresentationUnitMeasure != null)
                                                {
                                                    providerQuantity = missingProductDTO.QuantityToOrder / rpp.PresentationQuantity.Value;
                                                }
                                                else
                                                {
                                                    _notify.Warning(_localizer["Atención, no se pudo convertir la cantidad utilizando la información almacenada."]);
                                                }

                                                providerQuantity = Math.Ceiling(providerQuantity);
                                            }
                                            // Tubos
                                            else if (rpp.UnitMeasureId.Value == 38)
                                            {
                                                // Se completa el campo PresentationQuantity
                                                if (rpp.PresentationQuantity.HasValue && rpp.PresentationUnitMeasure != null)
                                                {
                                                    providerQuantity = missingProductDTO.QuantityToOrder / rpp.PresentationQuantity.Value;
                                                }
                                                else
                                                {
                                                    _notify.Warning(_localizer["Atención, no se pudo convertir la cantidad utilizando la información almacenada."]);
                                                }

                                                providerQuantity = Math.Ceiling(providerQuantity);
                                            }
                                            // Unidades
                                            else if (rpp.UnitMeasureId.Value == 38)
                                            {
                                                // Se completa el campo PresentationQuantity
                                                if (rpp.PresentationQuantity.HasValue && rpp.PresentationUnitMeasure != null)
                                                {
                                                    providerQuantity = missingProductDTO.QuantityToOrder / rpp.PresentationQuantity.Value;
                                                }
                                                else
                                                {
                                                    _notify.Warning(_localizer["Atención, no se pudo convertir la cantidad utilizando la información almacenada."]);
                                                }

                                                providerQuantity = Math.Ceiling(providerQuantity);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (productVM.QuantityToOrder.HasValue)
                                        {
                                            // Barras o Rollos
                                            if (rpp.UnitMeasureId.Value == 22 || rpp.UnitMeasureId.Value == 23)
                                            {
                                                // Se completa el campo Lenght
                                                if (rpp.Lenght.HasValue && rpp.LengthUnitMeasure != null)
                                                {
                                                    providerQuantity = productVM.QuantityToOrder.Value / rpp.Lenght.Value;
                                                }
                                                else
                                                {
                                                    _notify.Warning(_localizer["Atención, no se pudo convertir la cantidad utilizando la información almacenada."]);
                                                }

                                                // Si es Barras o Rollos o Bobinas redondeo al entero superior
                                                if (rpp.UnitMeasureId.Value == 22 || rpp.UnitMeasureId.Value == 23)
                                                {
                                                    providerQuantity = Math.Ceiling(providerQuantity);
                                                }
                                            }
                                            // Cajas
                                            else if (rpp.UnitMeasureId.Value == 33)
                                            {
                                                // Se completa el campo lenght
                                                if (rpp.Lenght.HasValue && rpp.LengthUnitMeasure != null)
                                                {
                                                    providerQuantity = productVM.QuantityToOrder.Value / rpp.Lenght.Value;
                                                }
                                                // o el campo PresentationQuantity
                                                else if (rpp.PresentationQuantity.HasValue && rpp.PresentationUnitMeasure != null)
                                                {
                                                    providerQuantity = productVM.QuantityToOrder.Value / rpp.PresentationQuantity.Value;
                                                }
                                                else
                                                {
                                                    _notify.Warning(_localizer["Atención, no se pudo convertir la cantidad utilizando la información almacenada."]);
                                                }

                                                providerQuantity = Math.Ceiling(providerQuantity);
                                            }
                                            // Hojas
                                            else if (rpp.UnitMeasureId.Value == 31)
                                            {
                                                // Se completa el campo lenght y width
                                                if (rpp.Lenght.HasValue && rpp.LengthUnitMeasure != null && rpp.Width.HasValue && rpp.WidthUnitMeasure != null)
                                                {
                                                    providerQuantity = productVM.QuantityToOrder.Value / (rpp.Lenght.Value * rpp.Width.Value);
                                                }
                                                else
                                                {
                                                    _notify.Warning(_localizer["Atención, no se pudo convertir la cantidad utilizando la información almacenada."]);
                                                }

                                                providerQuantity = Math.Ceiling(providerQuantity);
                                            }
                                            // Bidones
                                            else if (rpp.UnitMeasureId.Value == 32)
                                            {
                                                // Se completa el campo PresentationQuantity
                                                if (rpp.PresentationQuantity.HasValue && rpp.PresentationUnitMeasure != null)
                                                {
                                                    providerQuantity = productVM.QuantityToOrder.Value / rpp.PresentationQuantity.Value;
                                                }
                                                else
                                                {
                                                    _notify.Warning(_localizer["Atención, no se pudo convertir la cantidad utilizando la información almacenada."]);
                                                }

                                                providerQuantity = Math.Ceiling(providerQuantity);
                                            }
                                            // Latas
                                            else if (rpp.UnitMeasureId.Value == 37)
                                            {
                                                // Se completa el campo PresentationQuantity
                                                if (rpp.PresentationQuantity.HasValue && rpp.PresentationUnitMeasure != null)
                                                {
                                                    providerQuantity = productVM.QuantityToOrder.Value / rpp.PresentationQuantity.Value;
                                                }
                                                else
                                                {
                                                    _notify.Warning(_localizer["Atención, no se pudo convertir la cantidad utilizando la información almacenada."]);
                                                }

                                                providerQuantity = Math.Ceiling(providerQuantity);
                                            }
                                            // Tubos
                                            else if (rpp.UnitMeasureId.Value == 38)
                                            {
                                                // Se completa el campo PresentationQuantity
                                                if (rpp.PresentationQuantity.HasValue && rpp.PresentationUnitMeasure != null)
                                                {
                                                    providerQuantity = productVM.QuantityToOrder.Value / rpp.PresentationQuantity.Value;
                                                }
                                                else
                                                {
                                                    _notify.Warning(_localizer["Atención, no se pudo convertir la cantidad utilizando la información almacenada."]);
                                                }

                                                providerQuantity = Math.Ceiling(providerQuantity);
                                            }
                                            // Unidades
                                            else if (rpp.UnitMeasureId.Value == 38)
                                            {
                                                // Se completa el campo PresentationQuantity
                                                if (rpp.PresentationQuantity.HasValue && rpp.PresentationUnitMeasure != null)
                                                {
                                                    providerQuantity = productVM.QuantityToOrder.Value / rpp.PresentationQuantity.Value;
                                                }
                                                else
                                                {
                                                    _notify.Warning(_localizer["Atención, no se pudo convertir la cantidad utilizando la información almacenada."]);
                                                }

                                                providerQuantity = Math.Ceiling(providerQuantity);
                                            }
                                        }
                                    }
                                }
                            }

                            // Get quotationQuantity and quotationUnitMeasure
                            if (rpp.UnitMeasureId.HasValue && rpp.PriceUnitMeasureId.HasValue)
                            {
                                // Si la unidad de cotizacion es igual que la unidad del proveedor no se convierte.
                                if (rpp.PriceUnitMeasureId.Value == rpp.UnitMeasureId.Value)
                                {
                                    quotationQuantity = providerQuantity;
                                }
                                // Metros
                                else if (rpp.PriceUnitMeasureId.Value == 2)
                                {
                                    if (rpp.LengthUnitMeasureId.HasValue)
                                    {
                                        if (rpp.Lenght.HasValue && rpp.Lenght != null)
                                        {
                                            quotationQuantity = rpp.Lenght.Value * providerQuantity;
                                        }
                                    }
                                }
                                // Kilogramos
                                else if (rpp.PriceUnitMeasureId.Value == 1)
                                {
                                    if (rpp.WeightUnitMeasureId.HasValue)
                                    {
                                        if (rpp.Weight.HasValue && rpp.Weight != null)
                                        {
                                            quotationQuantity = rpp.Weight.Value * providerQuantity;
                                        }
                                    }
                                }
                                // Metros cuadrados
                                else if (rpp.PriceUnitMeasureId.Value == 16)
                                {
                                    if (rpp.LengthUnitMeasureId.HasValue && rpp.WeightUnitMeasureId.HasValue)
                                    {
                                        if (rpp.Lenght.HasValue && rpp.Lenght != null && rpp.Width.HasValue && rpp.Width != null)
                                        {
                                            quotationQuantity = (rpp.Lenght.Value * rpp.Width.Value) * providerQuantity;
                                        }
                                    }
                                }
                                // Litros
                                else if (rpp.PriceUnitMeasureId.Value == 18)
                                {
                                    if (rpp.PresentationUnitMeasureId.HasValue)
                                    {
                                        if (rpp.PresentationQuantity.HasValue && rpp.PresentationQuantity != null)
                                        {
                                            quotationQuantity = rpp.PresentationQuantity.Value * providerQuantity;
                                        }
                                    }
                                }
                                // Mililitros
                                else if (rpp.PriceUnitMeasureId.Value == 26)
                                {
                                    if (rpp.PresentationUnitMeasureId.HasValue)
                                    {
                                        if (rpp.PresentationQuantity.HasValue && rpp.PresentationQuantity != null)
                                        {
                                            quotationQuantity = rpp.PresentationQuantity.Value * providerQuantity;
                                        }
                                    }
                                }
                                // Centimetros cubicos
                                else if (rpp.PriceUnitMeasureId.Value == 39)
                                {
                                    if (rpp.PresentationUnitMeasureId.HasValue)
                                    {
                                        if (rpp.PresentationQuantity.HasValue && rpp.PresentationQuantity != null)
                                        {
                                            quotationQuantity = rpp.PresentationQuantity.Value * providerQuantity;
                                        }
                                    }
                                }
                                // Metros cubicos
                                else if (rpp.PriceUnitMeasureId.Value == 17)
                                {
                                    if (rpp.PresentationUnitMeasureId.HasValue)
                                    {
                                        if (rpp.PresentationQuantity.HasValue && rpp.PresentationQuantity != null)
                                        {
                                            quotationQuantity = rpp.PresentationQuantity.Value * providerQuantity;
                                        }
                                    }
                                }
                                // Barras
                                else if (rpp.PriceUnitMeasureId.Value == 22)
                                {
                                    if (rpp.LengthUnitMeasureId.HasValue)
                                    {
                                        if (rpp.Lenght.HasValue && rpp.Lenght != null)
                                        {
                                            quotationQuantity = rpp.Lenght.Value * providerQuantity;
                                        }
                                    }
                                }
                                // Rollos
                                else if (rpp.PriceUnitMeasureId.Value == 23)
                                {
                                    if (rpp.LengthUnitMeasureId.HasValue)
                                    {
                                        if (rpp.Lenght.HasValue && rpp.Lenght != null)
                                        {
                                            quotationQuantity = rpp.Lenght.Value * providerQuantity;
                                        }
                                    }
                                }
                                // Hojas
                                else if (rpp.PriceUnitMeasureId.Value == 31)
                                {
                                    if (rpp.LengthUnitMeasureId.HasValue && rpp.WeightUnitMeasureId.HasValue)
                                    {
                                        if (rpp.Lenght.HasValue && rpp.Lenght != null && rpp.Width.HasValue && rpp.Width != null)
                                        {
                                            quotationQuantity = (rpp.Lenght.Value * rpp.Width.Value) * providerQuantity;
                                        }
                                    }
                                }
                                // Bidones
                                else if (rpp.PriceUnitMeasureId.Value == 36)
                                {
                                    if (rpp.PresentationUnitMeasureId.HasValue)
                                    {
                                        if (rpp.PresentationQuantity.HasValue && rpp.PresentationQuantity != null)
                                        {
                                            quotationQuantity = rpp.PresentationQuantity.Value * providerQuantity;
                                        }
                                    }
                                    else if (rpp.WeightUnitMeasureId.HasValue)
                                    {
                                        if (rpp.Weight.HasValue && rpp.Weight != null)
                                        {
                                            quotationQuantity = rpp.Weight.Value * providerQuantity;
                                        }
                                    }
                                }
                                // Latas
                                else if (rpp.PriceUnitMeasureId.Value == 37)
                                {
                                    if (rpp.PresentationUnitMeasureId.HasValue)
                                    {
                                        if (rpp.PresentationQuantity.HasValue && rpp.PresentationQuantity != null)
                                        {
                                            quotationQuantity = rpp.PresentationQuantity.Value * providerQuantity;
                                        }
                                    }
                                    else if (rpp.WeightUnitMeasureId.HasValue)
                                    {
                                        if (rpp.Weight.HasValue && rpp.Weight != null)
                                        {
                                            quotationQuantity = rpp.Weight.Value * providerQuantity;
                                        }
                                    }
                                }
                                // Cajas
                                else if (rpp.PriceUnitMeasureId.Value == 33)
                                {
                                    if (rpp.PresentationUnitMeasureId.HasValue)
                                    {
                                        if (rpp.PresentationQuantity.HasValue && rpp.PresentationQuantity != null)
                                        {
                                            quotationQuantity = rpp.PresentationQuantity.Value * providerQuantity;
                                        }
                                    }
                                    else if (rpp.LengthUnitMeasureId.HasValue)
                                    {
                                        if (rpp.Lenght.HasValue && rpp.Lenght != null)
                                        {
                                            quotationQuantity = rpp.Lenght.Value * providerQuantity;
                                        }
                                    }
                                    else if (rpp.WeightUnitMeasureId.HasValue)
                                    {
                                        if (rpp.Weight.HasValue && rpp.Weight != null)
                                        {
                                            quotationQuantity = rpp.Weight.Value * providerQuantity;
                                        }
                                    }
                                }
                                // Tubos
                                else if (rpp.PriceUnitMeasureId.Value == 38)
                                {
                                    if (rpp.PresentationUnitMeasureId.HasValue)
                                    {
                                        if (rpp.PresentationQuantity.HasValue && rpp.PresentationQuantity != null)
                                        {
                                            quotationQuantity = rpp.PresentationQuantity.Value * providerQuantity;
                                        }
                                    }
                                    else if (rpp.WeightUnitMeasureId.HasValue)
                                    {
                                        if (rpp.Weight.HasValue && rpp.Weight != null)
                                        {
                                            quotationQuantity = rpp.Weight.Value * providerQuantity;
                                        }
                                    }
                                }
                                // Unidades
                                else if (rpp.PriceUnitMeasureId.Value == 19)
                                {
                                    if (rpp.PresentationUnitMeasureId.HasValue)
                                    {
                                        if (rpp.PresentationQuantity.HasValue && rpp.PresentationQuantity != null)
                                        {
                                            quotationQuantity = rpp.PresentationQuantity.Value * providerQuantity;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    var listToReturn = new List<decimal>
                    {
                        providerQuantity,
                        quotationQuantity
                    };

                    return listToReturn;
                }
                else
                {
                    return new List<decimal>();
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return new List<decimal>();
            }
        }

        // Método que se encarga de crear una respuesta de solicitud de cotización y además actualizar el estado de la cotizacion original y de los productos
        // faltantes incluidos en la respuesta al estado "Cotizado".
        [HttpPost]
        public async Task<JsonResult> OnPostCreateQuoteRequestResponse(int id, QuoteRequestResponseHeaderViewModel quoteRequestResponseHeaderVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (quoteRequestResponseHeaderVM.ProviderId != -1)
                    {
                        var detailsQuoteRequestResponse = quoteRequestResponseHeaderVM.QuoteRequestResponseDetails.Where(x => x != null).ToList();
                        if (detailsQuoteRequestResponse.Count() > 0)
                        {
                            quoteRequestResponseHeaderVM.QuoteRequestResponseDetails = detailsQuoteRequestResponse;

                            foreach (var qrrdVM in quoteRequestResponseHeaderVM.QuoteRequestResponseDetails)
                            {
                                if (qrrdVM.AlternativeProductId != null && qrrdVM.AlternativeProductId.HasValue && qrrdVM.Available && qrrdVM.MissingProductId != null && qrrdVM.MissingProductId.HasValue)
                                {
                                    var providerAndQuotationQuantityDiff = await getUnitsAndQuantityDiff(quoteRequestResponseHeaderVM.ProviderId, qrrdVM.MissingProductId.Value, 0);
                                    if (providerAndQuotationQuantityDiff.Count > 0)
                                    {
                                        qrrdVM.OriginalProviderQuantity = providerAndQuotationQuantityDiff[0];
                                        qrrdVM.OriginalPriceQuantity = providerAndQuotationQuantityDiff[1];
                                    }
                                }
                                else if (qrrdVM.AlternativeProductId != null && qrrdVM.AlternativeProductId.HasValue)
                                {
                                    var providerAndQuotationQuantityDiff = await getUnitsAndQuantityDiff(quoteRequestResponseHeaderVM.ProviderId, 0, qrrdVM.AlternativeProductId.Value);
                                    if (providerAndQuotationQuantityDiff.Count > 0)
                                    {
                                        qrrdVM.OriginalProviderQuantity = providerAndQuotationQuantityDiff[0];
                                        qrrdVM.OriginalPriceQuantity = providerAndQuotationQuantityDiff[1];
                                    }
                                }
                            }

                            if (id == 0)
                            {
                                // Create
                                var quoteRequestResponseHeaderDTO = _mapper.Map<QuoteRequestResponseHeaderDTO>(quoteRequestResponseHeaderVM);
                                var result = await _quoteRequestResponseHeaderViewManager.CreateAsync(quoteRequestResponseHeaderDTO);
                                if (result.Succeeded)
                                {
                                    id = result.Data;

                                    _notify.Success(_localizer["Quote request response created."]);
                                    return new JsonResult(new { isValid = true });
                                }
                                else
                                {
                                    _notify.Error(result.Message);
                                    quoteRequestResponseHeaderVM = await LoadDataForRefreshFormModal(quoteRequestResponseHeaderVM);
                                    var html = await _viewRenderer.RenderViewToStringAsync("_CreateQuoteRequestResponse", quoteRequestResponseHeaderVM);
                                    return new JsonResult(new { isValid = false, html = html });
                                }
                            }
                            else
                            {
                                // Update
                                var quoteRequestResponseHeaderDTO = _mapper.Map<QuoteRequestResponseHeaderDTO>(quoteRequestResponseHeaderVM);
                                var result = await _quoteRequestResponseHeaderViewManager.UpdateAsync(quoteRequestResponseHeaderDTO);
                                if (result.Succeeded)
                                {
                                    _notify.Success(_localizer["Quote request response updated."]);
                                    return new JsonResult(new { isValid = true });
                                }
                                else
                                {
                                    _notify.Error(result.Message);
                                    quoteRequestResponseHeaderVM = await LoadDataForRefreshFormModal(quoteRequestResponseHeaderVM);
                                    var html = await _viewRenderer.RenderViewToStringAsync("_CreateQuoteRequestResponse", quoteRequestResponseHeaderVM);
                                    return new JsonResult(new { isValid = false, html = html });
                                }
                            }
                        }
                        else
                        {
                            _notify.Warning(_localizer["You must add detail for this quote request."]);
                            quoteRequestResponseHeaderVM = await LoadDataForRefreshFormModal(quoteRequestResponseHeaderVM);
                            var html = await _viewRenderer.RenderViewToStringAsync("_CreateQuoteRequestResponse", quoteRequestResponseHeaderVM);
                            return new JsonResult(new { isValid = false, html = html });
                        }
                    }
                    else
                    {
                        _notify.Warning(_localizer["You must select provider for this quote request response."]);
                        quoteRequestResponseHeaderVM = await LoadDataForRefreshFormModal(quoteRequestResponseHeaderVM);
                        var html = await _viewRenderer.RenderViewToStringAsync("_CreateQuoteRequestResponse", quoteRequestResponseHeaderVM);
                        return new JsonResult(new { isValid = false, html = html });
                    }
                }
                else
                {
                    quoteRequestResponseHeaderVM = await LoadDataForRefreshFormModal(quoteRequestResponseHeaderVM);
                    var html = await _viewRenderer.RenderViewToStringAsync("_CreateQuoteRequestResponse", quoteRequestResponseHeaderVM);
                    return new JsonResult(new { isValid = false, html = html });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                quoteRequestResponseHeaderVM = await LoadDataForRefreshFormModal(quoteRequestResponseHeaderVM);
                var html = await _viewRenderer.RenderViewToStringAsync("_CreateQuoteRequestResponse", quoteRequestResponseHeaderVM);
                return new JsonResult(new { isValid = false, html = html });
            }
        }

        public async Task<QuoteRequestResponseHeaderViewModel> LoadDataForRefreshFormModal(QuoteRequestResponseHeaderViewModel quoteRequestResponseHeaderVM)
        {
            // Tengo que setear los detalles a los detalles de respuesta
            var getQuoteRequestDetailsByHeaderId = await _quoteRequestHeaderViewManager.FindBySpecification(new QuoteRequestHeaderWithAllSpecifications(quoteRequestResponseHeaderVM.QuoteRequestHeaderId));
            if (getQuoteRequestDetailsByHeaderId.Succeeded)
            {
                var quoteRequestHeadersVM = _mapper.Map<QuoteRequestHeaderViewModel>(getQuoteRequestDetailsByHeaderId.Data.FirstOrDefault());

                foreach (var quoteRequestDetail in quoteRequestHeadersVM.QuoteRequestDetails)
                {
                    foreach (var qrrD in quoteRequestResponseHeaderVM.QuoteRequestResponseDetails)
                    {
                        if (quoteRequestDetail.MissingProductId.HasValue)
                        {
                            qrrD.MissingProduct = quoteRequestDetail.MissingProduct;
                            qrrD.MissingProductId = quoteRequestDetail.MissingProductId.Value;
                        }

                        var getProviderUnitMeasure = await _unitMeasureViewManager.FindBySpecification(new UnitMeasureSpecification(qrrD.ProviderUnitMeasureId));
                        if (getProviderUnitMeasure.Succeeded)
                        {
                            var providerUMVM = _mapper.Map<UnitMeasureViewModel>(getProviderUnitMeasure.Data.FirstOrDefault());
                            qrrD.ProviderUnitMeasure = providerUMVM;
                        }
                        var getPriceUnitMeasure = await _unitMeasureViewManager.FindBySpecification(new UnitMeasureSpecification(qrrD.PriceUnitMeasureId));
                        if (getPriceUnitMeasure.Succeeded)
                        {
                            var priceUMVM = _mapper.Map<UnitMeasureViewModel>(getPriceUnitMeasure.Data.FirstOrDefault());
                            qrrD.PriceUnitMeasure = priceUMVM;
                        }
                    }
                }
                return quoteRequestResponseHeaderVM;
            }
            else
            {
                return null;
            }
        }

        [Authorize(Policy = Permissions.PurchaseOrder.View)]
        public async Task<JsonResult> OnGetCreatePurchaseOrder(int qrrhId, int providerId)
        {
            // Tengo que setear los detalles de la QRR a la PO en PurchaseOrderDetails
            //var getQuoteRequestResponseDetailsByHeaderId = await _quoteRequestResponseHeaderViewManager.FindBySpecification(new QuoteRequestResponseHeaderByQRHeaderIdSpecification(qrhId));
            var getQuoteRequestResponseDetailsByHeaderId = await _quoteRequestResponseHeaderViewManager.FindBySpecification(new QuoteRequestResponseHeaderWithAllSpecifications(qrrhId));
            if (getQuoteRequestResponseDetailsByHeaderId.Succeeded)
            {
                var quoteRequestResponseHeaderVM = _mapper.Map<QuoteRequestResponseHeaderViewModel>(getQuoteRequestResponseDetailsByHeaderId.Data.FirstOrDefault());

                var purchaseOrderViewModel = new PurchaseOrderHeaderViewModel();

                purchaseOrderViewModel.QuoteRequestResponseHeaderId = quoteRequestResponseHeaderVM.Id;
                purchaseOrderViewModel.Provider = quoteRequestResponseHeaderVM.Provider;
                purchaseOrderViewModel.ProviderId = quoteRequestResponseHeaderVM.ProviderId;
                purchaseOrderViewModel.Date = DateTime.Now.Date.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute);
                purchaseOrderViewModel.Bonus = quoteRequestResponseHeaderVM.Bonus;
                purchaseOrderViewModel.IVA = quoteRequestResponseHeaderVM.IVA;
                purchaseOrderViewModel.Total = quoteRequestResponseHeaderVM.Total;

                var purchaseOrderHeaders = _purchaseOrderHeaderViewManager.FindBySpecification(new PurchaseOrderHeaderSpecification());
                if (purchaseOrderHeaders.Result.Succeeded)
                {
                    var purchaseOrderHeadersVM = _mapper.Map<List<PurchaseOrderHeaderViewModel>>(purchaseOrderHeaders.Result.Data);
                    if (purchaseOrderHeadersVM.Count > 0)
                    {
                        purchaseOrderViewModel.Number = purchaseOrderHeadersVM.Max(poh => poh.Id);
                        purchaseOrderViewModel.Number += 1;
                    }
                    else
                    {
                        purchaseOrderViewModel.Number = 1;
                    }
                }

                foreach (var quoteRequestResponseDetail in quoteRequestResponseHeaderVM.QuoteRequestResponseDetails)
                {
                    var purchaseOrderDetailVM = new PurchaseOrderDetailViewModel();

                    purchaseOrderDetailVM.MissingProduct = quoteRequestResponseDetail.MissingProduct;
                    purchaseOrderDetailVM.MissingProductId = quoteRequestResponseDetail.MissingProductId;
                    purchaseOrderDetailVM.OriginalProviderQuantity = quoteRequestResponseDetail.OriginalProviderQuantity;
                    purchaseOrderDetailVM.ProviderQuantity = quoteRequestResponseDetail.ProviderQuantity;
                    purchaseOrderDetailVM.ProviderUnitMeasure = quoteRequestResponseDetail.ProviderUnitMeasure;
                    purchaseOrderDetailVM.ProviderUnitMeasureId = quoteRequestResponseDetail.ProviderUnitMeasureId;
                    purchaseOrderDetailVM.OriginalPriceQuantity = quoteRequestResponseDetail.OriginalPriceQuantity;
                    purchaseOrderDetailVM.PriceQuantity = quoteRequestResponseDetail.PriceQuantity;
                    purchaseOrderDetailVM.PriceUnitMeasure = quoteRequestResponseDetail.PriceUnitMeasure;
                    purchaseOrderDetailVM.PriceUnitMeasureId = quoteRequestResponseDetail.PriceUnitMeasureId;
                    purchaseOrderDetailVM.UnitPrice = quoteRequestResponseDetail.UnitPrice;
                    purchaseOrderDetailVM.MoneyType = quoteRequestResponseDetail.MoneyType;
                    purchaseOrderDetailVM.Bonus = quoteRequestResponseDetail.Bonus;
                    purchaseOrderDetailVM.Total = quoteRequestResponseDetail.Total;

                    purchaseOrderViewModel.PurchaseOrderDetails.Add(purchaseOrderDetailVM);
                }

                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("../PurchaseOrder/_Create", purchaseOrderViewModel) });

            }
            else
            {
                return new JsonResult(new { isValid = false, html = await _viewRenderer.RenderViewToStringAsync("../PurchaseOrder/_Create", new PurchaseOrderHeaderViewModel()) });
            }
        }

        [Authorize(Policy = Permissions.PurchaseOrder.View)]
        public async Task<JsonResult> OnGetCreatePurchaseOrderByIds(List<int> quoteRequestResponseHeaderIds, List<int> quoteRequestResponseDetailIds)
        {
            var qrrHeader = await _quoteRequestResponseHeaderViewManager.FindBySpecification(new QuoteRequestResponseHeaderWithAllSpecifications(quoteRequestResponseHeaderIds.First()));
            if (!qrrHeader.Succeeded)
            {
                return new JsonResult(new { isValid = false, html = await _viewRenderer.RenderViewToStringAsync("../PurchaseOrder/_Create", new PurchaseOrderHeaderViewModel()) });
            }

            var qrrHeaderVM = _mapper.Map<QuoteRequestResponseHeaderViewModel>(qrrHeader.Data.FirstOrDefault());
            var purchaseOrderViewModel = new PurchaseOrderHeaderViewModel();

            purchaseOrderViewModel.QuoteRequestResponseHeaderId = qrrHeaderVM.Id;
            purchaseOrderViewModel.Provider = qrrHeaderVM.Provider;
            purchaseOrderViewModel.ProviderId = qrrHeaderVM.ProviderId;
            purchaseOrderViewModel.Date = DateTime.Now.Date.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute);
            var purchaseOrderHeaders = _purchaseOrderHeaderViewManager.FindBySpecification(new PurchaseOrderHeaderSpecification());
            if (purchaseOrderHeaders.Result.Succeeded)
            {
                var purchaseOrderHeadersVM = _mapper.Map<List<PurchaseOrderHeaderViewModel>>(purchaseOrderHeaders.Result.Data);
                if (purchaseOrderHeadersVM.Count > 0)
                {
                    purchaseOrderViewModel.Number = purchaseOrderHeadersVM.Max(poh => poh.Id);
                    purchaseOrderViewModel.Number += 1;
                }
                else
                {
                    purchaseOrderViewModel.Number = 1;
                }
            }

            foreach (var qrrdId in quoteRequestResponseDetailIds)
            {
                var getQRRDetailById = await _quoteRequestResponseDetailViewManager.FindBySpecification(new QuoteRequestResponseDetailWithAllSpecifications(qrrdId));
                if (getQRRDetailById.Succeeded)
                {
                    var quoteRequestResponseDetail = _mapper.Map<QuoteRequestResponseDetailViewModel>(getQRRDetailById.Data.FirstOrDefault());

                    var purchaseOrderDetailVM = new PurchaseOrderDetailViewModel();

                    if (quoteRequestResponseDetail.MissingProductId.HasValue)
                    {
                        if (quoteRequestResponseDetail.MissingProduct.StateMissingProductId > 3 || quoteRequestResponseDetail.QuoteRequestResponseHeader.QuoteRequestHeader.QuoteRequestDetails.Any(x => x.PurchaseStateId > 3))
                        {
                            _notify.Warning(_localizer["Attention, make sure to select products where their status is \"Pending\", \"To quote\" or \"Quoted\"."]);
                            return new JsonResult(new { isValid = false, html = await _viewRenderer.RenderViewToStringAsync("../PurchaseOrder/_Create", new PurchaseOrderHeaderViewModel()) });
                        }

                        purchaseOrderDetailVM.MissingProduct = quoteRequestResponseDetail.MissingProduct;
                        purchaseOrderDetailVM.MissingProductId = quoteRequestResponseDetail.MissingProductId;
                    }
                    else if (quoteRequestResponseDetail.AlternativeProductId.HasValue)
                    {
                        purchaseOrderDetailVM.Product = quoteRequestResponseDetail.AlternativeProduct;
                        purchaseOrderDetailVM.ProductId = quoteRequestResponseDetail.AlternativeProductId;
                    }

                    purchaseOrderDetailVM.OriginalProviderQuantity = quoteRequestResponseDetail.OriginalProviderQuantity;
                    purchaseOrderDetailVM.ProviderQuantity = quoteRequestResponseDetail.ProviderQuantity;
                    purchaseOrderDetailVM.ProviderUnitMeasure = quoteRequestResponseDetail.ProviderUnitMeasure;
                    purchaseOrderDetailVM.ProviderUnitMeasureId = quoteRequestResponseDetail.ProviderUnitMeasureId;
                    purchaseOrderDetailVM.OriginalPriceQuantity = quoteRequestResponseDetail.OriginalPriceQuantity;
                    purchaseOrderDetailVM.PriceQuantity = quoteRequestResponseDetail.PriceQuantity;
                    purchaseOrderDetailVM.PriceUnitMeasure = quoteRequestResponseDetail.PriceUnitMeasure;
                    purchaseOrderDetailVM.PriceUnitMeasureId = quoteRequestResponseDetail.PriceUnitMeasureId;
                    purchaseOrderDetailVM.UnitPrice = quoteRequestResponseDetail.UnitPrice;
                    purchaseOrderDetailVM.MoneyType = quoteRequestResponseDetail.MoneyType;
                    purchaseOrderDetailVM.Bonus = quoteRequestResponseDetail.Bonus;
                    purchaseOrderDetailVM.Total = quoteRequestResponseDetail.Total;

                    purchaseOrderViewModel.PurchaseOrderDetails.Add(purchaseOrderDetailVM);

                }
                else
                {
                    return new JsonResult(new { isValid = false, html = await _viewRenderer.RenderViewToStringAsync("../PurchaseOrder/_Create", new PurchaseOrderHeaderViewModel()) });
                }
            }

            if (purchaseOrderViewModel.PurchaseOrderDetails.Count > 0)
            {
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("../PurchaseOrder/_Create", purchaseOrderViewModel) });
            }
            else
            {
                return new JsonResult(new { isValid = false, html = await _viewRenderer.RenderViewToStringAsync("../PurchaseOrder/_Create", new PurchaseOrderHeaderViewModel()) });
            }
        }

        public async Task<JsonResult> _LoadProvidersByQuoteRequestSelect2(string search, int qrhId)
        {
            var relQuoteRequestProviderReponse = await _relQuoteRequestProviderViewManager.FindBySpecification(new RelQuoteRequestProviderGetProviderByHeaderIdSpecification(qrhId));
            if (relQuoteRequestProviderReponse.Succeeded)
            {
                var relQuoteRequestProvider = new HashSet<RelQuoteRequestProviderDTO>();
                var getQRRHByQRHId = await _quoteRequestResponseHeaderViewManager.FindBySpecification(new QuoteRequestResponseHeaderByQRHeaderIdSpecification(qrhId));
                if (getQRRHByQRHId.Succeeded)
                {
                    if (getQRRHByQRHId.Data.Count() > 0)
                    {
                        // Proveedores a los cuales no se le hayan generado una respuesta de cotizacion.
                        relQuoteRequestProvider = relQuoteRequestProviderReponse.Data.Where(rqrp => !getQRRHByQRHId.Data.Any(qrh => qrh.ProviderId == rqrp.ProviderId)).ToHashSet();
                        //relQuoteRequestProvider = relQuoteRequestProviderReponse.Data.ToHashSet();
                    }
                    else
                    {
                        relQuoteRequestProvider = relQuoteRequestProviderReponse.Data.ToHashSet();
                    }
                }

                List<Select2ViewModel> mapSelect2Data = new List<Select2ViewModel>();
                HashSet<ProviderViewModel> providersViewModel = new HashSet<ProviderViewModel>();
                foreach (var rQRP in relQuoteRequestProvider)
                {
                    var providerVM = _mapper.Map<ProviderViewModel>(rQRP.Provider);
                    providersViewModel.Add(providerVM);
                }
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

        public async Task<JsonResult> _LoadAlternativeProductsByProviderId(string search, int providerId)
        {
            var relProviderProductsResponse = await _relProviderProductViewManager.FindBySpecification(new RelProviderProductByProviderIdSpecification(providerId));
            if (relProviderProductsResponse.Succeeded)
            {
                List<Select2ViewModel> mapSelect2Data = new List<Select2ViewModel>();
                HashSet<ProductViewModel> productsViewModel = new HashSet<ProductViewModel>();
                foreach (var relProviderProduct in relProviderProductsResponse.Data)
                {
                    var productVM = _mapper.Map<ProductViewModel>(relProviderProduct.Product);
                    productsViewModel.Add(productVM);
                }
                foreach (var productVM in productsViewModel)
                {
                    mapSelect2Data.Add(new Select2ViewModel() { Id = productVM.Id, Text = productVM.CodeAndDescription });
                }

                if (!(string.IsNullOrEmpty(search) || string.IsNullOrWhiteSpace(search)))
                {
                    mapSelect2Data = mapSelect2Data.Where(x => x.Text.ToLower().Contains(search.ToLower())).ToList();
                }
                return new JsonResult(new { alternativeProducts = mapSelect2Data.OrderBy(x => x.Text) });
            }
            else
            {
                return null;
            }
        }

        [HttpGet]
        public async Task<JsonResult> _LoadProductsByProviderIdSelect2(string search, int providerId, int[] providerIds)
        {
            try
            {
                if (providerIds != null && providerIds.Length > 0)
                {
                    foreach (var pvId in providerIds)
                    {
                        if (pvId != -1)
                        {
                            var relProviderProductsResponse = await _relProviderProductViewManager.FindBySpecification(new RelProviderProductByProviderIdSpecification(pvId));
                            if (relProviderProductsResponse.Succeeded)
                            {
                                List<Select2ViewModel> mapSelect2Data = new List<Select2ViewModel>();
                                HashSet<ProductViewModel> productViewModels = new HashSet<ProductViewModel>();
                                foreach (var relProviderProduct in relProviderProductsResponse.Data)
                                {
                                    var productViewModel = _mapper.Map<ProductViewModel>(relProviderProduct.Product);
                                    productViewModels.Add(productViewModel);
                                }

                                foreach (var productVM in productViewModels)
                                {
                                    mapSelect2Data.Add(new Select2ViewModel() { Id = productVM.Id, Text = productVM.CodeAndDescription });
                                }

                                if (!(string.IsNullOrEmpty(search) || string.IsNullOrWhiteSpace(search)))
                                {
                                    mapSelect2Data = mapSelect2Data.Where(x => x.Text.ToLower().Contains(search.ToLower())).ToList();
                                }
                                return new JsonResult(new { products = mapSelect2Data });
                            }
                            else
                            {
                                return new JsonResult(new { products = new List<Select2ViewModel>() });
                            }
                        }
                        else
                        {
                            return new JsonResult(new { products = new List<Select2ViewModel>() });
                        }
                    }
                    return new JsonResult(new { products = new List<Select2ViewModel>() });
                }
                else
                {
                    if (providerId != -1)
                    {
                        var relProviderProductsResponse = await _relProviderProductViewManager.FindBySpecification(new RelProviderProductByProviderIdSpecification(providerId));
                        if (relProviderProductsResponse.Succeeded)
                        {
                            List<Select2ViewModel> mapSelect2Data = new List<Select2ViewModel>();
                            HashSet<ProductViewModel> productViewModels = new HashSet<ProductViewModel>();
                            foreach (var relProviderProduct in relProviderProductsResponse.Data)
                            {
                                var productViewModel = _mapper.Map<ProductViewModel>(relProviderProduct.Product);
                                productViewModels.Add(productViewModel);
                            }

                            foreach (var productVM in productViewModels)
                            {
                                mapSelect2Data.Add(new Select2ViewModel() { Id = productVM.Id, Text = productVM.CodeAndDescription });
                            }

                            if (!(string.IsNullOrEmpty(search) || string.IsNullOrWhiteSpace(search)))
                            {
                                mapSelect2Data = mapSelect2Data.Where(x => x.Text.ToLower().Contains(search.ToLower())).ToList();
                            }

                            return new JsonResult(new { products = mapSelect2Data });
                        }
                        else
                        {
                            return new JsonResult(new { products = new List<Select2ViewModel>() });
                        }
                    }
                    else
                    {
                        return new JsonResult(new { products = new List<Select2ViewModel>() });
                    }
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return new JsonResult(new { products = new List<Select2ViewModel>() });
            }
        }

        public IActionResult _IndexQuoteRequestResponse()
        {
            return View();
        }

        public async Task<IActionResult> _LoadAllQuoteRequestResponses(DatatableParamsViewModel dataTableParams,
        string sSearch_2, string sSearch_3, string sSearch_4, string sSearch_5, string sSearch_6, string sSearch_7, string sSearch_8, string sSearch_9, string sSearch_10, string sSearch_11, string sSearch_12, string sSearch_13, string sSearch_14, string sSearch_15,
        DateTime dateFromFilter, DateTime dateToFilter)
        {
            try
            {
                var response = await _quoteRequestResponseDetailViewManager.FindBySpecification(new QuoteRequestResponseDetailWithAllSpecifications());
                if (response.Succeeded)
                {
                    var quoteRequestResponseDetailViewModels = _mapper.Map<List<QuoteRequestResponseDetailViewModel>>(response.Data);

                    quoteRequestResponseDetailViewModels = quoteRequestResponseDetailViewModels.Where(qrrd => qrrd.Available == true).ToList();

                    // From date -> To date
                    quoteRequestResponseDetailViewModels = quoteRequestResponseDetailViewModels.Where(qrrd => qrrd.QuoteRequestResponseHeader.QuoteRequestHeader.Date.Date >= dateFromFilter && qrrd.QuoteRequestResponseHeader.QuoteRequestHeader.Date.Date <= dateToFilter).ToList();

                    if (!string.IsNullOrEmpty(sSearch_2))
                    {
                        quoteRequestResponseDetailViewModels = quoteRequestResponseDetailViewModels.Where(qrrd => qrrd.QuoteRequestResponseHeader.Id.ToString().Contains(sSearch_2)).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_3))
                    {
                        quoteRequestResponseDetailViewModels = quoteRequestResponseDetailViewModels.Where(qrrd => qrrd.QuoteRequestResponseHeader.QuoteRequestHeaderId.ToString().Contains(sSearch_3)).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_4))
                    {
                        quoteRequestResponseDetailViewModels = quoteRequestResponseDetailViewModels.Where(qrrd => qrrd.QuoteRequestResponseHeader.Date.ToString("dd/MM/yyyy").Contains(sSearch_4)).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_5))
                    {
                        quoteRequestResponseDetailViewModels = quoteRequestResponseDetailViewModels.Where(qrrd => qrrd.MissingProduct != null ? qrrd.MissingProduct.Product.Code.ToString().ToLower().Contains(sSearch_5.ToLower()) : qrrd.AlternativeProduct.Code.ToString().ToLower().Contains(sSearch_5.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_6))
                    {
                        quoteRequestResponseDetailViewModels = quoteRequestResponseDetailViewModels.Where(qrrd => qrrd.MissingProduct != null ? qrrd.MissingProduct.Product.Description.ToString().ToLower().Contains(sSearch_6.ToLower()) : qrrd.AlternativeProduct.Description.ToString().ToLower().Contains(sSearch_6.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_7))
                    {
                        quoteRequestResponseDetailViewModels = quoteRequestResponseDetailViewModels.Where(qrrd => qrrd.QuoteRequestResponseHeader.Provider.getBussinessNameOrName.ToString().ToLower().Contains(sSearch_7.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_8))
                    {
                        quoteRequestResponseDetailViewModels = quoteRequestResponseDetailViewModels.Where(qrrd => qrrd.ProviderQuantity.ToString().ToLower().Contains(sSearch_8.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_9))
                    {
                        quoteRequestResponseDetailViewModels = quoteRequestResponseDetailViewModels.Where(qrrd => qrrd.ProviderUnitMeasure != null && qrrd.ProviderUnitMeasure.Description.ToString().ToLower().Contains(sSearch_9.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_10))
                    {
                        quoteRequestResponseDetailViewModels = quoteRequestResponseDetailViewModels.Where(qrrd => qrrd.PriceQuantity.ToString().ToLower().Contains(sSearch_10.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_11))
                    {
                        quoteRequestResponseDetailViewModels = quoteRequestResponseDetailViewModels.Where(qrrd => qrrd.PriceUnitMeasure != null && qrrd.PriceUnitMeasure.Description.ToString().ToLower().Contains(sSearch_11.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_12))
                    {
                        quoteRequestResponseDetailViewModels = quoteRequestResponseDetailViewModels.Where(qrrd => qrrd.MoneyType == 1 ? (qrrd.UnitPrice.ToString() + "ARS").ToString().ToLower().Contains(sSearch_12.ToLower()) : qrrd.MoneyType == 2 ? (qrrd.UnitPrice.ToString() + "USD").ToString().ToLower().Contains(sSearch_12.ToLower()) : qrrd.UnitPrice.ToString().ToLower().Contains(sSearch_12.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_13))
                    {
                        quoteRequestResponseDetailViewModels = quoteRequestResponseDetailViewModels.Where(qrrd => qrrd.Bonus.ToString().ToLower().Contains(sSearch_13.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_14))
                    {
                        quoteRequestResponseDetailViewModels = quoteRequestResponseDetailViewModels.Where(qrrd => qrrd.Total.ToString().ToLower().Contains(sSearch_14.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_15))
                    {
                        quoteRequestResponseDetailViewModels = quoteRequestResponseDetailViewModels.Where(qrrd => qrrd.Available ? "Si".ToString().ToLower().Contains(sSearch_15.ToLower()) : "No".ToString().ToLower().Contains(sSearch_15.ToLower())).ToList();
                    }

                    var sortColumnIndex = dataTableParams.iSortCol_0;
                    var sortDirection = dataTableParams.sSortDir_0;

                    if (sortColumnIndex == 2)
                    {
                        quoteRequestResponseDetailViewModels = sortDirection == "asc" ? quoteRequestResponseDetailViewModels.OrderBy(qrrd => qrrd.QuoteRequestResponseHeader.Id).ToList() : quoteRequestResponseDetailViewModels.OrderByDescending(qrrd => qrrd.QuoteRequestResponseHeader.Id).ToList();
                    }
                    else if (sortColumnIndex == 3)
                    {
                        quoteRequestResponseDetailViewModels = sortDirection == "asc" ? quoteRequestResponseDetailViewModels.OrderBy(qrrd => qrrd.QuoteRequestResponseHeader.QuoteRequestHeaderId).ToList() : quoteRequestResponseDetailViewModels.OrderByDescending(qrrd => qrrd.QuoteRequestResponseHeader.QuoteRequestHeaderId).ToList();
                    }
                    else if (sortColumnIndex == 4)
                    {
                        quoteRequestResponseDetailViewModels = sortDirection == "asc" ? quoteRequestResponseDetailViewModels.OrderBy(qrrd => qrrd.QuoteRequestResponseHeader.Date).ToList() : quoteRequestResponseDetailViewModels.OrderByDescending(qrrd => qrrd.QuoteRequestResponseHeader.Date).ToList();
                    }
                    else if (sortColumnIndex == 5)
                    {
                        quoteRequestResponseDetailViewModels = sortDirection == "asc" ? quoteRequestResponseDetailViewModels.OrderBy(qrrd => qrrd.MissingProduct != null ? qrrd.MissingProduct.Product.Code.ToString() : qrrd.AlternativeProduct.Code.ToString()).ToList() : quoteRequestResponseDetailViewModels.OrderByDescending(qrrd => qrrd.MissingProduct != null ? qrrd.MissingProduct.Product.Code.ToString() : qrrd.AlternativeProduct.Code.ToString()).ToList();
                    }
                    else if (sortColumnIndex == 6)
                    {
                        quoteRequestResponseDetailViewModels = sortDirection == "asc" ? quoteRequestResponseDetailViewModels.OrderBy(qrrd => qrrd.MissingProduct != null ? qrrd.MissingProduct.Product.Description.ToString() : qrrd.AlternativeProduct.Description.ToString()).ToList() : quoteRequestResponseDetailViewModels.OrderByDescending(qrrd => qrrd.MissingProduct != null ? qrrd.MissingProduct.Product.Description.ToString() : qrrd.AlternativeProduct.Description.ToString()).ToList();
                    }
                    else if (sortColumnIndex == 7)
                    {
                        quoteRequestResponseDetailViewModels = sortDirection == "asc" ? quoteRequestResponseDetailViewModels.OrderBy(qrrd => qrrd.QuoteRequestResponseHeader.Provider.getBussinessNameOrName.ToString()).ToList() : quoteRequestResponseDetailViewModels.OrderByDescending(qrrd => qrrd.QuoteRequestResponseHeader.Provider.getBussinessNameOrName.ToString()).ToList();
                    }
                    else if (sortColumnIndex == 8)
                    {
                        quoteRequestResponseDetailViewModels = sortDirection == "asc" ? quoteRequestResponseDetailViewModels.OrderBy(qrrd => qrrd.ProviderQuantity).ToList() : quoteRequestResponseDetailViewModels.OrderByDescending(qrrd => qrrd.ProviderQuantity).ToList();
                    }
                    else if (sortColumnIndex == 9)
                    {
                        quoteRequestResponseDetailViewModels = sortDirection == "asc" ? quoteRequestResponseDetailViewModels.OrderBy(qrrd => qrrd.ProviderUnitMeasure != null ? qrrd.ProviderUnitMeasure.Description.ToString() : null).ToList() : quoteRequestResponseDetailViewModels.OrderByDescending(qrrd => qrrd.ProviderUnitMeasure != null ? qrrd.ProviderUnitMeasure.Description.ToString() : null).ToList();
                    }
                    else if (sortColumnIndex == 10)
                    {
                        quoteRequestResponseDetailViewModels = sortDirection == "asc" ? quoteRequestResponseDetailViewModels.OrderBy(qrrd => qrrd.PriceQuantity).ToList() : quoteRequestResponseDetailViewModels.OrderByDescending(qrrd => qrrd.PriceQuantity).ToList();
                    }
                    else if (sortColumnIndex == 11)
                    {
                        quoteRequestResponseDetailViewModels = sortDirection == "asc" ? quoteRequestResponseDetailViewModels.OrderBy(qrrd => qrrd.PriceUnitMeasure != null ? qrrd.PriceUnitMeasure.Description.ToString() : null).ToList() : quoteRequestResponseDetailViewModels.OrderByDescending(qrrd => qrrd.PriceUnitMeasure != null ? qrrd.PriceUnitMeasure.Description.ToString() : null).ToList();
                    }
                    else if (sortColumnIndex == 12)
                    {
                        quoteRequestResponseDetailViewModels = sortDirection == "asc" ? quoteRequestResponseDetailViewModels.OrderBy(qrrd => qrrd.UnitPrice).ToList() : quoteRequestResponseDetailViewModels.OrderByDescending(qrrd => qrrd.UnitPrice).ToList();
                    }
                    else if (sortColumnIndex == 13)
                    {
                        quoteRequestResponseDetailViewModels = sortDirection == "asc" ? quoteRequestResponseDetailViewModels.OrderBy(qrrd => qrrd.Bonus).ToList() : quoteRequestResponseDetailViewModels.OrderByDescending(qrrd => qrrd.Bonus).ToList();
                    }
                    else if (sortColumnIndex == 14)
                    {
                        quoteRequestResponseDetailViewModels = sortDirection == "asc" ? quoteRequestResponseDetailViewModels.OrderBy(qrrd => qrrd.Total).ToList() : quoteRequestResponseDetailViewModels.OrderByDescending(qrrd => qrrd.Total).ToList();
                    }
                    else if (sortColumnIndex == 15)
                    {
                        quoteRequestResponseDetailViewModels = quoteRequestResponseDetailViewModels.Where(qrrd => qrrd.Available ? "Si".ToString().ToLower().Contains(sSearch_14.ToLower()) : "No".ToString().ToLower().Contains(sSearch_13.ToLower())).ToList();
                    }

                    var displayResult = quoteRequestResponseDetailViewModels.Skip(dataTableParams.iDisplayStart)
                        .Take(dataTableParams.iDisplayLength).ToList();

                    var totalRecords = quoteRequestResponseDetailViewModels.Count();

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
                    _notify.Error(_localizer["Quote request responses data could not be loaded."]);
                    return null;
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return null;
            }
        }

        // Only for Superadmin user role (validate in view)
        [HttpPost]
        public async Task<JsonResult> OnPostDeleteQuoteRequestResponse(int qrrdId, int qrrhId)
        {
            var deleteCommand = await _quoteRequestResponseDetailViewManager.DeleteAsync(qrrdId);
            if (deleteCommand.Succeeded)
            {
                var qrrdByQRRHId = await _quoteRequestResponseDetailViewManager.FindBySpecification(new QuoteRequestResponseDetailByQRRHIdSpecification(qrrhId));
                if (qrrdByQRRHId.Succeeded)
                {
                    if (qrrdByQRRHId.Data.Count == 0)
                    {
                        // Borro la solicitud completa o encabezado
                        await _quoteRequestResponseHeaderViewManager.DeleteAsync(qrrhId);
                    }

                    _notify.Information(_localizer["Quote request response deleted."]);
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

        public async Task<IActionResult> _IndexPriceHistory(int productId)
        {
            var getProductById = await _productViewManager.GetByIdAsync(productId);
            if (getProductById.Succeeded)
            {
                var productVM = _mapper.Map<ProductViewModel>(getProductById.Data);
                ViewBag.productCodeAndDescription = productVM.CodeAndDescription.ToString();
                ViewBag.productId = productId;
            }
            return View();
        }

        public async Task<IActionResult> _LoadAllPriceHistoryQuoteRequestResponsesByProductId(DatatableParamsViewModel dataTableParams,
        string sSearch_1, string sSearch_2, string sSearch_3, string sSearch_4, string sSearch_5, string sSearch_6, string sSearch_7, string sSearch_8,
        DateTime dateFromFilter, DateTime dateToFilter, int productId)
        {
            try
            {
                var response = await _quoteRequestResponseDetailViewManager.FindBySpecification(new QuoteRequestResponseDetailByProductIdSpecifications(productId));
                if (response.Succeeded)
                {
                    var quoteRequestResponseDetailViewModels = _mapper.Map<List<QuoteRequestResponseDetailViewModel>>(response.Data);

                    // From date -> To date
                    quoteRequestResponseDetailViewModels = quoteRequestResponseDetailViewModels.Where(qrrd => qrrd.QuoteRequestResponseHeader.QuoteRequestHeader.Date >= dateFromFilter && qrrd.QuoteRequestResponseHeader.QuoteRequestHeader.Date <= dateToFilter).ToList();

                    // Filtro la RPP que pertenezca al proveedor asignado para la respuesta de cotizacion.
                    foreach (var qrrd in quoteRequestResponseDetailViewModels)
                    {
                        var relProviderProducts = qrrd.MissingProduct.Product.RelProviderProducts.ToList();
                        foreach (var rpp in qrrd.MissingProduct.Product.RelProviderProducts.ToList())
                        {
                            if (rpp.ProviderId != qrrd.QuoteRequestResponseHeader.ProviderId)
                            {
                                relProviderProducts.Remove(rpp);
                            }
                        }

                        qrrd.MissingProduct.Product.RelProviderProducts = relProviderProducts;
                    }

                    if (!string.IsNullOrEmpty(sSearch_1))
                    {
                    }
                    if (!string.IsNullOrEmpty(sSearch_2))
                    {

                    }
                    if (!string.IsNullOrEmpty(sSearch_3))
                    {
                    }
                    if (!string.IsNullOrEmpty(sSearch_4))
                    {
                    }
                    if (!string.IsNullOrEmpty(sSearch_5))
                    {
                    }
                    if (!string.IsNullOrEmpty(sSearch_6))
                    {
                    }
                    if (!string.IsNullOrEmpty(sSearch_7))
                    {
                    }
                    if (!string.IsNullOrEmpty(sSearch_8))
                    {
                    }
                    var sortColumnIndex = dataTableParams.iSortCol_0;
                    var sortDirection = dataTableParams.sSortDir_0;
                    if (sortColumnIndex == 1)
                    {
                    }
                    else if (sortColumnIndex == 2)
                    {
                    }
                    else if (sortColumnIndex == 3)
                    {
                    }
                    else if (sortColumnIndex == 4)
                    {
                    }
                    else if (sortColumnIndex == 5)
                    {
                    }
                    else if (sortColumnIndex == 6)
                    {

                    }
                    else if (sortColumnIndex == 7)
                    {

                    }
                    else if (sortColumnIndex == 8)
                    {

                    }

                    var displayResult = quoteRequestResponseDetailViewModels.Skip(dataTableParams.iDisplayStart)
                        .Take(dataTableParams.iDisplayLength).ToList();

                    var totalRecords = quoteRequestResponseDetailViewModels.Count();

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
                    _notify.Error(_localizer["Quote request responses data could not be loaded."]);
                    return null;
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return null;
            }
        }

        public async Task<IActionResult> ExportToExcelQRR(string sSearch_2, string sSearch_3, string sSearch_4, string sSearch_5, string sSearch_6,
            string sSearch_7, string sSearch_8, string sSearch_9, string sSearch_10, string sSearch_11, string sSearch_12, string sSearch_13, string sSearch_14, string sSearch_15,
            int colIndexOrder, string colOrderDirection, string dateFromFilter, string dateToFilter)
        {
            try
            {
                var response = await _quoteRequestResponseDetailViewManager.FindBySpecification(new QuoteRequestResponseDetailWithAllSpecifications());
                if (response.Succeeded)
                {
                    var quoteRequestResponseDetailViewModels = _mapper.Map<List<QuoteRequestResponseDetailViewModel>>(response.Data);

                    // From date -> To date
                    quoteRequestResponseDetailViewModels = quoteRequestResponseDetailViewModels.Where(qrrd => qrrd.QuoteRequestResponseHeader.QuoteRequestHeader.Date >= Convert.ToDateTime(dateFromFilter) && qrrd.QuoteRequestResponseHeader.QuoteRequestHeader.Date <= Convert.ToDateTime(dateToFilter)).ToList();

                    if (!string.IsNullOrEmpty(sSearch_2))
                    {
                        quoteRequestResponseDetailViewModels = quoteRequestResponseDetailViewModels.Where(qrrd => qrrd.QuoteRequestResponseHeader.Id.ToString().Contains(sSearch_2)).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_3))
                    {
                        quoteRequestResponseDetailViewModels = quoteRequestResponseDetailViewModels.Where(qrrd => qrrd.QuoteRequestResponseHeader.QuoteRequestHeaderId.ToString().Contains(sSearch_3)).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_4))
                    {
                        quoteRequestResponseDetailViewModels = quoteRequestResponseDetailViewModels.Where(qrrd => qrrd.QuoteRequestResponseHeader.Date.ToString("dd/MM/yyyy").Contains(sSearch_4)).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_5))
                    {
                        quoteRequestResponseDetailViewModels = quoteRequestResponseDetailViewModels.Where(qrrd => qrrd.MissingProduct != null ? qrrd.MissingProduct.Product.Code.ToString().ToLower().Contains(sSearch_5.ToLower()) : qrrd.AlternativeProduct.Code.ToString().ToLower().Contains(sSearch_5.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_6))
                    {
                        quoteRequestResponseDetailViewModels = quoteRequestResponseDetailViewModels.Where(qrrd => qrrd.MissingProduct != null ? qrrd.MissingProduct.Product.Description.ToString().ToLower().Contains(sSearch_6.ToLower()) : qrrd.AlternativeProduct.Description.ToString().ToLower().Contains(sSearch_6.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_7))
                    {
                        quoteRequestResponseDetailViewModels = quoteRequestResponseDetailViewModels.Where(qrrd => qrrd.QuoteRequestResponseHeader.Provider.getBussinessNameOrName.ToString().ToLower().Contains(sSearch_7.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_8))
                    {
                        quoteRequestResponseDetailViewModels = quoteRequestResponseDetailViewModels.Where(qrrd => qrrd.ProviderQuantity.ToString().ToLower().Contains(sSearch_8.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_9))
                    {
                        quoteRequestResponseDetailViewModels = quoteRequestResponseDetailViewModels.Where(qrrd => qrrd.ProviderUnitMeasure != null && qrrd.ProviderUnitMeasure.Description.ToString().ToLower().Contains(sSearch_9.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_10))
                    {
                        quoteRequestResponseDetailViewModels = quoteRequestResponseDetailViewModels.Where(qrrd => qrrd.PriceQuantity.ToString().ToLower().Contains(sSearch_10.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_11))
                    {
                        quoteRequestResponseDetailViewModels = quoteRequestResponseDetailViewModels.Where(qrrd => qrrd.PriceUnitMeasure != null && qrrd.PriceUnitMeasure.Description.ToString().ToLower().Contains(sSearch_11.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_12))
                    {
                        quoteRequestResponseDetailViewModels = quoteRequestResponseDetailViewModels.Where(qrrd => qrrd.MoneyType == 1 ? (qrrd.UnitPrice.ToString() + "ARS").ToString().ToLower().Contains(sSearch_12.ToLower()) : qrrd.MoneyType == 2 ? (qrrd.UnitPrice.ToString() + "USD").ToString().ToLower().Contains(sSearch_12.ToLower()) : qrrd.UnitPrice.ToString().ToLower().Contains(sSearch_12.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_13))
                    {
                        quoteRequestResponseDetailViewModels = quoteRequestResponseDetailViewModels.Where(qrrd => qrrd.Bonus.ToString().ToLower().Contains(sSearch_13.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_14))
                    {
                        quoteRequestResponseDetailViewModels = quoteRequestResponseDetailViewModels.Where(qrrd => qrrd.Total.ToString().ToLower().Contains(sSearch_14.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_15))
                    {
                        quoteRequestResponseDetailViewModels = quoteRequestResponseDetailViewModels.Where(qrrd => qrrd.Available ? "Si".ToString().ToLower().Contains(sSearch_15.ToLower()) : "No".ToString().ToLower().Contains(sSearch_15.ToLower())).ToList();
                    }

                    if (colIndexOrder != 0 && colOrderDirection != "")
                    {
                        var sortColumnIndex = colIndexOrder;
                        var sortDirection = colOrderDirection;

                        if (sortColumnIndex == 2)
                        {
                            quoteRequestResponseDetailViewModels = sortDirection == "asc" ? quoteRequestResponseDetailViewModels.OrderBy(qrrd => qrrd.QuoteRequestResponseHeader.Id).ToList() : quoteRequestResponseDetailViewModels.OrderByDescending(qrrd => qrrd.QuoteRequestResponseHeader.Id).ToList();
                        }
                        else if (sortColumnIndex == 3)
                        {
                            quoteRequestResponseDetailViewModels = sortDirection == "asc" ? quoteRequestResponseDetailViewModels.OrderBy(qrrd => qrrd.QuoteRequestResponseHeader.QuoteRequestHeaderId).ToList() : quoteRequestResponseDetailViewModels.OrderByDescending(qrrd => qrrd.QuoteRequestResponseHeader.QuoteRequestHeaderId).ToList();
                        }
                        else if (sortColumnIndex == 4)
                        {
                            quoteRequestResponseDetailViewModels = sortDirection == "asc" ? quoteRequestResponseDetailViewModels.OrderBy(qrrd => qrrd.QuoteRequestResponseHeader.Date).ToList() : quoteRequestResponseDetailViewModels.OrderByDescending(qrrd => qrrd.QuoteRequestResponseHeader.Date).ToList();
                        }
                        else if (sortColumnIndex == 5)
                        {
                            quoteRequestResponseDetailViewModels = sortDirection == "asc" ? quoteRequestResponseDetailViewModels.OrderBy(qrrd => qrrd.MissingProduct != null ? qrrd.MissingProduct.Product.Code.ToString() : qrrd.AlternativeProduct.Code.ToString()).ToList() : quoteRequestResponseDetailViewModels.OrderByDescending(qrrd => qrrd.MissingProduct != null ? qrrd.MissingProduct.Product.Code.ToString() : qrrd.AlternativeProduct.Code.ToString()).ToList();
                        }
                        else if (sortColumnIndex == 6)
                        {
                            quoteRequestResponseDetailViewModels = sortDirection == "asc" ? quoteRequestResponseDetailViewModels.OrderBy(qrrd => qrrd.MissingProduct != null ? qrrd.MissingProduct.Product.Description.ToString() : qrrd.AlternativeProduct.Description.ToString()).ToList() : quoteRequestResponseDetailViewModels.OrderByDescending(qrrd => qrrd.MissingProduct != null ? qrrd.MissingProduct.Product.Description.ToString() : qrrd.AlternativeProduct.Description.ToString()).ToList();
                        }
                        else if (sortColumnIndex == 7)
                        {
                            quoteRequestResponseDetailViewModels = sortDirection == "asc" ? quoteRequestResponseDetailViewModels.OrderBy(qrrd => qrrd.QuoteRequestResponseHeader.Provider.getBussinessNameOrName.ToString()).ToList() : quoteRequestResponseDetailViewModels.OrderByDescending(qrrd => qrrd.QuoteRequestResponseHeader.Provider.getBussinessNameOrName.ToString()).ToList();
                        }
                        else if (sortColumnIndex == 8)
                        {
                            quoteRequestResponseDetailViewModels = sortDirection == "asc" ? quoteRequestResponseDetailViewModels.OrderBy(qrrd => qrrd.ProviderQuantity).ToList() : quoteRequestResponseDetailViewModels.OrderByDescending(qrrd => qrrd.ProviderQuantity).ToList();
                        }
                        else if (sortColumnIndex == 9)
                        {
                            quoteRequestResponseDetailViewModels = sortDirection == "asc" ? quoteRequestResponseDetailViewModels.OrderBy(qrrd => qrrd.ProviderUnitMeasure != null ? qrrd.ProviderUnitMeasure.Description.ToString() : null).ToList() : quoteRequestResponseDetailViewModels.OrderByDescending(qrrd => qrrd.ProviderUnitMeasure != null ? qrrd.ProviderUnitMeasure.Description.ToString() : null).ToList();
                        }
                        else if (sortColumnIndex == 10)
                        {
                            quoteRequestResponseDetailViewModels = sortDirection == "asc" ? quoteRequestResponseDetailViewModels.OrderBy(qrrd => qrrd.PriceQuantity).ToList() : quoteRequestResponseDetailViewModels.OrderByDescending(qrrd => qrrd.PriceQuantity).ToList();
                        }
                        else if (sortColumnIndex == 11)
                        {
                            quoteRequestResponseDetailViewModels = sortDirection == "asc" ? quoteRequestResponseDetailViewModels.OrderBy(qrrd => qrrd.PriceUnitMeasure != null ? qrrd.PriceUnitMeasure.Description.ToString() : null).ToList() : quoteRequestResponseDetailViewModels.OrderByDescending(qrrd => qrrd.PriceUnitMeasure != null ? qrrd.PriceUnitMeasure.Description.ToString() : null).ToList();
                        }
                        else if (sortColumnIndex == 12)
                        {
                            quoteRequestResponseDetailViewModels = sortDirection == "asc" ? quoteRequestResponseDetailViewModels.OrderBy(qrrd => qrrd.UnitPrice).ToList() : quoteRequestResponseDetailViewModels.OrderByDescending(qrrd => qrrd.UnitPrice).ToList();
                        }
                        else if (sortColumnIndex == 13)
                        {
                            quoteRequestResponseDetailViewModels = sortDirection == "asc" ? quoteRequestResponseDetailViewModels.OrderBy(qrrd => qrrd.Bonus).ToList() : quoteRequestResponseDetailViewModels.OrderByDescending(qrrd => qrrd.Bonus).ToList();
                        }
                        else if (sortColumnIndex == 14)
                        {
                            quoteRequestResponseDetailViewModels = sortDirection == "asc" ? quoteRequestResponseDetailViewModels.OrderBy(qrrd => qrrd.Total).ToList() : quoteRequestResponseDetailViewModels.OrderByDescending(qrrd => qrrd.Total).ToList();
                        }
                        else if (sortColumnIndex == 15)
                        {
                            quoteRequestResponseDetailViewModels = quoteRequestResponseDetailViewModels.Where(qrrd => qrrd.Available ? "Si".ToString().ToLower().Contains(sSearch_14.ToLower()) : "No".ToString().ToLower().Contains(sSearch_13.ToLower())).ToList();
                        }
                    }

                    return ExportViewModelToExcelQRR(quoteRequestResponseDetailViewModels);
                }
                else
                {
                    _notify.Error(_localizer["Quote request responses data could not be loaded."]);
                    return null;
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return null;
            }
        }

        public IActionResult ExportViewModelToExcelQRR(IEnumerable<QuoteRequestResponseDetailViewModel> viewModel)
        {
            try
            {
                var nameForExcelFile = "Respuestas de cotizaciones_";

                var stream = new MemoryStream();
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var xlPackage = new ExcelPackage(stream))
                {
                    var workSheet = xlPackage.Workbook.Worksheets.Add("Respuestas de cotizaciones");

                    workSheet.Cells["A1"].Value = _localizer["Number"];
                    workSheet.Cells["B1"].Value = _localizer["Nº SC"];
                    workSheet.Cells["C1"].Value = _localizer["Date"];
                    workSheet.Cells["D1"].Value = _localizer["Code"];
                    workSheet.Cells["E1"].Value = _localizer["Description"];
                    workSheet.Cells["F1"].Value = _localizer["Quantity"];
                    workSheet.Cells["G1"].Value = _localizer["Provider unit measure"];
                    workSheet.Cells["H1"].Value = _localizer["Quantity"];
                    workSheet.Cells["I1"].Value = _localizer["Price unit"];
                    workSheet.Cells["J1"].Value = _localizer["Unit price"];
                    workSheet.Cells["K1"].Value = "";
                    workSheet.Cells["L1"].Value = _localizer["Bonus"];
                    workSheet.Cells["M1"].Value = "";
                    workSheet.Cells["N1"].Value = _localizer["Total"];
                    //workSheet.Cells["O1"].Value = _localizer["Available"];

                    var row = 2;
                    foreach (var vm in viewModel)
                    {
                        workSheet.Cells[row, 1].Value = vm.QuoteRequestResponseHeader.Id;
                        workSheet.Cells[row, 1].Style.Numberformat.Format = "0";

                        workSheet.Cells[row, 2].Value = vm.QuoteRequestResponseHeader.QuoteRequestHeaderId;
                        workSheet.Cells[row, 2].Style.Numberformat.Format = "0";

                        workSheet.Cells[row, 3].Value = vm.QuoteRequestResponseHeader.Date;
                        workSheet.Cells[row, 3].Style.Numberformat.Format = "dd-MM-yyyy HH:mm";

                        if (vm.MissingProduct != null)
                        {
                            if (vm.MissingProduct.Product.Code != null)
                            {
                                workSheet.Cells[row, 4].Value = vm.MissingProduct.Product.Code.ToString();
                            }
                            else
                            {
                                workSheet.Cells[row, 4].Value = "";
                            }

                            if (vm.MissingProduct.Product.Description != null)
                            {
                                workSheet.Cells[row, 5].Value = vm.MissingProduct.Product.Description.ToString();
                            }
                            else
                            {
                                workSheet.Cells[row, 5].Value = "";
                            }
                        }
                        else if (vm.AlternativeProduct != null)
                        {
                            if (vm.AlternativeProduct.Code != null)
                            {
                                workSheet.Cells[row, 4].Value = vm.AlternativeProduct.Code.ToString();
                            }
                            else
                            {
                                workSheet.Cells[row, 4].Value = "";
                            }

                            if (vm.AlternativeProduct.Description != null)
                            {
                                workSheet.Cells[row, 5].Value = vm.AlternativeProduct.Description.ToString();
                            }
                            else
                            {
                                workSheet.Cells[row, 5].Value = "";
                            }
                        }
                        else
                        {
                            workSheet.Cells[row, 4].Value = "";
                            workSheet.Cells[row, 5].Value = "";
                        }

                        workSheet.Cells[row, 6].Value = vm.ProviderQuantity;
                        workSheet.Cells[row, 6].Style.Numberformat.Format = "0.00";

                        if (vm.ProviderUnitMeasure != null)
                        {
                            if (vm.ProviderUnitMeasure.Description != null)
                            {
                                workSheet.Cells[row, 7].Value = vm.ProviderUnitMeasure.Description.ToString();
                            }
                            else
                            {
                                workSheet.Cells[row, 7].Value = "";
                            }
                        }
                        else
                        {
                            workSheet.Cells[row, 7].Value = "";
                        }

                        workSheet.Cells[row, 8].Value = vm.PriceQuantity;
                        workSheet.Cells[row, 8].Style.Numberformat.Format = "0.00";

                        if (vm.PriceUnitMeasure != null)
                        {
                            if (vm.PriceUnitMeasure.Description != null)
                            {
                                workSheet.Cells[row, 9].Value = vm.PriceUnitMeasure.Description.ToString();
                            }
                            else
                            {
                                workSheet.Cells[row, 9].Value = "";
                            }
                        }
                        else
                        {
                            workSheet.Cells[row, 9].Value = "";
                        }

                        workSheet.Cells[row, 10].Value = vm.UnitPrice;
                        workSheet.Cells[row, 10].Style.Numberformat.Format = "0.00";

                        if (vm.MoneyType == 1)
                        {
                            workSheet.Cells[row, 11].Value = "ARS";
                        }
                        else if (vm.MoneyType == 2)
                        {
                            workSheet.Cells[row, 11].Value = "USD";
                        }

                        workSheet.Cells[row, 12].Value = vm.Bonus;
                        workSheet.Cells[row, 12].Style.Numberformat.Format = "0.00";
                        workSheet.Cells[row, 13].Value = "%";

                        workSheet.Cells[row, 14].Value = vm.Total;
                        workSheet.Cells[row, 14].Style.Numberformat.Format = "0.00";

                        //if (vm.Available)
                        //{
                        //    workSheet.Cells[row, 15].Value = "Si";
                        //}
                        //else
                        //{
                        //    workSheet.Cells[row, 15].Value = "No";
                        //}

                        row++;
                    }

                    workSheet.Cells["A1:N1"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                    workSheet.Cells["N1"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

                    workSheet.Row(1).Style.Font.Bold = true;
                    workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();

                    xlPackage.Workbook.Properties.Title = nameForExcelFile + DateTime.Now.Date.ToString("dd-MM-yyyy");
                    xlPackage.Save();
                }

                stream.Position = 0;
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nameForExcelFile + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xlsx");

            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return null;
            }
        }

        #endregion

        [HttpGet]
        public async Task<JsonResult> GetProductById(int id)
        {
            try
            {
                if (id != -1)
                {
                    var response = await _productViewManager.FindBySpecification(new ProductByIdWithUnitMeasureSpecification(id));
                    if (response.Succeeded)
                    {
                        var productVM = _mapper.Map<ProductViewModel>(response.Data.FirstOrDefault());
                        return new JsonResult(new { isValid = true, product = productVM });
                    }
                    else
                    {
                        return new JsonResult(new { isValid = false, product = "" });
                    }
                }
                else
                {
                    return new JsonResult(new { isValid = false, product = "" });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return new JsonResult(new { isValid = false, product = "" });
            }
        }

        [HttpGet]
        public JsonResult ShowAlerts(string inputDetail)
        {
            if (inputDetail == "product_repeat")
            {
                _notify.Warning(_localizer["The product already exists."]);
                return new JsonResult(new { isValid = false });
            }
            else if (inputDetail == "product_notselected")
            {
                _notify.Warning(_localizer["Please, you must select a product."]);
                return new JsonResult(new { isValid = false });
            }
            else if (inputDetail == "emptyRPP")
            {
                _notify.Warning(_localizer["Provider selected not have any products assigned."]);
                return new JsonResult(new { isValid = false });
            }
            else if (inputDetail == "empty_stockUnitMeasure")
            {
                _notify.Warning(_localizer["The product selected does not have stock unit measure assigned."]);
                return new JsonResult(new { isValid = false });
            }
            else if (inputDetail == "item_notSelected")
            {
                _notify.Warning(_localizer["You must select an item."]);
                return new JsonResult(new { isValid = false });
            }
            else if (inputDetail == "provider_repeated")
            {
                _notify.Warning(_localizer["You must select items that have only one supplier."]);
                return new JsonResult(new { isValid = false });
            }
            return null;
        }

    }
}
