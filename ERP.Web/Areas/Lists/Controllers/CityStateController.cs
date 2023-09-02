using AspNetCoreHero.Results;
using ERP.Application.Constants;
using ERP.Application.DTOs.Entities.Lists;
using ERP.Application.Interfaces.ViewManagers.Commons;
using ERP.Application.Interfaces.ViewManagers.Lists;
using ERP.Application.Specification.Lists;
using ERP.Language;
using ERP.Web.Abstractions;
using ERP.Web.Areas.Commons.Models;
using ERP.Web.Areas.Lists.Models;
using ERP.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Web.Areas.Lists.Controllers
{
    [Area("Lists")]
    public class CityStateController : BaseController<CityStateController>
    {
        private readonly IStringLocalizer<SharedResource> _localizer;

        private readonly ICityViewManager _cityViewManager;
        private readonly IStateViewManager _stateViewManager;
        private readonly ICountryViewManager _countryViewManager;

        public CityStateController(IStringLocalizer<SharedResource> localizer, ICityViewManager cityViewManager, IStateViewManager stateViewManager, ICountryViewManager countryViewManager)
        {
            _localizer = localizer;
            _cityViewManager = cityViewManager;
            _stateViewManager = stateViewManager;
            _countryViewManager = countryViewManager;
        }

        public IActionResult Index()
        {
            var model = new CityStateViewModel();
            return View();
        }

        public async Task<IActionResult> GetCitiesAndStatesRecords(DatatableParamsViewModel param, string sSearch_2, string sSearch_3, string sSearch_4)
        {
            var response = await _cityViewManager.FindWithSpecification(new CitySpecification());
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<CityStateViewModel>>(response.Data);
                IEnumerable<CityStateViewModel> citiesAndStates = viewModel;

                citiesAndStates = citiesAndStates.Where(x => x.City_Name != "");
                citiesAndStates = citiesAndStates.Where(x => x.State_Name != "");

                if (!string.IsNullOrEmpty(sSearch_2))
                {
                    citiesAndStates = citiesAndStates.Where(x => x.State_Name.ToLower().Contains(sSearch_2.ToLower())).ToList();
                }
                if (!string.IsNullOrEmpty(sSearch_3))
                {
                    citiesAndStates = citiesAndStates.Where(x => x.City_Name.ToLower().Contains(sSearch_3.ToLower())).ToList();
                }
                if (!string.IsNullOrEmpty(sSearch_4))
                {
                    citiesAndStates = citiesAndStates.Where(x => x.City_ZipCode.ToLower().Contains(sSearch_4.ToLower())).ToList();
                }

                var sortColumnIndex = param.iSortCol_0;
                var sortDirection = param.sSortDir_0;

                if (sortColumnIndex == 2)
                {
                    citiesAndStates = sortDirection == "asc" ? citiesAndStates.OrderBy(c => c.State_Name) : citiesAndStates.OrderByDescending(c => c.State_Name);
                }
                else if (sortColumnIndex == 3)
                {
                    citiesAndStates = sortDirection == "asc" ? citiesAndStates.OrderBy(c => c.City_Name) : citiesAndStates.OrderByDescending(c => c.City_Name);
                }
                else if (sortColumnIndex == 4)
                {
                    citiesAndStates = sortDirection == "asc" ? citiesAndStates.OrderBy(c => c.City_ZipCode) : citiesAndStates.OrderByDescending(c => c.City_ZipCode);
                }

                var displayResult = citiesAndStates.Skip(param.iDisplayStart)
                    .Take(param.iDisplayLength).ToList();

                var totalRecords = citiesAndStates.Count();

                return Json(new
                {
                    param.sEcho,
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


        [Authorize(Policy = Permissions.States.View)]
        public async Task<JsonResult> OnGetCreateOrEditState(int id = 0)
        {
            var countriesResponse = await _countryViewManager.LoadAll();

            if (id == 0)
            {
                var stateViewModel = new StateViewModel();

                if (countriesResponse.Succeeded)
                {
                    List<CountryViewModel> countryViewModel = new List<CountryViewModel>();
                    // Si es rol Ventas me traigo todos los países menos Argentina
                    if (User.IsInRole("Ventas"))
                    {
                        countryViewModel = _mapper.Map<List<CountryViewModel>>(countriesResponse.Data.Where(c => c.Id != 61));
                    }
                    else
                    {
                        countryViewModel = _mapper.Map<List<CountryViewModel>>(countriesResponse.Data);
                    }

                    stateViewModel.Countries = new SelectList(countryViewModel, nameof(CountryViewModel.Id), nameof(CountryViewModel.Description), null, null);
                }

                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEditState", stateViewModel) });
            }
            else
            {
                var response = await _stateViewManager.GetByIdAsync(id);
                if (response.Succeeded)
                {
                    var stateViewModel = _mapper.Map<StateViewModel>(response.Data);

                    if (countriesResponse.Succeeded)
                    {
                        List<CountryViewModel> countryViewModel = new List<CountryViewModel>();
                        // Si es rol Ventas me traigo todos los países menos Argentina
                        if (User.IsInRole("Ventas"))
                        {
                            countryViewModel = _mapper.Map<List<CountryViewModel>>(countriesResponse.Data.Where(c => c.Id != 61));
                        }
                        else
                        {
                            countryViewModel = _mapper.Map<List<CountryViewModel>>(countriesResponse.Data);
                        }

                        stateViewModel.Countries = new SelectList(countryViewModel, nameof(CountryViewModel.Id), nameof(CountryViewModel.Description), null, null);
                    }

                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEditState", stateViewModel) });
                }
                return null;
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEditState(int id, StateViewModel state)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (id == 0)
                    {
                        // Create
                        var stateDTO = _mapper.Map<StateDTO>(state);
                        var result = await _stateViewManager.CreateAsync(stateDTO);
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
                        var result = await _stateViewManager.UpdateAsync(stateDTO);
                        if (result.Succeeded) _notify.Information(_localizer["State with ID {0} update.", result.Data]);
                    }

                    // Return datatable after create or update
                    var cityStateVM = new CityStateViewModel();
                    cityStateVM.State_Id = state.Id;
                    cityStateVM.State_Name = state.Name;
                    cityStateVM.CountryId = state.CountryId;
                    cityStateVM.State_ConcurrencyToken = state.ConcurrencyToken;

                    var html = await _viewRenderer.RenderViewToStringAsync("Index", cityStateVM);
                    return new JsonResult(new { isValid = true, html });

                }
                else
                {
                    var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEditState", state);
                    return new JsonResult(new { isValid = false, html });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEditState", state);
                return new JsonResult(new { isValid = false, html });
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostDeleteState(int id)
        {
            try
            {
                var deleteCommand = await _stateViewManager.DeleteAsync(id);
                if (deleteCommand.Succeeded)
                {
                    _notify.Information(_localizer["State with ID {0} deleted.", id]);
                    return new JsonResult(true);
                }
                else
                {
                    _notify.Error(deleteCommand.Message);
                    return new JsonResult(false);
                }
            }
            catch (Exception ex)
            {
                _notify.Warning("No se puede eliminar una provincia asociada a un cliente.");
                return new JsonResult(false);
            }
        }


        [Authorize(Policy = Permissions.Cities.View)]
        public async Task<JsonResult> OnGetCreateOrEditCity(int id = 0)
        {
            Result<IReadOnlyList<StateDTO>> statesResponse = null;
            // Si es rol Ventas me traigo todos los países menos Argentina
            if (User.IsInRole("Ventas"))
            {
                statesResponse = await _stateViewManager.FindBySpecification(new StatesExcludeCountryIdSpecification(61));
            }
            else
            {
                statesResponse = await _stateViewManager.LoadAll();
            }

            if (id == 0)
            {
                var cityViewModel = new CityViewModel();

                if (statesResponse.Succeeded)
                {
                    var stateViewModel = _mapper.Map<List<StateViewModel>>(statesResponse.Data);
                    cityViewModel.States = new SelectList(stateViewModel, nameof(StateViewModel.Id), nameof(StateViewModel.Name), null, null);
                }

                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEditCity", cityViewModel) });
            }
            else
            {
                var response = await _cityViewManager.GetByIdAsync(id);
                if (response.Succeeded)
                {
                    var cityViewModel = _mapper.Map<CityViewModel>(response.Data);

                    if (statesResponse.Succeeded)
                    {
                        var stateViewModel = _mapper.Map<List<StateViewModel>>(statesResponse.Data);
                        cityViewModel.States = new SelectList(stateViewModel, nameof(StateViewModel.Id), nameof(StateViewModel.Name), null, null);
                    }

                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEditCity", cityViewModel) });
                }
                return null;
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEditCity(int id, CityViewModel city)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (id == 0)
                    {
                        // Create
                        var cityDTO = _mapper.Map<CityDTO>(city);
                        var result = await _cityViewManager.CreateAsync(cityDTO);
                        if (result.Succeeded)
                        {
                            id = result.Data;
                            _notify.Success(_localizer["City with ID {0} created.", result.Data]);
                        }
                        else
                        {
                            _notify.Error(result.Message);
                        }
                    }
                    else
                    {
                        // Update
                        var cityDTO = _mapper.Map<CityDTO>(city);
                        var result = await _cityViewManager.UpdateAsync(cityDTO);
                        if (result.Succeeded) _notify.Information(_localizer["City with ID {0} update.", result.Data]);
                    }

                    // Return datatable after create or update
                    var cityStateVM = new CityStateViewModel();
                    cityStateVM.City_Id = city.Id;
                    cityStateVM.City_Name = city.Name;
                    cityStateVM.City_ZipCode = city.ZipCode;
                    cityStateVM.StateId = city.StateId;
                    cityStateVM.City_ConcurrencyToken = city.ConcurrencyToken;

                    var html = await _viewRenderer.RenderViewToStringAsync("Index", cityStateVM);
                    return new JsonResult(new { isValid = true, html });
                }
                else
                {
                    var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEditCity", city);
                    return new JsonResult(new { isValid = false, html });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEditCity", city);
                return new JsonResult(new { isValid = false, html });
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostDeleteCity(int id)
        {
            try
            {
                var deleteCommand = await _cityViewManager.DeleteAsync(id);
                if (deleteCommand.Succeeded)
                {
                    _notify.Information(_localizer["City with ID {0} deleted.", id]);
                    return new JsonResult(true);
                }
                else
                {
                    _notify.Error(deleteCommand.Message);
                    return new JsonResult(false);
                }
            }
            catch (Exception ex)
            {
                _notify.Warning(_localizer["A city associated with client cannot be deleted."]);
                return new JsonResult(false);
            }

        }
    }
}
