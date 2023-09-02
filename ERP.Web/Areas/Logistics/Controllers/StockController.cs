using AspNetCoreHero.Results;
using ERP.Application.DTOs.Entities.Commons;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Application.Interfaces.ViewManagers.Commons;
using ERP.Application.Interfaces.ViewManagers.ProductMod;
using ERP.Application.Specification.Commons.UnitMeasureSpecifications;
using ERP.Application.Specification.ProductMod;
using ERP.Language;
using ERP.Web.Abstractions;
using ERP.Web.Areas.ProductMod.Models;
using ERP.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Web.Areas.Logistics.Controllers
{
    [Area("Logistics")]
    public class StockController : BaseController<StockController>
    {
        private readonly IStringLocalizer<SharedResource> _localizer;

        private readonly IProductViewManager _productViewManager;
        private readonly IStructureViewManager _structureViewManager;
        private readonly IUnitMeasureViewManager _unitMeasureViewManager;
        private readonly IStationViewManager _stationViewManager;

        public StockController(IStringLocalizer<SharedResource> localizer, IProductViewManager productViewManager, IStructureViewManager structureViewManager, IUnitMeasureViewManager unitMeasureViewManager, IStationViewManager stationViewManager)
        {
            _localizer = localizer;
            _productViewManager = productViewManager;
            _structureViewManager = structureViewManager;
            _unitMeasureViewManager = unitMeasureViewManager;
            _stationViewManager = stationViewManager;
        }

        public IActionResult _ExistencesIndex()
        {
            var productOnStockVM = new ProductViewModel();
            ViewData["ExistencesTitle"] = _localizer["Catalog"].ToString();
            return View(productOnStockVM);
        }

        public async Task<IActionResult> _LoadAllExistences(DatatableParamsViewModel DTParamsVM, string sSearch_2, string sSearch_3,
            string sSearch_4, string sSearch_5, string sSearch_6, string sSearch_7, string sSearch_8, string sSearch_9, string sSearch_10)
        {
            try
            {
                // Me tengo que traer todos los productos que estén en stock, quiere decir que tengan el atributo StoredStock de la tabla ProductFeatures activo.
                var existencesResponse = await _productViewManager.FindBySpecification(new ProductsStoredStockSpecification(true));
                if (existencesResponse.Succeeded)
                {
                    var existencesVM = _mapper.Map<IEnumerable<ProductViewModel>>(existencesResponse.Data);

                    //existencesVM = removeCycles(existencesVM);

                    existencesVM = removeChilds(existencesVM.ToList());

                    existencesVM = filterInformation(existencesVM, sSearch_2, sSearch_3, sSearch_4, sSearch_5, sSearch_6, sSearch_7, sSearch_8,
                        sSearch_9, sSearch_10, 0, null, DTParamsVM);

                    var displayResult = existencesVM.Skip(DTParamsVM.iDisplayStart)
                      .Take(DTParamsVM.iDisplayLength).ToList();

                    var totalRecords = existencesVM.Count();

                    return Json(new
                    {
                        DTParamsVM.sEcho,
                        iTotalRecords = totalRecords,
                        iTotalDisplayRecords = totalRecords,
                        aaData = displayResult
                    });

                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return null;
            }
        }

        //public async Task<JsonResult> loadProducts_select2()
        //{
        //    try
        //    {
        //        IEnumerable<ProductDTO> productDTOs = new List<ProductDTO>();

        //        var productsResponse = await _productViewManager.FindBySpecification(new ProductsStoredStockSpecification(false));

        //        if (productsResponse.Succeeded)
        //        {

        //            productDTOs = productsResponse.Data.OrderBy(p => p.Description);

        //            if (productDTOs.Count() > 0)
        //            {
        //                var products = new SelectList((from p in productDTOs.ToList() select new { Id = p.Id, CodeAndDescription = p.Code + " - " + p.Description }), "Id", "CodeAndDescription", null);
        //                return new JsonResult(new { isValid = true, values = products });
        //            }
        //            else
        //            {
        //                return new JsonResult(new { isValid = false, values = "" });
        //            }
        //        }
        //        else
        //        {
        //            return new JsonResult(new { isValid = false, values = "" });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _notify.Error(ex.Message);
        //        return new JsonResult(new { isValid = false, values = "" });
        //    }
        //}

        public async Task<JsonResult> LoadProductsSelect2(string search)
        {
            var productsResponse = await _productViewManager.FindBySpecification(new ProductsStoredStockSpecification(false, search));
            if (productsResponse.Succeeded)
            {
                List<Select2ViewModel> mapSelect2Data = new List<Select2ViewModel>();
                var productsViewModel = _mapper.Map<List<ProductViewModel>>(productsResponse.Data.ToList());

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

        public async Task<JsonResult> _OnGetCreateOrEditExistence(int productId = 0)
        {
            var productVM = new ProductViewModel();

            var stationResponse = await _productViewManager.GetStations();
            var unitMeasures = await _unitMeasureViewManager.FindBySpecification(new UnitMeasureIsActiveSpecification());

            if (productId == 0)
            {
                if (stationResponse.Succeeded)
                {
                    var stationsViewModel = _mapper.Map<List<StationViewModel>>(stationResponse.Data);
                    productVM.StationsList = stationsViewModel;
                }

                if (unitMeasures.Succeeded)
                {
                    var unitMeasureViewModel = _mapper.Map<List<UnitMeasureViewModel>>(unitMeasures.Data);
                    productVM.UnitsMeasure = new SelectList(unitMeasureViewModel, nameof(UnitMeasureViewModel.Id), nameof(UnitMeasureViewModel.Description), null, null);

                    productVM.StockWidthUnitMeasures = new SelectList(unitMeasureViewModel.Where(um => um.UnitType == 2).ToList(), nameof(UnitMeasureViewModel.Id), nameof(UnitMeasureViewModel.Description), null, null);
                    productVM.StockLengthUnitMeasures = new SelectList(unitMeasureViewModel.Where(um => um.UnitType == 2).ToList(), nameof(UnitMeasureViewModel.Id), nameof(UnitMeasureViewModel.Description), null, null);
                    productVM.StockHeightUnitMeasures = new SelectList(unitMeasureViewModel.Where(um => um.UnitType == 2).ToList(), nameof(UnitMeasureViewModel.Id), nameof(UnitMeasureViewModel.Description), null, null);
                    productVM.StockWeightUnitMeasures = new SelectList(unitMeasureViewModel.Where(um => um.UnitType == 3).ToList(), nameof(UnitMeasureViewModel.Id), nameof(UnitMeasureViewModel.Description), null, null);

                    productVM.StockQuantityUnitMeasures = new SelectList(unitMeasureViewModel.ToList(), nameof(UnitMeasureViewModel.Id), nameof(UnitMeasureViewModel.Description), null, null);

                    productVM.ExistenceUnitMeasures = new SelectList(unitMeasureViewModel.ToList(), nameof(UnitMeasureViewModel.Id), nameof(UnitMeasureViewModel.Description), null, null);
                    productVM.MinimumUnitMeasures = new SelectList(unitMeasureViewModel.ToList(), nameof(UnitMeasureViewModel.Id), nameof(UnitMeasureViewModel.Description), null, null);
                    productVM.QuantityToOrderUnitMeasures = new SelectList(unitMeasureViewModel.ToList(), nameof(UnitMeasureViewModel.Id), nameof(UnitMeasureViewModel.Description), null, null);

                }

                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_OnGetCreateOrEditExistence", productVM) });
            }
            else
            {
                var getExistence = await _productViewManager.FindBySpecification(new ProductsStoredStockSpecification(productId));
                if (getExistence.Succeeded)
                {
                    productVM = _mapper.Map<List<ProductViewModel>>(getExistence.Data).FirstOrDefault();

                    if (stationResponse.Succeeded)
                    {
                        var stationsViewModel = _mapper.Map<List<StationViewModel>>(stationResponse.Data);
                        productVM.StationsList = stationsViewModel;
                    }

                    if (unitMeasures.Succeeded)
                    {
                        var unitMeasureViewModel = _mapper.Map<List<UnitMeasureViewModel>>(unitMeasures.Data);
                        productVM.UnitsMeasure = new SelectList(unitMeasureViewModel, nameof(UnitMeasureViewModel.Id), nameof(UnitMeasureViewModel.Description), null, null);

                        productVM.StockWidthUnitMeasures = new SelectList(unitMeasureViewModel.Where(um => um.UnitType == 2).ToList(), nameof(UnitMeasureViewModel.Id), nameof(UnitMeasureViewModel.Description), productVM.StockWidthUnitMeasureId, null);
                        productVM.StockLengthUnitMeasures = new SelectList(unitMeasureViewModel.Where(um => um.UnitType == 2).ToList(), nameof(UnitMeasureViewModel.Id), nameof(UnitMeasureViewModel.Description), productVM.StockLengthUnitMeasureId, null);
                        productVM.StockHeightUnitMeasures = new SelectList(unitMeasureViewModel.Where(um => um.UnitType == 2).ToList(), nameof(UnitMeasureViewModel.Id), nameof(UnitMeasureViewModel.Description), productVM.StockHeightUnitMeasureId, null);
                        productVM.StockWeightUnitMeasures = new SelectList(unitMeasureViewModel.Where(um => um.UnitType == 3).ToList(), nameof(UnitMeasureViewModel.Id), nameof(UnitMeasureViewModel.Description), productVM.StockWeightUnitMeasureId, null);

                        productVM.StockQuantityUnitMeasures = new SelectList(unitMeasureViewModel.ToList(), nameof(UnitMeasureViewModel.Id), nameof(UnitMeasureViewModel.Description), productVM.StockQuantityUnitMeasureId, null);

                        if (productVM.ExistenceUnitMeasure != null)
                        {
                            productVM.ExistenceUnitMeasures = new SelectList(unitMeasureViewModel.ToList(), nameof(UnitMeasureViewModel.Id), nameof(UnitMeasureViewModel.Description), productVM.ExistenceUnitMeasureId, null);
                        }
                        else
                        {
                            productVM.ExistenceUnitMeasures = new SelectList(unitMeasureViewModel.ToList(), nameof(UnitMeasureViewModel.Id), nameof(UnitMeasureViewModel.Description), productVM.ExistenceUnitMeasureId, null);
                        }

                        if (productVM.MinimumUnitMeasure != null)
                        {
                            productVM.MinimumUnitMeasures = new SelectList(unitMeasureViewModel.ToList(), nameof(UnitMeasureViewModel.Id), nameof(UnitMeasureViewModel.Description), productVM.MinimumUnitMeasureId, null);
                        }
                        else
                        {
                            productVM.MinimumUnitMeasures = new SelectList(unitMeasureViewModel.ToList(), nameof(UnitMeasureViewModel.Id), nameof(UnitMeasureViewModel.Description), productVM.MinimumUnitMeasureId, null);
                        }

                        if (productVM.QuantityToOrderUnitMeasure != null)
                        {
                            productVM.QuantityToOrderUnitMeasures = new SelectList(unitMeasureViewModel.ToList(), nameof(UnitMeasureViewModel.Id), nameof(UnitMeasureViewModel.Description), productVM.QuantityToOrderUnitMeasureId, null);
                        }
                        else
                        {
                            productVM.QuantityToOrderUnitMeasures = new SelectList(unitMeasureViewModel.ToList(), nameof(UnitMeasureViewModel.Id), nameof(UnitMeasureViewModel.Description), productVM.QuantityToOrderUnitMeasureId, null);
                        }

                        //if (productVM.ExistenceUnitMeasure != null)
                        //{
                        //    productVM.ExistenceUnitMeasures = new SelectList(unitMeasureViewModel.Where(um => um.UnitType == productVM.ExistenceUnitMeasure.UnitType).ToList(), nameof(UnitMeasureViewModel.Id), nameof(UnitMeasureViewModel.Description), productVM.ExistenceUnitMeasureId, null);
                        //}
                        //else
                        //{
                        //    productVM.ExistenceUnitMeasures = new SelectList(unitMeasureViewModel.ToList(), nameof(UnitMeasureViewModel.Id), nameof(UnitMeasureViewModel.Description), productVM.ExistenceUnitMeasureId, null);
                        //}
                        //if (productVM.MinimumUnitMeasure != null)
                        //{
                        //    productVM.MinimumUnitMeasures = new SelectList(unitMeasureViewModel.Where(um => um.UnitType == productVM.MinimumUnitMeasure.UnitType).ToList(), nameof(UnitMeasureViewModel.Id), nameof(UnitMeasureViewModel.Description), productVM.MinimumUnitMeasureId, null);
                        //}
                        //else
                        //{
                        //    productVM.MinimumUnitMeasures = new SelectList(unitMeasureViewModel.ToList(), nameof(UnitMeasureViewModel.Id), nameof(UnitMeasureViewModel.Description), productVM.MinimumUnitMeasureId, null);
                        //}
                        //if (productVM.QuantityToOrderUnitMeasure != null)
                        //{
                        //    productVM.QuantityToOrderUnitMeasures = new SelectList(unitMeasureViewModel.Where(um => um.UnitType == productVM.QuantityToOrderUnitMeasure.UnitType).ToList(), nameof(UnitMeasureViewModel.Id), nameof(UnitMeasureViewModel.Description), productVM.QuantityToOrderUnitMeasureId, null);
                        //}
                        //else
                        //{
                        //    productVM.QuantityToOrderUnitMeasures = new SelectList(unitMeasureViewModel.ToList(), nameof(UnitMeasureViewModel.Id), nameof(UnitMeasureViewModel.Description), productVM.QuantityToOrderUnitMeasureId, null);
                        //}
                    }

                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_OnGetCreateOrEditExistence", productVM) });
                }
                else
                {
                    return null;
                }
            }
        }

        [HttpPost]
        public async Task<JsonResult> _OnPostCreateOrEditExistence(int modelId, int productId, ProductViewModel productVM)
        {
            try
            {
                Task<Result<IReadOnlyList<ProductDTO>>> productByIdResponse = null;
                if (modelId == 0)
                {
                    productByIdResponse = _productViewManager.FindBySpecification(new ProductByIdWithUnitMeasureSpecification(productId));
                }
                else
                {
                    productByIdResponse = _productViewManager.FindBySpecification(new ProductByIdWithUnitMeasureSpecification(modelId));
                }

                if (productByIdResponse.Result.Succeeded)
                {
                    // Hay un ciclo en ProductViewModel -> RelProviderProduct -> Product
                    //for (int i = productByIdResponse.Result.Data.Count() - 1; i >= 0; i--)
                    //{
                    //    for (int x = productByIdResponse.Result.Data.ElementAt(i).RelProviderProducts.Count() - 1; x >= 0; x--)
                    //    {
                    //        productByIdResponse.Result.Data.ElementAt(i).RelProviderProducts.ElementAt(x).Product = null;
                    //    }
                    //}

                    var productViewModel = _mapper.Map<ProductViewModel>(productByIdResponse.Result.Data.FirstOrDefault());

                    productViewModel.StorageQuantity = productVM.StorageQuantity;
                    productViewModel.StorageUnitMeasureId = productVM.StorageUnitMeasureId;
                    productViewModel.PhysicalSpaceWidth = productVM.PhysicalSpaceWidth;
                    productViewModel.PhysicalSpaceLength = productVM.PhysicalSpaceLength;
                    productViewModel.PhysicalSpaceHeight = productVM.PhysicalSpaceHeight;
                    productViewModel.LocationStationId = productVM.LocationStationId;
                    productViewModel.Shelves_Rack = productVM.Shelves_Rack;
                    productViewModel.Shelf = productVM.Shelf;
                    productViewModel.Position = productVM.Position;
                    productViewModel.StockWidth = productVM.StockWidth;
                    productViewModel.StockWidthUnitMeasureId = productVM.StockWidthUnitMeasureId;
                    productViewModel.StockLength = productVM.StockLength;
                    productViewModel.StockLengthUnitMeasureId = productVM.StockLengthUnitMeasureId;
                    productViewModel.StockHeight = productVM.StockHeight;
                    productViewModel.StockHeightUnitMeasureId = productVM.StockHeightUnitMeasureId;
                    productViewModel.StockQuantity = productVM.StockQuantity;
                    productViewModel.StockQuantityUnitMeasureId = productVM.StockQuantityUnitMeasureId;
                    productViewModel.StockWeight = productVM.StockWeight;
                    productViewModel.StockWeightUnitMeasureId = productVM.StockWeightUnitMeasureId;
                    productViewModel.Existence = productVM.Existence;
                    productViewModel.ExistenceUnitMeasureId = productVM.ExistenceUnitMeasureId;
                    productViewModel.Minimum = productVM.Minimum;
                    productViewModel.MinimumUnitMeasureId = productVM.MinimumUnitMeasureId;
                    productViewModel.QuantityToOrder = productVM.QuantityToOrder;
                    productViewModel.QuantityToOrderUnitMeasureId = productVM.QuantityToOrderUnitMeasureId;
                    productViewModel.StockObservations = productVM.StockObservations;

                    if (modelId == 0)
                    {
                        productViewModel.ProductFeature.StoredStock = true;
                    }

                    var productDTO = _mapper.Map<ProductDTO>(productViewModel);

                    var result = await _productViewManager.UpdateAsync(productDTO);
                    if (result.Succeeded)
                    {
                        _notify.Success(_localizer["Information updated."]);
                        return new JsonResult(new { isValid = true, html = "" });
                    }
                    else
                    {
                        productVM = await this.PopulateLists(productVM);
                        var html = await _viewRenderer.RenderViewToStringAsync("_OnGetCreateOrEditExistence", productVM);
                        return new JsonResult(new { isValid = false, html = html });
                    }

                }
                else
                {
                    productVM = await this.PopulateLists(productVM);
                    var html = await _viewRenderer.RenderViewToStringAsync("_OnGetCreateOrEditExistence", productVM);
                    return new JsonResult(new { isValid = false, html = html });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                productVM = await this.PopulateLists(productVM);
                var html = await _viewRenderer.RenderViewToStringAsync("_OnGetCreateOrEditExistence", productVM);
                return new JsonResult(new { isValid = false, html = html });
            }
        }

        public async Task<ProductViewModel> PopulateLists(ProductViewModel productVM)
        {
            if (productVM != null)
            {
                var stationResponse = await _productViewManager.GetStations();
                var unitMeasures = await _unitMeasureViewManager.FindBySpecification(new UnitMeasureIsActiveSpecification());

                if (stationResponse.Succeeded)
                {
                    var stationsViewModel = _mapper.Map<List<StationViewModel>>(stationResponse.Data);
                    productVM.StationsList = stationsViewModel;
                }

                if (unitMeasures.Succeeded)
                {
                    var unitMeasureViewModel = _mapper.Map<List<UnitMeasureDTO>>(unitMeasures.Data);
                    productVM.UnitsMeasure = new SelectList(unitMeasureViewModel, nameof(UnitMeasureViewModel.Id), nameof(UnitMeasureViewModel.Description), null, null);

                    productVM.StockWidthUnitMeasures = new SelectList(unitMeasureViewModel.Where(um => um.UnitType == 2).ToList(), nameof(UnitMeasureViewModel.Id), nameof(UnitMeasureViewModel.Description), null, null);
                    productVM.StockLengthUnitMeasures = new SelectList(unitMeasureViewModel.Where(um => um.UnitType == 2).ToList(), nameof(UnitMeasureViewModel.Id), nameof(UnitMeasureViewModel.Description), null, null);
                    productVM.StockHeightUnitMeasures = new SelectList(unitMeasureViewModel.Where(um => um.UnitType == 2).ToList(), nameof(UnitMeasureViewModel.Id), nameof(UnitMeasureViewModel.Description), null, null);
                    productVM.StockQuantityUnitMeasures = new SelectList(unitMeasureViewModel, nameof(UnitMeasureViewModel.Id), nameof(UnitMeasureViewModel.Description), null, null);
                    productVM.StockWeightUnitMeasures = new SelectList(unitMeasureViewModel.Where(um => um.UnitType == 3).ToList(), nameof(UnitMeasureViewModel.Id), nameof(UnitMeasureViewModel.Description), null, null);
                    productVM.ExistenceUnitMeasures = new SelectList(unitMeasureViewModel, nameof(UnitMeasureViewModel.Id), nameof(UnitMeasureViewModel.Description), null, null);
                    productVM.MinimumUnitMeasures = new SelectList(unitMeasureViewModel, nameof(UnitMeasureViewModel.Id), nameof(UnitMeasureViewModel.Description), null, null);
                    productVM.QuantityToOrderUnitMeasures = new SelectList(unitMeasureViewModel, nameof(UnitMeasureViewModel.Id), nameof(UnitMeasureViewModel.Description), null, null);
                }
            }

            return productVM;
        }

        public async Task<JsonResult> GetProductById(int id)
        {
            try
            {
                if (id != -1)
                {
                    var response = await _productViewManager.FindBySpecification(new ProductByIdWithUnitMeasureSpecification(id));
                    if (response.Succeeded)
                    {
                        foreach (var p in response.Data)
                        {
                            if (p.UnitMeasure != null)
                            {
                                p.SubCategory.Category.SubCategories = null;
                                return new JsonResult(new { isValid = true, product = p });
                            }
                            else
                            {
                                return new JsonResult(new { isValid = false, product = "" });
                            }
                        }
                        return new JsonResult(new { isValid = false, product = "" });
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

        [HttpPost]
        public async Task<JsonResult> _OnPostDelete(int productId)
        {
            try
            {
                var deactivateStoredStockProduct = await _productViewManager.DeactivateStoredStock(productId);
                if (deactivateStoredStockProduct.Succeeded)
                {
                    _notify.Information(_localizer["Product stock management was removed."]);
                    return new JsonResult(new { isValid = true });

                }
                else
                {
                    _notify.Error(deactivateStoredStockProduct.Message);
                    return null;
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return null;
            }
        }

        public async Task<IActionResult> ExportToExcel(string columnFilter_2, string columnFilter_3, string columnFilter_4, string columnFilter_5,
            string columnFilter_6, string columnFilter_7, string columnFilter_8, string columnFilter_9, string columnFilter_10,
            int colIndexOrder, string colOrderDirection)
        {
            try
            {
                // Me tengo que traer todos los productos que estén en stock, quiere decir que tengan el atributo StoredStock de la tabla ProductFeatures activo.
                var existencesResponse = await _productViewManager.FindBySpecification(new ProductsStoredStockSpecification(true));
                if (existencesResponse.Succeeded)
                {
                    var existencesVM = _mapper.Map<IEnumerable<ProductViewModel>>(existencesResponse.Data);

                    //existencesVM = removeCycles(existencesVM);

                    existencesVM = removeChilds(existencesVM.ToList());

                    existencesVM = filterInformation(existencesVM, columnFilter_2, columnFilter_3, columnFilter_4, columnFilter_5, columnFilter_6, columnFilter_7, columnFilter_8,
                        columnFilter_9, columnFilter_10, colIndexOrder, colOrderDirection, null);

                    return ExportViewModelToExcel(existencesVM);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return null;
            }
        }

        public IActionResult ExportViewModelToExcel(IEnumerable<ProductViewModel> viewModel)
        {
            try
            {
                var nameForExcelFile = "Existencias_";

                var stream = new MemoryStream();
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var xlPackage = new ExcelPackage(stream))
                {
                    var workSheet = xlPackage.Workbook.Worksheets.Add("Existencias");

                    workSheet.Cells["A1"].Value = _localizer["Code"];
                    workSheet.Cells["B1"].Value = _localizer["Product"];
                    workSheet.Cells["C1"].Value = _localizer["Quantity available"];
                    workSheet.Cells["D1"].Value = _localizer["Existence"];
                    workSheet.Cells["E1"].Value = _localizer["Point of order"];
                    workSheet.Cells["F1"].Value = _localizer["Quantity to order"];
                    workSheet.Cells["G1"].Value = _localizer["Storage unit"];
                    workSheet.Cells["H1"].Value = _localizer["Location"];
                    workSheet.Cells["I1"].Value = _localizer["Heading"];

                    var row = 2;
                    foreach (var itemVM in viewModel)
                    {
                        if (itemVM.Code != null)
                        {
                            workSheet.Cells[row, 1].Value = itemVM.Code.ToString();
                        }
                        else
                        {
                            workSheet.Cells[row, 1].Value = "";
                        }

                        if (itemVM.Description != null)
                        {
                            workSheet.Cells[row, 2].Value = itemVM.Description.ToString();
                        }
                        else
                        {
                            workSheet.Cells[row, 2].Value = "";
                        }

                        // Quantity available
                        workSheet.Cells[row, 3].Value = "";
                        workSheet.Cells[row, 3].Style.Numberformat.Format = "0";

                        if (itemVM.Existence != null)
                        {
                            workSheet.Cells[row, 4].Value = itemVM.Existence;
                            //workSheet.Cells[row, 4].Style.Numberformat.Format = "0.00";
                            workSheet.Cells[row, 4].Style.Numberformat.Format = "#,##0.00";
                        }
                        else
                        {
                            workSheet.Cells[row, 4].Value = "";
                        }

                        if (itemVM.Minimum != null)
                        {
                            workSheet.Cells[row, 5].Value = itemVM.Minimum;
                            //workSheet.Cells[row, 5].Style.Numberformat.Format = "0.00";
                            workSheet.Cells[row, 5].Style.Numberformat.Format = "#,##0.00";
                        }
                        else
                        {
                            workSheet.Cells[row, 5].Value = "";
                        }

                        if (itemVM.QuantityToOrder != null)
                        {
                            workSheet.Cells[row, 6].Value = itemVM.QuantityToOrder;
                            //workSheet.Cells[row, 6].Style.Numberformat.Format = "0.00";
                            workSheet.Cells[row, 6].Style.Numberformat.Format = "#,##0.00";
                        }
                        else
                        {
                            workSheet.Cells[row, 6].Value = "";
                        }

                        if (itemVM.StorageUnitMeasure != null)
                        {
                            workSheet.Cells[row, 7].Value = itemVM.StorageUnitMeasure.Description.ToString();
                        }
                        else
                        {
                            workSheet.Cells[row, 7].Value = "";
                        }

                        if (itemVM.LocationStation != null)
                        {
                            workSheet.Cells[row, 8].Value = itemVM.LocationStation.AbreviationDescription.ToString();
                        }
                        else
                        {
                            workSheet.Cells[row, 8].Value = "";
                        }

                        if (itemVM.SubCategory != null)
                        {
                            workSheet.Cells[row, 9].Value = itemVM.SubCategory.Description.ToString();
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

        public IEnumerable<ProductViewModel> filterInformation(IEnumerable<ProductViewModel> existencesVM, string sSearch_2, string sSearch_3, string sSearch_4,
             string sSearch_5, string sSearch_6, string sSearch_7, string sSearch_8, string sSearch_9, string sSearch_10, int colIndexOrder = 0,
             string colOrderDirection = null, DatatableParamsViewModel DTParamsVM = null)
        {
            try
            {
                if (!string.IsNullOrEmpty(sSearch_2))
                {
                    existencesVM = existencesVM.Where(ex => ex.SubCategory != null && ex.SubCategory.Description.ToString().ToLower().Contains(sSearch_2.ToLower())).ToList();
                }
                if (!string.IsNullOrEmpty(sSearch_3))
                {
                    existencesVM = existencesVM.Where(ex => ex.Code.ToString().ToLower().Contains(sSearch_3.ToLower())).ToList();
                }
                if (!string.IsNullOrEmpty(sSearch_4))
                {
                    existencesVM = existencesVM.Where(ex => ex.Description.ToString().ToLower().Contains(sSearch_4.ToLower())).ToList();
                }
                if (!string.IsNullOrEmpty(sSearch_5))
                {

                }
                if (!string.IsNullOrEmpty(sSearch_6))
                {
                    existencesVM = existencesVM.Where(ex => ex.Existence.ToString().ToLower().Contains(sSearch_6.ToLower())).ToList();
                }
                if (!string.IsNullOrEmpty(sSearch_7))
                {
                    existencesVM = existencesVM.Where(ex => ex.Minimum.ToString().ToLower().Contains(sSearch_7.ToLower())).ToList();
                }
                if (!string.IsNullOrEmpty(sSearch_8))
                {
                    existencesVM = existencesVM.Where(ex => ex.QuantityToOrder.ToString().ToLower().Contains(sSearch_8.ToLower())).ToList();
                }
                if (!string.IsNullOrEmpty(sSearch_9))
                {
                    existencesVM = existencesVM.Where(ex => ex.StorageUnitMeasure != null && ex.StorageUnitMeasure.Description.ToString().ToLower().Contains(sSearch_9.ToLower())).ToList();
                }
                if (!string.IsNullOrEmpty(sSearch_10))
                {
                    existencesVM = existencesVM.Where(ex => ex.LocationStation != null && ex.LocationStation.Description.ToString().ToLower().Contains(sSearch_10.ToLower())).ToList();
                }

                //var sortMultipleColumns = false;

                var sortColumnIndex = colIndexOrder;
                var sortDirection = colOrderDirection;
                if (DTParamsVM != null)
                {
                    //if (DTParamsVM.iSortingCols > 1)
                    //{
                    //    sortMultipleColumns = true;
                    //}
                    sortColumnIndex = DTParamsVM.iSortCol_0;
                    sortDirection = DTParamsVM.sSortDir_0;
                }

                var sortMultipleColumns = true;

                if (sortMultipleColumns)
                {
                    // Ordeno por rubro y luego por descripción de producto
                    existencesVM = existencesVM.OrderBy(ex => ex.SubCategory != null ? ex.SubCategory.Description : null).ThenBy(ex => ex.Description);
                }
                else
                {
                    if (sortColumnIndex == 2)
                    {
                        existencesVM = sortDirection == "asc" ? existencesVM.OrderBy(ex => ex.SubCategory != null ? ex.SubCategory.Description : null) : existencesVM.OrderByDescending(ex => ex.SubCategory != null ? ex.SubCategory.Description : null);
                    }
                    else if (sortColumnIndex == 3)
                    {
                        existencesVM = sortDirection == "asc" ? existencesVM.OrderBy(ex => ex.Code) : existencesVM.OrderByDescending(ex => ex.Code);
                    }
                    else if (sortColumnIndex == 4)
                    {
                        existencesVM = sortDirection == "asc" ? existencesVM.OrderBy(ex => ex.Description) : existencesVM.OrderByDescending(ex => ex.Description);
                    }
                    else if (sortColumnIndex == 5)
                    {

                    }
                    else if (sortColumnIndex == 6)
                    {
                        existencesVM = sortDirection == "asc" ? existencesVM.OrderBy(ex => ex.Existence) : existencesVM.OrderByDescending(ex => ex.Existence);
                    }
                    else if (sortColumnIndex == 7)
                    {
                        existencesVM = sortDirection == "asc" ? existencesVM.OrderBy(ex => ex.Minimum) : existencesVM.OrderByDescending(ex => ex.Minimum);
                    }
                    else if (sortColumnIndex == 8)
                    {
                        existencesVM = sortDirection == "asc" ? existencesVM.OrderBy(ex => ex.QuantityToOrder) : existencesVM.OrderByDescending(ex => ex.QuantityToOrder);
                    }
                    else if (sortColumnIndex == 9)
                    {
                        existencesVM = sortDirection == "asc" ? existencesVM.OrderBy(ex => ex.StorageUnitMeasure != null ? ex.StorageUnitMeasure.Description : null) : existencesVM.OrderByDescending(ex => ex.StorageUnitMeasure != null ? ex.StorageUnitMeasure.Description : null);
                    }
                    else if (sortColumnIndex == 10)
                    {
                        existencesVM = sortDirection == "asc" ? existencesVM.OrderBy(ex => ex.LocationStation != null ? ex.LocationStation.Description : null) : existencesVM.OrderByDescending(ex => ex.LocationStation != null ? ex.LocationStation.Description : null);
                    }
                }

                return existencesVM;
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return existencesVM;
            }
        }

        public IEnumerable<ProductViewModel> removeCycles(IEnumerable<ProductViewModel> existencesVM)
        {
            try
            {
                // Ciclo Product - Structures - Product ...
                for (int i = existencesVM.Count() - 1; i >= 0; i--)
                {
                    for (int x = existencesVM.ElementAt(i).Structures.Count() - 1; x >= 0; x--)
                    {
                        existencesVM.ElementAt(i).Structures.ElementAt(x).Product = null;
                    }
                }

                // Ciclo Product - Structures - StructureItems - Structure ...
                for (int i = existencesVM.Count() - 1; i >= 0; i--)
                {
                    for (int x = existencesVM.ElementAt(i).Structures.Count() - 1; x >= 0; x--)
                    {
                        for (int y = existencesVM.ElementAt(i).Structures.ElementAt(x).StructureItems.Count() - 1; y >= 0; y--)
                        {
                            existencesVM.ElementAt(i).Structures.ElementAt(x).StructureItems.ElementAt(y).Structure = null;
                        }
                    }
                }

                return existencesVM;
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return existencesVM;
            }
        }

        public IEnumerable<ProductViewModel> removeChilds(List<ProductViewModel> existencesVM)
        {
            try
            {
                // Si esta el padre cargado en stock se muestra solo el padre.
                // Si de un padre no tiene todos sus hijos se muestran esos hijos independientes.
                List<ProductViewModel> existencesVMAux = new List<ProductViewModel>();
                foreach (var product in existencesVM)
                {
                    existencesVMAux.Add(product);

                    if (product.Structures.Count > 0)
                    {
                        foreach (var st in product.Structures)
                        {
                            if (st.StructureItems.Count > 0)
                            {
                                foreach (var sti in st.StructureItems)
                                {
                                    if (sti.SonProduct != null)
                                    {
                                        if (existencesVM.Any(ex => ex.Id == sti.SonProduct.Id && ex.SubCategory.CategoryId != 4))
                                        {
                                            existencesVMAux.RemoveAll(ex => ex.Id == sti.SonProduct.Id);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                return existencesVMAux;
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return existencesVM;
            }
        }

    }
}
