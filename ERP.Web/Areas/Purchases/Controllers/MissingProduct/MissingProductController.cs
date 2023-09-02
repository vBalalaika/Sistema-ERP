using ERP.Application.Constants;
using ERP.Application.DTOs.Entities.Purchases.MissingProducts;
using ERP.Application.Interfaces.ViewManagers.Commons;
using ERP.Application.Interfaces.ViewManagers.ProductMod;
using ERP.Application.Interfaces.ViewManagers.Purchases.MissingProducts;
using ERP.Application.Specification.Commons.UnitMeasureSpecifications;
using ERP.Application.Specification.ProductMod;
using ERP.Application.Specification.Purchases.MissingProducts;
using ERP.Infrastructure.Identity.Models;
using ERP.Language;
using ERP.Web.Abstractions;
using ERP.Web.Areas.ProductMod.Models;
using ERP.Web.Areas.Purchases.Models.MissingProduct;
using ERP.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Web.Areas.Purchases.Controllers.MissingProduct
{
    [Area("Purchases")]
    public class MissingProductController : BaseController<MissingProductController>
    {
        private readonly IMissingProductViewManager _MissingProductViewManager;
        private readonly IStringLocalizer<SharedResource> _localizer;

        private readonly IProductViewManager _productViewManager;
        private readonly IUnitMeasureViewManager _unitMeasureViewManager;

        private readonly UserManager<ApplicationUser> _userManager;

        public MissingProductController(IMissingProductViewManager missingProductViewManager, IStringLocalizer<SharedResource> localizer, IProductViewManager productViewManager, IUnitMeasureViewManager unitMeasureViewManager, UserManager<ApplicationUser> userManager)
        {
            _MissingProductViewManager = missingProductViewManager;
            _localizer = localizer;
            _productViewManager = productViewManager;
            _unitMeasureViewManager = unitMeasureViewManager;
            _userManager = userManager;
        }

        public IActionResult _Index()
        {
            var model = new MissingProductViewModel();
            return View(model);
        }

        public async Task<IActionResult> _LoadAll(DatatableParamsViewModel dataTableParams,
        string sSearch_3, string sSearch_4, string sSearch_5, string sSearch_6, string sSearch_7, string sSearch_8, string sSearch_9,
        string sSearch_10, string sSearch_11, string sSearch_12, string sSearch_13, string sSearch_14, string sSearch_15, DateTime dateFromFilter, DateTime dateToFilter)
        {
            try
            {
                var response = await _MissingProductViewManager.FindWithAllSpecification(new MissingProductWithAllSpecification());
                if (response.Succeeded)
                {
                    var missingProductViewModels = _mapper.Map<List<MissingProductViewModel>>(response.Data);

                    // From date -> To date
                    missingProductViewModels = missingProductViewModels.Where(mp => mp.CreateDate.Date >= dateFromFilter && mp.CreateDate.Date <= dateToFilter).ToList();

                    if (!string.IsNullOrEmpty(sSearch_3))
                    {
                        missingProductViewModels = missingProductViewModels.Where(mp => mp.CreateDate.ToString("dd/MM/yyyy").Contains(sSearch_3)).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_4))
                    {
                        missingProductViewModels = missingProductViewModels.Where(mp => mp.Product != null && mp.Product.SubCategory.Description.ToString().Contains(sSearch_4)).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_5))
                    {
                        missingProductViewModels = missingProductViewModels.Where(mp => mp.Product != null && mp.Product.Code.ToString().ToLower().Contains(sSearch_5.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_6))
                    {
                        missingProductViewModels = missingProductViewModels.Where(mp => mp.Product != null && mp.Product.Description.ToString().ToLower().Contains(sSearch_6.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_7))
                    {
                        missingProductViewModels = missingProductViewModels.Where(mp => mp.QuantityToOrder.ToString().Contains(sSearch_7.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_8))
                    {
                        missingProductViewModels = missingProductViewModels.Where(mp => mp.QuantityToOrderUnitMeasure != null && mp.QuantityToOrderUnitMeasure.Description.ToString().ToLower().Contains(sSearch_8.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_9))
                    {
                        missingProductViewModels = missingProductViewModels.Where(mp => mp.Quantity.ToString().Contains(sSearch_9.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_10))
                    {
                        missingProductViewModels = missingProductViewModels.Where(mp => mp.StorageUnitMeasure != null && mp.StorageUnitMeasure.Description.ToString().ToLower().Contains(sSearch_10.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_11))
                    {
                        missingProductViewModels = missingProductViewModels.Where(mp => mp.PurchasedQuantity.ToString().Contains(sSearch_11.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_12))
                    {
                        missingProductViewModels = missingProductViewModels.Where(mp => mp.StateMissingProduct != null && mp.StateMissingProduct.Name.ToString().ToLower().Contains(sSearch_12.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_13))
                    {
                        missingProductViewModels = missingProductViewModels.Where(mp => mp.Provider != null && mp.Provider.getBussinessNameOrName.ToString().ToLower().Contains(sSearch_13.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_14))
                    {
                        missingProductViewModels = missingProductViewModels.Where(mp => mp.Product != null && mp.Product.LocationStation != null && mp.Product.LocationStation.Abbreviation.ToString().ToLower().Contains(sSearch_14.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_15))
                    {
                        missingProductViewModels = missingProductViewModels.Where(mp => mp.UserName.ToString().ToLower().Contains(sSearch_15.ToLower())).ToList();
                    }

                    var sortColumnIndex = dataTableParams.iSortCol_0;
                    var sortDirection = dataTableParams.sSortDir_0;

                    if (sortColumnIndex == 3)
                    {
                        missingProductViewModels = sortDirection == "asc" ? missingProductViewModels.OrderBy(mp => mp.CreateDate).ToList() : missingProductViewModels.OrderByDescending(mp => mp.CreateDate).ToList();
                    }
                    else if (sortColumnIndex == 4)
                    {
                        missingProductViewModels = sortDirection == "asc" ? missingProductViewModels.OrderBy(mp => mp.Product != null ? mp.Product.SubCategory.Description : null).ToList() : missingProductViewModels.OrderByDescending(mp => mp.Product != null ? mp.Product.SubCategory.Description : null).ToList();
                    }
                    else if (sortColumnIndex == 5)
                    {
                        missingProductViewModels = sortDirection == "asc" ? missingProductViewModels.OrderBy(mp => mp.Product != null ? mp.Product.Code.ToString() : null).ToList() : missingProductViewModels.OrderByDescending(mp => mp.Product != null ? mp.Product.Code.ToString() : null).ToList();
                    }
                    else if (sortColumnIndex == 6)
                    {
                        missingProductViewModels = sortDirection == "asc" ? missingProductViewModels.OrderBy(mp => mp.Product != null ? mp.Product.Description.ToString() : null).ToList() : missingProductViewModels.OrderByDescending(mp => mp.Product != null ? mp.Product.Description.ToString() : null).ToList();
                    }
                    else if (sortColumnIndex == 7)
                    {
                        missingProductViewModels = sortDirection == "asc" ? missingProductViewModels.OrderBy(mp => mp.QuantityToOrder).ToList() : missingProductViewModels.OrderByDescending(mp => mp.QuantityToOrder).ToList();
                    }
                    else if (sortColumnIndex == 8)
                    {
                        missingProductViewModels = sortDirection == "asc" ? missingProductViewModels.OrderBy(mp => mp.QuantityToOrderUnitMeasure != null ? mp.QuantityToOrderUnitMeasure.Description.ToString() : null).ToList() : missingProductViewModels.OrderByDescending(mp => mp.QuantityToOrderUnitMeasure != null ? mp.QuantityToOrderUnitMeasure.Description.ToString() : null).ToList();
                    }
                    else if (sortColumnIndex == 9)
                    {
                        missingProductViewModels = sortDirection == "asc" ? missingProductViewModels.OrderBy(mp => mp.Quantity).ToList() : missingProductViewModels.OrderByDescending(mp => mp.Quantity).ToList();
                    }
                    else if (sortColumnIndex == 10)
                    {
                        missingProductViewModels = sortDirection == "asc" ? missingProductViewModels.OrderBy(mp => mp.StorageUnitMeasure != null ? mp.StorageUnitMeasure.Description.ToString() : null).ToList() : missingProductViewModels.OrderByDescending(mp => mp.StorageUnitMeasure != null ? mp.StorageUnitMeasure.Description.ToString() : null).ToList();
                    }
                    else if (sortColumnIndex == 11)
                    {
                        missingProductViewModels = sortDirection == "asc" ? missingProductViewModels.OrderBy(mp => mp.PurchasedQuantity).ToList() : missingProductViewModels.OrderByDescending(mp => mp.PurchasedQuantity).ToList();
                    }
                    else if (sortColumnIndex == 12)
                    {
                        missingProductViewModels = sortDirection == "asc" ? missingProductViewModels.OrderBy(mp => mp.StateMissingProduct != null ? mp.StateMissingProduct.Name.ToString() : null).ToList() : missingProductViewModels.OrderByDescending(mp => mp.StateMissingProduct != null ? mp.StateMissingProduct.Name.ToString() : null).ToList();
                    }
                    else if (sortColumnIndex == 13)
                    {
                        missingProductViewModels = sortDirection == "asc" ? missingProductViewModels.OrderBy(mp => mp.Provider != null ? mp.Provider.getBussinessNameOrName.ToString() : null).ToList() : missingProductViewModels.OrderByDescending(mp => mp.Provider != null ? mp.Provider.getBussinessNameOrName.ToString() : null).ToList();
                    }
                    else if (sortColumnIndex == 14)
                    {
                        missingProductViewModels = sortDirection == "asc" ? missingProductViewModels.OrderBy(mp => mp.Product != null ? mp.Product.LocationStation != null ? mp.Product.LocationStation.Abbreviation : null : null).ToList() : missingProductViewModels.OrderByDescending(mp => mp.Product != null ? mp.Product.LocationStation != null ? mp.Product.LocationStation.Abbreviation : null : null).ToList();
                    }
                    else if (sortColumnIndex == 15)
                    {
                        missingProductViewModels = sortDirection == "asc" ? missingProductViewModels.OrderBy(mp => mp.UserName.ToString()).ToList() : missingProductViewModels.OrderByDescending(mp => mp.UserName.ToString()).ToList();
                    }

                    var displayResult = missingProductViewModels.Skip(dataTableParams.iDisplayStart)
                        .Take(dataTableParams.iDisplayLength).ToList();

                    var totalRecords = missingProductViewModels.Count();

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
                    _notify.Error(_localizer["Missing product data could not be loaded."]);
                    return null;
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return null;
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetProductById(int id, bool btnAdd)
        {
            try
            {
                if (id != -1)
                {
                    var response = await _productViewManager.FindBySpecification(new ProductByIdWithUnitMeasureSpecification(id));
                    if (response.Succeeded)
                    {
                        var productVM = _mapper.Map<ProductViewModel>(response.Data.FirstOrDefault());

                        if (btnAdd)
                        {
                            // Tengo que validar que el producto que se va a agregar como faltante no se haya agregado antes con estado "Pendiente", "A cotizar" o "Cotizado"
                            // Si existe, tengo que notificar al usuario.
                            // Si no existe, lo agrego a la grilla.
                            var getMissingProductByProductId = await _MissingProductViewManager.FindWithAllSpecification(new MissingProductByProductIdAndStateSpecification(productVM.Id));
                            if (getMissingProductByProductId.Succeeded)
                            {
                                if (getMissingProductByProductId.Data.Count > 0)
                                {
                                    // No puedo agregar el faltante
                                    _notify.Warning(_localizer["No se puede agregar el producto faltante, ya existe con estado \"Pendiente\" o \"A cotizar\" o \"Cotizado\"."]);
                                    return new JsonResult(new { isValid = true, product = productVM, canBeCreated = false });
                                }
                                else
                                {
                                    return new JsonResult(new { isValid = true, product = productVM, canBeCreated = true });
                                }
                            }
                            else
                            {
                                return new JsonResult(new { isValid = true, product = productVM, canBeCreated = false });
                            }
                        }
                        else
                        {
                            return new JsonResult(new { isValid = true, product = productVM });
                        }
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

        [Authorize(Policy = Permissions.MissingProducts.View)]
        public async Task<JsonResult> OnGetCreate(int id = 0)
        {
            if (id == 0)
            {
                // Create new missing product
                var missingProductViewModel = new MissingProductViewModel();

                var unitMeasuresResponse = await _unitMeasureViewManager.FindBySpecification(new UnitMeasureIsActiveSpecification());
                if (unitMeasuresResponse.Succeeded)
                {
                    var unitMeasuresVM = _mapper.Map<List<UnitMeasureViewModel>>(unitMeasuresResponse.Data);
                    missingProductViewModel.UnitMeasures = new SelectList(unitMeasuresVM, nameof(UnitMeasureViewModel.Id), nameof(UnitMeasureViewModel.Description), null, null);
                }

                missingProductViewModel.StateMissingProductId = 1;

                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_Create", missingProductViewModel) });

            }
            return null;
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreate(int id, MissingProductViewModel missingProductViewModel)
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                missingProductViewModel.UserName = currentUser.LastName + ", " + currentUser.FirstName;

                var MissingProductDTO = _mapper.Map<MissingProductDTO>(missingProductViewModel);
                var result = await _MissingProductViewManager.CreateAsync(MissingProductDTO);

                if (result.Succeeded)
                {
                    return new JsonResult(new { isValid = true });
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                var html = await _viewRenderer.RenderViewToStringAsync("_Create", missingProductViewModel);
                return new JsonResult(new { isValid = false, html = html });
            }
        }

        [Authorize(Policy = Permissions.MissingProducts.View)]
        public async Task<JsonResult> OnGetEdit(int id = 0)
        {
            if (id != 0)
            {
                // Update missing product
                var response = await _MissingProductViewManager.FindWithAllSpecification(new MissingProductByIdSpecification(id));
                if (response.Succeeded)
                {
                    var missingProductViewModel = _mapper.Map<MissingProductViewModel>(response.Data.First());
                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_Edit", missingProductViewModel) });
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

        [HttpPost]
        public async Task<JsonResult> OnPostEdit(int id, MissingProductViewModel missingProduct)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (id != 0)
                    {
                        var missingProductDTO = _mapper.Map<MissingProductDTO>(missingProduct);
                        var result = await _MissingProductViewManager.UpdateAsync(missingProductDTO);

                        if (result.Succeeded)
                        {
                            _notify.Information(_localizer["Missing product update."]);
                        }

                        //Result<int> result = null;
                        //// Update
                        //if (missingProduct.idsForUpdateMassiveComments != null)
                        //{
                        //    foreach (var mpId in missingProduct.idsForUpdateMassiveComments.Split(','))
                        //    {
                        //        var res = await _MissingProductViewManager.FindWithAllSpecification(new MissingProductByIdSpecification(Convert.ToInt32(mpId)));
                        //        if (res.Succeeded)
                        //        {
                        //            var missingProductViewModel = _mapper.Map<MissingProductViewModel>(res.Data.First());
                        //            missingProductViewModel.Comments = missingProduct.Comments;
                        //            var missingProductDTO = _mapper.Map<MissingProductDTO>(missingProductViewModel);
                        //            result = await _MissingProductViewManager.UpdateAsync(missingProductDTO);
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        //    var missingProductDTO = _mapper.Map<MissingProductDTO>(missingProduct);
                        //    result = await _MissingProductViewManager.UpdateAsync(missingProductDTO);
                        //}

                        //if (result.Succeeded)
                        //{
                        //    _notify.Information(_localizer["Missing product update."]);
                        //}
                    }

                    var response = await _MissingProductViewManager.FindWithAllSpecification(new MissingProductWithAllSpecification());
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
                    var html = await _viewRenderer.RenderViewToStringAsync("_Edit", missingProduct);
                    return new JsonResult(new { isValid = false, html = html });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                var html = await _viewRenderer.RenderViewToStringAsync("_Edit", missingProduct);
                return new JsonResult(new { isValid = false, html = html });
            }
        }

        // Solo se usa para editar comentarios masivos
        [Authorize(Policy = Permissions.MissingProducts.View)]
        public async Task<JsonResult> OnGetEditMultiple(string ids)
        {
            var idsMP = ids.Split(',');
            var missingProductViewModel = new MissingProductViewModel();
            if (idsMP != null)
            {
                if (Convert.ToInt32(idsMP.First()) != 0)
                {
                    var response = await _MissingProductViewManager.FindWithAllSpecification(new MissingProductByIdSpecification(Convert.ToInt32(idsMP.First())));
                    if (response.Succeeded)
                    {
                        missingProductViewModel = _mapper.Map<MissingProductViewModel>(response.Data.First());
                    }
                }

                missingProductViewModel.idsForUpdateMassiveComments = ids;
                missingProductViewModel.Comments = "";
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_EditOld", missingProductViewModel) });
            }
            else
            {
                return new JsonResult(new { isValid = false, html = await _viewRenderer.RenderViewToStringAsync("_EditOld", missingProductViewModel) });
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostDelete(int id)
        {
            try
            {
                var deleteCommand = await _MissingProductViewManager.DeleteAsync(id);
                if (deleteCommand.Succeeded)
                {
                    _notify.Information(_localizer["Missing product deleted."]);
                    return new JsonResult(new { isValid = true });
                }
                else
                {
                    _notify.Error(deleteCommand.Message);
                    return new JsonResult(new { isValid = false });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return new JsonResult(new { isValid = false });
            }
        }

        public async Task<JsonResult> LoadProductsSelect2(string search)
        {
            var productsResponse = await _productViewManager.FindBySpecification(new ProductWithAllSpecification(search));
            if (productsResponse.Succeeded)
            {
                List<Select2ViewModel> mapSelect2Data = new List<Select2ViewModel>();
                var productsViewModel = _mapper.Map<List<ProductViewModel>>(productsResponse.Data.Where(pr => pr.SubCategory.CategoryId != 1).ToList());

                foreach (var product in productsViewModel)
                {
                    mapSelect2Data.Add(new Select2ViewModel() { Id = product.Id, Text = product.CodeAndDescription });
                }

                if (!(string.IsNullOrEmpty(search) || string.IsNullOrWhiteSpace(search)))
                {
                    mapSelect2Data = mapSelect2Data.Where(x => x.Text.ToLower().Contains(search.ToLower())).ToList();
                }
                return new JsonResult(new { products = mapSelect2Data });
            }
            else
            {
                return null;
            }
        }

        [HttpGet]
        public JsonResult ShowAlerts(string inputDetail)
        {
            if (inputDetail == "quantity")
            {
                _notify.Warning(_localizer["Error on quantity input."]);
                return new JsonResult(new { isValid = false });
            }
            else if (inputDetail == "quantity_empty")
            {
                _notify.Warning(_localizer["Quantity input cannot be empty."]);
                return new JsonResult(new { isValid = false });
            }
            else if (inputDetail == "quantity_zero")
            {
                _notify.Warning(_localizer["The quantity cannot be zero."]);
                return new JsonResult(new { isValid = false });
            }
            else if (inputDetail == "product_notselected")
            {
                _notify.Warning(_localizer["Please, you must select a product."]);
                return new JsonResult(new { isValid = false });
            }
            return null;
        }

        public async Task<IActionResult> ExportToExcel(string sSearch_3, string sSearch_4, string sSearch_5, string sSearch_6,
            string sSearch_7, string sSearch_8, string sSearch_9, string sSearch_10, string sSearch_11, string sSearch_12, string sSearch_13, string sSearch_14, string sSearch_15,
            int colIndexOrder, string colOrderDirection, string dateFromFilterValue, string dateToFilterValue)
        {
            try
            {
                var response = await _MissingProductViewManager.FindWithAllSpecification(new MissingProductWithAllSpecification());
                if (response.Succeeded)
                {
                    var missingProductViewModels = _mapper.Map<List<MissingProductViewModel>>(response.Data);

                    // From date -> To date
                    missingProductViewModels = missingProductViewModels.Where(mp => mp.CreateDate >= Convert.ToDateTime(dateFromFilterValue) && mp.CreateDate <= Convert.ToDateTime(dateToFilterValue)).ToList();

                    if (!string.IsNullOrEmpty(sSearch_3))
                    {
                        missingProductViewModels = missingProductViewModels.Where(mp => mp.CreateDate.ToString("dd/MM/yyyy").Contains(sSearch_3)).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_4))
                    {
                        missingProductViewModels = missingProductViewModels.Where(mp => mp.Product != null && mp.Product.SubCategory.Description.ToString().Contains(sSearch_4)).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_5))
                    {
                        missingProductViewModels = missingProductViewModels.Where(mp => mp.Product != null && mp.Product.Code.ToString().ToLower().Contains(sSearch_5.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_6))
                    {
                        missingProductViewModels = missingProductViewModels.Where(mp => mp.Product != null && mp.Product.Description.ToString().ToLower().Contains(sSearch_6.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_7))
                    {
                        missingProductViewModels = missingProductViewModels.Where(mp => mp.QuantityToOrder.ToString().Contains(sSearch_7.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_8))
                    {
                        missingProductViewModels = missingProductViewModels.Where(mp => mp.QuantityToOrderUnitMeasure != null && mp.QuantityToOrderUnitMeasure.Description.ToString().ToLower().Contains(sSearch_8.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_9))
                    {
                        missingProductViewModels = missingProductViewModels.Where(mp => mp.Quantity.ToString().Contains(sSearch_9.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_10))
                    {
                        missingProductViewModels = missingProductViewModels.Where(mp => mp.StorageUnitMeasure != null && mp.StorageUnitMeasure.Description.ToString().ToLower().Contains(sSearch_10.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_11))
                    {
                        missingProductViewModels = missingProductViewModels.Where(mp => mp.PurchasedQuantity.ToString().Contains(sSearch_11.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_12))
                    {
                        missingProductViewModels = missingProductViewModels.Where(mp => mp.StateMissingProduct != null && mp.StateMissingProduct.Name.ToString().ToLower().Contains(sSearch_12.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_13))
                    {
                        missingProductViewModels = missingProductViewModels.Where(mp => mp.Provider != null && mp.Provider.getBussinessNameOrName.ToString().ToLower().Contains(sSearch_13.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_14))
                    {
                        missingProductViewModels = missingProductViewModels.Where(mp => mp.Product != null && mp.Product.LocationStation != null && mp.Product.LocationStation.Abbreviation.ToString().ToLower().Contains(sSearch_14.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_15))
                    {
                        missingProductViewModels = missingProductViewModels.Where(mp => mp.UserName.ToString().ToLower().Contains(sSearch_15.ToLower())).ToList();
                    }

                    if (colIndexOrder != 0 && colOrderDirection != "")
                    {
                        var sortColumnIndex = colIndexOrder;
                        var sortDirection = colOrderDirection;

                        if (sortColumnIndex == 3)
                        {
                            missingProductViewModels = sortDirection == "asc" ? missingProductViewModels.OrderBy(mp => mp.CreateDate).ToList() : missingProductViewModels.OrderByDescending(mp => mp.CreateDate).ToList();
                        }
                        else if (sortColumnIndex == 4)
                        {
                            missingProductViewModels = sortDirection == "asc" ? missingProductViewModels.OrderByDescending(mp => mp.Product != null ? mp.Product.SubCategory.Description : null).ToList() : missingProductViewModels.OrderByDescending(mp => mp.Product != null ? mp.Product.SubCategory.Description : null).ToList();
                        }
                        else if (sortColumnIndex == 5)
                        {
                            missingProductViewModels = sortDirection == "asc" ? missingProductViewModels.OrderBy(mp => mp.Product != null ? mp.Product.Code.ToString() : null).ToList() : missingProductViewModels.OrderByDescending(mp => mp.Product != null ? mp.Product.Code.ToString() : null).ToList();
                        }
                        else if (sortColumnIndex == 6)
                        {
                            missingProductViewModels = sortDirection == "asc" ? missingProductViewModels.OrderBy(mp => mp.Product != null ? mp.Product.Description.ToString() : null).ToList() : missingProductViewModels.OrderByDescending(mp => mp.Product != null ? mp.Product.Description.ToString() : null).ToList();
                        }
                        else if (sortColumnIndex == 7)
                        {
                            missingProductViewModels = sortDirection == "asc" ? missingProductViewModels.OrderBy(mp => mp.QuantityToOrder).ToList() : missingProductViewModels.OrderByDescending(mp => mp.QuantityToOrder).ToList();
                        }
                        else if (sortColumnIndex == 8)
                        {
                            missingProductViewModels = sortDirection == "asc" ? missingProductViewModels.OrderBy(mp => mp.QuantityToOrderUnitMeasure != null ? mp.QuantityToOrderUnitMeasure.Description.ToString() : null).ToList() : missingProductViewModels.OrderByDescending(mp => mp.QuantityToOrderUnitMeasure != null ? mp.QuantityToOrderUnitMeasure.Description.ToString() : null).ToList();
                        }
                        else if (sortColumnIndex == 9)
                        {
                            missingProductViewModels = sortDirection == "asc" ? missingProductViewModels.OrderBy(mp => mp.Quantity).ToList() : missingProductViewModels.OrderByDescending(mp => mp.Quantity).ToList();
                        }
                        else if (sortColumnIndex == 10)
                        {
                            missingProductViewModels = sortDirection == "asc" ? missingProductViewModels.OrderBy(mp => mp.StorageUnitMeasure != null ? mp.StorageUnitMeasure.Description.ToString() : null).ToList() : missingProductViewModels.OrderByDescending(mp => mp.StorageUnitMeasure != null ? mp.StorageUnitMeasure.Description.ToString() : null).ToList();
                        }
                        else if (sortColumnIndex == 11)
                        {
                            missingProductViewModels = sortDirection == "asc" ? missingProductViewModels.OrderBy(mp => mp.PurchasedQuantity).ToList() : missingProductViewModels.OrderByDescending(mp => mp.PurchasedQuantity).ToList();
                        }
                        else if (sortColumnIndex == 12)
                        {
                            missingProductViewModels = sortDirection == "asc" ? missingProductViewModels.OrderBy(mp => mp.StateMissingProduct != null ? mp.StateMissingProduct.Name.ToString() : null).ToList() : missingProductViewModels.OrderByDescending(mp => mp.StateMissingProduct != null ? mp.StateMissingProduct.Name.ToString() : null).ToList();
                        }
                        else if (sortColumnIndex == 13)
                        {
                            missingProductViewModels = sortDirection == "asc" ? missingProductViewModels.OrderBy(mp => mp.Provider != null ? mp.Provider.getBussinessNameOrName.ToString() : null).ToList() : missingProductViewModels.OrderByDescending(mp => mp.Provider != null ? mp.Provider.getBussinessNameOrName.ToString() : null).ToList();
                        }
                        else if (sortColumnIndex == 14)
                        {
                            missingProductViewModels = sortDirection == "asc" ? missingProductViewModels.OrderByDescending(mp => mp.Product != null ? mp.Product.LocationStation != null ? mp.Product.LocationStation.Abbreviation : null : null).ToList() : missingProductViewModels.OrderByDescending(mp => mp.Product != null ? mp.Product.LocationStation != null ? mp.Product.LocationStation.Abbreviation : null : null).ToList();
                        }
                        else if (sortColumnIndex == 15)
                        {
                            missingProductViewModels = sortDirection == "asc" ? missingProductViewModels.OrderBy(mp => mp.UserName.ToString()).ToList() : missingProductViewModels.OrderByDescending(mp => mp.UserName.ToString()).ToList();
                        }
                    }

                    return ExportViewModelToExcel(missingProductViewModels);
                }
                else
                {
                    _notify.Error(_localizer["Missing product data could not be loaded."]);
                    return null;
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return null;
            }
        }

        public IActionResult ExportViewModelToExcel(IEnumerable<MissingProductViewModel> viewModel)
        {
            try
            {
                var nameForExcelFile = "Productos faltantes_";

                var stream = new MemoryStream();
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var xlPackage = new ExcelPackage(stream))
                {
                    var workSheet = xlPackage.Workbook.Worksheets.Add("Productos faltantes");

                    workSheet.Cells["A1"].Value = _localizer["Date"];
                    workSheet.Cells["B1"].Value = _localizer["Heading"];
                    workSheet.Cells["C1"].Value = _localizer["Code"];
                    workSheet.Cells["D1"].Value = _localizer["Product"];
                    workSheet.Cells["E1"].Value = _localizer["Quantity to order"];
                    workSheet.Cells["F1"].Value = _localizer["Unit"];
                    workSheet.Cells["G1"].Value = _localizer["Quantity"];
                    workSheet.Cells["H1"].Value = _localizer["Storage unit measure"];
                    workSheet.Cells["I1"].Value = _localizer["Purchased quantity"];
                    workSheet.Cells["J1"].Value = _localizer["State"];
                    workSheet.Cells["K1"].Value = _localizer["Provider"];
                    workSheet.Cells["L1"].Value = _localizer["Location"];
                    workSheet.Cells["M1"].Value = _localizer["User"];

                    var row = 2;
                    foreach (var vm in viewModel)
                    {
                        workSheet.Cells[row, 1].Value = vm.CreateDate;
                        workSheet.Cells[row, 1].Style.Numberformat.Format = "dd-MM-yyyy HH:mm";

                        if (vm.Product != null)
                        {
                            if (vm.Product.SubCategory != null)
                            {
                                if (vm.Product.SubCategory.Description != null)
                                {
                                    workSheet.Cells[row, 2].Value = vm.Product.SubCategory.Description.ToString();
                                }
                                else
                                {
                                    workSheet.Cells[row, 2].Value = "";
                                }
                            }
                            else
                            {
                                workSheet.Cells[row, 2].Value = "";
                            }

                            if (vm.Product.Code != null)
                            {
                                workSheet.Cells[row, 3].Value = vm.Product.Code.ToString();
                            }
                            else
                            {
                                workSheet.Cells[row, 3].Value = "";
                            }

                            if (vm.Product.Description != null)
                            {
                                workSheet.Cells[row, 4].Value = vm.Product.Description.ToString();
                            }
                            else
                            {
                                workSheet.Cells[row, 4].Value = "";
                            }
                        }
                        else
                        {
                            workSheet.Cells[row, 2].Value = "";
                            workSheet.Cells[row, 3].Value = "";
                            workSheet.Cells[row, 4].Value = "";
                        }

                        workSheet.Cells[row, 5].Value = vm.QuantityToOrder;
                        workSheet.Cells[row, 5].Style.Numberformat.Format = "0.00";

                        if (vm.QuantityToOrderUnitMeasure != null)
                        {
                            if (vm.QuantityToOrderUnitMeasure.Description != null)
                            {
                                workSheet.Cells[row, 6].Value = vm.QuantityToOrderUnitMeasure.Description.ToString();
                            }
                            else
                            {
                                workSheet.Cells[row, 6].Value = "";
                            }
                        }
                        else
                        {
                            workSheet.Cells[row, 6].Value = "";
                        }

                        workSheet.Cells[row, 7].Value = vm.Quantity;
                        workSheet.Cells[row, 7].Style.Numberformat.Format = "0.00";

                        if (vm.StorageUnitMeasure != null)
                        {
                            if (vm.StorageUnitMeasure.Description != null)
                            {
                                workSheet.Cells[row, 8].Value = vm.StorageUnitMeasure.Description.ToString();
                            }
                            else
                            {
                                workSheet.Cells[row, 8].Value = "";
                            }
                        }
                        else
                        {
                            workSheet.Cells[row, 8].Value = "";
                        }

                        workSheet.Cells[row, 9].Value = vm.PurchasedQuantity;
                        workSheet.Cells[row, 9].Style.Numberformat.Format = "0.00";

                        if (vm.StateMissingProduct != null)
                        {
                            workSheet.Cells[row, 10].Value = vm.StateMissingProduct.Name.ToString();
                        }
                        else
                        {
                            workSheet.Cells[row, 10].Value = "";
                        }

                        if (vm.Provider != null)
                        {
                            if (vm.Provider.getBussinessNameOrName != null)
                            {
                                workSheet.Cells[row, 11].Value = vm.Provider.getBussinessNameOrName.ToString();
                            }
                            else
                            {
                                workSheet.Cells[row, 11].Value = "";
                            }
                        }
                        else
                        {
                            workSheet.Cells[row, 11].Value = "";
                        }

                        if (vm.Product.LocationStation != null)
                        {
                            if (vm.Product.LocationStation.Abbreviation != null)
                            {
                                workSheet.Cells[row, 12].Value = vm.Product.LocationStation.Abbreviation.ToString();
                            }
                            else
                            {
                                workSheet.Cells[row, 12].Value = "";
                            }
                        }
                        else
                        {
                            workSheet.Cells[row, 12].Value = "";
                        }

                        if (vm.UserName != null)
                        {
                            workSheet.Cells[row, 13].Value = vm.UserName.ToString();
                        }
                        else
                        {
                            workSheet.Cells[row, 13].Value = "";
                        }

                        row++;
                    }

                    workSheet.Cells["A1:M1"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                    workSheet.Cells["M1"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

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
