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
using ERP.Application.Interfaces.ViewManagers.ProductMod;
using ERP.Application.DTOs.Entities.ProductMod;

namespace ERP.Web.Areas.ProductMod.Controllers
{
    [Area("ProductMod")]
    public class CategoryController : BaseController<CategoryController>
    {
        private readonly ICategoryViewManager _viewManager;
        private readonly IStringLocalizer<SharedResource> _localizer;
        public CategoryController(ICategoryViewManager viewManager, IStringLocalizer<SharedResource> localizer)
        {
            _viewManager = viewManager;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            var model = new CategoryViewModel();
            return View(model);
        }

        public async Task<IActionResult> LoadAll()
        {
            var response = await _viewManager.LoadAll();
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<CategoryViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }

        [Authorize(Policy = Permissions.Categories.View)]
        public async Task<JsonResult> OnGetCreateOrEdit(int id = 0)
        {
            if (id == 0)
            {
                var categoryViewModel = new CategoryViewModel();
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", categoryViewModel) });
            }
            else
            {
                var response = await _viewManager.GetByIdAsync(id);
                if (response.Succeeded)
                {
                    var categoryViewModel = _mapper.Map<CategoryViewModel>(response.Data);
                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", categoryViewModel) });
                }
                return null;
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(int id, CategoryViewModel category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (id == 0)
                    {
                        var categoryDTO = _mapper.Map<CategoryDTO>(category);
                        var result = await _viewManager.CreateAsync(categoryDTO);
                        if (result.Succeeded)
                        {
                            id = result.Data;
                            _notify.Success(_localizer["Category with ID {0} Created.", result.Data]);
                        }
                        else _notify.Error(result.Message);
                    }
                    else
                    {
                        var categoryDTO = _mapper.Map<CategoryDTO>(category);
                        var result = await _viewManager.UpdateAsync(categoryDTO);
                        if (result.Succeeded) _notify.Information(_localizer["Category with ID {0} Update.", result.Data]);
                    }

                    var response = await _viewManager.LoadAll();
                    if (response.Succeeded)
                    {
                        var categoryViewModel = _mapper.Map<List<CategoryViewModel>>(response.Data);
                        var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", categoryViewModel);
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
                    var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", category);
                    return new JsonResult(new { isValid = false, html = html });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", category);
                return new JsonResult(new { isValid = false, html = html });
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostDelete(int id)
        {
            var deleteCommand = await _viewManager.DeleteAsync(id);
            if (deleteCommand.Succeeded)
            {
                _notify.Information(_localizer["Category with ID {0} Deleted.", id]);
                var response = await _viewManager.LoadAll();
                if (response.Succeeded)
                {
                    var categoryViewModel = _mapper.Map<List<CategoryViewModel>>(response.Data);
                    var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", categoryViewModel);
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



    }
}
