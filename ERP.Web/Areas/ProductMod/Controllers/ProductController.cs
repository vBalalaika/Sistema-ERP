using AutoMapper;
using ERP.Application.Constants;
using ERP.Application.DTOs.Entities.Commons;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Application.Interfaces.ViewManagers.ProductMod;
using ERP.Application.Interfaces.ViewManagers.Purchases.Providers;
using ERP.Application.Specification.ProductMod;
using ERP.Application.Specification.Purchases.ProviderProduct;
using ERP.Language;
using ERP.Web.Abstractions;
using ERP.Web.Areas.Commons.Models;
using ERP.Web.Areas.ProductMod.Models;
using ERP.Web.Areas.Purchases.Models.Providers;
using ERP.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Web.Areas.ProductMod.Controllers
{
    [Area("ProductMod")]
    public class ProductController : BaseController<ProductController>
    {
        private readonly IProductViewManager _viewManager;
        private readonly ISubCategoryViewManager _viewManagerSubCategory;
        private readonly IRelProviderProductViewManager _viewManagerRelProviderProduct;
        private readonly IStructureVersionViewManager _structureversionviewManager;
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly IArchiveViewManager _archiveViewManager;

        public ProductController(IProductViewManager viewManager, ISubCategoryViewManager viewManagerSubCategory, IStructureVersionViewManager structureversionviewManager, IRelProviderProductViewManager viewManagerRelProviderProduct, IArchiveViewManager archiveViewManager, IStringLocalizer<SharedResource> localizer)
        {
            _viewManager = viewManager;
            _viewManagerSubCategory = viewManagerSubCategory;
            _viewManagerRelProviderProduct = viewManagerRelProviderProduct;
            _structureversionviewManager = structureversionviewManager;
            _localizer = localizer;
            _archiveViewManager = archiveViewManager;
        }
        public IActionResult Index(int categoryId, string categoryDescription, bool? Smak, string msg, bool? productCreated)
        {
            if (productCreated != null)
            {
                if (productCreated == true)
                {
                    _notify.Success(msg);
                }
                else
                {
                    _notify.Information(msg);
                }

            }
            var model = new ProductViewModel();
            model.CategoryIdFilter = categoryId;
            model.CategoryDescriptionFilter = categoryDescription;
            model.ProducedBySmak = Smak;
            return View(model);
        }

        public async Task<IActionResult> ExportToExcel(int? categoryId, bool? Smak, string columnFilter_2, string columnFilter_3, string columnFilter_4, string columnFilter_5,
             string columnFilter_6, string columnFilter_7, string columnFilter_8, string columnFilter_9, string columnFilter_11, int colIndexOrder, string colOrderDirection)
        {
            try
            {
                //if (categoryId != null && Smak != null)
                //{
                AspNetCoreHero.Results.Result<IReadOnlyList<ProductDTO>> components_0 = null;
                var isComponent_0 = 0;
                AspNetCoreHero.Results.Result<IReadOnlyList<ProductDTO>> components_3 = null;
                var isComponent_3 = 3;
                AspNetCoreHero.Results.Result<IReadOnlyList<ProductDTO>> machines = null;
                var isMachines = 1;

                IEnumerable<ProductViewModel> viewModel = null;

                switch (categoryId)
                {
                    case 0:
                        components_0 = await _viewManager.FindBySpecification(new ProductByCategoryAndInHouseSpecification(null, Smak));
                        if (components_0 != null)
                        {
                            if (components_0.Succeeded)
                            {
                                viewModel = _mapper.Map<List<ProductViewModel>>(components_0.Data);

                                if (!string.IsNullOrEmpty(columnFilter_2))
                                {
                                    viewModel = viewModel.Where(x => x.Code.ToLower().Contains(columnFilter_2.ToLower())).ToList();
                                }
                                if (!string.IsNullOrEmpty(columnFilter_3))
                                {
                                    viewModel = viewModel.Where(x => x.Description.ToLower().Contains(columnFilter_3.ToLower())).ToList();
                                }
                                if (!string.IsNullOrEmpty(columnFilter_4))
                                {
                                    viewModel = viewModel.Where(x => x.SubCategory != null && x.SubCategory.Description.ToLower().Contains(columnFilter_4.ToLower())).ToList();
                                }

                                if (!string.IsNullOrEmpty(columnFilter_5))
                                {
                                    viewModel = viewModel.Where(x => x.ProductFeature != null && x.ProductFeature.RawMaterialCode.ToLower().Contains(columnFilter_5.ToLower())).ToList();
                                }
                                if (!string.IsNullOrEmpty(columnFilter_6))
                                {
                                    viewModel = viewModel.Where(x => x.ProductFeature != null && x.ProductFeature.RawMaterialDescription.ToLower().Contains(columnFilter_6.ToLower())).ToList();
                                }
                                if (!string.IsNullOrEmpty(columnFilter_7))
                                {
                                    viewModel = viewModel.Where(x => x.ProductFeature != null && x.ProductFeature.ComponentWidhtPiece.ToLower().Contains(columnFilter_7.ToLower())).ToList();
                                }
                                if (!string.IsNullOrEmpty(columnFilter_8))
                                {
                                    viewModel = viewModel.Where(x => x.ProductFeature != null && x.ProductFeature.ComponentLongPiece.ToLower().Contains(columnFilter_8.ToLower())).ToList();
                                }

                                if (!string.IsNullOrEmpty(columnFilter_9))
                                {
                                    viewModel = viewModel.Where(x => x.Roadmap.ToLower().Contains(columnFilter_9.ToLower())).ToList();
                                }

                                if (!string.IsNullOrEmpty(columnFilter_11))
                                {
                                    viewModel = viewModel.Where(x => x.UnitMeasure != null && x.UnitMeasure.Description.ToLower().Contains(columnFilter_11.ToLower())).ToList();
                                }

                                if (colIndexOrder != 0 && colOrderDirection != "")
                                {

                                    var sortColumnIndex = colIndexOrder;
                                    var sortDirection = colOrderDirection;

                                    // Code
                                    if (sortColumnIndex == 2)
                                    {
                                        viewModel = sortDirection == "asc" ? viewModel.OrderBy(p => p.Code) : viewModel.OrderByDescending(p => p.Code);
                                    }
                                    // Description
                                    else if (sortColumnIndex == 3)
                                    {
                                        viewModel = sortDirection == "asc" ? viewModel.OrderBy(p => p.Description) : viewModel.OrderByDescending(p => p.Description);
                                    }
                                    // Family/Heading
                                    else if (sortColumnIndex == 4)
                                    {
                                        viewModel = sortDirection == "asc" ? viewModel.OrderBy(p => p.SubCategory != null ? p.SubCategory.Description.ToString() : null) : viewModel.OrderByDescending(p => p.SubCategory != null ? p.SubCategory.Description.ToString() : null);
                                    }
                                    // Raw material code
                                    else if (sortColumnIndex == 5)
                                    {
                                        viewModel = sortDirection == "asc" ? viewModel.OrderBy(p => p.ProductFeature != null ? p.ProductFeature.RawMaterialCode.ToString() : null) : viewModel.OrderByDescending(p => p.ProductFeature != null ? p.ProductFeature.RawMaterialCode.ToString() : null);
                                    }
                                    // Raw material description
                                    else if (sortColumnIndex == 6)
                                    {
                                        viewModel = sortDirection == "asc" ? viewModel.OrderBy(p => p.ProductFeature != null ? p.ProductFeature.RawMaterialDescription.ToString() : null) : viewModel.OrderByDescending(p => p.ProductFeature != null ? p.ProductFeature.RawMaterialDescription.ToString() : null);
                                    }
                                    // Width
                                    else if (sortColumnIndex == 7)
                                    {
                                        viewModel = sortDirection == "asc" ? viewModel.OrderBy(p => p.ProductFeature != null ? p.ProductFeature.ComponentWidhtPiece.ToString() : null) : viewModel.OrderByDescending(p => p.ProductFeature != null ? p.ProductFeature.ComponentWidhtPiece.ToString() : null);
                                    }
                                    // Length
                                    else if (sortColumnIndex == 8)
                                    {
                                        viewModel = sortDirection == "asc" ? viewModel.OrderBy(p => p.ProductFeature != null ? p.ProductFeature.ComponentLongPiece.ToString() : null) : viewModel.OrderByDescending(p => p.ProductFeature != null ? p.ProductFeature.ComponentLongPiece.ToString() : null);
                                    }
                                    // Road map
                                    else if (sortColumnIndex == 9)
                                    {
                                        viewModel = sortDirection == "asc" ? viewModel.OrderBy(p => p.Roadmap) : viewModel.OrderByDescending(p => p.Roadmap);
                                    }
                                    // Unit
                                    else if (sortColumnIndex == 11)
                                    {
                                        viewModel = sortDirection == "asc" ? viewModel.OrderBy(p => p.UnitMeasure != null ? p.UnitMeasure.Description.ToString() : null) : viewModel.OrderByDescending(p => p.UnitMeasure != null ? p.UnitMeasure.Description.ToString() : null);
                                    }
                                }

                                return ExportViewModelToExcel(viewModel, isComponent_0);
                            }
                            else
                            {
                                return null;
                            }
                        }
                        else
                        {
                            return null;
                        }
                    case 3:
                        components_3 = await _viewManager.FindBySpecification(new ProductByCategoryAndInHouseSpecification(categoryId, Smak));
                        if (components_3 != null)
                        {
                            if (components_3.Succeeded)
                            {
                                viewModel = _mapper.Map<List<ProductViewModel>>(components_3.Data);

                                if (!string.IsNullOrEmpty(columnFilter_2))
                                {
                                    viewModel = viewModel.Where(x => x.Code.ToLower().Contains(columnFilter_2.ToLower())).ToList();
                                }
                                if (!string.IsNullOrEmpty(columnFilter_3))
                                {
                                    viewModel = viewModel.Where(x => x.Description.ToLower().Contains(columnFilter_3.ToLower())).ToList();
                                }
                                if (!string.IsNullOrEmpty(columnFilter_4))
                                {
                                    viewModel = viewModel.Where(x => x.SubCategory != null && x.SubCategory.Description.ToLower().Contains(columnFilter_4.ToLower())).ToList();
                                }

                                if (!string.IsNullOrEmpty(columnFilter_5))
                                {
                                    viewModel = viewModel.Where(x => x.ProductFeature != null && x.ProductFeature.RawMaterialCode.ToLower().Contains(columnFilter_5.ToLower())).ToList();
                                }
                                if (!string.IsNullOrEmpty(columnFilter_6))
                                {
                                    viewModel = viewModel.Where(x => x.ProductFeature != null && x.ProductFeature.RawMaterialDescription.ToLower().Contains(columnFilter_6.ToLower())).ToList();
                                }
                                if (!string.IsNullOrEmpty(columnFilter_7))
                                {
                                    viewModel = viewModel.Where(x => x.ProductFeature != null && x.ProductFeature.ComponentWidhtPiece.ToLower().Contains(columnFilter_7.ToLower())).ToList();
                                }
                                if (!string.IsNullOrEmpty(columnFilter_8))
                                {
                                    viewModel = viewModel.Where(x => x.ProductFeature != null && x.ProductFeature.ComponentLongPiece.ToLower().Contains(columnFilter_8.ToLower())).ToList();
                                }

                                if (!string.IsNullOrEmpty(columnFilter_9))
                                {
                                    viewModel = viewModel.Where(x => x.Roadmap.ToLower().Contains(columnFilter_9.ToLower())).ToList();
                                }

                                if (!string.IsNullOrEmpty(columnFilter_11))
                                {
                                    viewModel = viewModel.Where(x => x.UnitMeasure != null && x.UnitMeasure.Description.ToLower().Contains(columnFilter_11.ToLower())).ToList();
                                }

                                if (colIndexOrder != 0 && colOrderDirection != "")
                                {

                                    var sortColumnIndex = colIndexOrder;
                                    var sortDirection = colOrderDirection;

                                    // Code
                                    if (sortColumnIndex == 2)
                                    {
                                        viewModel = sortDirection == "asc" ? viewModel.OrderBy(p => p.Code) : viewModel.OrderByDescending(p => p.Code);
                                    }
                                    // Description
                                    else if (sortColumnIndex == 3)
                                    {
                                        viewModel = sortDirection == "asc" ? viewModel.OrderBy(p => p.Description) : viewModel.OrderByDescending(p => p.Description);
                                    }
                                    // Family/Heading
                                    else if (sortColumnIndex == 4)
                                    {
                                        viewModel = sortDirection == "asc" ? viewModel.OrderBy(p => p.SubCategory != null ? p.SubCategory.Description.ToString() : null) : viewModel.OrderByDescending(p => p.SubCategory != null ? p.SubCategory.Description.ToString() : null);
                                    }
                                    // Raw material code
                                    else if (sortColumnIndex == 5)
                                    {
                                        viewModel = sortDirection == "asc" ? viewModel.OrderBy(p => p.ProductFeature != null ? p.ProductFeature.RawMaterialCode.ToString() : null) : viewModel.OrderByDescending(p => p.ProductFeature != null ? p.ProductFeature.RawMaterialCode.ToString() : null);
                                    }
                                    // Raw material description
                                    else if (sortColumnIndex == 6)
                                    {
                                        viewModel = sortDirection == "asc" ? viewModel.OrderBy(p => p.ProductFeature != null ? p.ProductFeature.RawMaterialDescription.ToString() : null) : viewModel.OrderByDescending(p => p.ProductFeature != null ? p.ProductFeature.RawMaterialDescription.ToString() : null);
                                    }
                                    // Width
                                    else if (sortColumnIndex == 7)
                                    {
                                        viewModel = sortDirection == "asc" ? viewModel.OrderBy(p => p.ProductFeature != null ? p.ProductFeature.ComponentWidhtPiece.ToString() : null) : viewModel.OrderByDescending(p => p.ProductFeature != null ? p.ProductFeature.ComponentWidhtPiece.ToString() : null);
                                    }
                                    // Length
                                    else if (sortColumnIndex == 8)
                                    {
                                        viewModel = sortDirection == "asc" ? viewModel.OrderBy(p => p.ProductFeature != null ? p.ProductFeature.ComponentLongPiece.ToString() : null) : viewModel.OrderByDescending(p => p.ProductFeature != null ? p.ProductFeature.ComponentLongPiece.ToString() : null);
                                    }
                                    // Road map
                                    else if (sortColumnIndex == 9)
                                    {
                                        viewModel = sortDirection == "asc" ? viewModel.OrderBy(p => p.Roadmap) : viewModel.OrderByDescending(p => p.Roadmap);
                                    }
                                    // Unit
                                    else if (sortColumnIndex == 11)
                                    {
                                        viewModel = sortDirection == "asc" ? viewModel.OrderBy(p => p.UnitMeasure != null ? p.UnitMeasure.Description.ToString() : null) : viewModel.OrderByDescending(p => p.UnitMeasure != null ? p.UnitMeasure.Description.ToString() : null);
                                    }

                                }

                                return ExportViewModelToExcel(viewModel, isComponent_3);
                            }
                            else
                            {
                                return null;
                            }
                        }
                        else
                        {
                            return null;
                        }
                    default:
                        machines = await _viewManager.FindBySpecification(new ProductByCategoryAndInHouseSpecification(categoryId, Smak));
                        if (machines != null)
                        {
                            if (machines.Succeeded)
                            {
                                viewModel = _mapper.Map<List<ProductViewModel>>(machines.Data);

                                if (!string.IsNullOrEmpty(columnFilter_2))
                                {
                                    viewModel = viewModel.Where(x => x.Code.ToLower().Contains(columnFilter_2.ToLower())).ToList();
                                }
                                if (!string.IsNullOrEmpty(columnFilter_3))
                                {
                                    viewModel = viewModel.Where(x => x.Description.ToLower().Contains(columnFilter_3.ToLower())).ToList();
                                }
                                if (!string.IsNullOrEmpty(columnFilter_4))
                                {
                                    viewModel = viewModel.Where(x => x.SubCategory != null && x.SubCategory.Description.ToLower().Contains(columnFilter_4.ToLower())).ToList();
                                }
                                if (!string.IsNullOrEmpty(columnFilter_5))
                                {
                                    viewModel = viewModel.Where(x => x.Roadmap.ToLower().Contains(columnFilter_5.ToLower())).ToList();
                                }
                                if (!string.IsNullOrEmpty(columnFilter_7))
                                {
                                    viewModel = viewModel.Where(x => x.UnitMeasure != null && x.UnitMeasure.Description.ToLower().Contains(columnFilter_7.ToLower())).ToList();
                                }

                                if (colIndexOrder != 0 && colOrderDirection != "")
                                {

                                    var sortColumnIndex = colIndexOrder;
                                    var sortDirection = colOrderDirection;

                                    // Code
                                    if (sortColumnIndex == 2)
                                    {
                                        viewModel = sortDirection == "asc" ? viewModel.OrderBy(p => p.Code) : viewModel.OrderByDescending(p => p.Code);
                                    }
                                    // Description
                                    else if (sortColumnIndex == 3)
                                    {
                                        viewModel = sortDirection == "asc" ? viewModel.OrderBy(p => p.Description) : viewModel.OrderByDescending(p => p.Description);
                                    }
                                    // Family/Heading
                                    else if (sortColumnIndex == 4)
                                    {
                                        viewModel = sortDirection == "asc" ? viewModel.OrderBy(p => p.SubCategory != null ? p.SubCategory.Description.ToString() : null) : viewModel.OrderByDescending(p => p.SubCategory != null ? p.SubCategory.Description.ToString() : null);
                                    }
                                    // Road map
                                    else if (sortColumnIndex == 5)
                                    {
                                        viewModel = sortDirection == "asc" ? viewModel.OrderBy(p => p.Roadmap) : viewModel.OrderByDescending(p => p.Roadmap);
                                    }
                                    // Unit
                                    else if (sortColumnIndex == 7)
                                    {
                                        viewModel = sortDirection == "asc" ? viewModel.OrderBy(p => p.UnitMeasure != null ? p.UnitMeasure.Description.ToString() : null) : viewModel.OrderByDescending(p => p.UnitMeasure != null ? p.UnitMeasure.Description.ToString() : null);
                                    }

                                }

                                return ExportViewModelToExcel(viewModel, isMachines);
                            }
                            else
                            {
                                return null;
                            }
                        }
                        else
                        {
                            return null;
                        }
                }
                //}
                //else
                //{
                //    return null;
                //}

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IActionResult ExportViewModelToExcel(IEnumerable<ProductViewModel> viewModel, int productType)
        {
            try
            {
                var stream = new MemoryStream();
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var xlPackage = new ExcelPackage(stream))
                {
                    var workSheet = xlPackage.Workbook.Worksheets.Add("Productos");

                    if (productType == 0 || productType == 3)
                    {
                        workSheet.Cells["A1"].Value = _localizer["Code"];
                        workSheet.Cells["B1"].Value = _localizer["Description"];
                        workSheet.Cells["C1"].Value = _localizer["Family/Heading"];
                        workSheet.Cells["D1"].Value = _localizer["Raw material code"];
                        workSheet.Cells["E1"].Value = _localizer["Raw material description"];
                        workSheet.Cells["F1"].Value = _localizer["Width"];
                        workSheet.Cells["G1"].Value = _localizer["Length"];
                        workSheet.Cells["H1"].Value = _localizer["Road map"];
                        workSheet.Cells["I1"].Value = _localizer["Stock"];
                        workSheet.Cells["J1"].Value = _localizer["Unit"];
                    }

                    if (productType == 1)
                    {
                        workSheet.Cells["A1"].Value = _localizer["Code"];
                        workSheet.Cells["B1"].Value = _localizer["Description"];
                        workSheet.Cells["C1"].Value = _localizer["Family/Heading"];
                        workSheet.Cells["D1"].Value = _localizer["Road map"];
                        workSheet.Cells["E1"].Value = _localizer["Stock"];
                        workSheet.Cells["F1"].Value = _localizer["Unit"];
                    }

                    var row = 2;
                    foreach (ProductViewModel product in viewModel.ToList())
                    {
                        if (productType == 0 || productType == 3)
                        {
                            if (product.Code != null)
                            {
                                workSheet.Cells[row, 1].Value = product.Code.ToString();
                            }
                            else
                            {
                                workSheet.Cells[row, 1].Value = "";
                            }

                            if (product.Description != null)
                            {
                                workSheet.Cells[row, 2].Value = product.Description.ToString();
                            }
                            else
                            {
                                workSheet.Cells[row, 2].Value = "";
                            }

                            if (product.SubCategory != null)
                            {
                                if (product.SubCategory.Description != null)
                                {
                                    workSheet.Cells[row, 3].Value = product.SubCategory.Description.ToString();
                                }
                                else
                                {
                                    workSheet.Cells[row, 3].Value = "";
                                }
                            }
                            else
                            {
                                workSheet.Cells[row, 3].Value = "";
                            }

                            if (product.ProductFeature != null)
                            {
                                if (product.ProductFeature.RawMaterialCode != null)
                                {
                                    workSheet.Cells[row, 4].Value = product.ProductFeature.RawMaterialCode.ToString();
                                }
                                else
                                {
                                    workSheet.Cells[row, 4].Value = "";
                                }

                                if (product.ProductFeature.RawMaterialDescription != null)
                                {
                                    workSheet.Cells[row, 5].Value = product.ProductFeature.RawMaterialDescription.ToString();
                                }
                                else
                                {
                                    workSheet.Cells[row, 5].Value = "";
                                }

                                if (product.ProductFeature.ComponentWidhtPiece != null)
                                {
                                    if (product.ProductFeature.ComponentWidhtPiece.ToString() != "")
                                    {
                                        //workSheet.Cells[row, 6].Value = Convert.ToDecimal(product.ProductFeature.ComponentWidhtPiece.ToString());
                                        try
                                        {
                                            workSheet.Cells[row, 6].Value = Convert.ToDecimal(product.ProductFeature.ComponentWidhtPiece.ToString());
                                        }
                                        catch
                                        {
                                            workSheet.Cells[row, 6].Value = "";
                                        }
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
                                workSheet.Cells[row, 6].Style.Numberformat.Format = "#,##0.00";

                                if (product.ProductFeature.ComponentLongPiece != null)
                                {
                                    if (product.ProductFeature.ComponentLongPiece.ToString() != "")
                                    {
                                        //workSheet.Cells[row, 7].Value = Convert.ToDecimal(product.ProductFeature.ComponentLongPiece.ToString());
                                        try
                                        {
                                            workSheet.Cells[row, 7].Value = Convert.ToDecimal(product.ProductFeature.ComponentLongPiece.ToString());
                                        }
                                        catch
                                        {
                                            workSheet.Cells[row, 7].Value = "";
                                        }
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
                                workSheet.Cells[row, 7].Style.Numberformat.Format = "#,##0.00";
                            }
                            else
                            {
                                workSheet.Cells[row, 4].Value = "";
                                workSheet.Cells[row, 5].Value = "";
                                workSheet.Cells[row, 6].Value = "";
                                workSheet.Cells[row, 7].Value = "";
                            }

                            if (product.Roadmap != null)
                            {
                                workSheet.Cells[row, 8].Value = product.Roadmap.ToString();
                            }
                            else
                            {
                                workSheet.Cells[row, 8].Value = "";
                            }

                            if (product.ProductFeature.StoredStock)
                            {
                                workSheet.Cells[row, 9].Value = "Si";
                            }
                            else
                            {
                                workSheet.Cells[row, 9].Value = "No";
                            }

                            if (product.UnitMeasure != null)
                            {
                                if (product.UnitMeasure.Description != null)
                                {
                                    workSheet.Cells[row, 10].Value = product.UnitMeasure.Description.ToString();
                                }
                                else
                                {
                                    workSheet.Cells[row, 10].Value = "";
                                }
                            }
                            else
                            {
                                workSheet.Cells[row, 10].Value = "";
                            }
                        }

                        if (productType == 1)
                        {
                            if (product.Code != null)
                            {
                                workSheet.Cells[row, 1].Value = product.Code.ToString();
                            }
                            else
                            {
                                workSheet.Cells[row, 1].Value = "";
                            }

                            if (product.Description != null)
                            {
                                workSheet.Cells[row, 2].Value = product.Description.ToString();
                            }
                            else
                            {
                                workSheet.Cells[row, 2].Value = "";
                            }

                            if (product.SubCategory != null)
                            {
                                if (product.SubCategory.Description != null)
                                {
                                    workSheet.Cells[row, 3].Value = product.SubCategory.Description.ToString();
                                }
                                else
                                {
                                    workSheet.Cells[row, 3].Value = "";
                                }
                            }
                            else
                            {
                                workSheet.Cells[row, 3].Value = "";
                            }

                            if (product.Roadmap != null)
                            {
                                workSheet.Cells[row, 4].Value = product.Roadmap.ToString();
                            }
                            else
                            {
                                workSheet.Cells[row, 4].Value = "";
                            }

                            if (product.ProductFeature != null)
                            {
                                if (product.ProductFeature.StoredStock)
                                {
                                    workSheet.Cells[row, 5].Value = "Si";
                                }
                                else
                                {
                                    workSheet.Cells[row, 5].Value = "No";
                                }
                            }
                            else
                            {
                                workSheet.Cells[row, 5].Value = "";
                            }

                            if (product.SubCategory != null)
                            {
                                if (product.SubCategory.Category != null)
                                {
                                    if (product.SubCategory.Category.Description.ToString() == "Componentes Comprados")
                                    {
                                        if (product.UnitMeasure != null)
                                        {
                                            workSheet.Cells[row, 6].Value = product.UnitMeasure.Description.ToString();
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

                        }

                        row++;
                    }

                    if (productType == 0 || productType == 3)
                    {
                        workSheet.Cells["A1:J1"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                        workSheet.Cells["J1"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                    }
                    if (productType == 1)
                    {
                        workSheet.Cells["A1:F1"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                        workSheet.Cells["F1"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                    }

                    workSheet.Row(1).Style.Font.Bold = true;
                    workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();

                    xlPackage.Workbook.Properties.Title = "Productos_" + DateTime.Now.Date.ToString("dd-MM-yyyy");
                    xlPackage.Save();

                }

                stream.Position = 0;
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Productos_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xlsx");
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return null;
            }
        }

        // To solve circular reference on Product -> Structure -> Product
        public List<ProductViewModel> circularReferenceWithProduct(List<ProductViewModel> viewModel)
        {
            List<ProductViewModel> productsViewModels = new List<ProductViewModel>();
            if (viewModel.Count > 0)
            {
                foreach (var pr in viewModel)
                {
                    ProductViewModel productViewModel = new ProductViewModel();
                    productViewModel.Id = pr.Id;
                    productViewModel.ConcurrencyToken = pr.ConcurrencyToken;
                    productViewModel.Description = pr.Description;
                    productViewModel.Code = pr.Code;
                    productViewModel.Version = pr.Version;
                    productViewModel.Observation = pr.Observation;
                    productViewModel.Existence = pr.Existence;
                    productViewModel.Minimum = pr.Minimum;
                    productViewModel.QuantityToOrder = pr.QuantityToOrder;
                    productViewModel.Cost = pr.Cost;
                    productViewModel.Location = pr.Location;
                    productViewModel.PackingVolume = pr.PackingVolume;
                    productViewModel.WeightEspecification = pr.WeightEspecification;
                    productViewModel.ProductionSpeed = pr.ProductionSpeed;
                    productViewModel.WorkPressure = pr.WorkPressure;
                    productViewModel.CategoryIdFilter = pr.CategoryIdFilter;
                    productViewModel.CategoryDescriptionFilter = pr.CategoryDescriptionFilter;
                    productViewModel.ProducedBySmak = pr.ProducedBySmak;
                    productViewModel.ProductFeatureId = pr.ProductFeatureId;
                    productViewModel.ProductFeature = pr.ProductFeature;
                    productViewModel.LocationStationId = pr.LocationStationId;
                    productViewModel.ProductPieceTypes = pr.ProductPieceTypes;
                    productViewModel.ProductCutTypes = pr.ProductCutTypes;
                    productViewModel.ProductPieceFormats = pr.ProductPieceFormats;
                    productViewModel.ProductSupplyVoltages = pr.ProductSupplyVoltages;
                    productViewModel.StationsList = pr.StationsList;

                    // Tengo que mapear las RelProviderProduct ignorando los Product
                    if (pr.RelProviderProducts.Count > 0)
                    {
                        List<RelProviderProductViewModel> relProviderProductViewModels = new List<RelProviderProductViewModel>();
                        foreach (var rpp in pr.RelProviderProducts)
                        {
                            RelProviderProductViewModel relProviderProductViewModel = new RelProviderProductViewModel();
                            relProviderProductViewModel.Id = rpp.Id;
                            relProviderProductViewModel.ProviderId = rpp.ProviderId;
                            relProviderProductViewModel.Provider = rpp.Provider;
                            relProviderProductViewModel.ProviderCode = rpp.ProviderCode;
                            relProviderProductViewModel.UnitMeasureId = rpp.UnitMeasureId;
                            relProviderProductViewModel.UnitMeasure = rpp.UnitMeasure;
                            relProviderProductViewModel.Width = rpp.Width;
                            relProviderProductViewModel.WidthUnitMeasure = rpp.WidthUnitMeasure;
                            relProviderProductViewModel.WidthUnitMeasureId = rpp.WidthUnitMeasureId;
                            relProviderProductViewModel.Lenght = rpp.Lenght;
                            relProviderProductViewModel.LengthUnitMeasure = rpp.LengthUnitMeasure;
                            relProviderProductViewModel.LengthUnitMeasureId = rpp.LengthUnitMeasureId;
                            relProviderProductViewModel.Weight = rpp.Weight;
                            relProviderProductViewModel.WeightUnitMeasure = rpp.WeightUnitMeasure;
                            relProviderProductViewModel.WeightUnitMeasureId = rpp.WeightUnitMeasureId;
                            relProviderProductViewModel.PresentationQuantity = rpp.PresentationQuantity;
                            relProviderProductViewModel.PresentationUnitMeasure = rpp.PresentationUnitMeasure;
                            relProviderProductViewModel.PresentationUnitMeasureId = rpp.PresentationUnitMeasureId;
                            relProviderProductViewModel.Brand = rpp.Brand;
                            //relProviderProductViewModel.PresentationMeasure = rpp.PresentationMeasure;
                            relProviderProductViewModel.DollarPrice = rpp.DollarPrice;
                            relProviderProductViewModel.PesosPrice = rpp.PesosPrice;
                            relProviderProductViewModel.ConcurrencyToken = rpp.ConcurrencyToken;

                            relProviderProductViewModels.Add(relProviderProductViewModel);
                        }
                        productViewModel.RelProviderProducts = relProviderProductViewModels;
                    }
                    else
                    {
                        productViewModel.RelProviderProducts = pr.RelProviderProducts;
                    }

                    productViewModel.RelProductStations = pr.RelProductStations;
                    productViewModel.AccessoryProducts = pr.AccessoryProducts;
                    productViewModel.AccessoryProductsList = pr.AccessoryProductsList;
                    productViewModel.Archives = pr.Archives;
                    productViewModel.ArchiveTypesList = pr.ArchiveTypesList;
                    productViewModel.UnitsMeasureList = pr.UnitsMeasureList;
                    productViewModel.UnitMeasure = pr.UnitMeasure;
                    productViewModel.UnitMeasureId = pr.UnitMeasureId;
                    productViewModel.SubCategory = pr.SubCategory;
                    productViewModel.SubCategoryId = pr.SubCategoryId;

                    // Tengo que mapear las Structures ignorando los Product
                    if (pr.Structures.Count > 0)
                    {
                        List<StructureViewModel> structureViewModels = new List<StructureViewModel>();
                        foreach (var st in pr.Structures)
                        {
                            StructureViewModel structureViewModel = new StructureViewModel();
                            structureViewModel.Id = st.Id;
                            structureViewModel.IsBase = st.IsBase;
                            structureViewModel.IsStandard = st.IsStandard;
                            structureViewModel.Description = st.Description;
                            structureViewModel.ConcurrencyToken = st.ConcurrencyToken;
                            structureViewModel.LastVersion = st.LastVersion;
                            structureViewModel.LastVersionId = st.LastVersionId;
                            structureViewModel.ProductId = st.ProductId;
                            structureViewModel.StructureItems = st.StructureItems;
                            structureViewModel.ProductsList = st.ProductsList;

                            structureViewModels.Add(structureViewModel);
                        }
                        productViewModel.Structures = structureViewModels;
                    }
                    else
                    {
                        productViewModel.Structures = pr.Structures;
                    }

                    productViewModel.IVAType = pr.IVAType;
                    productViewModel.IVATypeId = pr.IVATypeId;
                    productViewModel.OperationId = pr.OperationId;
                    productViewModel.Operation = pr.Operation;
                    productViewModel.StockWidth = pr.StockWidth;
                    productViewModel.StockLength = pr.StockLength;
                    productViewModel.StockHeight = pr.StockHeight;
                    productViewModel.StockWeight = pr.StockWeight;
                    productViewModel.StockUnitMeasureId = pr.StockUnitMeasureId;
                    productViewModel.StockUnitMeasure = pr.StockUnitMeasure;

                    productsViewModels.Add(productViewModel);
                }
                return productsViewModels;
            }
            else
            {
                return productsViewModels;
            }
        }

        public async Task<IActionResult> LoadAllComponents_0(int? categoryId, bool? Smak, DatatableParamsViewModel datatableParams,
            string sSearch_2, string sSearch_3, string sSearch_4, string sSearch_5, string sSearch_6, string sSearch_7, string sSearch_8,
            string sSearch_9, string sSearch_11)
        {
            var response = await _viewManager.FindBySpecification(new ProductByCategoryAndInHouseSpecification(null, Smak));
            if (response.Succeeded)
            {
                try
                {
                    var viewModel = _mapper.Map<List<ProductViewModel>>(response.Data);

                    IEnumerable<ProductViewModel> products = circularReferenceWithProduct(viewModel);

                    if (!string.IsNullOrEmpty(sSearch_2))
                    {
                        products = products.Where(x => x.Code.ToLower().Contains(sSearch_2.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_3))
                    {
                        products = products.Where(x => x.Description.ToLower().Contains(sSearch_3.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_4))
                    {
                        products = products.Where(x => x.SubCategory != null && x.SubCategory.Description.ToLower().Contains(sSearch_4.ToLower())).ToList();
                    }

                    if (!string.IsNullOrEmpty(sSearch_5))
                    {
                        products = products.Where(x => x.ProductFeature != null && x.ProductFeature.RawMaterialCode != null && x.ProductFeature.RawMaterialCode.ToLower().Contains(sSearch_5.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_6))
                    {
                        products = products.Where(x => x.ProductFeature != null && x.ProductFeature.RawMaterialDescription != null && x.ProductFeature.RawMaterialDescription.ToLower().Contains(sSearch_6.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_7))
                    {
                        products = products.Where(x => x.ProductFeature != null && x.ProductFeature.ComponentWidhtPiece != null && x.ProductFeature.ComponentWidhtPiece.ToLower().Contains(sSearch_7.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_8))
                    {
                        products = products.Where(x => x.ProductFeature != null && x.ProductFeature.ComponentLongPiece != null && x.ProductFeature.ComponentLongPiece.ToLower().Contains(sSearch_8.ToLower())).ToList();
                    }

                    if (!string.IsNullOrEmpty(sSearch_9))
                    {
                        products = products.Where(x => x.Roadmap.ToLower().Contains(sSearch_9.ToLower())).ToList();
                    }

                    if (!string.IsNullOrEmpty(sSearch_11))
                    {
                        products = products.Where(x => x.UnitMeasure != null && x.UnitMeasure.Description.ToLower().Contains(sSearch_11.ToLower())).ToList();
                    }

                    var sortColumnIndex = datatableParams.iSortCol_0;
                    var sortDirection = datatableParams.sSortDir_0;

                    // Code
                    if (sortColumnIndex == 2)
                    {
                        products = sortDirection == "asc" ? products.OrderBy(p => p.Code) : products.OrderByDescending(p => p.Code);
                    }
                    // Description
                    else if (sortColumnIndex == 3)
                    {
                        products = sortDirection == "asc" ? products.OrderBy(p => p.Description) : products.OrderByDescending(p => p.Description);
                    }
                    // Family/Heading
                    else if (sortColumnIndex == 4)
                    {
                        products = sortDirection == "asc" ? products.OrderBy(p => p.SubCategory != null ? p.SubCategory.Description.ToString() : null) : products.OrderByDescending(p => p.SubCategory != null ? p.SubCategory.Description.ToString() : null);
                    }
                    // Raw material code
                    else if (sortColumnIndex == 5)
                    {
                        products = sortDirection == "asc" ? products.OrderBy(p => p.ProductFeature != null ? p.ProductFeature.RawMaterialCode.ToString() : null) : products.OrderByDescending(p => p.ProductFeature != null ? p.ProductFeature.RawMaterialCode.ToString() : null);
                    }
                    // Raw material description
                    else if (sortColumnIndex == 6)
                    {
                        products = sortDirection == "asc" ? products.OrderBy(p => p.ProductFeature != null ? p.ProductFeature.RawMaterialDescription.ToString() : null) : products.OrderByDescending(p => p.ProductFeature != null ? p.ProductFeature.RawMaterialDescription.ToString() : null);
                    }
                    // Width
                    else if (sortColumnIndex == 7)
                    {
                        products = sortDirection == "asc" ? products.OrderBy(p => p.ProductFeature != null ? p.ProductFeature.ComponentWidhtPiece.ToString() : null) : products.OrderByDescending(p => p.ProductFeature != null ? p.ProductFeature.ComponentWidhtPiece.ToString() : null);
                    }
                    // Length
                    else if (sortColumnIndex == 8)
                    {
                        products = sortDirection == "asc" ? products.OrderBy(p => p.ProductFeature != null ? p.ProductFeature.ComponentLongPiece.ToString() : null) : products.OrderByDescending(p => p.ProductFeature != null ? p.ProductFeature.ComponentLongPiece.ToString() : null);
                    }
                    // Road map
                    else if (sortColumnIndex == 9)
                    {
                        products = sortDirection == "asc" ? products.OrderBy(p => p.Roadmap) : products.OrderByDescending(p => p.Roadmap);
                    }
                    // Unit
                    else if (sortColumnIndex == 11)
                    {
                        products = sortDirection == "asc" ? products.OrderBy(p => p.UnitMeasure != null ? p.UnitMeasure.Description.ToString() : null) : products.OrderByDescending(p => p.UnitMeasure != null ? p.UnitMeasure.Description.ToString() : null);
                    }

                    var displayResult = products.Skip(datatableParams.iDisplayStart)
                        .Take(datatableParams.iDisplayLength).ToList();

                    var totalRecords = products.Count();

                    return Json(new
                    {
                        datatableParams.sEcho,
                        iTotalRecords = totalRecords,
                        iTotalDisplayRecords = totalRecords,
                        aaData = displayResult
                    });
                }
                catch (AutoMapperMappingException ex)
                {

                }
                return null;
            }
            else
            {
                return null;
            }
        }

        public async Task<IActionResult> LoadAllComponents_3(int? categoryId, bool? Smak, DatatableParamsViewModel datatableParams,
            string sSearch_2, string sSearch_3, string sSearch_4, string sSearch_5, string sSearch_6, string sSearch_7, string sSearch_8,
            string sSearch_9, string sSearch_11)
        {
            var response = await _viewManager.FindBySpecification(new ProductByCategoryAndInHouseSpecification(categoryId, Smak));
            if (response.Succeeded)
            {
                try
                {
                    var viewModel = _mapper.Map<List<ProductViewModel>>(response.Data);

                    IEnumerable<ProductViewModel> products = circularReferenceWithProduct(viewModel);

                    if (!string.IsNullOrEmpty(sSearch_2))
                    {
                        products = products.Where(x => x.Code.ToLower().Contains(sSearch_2.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_3))
                    {
                        products = products.Where(x => x.Description.ToLower().Contains(sSearch_3.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_4))
                    {
                        products = products.Where(x => x.SubCategory != null && x.SubCategory.Description.ToLower().Contains(sSearch_4.ToLower())).ToList();
                    }

                    if (!string.IsNullOrEmpty(sSearch_5))
                    {
                        products = products.Where(x => x.ProductFeature != null && x.ProductFeature.RawMaterialCode != null && x.ProductFeature.RawMaterialCode.ToLower().Contains(sSearch_5.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_6))
                    {
                        products = products.Where(x => x.ProductFeature != null && x.ProductFeature.RawMaterialDescription != null && x.ProductFeature.RawMaterialDescription.ToLower().Contains(sSearch_6.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_7))
                    {
                        products = products.Where(x => x.ProductFeature != null && x.ProductFeature.ComponentWidhtPiece != null && x.ProductFeature.ComponentWidhtPiece.ToLower().Contains(sSearch_7.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_8))
                    {
                        products = products.Where(x => x.ProductFeature != null && x.ProductFeature.ComponentLongPiece != null && x.ProductFeature.ComponentLongPiece.ToLower().Contains(sSearch_8.ToLower())).ToList();
                    }

                    if (!string.IsNullOrEmpty(sSearch_9))
                    {
                        products = products.Where(x => x.Roadmap.ToLower().Contains(sSearch_9.ToLower())).ToList();
                    }

                    if (!string.IsNullOrEmpty(sSearch_11))
                    {
                        products = products.Where(x => x.UnitMeasure != null && x.UnitMeasure.Description.ToLower().Contains(sSearch_11.ToLower())).ToList();
                    }

                    var sortColumnIndex = datatableParams.iSortCol_0;
                    var sortDirection = datatableParams.sSortDir_0;

                    // Code
                    if (sortColumnIndex == 2)
                    {
                        products = sortDirection == "asc" ? products.OrderBy(p => p.Code) : products.OrderByDescending(p => p.Code);
                    }
                    // Description
                    else if (sortColumnIndex == 3)
                    {
                        products = sortDirection == "asc" ? products.OrderBy(p => p.Description) : products.OrderByDescending(p => p.Description);
                    }
                    // Family/Heading
                    else if (sortColumnIndex == 4)
                    {
                        products = sortDirection == "asc" ? products.OrderBy(p => p.SubCategory != null ? p.SubCategory.Description.ToString() : null) : products.OrderByDescending(p => p.SubCategory != null ? p.SubCategory.Description.ToString() : null);
                    }
                    // Raw material code
                    else if (sortColumnIndex == 5)
                    {
                        products = sortDirection == "asc" ? products.OrderBy(p => p.ProductFeature != null ? p.ProductFeature.RawMaterialCode.ToString() : null) : products.OrderByDescending(p => p.ProductFeature != null ? p.ProductFeature.RawMaterialCode.ToString() : null);
                    }
                    // Raw material description
                    else if (sortColumnIndex == 6)
                    {
                        products = sortDirection == "asc" ? products.OrderBy(p => p.ProductFeature != null ? p.ProductFeature.RawMaterialDescription.ToString() : null) : products.OrderByDescending(p => p.ProductFeature != null ? p.ProductFeature.RawMaterialDescription.ToString() : null);
                    }
                    // Width
                    else if (sortColumnIndex == 7)
                    {
                        products = sortDirection == "asc" ? products.OrderBy(p => p.ProductFeature != null ? p.ProductFeature.ComponentWidhtPiece.ToString() : null) : products.OrderByDescending(p => p.ProductFeature != null ? p.ProductFeature.ComponentWidhtPiece.ToString() : null);
                    }
                    // Length
                    else if (sortColumnIndex == 8)
                    {
                        products = sortDirection == "asc" ? products.OrderBy(p => p.ProductFeature != null ? p.ProductFeature.ComponentLongPiece.ToString() : null) : products.OrderByDescending(p => p.ProductFeature != null ? p.ProductFeature.ComponentLongPiece.ToString() : null);
                    }
                    // Road map
                    else if (sortColumnIndex == 9)
                    {
                        products = sortDirection == "asc" ? products.OrderBy(p => p.Roadmap) : products.OrderByDescending(p => p.Roadmap);
                    }
                    // Unit
                    else if (sortColumnIndex == 11)
                    {
                        products = sortDirection == "asc" ? products.OrderBy(p => p.UnitMeasure != null ? p.UnitMeasure.Description.ToString() : null) : products.OrderByDescending(p => p.UnitMeasure != null ? p.UnitMeasure.Description.ToString() : null);
                    }

                    var displayResult = products.Skip(datatableParams.iDisplayStart)
                        .Take(datatableParams.iDisplayLength).ToList();

                    var totalRecords = products.Count();

                    return Json(new
                    {
                        datatableParams.sEcho,
                        iTotalRecords = totalRecords,
                        iTotalDisplayRecords = totalRecords,
                        aaData = displayResult
                    });
                }
                catch (AutoMapperMappingException ex)
                {

                }
                return null;
            }
            else
            {
                return null;
            }
        }


        public async Task<IActionResult> LoadAllMachines(int? categoryId, bool? Smak, DatatableParamsViewModel datatableParams,
            string sSearch_2, string sSearch_3, string sSearch_4, string sSearch_5, string sSearch_7)
        {
            var response = await _viewManager.FindBySpecification(new ProductByCategoryAndInHouseSpecification(categoryId, Smak));
            if (response.Succeeded)
            {
                try
                {
                    var viewModel = _mapper.Map<List<ProductViewModel>>(response.Data);

                    IEnumerable<ProductViewModel> products = circularReferenceWithProduct(viewModel);

                    if (!string.IsNullOrEmpty(sSearch_2))
                    {
                        products = products.Where(x => x.Code.ToLower().Contains(sSearch_2.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_3))
                    {
                        products = products.Where(x => x.Description.ToLower().Contains(sSearch_3.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_4))
                    {
                        products = products.Where(x => x.SubCategory != null && x.SubCategory.Description.ToLower().Contains(sSearch_4.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_5))
                    {
                        products = products.Where(x => x.Roadmap.ToLower().Contains(sSearch_5.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_7))
                    {
                        products = products.Where(x => x.UnitMeasure != null && x.UnitMeasure.Description.ToLower().Contains(sSearch_7.ToLower())).ToList();
                    }

                    var sortColumnIndex = datatableParams.iSortCol_0;
                    var sortDirection = datatableParams.sSortDir_0;

                    // Code
                    if (sortColumnIndex == 2)
                    {
                        products = sortDirection == "asc" ? products.OrderBy(p => p.Code) : products.OrderByDescending(p => p.Code);
                    }
                    // Description
                    else if (sortColumnIndex == 3)
                    {
                        products = sortDirection == "asc" ? products.OrderBy(p => p.Description) : products.OrderByDescending(p => p.Description);
                    }
                    // Family/Heading
                    else if (sortColumnIndex == 4)
                    {
                        products = sortDirection == "asc" ? products.OrderBy(p => p.SubCategory != null ? p.SubCategory.Description.ToString() : null) : products.OrderByDescending(p => p.SubCategory != null ? p.SubCategory.Description.ToString() : null);
                    }
                    // Road map
                    else if (sortColumnIndex == 5)
                    {
                        products = sortDirection == "asc" ? products.OrderBy(p => p.Roadmap) : products.OrderByDescending(p => p.Roadmap);
                    }
                    // Unit
                    else if (sortColumnIndex == 7)
                    {
                        products = sortDirection == "asc" ? products.OrderBy(p => p.UnitMeasure != null ? p.UnitMeasure.Description.ToString() : null) : products.OrderByDescending(p => p.UnitMeasure != null ? p.UnitMeasure.Description.ToString() : null);
                    }

                    var displayResult = products.Skip(datatableParams.iDisplayStart)
                        .Take(datatableParams.iDisplayLength).ToList();

                    var totalRecords = products.Count();

                    return Json(new
                    {
                        datatableParams.sEcho,
                        iTotalRecords = totalRecords,
                        iTotalDisplayRecords = totalRecords,
                        aaData = displayResult
                    });
                }
                catch (AutoMapperMappingException ex)
                {

                }
                return null;
            }
            else
            {
                return null;
            }
        }

        [Authorize(Policy = Permissions.Products.View)]
        public async Task<IActionResult> OnGetCreateOrEdit(int id = 0)
        {
            try
            {

                if (id == 0)
                {
                    var productViewModel = new ProductViewModel();

                    productViewModel = await this.PopulateLists(productViewModel);

                    //Por defecto que seleccione unidad si existe.
                    var umUnidad = productViewModel.UnitsMeasureList.Where(x => x.Description.ToLower().Equals("unidad")).FirstOrDefault();
                    if (umUnidad != null)
                        productViewModel.UnitMeasureId = umUnidad.Id;


                    return View("_CreateOrEdit", productViewModel);
                }
                else
                {
                    var response = await _viewManager.FindBySpecification(new ProductWithAllSpecification(id));
                    if (response.Succeeded)
                    {
                        var productViewModel = _mapper.Map<ProductViewModel>(response.Data.FirstOrDefault());

                        productViewModel = await this.PopulateLists(productViewModel);

                        //Si no tiene unidad de medida, que seleccione unidad si existe por defecto.
                        if (productViewModel.UnitMeasureId == null)
                        {
                            var umUnidad = productViewModel.UnitsMeasureList.Where(x => x.Description.ToLower().Equals("unidad")).FirstOrDefault();
                            if (umUnidad != null)
                                productViewModel.UnitMeasureId = umUnidad.Id;
                        }

                        return View("_CreateOrEdit", productViewModel);
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(int id, ProductViewModel product)
        {
            try
            {
                //Lo hacemos aca porque adentro del IsValid=false, da error de null
                product.ProductPieceTypes = product.ProductPieceTypes.Where(x => x != null).ToList();
                product.ProductPieceFormats = product.ProductPieceFormats.Where(x => x != null).ToList();
                product.ProductSupplyVoltages = product.ProductSupplyVoltages.Where(x => x != null).ToList();
                product.ProductCutTypes = product.ProductCutTypes.Where(x => x != null).ToList();
                if (ModelState.IsValid)
                {
                    var html = "";
                    bool? productCreated = null;
                    if (id == 0)
                    {
                        await asignDefaultStructureAsync(product);
                        product.SubCategory = null; //La limpio porque sino me creaba vacia.
                        var productDTO = _mapper.Map<ProductDTO>(product);

                        var productsToValidateCode = await _viewManager.FindBySpecification(new ProductsByCodeSpecification(productDTO.Code));
                        if (productsToValidateCode.Succeeded && productsToValidateCode.Data.Count == 0)
                        {
                            var result = await _viewManager.CreateAsync(productDTO);
                            if (result.Succeeded)
                            {
                                productCreated = true;
                                html = _localizer["Product with ID {0} Created.", result.Data];
                            }
                            else
                            {
                                product = await this.PopulateLists(product);
                                html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", product);
                                return new JsonResult(new { isValid = false, html = html });
                            }
                        }
                        else if (productsToValidateCode.Data.Count > 0)
                        {
                            product = await this.PopulateLists(product);
                            _notify.Information(_localizer["A product with this code already exists."]);
                            html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", product);
                            return new JsonResult(new { isValid = false, html = html });
                        }


                    }
                    else
                    {
                        // Tengo que actualizar las unidades de medida de stock cuando se actualiza la unidad de medida del producto
                        product.StockWidthUnitMeasureId = product.UnitMeasureId;
                        product.StockLengthUnitMeasureId = product.UnitMeasureId;
                        product.StockHeightUnitMeasureId = product.UnitMeasureId;
                        product.StockQuantityUnitMeasureId = product.UnitMeasureId;
                        product.ExistenceUnitMeasureId = product.UnitMeasureId;
                        product.MinimumUnitMeasureId = product.UnitMeasureId;
                        product.QuantityToOrderUnitMeasureId = product.UnitMeasureId;

                        var productDTO = _mapper.Map<ProductDTO>(product);

                        var result = await _viewManager.UpdateAsync(productDTO);
                        if (result.Succeeded)
                        {
                            productCreated = false;
                            html = _localizer["Product with ID {0} Update.", result.Data];
                        }
                        else
                        {
                            product = await this.PopulateLists(product);
                            html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", product);
                            return new JsonResult(new { isValid = false, html = html });
                        }



                    }
                    //if (Request.Form.Files.Count > 0)
                    //{
                    //    IFormFile file = Request.Form.Files.FirstOrDefault();
                    //    var image = file.OptimizeImageSize(700, 700);
                    //    await _viewManager.UpdateImageProduct(id, image);
                    //}

                    return new JsonResult(new { isValid = true, html = html, productCreated = productCreated });
                }
                else
                {
                    product = await this.PopulateLists(product);
                    var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", product);
                    return new JsonResult(new { isValid = false, html = html });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                product = await this.PopulateLists(product);
                var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", product);
                return new JsonResult(new { isValid = false, html = html });
            }
        }

        public async Task<ProductViewModel> asignDefaultStructureAsync(ProductViewModel product)
        {
            string[] prefixArray = { "AC", "CA", "CE", "CM", "CN", "CS", "IA", "IS", "MA", "MC", "MI", "MP", "MV" };
            var prefix = new string(product.Code.Where(Char.IsLetter).ToArray());
            if (prefixArray.Any(x => x == prefix))
            {
                StructureViewModel defaultStructure = new StructureViewModel();
                defaultStructure.Description = "Estándar(Por defecto)";
                defaultStructure.IsBase = false;
                defaultStructure.IsStandard = true;
                defaultStructure.ProductId = product.Id;
                await assignVersion(defaultStructure);
                product.Structures.Add(defaultStructure);
            }
            return product;

        }
        public async Task<int> assignVersion(StructureViewModel structure)
        {
            var version = new StructureVersionViewModel();
            version.Comment = "Versionado automatico desde ERP Web";
            if (structure.LastVersion == null)
            {
                version.VersionNumber = 1;
            }
            else
            {
                version.VersionNumber = (int)(structure.LastVersion.VersionNumber + 1);
            }
            var versionresponse = await _structureversionviewManager.CreateAsync(_mapper.Map<StructureVersionDTO>(version));
            var versionid = 0;
            if (versionresponse.Succeeded)
            {
                versionid = versionresponse.Data;
            }
            structure.LastVersionId = versionid;
            return versionid;
        }

        [HttpPost]
        public async Task<JsonResult> OnPostDelete(int id, int categoryId, bool producedBySmak)
        {
            var deleteCommand = await _viewManager.DeleteAsync(id);
            if (deleteCommand.Succeeded)
            {
                _notify.Information(_localizer["Product with ID {0} Deleted.", id]);

                switch (categoryId)
                {
                    case 0:
                        var response_0 = await _viewManager.FindBySpecification(new ProductByCategoryAndInHouseSpecification(null, producedBySmak));
                        if (response_0 != null)
                        {
                            if (response_0.Succeeded)
                            {
                                var viewModel = _mapper.Map<List<ProductViewModel>>(response_0.Data);
                                return new JsonResult(new { isValid = true, dataTable = "productTableComponents_0" });
                            }
                            else
                            {
                                _notify.Error(response_0.Message);
                                return new JsonResult(new { isValid = false, dataTable = "" });
                            }
                        }
                        else
                        {
                            _notify.Error(deleteCommand.Message);
                            return null;
                        }
                    case 3:
                        var response_3 = await _viewManager.FindBySpecification(new ProductByCategoryAndInHouseSpecification(categoryId, producedBySmak));
                        if (response_3 != null)
                        {
                            if (response_3.Succeeded)
                            {
                                var viewModel = _mapper.Map<List<ProductViewModel>>(response_3.Data);
                                return new JsonResult(new { isValid = true, dataTable = "productTableComponents_3" });
                            }
                            else
                            {
                                _notify.Error(response_3.Message);
                                return new JsonResult(new { isValid = false, dataTable = "" });
                            }
                        }
                        else
                        {
                            _notify.Error(deleteCommand.Message);
                            return null;
                        }
                    default:
                        var response_default = await _viewManager.FindBySpecification(new ProductByCategoryAndInHouseSpecification(categoryId, producedBySmak));
                        if (response_default != null)
                        {
                            if (response_default.Succeeded)
                            {
                                var viewModel = _mapper.Map<List<ProductViewModel>>(response_default.Data);
                                return new JsonResult(new { isValid = true, dataTable = "productTableMachines" });
                            }
                            else
                            {
                                _notify.Error(response_default.Message);
                                return new JsonResult(new { isValid = false, dataTable = "" });
                            }
                        }
                        else
                        {
                            _notify.Error(deleteCommand.Message);
                            return null;
                        }
                }
            }
            else
            {
                _notify.Error(deleteCommand.Message);
                return null;
            }
        }

        public async Task<JsonResult> GetProvidersByProduct(int id = 0)
        {
            try
            {
                var viewModel = new ProductProvidersViewModel();
                var responseproduct = await _viewManager.FindBySpecification(new ProductWithProductFeatureSpecification(id));
                var responseproviders = await _viewManagerRelProviderProduct.FindBySpecification(new RelProviderProductGetProvidersByProductIdSpecification(id));
                if (responseproviders.Succeeded && responseproduct.Succeeded)
                {
                    viewModel.Product = _mapper.Map<ProductViewModel>(responseproduct.Data[0]);
                    viewModel.Providers = _mapper.Map<List<RelProviderProductViewModel>>(responseproviders.Data);
                    var codeAndDescription = viewModel.Product.CodeAndDescription.ToString();
                    var html = await _viewRenderer.RenderViewToStringAsync("Partials/_ProductProviders", viewModel);
                    return new JsonResult(new { isValid = true, html, codeAndDescription });
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

        public async Task<JsonResult> GetFilesByProduct(int id = 0)
        {
            try
            {
                IList<ArchiveViewModel> viewModel = new List<ArchiveViewModel>();
                var responseproduct = await _viewManager.FindBySpecification(new ProductArchivesSpecification(id));
                if (responseproduct.Succeeded)
                {
                    ProductDTO product = responseproduct.Data.FirstOrDefault();
                    viewModel = _mapper.Map<IList<ArchiveViewModel>>(product.Archives);
                    var html = await _viewRenderer.RenderViewToStringAsync("Partials/_FilesTab", viewModel);
                    return new JsonResult(new { isValid = true, html });
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

        [HttpPost]
        public async Task<JsonResult> SaveFilesByProduct(IList<ArchiveViewModel> archiveViewModels)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Tengo que ver los archivos que tenga cargados el producto.
                    var getArchivesByProductId = await _viewManager.FindBySpecification(new ProductArchivesSpecification(archiveViewModels.First().ProductId));
                    if (getArchivesByProductId.Succeeded)
                    {
                        var oldArchives = _mapper.Map<List<ArchiveViewModel>>(getArchivesByProductId.Data.First().Archives);

                        // For create
                        foreach (var archiveVM in archiveViewModels.Where(archiveVM => archiveVM.Id == 0))
                        {
                            var archiveDTO = _mapper.Map<ArchiveDTO>(archiveVM);
                            var result = await _archiveViewManager.CreateAsync(archiveDTO);
                        }

                        // For update
                        foreach (var archiveVM in archiveViewModels.Where(archiveVM => archiveVM.Id != 0 && oldArchives.Any(oldArchiveVM => oldArchiveVM.Id == oldArchiveVM.Id)))
                        {
                            var archiveDTO = _mapper.Map<ArchiveDTO>(archiveVM);
                            var result = await _archiveViewManager.UpdateAsync(archiveDTO);
                        }

                        // For delete
                        foreach (var archiveVM in oldArchives.Where(oldArchiveVM => !archiveViewModels.Any(archiveVM => archiveVM.Id != 0 && archiveVM.Id == oldArchiveVM.Id)))
                        {
                            await _archiveViewManager.DeleteAsync(archiveVM.Id);
                        }
                    }

                    _notify.Success(_localizer["Product files have been updated."]);
                    return new JsonResult(new { isValid = true });
                }
                else
                {
                    _notify.Warning(_localizer["Faltan datos."]);
                    var html = await _viewRenderer.RenderViewToStringAsync("_FilesTab", archiveViewModels);
                    return new JsonResult(new { isValid = false, html = html });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                var html = await _viewRenderer.RenderViewToStringAsync("_FilesTab", archiveViewModels);
                return new JsonResult(new { isValid = false, html = html });
            }
        }

        public async Task<JsonResult> loadArchiveTypes_select2()
        {
            try
            {
                IEnumerable<ArchiveTypeViewModel> archiveTypesVMs = new List<ArchiveTypeViewModel>();
                var archiveTypesResponse = await _viewManager.GetArchiveTypes();
                if (archiveTypesResponse.Succeeded)
                {
                    archiveTypesVMs = _mapper.Map<List<ArchiveTypeViewModel>>(archiveTypesResponse.Data);

                    if (archiveTypesVMs.Count() > 0)
                    {
                        var archiveTypes = new SelectList((from at in archiveTypesVMs.ToList() select new { Id = at.Id, Description = at.Description }), "Id", "Description", null);
                        return new JsonResult(new { isValid = true, values = archiveTypes });
                    }
                    else
                    {
                        return new JsonResult(new { isValid = false, values = "" });
                    }
                }
                else
                {
                    return new JsonResult(new { isValid = false, values = "" });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return new JsonResult(new { isValid = false, values = "" });
            }
        }

        public async Task<JsonResult> GetCategoriesForSideBar()
        {
            var getCategoriesResponse = await _viewManager.GetCategories(5);
            if (getCategoriesResponse.Succeeded)
            {
                return new JsonResult(new { isValid = true, categories = getCategoriesResponse.Data });
            }
            else
            {
                return new JsonResult(new { isValid = false });
            }
        }

        public async Task<JsonResult> GetProductIdOfRawMaterialCode(string rawMaterialCode)
        {
            if (rawMaterialCode != null)
            {


                if (rawMaterialCode != "")
                {
                    // Tengo que traerme el id del producto por código de materia prima para preseleccionar la materia prima del producto que se va a editar.
                    var productByRawMaterialCode = await _viewManager.FindBySpecification(new ProductsByCodeSpecification(rawMaterialCode.ToString()));
                    if (productByRawMaterialCode.Succeeded)
                    {
                        var productIdRawMaterialCode = _mapper.Map<ProductViewModel>(productByRawMaterialCode.Data.FirstOrDefault());
                        return new JsonResult(new { isValid = true, productId = productIdRawMaterialCode.Id.ToString() });
                    }
                    else
                    {
                        return new JsonResult(new { isValid = false, productId = "0" });
                    }
                }
                else
                {
                    return new JsonResult(new { isValid = false, productId = "0" });
                }
            }
            else
            {
                return new JsonResult(new { isValid = false, productId = "0" });
            }
        }

        public async Task<ProductViewModel> PopulateLists(ProductViewModel productViewModel)
        {
            if (productViewModel != null)
            {
                var unitsmeasureResponse = await _viewManager.GetUnitsMeasure();
                var categoriesResponse = await _viewManager.GetCategories();
                var subcategoriesResponse = await _viewManager.GetSubCategories();
                var ivatypesResponse = await _viewManager.GetIvaTypes();
                var piecetypesResponse = await _viewManager.GetPieceTypes();
                var pieceformatsResponse = await _viewManager.GetPieceFormats();
                var cuttypesResponse = await _viewManager.GetCutTypes();
                var operationsReponse = await _viewManager.GetOperations();
                var supplyvoltageResponse = await _viewManager.GetSupplyVoltages();
                var stationResponse = await _viewManager.GetStations();
                var accesoryproductsResponse = await _viewManager.GetAccessoryProducts(productViewModel.Id);
                var rawmaterialsResponse = await _viewManager.FindBySpecification(new RawMaterialProductsSpecification(1)); /*Excluimos solamente Máquinas y Accesorios*/

                var archivetypesResponse = await _viewManager.GetArchiveTypes();

                if (unitsmeasureResponse.Succeeded)
                {
                    var unitsMeasureViewModel = _mapper.Map<List<UnitMeasureViewModel>>(unitsmeasureResponse.Data.Where(x => x.IsActive));
                    productViewModel.UnitsMeasureList = unitsMeasureViewModel;//Lo hacemos para no cambiar todo, pero habria q cambiar todos los select list de unit measure
                    productViewModel.UnitsMeasure = new SelectList(unitsMeasureViewModel, nameof(UnitMeasureViewModel.Id), nameof(UnitMeasureViewModel.Description), null, null);

                    // Stock unit
                    productViewModel.StockUnitMeasures = new SelectList(unitsMeasureViewModel, nameof(UnitMeasureViewModel.Id), nameof(UnitMeasureViewModel.Description), null, null);
                }
                if (categoriesResponse.Succeeded)
                {
                    var categoriesViewModel = _mapper.Map<List<CategoryDTO>>(categoriesResponse.Data);
                    productViewModel.Categories = new SelectList(categoriesViewModel, nameof(CategoryViewModel.Id), nameof(CategoryViewModel.Description), null, null);
                }
                if (subcategoriesResponse.Succeeded)
                {
                    var subcategoriesViewModel = _mapper.Map<List<SubCategoryDTO>>(subcategoriesResponse.Data);
                    productViewModel.SubCategories = new SelectList(subcategoriesViewModel, nameof(SubCategoryViewModel.Id), nameof(SubCategoryViewModel.Description), null, null);
                    if (productViewModel.SubCategoryId != 0)
                    {
                        var subCategorySelected = subcategoriesViewModel.Where(x => x.Id == productViewModel.SubCategoryId).FirstOrDefault();
                        productViewModel.SubCategory = _mapper.Map<SubCategoryViewModel>(subCategorySelected);
                    }
                }
                if (ivatypesResponse.Succeeded)
                {
                    var ivatypesViewModel = _mapper.Map<List<IVAConditionDTO>>(ivatypesResponse.Data);
                    productViewModel.IVATypes = new SelectList(ivatypesViewModel, nameof(IVAConditionViewModel.Id), nameof(IVAConditionViewModel.Description), null, null);
                }
                if (piecetypesResponse.Succeeded)
                {
                    var piecetypesViewModel = _mapper.Map<List<PieceTypeDTO>>(piecetypesResponse.Data);
                    productViewModel.PieceTypesList = new SelectList(piecetypesViewModel, nameof(PieceTypeViewModel.Id), nameof(PieceTypeViewModel.Description), null, null);
                    //En el caso de hacerlo con el viewmodel y .Selected. Pero da error en el mapeo a dto.
                    //var allPieceTypes = new List<ProductPieceTypeViewModel>();
                    //foreach (var item in piecetypesViewModel)
                    //{
                    //    ProductPieceTypeViewModel PieceTypeVM;

                    //    PieceTypeVM = new ProductPieceTypeViewModel();
                    //    PieceTypeVM.Description = item.Description;
                    //    PieceTypeVM.PieceTypeId = item.Id;
                    //    if (productViewModel.ProductPieceTypes != null)
                    //    {
                    //        var exist = productViewModel.ProductPieceTypes.Any(x => x.PieceTypeId == item.Id);
                    //        if (exist)
                    //        {
                    //            PieceTypeVM.Selected = exist;
                    //            if (productViewModel.Id != 0)
                    //                PieceTypeVM.Id = productViewModel.ProductPieceTypes.First(x => x.PieceTypeId == item.Id).Id;
                    //        }
                    //    }

                    //    allPieceTypes.Add(PieceTypeVM);
                    //}
                    //productViewModel.ProductPieceTypes = allPieceTypes;

                }
                if (pieceformatsResponse.Succeeded)
                {
                    var pieceformatsViewModel = _mapper.Map<List<PieceFormatDTO>>(pieceformatsResponse.Data);
                    productViewModel.PieceFormatsList = new SelectList(pieceformatsViewModel, nameof(PieceFormatViewModel.Id), nameof(PieceFormatViewModel.Description), null, null);
                }
                if (cuttypesResponse.Succeeded)
                {
                    var cuttypesViewModel = _mapper.Map<List<CutTypeDTO>>(cuttypesResponse.Data);
                    productViewModel.CutTypesList = new SelectList(cuttypesViewModel, nameof(CutTypeViewModel.Id), nameof(CutTypeViewModel.Description), null, null);
                }
                if (operationsReponse.Succeeded)
                {
                    var operationViewModel = _mapper.Map<List<OperationDTO>>(operationsReponse.Data);
                    productViewModel.Operations = new SelectList(operationViewModel, nameof(OperationViewModel.Id), nameof(OperationViewModel.Description), null, null);
                }
                if (supplyvoltageResponse.Succeeded)
                {
                    var supplyvoltageViewModel = _mapper.Map<List<SupplyVoltageDTO>>(supplyvoltageResponse.Data);
                    productViewModel.SupplyVoltagesList = new SelectList(supplyvoltageViewModel, nameof(SupplyVoltageViewModel.Id), nameof(SupplyVoltageViewModel.Description), null, null);
                }
                if (accesoryproductsResponse.Succeeded)
                {
                    var accessoryproductsViewModel = _mapper.Map<List<ProductViewModel>>(accesoryproductsResponse.Data);
                    productViewModel.AccessoryProductsList = accessoryproductsViewModel;
                }
                if (stationResponse.Succeeded)
                {
                    var stationsViewModel = _mapper.Map<List<StationViewModel>>(stationResponse.Data);
                    productViewModel.StationsList = stationsViewModel;
                }
                if (archivetypesResponse.Succeeded)
                {
                    productViewModel.ArchiveTypesList = _mapper.Map<List<ArchiveTypeViewModel>>(archivetypesResponse.Data);

                }
                if (rawmaterialsResponse.Succeeded)
                {
                    var rawmaterialsViewModel = _mapper.Map<List<ProductViewModel>>(rawmaterialsResponse.Data);
                    productViewModel.RawMaterials = new SelectList(rawmaterialsViewModel, nameof(ProductViewModel.Id), nameof(ProductViewModel.CodeAndDescription), null, null);
                }
            }

            return productViewModel;
        }

        #region Commented
        //public async Task<IActionResult> renderFiltered(int? categoryId, bool? Smak)
        //{
        //    switch (categoryId)
        //    {
        //        case 0:
        //            var response = await _viewManager.FindBySpecification(new ProductByCategoryAndInHouseSpecification(null, Smak));
        //            if (response.Succeeded)
        //            {
        //                try
        //                {
        //                    var viewModel = _mapper.Map<List<ProductViewModel>>(response.Data);
        //                    return PartialView("_ViewAllComponents", viewModel);
        //                }
        //                catch (AutoMapperMappingException ex)
        //                {


        //                }
        //            }
        //            break;
        //        case 3:
        //            var responseImported = await _viewManager.FindBySpecification(new ProductByCategoryAndInHouseSpecification(categoryId, Smak));
        //            if (responseImported.Succeeded)
        //            {
        //                try
        //                {
        //                    var viewModel = _mapper.Map<List<ProductViewModel>>(responseImported.Data);
        //                    return PartialView("_ViewAllComponents", viewModel);
        //                }
        //                catch (AutoMapperMappingException ex)
        //                {


        //                }

        //            }
        //            break;
        //        default:
        //            var responseSimplemak = await _viewManager.FindBySpecification(new ProductByCategoryAndInHouseSpecification(categoryId, Smak));
        //            if (responseSimplemak.Succeeded)
        //            {
        //                try
        //                {
        //                    var viewModel = _mapper.Map<List<ProductViewModel>>(responseSimplemak.Data);
        //                    return PartialView("_ViewAllMachines", viewModel);
        //                }
        //                catch (AutoMapperMappingException ex)
        //                {


        //                }

        //            }
        //            break;
        //    }
        //    return null;

        //}

        //public async Task<IActionResult> LoadAll(int? categoryId, bool? Smak)
        //{
        //    return await renderFiltered(categoryId, Smak);

        //}

        //[HttpPost]
        //public async Task<JsonResult> OnPostDelete(int id)
        //{
        //    var deleteCommand = await _viewManager.DeleteAsync(id);
        //    if (deleteCommand.Succeeded)
        //    {
        //        _notify.Information(_localizer["Product with ID {0} Deleted.", id]);
        //        var response = await _viewManager.LoadAll();
        //        if (response.Succeeded)
        //        {
        //            var viewModel = _mapper.Map<List<ProductViewModel>>(response.Data);
        //            var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", viewModel);
        //            return new JsonResult(new { isValid = true, html = html });
        //        }
        //        else
        //        {
        //            _notify.Error(response.Message);
        //            return null;
        //        }
        //    }
        //    else
        //    {
        //        _notify.Error(deleteCommand.Message);
        //        return null;
        //    }
        //}

        //[HttpPost]
        //public async Task<ActionResult> AddStationItem(ProductViewModel productViewModel)
        //{
        //    if (productViewModel.StationListId != null)
        //    {
        //        if (productViewModel.RelProductStations == null)
        //        {
        //            productViewModel.RelProductStations = new List<RelProductStation>();
        //        }

        //        if (productViewModel.RelProductStations == null || !productViewModel.RelProductStations.Any(x => x.StationId == productViewModel.StationListId))
        //        {
        //            var stationadd = _viewManager.GetStations().Result.Data.Where(x => x.Id == productViewModel.StationListId).FirstOrDefault();
        //            productViewModel.RelProductStations.Add(new RelProductStation
        //            {
        //                Station = stationadd,
        //                StationId = stationadd.Id,
        //                Time = productViewModel.StationTime
        //            });
        //        }
        //    }

        //    return PartialView("Partials/_StationItems", productViewModel);
        //}
        #endregion

    }
}