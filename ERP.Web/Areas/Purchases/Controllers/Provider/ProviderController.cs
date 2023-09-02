using ERP.Application.Constants;
using ERP.Application.DTOs.Entities.Commons;
using ERP.Application.DTOs.Entities.Purchases.Providers;
using ERP.Application.Interfaces.ViewManagers.Commons;
using ERP.Application.Interfaces.ViewManagers.Lists;
using ERP.Application.Interfaces.ViewManagers.ProductMod;
using ERP.Application.Interfaces.ViewManagers.Purchases.Providers;
using ERP.Application.Specification.Commons.UnitMeasureSpecifications;
using ERP.Application.Specification.Purchases.Providers;
using ERP.Language;
using ERP.Web.Abstractions;
using ERP.Web.Areas.Commons.Models;
using ERP.Web.Areas.Lists.Models;
using ERP.Web.Areas.ProductMod.Models;
using ERP.Web.Areas.Purchases.Models.Providers;
using ERP.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Web.Areas.Purchases.Controllers.Provider
{
    [Area("Purchases")]
    public class ProviderController : BaseController<ProviderController>
    {
        private readonly IProviderViewManager _providerViewManager;
        private readonly IStringLocalizer<SharedResource> _localizer;

        private readonly ICountryViewManager _countryViewManager;
        private readonly IDocumentTypeViewManager _documentTypeViewManager;
        private readonly IIVAConditionViewManager _iVAConditionViewManager;
        private readonly IStateViewManager _stateViewManager;
        private readonly ICityViewManager _cityViewManager;

        private readonly IContactViewManager _contactViewManager;
        private readonly ISubsidiaryViewManager _subsidiaryViewManager;
        private readonly IFinancialInformationViewManager _financialInformationViewManager;
        private readonly IRelProviderStationViewManager _relProviderStationViewManager;
        private readonly IStationViewManager _stationViewManager;
        private readonly IUnitMeasureViewManager _unitMeasureViewManager;

        public ProviderController(IProviderViewManager providerViewManager, IStringLocalizer<SharedResource> localizer, ICountryViewManager countryViewManager, IDocumentTypeViewManager documentTypeViewManager, IIVAConditionViewManager iVAConditionViewManager, IStateViewManager stateViewManager, ICityViewManager cityViewManager, IContactViewManager contactViewManager, ISubsidiaryViewManager subsidiaryViewManager, IFinancialInformationViewManager financialInformationViewManager, IRelProviderStationViewManager relProviderStationViewManager, IStationViewManager stationViewManager, IUnitMeasureViewManager unitMeasureViewManager)
        {
            _providerViewManager = providerViewManager;
            _localizer = localizer;
            _countryViewManager = countryViewManager;
            _documentTypeViewManager = documentTypeViewManager;
            _iVAConditionViewManager = iVAConditionViewManager;
            _stateViewManager = stateViewManager;
            _cityViewManager = cityViewManager;
            _contactViewManager = contactViewManager;
            _subsidiaryViewManager = subsidiaryViewManager;
            _financialInformationViewManager = financialInformationViewManager;
            _relProviderStationViewManager = relProviderStationViewManager;
            _stationViewManager = stationViewManager;
            _unitMeasureViewManager = unitMeasureViewManager;
        }

        public IActionResult Index()
        {
            var model = new ProviderViewModel();
            return View(model);
        }

        public async Task<IActionResult> LoadAll()
        {
            var response = await _providerViewManager.LoadAll();
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<ProviderViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }

        [Authorize(Policy = Permissions.Providers.View)]
        public async Task<JsonResult> OnGetCreateOrEdit(int id = 0, bool calledFromAnotherArea = false)
        {
            var countriesResponse = await _countryViewManager.LoadAll();
            var documentTypesResponse = await _documentTypeViewManager.LoadAll();
            var ivaConditionsResponse = await _iVAConditionViewManager.LoadAll();

            if (id == 0)
            {
                // Create new provider
                var providerViewModel = new ProviderViewModel();

                if (countriesResponse.Succeeded)
                {
                    var countryViewModel = _mapper.Map<List<CountryDTO>>(countriesResponse.Data);
                    providerViewModel.Countries = new SelectList(countryViewModel, nameof(CountryViewModel.Id), nameof(CountryViewModel.Denomination), null, null);
                }

                if (documentTypesResponse.Succeeded)
                {
                    var documentTypeViewModel = _mapper.Map<List<DocumentTypeDTO>>(documentTypesResponse.Data);
                    providerViewModel.DocumentTypes = new SelectList(documentTypeViewModel, nameof(DocumentTypeViewModel.Id), nameof(DocumentTypeViewModel.Description), null, null);
                }

                if (ivaConditionsResponse.Succeeded)
                {
                    var ivaConditionTypeViewModel = _mapper.Map<List<IVAConditionViewModel>>(ivaConditionsResponse.Data);
                    providerViewModel.IVAConditions = new SelectList(ivaConditionTypeViewModel, nameof(IVAConditionViewModel.Code), nameof(IVAConditionViewModel.Description), null, null);
                }

                providerViewModel.calledFromAnotherArea = calledFromAnotherArea;

                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", providerViewModel) });

            }
            else
            {
                // Update provider
                //var response = await _providerViewManager.GetByIdAsync(id);
                var response = await _providerViewManager.FindBySpecification(new ProviderByIdSpecifications(id));
                if (response.Succeeded)
                {
                    var providerViewModel = _mapper.Map<ProviderViewModel>(response.Data.First());

                    // Load contacts
                    var contactsByProvider = await _contactViewManager.GetContactsByProviderId(id);
                    if (contactsByProvider.Count > 0)
                    {
                        providerViewModel.Contacts = _mapper.Map<List<ContactViewModel>>(contactsByProvider);
                    }

                    // Load subsidiaries
                    var subsidiariesByProvider = await _subsidiaryViewManager.GetSubsidiariesByProviderId(id);
                    if (subsidiariesByProvider.Count > 0)
                    {
                        providerViewModel.Subsidiaries = _mapper.Map<List<SubsidiaryViewModel>>(subsidiariesByProvider);
                        foreach (var s in providerViewModel.Subsidiaries)
                        {
                            var countriesViewModel = _mapper.Map<List<CountryViewModel>>(countriesResponse.Data);
                            //s.Countries = countriesViewModel;
                            s.CountriesList = new SelectList(countriesViewModel, nameof(CountryViewModel.Id), nameof(CountryViewModel.Denomination), null, null);

                            var states = await _stateViewManager.GetStatesByCountryIdAsync(s.CountryId);
                            var statesViewModel = _mapper.Map<List<StateViewModel>>(states);
                            //s.States = statesViewModel;
                            s.StatesList = new SelectList(statesViewModel, nameof(StateViewModel.Id), nameof(StateViewModel.Name), null, null);

                            var cities = await _cityViewManager.GetCitiesByStateIdAsync(s.StateId);
                            var citiesViewModel = _mapper.Map<List<CityViewModel>>(cities);
                            //s.Cities = citiesViewModel;
                            s.CitiesList = new SelectList(citiesViewModel, nameof(CityViewModel.Id), nameof(CityViewModel.Name), null, null);
                        }
                    }

                    // Load financial information
                    var financialInfosByProvider = await _financialInformationViewManager.GetFinancialInfosByProviderId(id);
                    if (financialInfosByProvider.Count > 0)
                    {
                        providerViewModel.FinancialInformations = _mapper.Map<List<FinancialInformationViewModel>>(financialInfosByProvider);
                    }

                    if (countriesResponse.Succeeded)
                    {
                        var countryViewModel = _mapper.Map<List<CountryViewModel>>(countriesResponse.Data);
                        providerViewModel.Countries = new SelectList(countryViewModel, nameof(CountryViewModel.Id), nameof(CountryViewModel.Denomination), null, null);
                    }

                    if (providerViewModel.StateId != null)
                    {
                        var states = await _stateViewManager.GetStatesByCountryIdAsync(providerViewModel.CountryId);
                        var statesViewModel = _mapper.Map<List<StateViewModel>>(states);
                        providerViewModel.States = new SelectList(statesViewModel, nameof(StateViewModel.Id), nameof(StateViewModel.Name), null, null);
                    }

                    if (providerViewModel.CityId != null)
                    {
                        var cities = await _cityViewManager.GetCitiesByStateIdAsync(providerViewModel.StateId);
                        var citiesViewModel = _mapper.Map<List<CityViewModel>>(cities);
                        providerViewModel.Cities = new SelectList(citiesViewModel, nameof(CityViewModel.Id), nameof(CityViewModel.Name), null, null);
                    }

                    if (documentTypesResponse.Succeeded)
                    {
                        var documentTypeViewModel = _mapper.Map<List<DocumentTypeViewModel>>(documentTypesResponse.Data);
                        providerViewModel.DocumentTypes = new SelectList(documentTypeViewModel, nameof(DocumentTypeViewModel.Id), nameof(DocumentTypeViewModel.Description), null, null);
                    }

                    if (ivaConditionsResponse.Succeeded)
                    {
                        var ivaConditionTypeViewModel = _mapper.Map<List<IVAConditionViewModel>>(ivaConditionsResponse.Data);
                        providerViewModel.IVAConditions = new SelectList(ivaConditionTypeViewModel, nameof(IVAConditionViewModel.Code), nameof(IVAConditionViewModel.Description), null, null);
                    }

                    providerViewModel.calledFromAnotherArea = calledFromAnotherArea;

                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", providerViewModel) });
                }

                return null;

            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(int id, ProviderViewModel provider)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (provider.CityId == -1)
                    {
                        provider.CityId = null;
                    }

                    if (provider.CountryId == -1 || provider.CountryId == 0)
                    {
                        _notify.Warning(_localizer["You must select a country."]);
                        return new JsonResult(new { isValid = false, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", provider) });
                    }
                    if (provider.StateId == -1 || provider.StateId == 0)
                    {
                        provider.StateId = null;
                    }
                    if (provider.DocumentTypeId == -1)
                    {
                        _notify.Warning(_localizer["You must select a document type."]);
                        return new JsonResult(new { isValid = false, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", provider) });
                    }
                    if (provider.IVAConditionId == -1)
                    {
                        _notify.Warning(_localizer["You must select a IVA condition."]);
                        return new JsonResult(new { isValid = false, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", provider) });
                    }

                    if (provider.Subsidiaries.Where(s => s.CountryId == -1).Count() > 0)
                    {
                        _notify.Warning(_localizer["You must select a country."]);
                        return new JsonResult(new { isValid = false, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", provider) });
                    }

                    if (provider.Name == null || provider.Name == "")
                    {
                        provider.Name = provider.BusinessName;
                    }
                    else if (provider.BusinessName == null || provider.BusinessName == "")
                    {
                        provider.BusinessName = provider.Name;
                    }

                    if (id == 0)
                    {
                        // Create
                        var contactsProvider = provider.Contacts.Where(c => c != null).ToList();
                        provider.Contacts = contactsProvider;

                        var subsidiariesProvider = provider.Subsidiaries.Where(s => s != null).ToList();
                        provider.Subsidiaries = subsidiariesProvider;

                        var financialInfoProvider = provider.FinancialInformations.Where(f => f != null).ToList();
                        provider.FinancialInformations = financialInfoProvider;

                        var providerDTO = _mapper.Map<ProviderDTO>(provider);
                        var result = await _providerViewManager.CreateAsync(providerDTO);
                        if (result.Succeeded)
                        {
                            id = result.Data;
                            _notify.Success(_localizer["Provider with ID {0} created.", result.Data]);
                        }
                        else
                        {
                            _notify.Error(result.Message);
                        }
                    }
                    else
                    {
                        // Update
                        var contactsProvider = provider.Contacts.Where(x => x != null).ToList();
                        provider.Contacts = contactsProvider;

                        var subsidiariesProvider = provider.Subsidiaries.Where(x => x != null).ToList();
                        provider.Subsidiaries = subsidiariesProvider;

                        foreach (var s in provider.Subsidiaries.Where(s => s.StateId == 0))
                        {
                            s.StateId = null;
                        }

                        var financialInfoProvider = provider.FinancialInformations.Where(x => x != null).ToList();
                        provider.FinancialInformations = financialInfoProvider;

                        var providerDTO = _mapper.Map<ProviderDTO>(provider);
                        var result = await _providerViewManager.UpdateAsync(providerDTO);

                        if (result.Succeeded)
                        {
                            id = result.Data;
                            _notify.Information(_localizer["Provider with ID {0} update.", result.Data]);
                        }
                    }

                    var response = await _providerViewManager.LoadAll();
                    if (response.Succeeded)
                    {
                        var viewModel = _mapper.Map<List<ProviderViewModel>>(response.Data);
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
                    var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", provider);
                    return new JsonResult(new { isValid = false, html = html });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", provider);
                return new JsonResult(new { isValid = false, html = html });
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostDelete(int id)
        {
            try
            {
                var deleteCommand = await _providerViewManager.DeleteAsync(id);
                if (deleteCommand.Succeeded)
                {
                    _notify.Information(_localizer["Provider with ID {0} deleted.", id]);
                    var response = await _providerViewManager.LoadAll();
                    if (response.Succeeded)
                    {
                        var viewModel = _mapper.Map<List<ProviderViewModel>>(response.Data);
                        var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", viewModel);
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
            catch (Exception ex)
            {
                _notify.Warning("No se pudo eliminar este proveedor, tal vez tenga información asociada en otra parte del sistema.");
                return new JsonResult(new { isValid = false });
            }
        }

        public async Task<JsonResult> SearchCountries(string q)
        {
            var countries = await _countryViewManager.LoadAll();
            if (countries.Succeeded)
            {
                List<Select2ViewModel> mapCountriesOnSelect2 = new List<Select2ViewModel>();
                var countriesViewModel = _mapper.Map<List<CountryViewModel>>(countries.Data);

                foreach (var country in countriesViewModel)
                {
                    mapCountriesOnSelect2.Add(new Select2ViewModel() { Id = country.Id, Text = country.Denomination });
                }

                if (!(string.IsNullOrEmpty(q) || string.IsNullOrWhiteSpace(q)))
                {
                    mapCountriesOnSelect2 = mapCountriesOnSelect2.Where(x => x.Text.ToLower().StartsWith(q.ToLower())).ToList();
                }
                return new JsonResult(new { countries = mapCountriesOnSelect2 });
            }
            return null;
        }

        public async Task<JsonResult> SearchStates(string q, int countryId)
        {
            var states = await _stateViewManager.GetStatesByCountryIdAsync(countryId); ;

            List<Select2ViewModel> mapStatesOnSelect2 = new List<Select2ViewModel>();
            var statesViewModel = _mapper.Map<List<StateViewModel>>(states);

            foreach (var state in statesViewModel)
            {
                mapStatesOnSelect2.Add(new Select2ViewModel() { Id = state.Id, Text = state.Name });
            }

            if (!(string.IsNullOrEmpty(q) || string.IsNullOrWhiteSpace(q)))
            {
                mapStatesOnSelect2 = mapStatesOnSelect2.Where(x => x.Text.ToLower().StartsWith(q.ToLower())).ToList();
            }
            return new JsonResult(new { states = mapStatesOnSelect2 });

        }

        public async Task<JsonResult> SearchCities(string q, int stateId)
        {
            var cities = await _cityViewManager.GetCitiesByStateIdAsync(stateId); ;

            List<Select2ViewModel> mapCitiesOnSelect2 = new List<Select2ViewModel>();
            var citiesViewModel = _mapper.Map<List<CityViewModel>>(cities);

            foreach (var city in citiesViewModel)
            {
                mapCitiesOnSelect2.Add(new Select2ViewModel() { Id = city.Id, Text = city.Name });
            }

            if (!(string.IsNullOrEmpty(q) || string.IsNullOrWhiteSpace(q)))
            {
                mapCitiesOnSelect2 = mapCitiesOnSelect2.Where(x => x.Text.ToLower().StartsWith(q.ToLower())).ToList();
            }
            return new JsonResult(new { cities = mapCitiesOnSelect2 });

        }

        [HttpGet]
        public async Task<JsonResult> GetStatesByCountryId(int countryId)
        {
            var statesByCountryId = await _stateViewManager.GetStatesByCountryIdAsync(countryId);
            return Json(statesByCountryId);
        }

        [HttpGet]
        public async Task<JsonResult> GetCitiesByStateId(int stateId)
        {
            var citiesByStateId = await _cityViewManager.GetCitiesByStateIdAsync(stateId);
            return Json(citiesByStateId);
        }

        [HttpGet]
        public async Task<JsonResult> GetPostalCodeByCityId(int cityId)
        {
            var city = await _cityViewManager.GetByIdAsync(cityId);
            if (city.Succeeded)
            {
                var cityViewModel = _mapper.Map<CityViewModel>(city.Data);
                return Json(cityViewModel.ZipCode);
            }
            else
            {
                return null;
            }

        }

        public IActionResult WorkStationByProvider(int id, string providerName)
        {
            var model = new RelProviderStationViewModel();
            model.ProviderId = id;
            ViewBag.ProviderName = providerName;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> LoadAllWorkStationsByProvider(int id)
        {
            // Cargo todos los productos del proveedor que se seleccionado anteriormente.
            var response = await _relProviderStationViewManager.FindBySpecification(new RelProviderStationByProviderIdSpecification(id));
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<RelProviderStationViewModel>>(response.Data);
                ViewBag.ProviderId = id;
                if (viewModel.Count() > 0)
                {
                    ViewBag.ProviderName = viewModel.First().Provider.getBussinessNameOrName.ToString();
                }
                return PartialView("_ViewAllWorkStationsByProvider", viewModel);
            }
            return null;
        }

        [Authorize(Policy = Permissions.Providers.View)]
        public async Task<JsonResult> OnGetCreateOrEditProviderStation(int providerId, int id = 0)
        {
            if (id == 0)
            {
                // Add new station for this provider
                var relProviderStationViewModel = new RelProviderStationViewModel();
                relProviderStationViewModel.ProviderId = providerId;

                var unitMeasures = await _unitMeasureViewManager.FindBySpecification(new UnitMeasureIsActiveSpecification());
                if (unitMeasures.Succeeded)
                {
                    var unitMeasureViewModel = _mapper.Map<List<UnitMeasureDTO>>(unitMeasures.Data);
                    relProviderStationViewModel.UnitsMeasure = new SelectList(unitMeasureViewModel, nameof(UnitMeasureViewModel.Id), nameof(UnitMeasureViewModel.Description), null, null);
                }

                var stationsResponse = await _stationViewManager.LoadAll();
                if (stationsResponse.Succeeded)
                {
                    var stationsViewModel = _mapper.Map<List<StationViewModel>>(stationsResponse.Data);
                    relProviderStationViewModel.Stations = new SelectList(stationsViewModel, nameof(StationViewModel.Id), nameof(StationViewModel.AbreviationDescription), null, null);
                }

                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEditProviderStation", relProviderStationViewModel) });

            }
            else
            {
                // Update stations for this provider
                var response = await _relProviderStationViewManager.FindBySpecification(new RelProviderStationByIdWithAllSpecification(id));

                if (response.Succeeded)
                {

                    var relProviderStationViewModel = _mapper.Map<List<RelProviderStationViewModel>>(response.Data).FirstOrDefault();

                    var unitMeasures = await _unitMeasureViewManager.FindBySpecification(new UnitMeasureIsActiveSpecification());
                    if (unitMeasures.Succeeded)
                    {
                        var unitMeasureViewModel = _mapper.Map<List<UnitMeasureDTO>>(unitMeasures.Data);
                        relProviderStationViewModel.UnitsMeasure = new SelectList(unitMeasureViewModel, nameof(UnitMeasureViewModel.Id), nameof(UnitMeasureViewModel.Description), null, null);
                    }

                    var stationsResponse = await _stationViewManager.LoadAll();
                    if (stationsResponse.Succeeded)
                    {
                        var stationsViewModel = _mapper.Map<List<StationViewModel>>(stationsResponse.Data);
                        relProviderStationViewModel.Stations = new SelectList(stationsViewModel, nameof(StationViewModel.Id), nameof(StationViewModel.AbreviationDescription), null, null);
                    }

                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEditProviderStation", relProviderStationViewModel) });
                }

                return null;

            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEditProviderStation(int id, RelProviderStationViewModel relProviderStationViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    if (relProviderStationViewModel.StationId == -1)
                    {
                        _notify.Warning(_localizer["You must select a work station."]);

                        var unitMeasures = await _unitMeasureViewManager.FindBySpecification(new UnitMeasureIsActiveSpecification());
                        if (unitMeasures.Succeeded)
                        {
                            var unitMeasureViewModel = _mapper.Map<List<UnitMeasureDTO>>(unitMeasures.Data);
                            relProviderStationViewModel.UnitsMeasure = new SelectList(unitMeasureViewModel, nameof(UnitMeasureViewModel.Id), nameof(UnitMeasureViewModel.Description), null, null);
                        }

                        var stationsResponse = await _stationViewManager.LoadAll();
                        if (stationsResponse.Succeeded)
                        {
                            var stationsViewModel = _mapper.Map<List<StationViewModel>>(stationsResponse.Data);
                            relProviderStationViewModel.Stations = new SelectList(stationsViewModel, nameof(StationViewModel.Id), nameof(StationViewModel.AbreviationDescription), null, null);
                        }

                        return new JsonResult(new { isValid = false, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEditProviderStation", relProviderStationViewModel) });
                    }

                    if (id == 0)
                    {
                        // Add new station for the provider selected
                        var relProviderStationDTO = _mapper.Map<RelProviderStationDTO>(relProviderStationViewModel);
                        var result = await _relProviderStationViewManager.CreateAsync(relProviderStationDTO);
                        if (result.Succeeded)
                        {
                            id = result.Data;
                            _notify.Success(_localizer["The work station was assigned."]);
                        }
                        else
                        {
                            _notify.Error(result.Message);

                            var unitMeasures = await _unitMeasureViewManager.FindBySpecification(new UnitMeasureIsActiveSpecification());
                            if (unitMeasures.Succeeded)
                            {
                                var unitMeasureViewModel = _mapper.Map<List<UnitMeasureDTO>>(unitMeasures.Data);
                                relProviderStationViewModel.UnitsMeasure = new SelectList(unitMeasureViewModel, nameof(UnitMeasureViewModel.Id), nameof(UnitMeasureViewModel.Description), null, null);
                            }

                            var stationsResponse = await _stationViewManager.LoadAll();
                            if (stationsResponse.Succeeded)
                            {
                                var stationsViewModel = _mapper.Map<List<StationViewModel>>(stationsResponse.Data);
                                relProviderStationViewModel.Stations = new SelectList(stationsViewModel, nameof(StationViewModel.Id), nameof(StationViewModel.AbreviationDescription), null, null);
                            }

                            return new JsonResult(new { isValid = false, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEditProviderStation", relProviderStationViewModel) });
                        }
                    }
                    else
                    {
                        // Update stations for the provider selected
                        var relProviderStationDTO = _mapper.Map<RelProviderStationDTO>(relProviderStationViewModel);
                        var result = await _relProviderStationViewManager.UpdateAsync(relProviderStationDTO);

                        if (result.Succeeded)
                        {
                            id = result.Data;
                            _notify.Information(_localizer["The work station was updated."]);
                        }
                    }

                    var response = await _relProviderStationViewManager.FindBySpecification(new RelProviderStationByProviderIdSpecification(relProviderStationViewModel.ProviderId));
                    if (response.Succeeded)
                    {
                        var viewModel = _mapper.Map<List<RelProviderStationViewModel>>(response.Data);
                        var html = await _viewRenderer.RenderViewToStringAsync("_ViewAllWorkStationsByProvider", viewModel);
                        return new JsonResult(new { isValid = true, html = html });
                    }
                    else
                    {
                        _notify.Error(response.Message);

                        var unitMeasures = await _unitMeasureViewManager.FindBySpecification(new UnitMeasureIsActiveSpecification());
                        if (unitMeasures.Succeeded)
                        {
                            var unitMeasureViewModel = _mapper.Map<List<UnitMeasureDTO>>(unitMeasures.Data);
                            relProviderStationViewModel.UnitsMeasure = new SelectList(unitMeasureViewModel, nameof(UnitMeasureViewModel.Id), nameof(UnitMeasureViewModel.Description), null, null);
                        }

                        var stationsResponse = await _stationViewManager.LoadAll();
                        if (stationsResponse.Succeeded)
                        {
                            var stationsViewModel = _mapper.Map<List<StationViewModel>>(stationsResponse.Data);
                            relProviderStationViewModel.Stations = new SelectList(stationsViewModel, nameof(StationViewModel.Id), nameof(StationViewModel.AbreviationDescription), null, null);
                        }

                        return new JsonResult(new { isValid = false, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEditProviderStation", relProviderStationViewModel) });
                    }
                }
                else
                {
                    var unitMeasures = await _unitMeasureViewManager.FindBySpecification(new UnitMeasureIsActiveSpecification());
                    if (unitMeasures.Succeeded)
                    {
                        var unitMeasureViewModel = _mapper.Map<List<UnitMeasureDTO>>(unitMeasures.Data);
                        relProviderStationViewModel.UnitsMeasure = new SelectList(unitMeasureViewModel, nameof(UnitMeasureViewModel.Id), nameof(UnitMeasureViewModel.Description), null, null);
                    }

                    var stationsResponse = await _stationViewManager.LoadAll();
                    if (stationsResponse.Succeeded)
                    {
                        var stationsViewModel = _mapper.Map<List<StationViewModel>>(stationsResponse.Data);
                        relProviderStationViewModel.Stations = new SelectList(stationsViewModel, nameof(StationViewModel.Id), nameof(StationViewModel.AbreviationDescription), null, null);
                    }

                    return new JsonResult(new { isValid = false, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEditProviderStation", relProviderStationViewModel) });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);

                var unitMeasures = await _unitMeasureViewManager.FindBySpecification(new UnitMeasureIsActiveSpecification());
                if (unitMeasures.Succeeded)
                {
                    var unitMeasureViewModel = _mapper.Map<List<UnitMeasureDTO>>(unitMeasures.Data);
                    relProviderStationViewModel.UnitsMeasure = new SelectList(unitMeasureViewModel, nameof(UnitMeasureViewModel.Id), nameof(UnitMeasureViewModel.Description), null, null);
                }

                var stationsResponse = await _stationViewManager.LoadAll();
                if (stationsResponse.Succeeded)
                {
                    var stationsViewModel = _mapper.Map<List<StationViewModel>>(stationsResponse.Data);
                    relProviderStationViewModel.Stations = new SelectList(stationsViewModel, nameof(StationViewModel.Id), nameof(StationViewModel.AbreviationDescription), null, null);
                }

                return new JsonResult(new { isValid = false, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEditProviderStation", relProviderStationViewModel) });
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostDeleteProviderStation(int id, int providerId)
        {
            var deleteCommand = await _relProviderStationViewManager.DeleteAsync(id);
            if (deleteCommand.Succeeded)
            {
                _notify.Information(_localizer["The work station was removed."]);

                var response = await _relProviderStationViewManager.FindBySpecification(new RelProviderStationByProviderIdSpecification(providerId));
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<RelProviderStationViewModel>>(response.Data);
                    var html = await _viewRenderer.RenderViewToStringAsync("_ViewAllWorkStationsByProvider", viewModel);
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
                _notify.Error(deleteCommand.Message);
                return null;
            }
        }

    }
}
