using EntityFramework.Exceptions.Common;
using ERP.Application.Constants;
using ERP.Application.DTOs.Entities.Clients;
using ERP.Application.DTOs.Entities.Commons;
using ERP.Application.DTOs.Entities.Lists;
using ERP.Application.Enums;
using ERP.Application.Interfaces.ViewManagers.Clients;
using ERP.Application.Interfaces.ViewManagers.Commons;
using ERP.Application.Interfaces.ViewManagers.Lists;
using ERP.Application.Interfaces.ViewManagers.ProductMod;
using ERP.Application.Interfaces.ViewManagers.Sales;
using ERP.Application.Specification.Clients;
using ERP.Application.Specification.Lists;
using ERP.Application.Specification.ProductMod;
using ERP.Application.Specification.ProductMod.StructureSpecifications;
using ERP.Application.Specification.Sales;
using ERP.Language;
using ERP.Web.Abstractions;
using ERP.Web.Areas.Clients.Models;
using ERP.Web.Areas.Commons.Models;
using ERP.Web.Areas.Lists.Models;
using ERP.Web.Areas.ProductMod.Models;
using ERP.Web.Areas.Sales.Models;
using ERP.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ERP.Application.Constants.Permissions;
using PieceTypeViewModel = ERP.Web.Areas.ProductMod.Models.PieceTypeViewModel;

namespace ERP.Web.Areas.Clients.Controllers
{
    [Area("Clients")]
    public class ClientController : BaseController<ClientController>
    {
        private readonly IClientViewManager _viewManager;
        private readonly IReminderViewManager _reminderViewManager;

        private readonly ICountryViewManager _countryViewManager;
        private readonly IStateViewManager _stateViewManager;
        private readonly ICityViewManager _cityViewManager;
        private readonly IDocumentTypeViewManager _documentTypeViewManager;
        private readonly IOperationStateViewManager _operationStateViewManager;
        private readonly IProductViewManager _productViewManager;
        private readonly IStructureViewManager _structureViewManager;
        private readonly ICommunicationViewManager _communicationViewManager;
        private readonly IConsultedProductViewManager _consultedProductViewManager;
        private readonly IOrderHeaderViewManager _orderHeaderViewManager;
        private readonly IOrderDetailViewManager _orderDetailsViewManager;
        private readonly ISaleOperationViewManager _saleOperationViewManager;

        private readonly IStringLocalizer<SharedResource> _localizer;

        public ClientController(IClientViewManager viewManager, IReminderViewManager reminderViewManager, ICountryViewManager countryViewManager, IStateViewManager stateViewManager, ICityViewManager cityViewManager, IDocumentTypeViewManager documentTypeViewManager, IOperationStateViewManager operationStateViewManager, IProductViewManager productViewManager, IStructureViewManager structureViewManager, ICommunicationViewManager communicationViewManager, IConsultedProductViewManager consultedProductViewManager, IOrderHeaderViewManager orderHeaderViewManager, IOrderDetailViewManager orderDetailViewManager, ISaleOperationViewManager saleOperationViewManager, IStringLocalizer<SharedResource> localizer)
        {
            _viewManager = viewManager;
            _reminderViewManager = reminderViewManager;
            _countryViewManager = countryViewManager;
            _stateViewManager = stateViewManager;
            _cityViewManager = cityViewManager;
            _documentTypeViewManager = documentTypeViewManager;
            _operationStateViewManager = operationStateViewManager;
            _productViewManager = productViewManager;
            _structureViewManager = structureViewManager;
            _communicationViewManager = communicationViewManager;
            _consultedProductViewManager = consultedProductViewManager;
            _orderHeaderViewManager = orderHeaderViewManager;
            _orderDetailsViewManager = orderDetailViewManager;
            _saleOperationViewManager = saleOperationViewManager;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            var model = new ClientViewModel();
            return View(model);
        }

        public async Task<IActionResult> LoadAll()
        {
            var response = await _viewManager.FindBySpecification(new ClientsIsActiveSpecification());
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<ClientViewModel>>(response.Data);

                foreach (var client in viewModel)
                {
                    var responseSales = await _orderDetailsViewManager.FindBySpecification(new OrderDetailByClientId(client.Id));

                    if(responseSales.Succeeded)
                    {
                        if(responseSales.Data.Count > 0)
                        {
                            client.Bought = true;
                        }
                    }
                }
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }

