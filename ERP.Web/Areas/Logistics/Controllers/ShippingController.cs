using ERP.Application.DTOs.Entities.Production;
using ERP.Application.Enums;
using ERP.Application.Interfaces.ViewManagers.CommercialDocuments.DeliveryNote;
using ERP.Application.Interfaces.ViewManagers.Production;
using ERP.Application.Interfaces.ViewManagers.ProductMod;
using ERP.Application.Interfaces.ViewManagers.Purchases.Providers;
using ERP.Application.Specification.CommercialDocuments.DeliveryNote;
using ERP.Application.Specification.Production.WorkActivitySpecifications;
using ERP.Application.Specification.Production.WorkOrderItemSpecifications;
using ERP.Application.Specification.Production.WorkOrderSpecifications;
using ERP.Application.Specification.Purchases.Providers;
using ERP.Infrastructure.Identity.Models;
using ERP.Language;
using ERP.Web.Abstractions;
using ERP.Web.Areas.Logistics.Models;
using ERP.Web.Areas.Logistics.Models.DeliveryNote;
using ERP.Web.Areas.Production.Controllers;
using ERP.Web.Areas.Production.Models;
using ERP.Web.Areas.Production.Models.WorkActivity;
using ERP.Web.Areas.ProductMod.Models;
using ERP.Web.Areas.Purchases.Models.Providers;
using ERP.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Web.Areas.Logistics.Controllers
{
    public static class ShippingStates
    {
        // Valores que se muestan en PreviousStation
        public const string Pending = "Pendiente";
        public const string ForSend = "Para enviar";
        public const string Sent = "Enviado";
        public const string Finished = "Finalizado";
    }

    [Area("Logistics")]
    public class ShippingController : BaseController<ShippingController>
    {
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IStationViewManager _stationViewManager;
        private readonly IWorkActivityViewManager _workActivityViewManager;
        private readonly IWorkOrderViewManager _workOrderViewManager;
        private readonly IProviderViewManager _providerViewManager;
        private readonly IDeliveryNoteHeaderViewManager _deliveryNoteHeaderViewManager;
        private readonly IDeliveryNoteDetailViewManager _deliveryNoteDetailViewManager;
        private readonly IWorkOrderItemViewManager _workOrderItemViewManager;
        private readonly IWorkActionViewManager _workActionViewManager;

        public ShippingController(IStringLocalizer<SharedResource> localizer, UserManager<ApplicationUser> userManager, IStationViewManager stationViewManager, IWorkActivityViewManager workActivityViewManager, IWorkOrderViewManager workOrderViewManager, IProviderViewManager providerViewManager, IDeliveryNoteHeaderViewManager deliveryNoteHeaderViewManager, IDeliveryNoteDetailViewManager deliveryNoteDetailViewManager, IWorkOrderItemViewManager workOrderItemViewManager, IWorkActionViewManager workActionViewManager)
        {
            _localizer = localizer;
            _userManager = userManager;
            _stationViewManager = stationViewManager;
            _workActivityViewManager = workActivityViewManager;
            _workOrderViewManager = workOrderViewManager;
            _providerViewManager = providerViewManager;
            _deliveryNoteHeaderViewManager = deliveryNoteHeaderViewManager;
            _deliveryNoteDetailViewManager = deliveryNoteDetailViewManager;
            _workOrderItemViewManager = workOrderItemViewManager;
            _workActionViewManager = workActionViewManager;
        }

        // Mismo método que IndexGrouped en WorkActivityController
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = _localizer["Shipments"].ToString() + " - " + _localizer["Work orders"].ToString();
            var model = new WorkActivityViewModel();
            model.currentUser = await _userManager.GetUserAsync(HttpContext.User);
            return View(model);
        }

        // Mismo método que LoadAllGrouped en WorkActivityController
        public async Task<IActionResult> _LoadAll(DatatableParamsViewModel datatableParams,
             string sSearch_2, string sSearch_3, string sSearch_4, string sSearch_5, string sSearch_6, string sSearch_7, string sSearch_8,
             string sSearch_9, string sSearch_10, string sSearch_11, string sSearch_12,
             string dateFromFilterValue, string dateToFilterValue, string productItemCode, string deliveryNoteNumber, string provider, string transportProvider)
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                var response = await _workActivityViewManager.FindBySpecification(new WorkActivityWithAllSpecification(true, true));
                if (response.Succeeded)
                {
                    var viewModel = new List<WorkActivityViewModel>();

                    var workActivityViewModels = _mapper.Map<List<WorkActivityViewModel>>(response.Data);

                    if (!string.IsNullOrEmpty(productItemCode) || !string.IsNullOrEmpty(deliveryNoteNumber) || !string.IsNullOrEmpty(provider) || !string.IsNullOrEmpty(transportProvider))
                    {
                        foreach (var wa in workActivityViewModels)
                        {
                            var getDeliveryNoteDetailByWaId = await _deliveryNoteDetailViewManager.FindBySpecification(new DNDByWorkActivityIdSpecification(wa.Id));
                            if (getDeliveryNoteDetailByWaId.Succeeded)
                            {
                                var deliveryNoteDetailsVM = _mapper.Map<List<DeliveryNoteDetailViewModel>>(getDeliveryNoteDetailByWaId.Data);

                                if (!string.IsNullOrEmpty(productItemCode))
                                {
                                    if (wa.WorkOrderItem.Product.Code.ToString().ToLower().Contains(productItemCode.ToLower()))
                                    {
                                        viewModel.Add(wa);
                                    }
                                    else
                                    {
                                        deliveryNoteDetailsVM = deliveryNoteDetailsVM.Where(dnd => dnd.ProductItem.Code.ToString().ToLower().Contains(productItemCode.ToLower())).ToList();
                                    }
                                }
                                if (!string.IsNullOrEmpty(deliveryNoteNumber))
                                {
                                    deliveryNoteDetailsVM = deliveryNoteDetailsVM.Where(dnd => dnd.DeliveryNoteHeader.Number != null && dnd.DeliveryNoteHeader.Number.ToString().ToLower().Contains(deliveryNoteNumber.ToLower())).ToList();
                                }
                                if (!string.IsNullOrEmpty(provider))
                                {
                                    deliveryNoteDetailsVM = deliveryNoteDetailsVM.Where(dnd => dnd.DeliveryNoteHeader.Provider.getBussinessNameOrName.ToString().ToLower().Contains(provider.ToLower())).ToList();
                                }
                                if (!string.IsNullOrEmpty(transportProvider))
                                {
                                    deliveryNoteDetailsVM = deliveryNoteDetailsVM.Where(dnd => dnd.DeliveryNoteHeader.TransportProvider.getBussinessNameOrName.ToString().ToLower().Contains(transportProvider.ToLower())).ToList();
                                }

                                if (deliveryNoteDetailsVM.Count() > 0)
                                {
                                    viewModel.Add(wa);
                                }
                            }
                        }
                    }
                    else
                    {
                        viewModel = workActivityViewModels;
                    }

                    //Filtrado por usuario si no es admin
                    if (!HttpContext.User.IsInRole(Roles.SuperAdmin.ToString()))
                    {
                        viewModel = viewModel.Where(activity => activity.AssignedUsersIds == null ? false : activity.AssignedUsersIds.Split(',').Any(assignedId => assignedId == currentUser.Id)).ToList();
                    }

                    // Fran: Se cambia agrupamiento por WorkOrderHeaderNumber y Station
                    IEnumerable<WorkActivityGroupViewModel> workActivityListGroup = viewModel.GroupBy(x => (x.WorkOrderItem.WorkOrder.WorkOrderHeader.WorkOrderHeaderNumber, x.ProductStation.StationId))
                                       .Select(grp => new WorkActivityGroupViewModel
                                       {
                                           WorkOrderHeaderNumber = grp.Key.WorkOrderHeaderNumber,
                                           StationId = grp.Key.StationId,
                                           Activities = grp.Select(act => act).ToList(),
                                           Quantity = grp.Count(),
                                           WorkOrderId = grp.First().WorkOrderItem.WorkOrderId,
                                           WorkOrderIds = grp.Select(x => x.WorkOrderItem.WorkOrderId).ToHashSet(),
                                           ActivitiesStartDates = grp.Select(x => x.StartDate).ToList(),
                                           ActivitiesEndDates = grp.Select(x => x.EndingDate).ToList(),
                                           isActive = grp.All(x => x.StateActivity),
                                           ProductNumber = grp.First().WorkOrderItem.WorkOrder.OrderDetail.ProductNumber,
                                           ProductNumbers = grp.Select(x => x.WorkOrderItem.WorkOrder.OrderDetail.ProductNumber).ToHashSet(),
                                           ProductDescription = grp.First().WorkOrderItem.WorkOrder.OrderDetail.Product.CodeAndDescription,
                                           StructureDescription = grp.First().WorkOrderItem.WorkOrder.OrderDetail.Structure != null ? grp.First().WorkOrderItem.WorkOrder.OrderDetail.Structure.Description : "",
                                           StationDescription = grp.First().ProductStation.Station.AbreviationDescription,
                                           FinishedQuantity = (grp.Where(x => x.isFinished).Count()),
                                           //TotalTime = new TimeSpan(grp.Sum(x => x.TotalTime.Value.Ticks)).ToString(@"dd\ hh\:mm\:ss"),
                                           ActivitiesState = grp.All(x => x.getState == ActivityStates.Finalizado) ?
                                                                ActivityStates.Finalizado.ToString() : grp.Any(x => x.StartDate != null) ?
                                                                ActivityStates.EnProceso.ToString() : ActivityStates.Pendiente.ToString(),
                                           //DateToProduction = grp.First().DateToProduction.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                           DateToProduction = grp.First().CreatedOn.ToString("dd/MM/yyyy HH:mm:ss"),
                                           WOCreateDate = grp.First().WorkOrderItem.WorkOrder.WorkOrderHeader.CreatedOn,
                                           GroupUsers = grp.First().AssignedUsersIds == null ? "" : getUsersStringAsync(grp.First().AssignedUsersIds.Split(','))
                                       }).ToList();

                    workActivityListGroup = workActivityListGroup.Where(wa => Convert.ToDateTime(wa.DateToProduction).Date >= Convert.ToDateTime(dateFromFilterValue).Date && Convert.ToDateTime(wa.DateToProduction).Date <= Convert.ToDateTime(dateToFilterValue).Date);

                    foreach (var actGroup in workActivityListGroup)
                    {
                        // Agrupo las actividades de cada puesto para poder obtener correctamente el porcentaje de avance según los agrupamientos en cada puesto.
                        var responsestation = await _stationViewManager.GetByIdAsync(actGroup.StationId);
                        if (responsestation.Succeeded)
                        {
                            var stationViewModel = _mapper.Map<StationViewModel>(responsestation.Data);

                            if (stationViewModel.WorkOrderDisplayType == Stations.Envios)
                            {
                                // Filtrar la informacion según de que tipo de planilla se trate.
                                actGroup.Activities = await filterWAInformationByStock(actGroup.Activities, stationViewModel);
                            }

                            actGroup.Activities = actGroup.Activities.GroupBy(x => x.WorkOrderItem.Product.Code).Select(x => x.First()).ToList();
                        }
                    }

                    // Fran 13/4/2023: Se ordena primero por fecha de creación OT, luego por numero de producto y luego por order de area funcional y abreviatura del puesto de trabajo.
                    workActivityListGroup = workActivityListGroup.OrderByDescending(wa => wa.WOCreateDate).ThenBy(wa => wa.ProductNumber).ThenBy(wa => wa.GroupStation != null ? (wa.GroupStation.FunctionalArea != null ? wa.GroupStation.FunctionalArea.Order : wa.GroupStationId) : wa.GroupStationId).ThenBy(wa => wa.GroupStation.Abbreviation);

                    // Cualquier planilla que no tenga ninguna actividad que mostrar que no se muestre en "Ordenes de trabajo", "Envios" y "Abastecimiento".
                    workActivityListGroup = workActivityListGroup.Where(wag => wag.Activities.Count > 0).ToList();

                    if (!string.IsNullOrEmpty(sSearch_2))
                    {
                        workActivityListGroup = workActivityListGroup.Where(wag => wag.WorkOrderHeaderNumber.ToString().ToLower().Contains(sSearch_2.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_3))
                    {
                        workActivityListGroup = workActivityListGroup.Where(wag => wag.ProductNumber.ToString().ToLower().Contains(sSearch_3.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_4))
                    {
                        workActivityListGroup = workActivityListGroup.Where(wag => wag.ProductDescription.ToString().ToLower().Contains(sSearch_4.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_5))
                    {
                        workActivityListGroup = workActivityListGroup.Where(wag => wag.StructureDescription.ToString().ToLower().Contains(sSearch_5.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_6))
                    {
                        workActivityListGroup = workActivityListGroup.Where(wag => wag.StationDescription.ToString().ToLower().Contains(sSearch_6.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_7))
                    {
                        workActivityListGroup = workActivityListGroup.Where(wag => wag.ActivitiesState.ToString().ToLower().Contains(sSearch_7.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_8))
                    {
                        workActivityListGroup = workActivityListGroup.Where(wag => wag.ProgressPercentage.ToString().ToLower().Contains(sSearch_8.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_9))
                    {
                        workActivityListGroup = workActivityListGroup.Where(wag => wag.DateToProduction.ToString().ToLower().Contains(sSearch_9.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_10))
                    {
                        workActivityListGroup = workActivityListGroup.Where(wag => wag.StartDate.ToString().ToLower().Contains(sSearch_10.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_11))
                    {
                        workActivityListGroup = workActivityListGroup.Where(wag => wag.EndDate.ToString().ToLower().Contains(sSearch_11.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(sSearch_12))
                    {
                        workActivityListGroup = workActivityListGroup.Where(wag => wag.TotalTime.ToString().ToLower().Contains(sSearch_12.ToLower())).ToList();
                    }

                    var sortColumnIndex = datatableParams.iSortCol_0;
                    var sortDirection = datatableParams.sSortDir_0;

                    if (sortColumnIndex == 2)
                    {
                        workActivityListGroup = sortDirection == "asc" ? workActivityListGroup.OrderBy(wag => wag.WorkOrderHeaderNumber) : workActivityListGroup.OrderByDescending(wag => wag.WorkOrderHeaderNumber);
                    }
                    if (sortColumnIndex == 3)
                    {
                        workActivityListGroup = sortDirection == "asc" ? workActivityListGroup.OrderBy(wag => wag.ProductNumber) : workActivityListGroup.OrderByDescending(wag => wag.ProductNumber);
                    }
                    if (sortColumnIndex == 4)
                    {
                        workActivityListGroup = sortDirection == "asc" ? workActivityListGroup.OrderBy(wag => wag.ProductDescription) : workActivityListGroup.OrderByDescending(wag => wag.ProductDescription);
                    }
                    if (sortColumnIndex == 5)
                    {
                        workActivityListGroup = sortDirection == "asc" ? workActivityListGroup.OrderBy(wag => wag.StructureDescription) : workActivityListGroup.OrderByDescending(wag => wag.StructureDescription);
                    }
                    if (sortColumnIndex == 6)
                    {
                        workActivityListGroup = sortDirection == "asc" ? workActivityListGroup.OrderBy(wag => wag.StationDescription) : workActivityListGroup.OrderByDescending(wag => wag.StationDescription);
                    }
                    if (sortColumnIndex == 7)
                    {
                        workActivityListGroup = sortDirection == "asc" ? workActivityListGroup.OrderBy(wag => wag.ActivitiesState) : workActivityListGroup.OrderByDescending(wag => wag.ActivitiesState);
                    }
                    if (sortColumnIndex == 8)
                    {
                        workActivityListGroup = sortDirection == "asc" ? workActivityListGroup.OrderBy(wag => wag.ProgressPercentage) : workActivityListGroup.OrderByDescending(wag => wag.ProgressPercentage);
                    }
                    if (sortColumnIndex == 9)
                    {
                        workActivityListGroup = sortDirection == "asc" ? workActivityListGroup.OrderBy(wag => Convert.ToDateTime(wag.DateToProduction)) : workActivityListGroup.OrderByDescending(wag => Convert.ToDateTime(wag.DateToProduction));
                    }
                    if (sortColumnIndex == 10)
                    {
                        workActivityListGroup = sortDirection == "asc" ? workActivityListGroup.OrderBy(wag => wag.StartDate) : workActivityListGroup.OrderByDescending(wag => wag.StartDate);
                    }
                    if (sortColumnIndex == 11)
                    {
                        workActivityListGroup = sortDirection == "asc" ? workActivityListGroup.OrderBy(wag => wag.EndDate) : workActivityListGroup.OrderByDescending(wag => wag.EndDate);
                    }
                    if (sortColumnIndex == 12)
                    {
                        workActivityListGroup = sortDirection == "asc" ? workActivityListGroup.OrderBy(wag => wag.TotalTime) : workActivityListGroup.OrderByDescending(wag => wag.TotalTime);
                    }

                    var displayResult = workActivityListGroup.Skip(datatableParams.iDisplayStart)
                        .Take(datatableParams.iDisplayLength).ToList();

                    var totalRecords = workActivityListGroup.Count();

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
                    _notify.Information(_localizer["Error retrieving data."]);
                    return new JsonResult(new { isValid = false });
                }
            }
            catch (Exception ex)
            {
                _notify.Error($"{_localizer["Error"]}: {ex.Message}.");
                return new JsonResult(new { isValid = false });
            }
        }

        // Tarjeta trello: "Que dejar y sacar de stock en OTs"; Archivo excel: "Caracteristicas de OTs por puesto.xlsx" 
        public async Task<List<WorkActivityViewModel>> filterWAInformationByStock(List<WorkActivityViewModel> workActivitiesVM, StationViewModel stationViewModel)
        {
            try
            {
                List<WorkActivityViewModel> workActivitiesToAdd = new List<WorkActivityViewModel>();
                if (workActivitiesVM.Any(wa => wa.WorkOrderItem.WorkOrder.OrderDetail.ProductNumber.StartsWith("01") || wa.WorkOrderItem.WorkOrder.OrderDetail.ProductNumber.StartsWith("02") || wa.WorkOrderItem.WorkOrder.OrderDetail.ProductNumber.StartsWith("03")))
                {
                    if (stationViewModel.WorkOrderDisplayType == Stations.Envios)
                    {
                        foreach (var wa in workActivitiesVM)
                        {
                            if (wa.WorkOrderItem.StructureId != null)
                            {
                                // es padre => tiene hijos
                                // tengo que ver si ese padre no es hijo de otro item.
                                if (wa.WorkOrderItem.WorkOrderItemOfId.HasValue)
                                {
                                    var getwoiby = await _workOrderItemViewManager.FindBySpecification(new WOIByIdSpecification(wa.WorkOrderItem.WorkOrderItemOfId.Value));
                                    if (getwoiby.Succeeded)
                                    {
                                        var fatherfatherwoivm = _mapper.Map<WorkOrderItemViewModel>(getwoiby.Data.First());
                                        if (!fatherfatherwoivm.Product.ProductFeature.StoredStock && !wa.WorkOrderItem.Product.ProductFeature.StoredStock)
                                        {
                                            workActivitiesToAdd.Add(wa);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                // es hijo => busco al padre por el workorderitemofid
                                if (wa.WorkOrderItem.WorkOrderItemOfId.HasValue)
                                {
                                    var fatherwoi = await _workOrderItemViewManager.FindBySpecification(new WOIByIdSpecification(wa.WorkOrderItem.WorkOrderItemOfId.Value));
                                    if (fatherwoi.Succeeded)
                                    {
                                        var fatherwoivm = _mapper.Map<WorkOrderItemViewModel>(fatherwoi.Data.First());
                                        if (fatherwoivm.WorkOrderItemOfId.HasValue)
                                        {
                                            var fatherfatherwoi = await _workOrderItemViewManager.FindBySpecification(new WOIByIdSpecification(fatherwoivm.WorkOrderItemOfId.Value));
                                            if (fatherfatherwoi.Succeeded)
                                            {
                                                var fatherfatherwoivm = _mapper.Map<WorkOrderItemViewModel>(fatherfatherwoi.Data.First());
                                                if (!fatherfatherwoivm.Product.ProductFeature.StoredStock && !fatherwoivm.Product.ProductFeature.StoredStock && !wa.WorkOrderItem.Product.ProductFeature.StoredStock)
                                                {
                                                    workActivitiesToAdd.Add(wa);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (!wa.WorkOrderItem.Product.ProductFeature.StoredStock)
                                            {
                                                workActivitiesToAdd.Add(wa);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                workActivitiesVM = workActivitiesToAdd;

                return workActivitiesVM;
            }
            catch
            {
                _notify.Error("No se pudo filtrar la información de las actividades respecto a los datos de stock.");
                return workActivitiesVM;
            }
        }

        // Mismo método que Index en WorkActivityController
        public async Task<IActionResult> IndexByWorkOrderAndStation(string workOrderIds, int stationId)
        {
            var model = new WorkActivitiesViewModel();

            var responsestation = await _stationViewManager.GetByIdAsync(stationId);
            if (responsestation.Succeeded)
            {
                model.Station = _mapper.Map<StationViewModel>(responsestation.Data);
            }

            var workOrderIdList = workOrderIds.Split(',');

            foreach (var id in workOrderIdList)
            {
                var responseworkorder = await _workOrderViewManager.FindBySpecification(new WorkOrderWithHeaderSpecification(Convert.ToInt32(id)));
                if (responseworkorder.Succeeded)
                {
                    model.WorkOrders.Add(_mapper.Map<WorkOrderViewModel>(responseworkorder.Data.FirstOrDefault()));
                }

                var responseactivities = await _workActivityViewManager.FindBySpecification(new ActivitiesByWorkOrderAndStationSpecification(Convert.ToInt32(id), stationId));
                if (responseactivities.Succeeded)
                {
                    var activities = _mapper.Map<List<WorkActivityViewModel>>(responseactivities.Data);

                    model.WorkActivities.AddRange(activities);
                }

                model.ActivitiesGroups = generateActivitiesGroups(model.WorkActivities, model.Station);

            }

            return View(model);
        }

        public List<WorkActivityGroupViewModel> generateActivitiesGroups(List<WorkActivityViewModel> activities, StationViewModel station)
        {

            var groupsList = new List<WorkActivityGroupViewModel>();
            groupsList = activities.GroupBy(x => x.WorkOrderItem.Product.ProductFeature.RawMaterialCode).Select(grp => new WorkActivityGroupViewModel
            {
                Activities = grp.Select(x => x).ToList(),
                SubGroups = grp.Select(x => x).ToList().GroupBy(x => x.WorkOrderItem.Product.Code).Select(subgrp => new WorkActivitySubGroupViewModel
                {
                    Quantity = subgrp.Sum(x => x.WorkOrderItem.Quantity),
                    Activities = subgrp.Select(x => x).ToList(),
                }).ToList()
            }).ToList();
            return groupsList;
        }

        // Mismo método que LoadFiltered en WorkActivityController
        public async Task<IActionResult> _LoadAllActivities(string workOrderIds, int stationId)
        {
            try
            {
                var responsestation = await _stationViewManager.GetByIdAsync(stationId);
                var stationViewModel = new StationViewModel();
                if (responsestation.Succeeded)
                {
                    stationViewModel = _mapper.Map<StationViewModel>(responsestation.Data);
                }

                var viewmodel = new WorkActivitiesViewModel();
                var workOrderIdList = workOrderIds.Split(',');
                foreach (var id in workOrderIdList)
                {
                    var responseworkorder = await _workOrderViewManager.FindBySpecification(new WorkOrderWithHeaderSpecification(Convert.ToInt32(id)));
                    if (responseworkorder.Succeeded)
                    {
                        viewmodel.WorkOrders.Add(_mapper.Map<WorkOrderViewModel>(responseworkorder.Data.FirstOrDefault()));
                    }

                    var responseactivities = await _workActivityViewManager.FindBySpecification(new ActivitiesByWorkOrderAndStationSpecification(Convert.ToInt32(id), stationId));
                    if (responseactivities.Succeeded)
                    {
                        var activities = _mapper.Map<List<WorkActivityViewModel>>(responseactivities.Data);

                        // OT Envios   
                        if (stationViewModel.WorkOrderDisplayType != Stations.Envios)
                        {
                            activities = activities.Where(wa => wa.ToShipments.HasValue ? wa.ToShipments.Value == true : wa.ToShipments != null).ToList();
                        }
                        else
                        {
                            // Filtrar la informacion según de que tipo de planilla se trate.
                            activities = await filterWAInformationByStock(activities, stationViewModel);
                        }

                        viewmodel.WorkActivities.AddRange(activities);

                        foreach (var wa in viewmodel.WorkActivities)
                        {
                            // Me traigo informacion de los remitos creados para las activities
                            var getDNDByActivitiesIds = await _deliveryNoteDetailViewManager.FindBySpecification(new DNDByWorkActivityIdSpecification(wa.Id));
                            if (getDNDByActivitiesIds.Succeeded)
                            {
                                if (getDNDByActivitiesIds.Data.Count > 0)
                                {
                                    wa.DeliveryNoteHeaderViewModel = _mapper.Map<DeliveryNoteHeaderViewModel>(getDNDByActivitiesIds.Data.FirstOrDefault().DeliveryNoteHeader);
                                }
                            }

                            // Me traigo informacion en PreviousStation sobre el puesto anterior para representar el estado de cada actividad.
                            // Tengo que traerme con wa.WorkOrderItemId todas sus actividades
                            // Cuando tengo todas las actividades de ese WOI, tengo que ver si se inicio en alguno de los puestos anteriores a los de envio
                            // Entonces validando inicios y fines, obtengo el estado segun la accion en dicho puesto y seteo la descripcion de ese puesto en
                            // PreviousStation
                            // Fran 14/11/2022: Existe un problema cuando se inician actividades en un puesto agrupadas, solo se crea una acción de inicio
                            // para la primera del grupo, por lo tanto el estado inicial segun ese puesto solo el sistema lo conoce para la primera actividad

                            // TODO: Optimizar
                            var getActivitiesByWoiId = await _workActivityViewManager.FindBySpecification(new WorkActivitiesByWorkOrderItemIdSpecification(true, wa.WorkOrderItemId));
                            if (getActivitiesByWoiId.Succeeded)
                            {
                                var activitiesByWoi = _mapper.Map<List<WorkActivityViewModel>>(getActivitiesByWoiId.Data);
                                activitiesByWoi = activitiesByWoi.OrderBy(a => a.ProductStation.Order).ToList();

                                foreach (var act in activitiesByWoi)
                                {
                                    if (wa.DeliveryNoteHeaderViewModel != null)
                                    {
                                        if (wa.ComebackDate != null)
                                        {
                                            wa.PreviousStation = ShippingStates.Finished;
                                            break;
                                        }

                                        if (wa.DeliveryNoteHeaderViewModel.wasExportedToPDF)
                                        {
                                            wa.PreviousStation = ShippingStates.Sent;
                                            break;
                                        }
                                        else
                                        {
                                            wa.PreviousStation = ShippingStates.ForSend;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (wa.WorkOrderItem.Product.Code == "PD0450")
                                        {
                                            Console.WriteLine("asd");
                                        }

                                        if (wa.ComebackDate != null)
                                        {
                                            wa.PreviousStation = ShippingStates.Finished;
                                            break;
                                        }
                                        else if (wa.ProductStation.Order.HasValue)
                                        {
                                            if (activitiesByWoi.Count == 1 && activitiesByWoi.First().ProductStation.StationId == wa.ProductStation.StationId)
                                            {
                                                wa.PreviousStation = ShippingStates.ForSend;
                                                break;
                                            }
                                            else if ((wa.ProductStation.Order.Value - act.ProductStation.Order.Value) == 1)
                                            {
                                                if (!act.isStarted && !act.isFinished && wa.PreviousStation == null)
                                                {
                                                    wa.PreviousStation = ShippingStates.Pending;
                                                    break;
                                                }
                                                else if (act.isStarted && act.isFinished)
                                                {
                                                    wa.PreviousStation = ShippingStates.ForSend;
                                                    break;
                                                }
                                                else if (act.isStarted)
                                                {
                                                    wa.PreviousStation = act.ProductStation.Station.AbreviationDescription;
                                                    break;
                                                }
                                            }
                                            else
                                            {
                                                if (!act.isStarted && !act.isFinished && wa.PreviousStation == null)
                                                {
                                                    wa.PreviousStation = ShippingStates.Pending;
                                                    break;
                                                }
                                                else if (act.isStarted && act.isFinished)
                                                {
                                                    wa.PreviousStation = act.ProductStation.Station.AbreviationDescription;
                                                }
                                                else if (act.isStarted)
                                                {
                                                    wa.PreviousStation = act.ProductStation.Station.AbreviationDescription;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                viewmodel.ActivitiesGroups = generateActivitiesGroups(viewmodel.WorkActivities, stationViewModel);
                viewmodel.Station = stationViewModel;

                switch (stationViewModel.WorkOrderDisplayType)
                {
                    case Stations.Envios:
                        return PartialView("WorkActivitiesByStationEnvios", viewmodel);
                    default:
                        return PartialView("WorkActivitiesByStationEnvios", viewmodel);
                }

            }
            catch (Exception ex)
            {
                _notify.Error($"{_localizer["Error"]}: {ex.Message}.");
                return new JsonResult(new { isValid = false });
            }
        }

        public string getUsersStringAsync(string[] stringUsersIds)
        {
            var users = new List<string>();
            foreach (var id in stringUsersIds)
            {
                if (id != "")
                {
                    var userresponse = _userManager.FindByIdAsync(id).Result;
                    if (userresponse != null)
                    {
                        users.Add(_userManager.FindByIdAsync(id).Result.FirstAndLastName);
                    }

                }

            }
            if (users.Count > 0)
            {
                return string.Join('-', users);
            }
            return "";
        }

        public List<WorkActivityViewModel> clearObjectCycle(List<WorkActivityViewModel> vmList)
        {
            foreach (var waVM in vmList)
            {
                if (waVM.WorkOrderItem != null)
                {
                    waVM.WorkOrderItem.WorkActivities.First().WorkOrderItem = null;
                    waVM.WorkOrderItem.WorkOrder.WorkOrderItems = new List<WorkOrderItemViewModel>();
                    waVM.WorkOrderItem.WorkOrder.WorkOrderHeader.WorkOrders = new List<WorkOrderViewModel>();
                    waVM.WorkOrderItem.WorkOrder.OrderDetail.WorkOrder = null;
                }
            }
            return vmList;
        }

        // Fran: Método encargado de enviar WA a envíos desde el "detalle de avance" de una OT, view: /WorkOrder/_ViewAllPartial
        public async Task<JsonResult> GetWorkActivitiesToShipments(int[] woiIds, bool isSparesOrStock = false, string workOrderIds = "")
        {
            try
            {
                var sendToShipmentVM = new SendToShipmentViewModel();

                sendToShipmentVM.WorkOrderItemIds = woiIds;
                sendToShipmentVM.isSparesOrStock = isSparesOrStock;
                sendToShipmentVM.workOrderIds = workOrderIds;

                var stationsListItems = new List<SelectListItem>();
                var workActivities = new List<WorkActivityViewModel>();
                var stationsVM = new List<StationViewModel>();

                foreach (var woiId in sendToShipmentVM.WorkOrderItemIds)
                {
                    var getWorkActivitiesById = await _workActivityViewManager.FindBySpecification(new WorkActivitiesByWorkOrderItemIdSpecification(true, woiId));
                    if (getWorkActivitiesById.Succeeded)
                    {
                        var workActivitiesVM = _mapper.Map<List<WorkActivityViewModel>>(getWorkActivitiesById.Data);

                        // Lo seteo así porque es el mismo WorkOrderId para todas las WorkActivities
                        sendToShipmentVM.WorkOrderId = workActivitiesVM.First().WorkOrderItem.WorkOrderId;

                        foreach (var waVM in workActivitiesVM)
                        {
                            if (!waVM.isStarted && !waVM.isFinished)
                            {
                                workActivities.Add(waVM);

                                if (waVM.ToShipments == null)
                                {
                                    stationsVM.Add(waVM.ProductStation.Station);
                                }
                                else
                                {
                                    if (!waVM.ToShipments.Value)
                                    {
                                        stationsVM.Add(waVM.ProductStation.Station);
                                    }
                                }
                            }
                        }
                    }
                }

                var stationsVMGroupBy = stationsVM.Where(st => st.WorkOrderDisplayType != Stations.Envios).GroupBy(st => st.Id).ToList();
                foreach (var stGB in stationsVMGroupBy)
                {
                    if (stGB.Count() == woiIds.Count())
                    {
                        var selectListItem = new SelectListItem(stGB.First().AbreviationDescription, stGB.Key.ToString(), false);
                        if (stationsListItems.FirstOrDefault(t => t.Value == selectListItem.Value) == null)
                        {
                            stationsListItems.Add(selectListItem);
                        }
                    }
                    else
                    {
                        if (woiIds.Count() == 1)
                        {
                            var selectListItem = new SelectListItem(stGB.First().AbreviationDescription, stGB.Key.ToString(), false);
                            if (stationsListItems.FirstOrDefault(t => t.Value == selectListItem.Value) == null)
                            {
                                stationsListItems.Add(selectListItem);
                            }
                        }
                    }
                }

                if (stationsListItems.Count() > 1)
                {
                    stationsListItems.Add(new SelectListItem(_localizer["All work stations"].ToString(), "0", true));
                }
                else if (stationsListItems.Count() == 0)
                {
                    _notify.Information(_localizer["All stations were sent to shipments."].ToString());
                    return new JsonResult(new { isValid = false });
                }

                sendToShipmentVM.Stations = new SelectList(stationsListItems, "Value", "Text");

                // Cargo el dropdown de proveedores
                var providersResponse = await _providerViewManager.FindBySpecification(new ProvidersGetAllSpecification());
                if (providersResponse.Succeeded)
                {
                    var providersVM = _mapper.Map<List<ProviderViewModel>>(providersResponse.Data);
                    sendToShipmentVM.Providers = new SelectList(providersVM.Where(pr => pr.ProviderType == "Servicios").ToList(), nameof(ProviderViewModel.Id), nameof(ProviderViewModel.getBussinessNameOrName), null, null);
                }

                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_SendToShipments", sendToShipmentVM) });

            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return new JsonResult(new { isValid = false });
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostAssignToShipment(SendToShipmentViewModel sendToShipmentVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (sendToShipmentVM.StationId != null)
                    {
                        if (sendToShipmentVM.ProviderId != null)
                        {
                            var updateSuccesfully = false;
                            string stationDescription = _localizer["All stations"].ToString();
                            foreach (var woiId in sendToShipmentVM.WorkOrderItemIds)
                            {
                                var getWorkActivitiesByWoiId = await _workActivityViewManager.FindBySpecification(new WorkActivitiesByWorkOrderItemIdSpecification(true, woiId));
                                if (getWorkActivitiesByWoiId.Succeeded)
                                {
                                    var waVMs = _mapper.Map<List<WorkActivityViewModel>>(getWorkActivitiesByWoiId.Data);

                                    foreach (var waVM in waVMs)
                                    {
                                        // Si sendToShipmentVM.StationId == 0 => Se manda a envios todos los puestos para ese woi (cada wa de este woi representa a
                                        // una station, entonces se setean todas las wa (toShipments = true) de ese woi).
                                        if (sendToShipmentVM.StationId == 0)
                                        {
                                            waVM.ToShipments = true;

                                            waVM.OutsourcedProviderId = sendToShipmentVM.ProviderId;

                                            var waDTO = _mapper.Map<WorkActivityDTO>(waVM);
                                            var updateResult = await _workActivityViewManager.UpdateAsync(waDTO);
                                            if (updateResult.Succeeded)
                                            {
                                                updateSuccesfully = true;
                                            }

                                            // Tengo que iniciar la activadad para que quede en proceso
                                            //await startAction(waVM.Id);
                                        }
                                        else
                                        {
                                            // Si sendToShipmentVM.StationId != 0 => Hay que ver de que puesto se trata y setear la wa de ese woi para solo ese puesto
                                            // (toShipments = true para la wa que tiene el puesto seleccionado)
                                            if (sendToShipmentVM.StationId == waVM.ProductStation.StationId)
                                            {
                                                waVM.ToShipments = true;

                                                waVM.OutsourcedProviderId = sendToShipmentVM.ProviderId;

                                                stationDescription = waVM.ProductStation.Station.AbreviationDescription.ToString();

                                                var waDTO = _mapper.Map<WorkActivityDTO>(waVM);
                                                var updateResult = await _workActivityViewManager.UpdateAsync(waDTO);
                                                if (updateResult.Succeeded)
                                                {
                                                    updateSuccesfully = true;
                                                }

                                                // Tengo que iniciar la activadad para que quede en proceso
                                                //await startAction(waVM.Id);
                                            }
                                        }
                                    }
                                }
                            }

                            if (updateSuccesfully)
                            {
                                _notify.Success(_localizer["The selected activities were transferred to the shipments section for:"].ToString() + " " + stationDescription);
                                return new JsonResult(new { isValid = true, workOrderId = sendToShipmentVM.WorkOrderId, isSparesOrStock = sendToShipmentVM.isSparesOrStock, workOrderIds = sendToShipmentVM.workOrderIds });

                            }
                            else
                            {
                                _notify.Warning(_localizer["The selected activities could not be moved to the shipments section for:"].ToString() + " " + stationDescription);
                                return new JsonResult(new { isValid = false });
                            }
                        }
                        else
                        {
                            _notify.Warning(_localizer["Please, you must select a provider."].ToString());
                            return new JsonResult(new { isValid = false });
                        }
                    }
                    else
                    {
                        _notify.Warning(_localizer["Please, you must select a work station."].ToString());
                        return new JsonResult(new { isValid = false });
                    }

                }
                else
                {
                    _notify.Warning(_localizer["The selected activities could not be moved to the shipments section for:"].ToString());
                    return new JsonResult(new { isValid = false });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return new JsonResult(new { isValid = false });
            }
        }

        public async Task<JsonResult> LoadDeliveryNoteNumbers(string search, int providerId, int transportProviderId)
        {

            var deliveryNoteHeaders = await _deliveryNoteHeaderViewManager.FindBySpecification(new DNHByProviderAndTransportSpecification(providerId, transportProviderId));

            List<Select2ViewModel> mapDeliveryNoteNumbersOnSelect2 = new List<Select2ViewModel>();
            var dnhViewModel = _mapper.Map<List<DeliveryNoteHeaderViewModel>>(deliveryNoteHeaders.Data);

            foreach (var dnh in dnhViewModel)
            {
                mapDeliveryNoteNumbersOnSelect2.Add(new Select2ViewModel() { Id = dnh.Id, Text = dnh.Number });
            }

            if (!(string.IsNullOrEmpty(search) || string.IsNullOrWhiteSpace(search)))
            {
                mapDeliveryNoteNumbersOnSelect2 = mapDeliveryNoteNumbersOnSelect2.Where(x => x.Text.ToLower().StartsWith(search.ToLower())).ToList();
            }
            return new JsonResult(new { deliveryNoteNumbers = mapDeliveryNoteNumbersOnSelect2 });

        }

        public async Task<JsonResult> MergeDeliveryNoteByHeaderId(int dnhId, int countRows)
        {
            try
            {
                var getDNHById = await _deliveryNoteHeaderViewManager.FindBySpecification(new DNHSpecification(dnhId));
                if (getDNHById.Succeeded)
                {
                    var dnhVM = _mapper.Map<DeliveryNoteHeaderViewModel>(getDNHById.Data.First());
                    if (!dnhVM.wasExportedToPDF)
                    {
                        //if ((countRows + dnhVM.DeliveryNoteDetails.Count()) > 25)
                        //{
                        //    _notify.Warning("Lo siento, no se pueden agregar mas ítems a este remito porque supera el límite de filas.");
                        //    return new JsonResult(new { isValid = false });
                        //}
                        //else
                        //{
                        var htmlToReturn = "";

                        foreach (var dnd in dnhVM.DeliveryNoteDetails)
                        {
                            htmlToReturn += buildHtmlForTable(dnd);
                        }

                        if (htmlToReturn != "")
                        {
                            _notify.Success("Los ítems de remito seleccionado fueron agregados.");
                            return new JsonResult(new { isValid = true, html = htmlToReturn });
                        }
                        else
                        {
                            return new JsonResult(new { isValid = false });
                        }
                        //}
                    }
                    else
                    {
                        _notify.Warning("Lo siento, no se pueden agregar ítems a este remito porque ya fue generado su PDF correspondiente.");
                        return new JsonResult(new { isValid = false });
                    }
                }
                else
                {
                    _notify.Error("No se pudo agregar el remito seleccionado.");
                    return new JsonResult(new { isValid = false });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return new JsonResult(new { isValid = false });
            }
        }

        public string buildHtmlForTable(DeliveryNoteDetailViewModel deliveryNoteDetail)
        {
            var trForTable = "<tr>";

            trForTable += "<td class=" + '"' + "d-none" + '"' + ">";
            trForTable += "<input type=" + '"' + "hidden" + '"' + " name=" + '"' + "DeliveryNoteDetails.Index" + '"' + "value=" + '"' + deliveryNoteDetail.WorkActivity.Id + '"' + "/>";
            trForTable += "<input type=" + '"' + "hidden" + '"' + " name=" + '"' + "DeliveryNoteDetails[" + deliveryNoteDetail.WorkActivity.Id + "].Id" + '"' + "readonly value=" + '"' + 0 + '"' + "/>";
            trForTable += "<input type=" + '"' + "hidden" + '"' + " name=" + '"' + "DeliveryNoteDetails[" + deliveryNoteDetail.WorkActivity.Id + "].ConcurrencyToken" + '"' + "readonly value=" + '"' + 0 + '"' + "/>";
            trForTable += "<input type=" + '"' + "hidden" + '"' + " name=" + '"' + "DeliveryNoteDetails[" + deliveryNoteDetail.WorkActivity.Id + "].DeliveryNoteHeaderId" + '"' + "readonly value=" + '"' + 0 + '"' + "/>";
            trForTable += "<input type=" + '"' + "hidden" + '"' + " name=" + '"' + "DeliveryNoteDetails[" + deliveryNoteDetail.WorkActivity.Id + "].ProductItemId" + '"' + "readonly value=" + '"' + deliveryNoteDetail.WorkActivity.WorkOrderItem.Product.Id + '"' + "/>";
            trForTable += "<input type=" + '"' + "hidden" + '"' + " name=" + '"' + "DeliveryNoteDetails[" + deliveryNoteDetail.WorkActivity.Id + "].ProductId" + '"' + "readonly value=" + '"' + deliveryNoteDetail.WorkActivity.WorkOrderItem.WorkOrder.OrderDetail.Product.Id + '"' + "/>";
            trForTable += "<input type=" + '"' + "hidden" + '"' + " name=" + '"' + "DeliveryNoteDetails[" + deliveryNoteDetail.WorkActivity.Id + "].ConfigurationId" + '"' + "readonly value=" + '"' + deliveryNoteDetail.WorkActivity.WorkOrderItem.WorkOrder.OrderDetail.Structure.Id + '"' + "/>";
            trForTable += "<input type=" + '"' + "hidden" + '"' + " name=" + '"' + "DeliveryNoteDetails[" + deliveryNoteDetail.WorkActivity.Id + "].WorkActivityId" + '"' + "readonly value=" + '"' + deliveryNoteDetail.WorkActivity.Id + '"' + "/>";
            trForTable += "</td>";

            trForTable += "<td>";
            trForTable += "<input type=" + '"' + "text" + '"' + "class=" + '"' + "custom-form-control form-control" + '"' + "name=" + '"' + "DeliveryNoteDetails[" + deliveryNoteDetail.WorkActivity.Id + "].ProductItemCode" + '"' + "readonly value=" + '"' + deliveryNoteDetail.WorkActivity.WorkOrderItem.Product.Code + '"' + "/>";
            trForTable += "</td>";

            trForTable += "<td>";
            trForTable += "<input type=" + '"' + "text" + '"' + "class=" + '"' + "custom-form-control form-control" + '"' + "name=" + '"' + "DeliveryNoteDetails[" + deliveryNoteDetail.WorkActivity.Id + "].ProductItemDescription" + '"' + "readonly value=" + '"' + deliveryNoteDetail.WorkActivity.WorkOrderItem.Product.Description + '"' + "/>";
            trForTable += "</td>";

            trForTable += "<td>";
            trForTable += "<input type=" + '"' + "text" + '"' + "class=" + '"' + "custom-form-control form-control text-center" + '"' + "name=" + '"' + "DeliveryNoteDetails[" + deliveryNoteDetail.WorkActivity.Id + "].Quantity" + '"' + "readonly value=" + '"' + deliveryNoteDetail.Quantity + '"' + "/>";
            trForTable += "</td>";

            trForTable += "<td>";
            trForTable += "<input type=" + '"' + "number" + '"' + "class=" + '"' + "inputPackage form-control text-center" + '"' + "name=" + '"' + "DeliveryNoteDetails[" + deliveryNoteDetail.WorkActivity.Id + "].Package" + '"' + "value=" + '"' + deliveryNoteDetail.Package + '"' + "/>";
            trForTable += "</td>";

            trForTable += "<td>";
            trForTable += "<input type=" + '"' + "text" + '"' + "class=" + '"' + "inputPackageWeight form-control text-center" + '"' + "name=" + '"' + "DeliveryNoteDetails[" + deliveryNoteDetail.WorkActivity.Id + "].PackageWeight" + '"' + "value=" + '"' + Math.Round(deliveryNoteDetail.PackageWeight, 2) + '"' + " onkeypress=" + '"' + "return validateIsNumber(event);" + '"' + "/>";
            trForTable += "</td>";

            trForTable += "<td>";
            trForTable += "<input type=" + '"' + "text" + '"' + "class=" + '"' + "custom-form-control form-control text-center" + '"' + "name=" + '"' + "DeliveryNoteDetails[" + deliveryNoteDetail.WorkActivity.Id + "].WorkOrderHeaderNumber" + '"' + "readonly value=" + '"' + deliveryNoteDetail.WorkActivity.WorkOrderItem.WorkOrder.WorkOrderHeader.WorkOrderHeaderNumber + '"' + "/>";
            trForTable += "</td>";

            trForTable += "<td>";
            trForTable += "<input type=" + '"' + "text" + '"' + "class=" + '"' + "custom-form-control form-control" + '"' + "name=" + '"' + "DeliveryNoteDetails[" + deliveryNoteDetail.WorkActivity.Id + "].ProductNumber" + '"' + "readonly value=" + '"' + deliveryNoteDetail.WorkActivity.WorkOrderItem.WorkOrder.OrderDetail.ProductNumber + '"' + "/>";
            trForTable += "</td>";

            trForTable += "<td>";
            trForTable += "<input type=" + '"' + "text" + '"' + "class=" + '"' + "custom-form-control form-control" + '"' + "name=" + '"' + "DeliveryNoteDetails[" + deliveryNoteDetail.WorkActivity.Id + "].Product" + '"' + "readonly value=" + '"' + deliveryNoteDetail.WorkActivity.WorkOrderItem.WorkOrder.OrderDetail.Product.CodeAndDescription + '"' + "/>";
            trForTable += "</td>";

            trForTable += "<td>";
            trForTable += "<input type=" + '"' + "text" + '"' + "class=" + '"' + "custom-form-control form-control" + '"' + "name=" + '"' + "DeliveryNoteDetails[" + deliveryNoteDetail.WorkActivity.Id + "].Configuration" + '"' + "readonly value=" + '"' + deliveryNoteDetail.WorkActivity.WorkOrderItem.WorkOrder.OrderDetail.Structure.Description + '"' + "/>";
            trForTable += "</td>";


            trForTable += "</tr>";

            return trForTable;
        }

        public async Task<bool> startAction(int workActivityId)
        {
            try
            {
                var viewModel = new WorkActionViewModel();
                viewModel.WorkActivityId = workActivityId;
                viewModel.ActionName = "play";
                viewModel.StartDate = DateTime.Now;
                var responseAction = await _workActionViewManager.CreateAsync(_mapper.Map<WorkActionDTO>(viewModel));
                if (responseAction.Succeeded)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _notify.Error($"{_localizer["Error"]}: {ex.Message}.");
                return false;
            }
        }

    }

}
