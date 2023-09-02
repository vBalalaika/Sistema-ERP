using AspNetCoreHero.Results;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Application.DTOs.Entities.Sales;
using ERP.Application.Interfaces.ViewManagers.ProductMod;
using ERP.Application.Interfaces.ViewManagers.Purchases.Providers;
using ERP.Application.Interfaces.ViewManagers.Sales;
using ERP.Application.Specification.ProductMod;
using ERP.Application.Specification.ProductMod.CategorySpecifications;
using ERP.Application.Specification.ProductMod.StructureSpecifications;
using ERP.Application.Specification.ProductMod.SubCategorySpecifications;
using ERP.Application.Specification.Sales;
using ERP.Infrastructure.DbContexts;
using ERP.Language;
using ERP.Web.Abstractions;
using ERP.Web.Areas.ProductMod.Models;
using ERP.Web.Areas.Purchases.Models.Providers;
using ERP.Web.Areas.Sales.Models;
using ERP.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ERP.Web.Areas.Sales.Controllers
{
    [Area("Sales")]
    public class PriceController : BaseController<PriceController>
    {
        private readonly IPriceViewManager _priceViewManager;
        private readonly IStringLocalizer<SharedResource> _localizer;

        private readonly IProductViewManager _productViewManager;
        private readonly IStructureViewManager _structureViewManager;
        private readonly ICategoryViewManager _categoryViewManager;
        private readonly ISubCategoryViewManager _subCategoryViewManager;
        private readonly IProviderViewManager _providerViewManager;

        private IdentityContext _identityContext;

        public PriceController(IPriceViewManager priceViewManager, IStringLocalizer<SharedResource> localizer, IProductViewManager productViewManager, IStructureViewManager structureViewManager, ICategoryViewManager categoryViewManager, ISubCategoryViewManager subCategoryViewManager, IProviderViewManager providerViewManager, IdentityContext identityContext)
        {
            _priceViewManager = priceViewManager;
            _localizer = localizer;
            _productViewManager = productViewManager;
            _structureViewManager = structureViewManager;
            _categoryViewManager = categoryViewManager;
            _subCategoryViewManager = subCategoryViewManager;
            _providerViewManager = providerViewManager;
            _identityContext = identityContext;
        }

        public IActionResult Index(bool spares)
        {
            var model = new PriceViewModel();
            if (spares)
            {
                ViewData["PriceTitle"] = _localizer["Spares prices"].ToString();
            }
            else
            {
                ViewData["PriceTitle"] = _localizer["Products prices"].ToString();
            }
            ViewBag.spares = spares;
            return View(model);
        }

        public async Task<IActionResult> LoadPricesTable(DatatableParamsViewModel datatableParams, string sSearch_2, string sSearch_3, string sSearch_4, string sSearch_5, string sSearch_6, string sSearch_7, string sSearch_8, string sSearch_9, string sSearch_10, string sSearch_11, string sSearch_12, string sSearch_13, string sSearch_14, string sSearch_15,
            bool spares, bool checkSimplemak, bool checkImported, bool checkPriceAll, bool checkPriceArg, bool checkPriceMx)
        {
            var response = await _priceViewManager.FindBySpecification(new PriceSpecification());
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<PriceViewModel>>(response.Data);

                //// Set user which create price register
                //var usersList = _identityContext.Users.ToList();

                //foreach (var priceVM in viewModel)
                //{
                //    // User
                //    foreach (var user in usersList)
                //    {
                //        if (priceVM.CreatedBy == user.Id)
                //        {
                //            priceVM.CreatedBy = user.FirstName.ToString() + ", " + user.LastName.ToString();
                //        }
                //    }
                //}

                IEnumerable<PriceViewModel> prices = viewModel;

                // Hay un ciclo en ProductViewModel -> RelProviderProduct -> Product
                for (int i = prices.Count() - 1; i >= 0; i--)
                {
                    for (int x = prices.ElementAt(i).ProductViewModel.RelProviderProducts.Count() - 1; x >= 0; x--)
                    {
                        prices.ElementAt(i).ProductViewModel.RelProviderProducts.ElementAt(x).Product = null;
                    }
                }

                if (prices.Count() > 0)
                {
                    if (checkPriceAll)
                    {
                        prices = prices.GroupBy(p => new { p.ProductId, p.StructureId }).Select(p => p.OrderByDescending(p => p.PriceAllDate).First());
                    }
                    else if (checkPriceArg)
                    {
                        prices = prices.GroupBy(p => new { p.ProductId, p.StructureId }).Select(p => p.OrderByDescending(p => p.PriceArgDate).First());
                    }
                    else if (checkPriceMx)
                    {
                        prices = prices.GroupBy(p => new { p.ProductId, p.StructureId }).Select(p => p.OrderByDescending(p => p.PriceMxDate).First());
                    }
                    else
                    {
                        prices = prices.GroupBy(p => new { p.ProductId, p.StructureId }).Select(p => p.OrderByDescending(p => p.PriceAllDate).First());
                    }
                }

                if (spares)
                {
                    // Filtro por repuestos -> CategoryId = 2, CategoryId = 3 y CategoryId = 4
                    prices = prices.Where(p => p.ProductViewModel.SubCategory.CategoryId == 2 || p.ProductViewModel.SubCategory.CategoryId == 3 || p.ProductViewModel.SubCategory.CategoryId == 4).ToList();
                }
                else
                {
                    // Filtro por maquinas y accesorios -> CategoryId = 1
                    prices = prices.Where(p => p.ProductViewModel.SubCategory.CategoryId == 1).ToList();
                }

                // Si es Simplemak o Importado
                if (checkSimplemak && checkImported)
                {
                    prices = prices.Where(p => p.ProductViewModel.ProductFeature != null && (p.ProductViewModel.ProductFeature.InHouseManufacturing == true || p.ProductViewModel.ProductFeature.Bought == true)).ToList();
                }
                else
                {
                    if (checkSimplemak)
                    {
                        prices = prices.Where(p => p.ProductViewModel.ProductFeature != null && p.ProductViewModel.ProductFeature.InHouseManufacturing == true).ToList();
                    }
                    else
                    {
                        prices = prices.Where(p => p.ProductViewModel.ProductFeature != null && p.ProductViewModel.ProductFeature.Bought == true).ToList();
                    }
                }

                // Familia/Rubro de producto
                if (!string.IsNullOrEmpty(sSearch_2))
                {
                    prices = prices.Where(p => p.ProductViewModel != null && p.ProductViewModel.SubCategory != null && p.ProductViewModel.SubCategory.Description.ToString().ToLower().Contains(sSearch_2.ToLower())).ToList();
                }
                // Código de producto
                if (!string.IsNullOrEmpty(sSearch_3))
                {
                    prices = prices.Where(p => p.ProductViewModel != null && p.ProductViewModel.Code.ToString().ToLower().Contains(sSearch_3.ToLower())).ToList();
                }
                // Descripcion de producto
                if (!string.IsNullOrEmpty(sSearch_4))
                {
                    prices = prices.Where(p => p.ProductViewModel != null && p.ProductViewModel.Description.ToString().ToLower().Contains(sSearch_4.ToLower())).ToList();
                }
                // Configuración de producto
                if (!string.IsNullOrEmpty(sSearch_5))
                {
                    prices = prices.Where(p => p.StructureViewModel != null && p.StructureViewModel.Description.ToString().ToLower().Contains(sSearch_5.ToLower())).ToList();
                }
                // Unidad de producto
                if (!string.IsNullOrEmpty(sSearch_6))
                {
                    prices = prices.Where(p => p.ProductViewModel.UnitMeasure != null && p.ProductViewModel.UnitMeasure.Description.ToString().ToLower().Contains(sSearch_6.ToLower())).ToList();
                }
                if (!spares)
                {
                    // Price Arg
                    if (!string.IsNullOrEmpty(sSearch_7))
                    {
                        prices = prices.Where(p => Math.Round(p.PriceArg, 2).ToString().Replace(".", ",").Contains(sSearch_7)).ToList();
                    }
                    // Fecha
                    if (!string.IsNullOrEmpty(sSearch_8))
                    {
                        prices = prices.Where(p => p.PriceArgDate.Value.ToString("dd/MM/yyyy").Contains(sSearch_8)).ToList();
                    }
                    // Observaciones
                    if (!string.IsNullOrEmpty(sSearch_9))
                    {
                        prices = prices.Where(p => p.PriceArgObservations != null && p.PriceArgObservations.ToString().ToLower().Contains(sSearch_9.ToLower())).ToList();
                    }

                    // Price All
                    if (!string.IsNullOrEmpty(sSearch_10))
                    {
                        prices = prices.Where(p => Math.Round(p.PriceAll, 2).ToString().Replace(".", ",").Contains(sSearch_10)).ToList();
                    }
                    // Fecha
                    if (!string.IsNullOrEmpty(sSearch_11))
                    {
                        prices = prices.Where(p => p.PriceAllDate.Value.ToString("dd/MM/yyyy").Contains(sSearch_11)).ToList();
                    }
                    // Observaciones
                    if (!string.IsNullOrEmpty(sSearch_12))
                    {
                        prices = prices.Where(p => p.PriceAllObservations != null && p.PriceAllObservations.ToString().ToLower().Contains(sSearch_12.ToLower())).ToList();
                    }

                    // Price Mx
                    if (!string.IsNullOrEmpty(sSearch_13))
                    {
                        prices = prices.Where(p => Math.Round(p.PriceMx, 2).ToString().Replace(".", ",").Contains(sSearch_13)).ToList();
                    }
                    // Fecha
                    if (!string.IsNullOrEmpty(sSearch_14))
                    {
                        prices = prices.Where(p => p.PriceMxDate.Value.ToString("dd/MM/yyyy").Contains(sSearch_14)).ToList();
                    }
                    // Observaciones
                    if (!string.IsNullOrEmpty(sSearch_15))
                    {
                        prices = prices.Where(p => p.PriceMxObservations != null && p.PriceMxObservations.ToString().ToLower().Contains(sSearch_15.ToLower())).ToList();
                    }

                    // User
                    //if (!string.IsNullOrEmpty(sSearch_15))
                    //{
                    //    prices = prices.Where(p => p.CreatedBy != null && p.CreatedBy.ToString().ToLower().Contains(sSearch_15.ToLower())).ToList();
                    //}
                }
                else
                {
                    // Price All
                    if (!string.IsNullOrEmpty(sSearch_7))
                    {
                        prices = prices.Where(p => Math.Round(p.PriceAll, 2).ToString().Replace(".", ",").Contains(sSearch_7)).ToList();
                    }
                    // Fecha
                    if (!string.IsNullOrEmpty(sSearch_8))
                    {
                        prices = prices.Where(p => p.PriceAllDate.Value.ToString("dd/MM/yyyy").Contains(sSearch_8)).ToList();
                    }
                    // Observaciones
                    if (!string.IsNullOrEmpty(sSearch_9))
                    {
                        prices = prices.Where(p => p.PriceAllObservations != null && p.PriceAllObservations.ToString().ToLower().Contains(sSearch_9.ToLower())).ToList();
                    }

                    // User
                    //if (!string.IsNullOrEmpty(sSearch_9))
                    //{
                    //    prices = prices.Where(p => p.CreatedBy != null && p.CreatedBy.ToString().ToLower().Contains(sSearch_9.ToLower())).ToList();
                    //}
                }

                var sortColumnIndex = datatableParams.iSortCol_0;
                var sortDirection = datatableParams.sSortDir_0;

                if (sortColumnIndex == 2)
                {
                    prices = sortDirection == "asc" ? prices.OrderBy(p => p.ProductViewModel != null ? (p.ProductViewModel.SubCategory != null ? p.ProductViewModel.SubCategory.Description.ToString() : null) : null) : prices.OrderByDescending(p => p.ProductViewModel != null ? (p.ProductViewModel.SubCategory != null ? p.ProductViewModel.SubCategory.Description.ToString() : null) : null);
                }
                else if (sortColumnIndex == 3)
                {
                    prices = sortDirection == "asc" ? prices.OrderBy(p => p.ProductViewModel != null ? p.ProductViewModel.Code.ToString() : null) : prices.OrderByDescending(p => p.ProductViewModel != null ? p.ProductViewModel.Code.ToString() : null);
                }
                else if (sortColumnIndex == 4)
                {
                    prices = sortDirection == "asc" ? prices.OrderBy(p => p.ProductViewModel != null ? p.ProductViewModel.Description.ToString() : null) : prices.OrderByDescending(p => p.ProductViewModel != null ? p.ProductViewModel.Description.ToString() : null);
                }
                else if (sortColumnIndex == 5)
                {
                    prices = sortDirection == "asc" ? prices.OrderBy(p => p.StructureViewModel != null ? p.StructureViewModel.Description.ToString() : null) : prices.OrderByDescending(p => p.StructureViewModel != null ? p.StructureViewModel.Description.ToString() : null);
                }
                if (sortColumnIndex == 6)
                {
                    prices = sortDirection == "asc" ? prices.OrderBy(p => p.ProductViewModel != null ? p.ProductViewModel.UnitMeasure != null ? p.ProductViewModel.UnitMeasure.Description.ToString() : null : null) : prices.OrderByDescending(p => p.ProductViewModel != null ? p.ProductViewModel.UnitMeasure != null ? p.ProductViewModel.UnitMeasure.Description.ToString() : null : null);
                }

                if (!spares)
                {

                    if (sortColumnIndex == 7)
                    {
                        prices = sortDirection == "asc" ? prices.OrderBy(p => p.PriceArg) : prices.OrderByDescending(p => p.PriceArg);
                    }
                    else if (sortColumnIndex == 8)
                    {
                        prices = sortDirection == "asc" ? prices.OrderBy(p => p.PriceArgDate) : prices.OrderByDescending(p => p.PriceArgDate);
                    }
                    else if (sortColumnIndex == 10)
                    {
                        prices = sortDirection == "asc" ? prices.OrderBy(p => p.PriceAll) : prices.OrderByDescending(p => p.PriceAll);
                    }
                    else if (sortColumnIndex == 11)
                    {
                        prices = sortDirection == "asc" ? prices.OrderBy(p => p.PriceAllDate) : prices.OrderByDescending(p => p.PriceAllDate);
                    }
                    else if (sortColumnIndex == 13)
                    {
                        prices = sortDirection == "asc" ? prices.OrderBy(p => p.PriceMx) : prices.OrderByDescending(p => p.PriceMx);
                    }
                    else if (sortColumnIndex == 14)
                    {
                        prices = sortDirection == "asc" ? prices.OrderBy(p => p.PriceMxDate) : prices.OrderByDescending(p => p.PriceMxDate);
                    }
                    //else if (sortColumnIndex == 15)
                    //{
                    //    prices = sortDirection == "asc" ? prices.OrderBy(p => p.CreatedBy) : prices.OrderByDescending(p => p.CreatedBy);
                    //}
                }
                else
                {
                    if (sortColumnIndex == 7)
                    {
                        prices = sortDirection == "asc" ? prices.OrderBy(p => p.PriceAll) : prices.OrderByDescending(p => p.PriceAll);
                    }
                    else if (sortColumnIndex == 8)
                    {
                        prices = sortDirection == "asc" ? prices.OrderBy(p => p.PriceAllDate) : prices.OrderByDescending(p => p.PriceAllDate);
                    }
                    //else if (sortColumnIndex == 9)
                    //{
                    //    prices = sortDirection == "asc" ? prices.OrderBy(p => p.CreatedBy) : prices.OrderByDescending(p => p.CreatedBy);
                    //}
                }

                var displayResult = prices.Skip(datatableParams.iDisplayStart)
                    .Take(datatableParams.iDisplayLength).ToList();

                var totalRecords = prices.Count();

                return Json(new
                {
                    datatableParams.sEcho,
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

        public IActionResult _PriceHistoryByProduct(int productId, string codeAndDescription, int structureId, string structureDescription, bool spares)
        {
            ViewBag.productId = productId;
            ViewBag.structureId = structureId;
            ViewBag.spares = spares;
            ViewData["codeAndDescription"] = codeAndDescription;
            ViewData["structureDescription"] = structureDescription;
            ViewData["PriceHistoryTitle"] = _localizer["Historical product price"].ToString();
            return View();
        }

        public async Task<IActionResult> LoadPriceHistoryTable(DatatableParamsViewModel datatableParams, string sSearch_1, string sSearch_2, string sSearch_3, string sSearch_4, string sSearch_5, int productId, int structureId, bool spares)
        {
            var response = await _priceViewManager.FindBySpecification(new PriceSpecification(productId, structureId));
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<PriceViewModel>>(response.Data);

                // Set user which create price register
                var usersList = _identityContext.Users.ToList();

                foreach (var priceVM in viewModel)
                {
                    // User
                    foreach (var user in usersList)
                    {
                        if (priceVM.CreatedBy == user.Id)
                        {
                            priceVM.CreatedBy = user.FirstName.ToString() + ", " + user.LastName.ToString();
                        }

                        if (priceVM.LastModifiedBy == user.Id)
                        {
                            priceVM.LastModifiedBy = user.FirstName.ToString() + ", " + user.LastName.ToString();
                        }
                    }
                }

                IEnumerable<PriceViewModel> prices = viewModel;

                if (spares)
                {
                    // Fecha
                    if (!string.IsNullOrEmpty(sSearch_1))
                    {
                        prices = prices.Where(p => p.PriceAllDate.Value.ToString("dd/MM/yyyy").Contains(sSearch_1)).ToList();
                    }
                    // Price All
                    if (!string.IsNullOrEmpty(sSearch_2))
                    {
                        prices = prices.Where(p => Math.Round(p.PriceAll, 2).ToString().Replace(".", ",").Contains(sSearch_2)).ToList();
                    }
                    // User
                    if (!string.IsNullOrEmpty(sSearch_3))
                    {
                        prices = prices.Where(p => p.CreatedBy != null ? p.CreatedBy.ToString().ToLower().Contains(sSearch_3.ToLower()) : p.LastModifiedBy.ToString().ToLower().Contains(sSearch_3.ToLower())).ToList();
                    }
                }
                else
                {
                    // Fecha
                    if (!string.IsNullOrEmpty(sSearch_1))
                    {
                        prices = prices.Where(p => p.PriceArgDate.Value.ToString("dd/MM/yyyy").Contains(sSearch_1)).ToList();
                    }
                    // Price Arg
                    if (!string.IsNullOrEmpty(sSearch_2))
                    {
                        prices = prices.Where(p => Math.Round(p.PriceArg, 2).ToString().Replace(".", ",").Contains(sSearch_2)).ToList();
                    }

                    // Price All
                    if (!string.IsNullOrEmpty(sSearch_3))
                    {
                        prices = prices.Where(p => Math.Round(p.PriceAll, 2).ToString().Replace(".", ",").Contains(sSearch_3)).ToList();
                    }
                    // Price Mx
                    if (!string.IsNullOrEmpty(sSearch_4))
                    {
                        prices = prices.Where(p => Math.Round(p.PriceMx, 2).ToString().Replace(".", ",").Contains(sSearch_4)).ToList();
                    }
                    // User
                    if (!string.IsNullOrEmpty(sSearch_5))
                    {
                        prices = prices.Where(p => p.CreatedBy != null ? p.CreatedBy.ToString().ToLower().Contains(sSearch_5.ToLower()) : p.LastModifiedBy.ToString().ToLower().Contains(sSearch_5.ToLower())).ToList();
                    }
                }

                var sortColumnIndex = datatableParams.iSortCol_0;
                var sortDirection = datatableParams.sSortDir_0;

                if (spares)
                {
                    if (sortColumnIndex == 1)
                    {
                        prices = sortDirection == "asc" ? prices.OrderBy(p => p.LastModifiedOn != null ? p.LastModifiedOn : p.CreatedOn) : prices.OrderByDescending(p => p.LastModifiedOn != null ? p.LastModifiedOn : p.CreatedOn);
                    }
                    if (sortColumnIndex == 2)
                    {
                        prices = sortDirection == "asc" ? prices.OrderBy(p => p.PriceAll) : prices.OrderByDescending(p => p.PriceAll);
                    }
                    if (sortColumnIndex == 3)
                    {
                        prices = sortDirection == "asc" ? prices.OrderBy(p => p.CreatedBy != null ? p.CreatedBy : p.LastModifiedBy) : prices.OrderByDescending(p => p.CreatedBy != null ? p.CreatedBy : p.LastModifiedBy);
                    }
                }
                else
                {
                    if (sortColumnIndex == 1)
                    {
                        prices = sortDirection == "asc" ? prices.OrderBy(p => p.LastModifiedOn != null ? p.LastModifiedOn : p.CreatedOn) : prices.OrderByDescending(p => p.LastModifiedOn != null ? p.LastModifiedOn : p.CreatedOn);
                    }
                    if (sortColumnIndex == 2)
                    {
                        prices = sortDirection == "asc" ? prices.OrderBy(p => p.PriceArg) : prices.OrderByDescending(p => p.PriceArg);
                    }
                    if (sortColumnIndex == 3)
                    {
                        prices = sortDirection == "asc" ? prices.OrderBy(p => p.PriceAll) : prices.OrderByDescending(p => p.PriceAll);
                    }
                    if (sortColumnIndex == 4)
                    {
                        prices = sortDirection == "asc" ? prices.OrderBy(p => p.PriceMx) : prices.OrderByDescending(p => p.PriceMx);
                    }
                    if (sortColumnIndex == 5)
                    {
                        prices = sortDirection == "asc" ? prices.OrderBy(p => p.CreatedBy != null ? p.CreatedBy : p.LastModifiedBy) : prices.OrderByDescending(p => p.CreatedBy != null ? p.CreatedBy : p.LastModifiedBy);
                    }
                }

                var displayResult = prices.Skip(datatableParams.iDisplayStart)
                    .Take(datatableParams.iDisplayLength).ToList();

                var totalRecords = prices.Count();

                return Json(new
                {
                    datatableParams.sEcho,
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

        public IActionResult _UpdatePrices(bool spares)
        {
            var pricesViewModel = new List<PriceViewModel>();
            if (spares)
            {
                ViewData["PriceTitle"] = _localizer["Update spares prices"].ToString();
            }
            else
            {
                ViewData["PriceTitle"] = _localizer["Update products prices"].ToString();
            }
            ViewBag.spares = spares;
            return View(pricesViewModel);
        }

        public async Task<IActionResult> LoadUpdatePricesTable(DatatableParamsViewModel datatableParams, string categoryValue, int subCategoryId, int providerId, bool spares, int priceType, int productsSource, int productId = 0, int configurationId = 0)
        {
            var response = await _priceViewManager.FindBySpecification(new PriceSpecification());
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<PriceViewModel>>(response.Data);
                IEnumerable<PriceViewModel> prices = viewModel;

                // Hay un ciclo en ProductViewModel -> RelProviderProduct -> Product
                //for (int i = prices.Count() - 1; i >= 0; i--)
                //{
                //    for (int x = prices.ElementAt(i).ProductViewModel.RelProviderProducts.Count() - 1; x >= 0; x--)
                //    {
                //        prices.ElementAt(i).ProductViewModel.RelProviderProducts.ElementAt(x).Product = null;
                //    }
                //}

                if (productId != 0)
                {
                    prices = prices.Where(p => p.ProductViewModel.Id == productId).ToList();
                }

                if (configurationId != 0)
                {
                    prices = prices.Where(p => p.StructureViewModel != null && p.StructureViewModel.Id == configurationId).ToList();
                }

                // Si productsSource = 0 => Simplemak
                // Si productsSource = 1 => Importados
                if (productsSource == 0)
                {
                    prices = prices.Where(p => !p.ProductViewModel.ProductFeature.Bought).ToList();
                }
                else if (productsSource == 1)
                {
                    prices = prices.Where(p => p.ProductViewModel.ProductFeature.Bought).ToList();
                }

                if (prices.Count() > 0)
                {
                    // Si el valor de priceType es 1 - Lista COMEX.
                    // Si el valor de priceType es 2 - Lista Arg.
                    // Si el valor de priceType es 3 - Lista MX.
                    if (priceType == 1)
                    {
                        prices = prices.GroupBy(p => new { p.ProductId, p.StructureId }).Select(p => p.OrderByDescending(p => p.PriceAllDate).First());
                    }
                    else if (priceType == 2)
                    {
                        prices = prices.GroupBy(p => new { p.ProductId, p.StructureId }).Select(p => p.OrderByDescending(p => p.PriceArgDate).First());
                    }
                    else if (priceType == 3)
                    {
                        prices = prices.GroupBy(p => new { p.ProductId, p.StructureId }).Select(p => p.OrderByDescending(p => p.PriceMxDate).First());
                    }
                }

                if (spares)
                {
                    // Filtro por repuestos -> CategoryId = 2, CategoryId = 3 y CategoryId = 4
                    prices = prices.Where(p => p.ProductViewModel.SubCategory.CategoryId == 2 || p.ProductViewModel.SubCategory.CategoryId == 3 || p.ProductViewModel.SubCategory.CategoryId == 4).ToList();
                }
                else
                {
                    // Filtro por maquinas y accesorios -> CategoryId = 1
                    prices = prices.Where(p => p.ProductViewModel.SubCategory.CategoryId == 1).ToList();
                }

                if (categoryValue == "Maquinas y Accesorios")
                {
                    prices = prices.Where(p => p.ProductViewModel.SubCategory.CategoryId == 1).ToList();
                }
                else if (categoryValue == "Conjuntos")
                {
                    prices = prices.Where(p => p.ProductViewModel.SubCategory.CategoryId == 2).ToList();
                }
                else if (categoryValue == "Piezas Individuales")
                {
                    prices = prices.Where(p => p.ProductViewModel.SubCategory.CategoryId == 3).ToList();
                }
                else if (categoryValue == "Componentes Comprados")
                {
                    prices = prices.Where(p => p.ProductViewModel.SubCategory.CategoryId == 4).ToList();
                }

                if (subCategoryId != 0)
                {
                    prices = prices.Where(p => p.ProductViewModel.SubCategoryId == subCategoryId).ToList();
                }

                if (providerId != 0)
                {
                    HashSet<PriceViewModel> pricesAux = new HashSet<PriceViewModel>();

                    for (int i = prices.Count() - 1; i >= 0; i--)
                    {
                        for (int x = prices.ElementAt(i).ProductViewModel.RelProviderProducts.Count() - 1; x >= 0; x--)
                        {
                            if (prices.ElementAt(i).ProductViewModel.RelProviderProducts.ElementAt(x).ProviderId == providerId)
                            {
                                pricesAux.Add(prices.ElementAt(i));
                            }
                            else
                            {
                                pricesAux.Remove(prices.ElementAt(i));
                            }
                        }
                    }

                    prices = pricesAux;
                }

                var sortColumnIndex = datatableParams.iSortCol_0;
                var sortDirection = datatableParams.sSortDir_0;

                if (sortColumnIndex == 1)
                {
                    prices = sortDirection == "asc" ? prices.OrderBy(p => p.ProductViewModel != null ? (p.ProductViewModel.SubCategory != null ? p.ProductViewModel.SubCategory.Description.ToString() : null) : null) : prices.OrderByDescending(p => p.ProductViewModel != null ? (p.ProductViewModel.SubCategory != null ? p.ProductViewModel.SubCategory.Description.ToString() : null) : null);
                }
                else if (sortColumnIndex == 2)
                {
                    prices = sortDirection == "asc" ? prices.OrderBy(p => p.ProductViewModel != null ? p.ProductViewModel.Code.ToString() : null) : prices.OrderByDescending(p => p.ProductViewModel != null ? p.ProductViewModel.Code.ToString() : null);
                }
                else if (sortColumnIndex == 3)
                {
                    prices = sortDirection == "asc" ? prices.OrderBy(p => p.ProductViewModel != null ? p.ProductViewModel.Description.ToString() : null) : prices.OrderByDescending(p => p.ProductViewModel != null ? p.ProductViewModel.Description.ToString() : null);
                }

                if (spares)
                {
                    if (sortColumnIndex == 4)
                    {
                        prices = sortDirection == "asc" ? prices.OrderBy(p => p.PriceAll) : prices.OrderByDescending(p => p.PriceAll);
                    }
                    else if (sortColumnIndex == 6)
                    {
                        prices = sortDirection == "asc" ? prices.OrderBy(p => p.PriceAllDate) : prices.OrderByDescending(p => p.PriceAllDate);
                    }
                }
                else
                {
                    if (sortColumnIndex == 4)
                    {
                        prices = sortDirection == "asc" ? prices.OrderBy(p => p.PriceArg) : prices.OrderByDescending(p => p.PriceArg);
                    }
                    else if (sortColumnIndex == 6)
                    {
                        prices = sortDirection == "asc" ? prices.OrderBy(p => p.PriceArgDate) : prices.OrderByDescending(p => p.PriceArgDate);
                    }

                    if (sortColumnIndex == 7)
                    {
                        prices = sortDirection == "asc" ? prices.OrderBy(p => p.PriceAll) : prices.OrderByDescending(p => p.PriceAll);
                    }
                    else if (sortColumnIndex == 9)
                    {
                        prices = sortDirection == "asc" ? prices.OrderBy(p => p.PriceAllDate) : prices.OrderByDescending(p => p.PriceAllDate);
                    }

                    if (sortColumnIndex == 10)
                    {
                        prices = sortDirection == "asc" ? prices.OrderBy(p => p.PriceMx) : prices.OrderByDescending(p => p.PriceMx);
                    }
                    else if (sortColumnIndex == 12)
                    {
                        prices = sortDirection == "asc" ? prices.OrderBy(p => p.PriceMxDate) : prices.OrderByDescending(p => p.PriceMxDate);
                    }
                }

                var displayResult = prices.Skip(datatableParams.iDisplayStart)
                                    .Take(prices.Count()).ToList();

                var totalRecords = prices.Count();

                return Json(new
                {
                    datatableParams.sEcho,
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

        public async Task<JsonResult> LoadCategoriesOnSelect2(string q, bool spares)
        {
            var categories = await _categoryViewManager.FindBySpecification(new CategoriesForSparesSpecification(spares));

            List<Select2ViewModel> select2ViewModels = new List<Select2ViewModel>();
            var categoriesViewModel = _mapper.Map<List<CategoryViewModel>>(categories.Data);

            select2ViewModels.Add(new Select2ViewModel() { Id = 0, Text = _localizer["Select category"] });
            foreach (var category in categoriesViewModel)
            {
                select2ViewModels.Add(new Select2ViewModel() { Id = category.Id, Text = category.Description });
            }

            if (!(string.IsNullOrEmpty(q) || string.IsNullOrWhiteSpace(q)))
            {
                select2ViewModels = select2ViewModels.Where(x => x.Text.ToString().ToLower().Contains(q.ToLower())).ToList();
            }
            return new JsonResult(new { categories = select2ViewModels });
        }

        public async Task<JsonResult> LoadSubCategoriesOnSelect2(string q, int categoryId)
        {
            Result<IReadOnlyList<SubCategoryDTO>> subCategories = null;
            if (categoryId != 0)
            {
                subCategories = await _subCategoryViewManager.FindBySpecification(new SubCategoriesByCategoryIdSpecification(categoryId));
            }
            else
            {
                subCategories = await _subCategoryViewManager.LoadAll();
            }

            List<Select2ViewModel> select2ViewModels = new List<Select2ViewModel>();
            var subCategoriesViewModel = _mapper.Map<List<SubCategoryViewModel>>(subCategories.Data);

            select2ViewModels.Add(new Select2ViewModel() { Id = 0, Text = _localizer["Select subcategory"] });
            foreach (var subCategory in subCategoriesViewModel)
            {
                select2ViewModels.Add(new Select2ViewModel() { Id = subCategory.Id, Text = subCategory.Description });
            }

            if (!(string.IsNullOrEmpty(q) || string.IsNullOrWhiteSpace(q)))
            {
                select2ViewModels = select2ViewModels.Where(x => x.Text.ToString().ToLower().Contains(q.ToLower())).ToList();
            }
            return new JsonResult(new { subCategories = select2ViewModels });
        }

        public async Task<JsonResult> LoadProvidersOnSelect2(string q)
        {
            var providers = await _providerViewManager.LoadAll();

            List<Select2ViewModel> select2ViewModels = new List<Select2ViewModel>();
            var providersViewModel = _mapper.Map<List<ProviderViewModel>>(providers.Data);

            select2ViewModels.Add(new Select2ViewModel() { Id = 0, Text = _localizer["Select provider"] });
            foreach (var provider in providersViewModel)
            {
                select2ViewModels.Add(new Select2ViewModel() { Id = provider.Id, Text = provider.BusinessName });
            }

            if (!(string.IsNullOrEmpty(q) || string.IsNullOrWhiteSpace(q)))
            {
                select2ViewModels = select2ViewModels.Where(x => x.Text.ToString().ToLower().Contains(q.ToLower())).ToList();
            }
            return new JsonResult(new { providers = select2ViewModels });
        }

        public async Task<JsonResult> LoadProductsOnSelect2(string q, bool spares)
        {
            var products = await _productViewManager.FindBySpecification(new ProductWithSparesForPricesSpecification(spares));

            if (products.Succeeded)
            {
                List<Select2ViewModel> select2ViewModels = new List<Select2ViewModel>();
                var productsViewModel = _mapper.Map<List<ProductViewModel>>(products.Data);

                select2ViewModels.Add(new Select2ViewModel() { Id = 0, Text = _localizer["Select product"] });
                foreach (var product in productsViewModel)
                {
                    select2ViewModels.Add(new Select2ViewModel() { Id = product.Id, Text = product.CodeAndDescription });
                }

                if (!(string.IsNullOrEmpty(q) || string.IsNullOrWhiteSpace(q)))
                {
                    select2ViewModels = select2ViewModels.Where(x => x.Text.ToString().ToLower().Contains(q.ToLower())).ToList();
                }
                return new JsonResult(new { products = select2ViewModels });
            }
            else
            {
                return null;
            }
        }

        public async Task<JsonResult> LoadProductsSelect2(string search, bool spares)
        {
            var productsResponse = await _productViewManager.FindBySpecification(new ProductWithAllSpecification(search));
            if (productsResponse.Succeeded)
            {
                List<Select2ViewModel> mapSelect2Data = new List<Select2ViewModel>();
                List<ProductViewModel> productsViewModel = new List<ProductViewModel>();
                if (spares)
                {
                    productsViewModel = _mapper.Map<List<ProductViewModel>>(productsResponse.Data.Where(pr => pr.SubCategory.CategoryId == 2 || pr.SubCategory.CategoryId == 3 || pr.SubCategory.CategoryId == 4).ToList());
                }
                else
                {
                    productsViewModel = _mapper.Map<List<ProductViewModel>>(productsResponse.Data.Where(pr => pr.SubCategory.CategoryId == 1).ToList());
                }

                mapSelect2Data.Add(new Select2ViewModel() { Id = 0, Text = _localizer["Select product"] });
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

        public async Task<JsonResult> LoadConfigurationsByProductIdOnSelect2(string q, int productId)
        {
            //if (productId != 0)
            //{
            var configurations = await _structureViewManager.FindBySpecification(new StructureByProductIdSpecification(productId));

            if (configurations.Succeeded)
            {
                List<Select2ViewModel> select2ViewModels = new List<Select2ViewModel>();
                var structuresViewModel = _mapper.Map<List<StructureViewModel>>(configurations.Data);

                select2ViewModels.Add(new Select2ViewModel() { Id = 0, Text = _localizer["Select configuration"] });
                foreach (var structure in structuresViewModel)
                {
                    select2ViewModels.Add(new Select2ViewModel() { Id = structure.Id, Text = structure.Description });
                }

                if (!(string.IsNullOrEmpty(q) || string.IsNullOrWhiteSpace(q)))
                {
                    select2ViewModels = select2ViewModels.Where(x => x.Text.ToString().ToLower().Contains(q.ToLower())).ToList();
                }
                return new JsonResult(new { configurations = select2ViewModels });
            }
            else
            {
                return null;
            }
            //}
            //else
            //{
            //    _notify.Warning(_localizer["You must select product."]);
            //    return null;
            //}
        }

        public async Task<JsonResult> UpdatePrices(string pricesViewModel, bool spares, int priceType)
        {
            try
            {
                if (pricesViewModel != "")
                {
                    List<PriceViewModel> pricesVM = JsonSerializer.Deserialize<List<PriceViewModel>>(pricesViewModel);

                    var resultsSucceeded = new List<bool>();
                    var isSucceeded = true;
                    foreach (var price in pricesVM)
                    {
                        var priceVM = new PriceViewModel();

                        priceVM.ProductId = price.ProductId;
                        priceVM.StructureId = price.StructureId;

                        priceVM.PriceAll = price.PriceAll;
                        priceVM.PriceArg = price.PriceArg;
                        priceVM.PriceMx = price.PriceMx;

                        // Si el valor de priceType es 1 - Lista COMEX.
                        // Si el valor de priceType es 2 - Lista Arg.
                        // Si el valor de priceType es 3 - Lista MX.

                        // Get last date for PriceAllDate, PriceArgDate and PriceMxDate
                        var lastDateResponse = await _priceViewManager.FindBySpecification(new GetLastDateForPricesSpecification(priceVM.ProductId, priceVM.StructureId));

                        if (priceType == 1)
                        {
                            priceVM.PriceAllDate = DateTime.Today;
                            priceVM.PriceArgDate = lastDateResponse.Data.Max(p => p.PriceArgDate).Value;
                            priceVM.PriceMxDate = lastDateResponse.Data.Max(p => p.PriceMxDate).Value;
                        }
                        else if (priceType == 2)
                        {
                            priceVM.PriceArgDate = DateTime.Today;
                            priceVM.PriceAllDate = lastDateResponse.Data.Max(p => p.PriceAllDate).Value;
                            priceVM.PriceMxDate = lastDateResponse.Data.Max(p => p.PriceMxDate).Value;
                        }
                        else if (priceType == 3)
                        {
                            priceVM.PriceMxDate = DateTime.Today;
                            priceVM.PriceAllDate = lastDateResponse.Data.Max(p => p.PriceAllDate).Value;
                            priceVM.PriceArgDate = lastDateResponse.Data.Max(p => p.PriceArgDate).Value;
                        }

                        var priceDTO = _mapper.Map<PriceDTO>(priceVM);

                        var result = await _priceViewManager.CreateAsync(priceDTO);
                        if (result.Succeeded)
                        {
                            resultsSucceeded.Add(true);
                        }
                        else
                        {
                            resultsSucceeded.Add(false);
                        }
                    }

                    foreach (bool resultSucceeded in resultsSucceeded)
                    {
                        if (!resultSucceeded)
                        {
                            isSucceeded = false;
                        }
                    }

                    if (isSucceeded)
                    {
                        _notify.Success(_localizer["Updated prices."]);
                        return new JsonResult(new { isValid = true, spares = spares });
                    }
                    else
                    {
                        _notify.Error(_localizer["An error occurred while updating the prices."]);
                        return new JsonResult(new { isValid = false, spares = spares });
                    }
                }
                else
                {
                    _notify.Error(_localizer["An error occurred while updating the prices."]);
                    return new JsonResult(new { isValid = false, spares = spares });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return new JsonResult(new { isValid = false, spares = spares });
            }
        }

        public async Task<JsonResult> OnGetCreateOrEdit(bool spares, int id = 0)
        {
            if (id == 0)
            {
                // To create new Price
                var priceViewModel = new PriceViewModel();

                priceViewModel.isSpares = spares;

                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", priceViewModel) });
            }
            else
            {
                // To update Price
                var priceResponse = await _priceViewManager.FindBySpecification(new PriceSpecification(id));
                if (priceResponse.Succeeded)
                {
                    var priceViewModel = _mapper.Map<PriceViewModel>(priceResponse.Data.First());

                    priceViewModel.isSpares = spares;

                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", priceViewModel) });
                }
            }
            return null;
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(int id, PriceViewModel priceVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (id == 0)
                    {
                        if (priceVM.ProductId == 0)
                        {
                            _notify.Warning(_localizer["You must select product."]);
                            var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", priceVM);
                            return new JsonResult(new { isValid = false, html = html });
                        }

                        if (priceVM.StructureId == 0)
                        {
                            priceVM.StructureId = null;
                        }

                        priceVM.PriceAllDate = DateTime.Now.Date;
                        priceVM.PriceArgDate = DateTime.Now.Date;
                        priceVM.PriceMxDate = DateTime.Now.Date;

                        var priceDTO = _mapper.Map<PriceDTO>(priceVM);

                        var result = await _priceViewManager.CreateAsync(priceDTO);
                        if (result.Succeeded)
                        {
                            id = result.Data;
                            _notify.Success(_localizer["Price created."]);
                        }
                        else
                        {
                            _notify.Error(result.Message);
                        }
                    }
                    else
                    {
                        //Update
                        var priceDTO = _mapper.Map<PriceDTO>(priceVM);

                        var result = await _priceViewManager.UpdateAsync(priceDTO);
                        if (result.Succeeded) _notify.Information(_localizer["Price updated."]);
                    }

                    // Vuelvo a cargar la grilla
                    return new JsonResult(new { isValid = true, spares = priceVM.isSpares });
                }
                else
                {
                    var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", priceVM);
                    return new JsonResult(new { isValid = false, html = html });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", priceVM);
                return new JsonResult(new { isValid = false, html = html });
            }
        }

        public async Task<IActionResult> ExportToExcel(bool spares, string columnFilter_1, string columnFilter_2, string columnFilter_3, string columnFilter_4, string columnFilter_5, string columnFilter_6,
                                                       string columnFilter_7, string columnFilter_8, string columnFilter_9, string columnFilter_10, string columnFilter_11, string columnFilter_12, string columnFilter_13, string columnFilter_14,
                                                       bool filter_checkSimplemak, bool filter_checkImported, bool filter_checkPriceAll, bool filter_checkPriceArg, bool filter_checkPriceMx, int colIndexOrder, string colOrderDirection)
        {
            try
            {
                var prices = await _priceViewManager.FindBySpecification(new PriceSpecification());

                if (prices.Succeeded)
                {
                    IEnumerable<PriceViewModel> pricesVM = _mapper.Map<List<PriceViewModel>>(prices.Data);

                    if (pricesVM.Count() > 0)
                    {
                        if (filter_checkPriceAll)
                        {
                            pricesVM = pricesVM.GroupBy(p => new { p.ProductId, p.StructureId }).Select(p => p.OrderByDescending(p => p.PriceAllDate).First());
                        }
                        else if (filter_checkPriceArg)
                        {
                            pricesVM = pricesVM.GroupBy(p => new { p.ProductId, p.StructureId }).Select(p => p.OrderByDescending(p => p.PriceArgDate).First());
                        }
                        else if (filter_checkPriceMx)
                        {
                            pricesVM = pricesVM.GroupBy(p => new { p.ProductId, p.StructureId }).Select(p => p.OrderByDescending(p => p.PriceMxDate).First());
                        }
                        else
                        {
                            pricesVM = pricesVM.GroupBy(p => new { p.ProductId, p.StructureId }).Select(p => p.OrderByDescending(p => p.PriceAllDate).First());
                        }
                    }

                    if (spares)
                    {
                        // Filtro por repuestos -> CategoryId = 2, CategoryId = 3 y CategoryId = 4
                        pricesVM = pricesVM.Where(p => p.ProductViewModel.SubCategory.CategoryId == 2 || p.ProductViewModel.SubCategory.CategoryId == 3 || p.ProductViewModel.SubCategory.CategoryId == 4).ToList();
                    }
                    else
                    {
                        // Filtro por maquinas y accesorios -> CategoryId = 1
                        pricesVM = pricesVM.Where(p => p.ProductViewModel.SubCategory.CategoryId == 1).ToList();
                    }

                    // Si es Simplemak o Importado
                    if (filter_checkSimplemak && filter_checkImported)
                    {
                        pricesVM = pricesVM.Where(p => p.ProductViewModel.ProductFeature != null && (p.ProductViewModel.ProductFeature.InHouseManufacturing == true || p.ProductViewModel.ProductFeature.Bought == true)).ToList();
                    }
                    else
                    {
                        if (filter_checkSimplemak)
                        {
                            pricesVM = pricesVM.Where(p => p.ProductViewModel.ProductFeature != null && p.ProductViewModel.ProductFeature.InHouseManufacturing == true).ToList();
                        }
                        else
                        {
                            pricesVM = pricesVM.Where(p => p.ProductViewModel.ProductFeature != null && p.ProductViewModel.ProductFeature.Bought == true).ToList();
                        }
                    }

                    if (!string.IsNullOrEmpty(columnFilter_1))
                    {
                        pricesVM = pricesVM.Where(pr => pr.ProductViewModel != null && pr.ProductViewModel.SubCategory != null && pr.ProductViewModel.SubCategory.Description.ToString().ToLower().Contains(columnFilter_1.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(columnFilter_2))
                    {
                        pricesVM = pricesVM.Where(pr => pr.ProductViewModel != null && pr.ProductViewModel.Code.ToString().ToLower().Contains(columnFilter_2.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(columnFilter_3))
                    {
                        pricesVM = pricesVM.Where(pr => pr.ProductViewModel != null && pr.ProductViewModel.Description.ToString().ToLower().Contains(columnFilter_3.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(columnFilter_4))
                    {
                        pricesVM = pricesVM.Where(pr => pr.StructureViewModel != null && pr.StructureViewModel.Description.ToString().ToLower().Contains(columnFilter_4.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(columnFilter_5))
                    {
                        pricesVM = pricesVM.Where(pr => pr.ProductViewModel.UnitMeasure != null && pr.ProductViewModel.UnitMeasure.Description.ToString().ToLower().Contains(columnFilter_5.ToLower())).ToList();
                    }

                    if (spares)
                    {
                        if (!string.IsNullOrEmpty(columnFilter_6))
                        {
                            pricesVM = pricesVM.Where(pr => pr.PriceAll.ToString().Contains(columnFilter_6)).ToList();
                        }
                        if (!string.IsNullOrEmpty(columnFilter_7))
                        {
                            pricesVM = pricesVM.Where(pr => pr.PriceAllDate.HasValue && pr.PriceAllDate.Value.ToString("dd/MM/yyyy").Contains(columnFilter_7)).ToList();
                        }
                        if (!string.IsNullOrEmpty(columnFilter_8))
                        {
                            pricesVM = pricesVM.Where(pr => pr.PriceAllObservations.ToString().ToLower().Contains(columnFilter_8.ToLower())).ToList();
                        }
                        //if (!string.IsNullOrEmpty(columnFilter_8))
                        //{
                        //    var user = "";
                        //    foreach (var od in pricesVM)
                        //    {
                        //        foreach (var u in _identityContext.Users.ToList())
                        //        {
                        //            if (u.Id == od.CreatedBy)
                        //            {
                        //                user = u.FirstName.ToString() + ", " + u.LastName.ToString();
                        //                pricesVM = pricesVM.Where(o => user.ToString().ToLower().Contains(columnFilter_8.ToLower())).ToList();
                        //            }
                        //        }
                        //    }
                        //}
                    }
                    else
                    {
                        // 5, 6 y 7 para USD ARG
                        if (!string.IsNullOrEmpty(columnFilter_6))
                        {
                            pricesVM = pricesVM.Where(pr => pr.PriceArg.ToString().Contains(columnFilter_6)).ToList();
                        }
                        if (!string.IsNullOrEmpty(columnFilter_7))
                        {
                            pricesVM = pricesVM.Where(pr => pr.PriceArgDate.HasValue && pr.PriceArgDate.Value.ToString("dd/MM/yyyy").Contains(columnFilter_7)).ToList();
                        }
                        if (!string.IsNullOrEmpty(columnFilter_8))
                        {
                            pricesVM = pricesVM.Where(pr => pr.PriceArgObservations.ToString().ToLower().Contains(columnFilter_8.ToLower())).ToList();
                        }
                        // 8, 9 y 10 para USD COMEX
                        if (!string.IsNullOrEmpty(columnFilter_9))
                        {
                            pricesVM = pricesVM.Where(pr => pr.PriceAll.ToString().Contains(columnFilter_9)).ToList();
                        }
                        if (!string.IsNullOrEmpty(columnFilter_10))
                        {
                            pricesVM = pricesVM.Where(pr => pr.PriceAllDate.HasValue && pr.PriceAllDate.Value.ToString("dd/MM/yyyy").Contains(columnFilter_10)).ToList();
                        }
                        if (!string.IsNullOrEmpty(columnFilter_11))
                        {
                            pricesVM = pricesVM.Where(pr => pr.PriceAllObservations.ToString().ToLower().Contains(columnFilter_11.ToLower())).ToList();
                        }
                        // 11, 12 y 13 para $ MXN
                        if (!string.IsNullOrEmpty(columnFilter_12))
                        {
                            pricesVM = pricesVM.Where(pr => pr.PriceMx.ToString().Contains(columnFilter_12)).ToList();
                        }
                        if (!string.IsNullOrEmpty(columnFilter_13))
                        {
                            pricesVM = pricesVM.Where(pr => pr.PriceMxDate.HasValue && pr.PriceMxDate.Value.ToString("dd/MM/yyyy").Contains(columnFilter_13)).ToList();
                        }
                        if (!string.IsNullOrEmpty(columnFilter_14))
                        {
                            pricesVM = pricesVM.Where(pr => pr.PriceMxObservations.ToString().ToLower().Contains(columnFilter_14.ToLower())).ToList();
                        }
                        //if (!string.IsNullOrEmpty(columnFilter_14))
                        //{
                        //    var user = "";
                        //    foreach (var od in pricesVM)
                        //    {
                        //        foreach (var u in _identityContext.Users.ToList())
                        //        {
                        //            if (u.Id == od.CreatedBy)
                        //            {
                        //                user = u.FirstName.ToString() + ", " + u.LastName.ToString();
                        //                pricesVM = pricesVM.Where(o => user.ToString().ToLower().Contains(columnFilter_14.ToLower())).ToList();
                        //            }
                        //        }
                        //    }
                        //}
                    }

                    if (colIndexOrder != 0 && colOrderDirection != "")
                    {
                        var sortColumnIndex = colIndexOrder;
                        var sortDirection = colOrderDirection;

                        if (sortColumnIndex == 2)
                        {
                            pricesVM = sortDirection == "asc" ? pricesVM.OrderBy(pr => pr.ProductViewModel.SubCategory.Description) : pricesVM.OrderByDescending(pr => pr.ProductViewModel.SubCategory.Description);
                        }
                        else if (sortColumnIndex == 3)
                        {
                            pricesVM = sortDirection == "asc" ? pricesVM.OrderBy(pr => pr.ProductViewModel.Code) : pricesVM.OrderByDescending(pr => pr.ProductViewModel.Code);
                        }
                        else if (sortColumnIndex == 4)
                        {
                            pricesVM = sortDirection == "asc" ? pricesVM.OrderBy(pr => pr.ProductViewModel.Description) : pricesVM.OrderByDescending(pr => pr.ProductViewModel.Description);
                        }
                        else if (sortColumnIndex == 5)
                        {
                            pricesVM = sortDirection == "asc" ? pricesVM.OrderBy(pr => pr.StructureViewModel != null ? pr.StructureViewModel.Description.ToString() : null) : pricesVM.OrderByDescending(pr => pr.StructureViewModel != null ? pr.StructureViewModel.Description.ToString() : null);
                        }
                        else if (sortColumnIndex == 6)
                        {
                            pricesVM = sortDirection == "asc" ? pricesVM.OrderBy(pr => pr.ProductViewModel.UnitMeasure != null ? pr.ProductViewModel.UnitMeasure.Description.ToString() : null) : pricesVM.OrderByDescending(pr => pr.ProductViewModel.UnitMeasure != null ? pr.ProductViewModel.UnitMeasure.Description.ToString() : null);
                        }

                        if (spares)
                        {
                            // 6, 7 y 8 USD COMEX
                            if (sortColumnIndex == 7)
                            {
                                pricesVM = sortDirection == "asc" ? pricesVM.OrderBy(pr => pr.PriceAll) : pricesVM.OrderByDescending(pr => pr.PriceAll);
                            }
                            else if (sortColumnIndex == 8)
                            {
                                pricesVM = sortDirection == "asc" ? pricesVM.OrderBy(pr => pr.PriceAllDate) : pricesVM.OrderByDescending(pr => pr.PriceAllDate);
                            }
                            else if (sortColumnIndex == 9)
                            {
                                pricesVM = sortDirection == "asc" ? pricesVM.OrderBy(pr => pr.PriceAllObservations) : pricesVM.OrderByDescending(pr => pr.PriceAllObservations);
                            }
                            //else if (sortColumnIndex == 9)
                            //{
                            //    pricesVM = sortDirection == "asc" ? pricesVM.OrderBy(pr => pr.CreatedBy) : pricesVM.OrderByDescending(pr => pr.CreatedBy);
                            //}
                        }
                        else
                        {
                            // 6, 7 y 8 USD ARG
                            if (sortColumnIndex == 7)
                            {
                                pricesVM = sortDirection == "asc" ? pricesVM.OrderBy(pr => pr.PriceArg) : pricesVM.OrderByDescending(pr => pr.PriceArg);
                            }
                            else if (sortColumnIndex == 8)
                            {
                                pricesVM = sortDirection == "asc" ? pricesVM.OrderBy(pr => pr.PriceArgDate) : pricesVM.OrderByDescending(pr => pr.PriceArgDate);
                            }
                            else if (sortColumnIndex == 9)
                            {
                                pricesVM = sortDirection == "asc" ? pricesVM.OrderBy(pr => pr.PriceArgObservations) : pricesVM.OrderByDescending(pr => pr.PriceArgObservations);
                            }
                            // 9, 10 y 11 USD COMEX
                            else if (sortColumnIndex == 10)
                            {
                                pricesVM = sortDirection == "asc" ? pricesVM.OrderBy(pr => pr.PriceAll) : pricesVM.OrderByDescending(pr => pr.PriceAll);
                            }
                            else if (sortColumnIndex == 11)
                            {
                                pricesVM = sortDirection == "asc" ? pricesVM.OrderBy(pr => pr.PriceAllDate) : pricesVM.OrderByDescending(pr => pr.PriceAllDate);
                            }
                            else if (sortColumnIndex == 12)
                            {
                                pricesVM = sortDirection == "asc" ? pricesVM.OrderBy(pr => pr.PriceAllObservations) : pricesVM.OrderByDescending(pr => pr.PriceAllObservations);
                            }
                            // 12, 13 y 14 $ MXN
                            else if (sortColumnIndex == 13)
                            {
                                pricesVM = sortDirection == "asc" ? pricesVM.OrderBy(pr => pr.PriceMx) : pricesVM.OrderByDescending(pr => pr.PriceMx);
                            }
                            else if (sortColumnIndex == 14)
                            {
                                pricesVM = sortDirection == "asc" ? pricesVM.OrderBy(pr => pr.PriceMxDate) : pricesVM.OrderByDescending(pr => pr.PriceMxDate);
                            }
                            else if (sortColumnIndex == 15)
                            {
                                pricesVM = sortDirection == "asc" ? pricesVM.OrderBy(pr => pr.PriceMxObservations) : pricesVM.OrderByDescending(pr => pr.PriceMxObservations);
                            }
                            //else if (sortColumnIndex == 15)
                            //{
                            //    pricesVM = sortDirection == "asc" ? pricesVM.OrderBy(pr => pr.CreatedBy) : pricesVM.OrderByDescending(pr => pr.CreatedBy);
                            //}
                        }
                    }

                    return ExportViewModelToExcel(pricesVM, spares, filter_checkPriceAll, filter_checkPriceArg, filter_checkPriceMx);
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

        public IActionResult ExportViewModelToExcel(IEnumerable<PriceViewModel> priceViewModels, bool spares, bool filter_checkPriceAll, bool filter_checkPriceArg, bool filter_checkPriceMx)
        {
            try
            {
                var nameForExcelFile = "";
                if (spares)
                {
                    nameForExcelFile = "Precios de repuestos_";
                }
                else
                {
                    nameForExcelFile = "Precios_";
                }

                var stream = new MemoryStream();
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                if (spares)
                {
                    using (var xlPackage = new ExcelPackage(stream))
                    {
                        var workSheet = xlPackage.Workbook.Worksheets.Add("Precios de repuestos");

                        workSheet.Cells["A1"].Value = _localizer["Family/Heading"];
                        workSheet.Cells["B1"].Value = _localizer["Code"];
                        workSheet.Cells["C1"].Value = _localizer["Description"];
                        workSheet.Cells["D1"].Value = _localizer["Configuration"];
                        workSheet.Cells["E1"].Value = _localizer["Unit measure"];
                        workSheet.Cells["F1"].Value = "Precio USD COMEX";
                        workSheet.Cells["G1"].Value = _localizer["Date"];
                        workSheet.Cells["H1"].Value = _localizer["Observations"];
                        //workSheet.Cells["H1"].Value = _localizer["User"];

                        var row = 2;
                        foreach (var priceViewModel in priceViewModels)
                        {
                            workSheet.Cells[row, 1].Value = priceViewModel.ProductViewModel.SubCategory.Description.ToString();
                            workSheet.Cells[row, 2].Value = priceViewModel.ProductViewModel.Code.ToString();
                            workSheet.Cells[row, 3].Value = priceViewModel.ProductViewModel.Description.ToString();

                            if (priceViewModel.StructureViewModel != null)
                            {
                                workSheet.Cells[row, 4].Value = priceViewModel.StructureViewModel.Description.ToString();
                            }
                            else
                            {
                                workSheet.Cells[row, 4].Value = "";
                            }

                            if (priceViewModel.ProductViewModel != null)
                            {
                                if (priceViewModel.ProductViewModel.UnitMeasure != null)
                                {
                                    workSheet.Cells[row, 5].Value = priceViewModel.ProductViewModel.UnitMeasure.Description.ToString();
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

                            workSheet.Cells[row, 6].Value = priceViewModel.PriceAll;
                            //workSheet.Cells[row, 6].Style.Numberformat.Format = "0.00";
                            workSheet.Cells[row, 6].Style.Numberformat.Format = "#,##0.00";

                            if (priceViewModel.PriceAllDate != null)
                            {
                                workSheet.Cells[row, 7].Value = priceViewModel.PriceAllDate;
                                workSheet.Cells[row, 7].Style.Numberformat.Format = "dd-MM-yyyy HH:mm";
                            }
                            else
                            {
                                workSheet.Cells[row, 7].Value = "";
                            }

                            if (priceViewModel.PriceAllObservations != null)
                            {
                                workSheet.Cells[row, 8].Value = priceViewModel.PriceAllObservations.ToString();
                            }
                            else
                            {
                                workSheet.Cells[row, 8].Value = "";
                            }

                            //var user = "";
                            //foreach (var u in _identityContext.Users.ToList())
                            //{
                            //    if (u.Id == priceViewModel.CreatedBy)
                            //    {
                            //        user = u.FirstName.ToString() + ", " + u.LastName.ToString();
                            //    }
                            //}
                            //workSheet.Cells[row, 8].Value = user.ToString();

                            row++;
                        }

                        workSheet.Cells["A1:H1"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                        workSheet.Cells["H1"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

                        workSheet.Row(1).Style.Font.Bold = true;
                        workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();

                        xlPackage.Workbook.Properties.Title = nameForExcelFile + DateTime.Now.Date.ToString("dd-MM-yyyy");
                        xlPackage.Save();
                    }

                    stream.Position = 0;
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nameForExcelFile + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xlsx");
                }
                else
                {
                    using (var xlPackage = new ExcelPackage(stream))
                    {
                        var workSheet = xlPackage.Workbook.Worksheets.Add("Precios");

                        workSheet.Cells["A1"].Value = _localizer["Family/Heading"];
                        workSheet.Cells["B1"].Value = _localizer["Code"];
                        workSheet.Cells["C1"].Value = _localizer["Description"];
                        workSheet.Cells["D1"].Value = _localizer["Configuration"];
                        workSheet.Cells["E1"].Value = _localizer["Unit measure"];

                        if (filter_checkPriceArg)
                        {
                            workSheet.Cells["F1"].Value = "Precio USD ARG";
                            workSheet.Cells["G1"].Value = _localizer["Date"];
                            workSheet.Cells["H1"].Value = _localizer["Observations"];
                            //workSheet.Cells["H1"].Value = _localizer["User"];
                        }

                        if (filter_checkPriceAll)
                        {
                            workSheet.Cells["F1"].Value = "Precio USD COMEX";
                            workSheet.Cells["G1"].Value = _localizer["Date"];
                            workSheet.Cells["H1"].Value = _localizer["Observations"];
                            //workSheet.Cells["H1"].Value = _localizer["User"];
                        }

                        if (filter_checkPriceMx)
                        {
                            workSheet.Cells["F1"].Value = "Precio $ MXN";
                            workSheet.Cells["G1"].Value = _localizer["Date"];
                            workSheet.Cells["H1"].Value = _localizer["Observations"];
                            //workSheet.Cells["H1"].Value = _localizer["User"];
                        }

                        if (filter_checkPriceAll && filter_checkPriceArg)
                        {
                            workSheet.Cells["F1"].Value = "Precio USD ARG";
                            workSheet.Cells["G1"].Value = _localizer["Date"];
                            workSheet.Cells["H1"].Value = _localizer["Observations"];
                            workSheet.Cells["I1"].Value = "Precio USD COMEX";
                            workSheet.Cells["J1"].Value = _localizer["Date"];
                            workSheet.Cells["K1"].Value = _localizer["Observations"];
                            //workSheet.Cells["K1"].Value = _localizer["User"];
                        }

                        if (filter_checkPriceArg && filter_checkPriceMx)
                        {
                            workSheet.Cells["F1"].Value = "Precio USD ARG";
                            workSheet.Cells["G1"].Value = _localizer["Date"];
                            workSheet.Cells["H1"].Value = _localizer["Observations"];
                            workSheet.Cells["I1"].Value = "Precio $ MXN";
                            workSheet.Cells["J1"].Value = _localizer["Date"];
                            workSheet.Cells["K1"].Value = _localizer["Observations"];
                            //workSheet.Cells["K1"].Value = _localizer["User"];
                        }

                        if (filter_checkPriceAll && filter_checkPriceMx)
                        {
                            workSheet.Cells["G1"].Value = "Precio USD COMEX";
                            workSheet.Cells["G1"].Value = _localizer["Date"];
                            workSheet.Cells["H1"].Value = _localizer["Observations"];
                            workSheet.Cells["I1"].Value = "Precio $ MXN";
                            workSheet.Cells["J1"].Value = _localizer["Date"];
                            workSheet.Cells["K1"].Value = _localizer["Observations"];
                            //workSheet.Cells["K1"].Value = _localizer["User"];
                        }

                        if (filter_checkPriceAll && filter_checkPriceArg && filter_checkPriceMx)
                        {
                            workSheet.Cells["F1"].Value = "Precio USD ARG";
                            workSheet.Cells["G1"].Value = _localizer["Date"];
                            workSheet.Cells["H1"].Value = _localizer["Observations"];
                            workSheet.Cells["I1"].Value = "Precio USD COMEX";
                            workSheet.Cells["J1"].Value = _localizer["Date"];
                            workSheet.Cells["L1"].Value = _localizer["Observations"];
                            workSheet.Cells["L1"].Value = "Precio $ MXN";
                            workSheet.Cells["M1"].Value = _localizer["Date"];
                            workSheet.Cells["N1"].Value = _localizer["Observations"];
                            //workSheet.Cells["N1"].Value = _localizer["User"];
                        }

                        var row = 2;
                        foreach (var priceViewModel in priceViewModels)
                        {
                            var user = "";

                            workSheet.Cells[row, 1].Value = priceViewModel.ProductViewModel.SubCategory.Description.ToString();
                            workSheet.Cells[row, 2].Value = priceViewModel.ProductViewModel.Code.ToString();
                            workSheet.Cells[row, 3].Value = priceViewModel.ProductViewModel.Description.ToString();

                            if (priceViewModel.StructureViewModel != null)
                            {
                                workSheet.Cells[row, 4].Value = priceViewModel.StructureViewModel.Description.ToString();
                            }
                            else
                            {
                                workSheet.Cells[row, 4].Value = "";
                            }

                            if (priceViewModel.ProductViewModel != null)
                            {
                                if (priceViewModel.ProductViewModel.UnitMeasure != null)
                                {
                                    workSheet.Cells[row, 5].Value = priceViewModel.ProductViewModel.UnitMeasure.Description.ToString();
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

                            if (filter_checkPriceArg)
                            {
                                /* Price Arg */
                                workSheet.Cells[row, 6].Value = priceViewModel.PriceArg;
                                //workSheet.Cells[row, 6].Style.Numberformat.Format = "0.00";
                                workSheet.Cells[row, 6].Style.Numberformat.Format = "#,##0.00";

                                if (priceViewModel.PriceArgDate != null)
                                {
                                    workSheet.Cells[row, 7].Value = priceViewModel.PriceArgDate;
                                    workSheet.Cells[row, 7].Style.Numberformat.Format = "dd-MM-yyyy HH:mm";
                                }
                                else
                                {
                                    workSheet.Cells[row, 7].Value = "";
                                }

                                if (priceViewModel.PriceArgObservations != null)
                                {
                                    workSheet.Cells[row, 8].Value = priceViewModel.PriceArgObservations.ToString();
                                }
                                else
                                {
                                    workSheet.Cells[row, 8].Value = "";
                                }

                                ///* User */
                                //foreach (var u in _identityContext.Users.ToList())
                                //{
                                //    if (u.Id == priceViewModel.CreatedBy)
                                //    {
                                //        user = u.FirstName.ToString() + ", " + u.LastName.ToString();
                                //    }
                                //}
                                //workSheet.Cells[row, 8].Value = user.ToString();
                            }

                            if (filter_checkPriceAll)
                            {
                                /* Price All */
                                workSheet.Cells[row, 6].Value = priceViewModel.PriceAll;
                                //workSheet.Cells[row, 6].Style.Numberformat.Format = "0.00";
                                workSheet.Cells[row, 6].Style.Numberformat.Format = "#,##0.00";

                                if (priceViewModel.PriceAllDate != null)
                                {
                                    workSheet.Cells[row, 7].Value = priceViewModel.PriceAllDate;
                                    workSheet.Cells[row, 7].Style.Numberformat.Format = "dd-MM-yyyy HH:mm";
                                }
                                else
                                {
                                    workSheet.Cells[row, 7].Value = "";
                                }

                                if (priceViewModel.PriceAllObservations != null)
                                {
                                    workSheet.Cells[row, 8].Value = priceViewModel.PriceAllObservations.ToString();
                                }
                                else
                                {
                                    workSheet.Cells[row, 8].Value = "";
                                }

                                ///* User */
                                //foreach (var u in _identityContext.Users.ToList())
                                //{
                                //    if (u.Id == priceViewModel.CreatedBy)
                                //    {
                                //        user = u.FirstName.ToString() + ", " + u.LastName.ToString();
                                //    }
                                //}
                                //workSheet.Cells[row, 8].Value = user.ToString();
                            }

                            if (filter_checkPriceMx)
                            {
                                /* Price Mx */
                                workSheet.Cells[row, 6].Value = priceViewModel.PriceMx;
                                //workSheet.Cells[row, 6].Style.Numberformat.Format = "0.00";
                                workSheet.Cells[row, 6].Style.Numberformat.Format = "#,##0.00";

                                if (priceViewModel.PriceMxDate != null)
                                {
                                    workSheet.Cells[row, 7].Value = priceViewModel.PriceMxDate;
                                    workSheet.Cells[row, 7].Style.Numberformat.Format = "dd-MM-yyyy HH:mm";
                                }
                                else
                                {
                                    workSheet.Cells[row, 7].Value = "";
                                }

                                if (priceViewModel.PriceMxObservations != null)
                                {
                                    workSheet.Cells[row, 8].Value = priceViewModel.PriceMxObservations.ToString();
                                }
                                else
                                {
                                    workSheet.Cells[row, 8].Value = "";
                                }

                                ///* User */
                                //foreach (var u in _identityContext.Users.ToList())
                                //{
                                //    if (u.Id == priceViewModel.CreatedBy)
                                //    {
                                //        user = u.FirstName.ToString() + ", " + u.LastName.ToString();
                                //    }
                                //}
                                //workSheet.Cells[row, 8].Value = user.ToString();
                            }

                            if (filter_checkPriceAll && filter_checkPriceArg)
                            {
                                /* Price Arg */
                                workSheet.Cells[row, 6].Value = priceViewModel.PriceArg;
                                //workSheet.Cells[row, 6].Style.Numberformat.Format = "0.00";
                                workSheet.Cells[row, 6].Style.Numberformat.Format = "#,##0.00";

                                if (priceViewModel.PriceArgDate != null)
                                {
                                    workSheet.Cells[row, 7].Value = priceViewModel.PriceArgDate;
                                    workSheet.Cells[row, 7].Style.Numberformat.Format = "dd-MM-yyyy HH:mm";
                                }
                                else
                                {
                                    workSheet.Cells[row, 7].Value = "";
                                }

                                if (priceViewModel.PriceArgObservations != null)
                                {
                                    workSheet.Cells[row, 8].Value = priceViewModel.PriceArgObservations.ToString();
                                }
                                else
                                {
                                    workSheet.Cells[row, 8].Value = "";
                                }

                                /* Price All */
                                workSheet.Cells[row, 9].Value = priceViewModel.PriceAll;
                                //workSheet.Cells[row, 9].Style.Numberformat.Format = "0.00";
                                workSheet.Cells[row, 9].Style.Numberformat.Format = "#,##0.00";

                                if (priceViewModel.PriceAllDate != null)
                                {
                                    workSheet.Cells[row, 10].Value = priceViewModel.PriceAllDate;
                                    workSheet.Cells[row, 10].Style.Numberformat.Format = "dd-MM-yyyy HH:mm";
                                }
                                else
                                {
                                    workSheet.Cells[row, 10].Value = "";
                                }

                                if (priceViewModel.PriceAllObservations != null)
                                {
                                    workSheet.Cells[row, 11].Value = priceViewModel.PriceAllObservations.ToString();
                                }
                                else
                                {
                                    workSheet.Cells[row, 11].Value = "";
                                }

                                ///* User */
                                //foreach (var u in _identityContext.Users.ToList())
                                //{
                                //    if (u.Id == priceViewModel.CreatedBy)
                                //    {
                                //        user = u.FirstName.ToString() + ", " + u.LastName.ToString();
                                //    }
                                //}
                                //workSheet.Cells[row, 11].Value = user.ToString();
                            }

                            if (filter_checkPriceArg && filter_checkPriceMx)
                            {
                                /* Price Arg */
                                workSheet.Cells[row, 6].Value = priceViewModel.PriceArg;
                                //workSheet.Cells[row, 6].Style.Numberformat.Format = "0.00";
                                workSheet.Cells[row, 6].Style.Numberformat.Format = "#,##0.00";

                                if (priceViewModel.PriceArgDate != null)
                                {
                                    workSheet.Cells[row, 7].Value = priceViewModel.PriceArgDate;
                                    workSheet.Cells[row, 7].Style.Numberformat.Format = "dd-MM-yyyy HH:mm";
                                }
                                else
                                {
                                    workSheet.Cells[row, 7].Value = "";
                                }

                                if (priceViewModel.PriceArgObservations != null)
                                {
                                    workSheet.Cells[row, 8].Value = priceViewModel.PriceArgObservations.ToString();
                                }
                                else
                                {
                                    workSheet.Cells[row, 8].Value = "";
                                }

                                /* Price Mx */
                                workSheet.Cells[row, 9].Value = priceViewModel.PriceMx;
                                //workSheet.Cells[row, 9].Style.Numberformat.Format = "0.00";
                                workSheet.Cells[row, 9].Style.Numberformat.Format = "#,##0.00";

                                if (priceViewModel.PriceMxDate != null)
                                {
                                    workSheet.Cells[row, 10].Value = priceViewModel.PriceMxDate;
                                    workSheet.Cells[row, 10].Style.Numberformat.Format = "dd-MM-yyyy HH:mm";
                                }
                                else
                                {
                                    workSheet.Cells[row, 10].Value = "";
                                }

                                if (priceViewModel.PriceMxObservations != null)
                                {
                                    workSheet.Cells[row, 11].Value = priceViewModel.PriceMxObservations.ToString();
                                }
                                else
                                {
                                    workSheet.Cells[row, 11].Value = "";
                                }

                                ///* User */
                                //foreach (var u in _identityContext.Users.ToList())
                                //{
                                //    if (u.Id == priceViewModel.CreatedBy)
                                //    {
                                //        user = u.FirstName.ToString() + ", " + u.LastName.ToString();
                                //    }
                                //}
                                //workSheet.Cells[row, 11].Value = user.ToString();
                            }

                            if (filter_checkPriceAll && filter_checkPriceMx)
                            {
                                /* Price All */
                                workSheet.Cells[row, 6].Value = priceViewModel.PriceAll;
                                //workSheet.Cells[row, 6].Style.Numberformat.Format = "0.00";
                                workSheet.Cells[row, 6].Style.Numberformat.Format = "#,##0.00";

                                if (priceViewModel.PriceAllDate != null)
                                {
                                    workSheet.Cells[row, 7].Value = priceViewModel.PriceAllDate;
                                    workSheet.Cells[row, 7].Style.Numberformat.Format = "dd-MM-yyyy HH:mm";
                                }
                                else
                                {
                                    workSheet.Cells[row, 7].Value = "";
                                }

                                if (priceViewModel.PriceAllObservations != null)
                                {
                                    workSheet.Cells[row, 8].Value = priceViewModel.PriceAllObservations.ToString();
                                }
                                else
                                {
                                    workSheet.Cells[row, 8].Value = "";
                                }

                                /* Price Mx */
                                workSheet.Cells[row, 9].Value = priceViewModel.PriceMx;
                                //workSheet.Cells[row, 9].Style.Numberformat.Format = "0.00";
                                workSheet.Cells[row, 9].Style.Numberformat.Format = "#,##0.00";

                                if (priceViewModel.PriceMxDate != null)
                                {
                                    workSheet.Cells[row, 10].Value = priceViewModel.PriceMxDate;
                                    workSheet.Cells[row, 10].Style.Numberformat.Format = "dd-MM-yyyy HH:mm";
                                }
                                else
                                {
                                    workSheet.Cells[row, 10].Value = "";
                                }

                                if (priceViewModel.PriceMxObservations != null)
                                {
                                    workSheet.Cells[row, 11].Value = priceViewModel.PriceMxObservations.ToString();
                                }
                                else
                                {
                                    workSheet.Cells[row, 11].Value = "";
                                }

                                ///* User */
                                //foreach (var u in _identityContext.Users.ToList())
                                //{
                                //    if (u.Id == priceViewModel.CreatedBy)
                                //    {
                                //        user = u.FirstName.ToString() + ", " + u.LastName.ToString();
                                //    }
                                //}
                                //workSheet.Cells[row, 11].Value = user.ToString();
                            }

                            if (filter_checkPriceAll && filter_checkPriceArg && filter_checkPriceMx)
                            {
                                /* Price Arg */
                                workSheet.Cells[row, 6].Value = priceViewModel.PriceArg;
                                //workSheet.Cells[row, 6].Style.Numberformat.Format = "0.00";
                                workSheet.Cells[row, 6].Style.Numberformat.Format = "#,##0.00";

                                if (priceViewModel.PriceArgDate != null)
                                {
                                    workSheet.Cells[row, 7].Value = priceViewModel.PriceArgDate;
                                    workSheet.Cells[row, 7].Style.Numberformat.Format = "dd-MM-yyyy HH:mm";
                                }
                                else
                                {
                                    workSheet.Cells[row, 7].Value = "";
                                }

                                if (priceViewModel.PriceArgObservations != null)
                                {
                                    workSheet.Cells[row, 8].Value = priceViewModel.PriceArgObservations.ToString();
                                }
                                else
                                {
                                    workSheet.Cells[row, 8].Value = "";
                                }

                                /* Price All */
                                workSheet.Cells[row, 9].Value = priceViewModel.PriceAll;
                                //workSheet.Cells[row, 9].Style.Numberformat.Format = "0.00";
                                workSheet.Cells[row, 9].Style.Numberformat.Format = "#,##0.00";

                                if (priceViewModel.PriceAllDate != null)
                                {
                                    workSheet.Cells[row, 10].Value = priceViewModel.PriceAllDate;
                                    workSheet.Cells[row, 10].Style.Numberformat.Format = "dd-MM-yyyy HH:mm";
                                }
                                else
                                {
                                    workSheet.Cells[row, 10].Value = "";
                                }

                                if (priceViewModel.PriceAllObservations != null)
                                {
                                    workSheet.Cells[row, 11].Value = priceViewModel.PriceAllObservations.ToString();
                                }
                                else
                                {
                                    workSheet.Cells[row, 11].Value = "";
                                }

                                /* Price Mx */
                                workSheet.Cells[row, 12].Value = priceViewModel.PriceMx;
                                //workSheet.Cells[row, 12].Style.Numberformat.Format = "0.00";
                                workSheet.Cells[row, 12].Style.Numberformat.Format = "#,##0.00";

                                if (priceViewModel.PriceMxDate != null)
                                {
                                    workSheet.Cells[row, 13].Value = priceViewModel.PriceMxDate;
                                    workSheet.Cells[row, 13].Style.Numberformat.Format = "dd-MM-yyyy HH:mm";
                                }
                                else
                                {
                                    workSheet.Cells[row, 13].Value = "";
                                }

                                if (priceViewModel.PriceMxObservations != null)
                                {
                                    workSheet.Cells[row, 14].Value = priceViewModel.PriceMxObservations.ToString();
                                }
                                else
                                {
                                    workSheet.Cells[row, 14].Value = "";
                                }

                                ///* User */
                                //foreach (var u in _identityContext.Users.ToList())
                                //{
                                //    if (u.Id == priceViewModel.CreatedBy)
                                //    {
                                //        user = u.FirstName.ToString() + ", " + u.LastName.ToString();
                                //    }
                                //}
                                //workSheet.Cells[row, 14].Value = user.ToString();
                            }

                            row++;
                        }

                        if (filter_checkPriceArg)
                        {
                            workSheet.Cells["A1:G1"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                            workSheet.Cells["G1"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                        }

                        if (filter_checkPriceAll)
                        {
                            workSheet.Cells["A1:H1"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                            workSheet.Cells["H1"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                        }

                        if (filter_checkPriceMx)
                        {
                            workSheet.Cells["A1:H1"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                            workSheet.Cells["H1"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                        }

                        if (filter_checkPriceAll && filter_checkPriceArg)
                        {
                            workSheet.Cells["A1:K1"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                            workSheet.Cells["K1"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                        }

                        if (filter_checkPriceArg && filter_checkPriceMx)
                        {
                            workSheet.Cells["A1:K1"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                            workSheet.Cells["K1"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                        }

                        if (filter_checkPriceAll && filter_checkPriceMx)
                        {
                            workSheet.Cells["A1:K1"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                            workSheet.Cells["K1"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                        }

                        if (filter_checkPriceAll && filter_checkPriceArg && filter_checkPriceMx)
                        {
                            workSheet.Cells["A1:N1"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                            workSheet.Cells["N1"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                        }

                        workSheet.Row(1).Style.Font.Bold = true;
                        workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();

                        xlPackage.Workbook.Properties.Title = nameForExcelFile + DateTime.Now.Date.ToString("dd-MM-yyyy");
                        xlPackage.Save();
                    }

                    stream.Position = 0;
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nameForExcelFile + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xlsx");
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return null;
            }
        }
    }
}