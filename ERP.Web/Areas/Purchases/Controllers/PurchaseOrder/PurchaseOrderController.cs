using ERP.Application.Constants;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Application.DTOs.Entities.Purchases.MissingProducts;
using ERP.Application.DTOs.Entities.Purchases.PurchaseOrders;
using ERP.Application.Interfaces.ViewManagers.Commons;
using ERP.Application.Interfaces.ViewManagers.ProductMod;
using ERP.Application.Interfaces.ViewManagers.Purchases.MissingProducts;
using ERP.Application.Interfaces.ViewManagers.Purchases.Providers;
using ERP.Application.Interfaces.ViewManagers.Purchases.PurchaseOrders;
using ERP.Application.Interfaces.ViewManagers.Purchases.QuoteRequests;
using ERP.Application.Specification.ProductMod;
using ERP.Application.Specification.Purchases.MissingProducts;
using ERP.Application.Specification.Purchases.ProviderProduct;
using ERP.Application.Specification.Purchases.PurchaseOrders;
using ERP.Application.Specification.Purchases.QuoteRequests;
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

namespace ERP.Web.Areas.Purchases.Controllers.PurchaseOrder
{
    [Area("Purchases")]
    public class PurchaseOrderController : BaseController<PurchaseOrderController>
    {
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly IPurchaseOrderHeaderViewManager _purchaseOrderHeaderViewManager;
        private readonly IPurchaseOrderDetailViewManager _purchaseOrderDetailViewManager;
        private readonly IMissingProductViewManager _missingProductViewManager;
        private readonly IRelProviderProductViewManager _relProviderProductViewManager;
        private readonly IQuoteRequestResponseHeaderViewManager _quoteRequestResponseHeaderViewManager;
        private readonly IProductViewManager _productViewManager;
        private readonly IProviderViewManager _providerViewManager;

        public PurchaseOrderController(IStringLocalizer<SharedResource> localizer, IPurchaseOrderHeaderViewManager purchaseOrderHeaderViewManager, IPurchaseOrderDetailViewManager purchaseOrderDetailViewManager, IMissingProductViewManager missingProductViewManager, IRelProviderProductViewManager relProviderProductViewManager, IQuoteRequestResponseHeaderViewManager quoteRequestResponseHeaderViewManager, IProductViewManager productViewManager, IProviderViewManager providerViewManager, IUnitMeasureViewManager unitMeasureViewManager)
        {
            _localizer = localizer;
            _purchaseOrderHeaderViewManager = purchaseOrderHeaderViewManager;
            _purchaseOrderDetailViewManager = purchaseOrderDetailViewManager;
            _missingProductViewManager = missingProductViewManager;
            _relProviderProductViewManager = relProviderProductViewManager;
            _quoteRequestResponseHeaderViewManager = quoteRequestResponseHeaderViewManager;
            _productViewManager = productViewManager;
            _providerViewManager = providerViewManager;
        }

        public IActionResult _Index()
        {
            var model = new PurchaseOrderHeaderViewModel();
            return View(model);
        }

