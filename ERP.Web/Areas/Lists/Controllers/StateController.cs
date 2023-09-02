using ERP.Application.Constants;
using ERP.Application.DTOs.Entities.Commons;
using ERP.Application.DTOs.Entities.Lists;
using ERP.Application.Interfaces.ViewManagers.Commons;
using ERP.Application.Interfaces.ViewManagers.Lists;
using ERP.Language;
using ERP.Web.Abstractions;
using ERP.Web.Areas.Commons.Models;
using ERP.Web.Areas.Lists.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Web.Areas.Lists.Controllers
{
    [Area("Lists")]
    public class StateController : BaseController<StateController>
    {
        private readonly IStateViewManager _viewManager;
        private readonly ICountryViewManager _countryViewManager;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public StateController(IStateViewManager viewManager, ICountryViewManager countryViewManager, IStringLocalizer<SharedResource> localizer)
        {
            _viewManager = viewManager;
            _countryViewManager = countryViewManager;
            _localizer = localizer;
        }

        // Index
        public IActionResult Index()
        {
            var model = new StateViewModel();
            return View(model);
        }

        public async Task<IActionResult> LoadAll()
        {
            var response = await _viewManager.LoadAll();
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<StateViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }

        [Authorize(Policy = Permissions.States.View)]
        public async Task<JsonResult> OnGetCreateOrEdit(int id = 0)
        {

            // Implementar éste método para traerme en el combo los países
            var countriesResponse = await _countryViewManager.LoadAll();

            if (id == 0)
            {
                var stateViewModel = new StateViewModel();

                if (countriesResponse.Succeeded)
                {
                    var countryViewModel = _mapper.Map<List<CountryDTO>>(countriesResponse.Data);
                    stateViewModel.Countries = new SelectList(countryViewModel, nameof(CountryViewModel.Id), nameof(CountryViewModel.Description), null, null);
                }

                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", stateViewModel) });
            }
            else
            {
                var response = await _viewManager.GetByIdAsync(id);
                if (response.Succeeded)
                {
                    var stateViewModel = _mapper.Map<StateViewModel>(response.Data);

                    if (countriesResponse.Succeeded)
                    {
                        var countryViewModel = _mapper.Map<List<CountryViewModel>>(countriesResponse.Data);
                        stateViewModel.Countries = new SelectList(countryViewModel, nameof(CountryViewModel.Id), nameof(CountryViewModel.Description), null, null);
                    }

                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", stateViewModel) });
                }
                return null;
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(int id, StateViewModel state)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (id == 0)
                    {
                        // Create
                        var stateDTO = _mapper.Map<StateDTO>(state);
                        var result = await _viewManager.CreateAsync(stateDTO);
                        if (result.Succeeded)
                        {
                            id = result.Data;
                            _notify.Success(_localizer["State with ID {0} created.", result.Data]);
                        }
                        else
                        {
                            _notify.Error(result.Message);
                        }
                    }
                    else
                    {
                        // Update
                        var stateDTO = _mapper.Map<StateDTO>(state);
                        var result = await _viewManager.UpdateAsync(stateDTO);
                        if (result.Succeeded) _notify.Information(_localizer["State with ID {0} update.", result.Data]);
                    }
                    var response = await _viewManager.LoadAll();
                    if (response.Succeeded)
                    {
                        var viewModel = _mapper.Map<List<StateViewModel>>(response.Data);
                        var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", viewModel);
                        return new JsonResult(new { isValid = true, html });
                    }
                    else
                    {
                        _notify.Error(response.Message);
                        return null;
                    }
                }
                else
                {
                    var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", state);
                    return new JsonResult(new { isValid = false, html });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", state);
                return new JsonResult(new { isValid = false, html });
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostDelete(int id)
        {
            var deleteCommand = await _viewManager.DeleteAsync(id);
            if (deleteCommand.Succeeded)
            {
                // Delete
                _notify.Information(_localizer["State with ID {0} deleted.", id]);
                var response = await _viewManager.LoadAll();
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<StateViewModel>>(response.Data);
                    var html = _viewRenderer.RenderViewToStringAsync("_ViewAll", viewModel);
                    return new JsonResult(new { isValid = true, html });
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

    }
}