        [Authorize(Policy = Permissions.Clients.View)]
        public async Task<JsonResult> OnGetCreateOrEdit(int id = 0, bool calledFromAnotherArea = false)
        {
            var countriesResponse = await _countryViewManager.LoadAll();
            var documentTypesResponse = await _documentTypeViewManager.LoadAll();
            var operationStatesResponse = await _operationStateViewManager.LoadAll();

            if (id == 0)
            {
                // To create new Client

                var clientViewModel = new ClientViewModel();

                clientViewModel.DateOfBirth = DateTime.Now.Date;

                // Set Countries in ClientViewModel to show in SelectList
                if (countriesResponse.Succeeded)
                {
                    var countryViewModel = _mapper.Map<List<CountryDTO>>(countriesResponse.Data);
                    clientViewModel.Countries = new SelectList(countryViewModel, nameof(CountryViewModel.Id), nameof(CountryViewModel.Denomination), null, null);
                }

                // Set Documents Types in ClientViewModel to show in SelectList
                if (documentTypesResponse.Succeeded)
                {
                    var documentTypeViewModel = _mapper.Map<List<DocumentTypeDTO>>(documentTypesResponse.Data);
                    clientViewModel.DocumentTypes = new SelectList(documentTypeViewModel, nameof(DocumentTypeViewModel.Id), nameof(DocumentTypeViewModel.Description), null, null);
                }

                // Set Operation States in ClientViewModel to show in SelectList
                if (operationStatesResponse.Succeeded)
                {
                    var operationStateViewModel = _mapper.Map<List<OperationStateDTO>>(operationStatesResponse.Data);
                    clientViewModel.OperationStates = new SelectList(operationStateViewModel, nameof(OperationStateViewModel.Id), nameof(OperationStateViewModel.Description), null, null);
                }

                clientViewModel.calledFromAnotherArea = calledFromAnotherArea;

                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", clientViewModel) });
            }
            else
            {
                // To update a Client
                var response = await _viewManager.FindBySpecification(new ClientsIsActiveSpecification(id));
                if (response.Succeeded)
                {
                    var clientViewModel = _mapper.Map<List<ClientViewModel>>(response.Data).FirstOrDefault();

                    if (countriesResponse.Succeeded)
                    {
                        var countryViewModel = _mapper.Map<List<CountryViewModel>>(countriesResponse.Data);
                        clientViewModel.Countries = new SelectList(countryViewModel, nameof(CountryViewModel.Id), nameof(CountryViewModel.Denomination), clientViewModel.CountryId, null);
                    }

                    if (documentTypesResponse.Succeeded)
                    {
                        var documentTypeViewModel = _mapper.Map<List<DocumentTypeViewModel>>(documentTypesResponse.Data);
                        clientViewModel.DocumentTypes = new SelectList(documentTypeViewModel, nameof(DocumentTypeViewModel.Id), nameof(DocumentTypeViewModel.Description), null, null);
                    }

                    if (operationStatesResponse.Succeeded)
                    {
                        var operationStateViewModel = _mapper.Map<List<OperationStateViewModel>>(operationStatesResponse.Data);
                        clientViewModel.OperationStates = new SelectList(operationStateViewModel, nameof(OperationStateViewModel.Id), nameof(OperationStateViewModel.Description), null, null);
                    }

                    clientViewModel.calledFromAnotherArea = calledFromAnotherArea;

                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", clientViewModel) });
                }
                return null;
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(int id, ClientViewModel client)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (client.DocumentTypeId == -1)
                    {
                        client.DocumentTypeId = null;
                    }

                    if (client.OperationStateId == -1)
                    {
                        client.OperationStateId = null;
                    }

                    if (client.CityId == -1)
                    {
                        client.CityId = null;
                    }

                    if (client.CountryId == -1 || client.CountryId == 0)
                    {
                        _notify.Warning(_localizer["You must select a country."]);
                        return new JsonResult(new { isValid = false, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", client) });
                    }
                    if (client.StateId == -1 || client.StateId == 0)
                    {
                        client.StateId = null;
                    }

                    if (id == 0)
                    {
                        // Create
                        var clientDTO = _mapper.Map<ClientDTO>(client);

                        var result = await _viewManager.CreateAsync(clientDTO);
                        if (result.Succeeded)
                        {
                            id = result.Data;
                            _notify.Success(_localizer["Client created."]);
                        }
                        else
                        {
                            _notify.Error(result.Message);
                        }
                    }
                    else
                    {
                        //  Update
                        var clientDTO = _mapper.Map<ClientDTO>(client);

                        var result = await _viewManager.UpdateAsync(clientDTO);
                        if (result.Succeeded) _notify.Information(_localizer["Client update."]);
                    }
                    var response = await _viewManager.FindBySpecification(new ClientsIsActiveSpecification());
                    if (response.Succeeded)
                    {
                        var viewModel = _mapper.Map<List<ClientViewModel>>(response.Data);
                        var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", viewModel);
                        return new JsonResult(new { isValid = true, html = html, clientId = id });
                    }
                    else
                    {
                        _notify.Error(response.Message);
                        return null;
                    }
                }
                else
                {
                    var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", client);
                    return new JsonResult(new { isValid = false, html = html });
                }
            }
            catch (UniqueConstraintException ex)
            {
                var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", client);
                if ((ex.InnerException as SqlException).Number == 2601) // Unique key violation
                {
                    _notify.Warning(_localizer["The client you want to save already exists."]);
                    return new JsonResult(new { isValid = false, html = html });
                }
                else
                {
                    _notify.Error(ex.Message);
                    return new JsonResult(new { isValid = false, html = html });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", client);
                return new JsonResult(new { isValid = false, html = html });
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostDeactivated(int id)
        {
            var deleteCommand = await _viewManager.DeactivateAsync(id);
            if (deleteCommand.Succeeded)
            {
                _notify.Information(_localizer["Client deleted."]);
                var response = await _viewManager.FindBySpecification(new ClientsIsActiveSpecification());
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
                _notify.Error(deleteCommand.Message);
                return null;
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostDelete(int id)
        {
            var deleteCommand = await _viewManager.DeleteAsync(id);
            if (deleteCommand.Succeeded)
            {
                _notify.Information(_localizer["Client with ID {0} deleted.", id]);
                var response = await _viewManager.FindBySpecification(new ClientsIsActiveSpecification());
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
                _notify.Error(deleteCommand.Message);
                return null;
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostDeleteCommunication(int id, int clientId)
        {
            var deleteCommand = await _communicationViewManager.DeleteAsync(id);
            if (deleteCommand.Succeeded)
            {
                _notify.Information(_localizer["Communication deleted."]);
                var response = await _communicationViewManager.FindBySpecification(new CommunicationsWithAllSpecifications(clientId));
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<CommunicationViewModel>>(response.Data);
                    var html = await _viewRenderer.RenderViewToStringAsync("_ViewAllComunications", viewModel);
                    return new JsonResult(new { isValid = true, html = html, clientId = clientId });
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
        [HttpPost]
        public async Task<JsonResult> OnPostDeleteSaleOperation(int id, int clientId)
        {
            var deleteCommand = await _saleOperationViewManager.DeleteAsync(id);
            if (deleteCommand.Succeeded)
            {
                _notify.Information(_localizer["Operation deleted."]);
                var response = await _saleOperationViewManager.FindBySpecification(new SaleOperationByClientId(clientId));
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<SaleOperationViewModel>>(response.Data);
                    var html = await _viewRenderer.RenderViewToStringAsync("_ViewAllOperationsSales", viewModel);
                    return new JsonResult(new { isValid = true, html = html, clientId = clientId });
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
        public async void OnPostDeleteConsultedProduct(int id)
        {
            var deleteCommand = await _consultedProductViewManager.DeleteAsync(id);
            if(deleteCommand.Succeeded)
            {
                //Borrado
            }
        }

        [HttpPost]
        public async Task<JsonResult> AddStateFromSelect2(string Name, int CountryId)
        {
            var id = 0;
            var statesByName = await _stateViewManager.FindBySpecification(new GetStateByNameSpecification(Name));
            if (statesByName.Succeeded)
            {
                if (statesByName.Data.Count > 0)
                {
                    return new JsonResult(new { isValid = false, id = id });
                }
                else
                {
                    var stateViewModel = new StateViewModel();
                    stateViewModel.Name = Name;
                    stateViewModel.CountryId = CountryId;

                    var stateDTO = _mapper.Map<StateDTO>(stateViewModel);
                    var result = await _stateViewManager.CreateAsync(stateDTO);
                    if (result.Succeeded)
                    {
                        id = result.Data;
                        return new JsonResult(new { isValid = true, id = id });
                    }
                    else
                    {
                        return new JsonResult(new { isValid = false, id = id });
                    }
                }
            }
            else
            {
                return new JsonResult(new { isValid = false, id = id });
            }
        }

        [HttpPost]
        public async Task<JsonResult> AddCityFromSelect2(string Name, int StateId)
        {
            var id = 0;
            var citiesByName = await _cityViewManager.FindWithSpecification(new GetCityByNameSpecification(Name));
            if (citiesByName.Succeeded)
            {
                if (citiesByName.Data.Count > 0)
                {
                    return new JsonResult(new { isValid = false, id = id });
                }
                else
                {
                    var cityViewModel = new CityViewModel();
                    cityViewModel.Name = Name;
                    cityViewModel.StateId = StateId;

                    var cityDTO = _mapper.Map<CityDTO>(cityViewModel);
                    var result = await _cityViewManager.CreateAsync(cityDTO);
                    if (result.Succeeded)
                    {
                        id = result.Data;
                        return new JsonResult(new { isValid = true, id = id });
                    }
                    else
                    {
                        return new JsonResult(new { isValid = false, id = id });
                    }
                }
            }
            else
            {
                return new JsonResult(new { isValid = false, id = id });
            }
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
            if (cityId != 0)
            {
                var city = await _cityViewManager.GetByIdAsync(cityId);
                if (city.Succeeded)
                {
                    var cityViewModel = _mapper.Map<CityViewModel>(city.Data);
                    return Json(cityViewModel.ZipCode);
                }
                else
                {
                    return Json("");
                }
            }
            else
            {
                _notify.Warning(_localizer["You must create postal code."]);
                return Json("");
            }
        }

        [HttpGet]
        public async Task<JsonResult> AddPostalCodeByCityId(int cityId, string PostalCode)
        {
            var city = await _cityViewManager.FindWithSpecification(new CitySpecification(cityId));
            if (city.Succeeded)
            {
                var cityViewModel = _mapper.Map<CityViewModel>(city.Data.First());
                cityViewModel.ZipCode = PostalCode;

                var cityDTO = _mapper.Map<CityDTO>(cityViewModel);

                await _cityViewManager.UpdateAsync(cityDTO);

                _notify.Success(_localizer["Postal code updated."]);
                return Json(PostalCode);
            }
            else
            {
                return Json(PostalCode);
            }
        }

        [Authorize(Policy = Permissions.Clients.View)]
        public async Task<JsonResult> OnGetCreateReminderByClientId(int id)
        {
            DateTime dateTime = getDateTime(id);

            // To create new Reminder
            var reminderViewModel = new ReminderViewModel();
            reminderViewModel.ClientId = id;
            reminderViewModel.CountryTime = dateTime.ToString("HH:mm") + " hs.";
            reminderViewModel.ContactDate = DateTime.Now.Date.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute);
            return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateReminder", reminderViewModel) });
        }

        public DateTime getDateTime(int id)
        {
            TimeSpan offset = TimeSpan.Parse("-03:00");
            DateTimeOffset dateTime = DateTimeOffset.UtcNow.ToOffset(offset);

            var clientById = _viewManager.GetByIdLazyLoad(id);
            if (clientById.Result.Succeeded)
            {
                if (clientById.Result.Data.Country != null)
                {
                    offset = TimeSpan.Parse(clientById.Result.Data.Country.TimeOffset.ToString().Trim());
                    dateTime = DateTimeOffset.UtcNow.ToOffset(offset);
                }
            }

            return dateTime.DateTime;
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateReminder(int id, ReminderViewModel reminder)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (reminder.ReminderCheck)
                    {
                        if (reminder.ReminderDate != null)
                        {
                            // Create reminder
                            var reminderDTO = _mapper.Map<ReminderDTO>(reminder);
                            var result = await _reminderViewManager.CreateAsync(reminderDTO);
                            if (result.Succeeded)
                            {
                                id = result.Data;
                                _notify.Success(_localizer["Reminder with ID {0} created.", result.Data]);
                            }
                            else
                            {
                                _notify.Error(result.Message);
                            }

                            // Vuelvo a cargar el datatable de clientes
                            var response = await _viewManager.FindBySpecification(new ClientsIsActiveSpecification());
                            if (response.Succeeded)
                            {
                                var viewModel = _mapper.Map<List<ClientViewModel>>(response.Data);
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
                            _notify.Error(_localizer["If you activate the alert, the reminder date cannot be empty."]);
                            var html = await _viewRenderer.RenderViewToStringAsync("_CreateReminder", reminder);
                            return new JsonResult(new { isValid = false, html = html });
                        }
                    }
                    else
                    {
                        // Create reminder
                        var reminderDTO = _mapper.Map<ReminderDTO>(reminder);
                        var result = await _reminderViewManager.CreateAsync(reminderDTO);
                        if (result.Succeeded)
                        {
                            id = result.Data;
                            _notify.Success(_localizer["Reminder with ID {0} created.", result.Data]);
                        }
                        else
                        {
                            _notify.Error(result.Message);
                        }

                        // Vuelvo a cargar el datatable de clientes
                        var response = await _viewManager.FindBySpecification(new ClientsIsActiveSpecification());
                        if (response.Succeeded)
                        {
                            var viewModel = _mapper.Map<List<ClientViewModel>>(response.Data);
                            var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", viewModel);
                            return new JsonResult(new { isValid = true, html = html });
                        }
                        else
                        {
                            _notify.Error(response.Message);
                            return null;
                        }
                    }
                }
                else
                {
                    _notify.Warning(_localizer["Reminder date must be after contact date."], 4);
                    var html = await _viewRenderer.RenderViewToStringAsync("_CreateReminder", reminder);
                    return new JsonResult(new { isValid = false, html = html });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                var html = await _viewRenderer.RenderViewToStringAsync("_CreateReminder", reminder);
                return new JsonResult(new { isValid = false, html = html });
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetSimplemakProducts()
        {
            try
            {
                var productsResponse = await _productViewManager.FindBySpecification(new ProductByCategoryAndInHouseSpecification(null, true));
                if (productsResponse.Succeeded)
                {
                    if (productsResponse.Data.Count > 0)
                    {
                        var selectList = new SelectList((from p in productsResponse.Data.Where(p => p.SubCategory.CategoryId == 1).OrderBy(p => p.Description).ToList() select new { Id = p.Id, CodeAndDescription = p.Code + " - " + p.Description }), "Id", "CodeAndDescription", null);
                        return new JsonResult(new { isValid = true, data = selectList });
                    }
                    else
                    {
                        return new JsonResult(new { isValid = false, data = "" });
                    }
                }
                else
                {
                    return new JsonResult(new { isValid = false, data = "" });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return new JsonResult(new { isValid = false, data = "" });
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetImportedProducts()
        {
            try
            {
                var productsResponse = await _productViewManager.FindBySpecification(new ProductByCategoryAndInHouseSpecification(null, false));
                if (productsResponse.Succeeded)
                {
                    if (productsResponse.Data.Count > 0)
                    {
                        var selectList = new SelectList((from p in productsResponse.Data.Where(p => p.SubCategory.CategoryId == 1).OrderBy(p => p.Description).ToList() select new { Id = p.Id, CodeAndDescription = p.Code + " - " + p.Description }), "Id", "CodeAndDescription", null);
                        return new JsonResult(new { isValid = true, data = selectList });
                    }
                    else
                    {
                        return new JsonResult(new { isValid = false, data = "" });
                    }
                }
                else
                {
                    return new JsonResult(new { isValid = false, data = "" });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return new JsonResult(new { isValid = false, data = "" });
            }
        }

        [Authorize(Policy = Permissions.Clients.View)]
        public async Task<JsonResult> OnGetCreateCommunication(int id, int clientId, bool viewAll)
        {
            var communicationVM = new CommunicationViewModel();

            if (id == 0)
            {
                communicationVM.ClientId = clientId;
                communicationVM.ComunicationDate = DateTime.Now.Date.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute);
                //communicationVM.ComunicationDate = DateTime.Now.Date;

                #region DateTime for input date

                string YearToday = DateTime.Today.Year.ToString();
                string MonthToday = DateTime.Today.Month.ToString();
                string DayToday = DateTime.Today.Day.ToString();

                if (MonthToday.Count() == 1)
                {
                    communicationVM.DateTimeToday = YearToday + "-" + "0" + MonthToday + "-";
                }
                else
                {
                    communicationVM.DateTimeToday = YearToday + "-" + MonthToday + "-";
                }
                if (DayToday.Count() == 1)
                {
                    communicationVM.DateTimeToday += "0" + DayToday;
                }
                else
                {
                    communicationVM.DateTimeToday += DayToday;
                }
                #endregion

            }
            else//UPDATE
            {
                // To view Communication
                var getCommunication = await _communicationViewManager.FindBySpecification(new CommunicationByIdSpecification(id));

                if (getCommunication.Succeeded)
                {
                    communicationVM = _mapper.Map<CommunicationViewModel>(getCommunication.Data.FirstOrDefault());

                    #region DateTime for input date

                    string comDate = communicationVM.ComunicationDate.ToShortDateString();
                    string[] comDateArray = comDate.Split('/');

                    var day = comDateArray[0];
                    var month = comDateArray[1];
                    var year = comDateArray[2];

                    if(day.Length == 1)
                    {
                        day = day.Insert(0, "0");
                    }
                    if(month.Length == 1)
                    {
                        month = month.Insert(0, "0");
                    }
                    communicationVM.ComunicationDateString = year + "-" + month + "-" + day;
                    #endregion


                    if (communicationVM.SaleOperationId != 0)
                    {
                        var getSaleOperation = await _saleOperationViewManager.FindBySpecification(new SaleOperationForSelect2(communicationVM.SaleOperationId));

                        if (getSaleOperation.Succeeded)
                        {
                            var saleOperationVM = new SaleOperationViewModel();
                            saleOperationVM = _mapper.Map<SaleOperationViewModel>(getSaleOperation.Data.FirstOrDefault());
                            communicationVM.SaleOperation = saleOperationVM;
                        }
                    }
                }
            }

            // New select list for ContactSources
            var contactSourceVM = new List<ContactSourceViewModel> {
                new ContactSourceViewModel {Id = 1, Description = _localizer["Call"]},
                new ContactSourceViewModel {Id = 2, Description = _localizer["Face to face"]},
                new ContactSourceViewModel {Id = 3, Description = _localizer["Message"]},
                new ContactSourceViewModel {Id = 4, Description = _localizer["Email"]},
                new ContactSourceViewModel {Id = 5, Description = _localizer["Video call"]}
            };
            communicationVM.ContactSources = new SelectList(contactSourceVM, nameof(ContactSourceViewModel.Id), nameof(ContactSourceViewModel.Description), null);

            //Por defecto, cargo los productos de Simplemak y categoria Maquinas y accesorios
            //var productsResponse = await _productViewManager.FindBySpecification(new ProductByCategoryAndInHouseSpecification(null, true));
            //if (productsResponse.Succeeded)
            //{
            //    var productsViewModel = _mapper.Map<List<ProductDTO>>(productsResponse.Data);
            //    communicationVM.ConsultedProductsSL = new SelectList((from p in productsViewModel.Where(p => p.SubCategory.CategoryId == 1).OrderBy(p => p.Description).ToList() select new { Id = p.Id, CodeAndDescription = p.Code + " - " + p.Description }), "Id", "CodeAndDescription", null);
            //}

            // New select list for Functionalities
            var functionalitiesVM = new List<FunctionalityViewModel> {
                new FunctionalityViewModel {Id = 1, Description = "Corte"},
                new FunctionalityViewModel {Id = 2, Description = "Impresión"},
                new FunctionalityViewModel {Id = 3, Description = "Microperforado"},
                new FunctionalityViewModel {Id = 4, Description = "Macroperforado"},
                new FunctionalityViewModel {Id = 5, Description = "Troquelado"},
                new FunctionalityViewModel {Id = 6, Description = "Prensado"},
                new FunctionalityViewModel {Id = 7, Description = "Confección"} ,
                new FunctionalityViewModel {Id = 8, Description = "Valvulado"},
                new FunctionalityViewModel {Id = 9, Description = "Pegado"} ,
                new FunctionalityViewModel {Id = 10, Description = "Doblado"},
                new FunctionalityViewModel {Id = 11, Description = "Otros"}
            };
            communicationVM.Functionalities = new SelectList(functionalitiesVM, nameof(FunctionalityViewModel.Id), nameof(FunctionalityViewModel.Description), null);

            // New select list for PieceTypes
            var pieceTypesVM = new List<PieceTypeViewModel> {
                new PieceTypeViewModel {Id = 1, Description = "Saco chico"},
                new PieceTypeViewModel {Id = 2, Description = "Big bag"},
                new PieceTypeViewModel {Id = 3, Description = "Cinta reposera"},
                new PieceTypeViewModel {Id = 4, Description = "Eslinga"},
                new PieceTypeViewModel {Id = 5, Description = "Pasacalle"},
                new PieceTypeViewModel {Id = 6, Description = "Tejido coversol"},
                new PieceTypeViewModel {Id = 7, Description = "Otros"}
            };
            communicationVM.PieceTypes = new SelectList(pieceTypesVM, nameof(PieceTypeViewModel.Id), nameof(PieceTypeViewModel.Description), null);

            communicationVM.ViewAll = viewAll;

            return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateCommunication", communicationVM) });
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateComunication(int id, CommunicationViewModel communication)
        {
            try
            {
                if (ModelState.IsValid)
                {          
                    if(communication.CommunicationName is null)
                    {
                        #region Asign CommunicationNumber 

                        var communications = await _communicationViewManager.FindBySpecification(new CommunicationsWithAllSpecifications(communication.ClientId));

                        var communicationsViewModel = _mapper.Map<List<CommunicationViewModel>>(communications.Data.ToList());

                        int communicationNumber;

                        if (communicationsViewModel is null || communicationsViewModel.Count == 0)
                        {
                            communicationNumber = 0;
                        }
                        else
                        {
                            communicationNumber = communicationsViewModel.Max(so => so.CommunicationNumber);
                        }

                        communicationNumber = communicationNumber + 1;

                        string communicationName = "Comunicación " + communicationNumber;

                        communication.CommunicationNumber = communicationNumber;
                        communication.CommunicationName = communicationName;
                        #endregion
                    }

                    if (communication.NewOperation)
                    {
                        var saleOperations = await _saleOperationViewManager.FindBySpecification(new SaleOperationByClientId(communication.ClientId));

                        if (saleOperations.Succeeded)
                        {
                            var saleOperationsViewModel = _mapper.Map<List<SaleOperationViewModel>>(saleOperations.Data.ToList());

                            int operationNumber;

                            if (saleOperationsViewModel is null || saleOperationsViewModel.Count == 0)
                            {
                                operationNumber = 0;
                            }
                            else
                            {
                                operationNumber = saleOperationsViewModel.Max(so => so.OperationNumber);
                            }

                            operationNumber = operationNumber + 1;

                            string operationName = "Operacion " + operationNumber;

                            var saleOperationVM = new SaleOperationViewModel
                            {
                                StartDate = communication.ComunicationDate,
                                State = "En proceso",
                                ClientId = communication.ClientId,
                                Operation = operationName,
                                OperationNumber = operationNumber,
                                Client = communication.Client,
                            };

                            var saleOperationDTO = _mapper.Map<SaleOperationDTO>(saleOperationVM);
                            var result = await _saleOperationViewManager.CreateAsync(saleOperationDTO);

                            communication.SaleOperationId = result.Data;
                            //communication.SaleOperation = saleOperationVM;
                        }
                    }

                    var consultedProducts = communication.ConsultedProducts.Where(x => x != null).ToList();
                    if (communication.ConsultedProduct && consultedProducts.Count() <= 0)
                    {                           
                        _notify.Warning("Debe cargar productos a la tabla de productos consultados.");
                        var html = await _viewRenderer.RenderViewToStringAsync("_CreateCommunication", communication);
                        return new JsonResult(new { isValid = false, html = html });
                    }
                    
                    if(communication.Incoming is false && communication.Outgoing is false)
                    {
                        _notify.Warning("Debe asignar el origen de contacto.");
                        var html = await _viewRenderer.RenderViewToStringAsync("_CreateCommunication", communication);
                        return new JsonResult(new { isValid = false, html = html });
                    }

                    if (communication.ContactSource != "0")
                    {
                        if (communication.ContactSource == "1" || communication.ContactSource == _localizer["Call"])
                        {
                            communication.ContactSource = _localizer["Call"];
                        }
                        else if (communication.ContactSource == "2" || communication.ContactSource == _localizer["Face to face"])
                        {
                            communication.ContactSource = _localizer["Face to face"];
                        }
                        else if (communication.ContactSource == "3" || communication.ContactSource == _localizer["Message"])
                        {
                            communication.ContactSource = _localizer["Message"];
                        }
                        else if (communication.ContactSource == "4" || communication.ContactSource == _localizer["Email"])
                        {
                            communication.ContactSource = _localizer["Email"];
                        }
                        else if (communication.ContactSource == "5" || communication.ContactSource == _localizer["Video call"])
                        {
                            communication.ContactSource = _localizer["Video call"];
                        }

                        if (communication.Id != 0) // Update
                        {
                            var comDTO = _mapper.Map<CommunicationDTO>(communication);
                            var result = await _communicationViewManager.UpdateAsync(comDTO);
                            if (result.Succeeded) _notify.Information(_localizer["Communication update."]);
                        }
                        else // Create
                        {
                            var communicationDTO = _mapper.Map<CommunicationDTO>(communication);
                            var result = await _communicationViewManager.CreateAsync(communicationDTO);
                            if (result.Succeeded)
                            {
                                _notify.Success(_localizer["Communication created."]);
                            }
                            else
                            {
                                _notify.Error(result.Message);
                            }
                        }

                        
                        if(communication.ViewAll) // View all
                        {
                            return new JsonResult(new { isValid = true });

                            //var responseView = await _communicationViewManager.FindBySpecification(new CommunicationsWithAllSpecifications());
                            //if(responseView.Succeeded)
                            //{
                            //    var viewModel = _mapper.Map<List<CommunicationViewModel>>(responseView.Data);

                            //    return new JsonResult(new { isValid = true});
                            //}
                            //else
                            //{
                            //    _notify.Error(responseView.Message);
                            //    return null;
                            //}
                        }
                        else // View for clientId
                        {
                            return new JsonResult(new { isValid = true, viewAll = true });

                            //var responseView = await _communicationViewManager.FindBySpecification(new CommunicationsWithAllSpecifications(communication.ClientId));
                            //if (responseView.Succeeded)
                            //{
                            //    var viewModel = _mapper.Map<List<CommunicationViewModel>>(responseView.Data);
                            //    var html = await _viewRenderer.RenderViewToStringAsync("_ViewAllComunications", viewModel);

                            //    return new JsonResult(new { isValid = true, viewAll = true});
                            //}
                            //else
                            //{
                            //    _notify.Error(responseView.Message);
                            //    return null;
                            //}
                        }
                    }
                    else
                    {
                        _notify.Warning("Debe seleccionar un medio de contacto.");
                        var html = await _viewRenderer.RenderViewToStringAsync("_CreateCommunication", communication);
                        return new JsonResult(new { isValid = false, html = html });
                    }
                }
                else
                {
                    var html = await _viewRenderer.RenderViewToStringAsync("_CreateCommunication", communication);
                    return new JsonResult(new { isValid = false, html = html });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                var html = await _viewRenderer.RenderViewToStringAsync("_CreateCommunication", communication);
                return new JsonResult(new { isValid = false, html = html });
            }
        }

        public async Task<JsonResult> LoadExistingOrderDetails(string search, int id)
        {
            var responseOD = await _orderDetailsViewManager.FindBySpecification(new OrderDetailByClientId(id));
            if (responseOD.Succeeded)
            {
                List<Select2ViewModel> mapSelect2Data = new List<Select2ViewModel>();
                var ODViewModel = _mapper.Map<List<OrderDetailViewModel>>(responseOD.Data.ToList());

                foreach (var orderDetail in ODViewModel)
                {
                    mapSelect2Data.Add(new Select2ViewModel() { Id = orderDetail.Id, Text = orderDetail.Product.CodeAndDescription });
                }

                if (!(string.IsNullOrEmpty(search) || string.IsNullOrWhiteSpace(search)))
                {
                    mapSelect2Data = mapSelect2Data.Where(x => x.Text.ToLower().Contains(search.ToLower())).ToList();
                }
                return new JsonResult(new { orderDetails = mapSelect2Data });
            }
            else
            {
                return null;
            }
        }
        public async Task<JsonResult> LoadExistingOperations(string search, int id)
        {
            var saleOperationResponse = await _saleOperationViewManager.FindBySpecification(new SaleOperationByClientId(id));
            if (saleOperationResponse.Succeeded)
            {
                List<Select2ViewModel> mapSelect2Data = new List<Select2ViewModel>();
                var saleOperationViewModel = _mapper.Map<List<SaleOperationViewModel>>(saleOperationResponse.Data.ToList());

                foreach (var saleOperation in saleOperationViewModel)
                {
                    if(saleOperation.State != "Concretada" && saleOperation.State != "Concretized")
                    {
                        mapSelect2Data.Add(new Select2ViewModel() { Id = saleOperation.Id, Text = saleOperation.Operation });
                    }
                }

                if (!(string.IsNullOrEmpty(search) || string.IsNullOrWhiteSpace(search)))
                {
                    mapSelect2Data = mapSelect2Data.Where(x => x.Text.ToLower().Contains(search.ToLower())).ToList();
                }
                return new JsonResult(new { saleOperations = mapSelect2Data });
            }
            else
            {
                return null;
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetStructuresByProductId(int id)
        {
            try
            {
                if (id != -1)
                {
                    var structures = await _structureViewManager.FindBySpecification(new StructureByProductIdSpecification(id));
                    if (structures.Succeeded)
                    {
                        if (structures.Data.Count > 0)
                        {
                            var selectList = new SelectList((from s in structures.Data.ToList() select new { Id = s.Id, Description = s.Description }), "Id", "Description", null);
                            return new JsonResult(new { isValid = true, structures = selectList });
                        }
                        else
                        {
                            return new JsonResult(new { isValid = false, structures = "" });
                        }
                    }
                    else
                    {
                        return new JsonResult(new { isValid = false, structures = "" });
                    }
                }
                else
                {
                    return new JsonResult(new { isValid = false, structures = "" });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return new JsonResult(new { isValid = false, structures = "" });
            }
        }

        public async Task<IActionResult> _ViewAllComunications(int clientId)
        {
            DateTime dateTime = getDateTime(clientId);
            var clientById = _viewManager.GetByIdLazyLoad(clientId);

            ViewBag.CountryTime = dateTime.ToString("HH:mm") + " hs.";
            ViewBag.BusinessName = clientById.Result.Data.BusinessName.ToString();
            ViewBag.ClientId = clientId;

            var response = await _communicationViewManager.FindBySpecification(new CommunicationsWithAllSpecifications(clientId));
            if (response.Succeeded)
            {
                var communications = _mapper.Map<List<CommunicationViewModel>>(response.Data);
                #region Asing saleOperation
                foreach (var com in communications)
                {
                    if (com.SaleOperationId > 0)
                    {
                        var saleOperation = await _saleOperationViewManager.FindBySpecification(new SaleOperationForSelect2(com.SaleOperationId));

                        if (saleOperation.Succeeded)
                        {
                            var saleOperationVM = _mapper.Map<List<SaleOperationViewModel>>(saleOperation.Data);
                            foreach (var so in saleOperationVM)
                            {
                                com.SaleOperation = so;
                                break;
                            }
                        }
                    }
                }
                #endregion
                return View("_ViewAllComunications", communications);
            }
            return null;
        }

        public async Task<IActionResult> _ViewAllSales(int clientId)
        {
            var response = await _orderDetailsViewManager.FindBySpecification(new OrderDetailByClientId(clientId));

            var clientById = _viewManager.GetByIdLazyLoad(clientId);

            ViewBag.BusinessName = clientById.Result.Data.BusinessName.ToString();
            ViewBag.ClientId = clientId;

            if (response.Succeeded)
            {
                var sales = _mapper.Map<List<OrderDetailViewModel>>(response.Data);

                return View("_ViewAllSales", sales);
            }
            return null;
        }
        public async Task<IActionResult> _LoadAllSales(int clientId, DatatableParamsViewModel datatableParams, string sSearch_1,
            string sSearch_2, string sSearch_3, string sSearch_4, string sSearch_5, string sSearch_6, string sSearch_7, string sSearch_8, string sSearch_9,
            string sSearch_10, string sSearch_11, string sSearch_12, string checkMachines, string checkAccesories, string checkSpares)
        {
            var response = await _orderDetailsViewManager.FindBySpecification(new OrderDetailByClientId(clientId));

            var clientById = _viewManager.GetByIdLazyLoad(clientId);

            ViewBag.BusinessName = clientById.Result.Data.BusinessName.ToString();
            ViewBag.ClientId = clientId;

            if (response.Succeeded)
            {
                try
                {
                    var sales = _mapper.Map<List<OrderDetailViewModel>>(response.Data);

                    // Asignar sale operation
                    foreach (var sale in sales)
                    {
                        if (sale.OrderHeader.SaleOperationId is not null)
                        {
                            var saleOperationResponse = await _saleOperationViewManager.FindBySpecification(new SaleOperationForSelect2((int)sale.OrderHeader.SaleOperationId));

                            if (saleOperationResponse.Succeeded)
                            {
                                sale.SaleOperation = _mapper.Map<SaleOperationViewModel>(saleOperationResponse.Data.FirstOrDefault());
                            }
                        }

                    }

                    IEnumerable<OrderDetailViewModel> salesOp = sales;

                    HashSet<OrderDetailViewModel> ordersAux = new HashSet<OrderDetailViewModel>();
                    IEnumerable<OrderDetailViewModel> filterList = new List<OrderDetailViewModel>();

                    //AGREGAR OPERACION SEGUN ID DEL ORDER HEADER

                    #region Filters

                    //Nº OV/OP
                    if (!string.IsNullOrEmpty(sSearch_1))
                    {
                        salesOp = salesOp.Where(s => s.OrderHeader.Number.ToString().Contains(sSearch_1.ToLower())).ToList();
                    }

                    //Date
                    if (!string.IsNullOrEmpty(sSearch_2))
                    {
                        salesOp = salesOp.Where(s => s.OrderHeader.OrderDate.ToString("dd/MM/yyyy").Contains(sSearch_2.ToLower())).ToList();
                    }

                    //Delivery date
                    if (!string.IsNullOrEmpty(sSearch_3))
                    {
                        salesOp = salesOp.Where(s => s.DeliveryDate.HasValue && s.DeliveryDate.Value.ToString("dd/MM/yyyy").Contains(sSearch_3.ToLower())).ToList();
                    }

                    //ProductNumber
                    if (!string.IsNullOrEmpty(sSearch_4))
                    {
                        salesOp = salesOp.Where(s => s.ProductNumber.ToString().ToLower().Contains(sSearch_4.ToLower())).ToList();
                    }

                    //Product Code
                    if (!string.IsNullOrEmpty(sSearch_5))
                    {
                        salesOp = salesOp.Where(s => s.Product.Code != null && s.Product.Code.ToString().ToLower().Contains(sSearch_5.ToLower())).ToList();
                    }

                    //Product Description
                    if (!string.IsNullOrEmpty(sSearch_6))
                    {
                        salesOp = salesOp.Where(s => s.Product.Description != null && s.Product.Description.ToString().ToLower().Contains(sSearch_6.ToLower())).ToList();
                    }

                    //Structure Description
                    if (!string.IsNullOrEmpty(sSearch_7))
                    {
                        salesOp = salesOp.Where(s => s.Structure != null && s.Structure.Description.ToString().ToLower().Contains(sSearch_7.ToLower())).ToList();
                    }

                    //Sale operation
                    if (!string.IsNullOrEmpty(sSearch_8))
                    {
                        salesOp = salesOp.Where(s => s.SupplyVoltage != null && s.SupplyVoltage.Description.ToString().ToLower().Contains(sSearch_8.ToLower())).ToList();
                    }

                    //Quantity
                    if (!string.IsNullOrEmpty(sSearch_9))
                    {
                        salesOp = salesOp.Where(s => s.Quantity.ToString().ToLower().Contains(sSearch_9.ToLower()));
                    }

                    //SaleCategory
                    if (!string.IsNullOrEmpty(sSearch_10))
                    {
                        salesOp = salesOp.Where(s => s.SaleCategory.ToString().ToLower().Contains(sSearch_10.ToLower()));
                    }

                    //Product SubCategory Description
                    if (!string.IsNullOrEmpty(sSearch_11))
                    {
                        salesOp = salesOp.Where(s => s.Product != null && s.Product.SubCategory.Description.ToString().ToLower().Contains(sSearch_11.ToLower()));
                    }

                    //SaleOperation Operation
                    if (!string.IsNullOrEmpty(sSearch_12))
                    {
                        salesOp = salesOp.Where(s => s.SaleOperation != null && s.SaleOperation.Operation.ToString().ToLower().Contains(sSearch_12.ToLower()));
                    }
                    #endregion

                    #region Order

                    var sortColumnIndex = datatableParams.iSortCol_0;
                    var sortDirection = datatableParams.sSortDir_0;

                    if (sortColumnIndex == 1)
                    {
                        salesOp = sortDirection == "asc" ? salesOp.OrderBy(c => c.OrderHeader.Number) : salesOp.OrderByDescending(c => c.OrderHeader.Number);
                    }
                    else if (sortColumnIndex == 2)
                    {
                        salesOp = sortDirection == "asc" ? salesOp.OrderBy(c => c.OrderHeader.OrderDate) : salesOp.OrderByDescending(c => c.OrderHeader.OrderDate);
                    }
                    else if (sortColumnIndex == 3)
                    {
                        salesOp = sortDirection == "asc" ? salesOp.OrderBy(c => c.DeliveryDate) : salesOp.OrderByDescending(c => c.DeliveryDate);
                    }
                    else if (sortColumnIndex == 4)
                    {
                        salesOp = sortDirection == "asc" ? salesOp.OrderBy(c => c.ProductNumber) : salesOp.OrderByDescending(c => c.ProductNumber);
                    }
                    else if (sortColumnIndex == 5)
                    {
                        salesOp = sortDirection == "asc" ? salesOp.OrderBy(c => c.Product.Code) : salesOp.OrderByDescending(c => c.Product.Code);
                    }
                    else if (sortColumnIndex == 6)
                    {
                        salesOp = sortDirection == "asc" ? salesOp.OrderBy(c => c.Product.Description) : salesOp.OrderByDescending(c => c.Product.Description);
                    }
                    else if (sortColumnIndex == 7)
                    {
                        salesOp = sortDirection == "asc" ? salesOp.OrderBy(c => c.Structure != null ? c.Structure.Description : null) : salesOp.OrderByDescending(c => c.Structure != null ? c.Structure.Description : null);
                    }
                    else if (sortColumnIndex == 8)
                    {
                        salesOp = sortDirection == "asc" ? salesOp.OrderBy(c => c.SupplyVoltage != null ? c.SupplyVoltage.Description : null) : salesOp.OrderByDescending(c => c.SupplyVoltage != null ? c.SupplyVoltage.Description : null);
                    }
                    else if (sortColumnIndex == 9)
                    {
                        salesOp = sortDirection == "asc" ? salesOp.OrderBy(c => c.Quantity) : salesOp.OrderByDescending(c => c.Quantity);
                    }
                    else if (sortColumnIndex == 10)
                    {
                        salesOp = sortDirection == "asc" ? salesOp.OrderBy(c => c.SaleCategory) : salesOp.OrderByDescending(c => c.SaleCategory);
                    }
                    else if (sortColumnIndex == 11)
                    {
                        salesOp = sortDirection == "asc" ? salesOp.OrderBy(c => c.Product.SubCategory.Description) : salesOp.OrderByDescending(c => c.Product.SubCategory.Description);
                    }
                    else if (sortColumnIndex == 12)
                    {
                        salesOp = sortDirection == "asc" ? salesOp.OrderBy(c => c.SaleOperation != null ? c.SaleOperation.Operation : null) : salesOp.OrderByDescending(c => c.SaleOperation != null ? c.SaleOperation.Operation : null);
                    }
                    #endregion

                    if (checkMachines == "true")
                    {
                        salesOp = salesOp.Where(o => o.Product.SubCategory.Prefix.Contains("MC") || o.Product.SubCategory.Prefix.Contains("MI") || o.Product.SubCategory.Prefix.Contains("MP")
                        || o.Product.SubCategory.Prefix.Contains("MV") || o.Product.SubCategory.Prefix.Contains("MA")).ToList();
                    }
                    else if (checkAccesories == "true")
                    {
                        salesOp = salesOp.Where(o => o.Product.SubCategory.Prefix.Contains("AC")).ToList();
                    }
                    else if (checkSpares == "true")
                    {
                        salesOp = salesOp.Where(o => o.Product.SubCategory.CategoryId == 2 || o.Product.SubCategory.CategoryId == 3 || o.Product.SubCategory.CategoryId == 4).ToList();
                    }

                    var displayResult = salesOp.ToList();

                    var totalRecords = salesOp.Count();

                    return Json(new
                    {
                        iTotalRecords = totalRecords,
                        iTotalDisplayRecords = totalRecords,
                        aaData = displayResult
                    });
                }
                catch (Exception ex)
                {
                    _notify.Error(ex.Message);
                    return null;
                }

            }
            return null;
        }

        public async Task<IActionResult> _ViewAllOperationsSales(int clientId)
        {
            var response = await _saleOperationViewManager.FindBySpecification(new SaleOperationByClientId(clientId));

            var clientById = _viewManager.GetByIdLazyLoad(clientId);
            ViewBag.BusinessName = clientById.Result.Data.BusinessName.ToString();
            ViewBag.ClientId = clientId;

            if (response.Succeeded)
            {
                var sales = _mapper.Map<List<SaleOperationViewModel>>(response.Data);

                return View("_ViewAllOperationsSales", sales);
            }
            return null;
        }


        public async Task<JsonResult> _ViewComments(int id)
        {
            // To view comments
            var response = await _communicationViewManager.GetByIdAsync(id);
            if (response.Succeeded)
            {
                var communicationViewModel = _mapper.Map<CommunicationViewModel>(response.Data);

                if (communicationViewModel != null)
                {
                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_ViewComments", communicationViewModel) });
                }
                else
                {
                    return null;
                }
            }
            return null;
        }

        public async Task<JsonResult> _ViewReminderComments(int id)
        {
            // To view comments
            var response = await _reminderViewManager.GetByIdAsync(id);
            if (response.Succeeded)
            {
                var reminderViewModel = _mapper.Map<ReminderViewModel>(response.Data);

                if (reminderViewModel != null)
                {
                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_ViewReminderComments", reminderViewModel) });
                }
                else
                {
                    return null;
                }
            }
            return null;
        }

        [HttpGet]
        public JsonResult ShowAlerts(int alertId)
        {
            if (alertId == 1)
            {
                _notify.Warning(_localizer["Please, you must select a product with structure."]);
                return new JsonResult(new { isValid = false });
            }
            else if (alertId == 2)
            {
                _notify.Warning(_localizer["Please, you must select a functionality and a piece type."]);
                return new JsonResult(new { isValid = false });
            }
            return null;
        }

        public async Task<IActionResult> IndexAllCommunications(bool? postSale)
        {
            var response = await _communicationViewManager.FindBySpecification(new CommunicationsWithAllSpecifications());
            if (response.Succeeded)
            {
                ViewBag.postSale = null;
                var communications = _mapper.Map<List<CommunicationViewModel>>(response.Data);

                #region Asign saleOperation
                foreach (var com in communications)
                {
                    if (com.SaleOperationId > 0)
                    {
                        var saleOperation = await _saleOperationViewManager.FindBySpecification(new SaleOperationForSelect2(com.SaleOperationId));

                        if(saleOperation.Succeeded)
                        {
                            var saleOperationVM = _mapper.Map<List<SaleOperationViewModel>>(saleOperation.Data);
                            foreach (var so in saleOperationVM)
                            {
                                com.SaleOperation = so;
                                break;
                            }
                        }
                    }
                }
                #endregion

                return View("_ViewCommunications", communications);
            }         
            return null;
        }

        [HttpPost]
        public async Task<IActionResult> FilterCommunicatiosDate(DateTime startDate, DateTime endingDate)
        {
            var response = await _communicationViewManager.FindBySpecification(new CommunicationsWithAllSpecifications());
            if (response.Succeeded)
            {
                ViewBag.postSale = null;
                var communications = _mapper.Map<List<CommunicationViewModel>>(response.Data);

                var communicatiosFilter = communications.Where(d => d.ComunicationDate >= startDate && d.ComunicationDate <= endingDate).ToList();

                return PartialView("_ViewCommunications", communicatiosFilter);
            }

            return null;
        }

        public async Task<IActionResult> IndexAllReminders()
        {
            var response = await _reminderViewManager.FindBySpecification(new RemindersWithAllSpecifications());
            if (response.Succeeded)
            {
                var reminders = _mapper.Map<List<ReminderViewModel>>(response.Data);
                return View("_ViewAllReminders", reminders);
            }

            return null;
        }
        public async Task<IActionResult> IndexAllSalesOperations()
        {
            var response = await _saleOperationViewManager.FindBySpecification(new SaleOperationForSelect2());
            if (response.Succeeded)
            {
                var operations = _mapper.Map<List<SaleOperationViewModel>>(response.Data);
                foreach (var saleOperation in operations)
                {
                    var responseClient = await _viewManager.FindBySpecification(new ClientById(saleOperation.ClientId));
                    if (responseClient.Succeeded)
                    {
                        var clients = _mapper.Map<List<ClientViewModel>>(responseClient.Data);
                        foreach (var client in clients)
                        {
                            saleOperation.Client = client;
                            break;
                        }
                    }
                }

                #region DateTime for input date

                string YearToday = DateTime.Today.Year.ToString();
                string MonthToday = DateTime.Today.Month.ToString();
                string DayToday = DateTime.Today.Day.ToString();
                string dateTimeNow;

                if (MonthToday.Count() == 1)
                {
                    dateTimeNow = YearToday + "-" + "0" + MonthToday + "-";
                }
                else
                {
                    dateTimeNow = YearToday + "-" + MonthToday + "-";
                }
                if (DayToday.Count() == 1)
                {
                    dateTimeNow += "0" + DayToday;
                }
                else
                {
                    dateTimeNow += DayToday;
                }
                #endregion

                ViewBag.DateTimeNow = dateTimeNow;
                ViewBag.DateTime6MonthsBack = DateTime.Now.AddMonths(-6).ToShortDateString();

                return View("_ViewAllSalesOperations", operations);
            }
            return null;
        }
        public async Task<IActionResult> _EditSaleOperation(int saleOperationId, string view)
        {
            var responseSaleOperation = await _saleOperationViewManager.FindBySpecification(new SaleOperationEdit(saleOperationId));

            if (responseSaleOperation.Succeeded)
            {

                List<ProductViewModel> products = new List<ProductViewModel>();

                var saleOperation = _mapper.Map<List<SaleOperationViewModel>>(responseSaleOperation.Data).FirstOrDefault();
                foreach (var com in saleOperation.Communications)
                {
                    var cpResponse = await _consultedProductViewManager.FindBySpecification(new ConsultedProductByCommunicationId(com.Id));

                    if(cpResponse.Succeeded)
                    {
                        foreach (var cp in _mapper.Map<List<ConsultedProductViewModel>>(cpResponse.Data))
                        {
                            com.ConsultedProducts.Add(cp);
                        }
                    }
                }

                var orderHeadersResponse = await _orderHeaderViewManager.FindBySpecification(new OrderHeaderBySaleOperationId(saleOperationId));
                var orderHeaderViewModel = _mapper.Map<List<OrderHeaderViewModel>>(orderHeadersResponse.Data);

                foreach (var orderHeader in orderHeaderViewModel)
                {
                    var orderDetailResponse = await _orderDetailsViewManager.FindBySpecification(new OrderDetailById(orderHeader.Id));
                    if(orderDetailResponse.Succeeded)
                    {
                        foreach (var od in _mapper.Map<List<OrderDetailViewModel>>(orderDetailResponse.Data))
                        {
                            saleOperation.OrderDetails.Add(od);
                        }
                    }
                }

                var saleOperationVM = new SaleOperationViewModel
                {
                    StartDate = saleOperation.StartDate,
                    EndingDate = saleOperation.EndingDate,
                    Comments = saleOperation.Comments,
                    Communications = saleOperation.Communications,
                    OrderDetails = saleOperation.OrderDetails,
                    ClientId = saleOperation.ClientId,
                    ConcurrencyToken = saleOperation.ConcurrencyToken,
                    Id = saleOperation.Id,
                    Operation = saleOperation.Operation,
                    OperationNumber = saleOperation.OperationNumber,
                    State = saleOperation.State,
                    View = view,
                    Products = products
                };

                //REALIZAR LA CARGA DE FECHA ENDING DEL DIA DE HOY

                #region DateTime for Start & Ending Date

                string soStartDate = saleOperationVM.StartDate.ToShortDateString();
                string soEndingDate = saleOperationVM.EndingDate.ToShortDateString();

                string[] soStartDateArray = soStartDate.Split('/');
                string[] soEndingDateArray = soEndingDate.Split('/');

                var dayS = soStartDateArray[0];
                var monthS = soStartDateArray[1];
                var yearS = soStartDateArray[2];

                var dayE = soEndingDateArray[0];
                var monthE = soEndingDateArray[1];
                var yearE = soEndingDateArray[2];

                if (dayS.Length == 1)
                {
                    dayS = dayS.Insert(0, "0");
                }
                if (monthS.Length == 1)
                {
                    monthS = monthS.Insert(0, "0");
                }
                if (dayE.Length == 1)
                {
                    dayE = dayE.Insert(0, "0");
                }
                if (monthE.Length == 1)
                {
                    monthE = monthE.Insert(0, "0");
                }
                saleOperationVM.StartDateString = yearS + "-" + monthS + "-" + dayS;
                //saleOperationVM.EndingDateString = yearE + "-" + monthE + "-" + dayE;

                #endregion

                #region DateTime for input date

                string YearToday = DateTime.Today.Year.ToString();
                string MonthToday = DateTime.Today.Month.ToString();
                string DayToday = DateTime.Today.Day.ToString();
                string dateTimeNow;

                if (MonthToday.Count() == 1)
                {
                    dateTimeNow = YearToday + "-" + "0" + MonthToday + "-";
                }
                else
                {
                    dateTimeNow = YearToday + "-" + MonthToday + "-";
                }
                if (DayToday.Count() == 1)
                {
                    dateTimeNow += "0" + DayToday;
                }
                else
                {
                    dateTimeNow += DayToday;
                }
                #endregion


                saleOperationVM.EndingDateString = dateTimeNow;


                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_EditSaleOperation", saleOperationVM) });

            }
            return null;
        }

        [HttpPost]
        public async Task<JsonResult> OnPostEditSaleOperation(int saleOperationId, string state, DateTime endingDate, DateTime startDate, string comment, string view)
        {
            if (saleOperationId != 0) // Update
            {
                //var saleOperation = await _saleOperationViewManager.GetByIdLazyLoad(saleOperationId);
                var responseSaleOperation = await _saleOperationViewManager.FindBySpecification(new SaleOperationForSelect2(saleOperationId));

                var responseCom = await _communicationViewManager.FindBySpecification(new CommunicationBySaleOperationId(saleOperationId)); // Traemos las comunicaciones asociadas

                if (responseCom.Succeeded && responseSaleOperation.Succeeded)
                {
                    var communications = _mapper.Map<List<CommunicationViewModel>>(responseCom.Data);

                    foreach (var com in communications)
                    {
                        var responseCP = await _consultedProductViewManager.FindBySpecification(new ConsultedProductByCommunicationId(com.Id)); // Traemos los productos consultados asociados a las comunicaciones

                        var consultedProducts = _mapper.Map<List<ConsultedProductViewModel>>(responseCP.Data);

                        com.ConsultedProducts = consultedProducts;
                    }

                    #region Region for States

                    switch(state)
                    {
                        case "1":
                            state = _localizer["In progress"];
                            break;
                        case "2":
                            state = _localizer["Concretized"];
                            break;
                        case "3":
                            state = _localizer["Not completed"];
                            break;
                    }

                    #endregion


                    var saleOperation = _mapper.Map<List<SaleOperationViewModel>>(responseSaleOperation.Data).FirstOrDefault();

                    var saleOperationVM = new SaleOperationViewModel
                    {
                        StartDate = startDate, //
                        EndingDate = endingDate,//
                        Comments = comment,//
                        Communications = communications,
                        ClientId = saleOperation.ClientId,
                        ConcurrencyToken = saleOperation.ConcurrencyToken,
                        Id = saleOperation.Id,
                        Operation = saleOperation.Operation,
                        OperationNumber = saleOperation.OperationNumber,
                        State = state//
                    };

                    #region DateTime for Start

                    string soStartDate = startDate.ToShortDateString();

                    string[] soStartDateArray = soStartDate.Split('/');

                    var dayS = soStartDateArray[0];
                    var monthS = soStartDateArray[1];
                    var yearS = soStartDateArray[2];


                    if (dayS.Length == 1)
                    {
                        dayS = dayS.Insert(0, "0");
                    }
                    if (monthS.Length == 1)
                    {
                        monthS = monthS.Insert(0, "0");
                    }

                    saleOperationVM.StartDateString = yearS + "-" + monthS + "-" + dayS;

                    #endregion



                    if (saleOperationVM.State != _localizer["Concretada"])
                    {
                        saleOperationVM.EndingDate = DateTime.MinValue;
                        saleOperationVM.EndingDateString = "01/01/0001";
                    }

                    var soDTO = _mapper.Map<SaleOperationDTO>(saleOperationVM);
                    var result = await _saleOperationViewManager.UpdateAsync(soDTO); // Actualizamos la operacion

                    if (result.Succeeded)
                    {
                        _notify.Information(_localizer["Operation update."]);

                        if (view == "_ViewAllSalesOperations")
                        {
                            var response = await _saleOperationViewManager.FindBySpecification(new SaleOperationForSelect2());

                            if(response.Succeeded)
                            {
                                var viewModel = _mapper.Map<List<SaleOperationViewModel>>(response.Data);

                                //var html = await _viewRenderer.RenderViewToStringAsync(view, viewModel);

                                return new JsonResult(new { isValid = true});
                            }
                        }
                        else //foreach
                        {
                            var response = await _saleOperationViewManager.FindBySpecification(new SaleOperationByClientId(saleOperationVM.ClientId)); 

                            if(response.Succeeded)
                            {
                                var clientById = _viewManager.GetByIdLazyLoad(saleOperationVM.ClientId);

                                var viewModel = _mapper.Map<List<SaleOperationViewModel>>(response.Data);

                                var html = await _viewRenderer.RenderViewToStringAsync(view, viewModel);

                                return new JsonResult(new { isValid = true, html = html, ClientId = saleOperationVM.ClientId, BusinessName = clientById.Result.Data.BusinessName.ToString() });
                            }
                        }
                        
                    }
                }

                else
                {
                    _notify.Error("Error");
                    return null;
                }
            }
            else
            {
                _notify.Error("Error");
                return null;
            }

            return null;
        }
        public async Task<IActionResult> LoadAllCommunications(DatatableParamsViewModel datatableParams, string sSearch_2, string sSearch_3, string sSearch_4, string sSearch_5, string sSearch_6, string sSearch_7, string sSearch_8, string sSearch_9, string sSearch_10, DateTime dateFromFilterValue, DateTime dateToFilterValue)
        {
            var response = await _communicationViewManager.FindBySpecification(new CommunicationsWithAllSpecifications());

            if(response.Succeeded)
            {
                try
                {
                    var communicationsVM = _mapper.Map<List<CommunicationViewModel>>(response.Data);

                    List<CommunicationViewModel> listCommunications = new List<CommunicationViewModel>();

                    foreach (var com in communicationsVM)
                    {
                        com.ConsultedProductsCount = "0";

                        if(com.Incoming)
                        {
                            com.IncomingOrOutgoin = _localizer["Incoming"];
                        }else if(com.Outgoing){
                            com.IncomingOrOutgoin = _localizer["Outgoing"];
                        }

                        if (com.ConsultedProducts.Count > 0)
                        {
                            com.ConsultedProductsCount = "" + com.ConsultedProducts.Count;

                            var responseCP = await _consultedProductViewManager.FindBySpecification(new ConsultedProductByCommunicationId(com.Id));

                            if (response.Succeeded)
                            {
                                var consultedProductsVM = _mapper.Map<List<ConsultedProductViewModel>>(responseCP.Data);

                                foreach (var cp in consultedProductsVM)
                                {
                                    CommunicationViewModel communicationNew = com;
                                    communicationNew.cpCodeAndDescription = cp.ProductCode + " " + cp.ProductDescription;
                                    communicationNew.cpFuncionality = cp.Functionality;
                                    communicationNew.cpPieceType = cp.PieceType;

                                    listCommunications.Add(communicationNew);
                                }
                            }
                        }
                        else
                        {
                            listCommunications.Add(com);
                        }
                    }

                    IEnumerable<CommunicationViewModel> communications = communicationsVM.ToList();


                    #region Filters

                    //From date -> To date
                    communications = communications.Where(c => c.ComunicationDate>= dateFromFilterValue && c.ComunicationDate <= dateToFilterValue).ToList();

                    //Client
                    if (!string.IsNullOrEmpty(sSearch_2))
                    {
                        communications = communications.Where(s => s.Client.BusinessName.ToString().ToLower().Contains(sSearch_2.ToLower())).ToList();
                    }

                    //ComunicationDate
                    if (!string.IsNullOrEmpty(sSearch_3))
                    {
                        communications = communications.Where(c => c.ComunicationDate.ToString("dd/MM/yyyy").Contains(sSearch_3.ToLower())).ToList();
                    }

                    //cpCodeAndDescription
                    if (!string.IsNullOrEmpty(sSearch_4))
                    {
                        communications = communications.Where(s => s.cpCodeAndDescription != null && s.cpCodeAndDescription .ToString().ToLower().Contains(sSearch_4.ToLower())).ToList();
                    }

                    //Funcionality
                    if (!string.IsNullOrEmpty(sSearch_5))
                    {
                        communications = communications.Where(s => s.cpFuncionality != null && s.cpFuncionality.ToString().ToLower().Contains(sSearch_5.ToLower())).ToList();
                    }

                    //cpPieceType
                    if (!string.IsNullOrEmpty(sSearch_6))
                    {
                        communications = communications.Where(s => s.cpPieceType != null && s.cpPieceType.ToString().ToLower().Contains(sSearch_6.ToLower())).ToList();
                    }

                    //Origin contact
                    if (!string.IsNullOrEmpty(sSearch_7))
                    {
                        communications = communications.Where(s => s.IncomingOrOutgoin != null && s.IncomingOrOutgoin.ToString().ToLower().Contains(sSearch_7.ToLower())).ToList();
                    }
                    
                    //Contact source
                    if (!string.IsNullOrEmpty(sSearch_8))
                    {
                        communications = communications.Where(s => s.ContactSource.ToString().ToLower().Contains(sSearch_8.ToLower())).ToList();
                    }

                    //Sale operation
                    if (!string.IsNullOrEmpty(sSearch_9))
                    {
                        communications = communications.Where(s => s.SaleOperation != null && s.SaleOperation.Operation.ToString().ToLower().Contains(sSearch_9.ToLower())).ToList();
                    }
                    //Communication
                    if (!string.IsNullOrEmpty(sSearch_10))
                    {
                        communications = communications.Where(s => s.CommunicationName != null && s.CommunicationName.ToString().ToLower().Contains(sSearch_10.ToLower()) || s.CommunicationNumber.ToString().Contains(sSearch_10)).ToList();
                    }
                    #endregion

                    #region Order

                    var sortColumnIndex = datatableParams.iSortCol_0;
                    var sortDirection = datatableParams.sSortDir_0;

                    if (sortColumnIndex == 2)
                    {
                        communications = sortDirection == "asc" ? communications.OrderBy(c => c.Client.BusinessName) : communications.OrderByDescending(c => c.Client.BusinessName);
                    }
                    else if (sortColumnIndex == 3)
                    {
                        communications = sortDirection == "asc" ? communications.OrderBy(c => c.ComunicationDate) : communications.OrderByDescending(c => c.ComunicationDate);
                    }
                    else if (sortColumnIndex == 4)
                    {
                        communications = sortDirection == "asc" ? communications.OrderBy(c => c.ConsultedProductsCount) : communications.OrderByDescending(c => c.ConsultedProductsCount);
                    }
                    else if (sortColumnIndex == 5)
                    {
                        communications = sortDirection == "asc" ? communications.OrderBy(c => c.cpFuncionality) : communications.OrderByDescending(c => c.cpFuncionality);
                    }
                    else if (sortColumnIndex == 6)
                    {
                        communications = sortDirection == "asc" ? communications.OrderBy(c => c.cpPieceType) : communications.OrderByDescending(c => c.cpPieceType);
                    }
                    else if (sortColumnIndex == 7)
                    {
                        communications = sortDirection == "asc" ? communications.OrderBy(c => c.IncomingOrOutgoin) : communications.OrderByDescending(c => c.IncomingOrOutgoin);
                    }
                    else if (sortColumnIndex == 8)
                    {
                        communications = sortDirection == "asc" ? communications.OrderBy(c => c.ContactSource) : communications.OrderByDescending(c => c.ContactSource);
                    }
                    else if (sortColumnIndex == 9)
                    {
                        communications = sortDirection == "asc" ? communications.OrderBy(c => c.SaleOperation.Operation) : communications.OrderByDescending(c => c.SaleOperation.Operation);
                    }
                    else if (sortColumnIndex == 10)
                    {
                        communications = sortDirection == "asc" ? communications.OrderBy(c => c.CommunicationName) : communications.OrderByDescending(c => c.CommunicationName);
                    }
                    #endregion



                    var displayResult = communications.ToList();

                    var totalRecords = communications.ToList();

                    return Json(new
                    {
                        iTotalRecords = totalRecords,
                        iTotalDisplayRecords = totalRecords,
                        aaData = displayResult
                    });
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
            else
            {
                return null;
            }
        }
        

        public async Task<IActionResult> LoadAllReminders(DatatableParamsViewModel datatableParams, string sSearch_2, string sSearch_3, string sSearch_4, string sSearch_5, string sSearch_6, string sSearch_7, DateTime dateFromFilterValue, DateTime dateToFilterValue)
        {
            var response = await _reminderViewManager.FindBySpecification(new RemindersWithAllSpecifications());

            if(response.Succeeded)
            {
                try
                {
                    var remindersVM = _mapper.Map<List<ReminderViewModel>>(response.Data);

                    IEnumerable<ReminderViewModel> reminders = remindersVM.ToList();

                    #region Filters

                    //From date -> To date
                    reminders = reminders.Where(o => o.ReminderDate >= dateFromFilterValue && o.ReminderDate <= dateToFilterValue);

                    //Client
                    if (!string.IsNullOrEmpty(sSearch_2))
                    {
                        reminders = reminders.Where(s => s.Client.BusinessName.ToString().ToLower().Contains(sSearch_2.ToLower())).ToList();
                    }

                    //Contact date
                    if (!string.IsNullOrEmpty(sSearch_3))
                    {
                        reminders = reminders.Where(s => s.ContactDate.ToString("dd/MM/yyyy").Contains(sSearch_3.ToLower())).ToList();
                    }

                    //Reminder date
                    if (!string.IsNullOrEmpty(sSearch_4))
                    {
                        reminders = reminders.Where(s => s.ReminderDate.HasValue && s.ReminderDate.Value.ToString("dd/MM/yyyy").Contains(sSearch_4.ToLower())).ToList();
                    }
                    #endregion

                    #region Order

                    var sortColumnIndex = datatableParams.iSortCol_0;
                    var sortDirection = datatableParams.sSortDir_0;

                    if (sortColumnIndex == 2)
                    {
                        reminders = sortDirection == "asc" ? reminders.OrderBy(s => s.Client.BusinessName) : reminders.OrderByDescending(s => s.Client.BusinessName);
                    }
                    else if (sortColumnIndex == 3)
                    {
                        reminders = sortDirection == "asc" ? reminders.OrderBy(s => s.ContactDate) : reminders.OrderByDescending(s => s.ContactDate);
                    }
                    else if (sortColumnIndex == 4)
                    {
                        reminders = sortDirection == "asc" ? reminders.OrderByDescending(s => s.ReminderDate.HasValue).ThenBy(s => s.ReminderDate) : reminders.OrderByDescending(so => so.ReminderDate.HasValue).ThenByDescending(s => s.ReminderDate);
                    }
                    #endregion

                    var displayResult = reminders.ToList();

                    var totalRecords = reminders.Count();

                    return Json(new
                    {
                        iTotalRecords = totalRecords,
                        iTotalDisplayRecords = totalRecords,
                        aaData = displayResult
                    });
                }
                catch(Exception ex)
                {
                    _notify.Error(ex.Message);
                    return null;
                }
            }
            else
            {
                return null;
            }

        }
        public async Task<IActionResult> LoadAllSaleOperations(DatatableParamsViewModel datatableParams, string sSearch_2, string sSearch_3, string sSearch_4, string sSearch_5, string sSearch_6, string sSearch_7, DateTime dateFromFilterValue, DateTime dateToFilterValue)
        {
            var response = await _saleOperationViewManager.FindBySpecification(new SaleOperationForSelect2());

            if(response.Succeeded)
            {
                try
                {
                    var operationsVM = _mapper.Map<List<SaleOperationViewModel>>(response.Data);

                    foreach (var so in operationsVM)
                    {
                        var responseClients = await _viewManager.FindBySpecification(new ClientById(so.ClientId));
                        
                        if(response.Succeeded)
                        {
                            var client = _mapper.Map<List<ClientViewModel>>(responseClients.Data).FirstOrDefault();

                            so.Client = client;
                        }
                    }

                    IEnumerable<SaleOperationViewModel> operations = operationsVM.ToList();

                    #region Filters

                    // From date -> To date
                    operations = operations.Where(o => o.StartDate >= dateFromFilterValue && o.StartDate <= dateToFilterValue);


                    //Operation
                    if (!string.IsNullOrEmpty(sSearch_2))
                    {
                        operations = operations.Where(s => s.Operation.ToString().ToLower().Contains(sSearch_2.ToLower())).ToList();
                    }

                    //Client
                    if (!string.IsNullOrEmpty(sSearch_3))
                    {
                        operations = operations.Where(s => s.Client.BusinessName.ToString().ToLower().Contains(sSearch_3.ToLower())).ToList();
                    }

                    //StartDate
                    if (!string.IsNullOrEmpty(sSearch_4))
                    {
                        operations = operations.Where(s => s.StartDate.ToString("dd/MM/yyyy").Contains(sSearch_4.ToLower())).ToList();
                    }

                    //EndingDate
                    if (!string.IsNullOrEmpty(sSearch_5))
                    {
                        operations = operations.Where(s => s.EndingDate.ToString("dd/MM/yyyy").Contains(sSearch_5.ToLower())).ToList();
                    }

                    //State
                    if (!string.IsNullOrEmpty(sSearch_6))
                    {
                        operations = operations.Where(s => s.State.ToString().ToLower().Contains(sSearch_6.ToLower())).ToList();
                    }
                    #endregion

                    #region Order

                    var sortColumnIndex = datatableParams.iSortCol_0;
                    var sortDirection = datatableParams.sSortDir_0;

                    if (sortColumnIndex == 2)
                    {
                        operations = sortDirection == "asc" ? operations.OrderBy(s => s.Operation) : operations.OrderByDescending(s => s.Operation);
                    }
                    else if (sortColumnIndex == 3)
                    {
                        operations = sortDirection == "asc" ? operations.OrderBy(s => s.Client.BusinessName) : operations.OrderByDescending(s => s.Client.BusinessName);
                    }
                    else if (sortColumnIndex == 4)
                    {
                        operations = sortDirection == "asc" ? operations.OrderBy(s => s.StartDate) : operations.OrderByDescending(s => s.StartDate);
                    }
                    else if (sortColumnIndex == 5)
                    {
                        operations = sortDirection == "asc" ? operations.OrderBy(s => s.EndingDate) : operations.OrderByDescending(s => s.EndingDate);
                    }
                    else if (sortColumnIndex == 6)
                    {
                        operations = sortDirection == "asc" ? operations.OrderBy(s => s.State) : operations.OrderByDescending(s => s.State);
                    }
                    #endregion

                    var displayResult = operations.ToList();

                    var totalRecords = operations.Count();

                    return Json(new
                    {
                        iTotalRecords = totalRecords,
                        iTotalDisplayRecords = totalRecords,
                        aaData = displayResult
                    });
                }
                catch(Exception ex)
                {
                    _notify.Error(ex.Message);
                    return null;
                }
            }
            else
            {
                return null;
            }

        }

    }
}