        public async Task<IActionResult> _LoadAll(DatatableParamsViewModel dataTableParams,
            string sSearch_2, string sSearch_3, string sSearch_4, string sSearch_5, string sSearch_6, string sSearch_7, string sSearch_8, string sSearch_9, string sSearch_10,
            string sSearch_11, string sSearch_12, string sSearch_13, string sSearch_14, DateTime dateFromFilter, DateTime dateToFilter)
        {
            try
            {
                var response = await _purchaseOrderDetailViewManager.FindBySpecification(new PurchasesOrderDetailWithAllSpecification());
                if (response.Succeeded)
                {
                    var purchaseOrderDetailViewModels = _mapper.Map<List<PurchaseOrderDetailViewModel>>(response.Data);

                    // From date -> To date
                    purchaseOrderDetailViewModels = purchaseOrderDetailViewModels.Where(pod => pod.PurchaseOrderHeader.Date >= dateFromFilter && pod.PurchaseOrderHeader.Date <= dateToFilter).ToList();

                    if (!string.IsNullOrEmpty(sSearch_2))
                    {
                        purchaseOrderDetailViewModels = purchaseOrderDetailViewModels.Where(pod => pod.PurchaseOrderHeader.Id.ToString().Contains(sSearch_2)).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_3))
                    {
                        purchaseOrderDetailViewModels = purchaseOrderDetailViewModels.Where(pod => pod.PurchaseOrderHeader.Date.ToString("dd/MM/yyyy").Contains(sSearch_3)).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_4))
                    {
                        purchaseOrderDetailViewModels = purchaseOrderDetailViewModels.Where(pod => pod.MissingProduct != null ? pod.MissingProduct.Product.Code.ToString().ToLower().Contains(sSearch_4.ToLower()) : pod.Product.Code.ToString().ToLower().Contains(sSearch_4.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_5))
                    {
                        purchaseOrderDetailViewModels = purchaseOrderDetailViewModels.Where(pod => pod.MissingProduct != null ? pod.MissingProduct.Product.Description.ToString().ToLower().Contains(sSearch_5.ToLower()) : pod.Product.Description.ToString().ToLower().Contains(sSearch_5.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_6))
                    {
                        purchaseOrderDetailViewModels = purchaseOrderDetailViewModels.Where(pod => pod.PurchaseOrderHeader.Provider.getBussinessNameOrName.ToString().ToLower().Contains(sSearch_6.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_7))
                    {
                        purchaseOrderDetailViewModels = purchaseOrderDetailViewModels.Where(pod => pod.ProviderQuantity.ToString().ToLower().Contains(sSearch_7.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_8))
                    {
                        purchaseOrderDetailViewModels = purchaseOrderDetailViewModels.Where(pod => pod.ProviderUnitMeasure != null && pod.ProviderUnitMeasure.Description.ToString().ToLower().Contains(sSearch_8.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_9))
                    {
                        purchaseOrderDetailViewModels = purchaseOrderDetailViewModels.Where(pod => pod.PriceQuantity.ToString().ToLower().Contains(sSearch_9.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_10))
                    {
                        purchaseOrderDetailViewModels = purchaseOrderDetailViewModels.Where(pod => pod.PriceUnitMeasure != null && pod.PriceUnitMeasure.Description.ToString().ToLower().Contains(sSearch_10.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_11))
                    {
                        purchaseOrderDetailViewModels = purchaseOrderDetailViewModels.Where(pod => pod.MoneyType == 1 ? (pod.UnitPrice.ToString() + "ARS").ToString().ToLower().Contains(sSearch_11.ToLower()) : pod.MoneyType == 2 ? (pod.UnitPrice.ToString() + "USD").ToString().ToLower().Contains(sSearch_11.ToLower()) : pod.UnitPrice.ToString().ToLower().Contains(sSearch_11.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_12))
                    {
                        purchaseOrderDetailViewModels = purchaseOrderDetailViewModels.Where(pod => pod.Bonus.ToString().ToLower().Contains(sSearch_12.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_13))
                    {
                        purchaseOrderDetailViewModels = purchaseOrderDetailViewModels.Where(pod => pod.Total.ToString().ToLower().Contains(sSearch_13.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_14))
                    {
                        purchaseOrderDetailViewModels = purchaseOrderDetailViewModels.Where(pod => pod.PurchaseStateId == 8 ? _localizer["Yes"].ToString().ToLower().Contains(sSearch_14.ToString().ToLower()) : "".ToString().ToLower().Contains(sSearch_14.ToString().ToLower())).ToList();
                    }

                    var sortColumnIndex = dataTableParams.iSortCol_0;
                    var sortDirection = dataTableParams.sSortDir_0;

                    if (sortColumnIndex == 2)
                    {
                        purchaseOrderDetailViewModels = sortDirection == "asc" ? purchaseOrderDetailViewModels.OrderBy(pod => pod.PurchaseOrderHeader.Id).ToList() : purchaseOrderDetailViewModels.OrderByDescending(pod => pod.PurchaseOrderHeader.Id).ToList();
                    }
                    else if (sortColumnIndex == 3)
                    {
                        purchaseOrderDetailViewModels = sortDirection == "asc" ? purchaseOrderDetailViewModels.OrderBy(pod => pod.PurchaseOrderHeader.Date).ToList() : purchaseOrderDetailViewModels.OrderByDescending(pod => pod.PurchaseOrderHeader.Date).ToList();
                    }
                    else if (sortColumnIndex == 4)
                    {
                        purchaseOrderDetailViewModels = sortDirection == "asc" ? purchaseOrderDetailViewModels.OrderBy(pod => pod.MissingProduct != null ? pod.MissingProduct.Product.Code.ToString() : pod.Product.Code.ToString()).ToList() : purchaseOrderDetailViewModels.OrderByDescending(pod => pod.MissingProduct != null ? pod.MissingProduct.Product.Code.ToString() : pod.Product.Code.ToString()).ToList();
                    }
                    else if (sortColumnIndex == 5)
                    {
                        purchaseOrderDetailViewModels = sortDirection == "asc" ? purchaseOrderDetailViewModels.OrderBy(pod => pod.MissingProduct != null ? pod.MissingProduct.Product.Description.ToString() : pod.Product.Description.ToString()).ToList() : purchaseOrderDetailViewModels.OrderByDescending(pod => pod.MissingProduct != null ? pod.MissingProduct.Product.Description.ToString() : pod.Product.Description.ToString()).ToList();
                    }
                    else if (sortColumnIndex == 6)
                    {
                        purchaseOrderDetailViewModels = sortDirection == "asc" ? purchaseOrderDetailViewModels.OrderBy(pod => pod.PurchaseOrderHeader.Provider.getBussinessNameOrName.ToString()).ToList() : purchaseOrderDetailViewModels.OrderByDescending(pod => pod.PurchaseOrderHeader.Provider.getBussinessNameOrName.ToString()).ToList();
                    }
                    else if (sortColumnIndex == 7)
                    {
                        purchaseOrderDetailViewModels = sortDirection == "asc" ? purchaseOrderDetailViewModels.OrderBy(pod => pod.ProviderQuantity).ToList() : purchaseOrderDetailViewModels.OrderByDescending(pod => pod.ProviderQuantity).ToList();
                    }
                    else if (sortColumnIndex == 8)
                    {
                        purchaseOrderDetailViewModels = sortDirection == "asc" ? purchaseOrderDetailViewModels.OrderBy(pod => pod.ProviderUnitMeasure != null ? pod.ProviderUnitMeasure.Description.ToString() : null).ToList() : purchaseOrderDetailViewModels.OrderByDescending(pod => pod.ProviderUnitMeasure != null ? pod.ProviderUnitMeasure.Description.ToString() : null).ToList();
                    }
                    else if (sortColumnIndex == 9)
                    {
                        purchaseOrderDetailViewModels = sortDirection == "asc" ? purchaseOrderDetailViewModels.OrderBy(pod => pod.PriceQuantity).ToList() : purchaseOrderDetailViewModels.OrderByDescending(pod => pod.PriceQuantity).ToList();
                    }
                    else if (sortColumnIndex == 10)
                    {
                        purchaseOrderDetailViewModels = sortDirection == "asc" ? purchaseOrderDetailViewModels.OrderBy(pod => pod.PriceUnitMeasure != null ? pod.PriceUnitMeasure.Description.ToString() : null).ToList() : purchaseOrderDetailViewModels.OrderByDescending(pod => pod.PriceUnitMeasure != null ? pod.PriceUnitMeasure.Description.ToString() : null).ToList();
                    }
                    else if (sortColumnIndex == 11)
                    {
                        purchaseOrderDetailViewModels = sortDirection == "asc" ? purchaseOrderDetailViewModels.OrderBy(pod => pod.UnitPrice).ToList() : purchaseOrderDetailViewModels.OrderByDescending(pod => pod.UnitPrice).ToList();
                    }
                    else if (sortColumnIndex == 12)
                    {
                        purchaseOrderDetailViewModels = sortDirection == "asc" ? purchaseOrderDetailViewModels.OrderBy(pod => pod.Bonus).ToList() : purchaseOrderDetailViewModels.OrderByDescending(pod => pod.Bonus).ToList();
                    }
                    else if (sortColumnIndex == 13)
                    {
                        purchaseOrderDetailViewModels = sortDirection == "asc" ? purchaseOrderDetailViewModels.OrderBy(pod => pod.Total).ToList() : purchaseOrderDetailViewModels.OrderByDescending(pod => pod.Total).ToList();
                    }
                    else if (sortColumnIndex == 14)
                    {
                        purchaseOrderDetailViewModels = sortDirection == "asc" ? purchaseOrderDetailViewModels.OrderBy(pod => pod.PurchaseStateId == 8 ? _localizer["Yes"] : "").ToList() : purchaseOrderDetailViewModels.OrderByDescending(pod => pod.PurchaseStateId == 8 ? _localizer["Yes"] : "").ToList();
                    }

                    var displayResult = purchaseOrderDetailViewModels.Skip(dataTableParams.iDisplayStart)
                        .Take(dataTableParams.iDisplayLength).ToList();

                    var totalRecords = purchaseOrderDetailViewModels.Count();

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
                    _notify.Error(_localizer["Purchase orders data could not be loaded."]);
                    return null;
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return null;
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
                            if (rpp.UnitMeasureId.HasValue && productVM.UnitMeasureId.HasValue)
                            {
                                // Si la unidad del proveedor es igual que la unidad del producto:
                                if ((rpp.UnitMeasureId.Value == productVM.UnitMeasureId.Value) && (productVM.QuantityToOrder.HasValue && productVM.QuantityToOrderUnitMeasure != null && productVM.QuantityToOrderUnitMeasureId.HasValue))
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
                                            // Barras, Rollos, Metros o Bobinas
                                            if (rpp.UnitMeasureId.Value == 22 || rpp.UnitMeasureId.Value == 23 || rpp.UnitMeasureId.Value == 2 || rpp.UnitMeasureId.Value == 32)
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
                                                if (rpp.UnitMeasureId.Value == 22 || rpp.UnitMeasureId.Value == 23 || rpp.UnitMeasureId.Value == 32)
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
                                            // Barras, Rollos, Metros o Bobinas
                                            if (rpp.UnitMeasureId.Value == 22 || rpp.UnitMeasureId.Value == 23 || rpp.UnitMeasureId.Value == 2 || rpp.UnitMeasureId.Value == 32)
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
                                                if (rpp.UnitMeasureId.Value == 22 || rpp.UnitMeasureId.Value == 23 || rpp.UnitMeasureId.Value == 32)
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

        [Authorize(Policy = Permissions.PurchaseOrder.View)]
        public async Task<JsonResult> OnGetCreate(int id = 0)
        {
            if (id == 0)
            {
                // For create
                var purchaseOrderViewModel = new PurchaseOrderHeaderViewModel();

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

                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_Create", purchaseOrderViewModel) });
            }
            else
            {
                // For update
                var getPOByHeaderId = await _purchaseOrderHeaderViewManager.FindBySpecification(new PurchaseOrderWithAllSpecifications(id));
                if (getPOByHeaderId.Succeeded)
                {
                    var purchaseOrderViewModel = _mapper.Map<PurchaseOrderHeaderViewModel>(getPOByHeaderId.Data.First());

                    HashSet<ProductViewModel> productsViewModel = new HashSet<ProductViewModel>();
                    var relProviderProductsResponse = await _relProviderProductViewManager.FindBySpecification(new RelProviderProductByProviderIdSpecification(purchaseOrderViewModel.ProviderId));
                    if (relProviderProductsResponse.Succeeded)
                    {
                        foreach (var relProviderProduct in relProviderProductsResponse.Data)
                        {
                            var productVM = _mapper.Map<ProductViewModel>(relProviderProduct.Product);
                            productsViewModel.Add(productVM);
                        }
                    }

                    purchaseOrderViewModel.Number = purchaseOrderViewModel.Id;

                    purchaseOrderViewModel.AlternativeProducts = new SelectList(productsViewModel.OrderBy(x => x.CodeAndDescription), nameof(ProductViewModel.Id), nameof(ProductViewModel.CodeAndDescription), null, null);


                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_Create", purchaseOrderViewModel) });
                }
            }
            return null;
        }

        // Método encargado de generar una orden de compra, y ademas, si se genera desde solicitud de cotizaciones, se actualiza el estado de la cotizacion
        // y el estado de los productos faltantes a "Comprado"
        [HttpPost]
        public async Task<JsonResult> OnPostCreate(int id, PurchaseOrderHeaderViewModel purchaseOrderHeaderViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (purchaseOrderHeaderViewModel.ProviderId <= 0)
                    {
                        purchaseOrderHeaderViewModel = await LoadDataForRefreshFormModal(purchaseOrderHeaderViewModel);
                        _notify.Warning(_localizer["You must select a provider."]);
                        return new JsonResult(new { isValid = false, html = await _viewRenderer.RenderViewToStringAsync("_Create", purchaseOrderHeaderViewModel) });
                    }

                    var detailsPurchaseOrder = purchaseOrderHeaderViewModel.PurchaseOrderDetails.Where(x => x != null).ToList();
                    if (detailsPurchaseOrder.Count() > 0)
                    {
                        purchaseOrderHeaderViewModel.PurchaseOrderDetails = detailsPurchaseOrder;

                        if (id == 0)
                        {
                            // Create
                            foreach (var poVM in purchaseOrderHeaderViewModel.PurchaseOrderDetails)
                            {
                                if (poVM.MissingProductId != null && poVM.MissingProductId.HasValue)
                                {
                                    var providerAndQuotationQuantityDiff = await getUnitsAndQuantityDiff(purchaseOrderHeaderViewModel.ProviderId, poVM.MissingProductId.Value, 0);
                                    if (providerAndQuotationQuantityDiff.Count > 0)
                                    {
                                        poVM.OriginalProviderQuantity = providerAndQuotationQuantityDiff[0];
                                        poVM.OriginalPriceQuantity = providerAndQuotationQuantityDiff[1];
                                    }

                                    decimal purchasedQuantity = await getQuantityToOrderByProviderQuantity(purchaseOrderHeaderViewModel.ProviderId, poVM.MissingProductId.Value, poVM.ProviderQuantity);
                                    poVM.PurchasedQuantity = purchasedQuantity;
                                }
                                else if (poVM.ProductId != null && poVM.ProductId.HasValue)
                                {
                                    var providerAndQuotationQuantityDiff = await getUnitsAndQuantityDiff(purchaseOrderHeaderViewModel.ProviderId, 0, poVM.ProductId.Value);
                                    if (providerAndQuotationQuantityDiff.Count > 0)
                                    {
                                        poVM.OriginalProviderQuantity = providerAndQuotationQuantityDiff[0];
                                        poVM.OriginalPriceQuantity = providerAndQuotationQuantityDiff[1];
                                    }

                                }
                            }

                            var purchaseOrderHeaderDTO = _mapper.Map<PurchaseOrderHeaderDTO>(purchaseOrderHeaderViewModel);
                            var result = await _purchaseOrderHeaderViewManager.CreateAsync(purchaseOrderHeaderDTO);
                            if (result.Succeeded)
                            {
                                id = result.Data;

                                _notify.Success(_localizer["Purchase order created."]);
                                return new JsonResult(new { isValid = true, pohId = id });
                            }
                            else
                            {
                                purchaseOrderHeaderViewModel = await LoadDataForRefreshFormModal(purchaseOrderHeaderViewModel);
                                _notify.Error(result.Message);
                                var html = await _viewRenderer.RenderViewToStringAsync("_Create", purchaseOrderHeaderViewModel);
                                return new JsonResult(new { isValid = false, html = html });
                            }

                        }
                        else
                        {
                            // Update
                            var purchaseOrderHeaderDTO = _mapper.Map<PurchaseOrderHeaderDTO>(purchaseOrderHeaderViewModel);
                            var result = await _purchaseOrderHeaderViewManager.UpdateAsync(purchaseOrderHeaderDTO);
                            if (result.Succeeded)
                            {
                                _notify.Success(_localizer["Purchase order updated."]);
                                return new JsonResult(new { isValid = true, pohId = id });
                            }
                            else
                            {
                                _notify.Error(result.Message);
                                purchaseOrderHeaderViewModel = await LoadDataForRefreshFormModal(purchaseOrderHeaderViewModel);
                                var html = await _viewRenderer.RenderViewToStringAsync("_Create", purchaseOrderHeaderViewModel);
                                return new JsonResult(new { isValid = false, html = html });
                            }
                        }
                    }
                    else
                    {
                        purchaseOrderHeaderViewModel = await LoadDataForRefreshFormModal(purchaseOrderHeaderViewModel);
                        _notify.Warning(_localizer["You must add detail for this purchase order."]);
                        var html = await _viewRenderer.RenderViewToStringAsync("_Create", purchaseOrderHeaderViewModel);
                        return new JsonResult(new { isValid = false, html = html });
                    }
                }
                else
                {
                    purchaseOrderHeaderViewModel = await LoadDataForRefreshFormModal(purchaseOrderHeaderViewModel);
                    var html = await _viewRenderer.RenderViewToStringAsync("_Create", purchaseOrderHeaderViewModel);
                    return new JsonResult(new { isValid = false, html = html });
                }
            }
            catch (Exception ex)
            {
                purchaseOrderHeaderViewModel = await LoadDataForRefreshFormModal(purchaseOrderHeaderViewModel);
                _notify.Error(ex.Message);
                var html = await _viewRenderer.RenderViewToStringAsync("_Create", purchaseOrderHeaderViewModel);
                return new JsonResult(new { isValid = false, html = html });
            }
        }

        #region Fran 16/5/2023: Se mudo esta funcionalidad al servicio PurchaseOrderHeaderService
        //// Una vez que se genera la Orden de compra -> Se deben generar registros de ingresos con los detalles de compra.
        //public async Task<int> createIncomesWithPurchaseOrderDetails(PurchaseOrderHeaderViewModel pohVM)
        //{
        //    var incomeHeaderVM = new IncomeHeaderViewModel();
        //    incomeHeaderVM.IncomeDate = pohVM.Date;
        //    incomeHeaderVM.ProviderId = pohVM.ProviderId;
        //    incomeHeaderVM.TransportProviderId = pohVM.ProviderId;
        //    incomeHeaderVM.OwnTransport = true;
        //    // El numero de remito es el de recepcion
        //    //incomeHeaderVM.DeliveryNoteNumber
        //    // Numero de factura    
        //    //incomeHeaderVM.InvoiceNumber
        //    // Nulo
        //    //incomeHeaderVM.ExternalProcessStationId

        //    foreach (var podVM in pohVM.PurchaseOrderDetails)
        //    {
        //        var incomeDetailVM = new IncomeDetailViewModel();
        //        incomeDetailVM.IncomeProductId = podVM.MissingProductId.Value;
        //        incomeDetailVM.Quantity = podVM.ProviderQuantity;
        //        incomeDetailVM.UnitId = podVM.ProviderUnitMeasureId;
        //        //incomeDetailVM.BatchNumber
        //        //incomeDetailVM.OTNumber
        //        //incomeDetailVM.FatherProductId
        //        //incomeDetailVM.FatherStructureId
        //        incomeDetailVM.OCNumber = pohVM.Number;
        //        // Estado: "Pendiente de recepcion"
        //        incomeDetailVM.IncomeStateId = 1;

        //        // Set "Reception" and "NextStation"
        //        var getProductById = await _productViewManager.FindBySpecification(new ProductWithSubCategoryAndCategorySpecification(incomeDetailVM.IncomeProductId));
        //        if (getProductById.Succeeded)
        //        {
        //            // Si el producto es CT: en recepcion tiene recepcion R0algo y en NextStation tiene un almacen A0algo.
        //            if (getProductById.Data.First().SubCategory.Prefix == "CT" || getProductById.Data.First().SubCategory.CategoryId == 4)
        //            {
        //                var getRPSByProductId = await _productViewManager.FindBySpecification(new ProductWithRelProductStationsSpecification(incomeDetailVM.IncomeProductId));
        //                if (getRPSByProductId.Succeeded)
        //                {
        //                    var relProductStationVM = _mapper.Map<List<RelProductStationViewModel>>(getRPSByProductId.Data.First().RelProductStations);
        //                    for (int i = 0; i < relProductStationVM.Count() - 1; i++)
        //                    {
        //                        incomeDetailVM.Reception = relProductStationVM[i].Station.AbreviationDescription;
        //                        if ((i + 1) <= (relProductStationVM.Count() - 1))
        //                        {
        //                            incomeDetailVM.NextStation = relProductStationVM[i + 1].Station.AbreviationDescription;
        //                        }
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                // Reception by default
        //                incomeDetailVM.NextStation = "";
        //            }
        //        }

        //        incomeHeaderVM.IncomeDetails.Add(incomeDetailVM);
        //    }

        //    var incomeHeaderDTO = _mapper.Map<IncomeHeaderDTO>(incomeHeaderVM);
        //    var createIncome = await _incomeHeaderViewManager.CreateAsync(incomeHeaderDTO);
        //    if (createIncome.Succeeded)
        //    {
        //        return createIncome.Data;
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}
        #endregion

        // Only for Superadmin user role (validate in view)
        [HttpPost]
        public async Task<JsonResult> OnPostDelete(int podId, int pohId)
        {
            var deleteCommand = await _purchaseOrderDetailViewManager.DeleteAsync(podId);
            if (deleteCommand.Succeeded)
            {
                var podByPOHId = await _purchaseOrderDetailViewManager.FindBySpecification(new PurchaseOrderDetailByPOIdSpecification(pohId));
                if (podByPOHId.Succeeded)
                {
                    if (podByPOHId.Data.Count == 0)
                    {
                        await _purchaseOrderHeaderViewManager.DeleteAsync(pohId);
                    }

                    _notify.Information(_localizer["Purchase order deleted."]);
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

        [HttpPost]
        public async Task<JsonResult> OnPostReturnPODItem(int podId)
        {
            try
            {
                await _purchaseOrderDetailViewManager.UpdateDataOnReturnItem(podId);
                _notify.Information(_localizer["Item returned."]);
                return new JsonResult(new { isValid = true });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { isValid = false, exception = ex });
            }
        }

        public async Task<decimal> getQuantityToOrderByProviderQuantity(int providerId, int missingProductId, decimal providerQuantity)
        {
            // Value by default
            decimal purchasedQuantity = 0;
            try
            {
                // Me traigo los datos de productos-proveedores por id de proveedor
                var getRPPByProviderId = await _relProviderProductViewManager.FindBySpecification(new RelProviderProductByProviderIdSpecification(providerId));
                if (getRPPByProviderId.Succeeded)
                {
                    foreach (var rpp in getRPPByProviderId.Data)
                    {
                        var getMissingProductById = await _missingProductViewManager.GetByIdAsync(missingProductId);
                        //var getProductById = await _productViewManager.FindBySpecification(new ProductByIdWithUnitMeasureSpecification(getMissingProductById.Data.ProductId));
                        //var productVM = _mapper.Map<ProductViewModel>(getProductById.Data.First());
                        //if (rpp.ProductId == productVM.Id)
                        if (rpp.ProductId == getMissingProductById.Data.ProductId)
                        {
                            //if (rpp.UnitMeasureId.HasValue && productVM.QuantityToOrderUnitMeasureId.HasValue)
                            if (rpp.UnitMeasureId.HasValue && getMissingProductById.Data.QuantityToOrderUnitMeasureId.HasValue)
                            {
                                // Si la unidad del proveedor es igual que la unidad de la cantidad a pedir del producto:
                                //if ((rpp.UnitMeasureId.Value == productVM.QuantityToOrderUnitMeasureId.Value) && (productVM.QuantityToOrder.HasValue && productVM.QuantityToOrderUnitMeasure != null && productVM.QuantityToOrderUnitMeasureId.HasValue))
                                if (rpp.UnitMeasureId.Value == getMissingProductById.Data.QuantityToOrderUnitMeasureId.Value)
                                {
                                    purchasedQuantity = providerQuantity;
                                }
                                else
                                {
                                    // Barras o Rollos
                                    if (rpp.UnitMeasureId.Value == 22 || rpp.UnitMeasureId.Value == 23)
                                    {
                                        // Se completa el campo Lenght
                                        if (rpp.Lenght.HasValue && rpp.LengthUnitMeasure != null)
                                        {
                                            purchasedQuantity = providerQuantity * rpp.Lenght.Value;
                                        }
                                    }
                                    // Cajas
                                    else if (rpp.UnitMeasureId.Value == 33)
                                    {
                                        // Se completa el campo lenght
                                        if (rpp.Lenght.HasValue && rpp.LengthUnitMeasure != null)
                                        {
                                            purchasedQuantity = providerQuantity * rpp.Lenght.Value;
                                        }
                                        // o el campo PresentationQuantity
                                        else if (rpp.PresentationQuantity.HasValue && rpp.PresentationUnitMeasure != null)
                                        {
                                            purchasedQuantity = providerQuantity * rpp.PresentationQuantity.Value;
                                        }
                                    }
                                    // Hojas
                                    else if (rpp.UnitMeasureId.Value == 31)
                                    {
                                        // Se completa el campo lenght y width
                                        if (rpp.Lenght.HasValue && rpp.LengthUnitMeasure != null && rpp.Width.HasValue && rpp.WidthUnitMeasure != null)
                                        {
                                            purchasedQuantity = providerQuantity * rpp.Lenght.Value * rpp.Width.Value;
                                        }
                                    }
                                    // Bidones
                                    else if (rpp.UnitMeasureId.Value == 32)
                                    {
                                        // Se completa el campo PresentationQuantity
                                        if (rpp.PresentationQuantity.HasValue && rpp.PresentationUnitMeasure != null)
                                        {
                                            purchasedQuantity = providerQuantity * rpp.PresentationQuantity.Value;
                                        }
                                    }
                                    // Latas
                                    else if (rpp.UnitMeasureId.Value == 37)
                                    {
                                        // Se completa el campo PresentationQuantity
                                        if (rpp.PresentationQuantity.HasValue && rpp.PresentationUnitMeasure != null)
                                        {
                                            purchasedQuantity = providerQuantity * rpp.PresentationQuantity.Value;
                                        }
                                    }
                                    // Tubos
                                    else if (rpp.UnitMeasureId.Value == 38)
                                    {
                                        // Se completa el campo PresentationQuantity
                                        if (rpp.PresentationQuantity.HasValue && rpp.PresentationUnitMeasure != null)
                                        {
                                            purchasedQuantity = providerQuantity * rpp.PresentationQuantity.Value;
                                        }
                                    }
                                    // Unidades
                                    else if (rpp.UnitMeasureId.Value == 38)
                                    {
                                        // Se completa el campo PresentationQuantity
                                        if (rpp.PresentationQuantity.HasValue && rpp.PresentationUnitMeasure != null)
                                        {
                                            purchasedQuantity = providerQuantity * rpp.PresentationQuantity.Value;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    return purchasedQuantity;
                }
                else
                {
                    return purchasedQuantity;
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return purchasedQuantity;
            }
        }

        public async Task<PurchaseOrderHeaderViewModel> LoadDataForRefreshFormModal(PurchaseOrderHeaderViewModel purchaseOrderHeaderViewModel)
        {
            // Tengo que setear los detalles de la QRR a la OC en PurchaseOrderDetails para recargar el formulario
            if (purchaseOrderHeaderViewModel.QuoteRequestResponseHeaderId.HasValue)
            {
                var getQuoteRequestResponseDetailsByHeaderId = await _quoteRequestResponseHeaderViewManager.FindBySpecification(new QuoteRequestResponseHeaderByQRHeaderIdSpecification(purchaseOrderHeaderViewModel.QuoteRequestResponseHeaderId.Value));
                if (getQuoteRequestResponseDetailsByHeaderId.Succeeded)
                {
                    var quoteRequestResponseHeadersVM = _mapper.Map<QuoteRequestResponseHeaderViewModel>(getQuoteRequestResponseDetailsByHeaderId.Data.FirstOrDefault());

                    foreach (var quoteRequestResponseDetail in quoteRequestResponseHeadersVM.QuoteRequestResponseDetails)
                    {
                        if (quoteRequestResponseDetail.MissingProductId.HasValue)
                        {
                            var purchaseOrderDetailVM = new PurchaseOrderDetailViewModel();

                            purchaseOrderDetailVM.MissingProduct = quoteRequestResponseDetail.MissingProduct;
                            purchaseOrderDetailVM.MissingProductId = quoteRequestResponseDetail.MissingProductId.Value;

                            purchaseOrderHeaderViewModel.PurchaseOrderDetails.Add(purchaseOrderDetailVM);
                        }
                    }
                    return purchaseOrderHeaderViewModel;
                }
                else
                {
                    return purchaseOrderHeaderViewModel;
                }
            }
            else
            {
                return purchaseOrderHeaderViewModel;
            }
        }

        [Authorize(Policy = Permissions.PurchaseOrder.View)]
        [HttpPost]
        public async Task<JsonResult> CreatePurchaseOrderFromMissingProducts(List<int> missingProductsIds)
        {
            var existsPurchasedMissingProduct = false;
            var purchaseOrderViewModel = new PurchaseOrderHeaderViewModel();
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

                    purchaseOrderViewModel.isGenerateMissingProducts = true;

                    // Tengo que setear los detalles de acuerdo a cada producto faltante en missingProductViewModels
                    foreach (var mp in missingProductViewModels)
                    {
                        var purchaseOrderDetailVM = new PurchaseOrderDetailViewModel();

                        purchaseOrderDetailVM.MissingProduct = mp;
                        purchaseOrderDetailVM.MissingProductId = mp.Id;
                        purchaseOrderViewModel.PurchaseOrderDetails.Add(purchaseOrderDetailVM);
                    }

                    if (existsPurchasedMissingProduct)
                    {
                        //_notify.Warning(_localizer["Attention, some products may not have been added because their status not is \"Pending\"."]);
                        // _notify.Warning(_localizer["Attention, some products may not have been added because their status is \"Purchased\"."]);
                        _notify.Warning(_localizer["Attention, make sure to select products where their status is \"Pending\", \"To quote\" or \"Quoted\"."]);
                        purchaseOrderViewModel.missingProductsIds = missingProductsIds;
                        return new JsonResult(new { isValid = false, html = await _viewRenderer.RenderViewToStringAsync("_Create", purchaseOrderViewModel) });
                    }
                    else
                    {
                        purchaseOrderViewModel.missingProductsIds = missingProductsIds;
                        return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_Create", purchaseOrderViewModel) });
                    }

                }
                else
                {
                    purchaseOrderViewModel = await LoadDataForRefreshFormModal(purchaseOrderViewModel);
                    var html = await _viewRenderer.RenderViewToStringAsync("_Create", purchaseOrderViewModel);
                    return new JsonResult(new { isValid = false, html = html });
                }
            }
            else
            {
                purchaseOrderViewModel = await LoadDataForRefreshFormModal(purchaseOrderViewModel);
                var html = await _viewRenderer.RenderViewToStringAsync("_Create", purchaseOrderViewModel);
                return new JsonResult(new { isValid = false, html = html });
            }
        }

        public async Task<JsonResult> getMissingProductsById(List<int> missingProductIds)
        {
            var missingProducts = new List<MissingProductViewModel>();
            try
            {
                foreach (var mpId in missingProductIds)
                {
                    var getMPById = await _missingProductViewManager.FindWithAllSpecification(new MissingProductByIdSpecification(mpId));
                    if (getMPById.Succeeded)
                    {
                        var mpVM = _mapper.Map<MissingProductViewModel>(getMPById.Data.First());
                        missingProducts.Add(mpVM);
                    }
                }

                return new JsonResult(new { isValid = true, missingProducts = missingProducts });
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return new JsonResult(new { isValid = false, missingProducts = missingProducts });
            }
        }

        public async Task<IActionResult> ExportToPdf(int pohId)
        {
            try
            {
                var response = await _purchaseOrderHeaderViewManager.FindBySpecification(new PurchaseOrderWithAllSpecifications(pohId));
                if (response.Succeeded)
                {
                    var purchaseOrderHeaderViewModel = _mapper.Map<PurchaseOrderHeaderViewModel>(response.Data.FirstOrDefault());

                    purchaseOrderHeaderViewModel.PurchaseOrderDetails = purchaseOrderHeaderViewModel.PurchaseOrderDetails.OrderBy(qrd => qrd.Id).ToList();

                    return new ViewAsPdf("_PurchaseOrderPdfReport", purchaseOrderHeaderViewModel)
                    {
                        PageSize = Rotativa.AspNetCore.Options.Size.A4,
                        PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                        PageMargins = new Rotativa.AspNetCore.Options.Margins(2, 4, 2, 4),
                        FileName = "Orden de compra_" + purchaseOrderHeaderViewModel.Id.ToString("D9") + "_" + purchaseOrderHeaderViewModel.Provider.getBussinessNameOrName.ToString() + ".pdf"
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

        public async Task<JsonResult> _LoadProvidersByMissingProductIdsSelect2(string search, string missingProductIds)
        {
            var MissingProductIds = missingProductIds.Replace('[', ' ').Replace(']', ' ').Replace('"', ' ').Trim().Split(',');
            List<Select2ViewModel> mapSelect2Data = new List<Select2ViewModel>();
            HashSet<ProviderViewModel> providersViewModel = new HashSet<ProviderViewModel>();
            foreach (var mpId in MissingProductIds)
            {
                var getMPById = await _missingProductViewManager.GetByIdAsync(Convert.ToInt32(mpId));

                var relProviderProductResponse = await _relProviderProductViewManager.FindBySpecification(new RelProviderProductGetProvidersByProductIdSpecification(getMPById.Data.ProductId));
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
            //providersViewModel = providersViewModel.GroupBy(pv => pv.Id).Where(grp => grp.Count() == missingProductIds.Count()).Select(grp => grp.First()).ToHashSet();
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


        public async Task<JsonResult> _LoadProviderById(int id)
        {
            List<Select2ViewModel> mapSelect2Data = new List<Select2ViewModel>();
            var providerResponse = await _providerViewManager.GetByIdAsync(id);
            if (providerResponse.Succeeded)
            {
                var providerVM = _mapper.Map<ProviderViewModel>(providerResponse.Data);
                mapSelect2Data.Add(new Select2ViewModel() { Id = providerVM.Id, Text = providerVM.getBussinessNameOrName });
                var selectList = new SelectList(mapSelect2Data, "Id", "Text", providerVM.Id);
                return new JsonResult(new { provider = mapSelect2Data });
            }
            else
            {
                return new JsonResult(new { provider = mapSelect2Data });
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
        public async Task<JsonResult> GetProductById(int id)
        {
            try
            {
                if (id != -1)
                {
                    var response = await _missingProductViewManager.FindWithAllSpecification(new MissingProductByProductIdSpecification(id));
                    if (response.Succeeded)
                    {
                        var missingProductVM = _mapper.Map<MissingProductViewModel>(response.Data.FirstOrDefault());
                        return new JsonResult(new { isValid = true, missingProduct = missingProductVM });
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
        public async Task<JsonResult> GetProductByselect2ProductsByProviderId(int id)
        {
            try
            {
                if (id != -1)
                {
                    var response = await _productViewManager.FindBySpecification(new ProductByIdWithUnitMeasureSpecification(id));
                    if (response.Succeeded)
                    {
                        var productVM = _mapper.Map<ProductViewModel>(response.Data.FirstOrDefault());
                        return new JsonResult(new { isValid = true, productVM = productVM });
                    }
                    else
                    {
                        return new JsonResult(new { isValid = false, productVM = "" });
                    }
                }
                else
                {
                    return new JsonResult(new { isValid = false, productVM = "" });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return new JsonResult(new { isValid = false, productVM = "" });
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
            else if (inputDetail == "twice_product_selected")
            {
                _notify.Warning(_localizer["You must select only one product."]);
                return new JsonResult(new { isValid = false });
            }
            return null;
        }

        public async Task<IActionResult> ExportToExcel(string sSearch_2, string sSearch_3, string sSearch_4, string sSearch_5, string sSearch_6,
       string sSearch_7, string sSearch_8, string sSearch_9, string sSearch_10, string sSearch_11, string sSearch_12, string sSearch_13, string sSearch_14,
       int colIndexOrder, string colOrderDirection, string dateFromFilter, string dateToFilter)
        {
            try
            {
                var response = await _purchaseOrderDetailViewManager.FindBySpecification(new PurchasesOrderDetailWithAllSpecification());
                if (response.Succeeded)
                {
                    var purchaseOrderDetailViewModels = _mapper.Map<List<PurchaseOrderDetailViewModel>>(response.Data);

                    // From date -> To date
                    purchaseOrderDetailViewModels = purchaseOrderDetailViewModels.Where(pod => pod.PurchaseOrderHeader.Date >= Convert.ToDateTime(dateFromFilter) && pod.PurchaseOrderHeader.Date <= Convert.ToDateTime(dateToFilter)).ToList();

                    if (!string.IsNullOrEmpty(sSearch_2))
                    {
                        purchaseOrderDetailViewModels = purchaseOrderDetailViewModels.Where(pod => pod.PurchaseOrderHeader.Id.ToString().Contains(sSearch_2)).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_3))
                    {
                        purchaseOrderDetailViewModels = purchaseOrderDetailViewModels.Where(pod => pod.PurchaseOrderHeader.Date.ToString("dd/MM/yyyy").Contains(sSearch_3)).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_4))
                    {
                        purchaseOrderDetailViewModels = purchaseOrderDetailViewModels.Where(pod => pod.MissingProduct != null ? pod.MissingProduct.Product.Code.ToString().ToLower().Contains(sSearch_4.ToLower()) : pod.Product.Code.ToString().ToLower().Contains(sSearch_4.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_5))
                    {
                        purchaseOrderDetailViewModels = purchaseOrderDetailViewModels.Where(pod => pod.MissingProduct != null ? pod.MissingProduct.Product.Description.ToString().ToLower().Contains(sSearch_5.ToLower()) : pod.Product.Description.ToString().ToLower().Contains(sSearch_4.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_6))
                    {
                        purchaseOrderDetailViewModels = purchaseOrderDetailViewModels.Where(pod => pod.PurchaseOrderHeader.Provider.getBussinessNameOrName.ToString().ToLower().Contains(sSearch_6.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_7))
                    {
                        purchaseOrderDetailViewModels = purchaseOrderDetailViewModels.Where(pod => pod.ProviderQuantity.ToString().ToLower().Contains(sSearch_7.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_8))
                    {
                        purchaseOrderDetailViewModels = purchaseOrderDetailViewModels.Where(pod => pod.ProviderUnitMeasure != null && pod.ProviderUnitMeasure.Description.ToString().ToLower().Contains(sSearch_8.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_9))
                    {
                        purchaseOrderDetailViewModels = purchaseOrderDetailViewModels.Where(pod => pod.PriceQuantity.ToString().ToLower().Contains(sSearch_9.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_10))
                    {
                        purchaseOrderDetailViewModels = purchaseOrderDetailViewModels.Where(pod => pod.PriceUnitMeasure != null && pod.PriceUnitMeasure.Description.ToString().ToLower().Contains(sSearch_10.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_11))
                    {
                        purchaseOrderDetailViewModels = purchaseOrderDetailViewModels.Where(pod => pod.MoneyType == 1 ? (pod.UnitPrice.ToString() + "ARS").ToString().ToLower().Contains(sSearch_11.ToLower()) : pod.MoneyType == 2 ? (pod.UnitPrice.ToString() + "USD").ToString().ToLower().Contains(sSearch_11.ToLower()) : pod.UnitPrice.ToString().ToLower().Contains(sSearch_11.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_12))
                    {
                        purchaseOrderDetailViewModels = purchaseOrderDetailViewModels.Where(pod => pod.Bonus.ToString().ToLower().Contains(sSearch_12.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_13))
                    {
                        purchaseOrderDetailViewModels = purchaseOrderDetailViewModels.Where(pod => pod.Total.ToString().ToLower().Contains(sSearch_13.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_14))
                    {
                        purchaseOrderDetailViewModels = purchaseOrderDetailViewModels.Where(pod => pod.PurchaseStateId == 8 ? _localizer["Yes"].ToString().ToLower().Contains(sSearch_14.ToString().ToLower()) : "".ToString().ToLower().Contains(sSearch_14.ToString().ToLower())).ToList();
                    }

                    if (colIndexOrder != 0 && colOrderDirection != "")
                    {
                        var sortColumnIndex = colIndexOrder;
                        var sortDirection = colOrderDirection;

                        if (sortColumnIndex == 2)
                        {
                            purchaseOrderDetailViewModels = sortDirection == "asc" ? purchaseOrderDetailViewModels.OrderBy(pod => pod.PurchaseOrderHeader.Id).ToList() : purchaseOrderDetailViewModels.OrderByDescending(pod => pod.PurchaseOrderHeader.Id).ToList();
                        }
                        else if (sortColumnIndex == 3)
                        {
                            purchaseOrderDetailViewModels = sortDirection == "asc" ? purchaseOrderDetailViewModels.OrderByDescending(pod => pod.PurchaseOrderHeader.Date).ToList() : purchaseOrderDetailViewModels.OrderByDescending(pod => pod.PurchaseOrderHeader.Date).ToList();
                        }
                        else if (sortColumnIndex == 4)
                        {
                            purchaseOrderDetailViewModels = sortDirection == "asc" ? purchaseOrderDetailViewModels.OrderBy(pod => pod.MissingProduct != null ? pod.MissingProduct.Product.Code.ToString() : pod.Product.Code.ToString()).ToList() : purchaseOrderDetailViewModels.OrderByDescending(pod => pod.MissingProduct != null ? pod.MissingProduct.Product.Code.ToString() : pod.Product.Code.ToString()).ToList();
                        }
                        else if (sortColumnIndex == 5)
                        {
                            purchaseOrderDetailViewModels = sortDirection == "asc" ? purchaseOrderDetailViewModels.OrderBy(pod => pod.MissingProduct != null ? pod.MissingProduct.Product.Description.ToString() : pod.Product.Description.ToString()).ToList() : purchaseOrderDetailViewModels.OrderByDescending(pod => pod.MissingProduct != null ? pod.MissingProduct.Product.Description.ToString() : pod.Product.Description.ToString()).ToList();
                        }
                        else if (sortColumnIndex == 6)
                        {
                            purchaseOrderDetailViewModels = sortDirection == "asc" ? purchaseOrderDetailViewModels.OrderBy(pod => pod.PurchaseOrderHeader.Provider.getBussinessNameOrName.ToString()).ToList() : purchaseOrderDetailViewModels.OrderByDescending(pod => pod.PurchaseOrderHeader.Provider.getBussinessNameOrName.ToString()).ToList();
                        }
                        else if (sortColumnIndex == 7)
                        {
                            purchaseOrderDetailViewModels = sortDirection == "asc" ? purchaseOrderDetailViewModels.OrderBy(pod => pod.ProviderQuantity).ToList() : purchaseOrderDetailViewModels.OrderByDescending(pod => pod.ProviderQuantity).ToList();
                        }
                        else if (sortColumnIndex == 8)
                        {
                            purchaseOrderDetailViewModels = sortDirection == "asc" ? purchaseOrderDetailViewModels.OrderBy(pod => pod.ProviderUnitMeasure != null ? pod.ProviderUnitMeasure.Description.ToString() : null).ToList() : purchaseOrderDetailViewModels.OrderByDescending(pod => pod.ProviderUnitMeasure != null ? pod.ProviderUnitMeasure.Description.ToString() : null).ToList();
                        }
                        else if (sortColumnIndex == 9)
                        {
                            purchaseOrderDetailViewModels = sortDirection == "asc" ? purchaseOrderDetailViewModels.OrderBy(pod => pod.PriceQuantity).ToList() : purchaseOrderDetailViewModels.OrderByDescending(pod => pod.PriceQuantity).ToList();
                        }
                        else if (sortColumnIndex == 10)
                        {
                            purchaseOrderDetailViewModels = sortDirection == "asc" ? purchaseOrderDetailViewModels.OrderBy(pod => pod.PriceUnitMeasure != null ? pod.PriceUnitMeasure.Description.ToString() : null).ToList() : purchaseOrderDetailViewModels.OrderByDescending(pod => pod.PriceUnitMeasure != null ? pod.PriceUnitMeasure.Description.ToString() : null).ToList();
                        }
                        else if (sortColumnIndex == 11)
                        {
                            purchaseOrderDetailViewModels = sortDirection == "asc" ? purchaseOrderDetailViewModels.OrderBy(pod => pod.UnitPrice).ToList() : purchaseOrderDetailViewModels.OrderByDescending(pod => pod.UnitPrice).ToList();
                        }
                        else if (sortColumnIndex == 12)
                        {
                            purchaseOrderDetailViewModels = sortDirection == "asc" ? purchaseOrderDetailViewModels.OrderBy(pod => pod.Bonus).ToList() : purchaseOrderDetailViewModels.OrderByDescending(pod => pod.Bonus).ToList();
                        }
                        else if (sortColumnIndex == 13)
                        {
                            purchaseOrderDetailViewModels = sortDirection == "asc" ? purchaseOrderDetailViewModels.OrderBy(pod => pod.Total).ToList() : purchaseOrderDetailViewModels.OrderByDescending(pod => pod.Total).ToList();
                        }
                        else if (sortColumnIndex == 14)
                        {
                            purchaseOrderDetailViewModels = sortDirection == "asc" ? purchaseOrderDetailViewModels.OrderBy(pod => pod.PurchaseStateId == 8 ? _localizer["Yes"] : "").ToList() : purchaseOrderDetailViewModels.OrderByDescending(pod => pod.PurchaseStateId == 8 ? _localizer["Yes"] : "").ToList();
                        }
                    }

                    return ExportViewModelToExcel(purchaseOrderDetailViewModels);
                }
                else
                {
                    _notify.Error(_localizer["Purchase orders data could not be loaded."]);
                    return null;
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return null;
            }
        }

        public IActionResult ExportViewModelToExcel(IEnumerable<PurchaseOrderDetailViewModel> viewModel)
        {
            try
            {
                var nameForExcelFile = "Ordenes de compras_";

                var stream = new MemoryStream();
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var xlPackage = new ExcelPackage(stream))
                {
                    var workSheet = xlPackage.Workbook.Worksheets.Add("Ordenes de compras");

                    workSheet.Cells["A1"].Value = _localizer["Number"];
                    workSheet.Cells["B1"].Value = _localizer["Date"];
                    workSheet.Cells["C1"].Value = _localizer["Code"];
                    workSheet.Cells["D1"].Value = _localizer["Description"];
                    workSheet.Cells["E1"].Value = _localizer["Provider"];
                    workSheet.Cells["F1"].Value = _localizer["Quantity"];
                    workSheet.Cells["G1"].Value = _localizer["Provider unit measure"];
                    workSheet.Cells["H1"].Value = _localizer["Quantity"];
                    workSheet.Cells["I1"].Value = _localizer["Price unit"];
                    workSheet.Cells["J1"].Value = _localizer["Unit price"];
                    workSheet.Cells["K1"].Value = "";
                    workSheet.Cells["L1"].Value = _localizer["Bonus"];
                    workSheet.Cells["M1"].Value = "";
                    workSheet.Cells["N1"].Value = _localizer["Total"];
                    workSheet.Cells["O1"].Value = _localizer["Returned"];

                    var row = 2;
                    foreach (var vm in viewModel)
                    {
                        workSheet.Cells[row, 1].Value = vm.PurchaseOrderHeader.Id;
                        workSheet.Cells[row, 1].Style.Numberformat.Format = "0";

                        workSheet.Cells[row, 2].Value = vm.PurchaseOrderHeader.Date;
                        workSheet.Cells[row, 2].Style.Numberformat.Format = "dd-MM-yyyy HH:mm";

                        if (vm.MissingProduct != null)
                        {
                            if (vm.MissingProduct.Product.Code != null)
                            {
                                workSheet.Cells[row, 3].Value = vm.MissingProduct.Product.Code.ToString();
                            }
                            else
                            {
                                workSheet.Cells[row, 3].Value = "";
                            }

                            if (vm.MissingProduct.Product.Description != null)
                            {
                                workSheet.Cells[row, 4].Value = vm.MissingProduct.Product.Description.ToString();
                            }
                            else
                            {
                                workSheet.Cells[row, 4].Value = "";
                            }
                        }
                        else
                        {
                            workSheet.Cells[row, 3].Value = "";
                            workSheet.Cells[row, 4].Value = "";
                        }

                        if (vm.PurchaseOrderHeader.Provider != null)
                        {
                            if (vm.PurchaseOrderHeader.Provider.getBussinessNameOrName != null)
                            {
                                workSheet.Cells[row, 5].Value = vm.PurchaseOrderHeader.Provider.getBussinessNameOrName.ToString();
                            }
                            else
                            {
                                workSheet.Cells[row, 5].Value = "";
                            }
                        }
                        else
                        {
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

                        if (vm.PurchaseStateId == 8)
                        {
                            workSheet.Cells[row, 15].Value = vm.PurchaseState.Name.ToString();
                        }
                        else
                        {
                            workSheet.Cells[row, 15].Value = "";
                        }

                        row++;
                    }

                    workSheet.Cells["A1:O1"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                    workSheet.Cells["O1"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

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
    }
}