using ERP.Application.Constants;
using ERP.Application.DTOs.Entities.Commons;
using ERP.Application.DTOs.Entities.Logistics.Incomes;
using ERP.Application.DTOs.Entities.Production;
using ERP.Application.Interfaces.ViewManagers.CommercialDocuments.DeliveryNote;
using ERP.Application.Interfaces.ViewManagers.Commons;
using ERP.Application.Interfaces.ViewManagers.Logistics.Incomes;
using ERP.Application.Interfaces.ViewManagers.Production;
using ERP.Application.Interfaces.ViewManagers.ProductMod;
using ERP.Application.Interfaces.ViewManagers.Purchases.MissingProducts;
using ERP.Application.Interfaces.ViewManagers.Purchases.Providers;
using ERP.Application.Interfaces.ViewManagers.Sales;
using ERP.Application.Specification.Commons.UnitMeasureSpecifications;
using ERP.Application.Specification.Logistics.Incomes;
using ERP.Application.Specification.Production.WorkActivitySpecifications;
using ERP.Application.Specification.Production.WorkOrderItemSpecifications;
using ERP.Application.Specification.ProductMod;
using ERP.Application.Specification.ProductMod.StructureSpecifications;
using ERP.Application.Specification.Purchases.MissingProducts;
using ERP.Application.Specification.Sales;
using ERP.Domain.Entities.Production;
using ERP.Infrastructure.DbContexts;
using ERP.Language;
using ERP.Web.Abstractions;
using ERP.Web.Areas.Logistics.Models;
using ERP.Web.Areas.Production.Models;
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
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Web.Areas.Logistics.Controllers
{
    [Area("Logistics")]
    public class IncomeController : BaseController<IncomeController>
    {
        private IdentityContext _identityContext;
        private readonly IStringLocalizer<SharedResource> _localizer;

        private readonly IIncomeHeaderViewManager _incomeHeaderViewManager;
        private readonly IIncomeDetailViewManager _incomeDetailViewManager;
        private readonly IIncomeStateViewManager _incomeStateViewManager;

        private readonly IProductViewManager _productViewManager;
        private readonly IStructureViewManager _structureViewManager;
        private readonly IUnitMeasureViewManager _unitMeasureViewManager;
        private readonly IProviderViewManager _providerViewManager;
        private readonly IStationViewManager _stationViewManager;
        private readonly IOrderDetailViewManager _orderDetailViewManager;
        private readonly IWorkActivityViewManager _workActivityViewManager;
        private readonly IWorkActionViewManager _workActionViewManager;
        private readonly IWorkOrderItemViewManager _workOrderItemViewManager;
        private readonly IDeliveryNoteHeaderViewManager _deliveryNoteHeaderViewManager;
        private readonly IMissingProductViewManager _missingProductViewManager;

        public IncomeController(IdentityContext identityContext, IStringLocalizer<SharedResource> localizer, IIncomeHeaderViewManager incomeHeaderViewManager, IIncomeDetailViewManager incomeDetailViewManager, IIncomeStateViewManager incomeStateViewManager, IProductViewManager productViewManager, IStructureViewManager structureViewManager, IUnitMeasureViewManager unitMeasureViewManager, IProviderViewManager providerViewManager, IStationViewManager stationViewManager, IOrderDetailViewManager orderDetailViewManager, IWorkActivityViewManager workActivityViewManager, IWorkActionViewManager workActionViewManager, IWorkOrderItemViewManager workOrderItemViewManager, IDeliveryNoteHeaderViewManager deliveryNoteDetailViewManager, IMissingProductViewManager missingProductViewManager)
        {
            _identityContext = identityContext;
            _localizer = localizer;
            _incomeHeaderViewManager = incomeHeaderViewManager;
            _incomeDetailViewManager = incomeDetailViewManager;
            _incomeStateViewManager = incomeStateViewManager;
            _productViewManager = productViewManager;
            _structureViewManager = structureViewManager;
            _unitMeasureViewManager = unitMeasureViewManager;
            _providerViewManager = providerViewManager;
            _stationViewManager = stationViewManager;
            _orderDetailViewManager = orderDetailViewManager;
            _workActivityViewManager = workActivityViewManager;
            _workActionViewManager = workActionViewManager;
            _workOrderItemViewManager = workOrderItemViewManager;
            _deliveryNoteHeaderViewManager = deliveryNoteDetailViewManager;
            _missingProductViewManager = missingProductViewManager;
        }

        public IActionResult Index()
        {
            var incomeDetailVM = new IncomeDetailViewModel();
            ViewData["IncomesTitle"] = _localizer["Incomes"].ToString();
            return View(incomeDetailVM);
        }

        public async Task<IActionResult> LoadAllIncomes(DatatableParamsViewModel datatableParamsViewModel, string sSearch_2, string sSearch_3, string sSearch_4, string sSearch_5, string sSearch_6, string sSearch_7, string sSearch_8, string sSearch_9, string sSearch_10, string sSearch_11, string sSearch_12, string sSearch_13, string sSearch_14, string sSearch_15, string sSearch_16, string sSearch_17, string sSearch_18, string sSearch_19, string sSearch_20, string sSearch_21, string sSearch_22, string sSearch_23, string sSearch_24, DateTime dateFromFilterValue, DateTime dateToFilterValue)
        {
            try
            {
                var loadIncomes = await _incomeDetailViewManager.FindBySpecification(new IncomeDetailsWithAllSpecifications());
                if (loadIncomes.Succeeded)
                {
                    var incomesVM = _mapper.Map<IEnumerable<IncomeDetailViewModel>>(loadIncomes.Data);

                    var usersList = _identityContext.Users.ToList();

                    foreach (var iVM in incomesVM)
                    {
                        foreach (var u in usersList)
                        {
                            if (u.Id == iVM.CreatedBy)
                            {
                                iVM.CreatedBy = u.FirstName.ToString() + ", " + u.LastName.ToString();
                            }
                        }
                    }

                    // From date -> To date
                    incomesVM = incomesVM.Where(id => id.IncomeHeader.IncomeDate.Date >= dateFromFilterValue && id.IncomeHeader.IncomeDate.Date <= dateToFilterValue);

                    if (!string.IsNullOrEmpty(sSearch_2))
                    {
                        incomesVM = incomesVM.Where(id => id.IncomeDate.HasValue ? id.IncomeDate.Value.ToString("dd/MM/yyyy").Contains(sSearch_2) : id.IncomeHeader.IncomeDate.ToString("dd/MM/yyyy").Contains(sSearch_2)).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_3))
                    {
                        incomesVM = incomesVM.Where(id => id.IncomeProduct != null && id.IncomeProduct.Code.ToString().ToLower().Contains(sSearch_3.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_4))
                    {
                        incomesVM = incomesVM.Where(id => id.IncomeProduct != null && id.IncomeProduct.Description.ToString().ToLower().Contains(sSearch_4.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_5))
                    {
                        incomesVM = incomesVM.Where(id => id.Quantity.ToString().ToLower().Contains(sSearch_5.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_6))
                    {
                        incomesVM = incomesVM.Where(id => id.Unit != null && id.Unit.Description.ToString().ToLower().Contains(sSearch_6.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_7))
                    {
                        incomesVM = incomesVM.Where(id => id.IncomeHeader != null && id.IncomeHeader.Provider.BusinessName.ToString().ToLower().Contains(sSearch_7.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_8))
                    {
                        incomesVM = incomesVM.Where(id => id.IncomeHeader != null && id.IncomeHeader.TransportProvider.BusinessName.ToString().ToLower().Contains(sSearch_8.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_9))
                    {
                        incomesVM = incomesVM.Where(id => id.IncomeHeader.DeliveryNoteNumber != null && id.IncomeHeader.DeliveryNoteNumber.ToString().ToLower().Contains(sSearch_9.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_10))
                    {
                        incomesVM = incomesVM.Where(id => id.BatchNumber != null && id.BatchNumber.ToString().ToLower().Contains(sSearch_10.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_11))
                    {
                        incomesVM = incomesVM.Where(id => id.Reception.ToString().ToLower().Contains(sSearch_11.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_12))
                    {
                        incomesVM = incomesVM.Where(id => id.NextStation.ToString().ToLower().Contains(sSearch_12.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_13))
                    {
                        incomesVM = incomesVM.Where(id => id.IncomeState.Description.ToString().ToLower().Contains(sSearch_13.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_14))
                    {
                        incomesVM = incomesVM.Where(id => id.OCNumber != null && id.OCNumber.ToString().ToLower().Contains(sSearch_14.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_15))
                    {
                        incomesVM = incomesVM.Where(id => id.IncomeHeader != null && id.IncomeHeader.InvoiceNumber != null && id.IncomeHeader.InvoiceNumber.ToString().ToLower().Contains(sSearch_15.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_16))
                    {
                        incomesVM = incomesVM.Where(id => id.OTNumber != null && id.OTNumber.ToString().ToLower().Contains(sSearch_16.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_17))
                    {
                        incomesVM = incomesVM.Where(id => id.ProductNumber != null && id.ProductNumber.ToString().ToLower().Contains(sSearch_17.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_18))
                    {
                        incomesVM = incomesVM.Where(id => id.FatherProduct != null && id.FatherProduct.CodeAndDescription.ToString().ToLower().Contains(sSearch_18.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_19))
                    {
                        incomesVM = incomesVM.Where(id => id.FatherStructure != null && id.FatherStructure.Description.ToString().ToLower().Contains(sSearch_19.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_20))
                    {
                        incomesVM = incomesVM.Where(id => id.IncomeHeader.ExternalProcessStation != null && id.IncomeHeader.ExternalProcessStation.AbreviationDescription.ToString().ToLower().Contains(sSearch_20.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_21))
                    {
                        incomesVM = incomesVM.Where(id => id.IncomeHeader.IncomeDate.ToString().ToLower().Contains(sSearch_21.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_22))
                    {
                        incomesVM = incomesVM.Where(id => id.IncomeDate.HasValue ? Math.Abs((id.IncomeHeader.IncomeDate.Date - id.IncomeDate.Value.Date).Days).ToString().ToLower().Contains(sSearch_22.ToLower()) : "0 días".ToString().ToLower().Contains(sSearch_22.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_23))
                    {
                        incomesVM = incomesVM.Where(id => id.Weight.ToString().ToLower().Contains(sSearch_23.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_24))
                    {
                        incomesVM = incomesVM.Where(id => id.CreatedBy.ToString().ToLower().Contains(sSearch_24.ToLower())).ToList();
                    }

                    var sortColumnIndex = datatableParamsViewModel.iSortCol_0;
                    var sortDirection = datatableParamsViewModel.sSortDir_0;

                    if (sortColumnIndex == 2)
                    {
                        incomesVM = sortDirection == "asc" ? incomesVM.OrderBy(id => id.IncomeDate.HasValue ? id.IncomeDate.Value.Date : id.IncomeHeader.IncomeDate.Date) : incomesVM.OrderByDescending(id => id.IncomeDate.HasValue ? id.IncomeDate.Value.Date : id.IncomeHeader.IncomeDate.Date);
                    }
                    else if (sortColumnIndex == 3)
                    {
                        incomesVM = sortDirection == "asc" ? incomesVM.OrderBy(id => id.IncomeProduct.Code) : incomesVM.OrderByDescending(id => id.IncomeProduct.Code);
                    }
                    else if (sortColumnIndex == 4)
                    {
                        incomesVM = sortDirection == "asc" ? incomesVM.OrderBy(id => id.IncomeProduct.Description) : incomesVM.OrderByDescending(id => id.IncomeProduct.Description);
                    }
                    else if (sortColumnIndex == 5)
                    {
                        incomesVM = sortDirection == "asc" ? incomesVM.OrderBy(id => id.Quantity) : incomesVM.OrderByDescending(id => id.Quantity);
                    }
                    else if (sortColumnIndex == 6)
                    {
                        incomesVM = sortDirection == "asc" ? incomesVM.OrderBy(id => id.Unit.Description) : incomesVM.OrderByDescending(id => id.Unit.Description);
                    }
                    else if (sortColumnIndex == 7)
                    {
                        incomesVM = sortDirection == "asc" ? incomesVM.OrderBy(id => id.IncomeHeader.Provider.BusinessName) : incomesVM.OrderByDescending(id => id.IncomeHeader.Provider.BusinessName);
                    }
                    else if (sortColumnIndex == 8)
                    {
                        incomesVM = sortDirection == "asc" ? incomesVM.OrderBy(id => id.IncomeHeader.TransportProvider.BusinessName) : incomesVM.OrderByDescending(id => id.IncomeHeader.TransportProvider.BusinessName);
                    }
                    else if (sortColumnIndex == 9)
                    {
                        incomesVM = sortDirection == "asc" ? incomesVM.OrderBy(id => id.IncomeHeader.DeliveryNoteNumber) : incomesVM.OrderByDescending(id => id.IncomeHeader.DeliveryNoteNumber);
                    }
                    else if (sortColumnIndex == 10)
                    {
                        incomesVM = sortDirection == "asc" ? incomesVM.OrderBy(id => id.BatchNumber) : incomesVM.OrderByDescending(id => id.BatchNumber);
                    }
                    else if (sortColumnIndex == 11)
                    {
                        incomesVM = sortDirection == "asc" ? incomesVM.OrderBy(id => id.Reception) : incomesVM.OrderByDescending(id => id.Reception);
                    }
                    else if (sortColumnIndex == 12)
                    {
                        incomesVM = sortDirection == "asc" ? incomesVM.OrderBy(id => id.NextStation) : incomesVM.OrderByDescending(id => id.NextStation);
                    }
                    else if (sortColumnIndex == 13)
                    {
                        incomesVM = sortDirection == "asc" ? incomesVM.OrderBy(id => id.IncomeState.Description) : incomesVM.OrderByDescending(id => id.IncomeState.Description);
                    }
                    else if (sortColumnIndex == 14)
                    {
                        incomesVM = sortDirection == "asc" ? incomesVM.OrderBy(id => id.OCNumber) : incomesVM.OrderByDescending(id => id.OCNumber);
                    }
                    else if (sortColumnIndex == 15)
                    {
                        incomesVM = sortDirection == "asc" ? incomesVM.OrderBy(id => id.IncomeHeader.InvoiceNumber) : incomesVM.OrderByDescending(id => id.IncomeHeader.InvoiceNumber);
                    }
                    else if (sortColumnIndex == 16)
                    {
                        incomesVM = sortDirection == "asc" ? incomesVM.OrderBy(id => id.OTNumber) : incomesVM.OrderByDescending(id => id.OTNumber);
                    }
                    else if (sortColumnIndex == 17)
                    {
                        incomesVM = sortDirection == "asc" ? incomesVM.OrderBy(id => id.ProductNumber) : incomesVM.OrderByDescending(id => id.ProductNumber);
                    }
                    else if (sortColumnIndex == 18)
                    {
                        incomesVM = sortDirection == "asc" ? incomesVM.OrderBy(id => id.FatherProduct.CodeAndDescription) : incomesVM.OrderByDescending(id => id.FatherProduct.CodeAndDescription);
                    }
                    else if (sortColumnIndex == 19)
                    {
                        incomesVM = sortDirection == "asc" ? incomesVM.OrderBy(id => id.FatherStructure != null ? id.FatherStructure.Description : null) : incomesVM.OrderByDescending(id => id.FatherStructure != null ? id.FatherStructure.Description : null);
                    }
                    else if (sortColumnIndex == 20)
                    {
                        incomesVM = sortDirection == "asc" ? incomesVM.OrderBy(id => id.IncomeHeader.ExternalProcessStation.AbreviationDescription) : incomesVM.OrderByDescending(id => id.IncomeHeader.ExternalProcessStation.AbreviationDescription);
                    }
                    else if (sortColumnIndex == 21)
                    {
                        incomesVM = sortDirection == "asc" ? incomesVM.OrderBy(id => id.IncomeHeader.IncomeDate.Date) : incomesVM.OrderByDescending(id => id.IncomeHeader.IncomeDate.Date);
                    }
                    else if (sortColumnIndex == 22)
                    {
                        incomesVM = sortDirection == "asc" ? incomesVM.OrderBy(id => id.IncomeDate.HasValue ? Math.Abs((id.IncomeHeader.IncomeDate.Date - id.IncomeDate.Value.Date).Days).ToString() : "0 días") : incomesVM.OrderByDescending(id => id.IncomeDate.HasValue ? Math.Abs((id.IncomeHeader.IncomeDate.Date - id.IncomeDate.Value.Date).Days).ToString() : "0 días");
                    }
                    else if (sortColumnIndex == 23)
                    {
                        incomesVM = sortDirection == "asc" ? incomesVM.OrderBy(id => id.Weight) : incomesVM.OrderByDescending(id => id.Weight);
                    }
                    else if (sortColumnIndex == 24)
                    {
                        incomesVM = sortDirection == "asc" ? incomesVM.OrderBy(id => id.CreatedBy) : incomesVM.OrderByDescending(id => id.CreatedBy);
                    }

                    var displayResult = incomesVM.Skip(datatableParamsViewModel.iDisplayStart)
                      .Take(datatableParamsViewModel.iDisplayLength).ToList();

                    var totalRecords = incomesVM.Count();

                    return Json(new
                    {
                        datatableParamsViewModel.sEcho,
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

        [Authorize(Policy = Permissions.Logistic.View)]
        public async Task<JsonResult> OnGetCreateOrEdit(int id = 0)
        {
            if (id == 0)
            {
                // To create Income
                var incomeHeaderVM = new IncomeHeaderViewModel();

                var getAllProviders = await _providerViewManager.LoadAll();
                if (getAllProviders.Succeeded)
                {
                    var providersVM = _mapper.Map<List<ProviderViewModel>>(getAllProviders.Data);
                    var productAndServiceProviders = providersVM.Where(pr => pr.ProviderType == "Productos" || pr.ProviderType == "Servicios").ToList();
                    incomeHeaderVM.Providers = new SelectList(productAndServiceProviders, nameof(ProviderViewModel.Id), nameof(ProviderViewModel.getBussinessNameOrName), null, null);
                    var transportProviders = providersVM.Where(pr => pr.ProviderType == "Transporte").ToList();
                    incomeHeaderVM.TransportProviders = new SelectList(transportProviders, nameof(ProviderViewModel.Id), nameof(ProviderViewModel.getBussinessNameOrName), null, null);
                }

                var getAllStations = await _stationViewManager.LoadAllWithFunctionalArea();
                if (getAllStations.Succeeded)
                {
                    var stationsVM = _mapper.Map<List<StationViewModel>>(getAllStations.Data);
                    incomeHeaderVM.ExternalProcessStations = new SelectList(stationsVM, nameof(StationViewModel.Id), nameof(StationViewModel.AbreviationDescription), null, null);
                }

                incomeHeaderVM.IncomeDate = DateTime.Now.Date.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute);

                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", incomeHeaderVM) });
            }
            // To update Income
            else
            {
                var getIncomeToUpdate = await _incomeHeaderViewManager.FindBySpecification(new IncomeHeaderWithAllSpecifications(id));
                if (getIncomeToUpdate.Succeeded)
                {
                    var incomeHeaderVM = _mapper.Map<List<IncomeHeaderViewModel>>(getIncomeToUpdate.Data).FirstOrDefault();

                    var getAllProviders = await _providerViewManager.LoadAll();
                    if (getAllProviders.Succeeded)
                    {
                        var providersVM = _mapper.Map<List<ProviderViewModel>>(getAllProviders.Data);
                        var productAndServiceProviders = providersVM.Where(pr => pr.ProviderType == "Productos" || pr.ProviderType == "Servicios").ToList();
                        incomeHeaderVM.Providers = new SelectList(productAndServiceProviders, nameof(ProviderViewModel.Id), nameof(ProviderViewModel.getBussinessNameOrName), null, null);
                        var transportProviders = providersVM.Where(pr => pr.ProviderType == "Transporte").ToList();
                        incomeHeaderVM.TransportProviders = new SelectList(transportProviders, nameof(ProviderViewModel.Id), nameof(ProviderViewModel.getBussinessNameOrName), null, null);
                    }

                    var getAllStations = await _stationViewManager.LoadAllWithFunctionalArea();
                    if (getAllStations.Succeeded)
                    {
                        var stationsVM = _mapper.Map<List<StationViewModel>>(getAllStations.Data);
                        incomeHeaderVM.ExternalProcessStations = new SelectList(stationsVM, nameof(StationViewModel.Id), nameof(StationViewModel.AbreviationDescription), null, null);
                    }

                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", incomeHeaderVM) });

                }
            }
            return null;
        }

        public async Task<JsonResult> LoadProductsSelect2(string search)
        {
            var productsResponse = await _productViewManager.FindBySpecification(new ProductWithAllSpecification(search));
            if (productsResponse.Succeeded)
            {
                List<Select2ViewModel> mapSelect2Data = new List<Select2ViewModel>();
                List<ProductViewModel> productsViewModel = new List<ProductViewModel>();
                productsViewModel = _mapper.Map<List<ProductViewModel>>(productsResponse.Data.ToList());

                mapSelect2Data.Add(new Select2ViewModel() { Id = -1, Text = _localizer["Select product"] });
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

        public async Task<JsonResult> loadUnits_select2()
        {
            try
            {
                IEnumerable<UnitMeasureDTO> unitMeasureDTOs = new List<UnitMeasureDTO>();

                var unitsResponse = await _unitMeasureViewManager.FindBySpecification(new UnitMeasureIsActiveSpecification());

                if (unitsResponse.Succeeded)
                {

                    unitMeasureDTOs = unitsResponse.Data.OrderBy(um => um.Description);

                    if (unitMeasureDTOs.Count() > 0)
                    {
                        var units = new SelectList((from u in unitMeasureDTOs.ToList() select new { Id = u.Id, Description = u.Description }), "Id", "Description", null);
                        return new JsonResult(new { isValid = true, values = units });
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

        public async Task<JsonResult> loadIncomeStates_select2()
        {
            try
            {
                var incomeStatesResponse = await _incomeStateViewManager.LoadAll();

                if (incomeStatesResponse.Succeeded)
                {

                    if (incomeStatesResponse.Data.Count() > 0)
                    {
                        var incomeStates = new SelectList((from ist in incomeStatesResponse.Data.ToList() select new { Id = ist.Id, Description = ist.Description }), "Id", "Description", null);
                        return new JsonResult(new { isValid = true, values = incomeStates });
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

        public async Task<JsonResult> getConfigurationByFatherProductId(int fatherProductId)
        {
            try
            {
                if (fatherProductId != -1)
                {
                    var structures = await _structureViewManager.FindBySpecification(new StructureByProductIdSpecification(fatherProductId));
                    if (structures.Succeeded)
                    {
                        if (structures.Data.Count > 0)
                        {
                            // Traigo todo excepto las que son configuraciones base
                            var selectList = new SelectList((from s in structures.Data.Where(st => st.IsBase == false).ToList() select new { Id = s.Id, Description = s.Description }), "Id", "Description", null);
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

        [HttpPost]
        public async Task<JsonResult> getFatherProductAndConfigByProductNumber(string productNumber)
        {
            try
            {
                if (productNumber != null)
                {
                    if (productNumber != "")
                    {
                        var getODByProductNumber = await _orderDetailViewManager.FindBySpecification(new OrderDetailByProductNumberSpecification(productNumber.Trim()));
                        if (getODByProductNumber.Succeeded)
                        {
                            var fatherProductSelectList = new SelectList((from od in getODByProductNumber.Data.Where(od => od.Product != null).ToList() select new { Id = od.Product.Id, Description = od.Product.Code + " - " + od.Product.Description }), "Id", "Description", null);
                            var structureSelectList = new SelectList((from od in getODByProductNumber.Data.Where(od => od.Structure != null && od.Structure.IsBase == false).ToList() select new { Id = od.Structure.Id, Description = od.Structure.Description }), "Id", "Description", null);

                            return new JsonResult(new { isValid = true, fatherProducts = fatherProductSelectList, structures = structureSelectList });
                        }
                        else
                        {
                            return new JsonResult(new { isValid = false, fatherProducts = "", structures = "" });
                        }
                    }
                    else
                    {
                        return new JsonResult(new { isValid = false, fatherProducts = "", structures = "" });
                    }
                }
                else
                {
                    return new JsonResult(new { isValid = false, fatherProducts = "", structures = "" });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return new JsonResult(new { isValid = false, fatherProducts = "", structures = "" });
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(int id, IncomeHeaderViewModel IncomeHeaderViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    if (IncomeHeaderViewModel.ProviderId == -1)
                    {
                        _notify.Warning(_localizer["You must select a provider."]);
                        var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", IncomeHeaderViewModel);
                        return new JsonResult(new { isValid = false, html = html });
                    }

                    if (IncomeHeaderViewModel.OwnTransport)
                    {
                        IncomeHeaderViewModel.TransportProviderId = IncomeHeaderViewModel.ProviderId;
                    }

                    if (IncomeHeaderViewModel.TransportProviderId == -1)
                    {
                        _notify.Warning(_localizer["You must select a transport."]);
                        var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", IncomeHeaderViewModel);
                        return new JsonResult(new { isValid = false, html = html });
                    }

                    if (IncomeHeaderViewModel.ExternalProcessStationId == -1)
                    {
                        IncomeHeaderViewModel.ExternalProcessStationId = null;
                    }

                    if (id == 0)
                    {
                        // Create
                        var incomeDetails = IncomeHeaderViewModel.IncomeDetails.Where(id => id != null).ToList();
                        if (incomeDetails.Count() > 0)
                        {
                            var incomeDetailsSetNullReferences = new List<IncomeDetailViewModel>();
                            foreach (var incomeDetail in incomeDetails)
                            {
                                var incomeDetailVM = new IncomeDetailViewModel();
                                incomeDetailVM = incomeDetail;
                                if (incomeDetail.FatherProductId == -1)
                                {
                                    incomeDetailVM.FatherProductId = null;
                                }
                                if (incomeDetail.FatherStructureId == -1)
                                {
                                    incomeDetailVM.FatherStructureId = null;
                                }

                                incomeDetailsSetNullReferences.Add(incomeDetailVM);
                            }

                            // Seteo los detalles agregados al Header
                            IncomeHeaderViewModel.IncomeDetails = incomeDetailsSetNullReferences;

                            // Chequeo si existe algun detalle con cantidad 0.
                            var detailsWithQuantityNull = incomeDetails.Where(id => id.Quantity == 0).ToList();
                            if (detailsWithQuantityNull.Count > 0)
                            {
                                _notify.Warning(_localizer["The quantity cannot be zero."]);
                                var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", IncomeHeaderViewModel);
                                return new JsonResult(new { isValid = false, html = html });
                            }

                            // Set "Reception" and "NextStation"
                            IncomeHeaderViewModel.IncomeDetails = await setReceptionAndNextStation(IncomeHeaderViewModel);

                            var IncomeHeaderDTO = _mapper.Map<IncomeHeaderDTO>(IncomeHeaderViewModel);
                            var result = await _incomeHeaderViewManager.CreateAsync(IncomeHeaderDTO);
                            if (result.Succeeded)
                            {
                                id = result.Data;
                                _notify.Success(_localizer["Income created."]);
                            }
                            else
                            {
                                _notify.Error(result.Message);
                            }
                        }
                        else
                        {
                            _notify.Warning(_localizer["You must add details."]);
                            var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", IncomeHeaderViewModel);
                            return new JsonResult(new { isValid = false, html = html });
                        }
                    }
                    else
                    {
                        // Update
                        var IncomeHeaderDTO = _mapper.Map<IncomeHeaderDTO>(IncomeHeaderViewModel);

                        var result = await _incomeHeaderViewManager.UpdateAsync(IncomeHeaderDTO);
                        if (result.Succeeded)
                        {
                            _notify.Information(_localizer["Income updated."]);
                        }
                    }

                    return new JsonResult(new { isValid = true });
                }
                else
                {
                    var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", IncomeHeaderViewModel);
                    return new JsonResult(new { isValid = false, html = html });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", IncomeHeaderViewModel);
                return new JsonResult(new { isValid = false, html = html });
            }
        }

        public async Task<ICollection<IncomeDetailViewModel>> setReceptionAndNextStation(IncomeHeaderViewModel incomeHeaderViewModel)
        {
            foreach (var incomeDetailVM in incomeHeaderViewModel.IncomeDetails)
            {
                var getRPSByProductId = await _productViewManager.FindBySpecification(new ProductWithRelProductStationsSpecification(incomeDetailVM.IncomeProductId));
                if (getRPSByProductId.Succeeded)
                {
                    var relProductStationVM = _mapper.Map<List<RelProductStationViewModel>>(getRPSByProductId.Data.First().RelProductStations);
                    for (int i = 0; i < relProductStationVM.Count() - 1; i++)
                    {
                        if (incomeHeaderViewModel.ExternalProcessStationId.HasValue && incomeHeaderViewModel.ExternalProcessStationId != null)
                        {
                            var getStationById = await _stationViewManager.GetByIdAsync(incomeHeaderViewModel.ExternalProcessStationId.Value);
                            if (getStationById.Succeeded)
                            {
                                if (getStationById.Data.WorkOrderDisplayType == "Envios")
                                {
                                    if (incomeHeaderViewModel.ExternalProcessStationId == relProductStationVM[i].StationId)
                                    {
                                        if ((i + 1) <= (relProductStationVM.Count() - 1))
                                        {
                                            incomeDetailVM.Reception = relProductStationVM[i + 1].Station.Abbreviation;
                                        }

                                        if ((i + 2) <= (relProductStationVM.Count() - 1))
                                        {
                                            incomeDetailVM.NextStation = relProductStationVM[i + 2].Station.Abbreviation;
                                        }
                                        else
                                        {
                                            // Busco la HDR del padre y se la asigno al NextStation
                                            var getWOIsByProductNumber = await _workOrderItemViewManager.FindBySpecification(new WOIByProductNumberSpecification(incomeDetailVM.ProductNumber.Trim()));
                                            if (getWOIsByProductNumber.Succeeded)
                                            {
                                                var workOrderItems = _mapper.Map<List<WorkOrderItem>>(getWOIsByProductNumber.Data);
                                                if (workOrderItems.Count > 0)
                                                {
                                                    foreach (var woi in workOrderItems)
                                                    {
                                                        // Si tiene padre
                                                        if (woi.ProductId == incomeDetailVM.IncomeProductId && woi.WorkOrderItemOf != null)
                                                        {
                                                            var responseProductStationsFather = await _productViewManager.FindBySpecification(new ProductWithRelProductStationsSpecification((int)woi.WorkOrderItemOf.ProductId));
                                                            var fatherProduct = _mapper.Map<ProductViewModel>(responseProductStationsFather.Data.FirstOrDefault());
                                                            // Si el padre tiene actividades, asignamos la 1er estacion del padre como NextStation
                                                            if (fatherProduct.RelProductStations.Count > 0)
                                                            {
                                                                if (incomeDetailVM.NextStation != null)
                                                                {
                                                                    if (!incomeDetailVM.NextStation.Contains(fatherProduct.RelProductStations[0].Station.Abbreviation))
                                                                    {
                                                                        incomeDetailVM.NextStation += fatherProduct.RelProductStations[0].Station.Abbreviation;
                                                                        incomeDetailVM.NextStation += ", ";
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    incomeDetailVM.NextStation += fatherProduct.RelProductStations[0].Station.Abbreviation;
                                                                    incomeDetailVM.NextStation += ", ";
                                                                }

                                                                //incomeDetailVM.NextStation = fatherProduct.RelProductStations[0].Station.Abbreviation;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    // Cuando viene de planilla donde se enviaron a producir afuera.
                                    if (incomeHeaderViewModel.ExternalProcessStationId == relProductStationVM[i].StationId)
                                    {
                                        if ((i + 1) <= (relProductStationVM.Count() - 1))
                                        {
                                            if (relProductStationVM[i + 1].Station.Abbreviation != "R01" && relProductStationVM[i + 1].Station.Abbreviation != "R02")
                                            {
                                                incomeDetailVM.Reception = "R01";
                                                incomeDetailVM.NextStation = relProductStationVM[i + 1].Station.Abbreviation;
                                            }
                                        }
                                        else
                                        {
                                            // Busco la HDR del padre y se la asigno al NextStation
                                            var getWOIsByProductNumber = await _workOrderItemViewManager.FindBySpecification(new WOIByProductNumberSpecification(incomeDetailVM.ProductNumber.Trim()));
                                            if (getWOIsByProductNumber.Succeeded)
                                            {
                                                var workOrderItems = _mapper.Map<List<WorkOrderItem>>(getWOIsByProductNumber.Data);
                                                if (workOrderItems.Count > 0)
                                                {
                                                    foreach (var woi in workOrderItems)
                                                    {
                                                        // Si tiene padre
                                                        if (woi.ProductId == incomeDetailVM.IncomeProductId && woi.WorkOrderItemOf != null)
                                                        {
                                                            var responseProductStationsFather = await _productViewManager.FindBySpecification(new ProductWithRelProductStationsSpecification((int)woi.WorkOrderItemOf.ProductId));
                                                            var fatherProduct = _mapper.Map<ProductViewModel>(responseProductStationsFather.Data.FirstOrDefault());
                                                            // Si el padre tiene actividades, asignamos la 1er estacion del padre como NextStation
                                                            if (fatherProduct.RelProductStations.Count > 0)
                                                            {
                                                                if (incomeDetailVM.NextStation != null)
                                                                {
                                                                    if (!incomeDetailVM.NextStation.Contains(fatherProduct.RelProductStations[0].Station.Abbreviation))
                                                                    {
                                                                        incomeDetailVM.NextStation += fatherProduct.RelProductStations[0].Station.Abbreviation;
                                                                        incomeDetailVM.NextStation += ", ";
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    incomeDetailVM.NextStation += fatherProduct.RelProductStations[0].Station.Abbreviation;
                                                                    incomeDetailVM.NextStation += ", ";
                                                                }

                                                                //incomeDetailVM.NextStation = fatherProduct.RelProductStations[0].Station.Abbreviation;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            incomeDetailVM.Reception = relProductStationVM[i].Station.Abbreviation;

                            if ((i + 1) <= (relProductStationVM.Count() - 1))
                            {
                                incomeDetailVM.NextStation = relProductStationVM[i + 1].Station.Abbreviation;
                            }
                        }
                    }
                }
            }

            return incomeHeaderViewModel.IncomeDetails;
        }

        public async Task<IActionResult> ExportToExcel(string columnFilter_2, string columnFilter_3, string columnFilter_4, string columnFilter_5, string columnFilter_6,
             string columnFilter_7, string columnFilter_8, string columnFilter_9, string columnFilter_10, string columnFilter_11, string columnFilter_12, string columnFilter_13, string columnFilter_14,
             string columnFilter_15, string columnFilter_16, string columnFilter_17, string columnFilter_18, string columnFilter_19, string columnFilter_20, string columnFilter_21, string columnFilter_22, string columnFilter_23, string columnFilter_24,
             int colIndexOrder, string colOrderDirection, string dateFromFilterValue, string dateToFilterValue)
        {
            try
            {

                var loadIncomes = await _incomeDetailViewManager.FindBySpecification(new IncomeDetailsWithAllSpecifications());
                if (loadIncomes.Succeeded)
                {
                    var incomesVM = _mapper.Map<IEnumerable<IncomeDetailViewModel>>(loadIncomes.Data);

                    var usersList = _identityContext.Users.ToList();

                    foreach (var iVM in incomesVM)
                    {
                        foreach (var u in usersList)
                        {
                            if (u.Id == iVM.CreatedBy)
                            {
                                iVM.CreatedBy = u.FirstName.ToString() + ", " + u.LastName.ToString();
                            }
                        }
                    }

                    // From date -> To date
                    incomesVM = incomesVM.Where(id => id.IncomeHeader != null && (id.IncomeHeader.IncomeDate >= Convert.ToDateTime(dateFromFilterValue) && id.IncomeHeader.IncomeDate <= Convert.ToDateTime(dateToFilterValue)));

                    if (!string.IsNullOrEmpty(columnFilter_2))
                    {
                        incomesVM = incomesVM.Where(id => id.IncomeDate.HasValue ? id.IncomeDate.Value.ToString("dd/MM/yyyy").Contains(columnFilter_2) : id.IncomeHeader.IncomeDate.ToString("dd/MM/yyyy").Contains(columnFilter_2)).ToList();
                    }
                    if (!string.IsNullOrEmpty(columnFilter_3))
                    {
                        incomesVM = incomesVM.Where(id => id.IncomeProduct != null && id.IncomeProduct.Code.ToString().ToLower().Contains(columnFilter_3.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(columnFilter_4))
                    {
                        incomesVM = incomesVM.Where(id => id.IncomeProduct != null && id.IncomeProduct.Description.ToString().ToLower().Contains(columnFilter_4.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(columnFilter_5))
                    {
                        incomesVM = incomesVM.Where(id => id.Quantity.ToString().ToLower().Contains(columnFilter_5.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(columnFilter_6))
                    {
                        incomesVM = incomesVM.Where(id => id.Unit != null && id.Unit.Description.ToString().ToLower().Contains(columnFilter_6.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(columnFilter_7))
                    {
                        incomesVM = incomesVM.Where(id => id.IncomeHeader != null && id.IncomeHeader.Provider.BusinessName.ToString().ToLower().Contains(columnFilter_7.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(columnFilter_8))
                    {
                        incomesVM = incomesVM.Where(id => id.IncomeHeader != null && id.IncomeHeader.TransportProvider.BusinessName.ToString().ToLower().Contains(columnFilter_8.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(columnFilter_9))
                    {
                        incomesVM = incomesVM.Where(id => id.IncomeHeader.DeliveryNoteNumber != null && id.IncomeHeader.DeliveryNoteNumber.ToString().ToLower().Contains(columnFilter_9.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(columnFilter_10))
                    {
                        incomesVM = incomesVM.Where(id => id.BatchNumber != null && id.BatchNumber.ToString().ToLower().Contains(columnFilter_10.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(columnFilter_11))
                    {
                        incomesVM = incomesVM.Where(id => id.Reception.ToString().ToLower().Contains(columnFilter_11.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(columnFilter_12))
                    {
                        incomesVM = incomesVM.Where(id => id.NextStation.ToString().ToLower().Contains(columnFilter_12.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(columnFilter_13))
                    {
                        incomesVM = incomesVM.Where(id => id.IncomeState.Description.ToString().ToLower().Contains(columnFilter_13.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(columnFilter_14))
                    {
                        incomesVM = incomesVM.Where(id => id.OCNumber != null && id.OCNumber.ToString().ToLower().Contains(columnFilter_14.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(columnFilter_15))
                    {
                        incomesVM = incomesVM.Where(id => id.IncomeHeader != null && id.IncomeHeader.InvoiceNumber != null && id.IncomeHeader.InvoiceNumber.ToString().ToLower().Contains(columnFilter_15.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(columnFilter_16))
                    {
                        incomesVM = incomesVM.Where(id => id.OTNumber != null && id.OTNumber.ToString().ToLower().Contains(columnFilter_16.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(columnFilter_17))
                    {
                        incomesVM = incomesVM.Where(id => id.ProductNumber != null && id.ProductNumber.ToString().ToLower().Contains(columnFilter_17.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(columnFilter_18))
                    {
                        incomesVM = incomesVM.Where(id => id.FatherProduct != null && id.FatherProduct.CodeAndDescription.ToString().ToLower().Contains(columnFilter_18.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(columnFilter_19))
                    {
                        incomesVM = incomesVM.Where(id => id.FatherStructure != null && id.FatherStructure.Description.ToString().ToLower().Contains(columnFilter_19.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(columnFilter_20))
                    {
                        incomesVM = incomesVM.Where(id => id.IncomeHeader.ExternalProcessStation != null && id.IncomeHeader.ExternalProcessStation.AbreviationDescription.ToString().ToLower().Contains(columnFilter_20.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(columnFilter_21))
                    {
                        incomesVM = incomesVM.Where(id => id.IncomeHeader.IncomeDate.ToString().ToLower().Contains(columnFilter_21.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(columnFilter_22))
                    {
                        incomesVM = incomesVM.Where(id => id.IncomeDate.HasValue ? Math.Abs((id.IncomeHeader.IncomeDate.Date - id.IncomeDate.Value.Date).Days).ToString().ToLower().Contains(columnFilter_22.ToLower()) : "0 días".ToString().ToLower().Contains(columnFilter_22.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(columnFilter_23))
                    {
                        incomesVM = incomesVM.Where(id => id.Weight.ToString().ToLower().Contains(columnFilter_23.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(columnFilter_24))
                    {
                        incomesVM = incomesVM.Where(id => id.CreatedBy.ToString().ToLower().Contains(columnFilter_24.ToLower())).ToList();
                    }

                    if (colIndexOrder != 0 && colOrderDirection != "")
                    {
                        var sortColumnIndex = colIndexOrder;
                        var sortDirection = colOrderDirection;

                        if (sortColumnIndex == 2)
                        {
                            incomesVM = sortDirection == "asc" ? incomesVM.OrderBy(id => id.IncomeDate.HasValue ? id.IncomeDate.Value.Date : id.IncomeHeader.IncomeDate.Date) : incomesVM.OrderByDescending(id => id.IncomeDate.HasValue ? id.IncomeDate.Value.Date : id.IncomeHeader.IncomeDate.Date);
                        }
                        else if (sortColumnIndex == 3)
                        {
                            incomesVM = sortDirection == "asc" ? incomesVM.OrderBy(id => id.IncomeProduct.Code) : incomesVM.OrderByDescending(id => id.IncomeProduct.Code);
                        }
                        else if (sortColumnIndex == 4)
                        {
                            incomesVM = sortDirection == "asc" ? incomesVM.OrderBy(id => id.IncomeProduct.Description) : incomesVM.OrderByDescending(id => id.IncomeProduct.Description);
                        }
                        else if (sortColumnIndex == 5)
                        {
                            incomesVM = sortDirection == "asc" ? incomesVM.OrderBy(id => id.Quantity) : incomesVM.OrderByDescending(id => id.Quantity);
                        }
                        else if (sortColumnIndex == 6)
                        {
                            incomesVM = sortDirection == "asc" ? incomesVM.OrderBy(id => id.Unit.Description) : incomesVM.OrderByDescending(id => id.Unit.Description);
                        }
                        else if (sortColumnIndex == 7)
                        {
                            incomesVM = sortDirection == "asc" ? incomesVM.OrderBy(id => id.IncomeHeader.Provider.BusinessName) : incomesVM.OrderByDescending(id => id.IncomeHeader.Provider.BusinessName);
                        }
                        else if (sortColumnIndex == 8)
                        {
                            incomesVM = sortDirection == "asc" ? incomesVM.OrderBy(id => id.IncomeHeader.TransportProvider.BusinessName) : incomesVM.OrderByDescending(id => id.IncomeHeader.TransportProvider.BusinessName);
                        }
                        else if (sortColumnIndex == 9)
                        {
                            incomesVM = sortDirection == "asc" ? incomesVM.OrderBy(id => id.IncomeHeader.DeliveryNoteNumber) : incomesVM.OrderByDescending(id => id.IncomeHeader.DeliveryNoteNumber);
                        }
                        else if (sortColumnIndex == 10)
                        {
                            incomesVM = sortDirection == "asc" ? incomesVM.OrderBy(id => id.BatchNumber) : incomesVM.OrderByDescending(id => id.BatchNumber);
                        }
                        else if (sortColumnIndex == 11)
                        {
                            incomesVM = sortDirection == "asc" ? incomesVM.OrderBy(id => id.Reception) : incomesVM.OrderByDescending(id => id.Reception);
                        }
                        else if (sortColumnIndex == 12)
                        {
                            incomesVM = sortDirection == "asc" ? incomesVM.OrderBy(id => id.NextStation) : incomesVM.OrderByDescending(id => id.NextStation);
                        }
                        else if (sortColumnIndex == 13)
                        {
                            incomesVM = sortDirection == "asc" ? incomesVM.OrderBy(id => id.IncomeState.Description) : incomesVM.OrderByDescending(id => id.IncomeState.Description);
                        }
                        else if (sortColumnIndex == 14)
                        {
                            incomesVM = sortDirection == "asc" ? incomesVM.OrderBy(id => id.OCNumber) : incomesVM.OrderByDescending(id => id.OCNumber);
                        }
                        else if (sortColumnIndex == 15)
                        {
                            incomesVM = sortDirection == "asc" ? incomesVM.OrderBy(id => id.IncomeHeader.InvoiceNumber) : incomesVM.OrderByDescending(id => id.IncomeHeader.InvoiceNumber);
                        }
                        else if (sortColumnIndex == 16)
                        {
                            incomesVM = sortDirection == "asc" ? incomesVM.OrderBy(id => id.OTNumber) : incomesVM.OrderByDescending(id => id.OTNumber);
                        }
                        else if (sortColumnIndex == 17)
                        {
                            incomesVM = sortDirection == "asc" ? incomesVM.OrderBy(id => id.ProductNumber) : incomesVM.OrderByDescending(id => id.ProductNumber);
                        }
                        else if (sortColumnIndex == 18)
                        {
                            incomesVM = sortDirection == "asc" ? incomesVM.OrderBy(id => id.FatherProduct.CodeAndDescription) : incomesVM.OrderByDescending(id => id.FatherProduct.CodeAndDescription);
                        }
                        else if (sortColumnIndex == 19)
                        {
                            incomesVM = sortDirection == "asc" ? incomesVM.OrderBy(id => id.FatherStructure != null ? id.FatherStructure.Description : null) : incomesVM.OrderByDescending(id => id.FatherStructure != null ? id.FatherStructure.Description : null);
                        }
                        else if (sortColumnIndex == 20)
                        {
                            incomesVM = sortDirection == "asc" ? incomesVM.OrderBy(id => id.IncomeHeader.ExternalProcessStation.AbreviationDescription) : incomesVM.OrderByDescending(id => id.IncomeHeader.ExternalProcessStation.AbreviationDescription);
                        }
                        else if (sortColumnIndex == 21)
                        {
                            incomesVM = sortDirection == "asc" ? incomesVM.OrderBy(id => id.IncomeHeader.IncomeDate.Date) : incomesVM.OrderByDescending(id => id.IncomeHeader.IncomeDate.Date);
                        }
                        else if (sortColumnIndex == 22)
                        {
                            incomesVM = sortDirection == "asc" ? incomesVM.OrderBy(id => id.IncomeDate.HasValue ? Math.Abs((id.IncomeHeader.IncomeDate.Date - id.IncomeDate.Value.Date).Days).ToString() : "0 días") : incomesVM.OrderByDescending(id => id.IncomeDate.HasValue ? Math.Abs((id.IncomeHeader.IncomeDate.Date - id.IncomeDate.Value.Date).Days).ToString() : "0 días");
                        }
                        else if (sortColumnIndex == 23)
                        {
                            incomesVM = sortDirection == "asc" ? incomesVM.OrderBy(id => id.Weight) : incomesVM.OrderByDescending(id => id.Weight);
                        }
                        else if (sortColumnIndex == 24)
                        {
                            incomesVM = sortDirection == "asc" ? incomesVM.OrderBy(id => id.CreatedBy) : incomesVM.OrderByDescending(id => id.CreatedBy);
                        }
                    }

                    return ExportViewModelToExcel(incomesVM);

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

        public IActionResult ExportViewModelToExcel(IEnumerable<IncomeDetailViewModel> viewModel)
        {
            try
            {
                var nameForExcelFile = "Ingresos_";

                var stream = new MemoryStream();
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var xlPackage = new ExcelPackage(stream))
                {
                    var workSheet = xlPackage.Workbook.Worksheets.Add("Ingresos");

                    workSheet.Cells["A1"].Value = _localizer["Income date"];
                    workSheet.Cells["B1"].Value = _localizer["Code"];
                    workSheet.Cells["C1"].Value = _localizer["Description"];
                    workSheet.Cells["D1"].Value = _localizer["Quantity"];
                    workSheet.Cells["E1"].Value = _localizer["Unit"];
                    workSheet.Cells["F1"].Value = _localizer["Provider"];
                    workSheet.Cells["G1"].Value = _localizer["Transport"];
                    workSheet.Cells["H1"].Value = "Nº Remito";
                    workSheet.Cells["I1"].Value = _localizer["Batch"];
                    workSheet.Cells["J1"].Value = _localizer["Reception"];
                    workSheet.Cells["K1"].Value = _localizer["Next station"];
                    workSheet.Cells["L1"].Value = _localizer["State"];
                    workSheet.Cells["M1"].Value = "Nº OC";
                    workSheet.Cells["N1"].Value = "Nº " + _localizer["Invoice"];
                    workSheet.Cells["O1"].Value = "Nº OT";
                    workSheet.Cells["P1"].Value = "Nº " + _localizer["Product"];
                    workSheet.Cells["Q1"].Value = _localizer["Product"];
                    workSheet.Cells["R1"].Value = _localizer["Configuration"];
                    workSheet.Cells["S1"].Value = _localizer["External process"];
                    workSheet.Cells["T1"].Value = _localizer["Shipping date"];
                    workSheet.Cells["U1"].Value = _localizer["Total time"];
                    workSheet.Cells["V1"].Value = _localizer["Weight"];
                    workSheet.Cells["W1"].Value = _localizer["User"];

                    var row = 2;
                    foreach (var incomeDetailVM in viewModel)
                    {
                        if (incomeDetailVM.IncomeDate.HasValue)
                        {
                            workSheet.Cells[row, 1].Value = incomeDetailVM.IncomeDate.Value;
                            workSheet.Cells[row, 1].Style.Numberformat.Format = "dd-MM-yyyy HH:mm";
                        }
                        else
                        {
                            workSheet.Cells[row, 1].Value = incomeDetailVM.IncomeHeader.IncomeDate;
                            workSheet.Cells[row, 1].Style.Numberformat.Format = "dd-MM-yyyy HH:mm";
                        }

                        if (incomeDetailVM.IncomeProduct != null)
                        {
                            if (incomeDetailVM.IncomeProduct.Code != null)
                            {
                                workSheet.Cells[row, 2].Value = incomeDetailVM.IncomeProduct.Code.ToString();
                            }
                            else
                            {
                                workSheet.Cells[row, 2].Value = "";
                            }

                            if (incomeDetailVM.IncomeProduct.Description != null)
                            {
                                workSheet.Cells[row, 3].Value = incomeDetailVM.IncomeProduct.Description.ToString();
                            }
                            else
                            {
                                workSheet.Cells[row, 3].Value = "";
                            }
                        }
                        else
                        {
                            workSheet.Cells[row, 2].Value = "";
                            workSheet.Cells[row, 3].Value = "";
                        }

                        workSheet.Cells[row, 4].Value = incomeDetailVM.Quantity;
                        workSheet.Cells[row, 4].Style.Numberformat.Format = "0";

                        if (incomeDetailVM.Unit != null)
                        {
                            if (incomeDetailVM.Unit.Description != null)
                            {
                                workSheet.Cells[row, 5].Value = incomeDetailVM.Unit.Description.ToString();
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

                        if (incomeDetailVM.IncomeHeader.Provider != null)
                        {
                            if (incomeDetailVM.IncomeHeader.Provider.BusinessName != null)
                            {
                                workSheet.Cells[row, 6].Value = incomeDetailVM.IncomeHeader.Provider.BusinessName.ToString();
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

                        if (incomeDetailVM.IncomeHeader.TransportProvider != null)
                        {
                            if (incomeDetailVM.IncomeHeader.TransportProvider.BusinessName != null)
                            {
                                workSheet.Cells[row, 7].Value = incomeDetailVM.IncomeHeader.TransportProvider.BusinessName.ToString();
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

                        if (incomeDetailVM.IncomeHeader.DeliveryNoteNumber != null)
                        {
                            workSheet.Cells[row, 8].Value = incomeDetailVM.IncomeHeader.DeliveryNoteNumber.ToString();
                        }
                        else
                        {
                            workSheet.Cells[row, 8].Value = "";
                        }

                        if (incomeDetailVM.BatchNumber != null)
                        {
                            workSheet.Cells[row, 9].Value = incomeDetailVM.BatchNumber;
                            workSheet.Cells[row, 9].Style.Numberformat.Format = "0";
                        }
                        else
                        {
                            workSheet.Cells[row, 9].Value = "";
                        }

                        if (incomeDetailVM.Reception != null)
                        {
                            workSheet.Cells[row, 10].Value = incomeDetailVM.Reception.ToString();
                        }
                        else
                        {
                            workSheet.Cells[row, 10].Value = "";
                        }

                        if (incomeDetailVM.NextStation != null)
                        {
                            workSheet.Cells[row, 11].Value = incomeDetailVM.NextStation.ToString();
                        }
                        else
                        {
                            workSheet.Cells[row, 11].Value = "";
                        }

                        workSheet.Cells[row, 12].Value = incomeDetailVM.IncomeState.Description.ToString();

                        if (incomeDetailVM.OCNumber != null)
                        {
                            workSheet.Cells[row, 13].Value = incomeDetailVM.OCNumber.ToString();
                        }
                        else
                        {
                            workSheet.Cells[row, 13].Value = incomeDetailVM.OCNumber.ToString();
                        }

                        if (incomeDetailVM.IncomeHeader.InvoiceNumber != null)
                        {
                            workSheet.Cells[row, 14].Value = incomeDetailVM.IncomeHeader.InvoiceNumber.ToString();
                        }
                        else
                        {
                            workSheet.Cells[row, 14].Value = "";
                        }

                        if (incomeDetailVM.OTNumber != null)
                        {
                            workSheet.Cells[row, 15].Value = incomeDetailVM.OTNumber.ToString();
                        }
                        else
                        {
                            workSheet.Cells[row, 15].Value = "";
                        }

                        if (incomeDetailVM.ProductNumber != null)
                        {
                            workSheet.Cells[row, 16].Value = incomeDetailVM.ProductNumber.ToString();
                        }
                        else
                        {
                            workSheet.Cells[row, 16].Value = "";
                        }

                        if (incomeDetailVM.FatherProduct != null)
                        {
                            if (incomeDetailVM.FatherProduct.CodeAndDescription != null)
                            {
                                workSheet.Cells[row, 17].Value = incomeDetailVM.FatherProduct.CodeAndDescription.ToString();
                            }
                            else
                            {
                                workSheet.Cells[row, 17].Value = "";
                            }
                        }
                        else
                        {
                            workSheet.Cells[row, 17].Value = "";
                        }

                        if (incomeDetailVM.FatherStructure != null)
                        {
                            workSheet.Cells[row, 18].Value = incomeDetailVM.FatherStructure.Description.ToString();
                        }
                        else
                        {
                            workSheet.Cells[row, 18].Value = "";
                        }

                        if (incomeDetailVM.IncomeHeader.ExternalProcessStation != null)
                        {
                            if (incomeDetailVM.IncomeHeader.ExternalProcessStation.AbreviationDescription != null)
                            {
                                workSheet.Cells[row, 19].Value = incomeDetailVM.IncomeHeader.ExternalProcessStation.AbreviationDescription.ToString();
                            }
                            else
                            {
                                workSheet.Cells[row, 19].Value = "";
                            }
                        }
                        else
                        {
                            workSheet.Cells[row, 19].Value = "";
                        }

                        workSheet.Cells[row, 20].Value = incomeDetailVM.IncomeHeader.IncomeDate;
                        workSheet.Cells[row, 20].Style.Numberformat.Format = "dd-MM-yyyy HH:mm";

                        if (incomeDetailVM.IncomeDate.HasValue)
                        {
                            if (incomeDetailVM.IncomeHeader.IncomeDate > incomeDetailVM.IncomeDate.Value)
                            {
                                workSheet.Cells[row, 21].Value = (incomeDetailVM.IncomeHeader.IncomeDate.Date - incomeDetailVM.IncomeDate.Value.Date).Days;
                            }
                            else
                            {
                                workSheet.Cells[row, 21].Value = (incomeDetailVM.IncomeDate.Value.Date - incomeDetailVM.IncomeHeader.IncomeDate.Date).Days;
                            }
                        }
                        else
                        {
                            workSheet.Cells[row, 21].Value = 0;
                        }

                        workSheet.Cells[row, 22].Value = incomeDetailVM.Weight;
                        workSheet.Cells[row, 22].Style.Numberformat.Format = "0.00";

                        if (incomeDetailVM.CreatedBy != null)
                        {
                            workSheet.Cells[row, 23].Value = incomeDetailVM.CreatedBy.ToString();
                        }
                        else
                        {
                            workSheet.Cells[row, 23].Value = "";
                        }

                        row++;
                    }

                    workSheet.Cells["A1:W1"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                    workSheet.Cells["W1"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

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

        public async Task<JsonResult> stopIncomesHeader(int[] incomeHeaderIds)
        {
            try
            {
                if (incomeHeaderIds.Length > 0)
                {
                    var updateHeaderHasSuccess = true;
                    var updateDetailHasSuccess = true;
                    foreach (var incomeHeaderId in incomeHeaderIds)
                    {
                        var getIncomeHeaderById = _incomeHeaderViewManager.FindBySpecification(new IncomeHeaderWithAllSpecifications(incomeHeaderId));
                        if (getIncomeHeaderById.Result.Succeeded)
                        {
                            var incomeHeaderVM = _mapper.Map<IncomeHeaderViewModel>(getIncomeHeaderById.Result.Data.FirstOrDefault());
                            // Se setea la fecha del ingreso al momento que se le pone stop.
                            incomeHeaderVM.IncomeDate = DateTime.Now;

                            foreach (var incomeDetailViewModel in incomeHeaderVM.IncomeDetails)
                            {
                                // Se setea el estado "Recibido" al momento que se le pone stop.
                                incomeDetailViewModel.IncomeStateId = 2;
                                var incomeDetailDTO = _mapper.Map<IncomeDetailDTO>(incomeDetailViewModel);
                                var updateDetailResult = await _incomeDetailViewManager.UpdateAsync(incomeDetailDTO);
                                if (!updateDetailResult.Succeeded && updateDetailResult.Data == 0)
                                {
                                    updateDetailHasSuccess = false;
                                }
                            }

                            // Fran: 3/4/2023 Se finalizan todas las actividades relacionadas al encabezado IncomeHeader y su detalle
                            // para poder calcular mejor el porcentaje de avance en las planillas de Envíos.
                            if (incomeHeaderVM.ExternalProcessStationId.HasValue && incomeHeaderVM.IncomeDetails.First().OTNumber.HasValue)
                            {
                                var getWAByStationAndWONumber = await _workActivityViewManager.FindBySpecification(new WorkActivitiesByWONumberSpecification(incomeHeaderVM.IncomeDetails.First().OTNumber.Value, incomeHeaderVM.ExternalProcessStationId.Value));
                                if (getWAByStationAndWONumber.Succeeded)
                                {
                                    if (getWAByStationAndWONumber.Data.Count > 0)
                                    {
                                        var workActivitiesVM = _mapper.Map<List<WorkActivityViewModel>>(getWAByStationAndWONumber.Data);
                                        foreach (var incomeDetailViewModel in incomeHeaderVM.IncomeDetails)
                                        {
                                            foreach (var waVM in workActivitiesVM)
                                            {
                                                if (waVM.WorkOrderItem.ProductId == incomeDetailViewModel.IncomeProductId)
                                                {
                                                    // Se crea la nueva acción stop
                                                    var workActionViewModel = new WorkActionViewModel();
                                                    workActionViewModel.WorkActivityId = waVM.Id;
                                                    workActionViewModel.ActionName = "stop";
                                                    workActionViewModel.StartDate = DateTime.Now;
                                                    workActionViewModel.EndingDate = DateTime.Now;
                                                    await _workActionViewManager.CreateAsync(_mapper.Map<WorkActionDTO>(workActionViewModel));
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            var incomeHeaderDTO = _mapper.Map<IncomeHeaderDTO>(incomeHeaderVM);
                            var updateResult = await _incomeHeaderViewManager.UpdateAsync(incomeHeaderDTO);
                            if (!updateResult.Succeeded && updateResult.Data == 0)
                            {
                                updateHeaderHasSuccess = false;
                            }
                        }
                    }
                    if (updateHeaderHasSuccess && updateDetailHasSuccess)
                    {
                        return new JsonResult(new { isValid = true });
                    }
                    else
                    {
                        return new JsonResult(new { isValid = false });
                    }
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

        public async Task<JsonResult> stopIncomes(int[] incomeDetailsIds)
        {
            try
            {
                if (incomeDetailsIds.Length > 0)
                {
                    var updateDetailHasSuccess = true;
                    foreach (var incomeDetailId in incomeDetailsIds)
                    {
                        var getIncomeDetailById = _incomeDetailViewManager.FindBySpecification(new IncomeDetailsWithAllSpecifications(incomeDetailId));
                        if (getIncomeDetailById.Result.Succeeded)
                        {
                            var incomeDetailDTO = _mapper.Map<IncomeDetailDTO>(getIncomeDetailById.Result.Data.FirstOrDefault());

                            // Actualizo el estado del producto faltante a "Finalizado"
                            if (incomeDetailDTO.MissingProductId.HasValue)
                            {
                                var getMPById = await _missingProductViewManager.FindWithAllSpecification(new MissingProductByIdSpecification(incomeDetailDTO.MissingProductId.Value));
                                if (getMPById.Succeeded && getMPById.Data != null)
                                {
                                    var mpDTO = getMPById.Data.FirstOrDefault();
                                    if (mpDTO.StateMissingProductId != 5)
                                    {
                                        mpDTO.StateMissingProductId = 5;
                                        await _missingProductViewManager.UpdateAsync(mpDTO);
                                    }
                                }
                            }

                            // Se setea el estado "Finalizado" al momento que se le pone stop.
                            incomeDetailDTO.IncomeStateId = 3;
                            incomeDetailDTO.IncomeDate = DateTime.Now;
                            var updateResult = await _incomeDetailViewManager.UpdateAsync(incomeDetailDTO);
                            if (!updateResult.Succeeded && updateResult.Data == 0)
                            {
                                updateDetailHasSuccess = false;
                            }

                            // Fran: 3/4/2023 Se finalizan todas las actividades relacionadas al encabezado IncomeHeader y su detalle
                            // para poder calcular mejor el porcentaje de avance en las planillas de Envíos.
                            if (incomeDetailDTO.IncomeHeader.ExternalProcessStationId.HasValue && incomeDetailDTO.OTNumber.HasValue)
                            {
                                var getWAByStationAndWONumber = await _workActivityViewManager.FindBySpecification(new WorkActivitiesByWONumberSpecification(incomeDetailDTO.OTNumber.Value, incomeDetailDTO.IncomeHeader.ExternalProcessStationId.Value));
                                if (getWAByStationAndWONumber.Succeeded)
                                {
                                    if (getWAByStationAndWONumber.Data.Count > 0)
                                    {
                                        var workActivitiesVM = _mapper.Map<List<WorkActivityViewModel>>(getWAByStationAndWONumber.Data);
                                        foreach (var waVM in workActivitiesVM)
                                        {
                                            if (waVM.WorkOrderItem.ProductId == incomeDetailDTO.IncomeProductId)
                                            {
                                                if (waVM.NextStation.Trim().Split("-")[0].Trim().StartsWith("R"))
                                                {
                                                    var getRPSByProductId = await _productViewManager.FindBySpecification(new ProductWithRelProductStationsSpecification(incomeDetailDTO.IncomeProductId));
                                                    if (getRPSByProductId.Succeeded)
                                                    {
                                                        var relProductStationVMs = _mapper.Map<List<RelProductStationViewModel>>(getRPSByProductId.Data.First().RelProductStations.ToList());
                                                        foreach (var rps in relProductStationVMs)
                                                        {
                                                            if (waVM.NextStation.Trim().Equals(rps.Station.AbreviationDescription.Trim()))
                                                            {
                                                                var getWAToFinish = await _workActivityViewManager.FindBySpecification(new WorkActivitiesByWorkOrderItemIdSpecification(waVM.WorkOrderItemId));
                                                                if (getWAToFinish.Succeeded)
                                                                {
                                                                    foreach (var wa in getWAToFinish.Data)
                                                                    {
                                                                        if (wa.ProductStation.Station.Abbreviation.Trim().Equals(waVM.NextStation.Trim().Split("-")[0].Trim()))
                                                                        {
                                                                            // Se crea la nueva acción stop para el puesto de recepción
                                                                            var workActionVM = new WorkActionViewModel();
                                                                            workActionVM.WorkActivityId = wa.Id;
                                                                            workActionVM.ActionName = "stop";
                                                                            workActionVM.StartDate = DateTime.Now;
                                                                            workActionVM.EndingDate = DateTime.Now;
                                                                            await _workActionViewManager.CreateAsync(_mapper.Map<WorkActionDTO>(workActionVM));
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }

                                                // Se crea la nueva acción stop
                                                var workActionViewModel = new WorkActionViewModel();
                                                workActionViewModel.WorkActivityId = waVM.Id;
                                                workActionViewModel.ActionName = "stop";
                                                workActionViewModel.StartDate = DateTime.Now;
                                                workActionViewModel.EndingDate = DateTime.Now;
                                                await _workActionViewManager.CreateAsync(_mapper.Map<WorkActionDTO>(workActionViewModel));

                                                // Se setea la fecha de regreso de la actividad, la cual es la misma que la fecha del ingreso.
                                                waVM.ComebackDate = incomeDetailDTO.IncomeDate;

                                                var waDTO = _mapper.Map<WorkActivityDTO>(waVM);

                                                updateResult = await _workActivityViewManager.UpdateAsync(waDTO);
                                                if (!updateResult.Succeeded && updateResult.Data == 0)
                                                {
                                                    updateDetailHasSuccess = false;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            return new JsonResult(new { isValid = false });
                        }
                    }
                    if (updateDetailHasSuccess)
                    {
                        return new JsonResult(new { isValid = true });
                    }
                    else
                    {
                        return new JsonResult(new { isValid = false });
                    }
                }
                else
                {
                    return new JsonResult(new { isValid = false });
                }
            }
            catch (DBConcurrencyException _concurrencyException)
            {
                _notify.Error("Error de concurrencia de datos: " + _concurrencyException.Message.ToString());
                return new JsonResult(new { isValid = false });
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message.ToString());
                return new JsonResult(new { isValid = false });
            }
        }

        //public async Task<JsonResult> stopIncomes(int[] incomeDetailsIds)
        //{
        //    try
        //    {
        //        if (incomeDetailsIds.Length > 0)
        //        {
        //            var updateDetailHasSuccess = true;
        //            foreach (var incomeDetailId in incomeDetailsIds)
        //            {
        //                var getIncomeDetailById = _incomeDetailViewManager.FindBySpecification(new IncomeDetailsWithAllSpecifications(incomeDetailId));
        //                if (getIncomeDetailById.Result.Succeeded)
        //                {
        //                    var incomeDetailVM = _mapper.Map<IncomeDetailViewModel>(getIncomeDetailById.Result.Data.FirstOrDefault());

        //                    // Se setea el estado "Finalizado" al momento que se le pone stop.
        //                    incomeDetailVM.IncomeStateId = 3;
        //                    incomeDetailVM.IncomeDate = DateTime.Now;

        //                    // Actualizo el estado del producto faltante a "Finalizado"
        //                    if (incomeDetailVM.MissingProductId.HasValue)
        //                    {
        //                        var getMPById = await _missingProductViewManager.FindWithAllSpecification(new MissingProductByIdSpecification(incomeDetailVM.MissingProductId.Value));
        //                        if (getMPById.Succeeded && getMPById.Data != null)
        //                        {
        //                            var mpVM = _mapper.Map<MissingProductViewModel>(getMPById.Data.First());
        //                            mpVM.StateMissingProductId = 5;
        //                            var mpDTO = _mapper.Map<MissingProductDTO>(mpVM);
        //                            await _missingProductViewManager.UpdateAsync(mpDTO);
        //                        }
        //                    }

        //                    //// Si es por una OC de compras podemos poner la fecha de la OC de compras.
        //                    //if (incomeDetailVM.PurchaseOrderDetail != null)
        //                    //{
        //                    //    incomeDetailVM.IncomeDate = incomeDetailVM.PurchaseOrderDetail.PurchaseOrderHeader.Date;
        //                    //}
        //                    //// Si es por una OC de servicios, es la fecha del remito de la OT de envíos o la fecha de OC de servicios que es la misma.
        //                    //else if (incomeDetailVM.DeliveryNoteHeaderId.HasValue)
        //                    //{
        //                    //    var getDNDById = await _deliveryNoteHeaderViewManager.GetByIdAsync(incomeDetailVM.DeliveryNoteHeaderId.Value);
        //                    //    if (getDNDById.Succeeded && getDNDById.Data != null)
        //                    //    {
        //                    //        incomeDetailVM.IncomeDate = getDNDById.Data.Date;
        //                    //    }
        //                    //}
        //                    //else
        //                    //{
        //                    //    incomeDetailVM.IncomeDate = DateTime.Now;
        //                    //}

        //                    var incomeDetailDTO = _mapper.Map<IncomeDetailDTO>(incomeDetailVM);
        //                    var updateResult = await _incomeDetailViewManager.UpdateAsync(incomeDetailDTO);
        //                    if (!updateResult.Succeeded && updateResult.Data == 0)
        //                    {
        //                        updateDetailHasSuccess = false;
        //                    }

        //                    // Fran: 3/4/2023 Se finalizan todas las actividades relacionadas al encabezado IncomeHeader y su detalle
        //                    // para poder calcular mejor el porcentaje de avance en las planillas de Envíos.
        //                    if (incomeDetailVM.IncomeHeader.ExternalProcessStationId.HasValue && incomeDetailVM.OTNumber.HasValue)
        //                    {
        //                        var getWAByStationAndWONumber = await _workActivityViewManager.FindBySpecification(new WorkActivitiesByWONumberSpecification(incomeDetailVM.OTNumber.Value, incomeDetailVM.IncomeHeader.ExternalProcessStationId.Value));
        //                        if (getWAByStationAndWONumber.Succeeded)
        //                        {
        //                            if (getWAByStationAndWONumber.Data.Count > 0)
        //                            {
        //                                var workActivitiesVM = _mapper.Map<List<WorkActivityViewModel>>(getWAByStationAndWONumber.Data);
        //                                foreach (var waVM in workActivitiesVM)
        //                                {
        //                                    if (waVM.WorkOrderItem.ProductId == incomeDetailVM.IncomeProductId)
        //                                    {
        //                                        if (waVM.NextStation.Trim().Split("-")[0].Trim().StartsWith("R"))
        //                                        {
        //                                            var getRPSByProductId = await _productViewManager.FindBySpecification(new ProductWithRelProductStationsSpecification(incomeDetailVM.IncomeProductId));
        //                                            if (getRPSByProductId.Succeeded)
        //                                            {
        //                                                var relProductStationVMs = _mapper.Map<List<RelProductStationViewModel>>(getRPSByProductId.Data.First().RelProductStations.ToList());
        //                                                foreach (var rps in relProductStationVMs)
        //                                                {
        //                                                    if (waVM.NextStation.Trim().Equals(rps.Station.AbreviationDescription.Trim()))
        //                                                    {
        //                                                        var getWAToFinish = await _workActivityViewManager.FindBySpecification(new WorkActivitiesByWorkOrderItemIdSpecification(waVM.WorkOrderItemId));
        //                                                        if (getWAToFinish.Succeeded)
        //                                                        {
        //                                                            foreach (var wa in getWAToFinish.Data)
        //                                                            {
        //                                                                if (wa.ProductStation.Station.Abbreviation.Trim().Equals(waVM.NextStation.Trim().Split("-")[0].Trim()))
        //                                                                {
        //                                                                    // Se crea la nueva acción stop para el puesto de recepción
        //                                                                    var workActionVM = new WorkActionViewModel();
        //                                                                    workActionVM.WorkActivityId = wa.Id;
        //                                                                    workActionVM.ActionName = "stop";
        //                                                                    workActionVM.StartDate = DateTime.Now;
        //                                                                    workActionVM.EndingDate = DateTime.Now;
        //                                                                    await _workActionViewManager.CreateAsync(_mapper.Map<WorkActionDTO>(workActionVM));
        //                                                                }
        //                                                            }
        //                                                        }
        //                                                    }
        //                                                }
        //                                            }
        //                                        }

        //                                        // Se crea la nueva acción stop
        //                                        var workActionViewModel = new WorkActionViewModel();
        //                                        workActionViewModel.WorkActivityId = waVM.Id;
        //                                        workActionViewModel.ActionName = "stop";
        //                                        workActionViewModel.StartDate = DateTime.Now;
        //                                        workActionViewModel.EndingDate = DateTime.Now;
        //                                        await _workActionViewManager.CreateAsync(_mapper.Map<WorkActionDTO>(workActionViewModel));

        //                                        // Se setea la fecha de regreso de la actividad, la cual es la misma que la fecha del ingreso.
        //                                        waVM.ComebackDate = incomeDetailVM.IncomeDate;

        //                                        var waDTO = _mapper.Map<WorkActivityDTO>(waVM);

        //                                        updateResult = await _workActivityViewManager.UpdateAsync(waDTO);
        //                                        if (!updateResult.Succeeded && updateResult.Data == 0)
        //                                        {
        //                                            updateDetailHasSuccess = false;
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    return new JsonResult(new { isValid = false });
        //                }
        //            }
        //            if (updateDetailHasSuccess)
        //            {
        //                return new JsonResult(new { isValid = true });
        //            }
        //            else
        //            {
        //                return new JsonResult(new { isValid = false });
        //            }
        //        }
        //        else
        //        {
        //            return new JsonResult(new { isValid = false });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //_notify.Error(ex.Message);
        //        return new JsonResult(new { isValid = false });
        //    }
        //}

    }
}
