using ERP.Application.Constants;
using ERP.Application.DTOs.Entities.Commons;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Application.DTOs.Entities.Purchases.Providers;
using ERP.Application.Interfaces.ViewManagers.Commons;
using ERP.Application.Interfaces.ViewManagers.ProductMod;
using ERP.Application.Interfaces.ViewManagers.Purchases.Providers;
using ERP.Application.Specification.Commons.DollarExchangeRateSpecifications;
using ERP.Application.Specification.Commons.UnitMeasureSpecifications;
using ERP.Application.Specification.ProductMod;
using ERP.Application.Specification.Purchases.ProviderProduct;
using ERP.Language;
using ERP.Web.Abstractions;
using ERP.Web.Areas.ProductMod.Models;
using ERP.Web.Areas.Purchases.Models.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Web.Areas.Purchases.Controllers.Provider
{
    [Area("Purchases")]
    public class ProviderProductController : BaseController<ProviderController>
    {
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly IProviderViewManager _providerViewManager;
        private readonly IProductViewManager _productViewManager;
        private readonly IRelProviderProductViewManager _relProviderProductViewManager;
        private readonly IUnitMeasureViewManager _unitMeasureViewManager;
        private readonly IDollarExchangeRateViewManager _dollarExchangeRateViewManager;

        public ProviderProductController(IStringLocalizer<SharedResource> localizer, IProviderViewManager providerViewManager, IProductViewManager productViewManager, IRelProviderProductViewManager relProviderProductViewManager,
            IUnitMeasureViewManager unitMeasureViewManager, IDollarExchangeRateViewManager dollarExchangeRateViewManager)
        {
            _localizer = localizer;
            _providerViewManager = providerViewManager;
            _productViewManager = productViewManager;
            _relProviderProductViewManager = relProviderProductViewManager;
            _unitMeasureViewManager = unitMeasureViewManager;
            _dollarExchangeRateViewManager = dollarExchangeRateViewManager;
        }

        public IActionResult Index(int id, string providerName)
        {
            var model = new RelProviderProductViewModel();
            model.ProviderId = id;
            ViewBag.ProviderName = providerName;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> LoadAll(int id)
        {
            // Cargo todos los productos del proveedor que se seleccionado anteriormente.
            var response = await _relProviderProductViewManager.FindBySpecification(new RelProviderProductByProviderIdSpecification(id));
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<RelProviderProductViewModel>>(response.Data);
                ViewBag.ProviderId = id;
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }

        public IActionResult SearchIndex()
        {
            var model = new RelProviderProductViewModel();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> LoadAllProviderProducts()
        {
            var response = await _relProviderProductViewManager.FindBySpecification(new RelProviderProductWithAllSpecification());
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<RelProviderProductViewModel>>(response.Data);
                return PartialView("_ViewAllPP", viewModel);
            }
            return null;
        }

        [Authorize(Policy = Permissions.Providers.View)]
        public async Task<JsonResult> OnGetCreateOrEdit(int providerId, bool viewDetails, bool editFromAllProductsProviders = false, int id = 0)
        {
            var unitMeasures = await _unitMeasureViewManager.FindBySpecification(new UnitMeasureIsActiveSpecification());

            if (id == 0)
            {
                // Add new product for this provider
                var RelProviderProductViewModel = new RelProviderProductViewModel();

                RelProviderProductViewModel.ProviderId = providerId;

                if (unitMeasures.Succeeded)
                {
                    var unitMeasureViewModel = _mapper.Map<List<UnitMeasureDTO>>(unitMeasures.Data);
                    RelProviderProductViewModel.UnitsMeasure = new SelectList(unitMeasureViewModel, nameof(UnitMeasureViewModel.Id), nameof(UnitMeasureViewModel.Description), null, null);
                }

                //var productsResponse = await _productViewManager.FindBySpecification(new ProductWithAllSpecification());
                // Se cambio de Specification para optimizar el tiempo de carga en el select2.
                var productsResponse = await _productViewManager.FindBySpecification(new ProductWithAllUnitMeasuresSpecification());
                if (productsResponse.Succeeded)
                {
                    var productsViewModel = _mapper.Map<List<ProductDTO>>(productsResponse.Data);
                    RelProviderProductViewModel.Products = new SelectList((from p in productsViewModel.OrderBy(p => p.Description).ToList() select new { Id = p.Id, CodeAndDescription = p.Code + " - " + p.Description }), "Id", "CodeAndDescription", null);
                }

                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", RelProviderProductViewModel) });

            }
            else
            {
                // Update products for this provider
                var response = await _relProviderProductViewManager.FindBySpecification(new RelProviderProductWithAllSpecification(id));

                if (response.Succeeded)
                {

                    var RelProviderProductViewModel = _mapper.Map<List<RelProviderProductViewModel>>(response.Data).FirstOrDefault();

                    if (unitMeasures.Succeeded)
                    {
                        var unitMeasureViewModel = _mapper.Map<List<UnitMeasureDTO>>(unitMeasures.Data);
                        RelProviderProductViewModel.UnitsMeasure = new SelectList(unitMeasureViewModel, nameof(UnitMeasureViewModel.Id), nameof(UnitMeasureViewModel.Description), null, null);
                    }

                    //var productsResponse = await _productViewManager.FindBySpecification(new ProductWithAllSpecification());
                    // Se cambio de Specification para optimizar el tiempo de carga en el select2.
                    var productsResponse = await _productViewManager.FindBySpecification(new ProductWithAllUnitMeasuresSpecification());
                    if (productsResponse.Succeeded)
                    {
                        var productsViewModel = _mapper.Map<List<ProductDTO>>(productsResponse.Data);
                        RelProviderProductViewModel.Products = new SelectList((from p in productsViewModel.OrderBy(p => p.Description).ToList() select new { Id = p.Id, CodeAndDescription = p.Code + " - " + p.Description }), "Id", "CodeAndDescription", null);
                    }

                    RelProviderProductViewModel.viewDetails = viewDetails;
                    RelProviderProductViewModel.editFromAllProductsProviders = editFromAllProductsProviders;

                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", RelProviderProductViewModel) });
                }

                return null;

            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(int id, int providerId, RelProviderProductViewModel relProviderProductViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var unitMeasures = await _unitMeasureViewManager.FindBySpecification(new UnitMeasureIsActiveSpecification());
                    if (unitMeasures.Succeeded)
                    {
                        var unitMeasureViewModel = _mapper.Map<List<UnitMeasureDTO>>(unitMeasures.Data);
                        relProviderProductViewModel.UnitsMeasure = new SelectList(unitMeasureViewModel, nameof(UnitMeasureViewModel.Id), nameof(UnitMeasureViewModel.Description), null, null);
                    }

                    if (relProviderProductViewModel.ProductId == -1)
                    {
                        _notify.Warning(_localizer["You must select a product."]);
                        return new JsonResult(new { isValid = false, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", relProviderProductViewModel) });
                    }

                    if (id == 0)
                    {
                        // Validar que no se agreguen productos dos veces
                        var RPPByProviderIdResponse = _relProviderProductViewManager.FindBySpecification(new RelProviderProductByProviderIdSpecification(relProviderProductViewModel.ProviderId));
                        if (RPPByProviderIdResponse.Result.Succeeded)
                        {
                            if (RPPByProviderIdResponse.Result.Data.Any(rpp => rpp.ProductId == relProviderProductViewModel.ProductId))
                            {
                                _notify.Warning(_localizer["The product already belongs to the provider."]);
                                return new JsonResult(new { isValid = false, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", relProviderProductViewModel) });
                            }
                        }

                        // Add new product for the provider selected
                        var relProviderProductDTO = _mapper.Map<RelProviderProductDTO>(relProviderProductViewModel);
                        var result = await _relProviderProductViewManager.CreateAsync(relProviderProductDTO);
                        if (result.Succeeded)
                        {
                            id = result.Data;
                            _notify.Success(_localizer["The product was assigned."]);
                            return new JsonResult(new { isValid = true, html = "" });
                        }
                        else
                        {
                            _notify.Error(result.Message);
                            return new JsonResult(new { isValid = false, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", relProviderProductViewModel) });
                        }
                    }
                    else
                    {
                        // Update products for the provider selected
                        var relProviderProductDTO = _mapper.Map<RelProviderProductDTO>(relProviderProductViewModel);
                        var result = await _relProviderProductViewManager.UpdateAsync(relProviderProductDTO);

                        if (result.Succeeded)
                        {
                            id = result.Data;
                            _notify.Information(_localizer["The product was updated."]);
                            return new JsonResult(new { isValid = true, html = "" });
                        }
                    }

                    var response = await _relProviderProductViewManager.FindBySpecification(new RelProviderProductByProviderIdSpecification(relProviderProductViewModel.ProviderId));
                    if (response.Succeeded)
                    {
                        return new JsonResult(new { isValid = true, html = "" });
                    }
                    else
                    {
                        _notify.Error(response.Message);
                        return new JsonResult(new { isValid = false, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", relProviderProductViewModel) });
                    }
                }
                else
                {
                    //return new JsonResult(new { isValid = false, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", relProviderProductViewModel) });
                    return new JsonResult(new { isValid = false });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return new JsonResult(new { isValid = false, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", relProviderProductViewModel) });
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostDelete(int id, int providerId)
        {
            var deleteCommand = await _relProviderProductViewManager.DeleteAsync(id);
            if (deleteCommand.Succeeded)
            {
                _notify.Information(_localizer["The product was removed."]);

                var response = await _relProviderProductViewManager.FindBySpecification(new RelProviderProductByProviderIdSpecification(providerId));
                if (response.Succeeded)
                {
                    return new JsonResult(new { isValid = true });
                }
                else
                {
                    _notify.Error(response.Message);
                    return null;
                }
            }
            else
            {
                _notify.Error(deleteCommand.Message);
                return null;
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetDollarQuotation(decimal pesosInput, decimal dollarInput)
        {
            try
            {
                decimal pesosToDollar = 0;
                decimal dollarToPesos = 0;
                decimal salePrice = 0;
                decimal purchasePrice = 0;
                DateTime date = new DateTime();
                // Tengo que buscar por fecha de hoy en la tabla DollarExchangeRates
                var getDollarToday = await _dollarExchangeRateViewManager.FindBySpecification(new DollarExchangeRateByTodayDateSpecification(DateTime.Now.Date));
                if (getDollarToday.Succeeded)
                {
                    if (getDollarToday.Data.Count > 0)
                    {
                        foreach (var dt in getDollarToday.Data)
                        {
                            if (pesosInput != 0)
                            {
                                pesosToDollar = pesosInput / dt.PurchasePrice;
                            }
                            else if (dollarInput != 0)
                            {
                                dollarToPesos = dollarInput * dt.PurchasePrice;
                            }

                            salePrice = dt.SalePrice;
                            purchasePrice = dt.PurchasePrice;
                            date = (DateTime)dt.Date;
                        }

                        return new JsonResult(new { pesosToDollar = Math.Round(pesosToDollar, 2), dollarToPesos = Math.Round(dollarToPesos, 2), salePrice = Math.Round(salePrice, 2), purchasePrice = Math.Round(purchasePrice, 2), date = date.ToString("dd/MM/yyyy") });
                    }
                    else
                    {
                        // Si no hay ningun registro con fecha actual, es porque todavia no se actualizó la cotización del dolar, me traigo la ultima.
                        var getDollarLastMonth = await _dollarExchangeRateViewManager.FindBySpecification(new LastDollarExchangeRateSpecification());
                        if (getDollarLastMonth.Succeeded)
                        {
                            if (getDollarLastMonth.Data.Count > 0)
                            {
                                var lastDollarExchangeRate = getDollarLastMonth.Data.FirstOrDefault();

                                if (pesosInput != 0)
                                {
                                    pesosToDollar = pesosInput / lastDollarExchangeRate.PurchasePrice;
                                }
                                else if (dollarInput != 0)
                                {
                                    dollarToPesos = dollarInput * lastDollarExchangeRate.PurchasePrice;
                                }

                                salePrice = lastDollarExchangeRate.SalePrice;
                                purchasePrice = lastDollarExchangeRate.PurchasePrice;
                                date = (DateTime)lastDollarExchangeRate.Date;

                                return new JsonResult(new { pesosToDollar = Math.Round(pesosToDollar, 2), dollarToPesos = Math.Round(dollarToPesos, 2), salePrice = Math.Round(salePrice, 2), purchasePrice = Math.Round(purchasePrice, 2), date = date.ToString("dd/MM/yyyy") });
                            }
                        }
                    }
                }
                _notify.Warning(_localizer["Attention, make sure the price of the dollar is stored."]);
                return new JsonResult(new { pesosToDollar = Math.Round(pesosToDollar, 2), dollarToPesos = Math.Round(dollarToPesos, 2), salePrice = Math.Round(salePrice, 2), purchasePrice = Math.Round(purchasePrice, 2), date = date.ToString("dd/MM/yyyy") });
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return null;
            }
        }
    }
}
