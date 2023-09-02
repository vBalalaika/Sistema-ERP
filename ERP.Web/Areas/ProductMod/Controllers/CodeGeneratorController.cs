using ERP.Application.Constants;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Application.Interfaces.ViewManagers.ProductMod;
using ERP.Application.Specification.ProductMod;
using ERP.Application.Specification.ProductMod.SubCategorySpecifications;
using ERP.Domain.Entities.ProductMod;
using ERP.Language;
using ERP.Web.Abstractions;
using ERP.Web.Areas.ProductMod.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Web.Areas.ProductMod.Controllers
{
    [Area("ProductMod")]
    public class CodeGeneratorController : BaseController<CodeGeneratorController>
    {
        private readonly IProductViewManager _viewManagerProduct;
        private readonly ISubCategoryViewManager _viewManagerSubCategory;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public CodeGeneratorController(IProductViewManager viewManagerProduct, ISubCategoryViewManager viewManagerSubCategory, IStringLocalizer<SharedResource> localizer)
        {
            _viewManagerProduct = viewManagerProduct;
            _viewManagerSubCategory = viewManagerSubCategory;
            _localizer = localizer;
        }
        public IActionResult Index()
        {
            var model = new CodeGeneratorViewModel();
            return View(model);
        }

        [Authorize(Policy = Permissions.CodeGenerator.View)]
        public async Task<IActionResult> OnGetCreateCodeGenerator()
        {
            try
            {
                var codeGeneratorViewModel = new CodeGeneratorViewModel();
                var subcategoriesResponse = await _viewManagerProduct.GetSubCategories();
                if (subcategoriesResponse.Succeeded)
                {
                    var subcategoriesViewModel = _mapper.Map<List<SubCategoryDTO>>(subcategoriesResponse.Data);
                    codeGeneratorViewModel.SubCategories = new SelectList(subcategoriesViewModel, nameof(SubCategoryViewModel.Id), nameof(SubCategoryViewModel.Description), null, null);
                }

                return View("_Create", codeGeneratorViewModel);
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        public async Task<IActionResult> OnGetGenerateCodeGenerator(int subCategoryId, int quantity)
        {
            var codeGeneratorVM = new CodeGeneratorViewModel();
            codeGeneratorVM.SubCategoryId = subCategoryId;
            codeGeneratorVM.Quantity = quantity;

            try
            {
                if (ModelState.IsValid)
                {
                    codeGeneratorVM.Products = new List<ProductViewModel>();

                    var subcategoryDTO = await _viewManagerProduct.GetSubCategoryById(codeGeneratorVM.SubCategoryId);
                    var subCategoryViewModel = _mapper.Map<SubCategoryViewModel>(subcategoryDTO.Data);

                    if (subcategoryDTO.Succeeded)
                    {
                        var lastCodes = await GetProductCodesBySubCategory(subCategoryViewModel, codeGeneratorVM.Quantity);

                        foreach (var code in lastCodes)
                        {
                            var productVM = new ProductViewModel();
                            productVM.Code = code;
                            productVM.SubCategoryId = codeGeneratorVM.SubCategoryId;
                            productVM.SubCategory = subCategoryViewModel;
                            productVM.Description = "Producto generado " + code;
                            codeGeneratorVM.Products.Add(productVM);
                        }

                        codeGeneratorVM = await PopulateSubCategories(codeGeneratorVM);
                        // _notify.Success(_localizer["Product codes generated"]);
                        return PartialView("Partials/_ProductsItemsTbody", codeGeneratorVM);
                    }
                    else
                    {
                        codeGeneratorVM = await PopulateSubCategories(codeGeneratorVM);
                        _notify.Information(_localizer["Product codes not generated"]);
                        return PartialView("Partials/_ProductsItemsTbody", codeGeneratorVM);
                    }

                }
                else
                {
                    codeGeneratorVM = await PopulateSubCategories(codeGeneratorVM);
                    _notify.Information(_localizer["Invalid data"]);
                    return PartialView("Partials/_ProductsItemsTbody", codeGeneratorVM);
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                codeGeneratorVM = await PopulateSubCategories(codeGeneratorVM);
                return PartialView("Partials/_ProductsItemsTbody", codeGeneratorVM);
            }
        }

        [HttpPost]
        public async Task<IActionResult> OnPostCreateCodeGenerator(CodeGeneratorViewModel codeGeneratorVM)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    if (codeGeneratorVM.Products.Count > 0)
                    {
                        foreach (var prod in codeGeneratorVM.Products.Where(x => x.Id == 0).ToList())
                        {
                            var productDTO = _mapper.Map<ProductDTO>(prod);
                            productDTO.SubCategory = null;
                            productDTO.ProductFeature = new ProductFeature();
                            productDTO.ProductFeature.InHouseManufacturing = true;
                            productDTO.UnitMeasureId = 19;
                            var result = await _viewManagerProduct.CreateAsync(productDTO);
                            if (result.Succeeded)
                            {
                                prod.Id = result.Data;
                                _notify.Success(_localizer["Product with ID {0} Created.", result.Data]);
                            }
                            else
                            {
                                _notify.Error(result.Message);
                            }
                        }
                        codeGeneratorVM = await PopulateSubCategories(codeGeneratorVM);
                        return View("_Create", codeGeneratorVM);
                    }
                    else
                    {
                        codeGeneratorVM = await PopulateSubCategories(codeGeneratorVM);
                        return View("_Create", codeGeneratorVM);
                    }

                }
                else
                {
                    codeGeneratorVM = await PopulateSubCategories(codeGeneratorVM);
                    return View("_Create", codeGeneratorVM);
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                codeGeneratorVM = await PopulateSubCategories(codeGeneratorVM);
                return View("_Create", codeGeneratorVM);
            }
        }


        /// <summary>
        /// Generador de codigos. Este metodo genera codigos de productos para una subcategoria.
        /// Genera faltantes (codigos intermedios) y en caso de no existir faltantes genera nuevos.
        /// Tambien si existe mas de una subcategoria con el mismo prefijo asigna correctamente.
        /// </summary>
        /// <param name="subCategory">SubCategoria de los productos a generar.</param>
        /// <param name="quantity">Cantidad a generar. Por defecto 1</param>
        /// <returns></returns>
        private async Task<List<string>> GetProductCodesBySubCategory(SubCategoryViewModel subCategory, int quantity = 1)
        {
            List<string> responseCode = new List<string>();
            if (subCategory != null)
            {
                //Recuperamos todas las subcategoria con el mismo prefijo para que no haya codigos repetidos.
                var responseSubCategories = await _viewManagerSubCategory.FindBySpecification(new SubCategoriesByPrefixSpecification(subCategory.Prefix));
                var subCaregories = _mapper.Map<List<SubCategoryDTO>>(responseSubCategories.Data);
                //Recuperamos los productos que contengan el array de subcategoria
                var responseProducts = await _viewManagerProduct.FindBySpecification(new ProductsBySubCategoryIdSpecification(subCaregories.Select(x => x.Id).ToList()));

                if (responseProducts.Succeeded)
                {
                    var productsDTO = _mapper.Map<List<ProductDTO>>(responseProducts.Data);

                    if (productsDTO.Count > 0)
                    {
                        //Buscamos los codigos intermedios si no estan ocupados y tambien generamos desde el 0001 si no estan ocupados
                        var codesList = productsDTO.Where(s => s.CodeWithOutPrefix != null)
                                                    .Select(x => x.CodeWithOutPrefix).ToList().OrderBy(x => x.Value);

                        int codeToCompare = 1;
                        for (int i = 0; i < codesList.Count(); i++)
                        {
                            //La idea es que empiece siempre con el 0001 y vaya comparando con los codigos ya existentes.
                            //Entonces siempre se le resta el 1er codigo existente con el codeToCompare.
                            //Si el resultado es >=1 quiere decir que hay espacios libres. Entonces lo agregamos.
                            //Si el resultado es 0, el codigo esta ocupado sale del while y compara con otro codigo existente.
                            while (codeToCompare != codesList.ElementAt(i))
                            {
                                var result = codesList.ElementAt(i) - codeToCompare;

                                if (result >= 1)
                                {
                                    responseCode.Add(subCategory.Prefix + string.Format("{0:0000}", codeToCompare));
                                    codeToCompare++;
                                    if (responseCode.Count == quantity)
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    codeToCompare = codesList.ElementAt(i).Value;
                                }
                            }
                            if (responseCode.Count == quantity)
                            {
                                break;
                            }
                            codeToCompare++;
                        }

                        //Si la cantidad de codigos no es igual a la pedida. Empieza a generar codigos a partir del Max.
                        if (responseCode.Count < quantity)
                        {
                            var maxCode = productsDTO.Select(x => x.CodeWithOutPrefix).Max();

                            if (maxCode != null)
                            {
                                maxCode = maxCode.Value;

                                for (int i = 0; i < quantity; i++)
                                {
                                    maxCode++;
                                    responseCode.Add(subCategory.Prefix + string.Format("{0:0000}", maxCode));
                                    if (responseCode.Count == quantity)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int i = 1; i < quantity + 1; i++)
                        {
                            responseCode.Add(subCategory.Prefix + string.Format("{0:0000}", i));
                        }

                    }
                }
            }
            return responseCode;
        }

        public async Task<CodeGeneratorViewModel> PopulateSubCategories(CodeGeneratorViewModel codeGeneratiorViewModel)
        {
            var subcategoriesResponse = await _viewManagerProduct.GetSubCategories();
            if (subcategoriesResponse.Succeeded)
            {
                var subcategoriesViewModel = _mapper.Map<List<SubCategoryDTO>>(subcategoriesResponse.Data);
                codeGeneratiorViewModel.SubCategories = new SelectList(subcategoriesViewModel, nameof(SubCategoryViewModel.Id), nameof(SubCategoryViewModel.Description), null, null);
            }
            return codeGeneratiorViewModel;
        }

        //Para el alta de Producto
        public async Task<JsonResult> GetCodeAndSubCategoryById(int subcategoryId)
        {
            var subcategoryDTO = await _viewManagerProduct.GetSubCategoryById(subcategoryId);
            var subCategoryViewModel = _mapper.Map<SubCategoryViewModel>(subcategoryDTO.Data);

            if (subcategoryDTO.Succeeded)
            {
                var lastCode = await GetProductCodesBySubCategory(subCategoryViewModel);
                return new JsonResult(new { isValid = true, subcategory = subcategoryDTO.Data, lastCode = lastCode.FirstOrDefault() });
            }
            else
            {
                return new JsonResult(new { isValid = false });
            }
        }
    }
}
