using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.Web.Areas.ProductMod.Models;
using ERP.Web.Abstractions;
using Microsoft.Extensions.Localization;
using ERP.Language;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ERP.Application.Constants;
using Microsoft.AspNetCore.Mvc.Rendering;
using ERP.Application.Interfaces.ViewManagers.ProductMod;
using ERP.Application.DTOs.Entities.ProductMod;

namespace ERP.Web.Areas.ProductMod.Controllers
{
    [Area("ProductMod")]
    public class SubCategoryController : BaseController<SubCategoryController>
    {
        private readonly ISubCategoryViewManager _viewManager;
        private readonly IStringLocalizer<SharedResource> _localizer;
        public SubCategoryController(ISubCategoryViewManager viewManager, IStringLocalizer<SharedResource> localizer)
        {
            _viewManager = viewManager;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            var model = new SubCategoryViewModel();
            return View(model);
        }

        public async Task<IActionResult> LoadAll()
        {
            var response = await _viewManager.LoadAllWithCategory();
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<SubCategoryViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }

        [Authorize(Policy = Permissions.SubCategories.View)]
        public async Task<JsonResult> OnGetCreateOrEdit(int id = 0)
        {
            var categoriesResponse = await _viewManager.GetAllCategories();
            if (id == 0)
            {
                var subCategoryViewModel = new SubCategoryViewModel();
                if (categoriesResponse.Succeeded)
                {
                    var categoriesViewModel = _mapper.Map<List<CategoryDTO>>(categoriesResponse.Data);
                    subCategoryViewModel.Categories = new SelectList(categoriesViewModel, nameof(CategoryViewModel.Id), nameof(CategoryViewModel.Description), null, null);
                }
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", subCategoryViewModel) });
            }
            else
            {
                var response = await _viewManager.GetByIdAsync(id);
                if (response.Succeeded)
                {

                    var subCategoryViewModel = _mapper.Map<SubCategoryViewModel>(response.Data);
                    if (categoriesResponse.Succeeded)
                    {
                        var categoriesViewModel = _mapper.Map<List<CategoryDTO>>(categoriesResponse.Data);
                        subCategoryViewModel.Categories = new SelectList(categoriesViewModel, nameof(CategoryViewModel.Id), nameof(CategoryViewModel.Description), null, null);
                    }
                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", subCategoryViewModel) });
                }
                return null;
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(int id, SubCategoryViewModel subCategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (id == 0)
                    {
                        var subCategoryDTO = _mapper.Map<SubCategoryDTO>(subCategory);
                        subCategoryDTO.Prefix = subCategoryDTO.Prefix.ToUpper();

                        var result = await _viewManager.CreateAsync(subCategoryDTO);
                        if (result.Succeeded)
                        {
                            id = result.Data;
                            _notify.Success(_localizer["Sub Category with ID {0} Created.", result.Data]);
                        }
                        else _notify.Error(result.Message);
                    }
                    else
                    {

                        var subCategoryDTO = _mapper.Map<SubCategoryDTO>(subCategory);
                        subCategoryDTO.Prefix = subCategoryDTO.Prefix.ToUpper();

                        var result = await _viewManager.UpdateAsync(subCategoryDTO);
                        if (result.Succeeded) _notify.Information(_localizer["Sub Category with ID {0} Update.", result.Data]);
                    }

                    var response = await _viewManager.LoadAllWithCategory();
                    if (response.Succeeded)
                    {
                        var viewModel = _mapper.Map<List<SubCategoryViewModel>>(response.Data);
                        var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", viewModel);
                        return new JsonResult(new { isValid = true, html = html });
                    }
                    else
                    {
                        _notify.Error(response.Message);
                        return null;
                    }
                }
                else
                {
                    var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", subCategory);
                    return new JsonResult(new { isValid = false, html = html });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", subCategory);
                return new JsonResult(new { isValid = false, html = html });
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostDelete(int id)
        {
            var deleteCommand = await _viewManager.DeleteAsync(id);
            if (deleteCommand.Succeeded)
            {
                _notify.Information(_localizer["Sub Category with ID {0} Deleted.", id]);
                var response = await _viewManager.LoadAllWithCategory();
                if (response.Succeeded)
                {
                    var subCategoryViewModel = _mapper.Map<List<SubCategoryViewModel>>(response.Data);
                    var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", subCategoryViewModel);
                    return new JsonResult(new { isValid = true, html = html });
                }
                else
                {
                    _notify.Error(response.Message);
                    return new JsonResult(new { isValid = false });
                }
            }
            else
            {
                _notify.Error(deleteCommand.Message);
                return new JsonResult(new { isValid = false });
            }
        }

        public async Task<JsonResult> GetSubCategoryById(int subcategoryId)
        {
            var subcategoryDTO = await _viewManager.GetByIdAsync(subcategoryId);
            var subCategoryViewModel = _mapper.Map<SubCategoryViewModel>(subcategoryDTO.Data);

            if (subcategoryDTO.Succeeded)
            {
                return new JsonResult(new { isValid = true, subcategory = subcategoryDTO.Data });
            }
            else
            {
                return new JsonResult(new { isValid = false });
            }
        }



    }
}
