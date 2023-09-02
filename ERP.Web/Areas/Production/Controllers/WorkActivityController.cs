using ERP.Application.Constants;
using ERP.Application.DTOs.Entities.Production;
using ERP.Application.Enums;
using ERP.Application.Interfaces.ViewManagers.Production;
using ERP.Application.Interfaces.ViewManagers.ProductMod;
using ERP.Application.Specification.Production.GroupedWorkActionSpecifications;
using ERP.Application.Specification.Production.WorkActivitySpecifications;
using ERP.Application.Specification.Production.WorkOrderItemSpecifications;
using ERP.Application.Specification.Production.WorkOrderSpecifications;
using ERP.Infrastructure.Identity.Models;
using ERP.Language;
using ERP.Web.Abstractions;
using ERP.Web.Areas.Production.Models;
using ERP.Web.Areas.Production.Models.WorkAction;
using ERP.Web.Areas.Production.Models.WorkActivity;
using ERP.Web.Areas.ProductMod.Models;
using ERP.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Web.Areas.Production.Controllers
{

    public static class Stations
    {
        // Valores que se guardan en el puesto, WorkOrderDisplayType
        public const string Serrucho = "Serrucho";
        public const string Laser = "Laser";
        public const string Torno = "Torno";
        public const string Soldadura = "Soldadura";
        public const string Montaje = "Montaje";
        public const string Envios = "Envios";
        public const string Abastecimientos = "Abastecimientos";
    }

    [Area("Production")]
    public class WorkActivityController : BaseController<WorkActivityController>
    {
        private readonly IWorkActivityViewManager _viewManager;
        private readonly IWorkOrderViewManager _workOrderViewManager;
        private readonly IStationViewManager _stationViewManager;
        private readonly IWorkActionViewManager _workActionViewManager;
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly IGroupedWorkActionViewManager _groupedWorkActionViewManager;
        private readonly IWorkActivityViewManager _workActivityViewManager;
        private readonly IWorkOrderItemViewManager _workOrderItemViewManager;
        private readonly IProductViewManager _productViewManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public WorkActivityController(IWorkActivityViewManager viewManager, IWorkOrderViewManager workOrderViewManager, IStationViewManager stationViewManager, IWorkActionViewManager workActionViewManager, IStringLocalizer<SharedResource> localizer, IGroupedWorkActionViewManager groupedWorkActionViewManager, IWorkActivityViewManager workActivityViewManager, IWorkOrderItemViewManager workOrderItemViewManager, IProductViewManager productViewManager, UserManager<ApplicationUser> userManager)
        {
            _viewManager = viewManager;
            _workOrderViewManager = workOrderViewManager;
            _stationViewManager = stationViewManager;
            _workActionViewManager = workActionViewManager;
            _localizer = localizer;
            _groupedWorkActionViewManager = groupedWorkActionViewManager;
            _workActivityViewManager = workActivityViewManager;
            _workOrderItemViewManager = workOrderItemViewManager;
            _productViewManager = productViewManager;
            _userManager = userManager;
        }

        // Se llama desde el "ojito" en el menu de "Ordenes de trabajo"
        public async Task<IActionResult> Index(string workOrderIds, int stationId)
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

                var responseactivities = await _viewManager.FindBySpecification(new ActivitiesByWorkOrderAndStationSpecification(Convert.ToInt32(id), stationId));
                if (responseactivities.Succeeded)
                {
                    var activities = _mapper.Map<List<WorkActivityViewModel>>(responseactivities.Data);

                    model.WorkActivities.AddRange(activities);
                }

                model.ActivitiesGroups = generateActivitiesGroups(model.WorkActivities, model.Station);

            }

            return View(model);
        }

        public async Task<IActionResult> LoadFiltered(string workOrderIds, int stationId)
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

                    var responseactivities = await _viewManager.FindBySpecification(new ActivitiesByWorkOrderAndStationSpecification(Convert.ToInt32(id), stationId));
                    if (responseactivities.Succeeded)
                    {
                        var activities = _mapper.Map<List<WorkActivityViewModel>>(responseactivities.Data);

                        // No se muestran las que fueron a envio externo
                        activities = activities.Where(wa => wa.ToShipments.HasValue ? wa.ToShipments.Value == false : wa.ToShipments == null).ToList();

                        // Filtrar la informacion según de que tipo de planilla se trate.
                        activities = await filterWAInformationByStock(activities, stationViewModel);

                        // OT Torno
                        if (stationViewModel.WorkOrderDisplayType == Stations.Torno)
                        {
                            removeChilds(activities);
                            activities = activities.OrderByDescending(activity => activity.haveTornoInRoute).ThenByDescending(activity => activity.haveCincadoInRoute).ThenByDescending(activity => activity.havePinturaInRoute).ToList();
                        }

                        // OT Soldadura, OT Montaje
                        if (stationViewModel.WorkOrderDisplayType == Stations.Soldadura || stationViewModel.WorkOrderDisplayType == Stations.Montaje)
                        {
                            removeChilds(activities);
                            activities = activities.OrderByDescending(activity => activity.haveCincadoInRoute).ThenByDescending(activity => activity.havePinturaInRoute).ToList();
                            foreach (var item in activities)
                            {
                                await getDescendents(item);
                            }
                        }

                        viewmodel.WorkActivities.AddRange(activities);
                    }
                }

                viewmodel.ActivitiesGroups = generateActivitiesGroups(viewmodel.WorkActivities, stationViewModel);
                viewmodel.Station = stationViewModel;

                switch (stationViewModel.WorkOrderDisplayType)
                {
                    case Stations.Serrucho:
                        return PartialView("WorkActivitiesByStationSerrucho", viewmodel);
                    case Stations.Laser:
                        return PartialView("WorkActivitiesByStationLaser", viewmodel);
                    case Stations.Torno:
                        return PartialView("WorkActivitiesByStationTorno", viewmodel);
                    case Stations.Soldadura:
                        return PartialView("WorkActivitiesByStationSoldaduraNew", viewmodel);
                    case Stations.Montaje:
                        return PartialView("WorkActivitiesByStationSoldaduraNew", viewmodel);
                    default:
                        return PartialView("WorkActivitiesByStationLaser", viewmodel);
                }

            }
            catch (Exception ex)
            {
                _notify.Error($"{_localizer["Error"]}: {ex.Message}.");
                return new JsonResult(new { isValid = false });
            }
        }

        public async Task<IActionResult> IndexGroupedAsync()
        {
            ViewData["Title"] = _localizer["Production"].ToString() + " - " + _localizer["Work orders"].ToString();
            var model = new WorkActivityViewModel();
            model.currentUser = await _userManager.GetUserAsync(HttpContext.User);
            return View(model);
        }

        public IActionResult IndexByStation(string strStation, int idStation)
        {
            var model = new WorkActivityViewModel();
            model.FilteredStation = strStation;
            model.IdFilteredStation = idStation;
            return View(model);
        }
        public async Task<IActionResult> LoadAll()
        {
            var response = await _viewManager.LoadAll();
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<WorkActivityViewModel>>(response.Data);
                return PartialView("WorkActivitiesByStation", viewModel);
            }
            return null;
        }

        public async Task<IActionResult> IndexWAInProcess(string workOrderIds, int stationId)
        {
            var model = new WorkActivitiesViewModel();
            var workOrderIdList = workOrderIds.Split(',');
            foreach (var id in workOrderIdList)
            {
                var responseworkorder = await _workOrderViewManager.FindBySpecification(new WorkOrderWithHeaderSpecification(Convert.ToInt32(id)));
                if (responseworkorder.Succeeded)
                {
                    model.WorkOrders.Add(_mapper.Map<WorkOrderViewModel>(responseworkorder.Data.FirstOrDefault()));
                }
            }
            var responsestation = await _stationViewManager.GetByIdAsync(stationId);
            if (responsestation.Succeeded)
            {
                model.Station = _mapper.Map<StationViewModel>(responsestation.Data);
            }
            return View(model);
        }

        public async Task<IActionResult> WorkActivitiesInProcess(string workOrderIds, int stationId)
        {
            try
            {
                var workOrderIdList = workOrderIds.Split(',');

                var responseStation = await _stationViewManager.GetByIdAsync(stationId);
                var stationViewModel = new StationViewModel();
                if (responseStation.Succeeded)
                {
                    stationViewModel = _mapper.Map<StationViewModel>(responseStation.Data);
                }

                WorkActivitiesViewModel viewmodel = new WorkActivitiesViewModel();
                foreach (var id in workOrderIdList)
                {
                    var responseworkorder = await _workOrderViewManager.FindBySpecification(new WorkOrderWithHeaderSpecification(Convert.ToInt32(id)));
                    if (responseworkorder.Succeeded)
                    {
                        viewmodel.WorkOrders.Add(_mapper.Map<WorkOrderViewModel>(responseworkorder.Data.FirstOrDefault()));
                    }

                    var responseactivities = await _viewManager.FindBySpecification(new ActivitiesByWorkOrderAndStationSpecification(Convert.ToInt32(id), stationId));
                    if (responseactivities.Succeeded)
                    {
                        var activities = _mapper.Map<List<WorkActivityViewModel>>(responseactivities.Data);

                        // Filtro todas las WorkActivities que se hayan iniciado. (Son las que se van seleccionando y se les asigna un ActionName = "play")
                        activities = activities.Where(act => act.hasActionPlay && !act.hasActionStop).ToList();

                        if (stationViewModel.Description == Stations.Torno)
                        {
                            removeChilds(activities);
                            activities = activities.OrderByDescending(activity => activity.haveTornoInRoute).ThenByDescending(activity => activity.haveCincadoInRoute).ThenByDescending(activity => activity.havePinturaInRoute).ToList();
                            foreach (var item in activities)
                            {
                                await getDescendents(item);
                            }
                        }

                        viewmodel.WorkActivities.AddRange(activities);
                    }
                }
                viewmodel.ActivitiesGroups = generateActivitiesGroups(viewmodel.WorkActivities, stationViewModel);
                viewmodel.Station = stationViewModel;

                var groupedActionsVM = new List<GroupedWorkActionViewModel>();
                var getGroupedWorkActions = await _groupedWorkActionViewManager.FindBySpecification(new GetGroupedWorkActionsSpecification(stationId));
                if (getGroupedWorkActions.Succeeded)
                {
                    groupedActionsVM = _mapper.Map<List<GroupedWorkActionViewModel>>(getGroupedWorkActions.Data);
                }

                foreach (var activitygroup in viewmodel.ActivitiesGroups)
                {
                    foreach (var activity in activitygroup.Activities)
                    {
                        if (groupedActionsVM.Any(ga => ga.WorkActivitiesIds.Contains(activity.Id.ToString())))
                        {
                            foreach (var gact in groupedActionsVM.Where(ga => ga.WorkActivitiesIds.Contains(activity.Id.ToString())).Select(ga => ga.WorkActivitiesIds).ToList())
                            {
                                activity.groupedValue = gact.ToString();
                            }
                        }
                    }
                }

                return PartialView("WorkActivitiesInProcess", viewmodel);

            }
            catch (Exception ex)
            {
                _notify.Error($"{_localizer["Error"]}: {ex.Message}.");
                return new JsonResult(new { isValid = false });
            }
        }

        public void removeChilds(List<WorkActivityViewModel> allActivities)
        {
            var workOrderItemsIds = allActivities.Select(activity => activity.WorkOrderItem.Id);
            var activitiesToRemove = new List<WorkActivityViewModel>();
            foreach (var activity in allActivities)
            {
                if (activity.WorkOrderItem.WorkOrderItemOfId != null && workOrderItemsIds.Any(id => id == activity.WorkOrderItem.WorkOrderItemOfId))
                {
                    allActivities.Where(father => father.WorkOrderItemId == activity.WorkOrderItem.WorkOrderItemOfId).FirstOrDefault().ChildsActivities.Add(activity);
                    activitiesToRemove.Add(activity);
                }
            }
            foreach (var activityToRemove in activitiesToRemove)
            {
                allActivities.Remove(activityToRemove);
            }
        }

        public List<WorkActivityGroupViewModel> generateActivitiesGroups(List<WorkActivityViewModel> activities, StationViewModel station)
        {

            var groupsList = new List<WorkActivityGroupViewModel>();
            switch (station.WorkOrderDisplayType)
            {
                case Stations.Serrucho:
                    groupsList = activities.GroupBy(x => x.WorkOrderItem.Product.ProductFeature.RawMaterialCode).Select(grp => new WorkActivityGroupViewModel
                    {
                        Activities = grp.Select(x => x).ToList(),
                        SubGroups = grp.Select(x => x).ToList().GroupBy(x => x.WorkOrderItem.Product.ProductFeature.ComponentLongPiece).Select(subgrp => new WorkActivitySubGroupViewModel
                        {
                            Quantity = subgrp.Sum(x => x.WorkOrderItem.Quantity),
                            Activities = subgrp.Select(x => x).ToList(),
                            SubGroups = subgrp.Select(x => x).ToList().GroupBy(x => x.WorkOrderItem.Product.Code).Select(subsubgrp => new WorkActivitySubGroupViewModel
                            {
                                Quantity = subsubgrp.Sum(x => x.WorkOrderItem.Quantity),
                                Activities = subsubgrp.Select(x => x).ToList(),
                            }).ToList()
                        }).ToList()
                    }).ToList();
                    break;

                case Stations.Laser:
                    groupsList = activities.GroupBy(x => x.WorkOrderItem.Product.ProductFeature.RawMaterialCode).Select(grp => new WorkActivityGroupViewModel
                    {
                        Activities = grp.Select(x => x).ToList(),
                        SubGroups = grp.Select(x => x).ToList().GroupBy(x => x.WorkOrderItem.Product.Code).Select(subgrp => new WorkActivitySubGroupViewModel
                        {
                            Quantity = subgrp.Sum(x => x.WorkOrderItem.Quantity),
                            Activities = subgrp.Select(x => x).ToList(),
                        }).ToList()
                    }).ToList();
                    break;

                case Stations.Torno:
                    groupsList = activities.GroupBy(x => x.WorkOrderItem.Product.Code).Select(grp => new WorkActivityGroupViewModel
                    {
                        Quantity = grp.Sum(x => x.WorkOrderItem.Quantity),
                        Activities = grp.Select(x => x).ToList(),
                    }).ToList();
                    break;

                case Stations.Soldadura:
                    //groupsList = activities.GroupBy(x => x.WorkOrderItem.Product.Code).Select(grp => new WorkActivityGroupViewModel
                    groupsList = activities.GroupBy(x => new { x.WorkOrderItem.StructureId, x.WorkOrderItem.Product.Code }).Select(grp => new WorkActivityGroupViewModel
                    {
                        Quantity = grp.Sum(x => x.WorkOrderItem.Quantity),
                        Activities = grp.Select(x => x).ToList(),
                    }).ToList();
                    break;
                case Stations.Montaje:
                    //groupsList = activities.GroupBy(x => x.WorkOrderItem.Product.Code).Select(grp => new WorkActivityGroupViewModel  
                    groupsList = activities.GroupBy(x => new { x.WorkOrderItem.StructureId, x.WorkOrderItem.Product.Code }).Select(grp => new WorkActivityGroupViewModel
                    {
                        Quantity = grp.Sum(x => x.WorkOrderItem.Quantity),
                        Activities = grp.Select(x => x).ToList(),
                    }).ToList();
                    break;
                default:
                    groupsList = activities.GroupBy(x => x.WorkOrderItem.Product.ProductFeature.RawMaterialCode).Select(grp => new WorkActivityGroupViewModel
                    {
                        Activities = grp.Select(x => x).ToList(),
                        SubGroups = grp.Select(x => x).ToList().GroupBy(x => x.WorkOrderItem.Product.Code).Select(subgrp => new WorkActivitySubGroupViewModel
                        {
                            Quantity = subgrp.Sum(x => x.WorkOrderItem.Quantity),
                            Activities = subgrp.Select(x => x).ToList(),
                        }).ToList()
                    }).ToList();
                    break;
            }

            if (station.WorkOrderDisplayType == Stations.Torno)
            {
                return groupsList;
            }
            else
            {
                return validatePreviousStation(groupsList, station.WorkOrderDisplayType).Result;
            }
        }

        public async Task<List<WorkActivityGroupViewModel>> validatePreviousStation(List<WorkActivityGroupViewModel> groupsList, string WorkOrderDisplayType)
        {
            // OT Torno: Se valida el puesto anterior cuando se seleccionan con el check y se envían a producir. Metodo : validatePreviousStationForTorno 
            switch (WorkOrderDisplayType)
            {
                case Stations.Serrucho:
                    foreach (var workActivityGroupVM in groupsList)
                    {
                        foreach (var subGroup in workActivityGroupVM.SubGroups)
                        {
                            foreach (var subSubGroup in subGroup.SubGroups)
                            {
                                // Fran 15/6/2023: Se tienen en cuenta las estaciones de "Abastecimientos".
                                var getActivitiesByWoiId = await _workActivityViewManager.FindBySpecification(new WorkActivitiesByWorkOrderItemIdSpecification(subSubGroup.Activities.First().WorkOrderItemId));
                                if (getActivitiesByWoiId.Succeeded)
                                {
                                    var activitiesByWoi = _mapper.Map<List<WorkActivityViewModel>>(getActivitiesByWoiId.Data);
                                    activitiesByWoi = activitiesByWoi.OrderBy(a => a.ProductStation.Order).ToList();
                                    foreach (var act in activitiesByWoi)
                                    {

                                        if (!subSubGroup.Activities.First().isStarted)
                                        {
                                            if (subSubGroup.Activities.First().ProductStation.Order != null)
                                            {
                                                if ((subSubGroup.Activities.First().ProductStation.Order.Value - act.ProductStation.Order.Value) == 1)
                                                {
                                                    if (!act.isStarted && !act.isFinished)
                                                    {
                                                        subSubGroup.Activities.First().DisablePlayBtn = "disabled";
                                                        subSubGroup.Activities.First().DisablePauseBtn = "disabled";
                                                        subSubGroup.Activities.First().DisableStopBtn = "disabled";
                                                        break;
                                                    }
                                                    else if (act.isStarted && act.isFinished)
                                                    {
                                                        subSubGroup.Activities.First().DisablePlayBtn = "";
                                                        subSubGroup.Activities.First().DisablePauseBtn = "disabled";
                                                        subSubGroup.Activities.First().DisableStopBtn = "disabled";
                                                        break;
                                                    }
                                                    else if (act.isStarted && !act.isFinished)
                                                    {
                                                        subSubGroup.Activities.First().DisablePlayBtn = "disabled";
                                                        subSubGroup.Activities.First().DisablePauseBtn = "disabled";
                                                        subSubGroup.Activities.First().DisableStopBtn = "disabled";
                                                        break;
                                                    }
                                                }
                                                else if (subSubGroup.Activities.First().ProductStation.Order.Value == 1)
                                                {
                                                    subSubGroup.Activities.First().DisablePlayBtn = "";
                                                    subSubGroup.Activities.First().DisablePauseBtn = "disabled";
                                                    subSubGroup.Activities.First().DisableStopBtn = "disabled";
                                                    break;
                                                }
                                                else
                                                {
                                                    subSubGroup.Activities.First().DisablePlayBtn = "";
                                                    subSubGroup.Activities.First().DisablePauseBtn = "disabled";
                                                    subSubGroup.Activities.First().DisableStopBtn = "disabled";
                                                    break;
                                                }
                                            }
                                            else
                                            {
                                                subSubGroup.Activities.First().DisablePlayBtn = "disabled";
                                                subSubGroup.Activities.First().DisablePauseBtn = "disabled";
                                                subSubGroup.Activities.First().DisableStopBtn = "disabled";
                                                break;
                                            }
                                        }

                                    }
                                }

                            }
                        }
                    }
                    break;
                case Stations.Laser:
                    foreach (var workActivityGroupVM in groupsList)
                    {
                        foreach (var subGroup in workActivityGroupVM.SubGroups)
                        {
                            // Fran 15/6/2023: Se tienen en cuenta las estaciones de "Abastecimientos".
                            var getActivitiesByWoiId = await _workActivityViewManager.FindBySpecification(new WorkActivitiesByWorkOrderItemIdSpecification(subGroup.Activities.First().WorkOrderItemId));
                            if (getActivitiesByWoiId.Succeeded)
                            {
                                var activitiesByWoi = _mapper.Map<List<WorkActivityViewModel>>(getActivitiesByWoiId.Data);
                                activitiesByWoi = activitiesByWoi.OrderBy(a => a.ProductStation.Order).ToList();
                                foreach (var act in activitiesByWoi)
                                {
                                    if (!subGroup.Activities.First().isStarted)
                                    {

                                        if (subGroup.Activities.First().ProductStation.Order != null)
                                        {
                                            if ((subGroup.Activities.First().ProductStation.Order.Value - act.ProductStation.Order.Value) == 1)
                                            {
                                                if (!act.isStarted && !act.isFinished)
                                                {
                                                    subGroup.Activities.First().DisablePlayBtn = "disabled";
                                                    subGroup.Activities.First().DisablePauseBtn = "disabled";
                                                    subGroup.Activities.First().DisableStopBtn = "disabled";
                                                    break;
                                                }
                                                else if (act.isStarted && act.isFinished)
                                                {
                                                    subGroup.Activities.First().DisablePlayBtn = "";
                                                    subGroup.Activities.First().DisablePauseBtn = "disabled";
                                                    subGroup.Activities.First().DisableStopBtn = "disabled";
                                                    break;
                                                }
                                                else if (act.isStarted && !act.isFinished)
                                                {
                                                    subGroup.Activities.First().DisablePlayBtn = "disabled";
                                                    subGroup.Activities.First().DisablePauseBtn = "disabled";
                                                    subGroup.Activities.First().DisableStopBtn = "disabled";
                                                    break;
                                                }
                                            }
                                            else if (subGroup.Activities.First().ProductStation.Order.Value == 1)
                                            {
                                                subGroup.Activities.First().DisablePlayBtn = "";
                                                subGroup.Activities.First().DisablePauseBtn = "disabled";
                                                subGroup.Activities.First().DisableStopBtn = "disabled";
                                                break;
                                            }
                                            else
                                            {
                                                subGroup.Activities.First().DisablePlayBtn = "";
                                                subGroup.Activities.First().DisablePauseBtn = "disabled";
                                                subGroup.Activities.First().DisableStopBtn = "disabled";
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            subGroup.Activities.First().DisablePlayBtn = "disabled";
                                            subGroup.Activities.First().DisablePauseBtn = "disabled";
                                            subGroup.Activities.First().DisableStopBtn = "disabled";
                                            break;
                                        }
                                    }
                                }
                            }

                        }
                    }
                    break;
                case Stations.Soldadura:
                    foreach (var workActivityGroupVM in groupsList)
                    {
                        if (workActivityGroupVM.AllActivities.Count > 1)
                        {
                            await validatePreviousStationRecursive(workActivityGroupVM);
                        }
                        else
                        {
                            // Fran 15/6/2023: Se tienen en cuenta las estaciones de "Abastecimientos".
                            var getActivitiesByWoiId = await _workActivityViewManager.FindBySpecification(new WorkActivitiesByWorkOrderItemIdSpecification(workActivityGroupVM.Activities.First().WorkOrderItemId));
                            if (getActivitiesByWoiId.Succeeded)
                            {
                                var activitiesByWoi = _mapper.Map<List<WorkActivityViewModel>>(getActivitiesByWoiId.Data);
                                activitiesByWoi = activitiesByWoi.OrderBy(a => a.ProductStation.Order).ToList();
                                foreach (var act in activitiesByWoi)
                                {
                                    if (!workActivityGroupVM.Activities.First().isStarted)
                                    {
                                        if (workActivityGroupVM.Activities.First().ProductStation.Order != null && workActivityGroupVM.Activities.First().ProductStation.Order.HasValue)
                                        {
                                            if ((workActivityGroupVM.Activities.First().ProductStation.Order.Value - act.ProductStation.Order.Value) >= 1)
                                            {
                                                if (act.isFinished)
                                                {
                                                    workActivityGroupVM.Activities.First().DisablePlayBtn = "";
                                                    workActivityGroupVM.Activities.First().DisablePauseBtn = "disabled";
                                                    workActivityGroupVM.Activities.First().DisableStopBtn = "disabled";
                                                }
                                                else
                                                {
                                                    workActivityGroupVM.Activities.First().DisablePlayBtn = "disabled";
                                                    workActivityGroupVM.Activities.First().DisablePauseBtn = "disabled";
                                                    workActivityGroupVM.Activities.First().DisableStopBtn = "disabled";
                                                    break;
                                                }
                                            }
                                            else if ((workActivityGroupVM.Activities.First().ProductStation.Order.Value - act.ProductStation.Order.Value) == 0)
                                            {
                                                workActivityGroupVM.Activities.First().DisablePlayBtn = "";
                                                workActivityGroupVM.Activities.First().DisablePauseBtn = "disabled";
                                                workActivityGroupVM.Activities.First().DisableStopBtn = "disabled";
                                            }
                                            else if (workActivityGroupVM.Activities.First().ProductStation.Order.Value == 1)
                                            {
                                                if (act.isFinished)
                                                {
                                                    workActivityGroupVM.Activities.First().DisablePlayBtn = "";
                                                    workActivityGroupVM.Activities.First().DisablePauseBtn = "disabled";
                                                    workActivityGroupVM.Activities.First().DisableStopBtn = "disabled";
                                                }
                                                else
                                                {
                                                    workActivityGroupVM.Activities.First().DisablePlayBtn = "disabled";
                                                    workActivityGroupVM.Activities.First().DisablePauseBtn = "disabled";
                                                    workActivityGroupVM.Activities.First().DisableStopBtn = "disabled";
                                                    break;
                                                }
                                            }
                                            else if ((act.ProductStation.Order.Value - workActivityGroupVM.Activities.First().ProductStation.Order.Value) > 0)
                                            {
                                                workActivityGroupVM.Activities.First().DisablePlayBtn = "";
                                                workActivityGroupVM.Activities.First().DisablePauseBtn = "disabled";
                                                workActivityGroupVM.Activities.First().DisableStopBtn = "disabled";
                                                break;
                                            }
                                            else
                                            {
                                                workActivityGroupVM.Activities.First().DisablePlayBtn = "disabled";
                                                workActivityGroupVM.Activities.First().DisablePauseBtn = "disabled";
                                                workActivityGroupVM.Activities.First().DisableStopBtn = "disabled";
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            workActivityGroupVM.Activities.First().DisablePlayBtn = "disabled";
                                            workActivityGroupVM.Activities.First().DisablePauseBtn = "disabled";
                                            workActivityGroupVM.Activities.First().DisableStopBtn = "disabled";
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (workActivityGroupVM.Activities.First().isFinished)
                                        {
                                            workActivityGroupVM.Activities.First().DisablePlayBtn = "disabled";
                                            workActivityGroupVM.Activities.First().DisablePauseBtn = "disabled";
                                            workActivityGroupVM.Activities.First().DisableStopBtn = "disabled";
                                            break;
                                        }
                                        else
                                        {
                                            workActivityGroupVM.Activities.First().DisablePlayBtn = "disabled";
                                            workActivityGroupVM.Activities.First().DisablePauseBtn = "";
                                            workActivityGroupVM.Activities.First().DisableStopBtn = "";
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;
                case Stations.Montaje:
                    foreach (var workActivityGroupVM in groupsList)
                    {
                        if (workActivityGroupVM.AllActivities.Count > 1)
                        {
                            await validatePreviousStationRecursive(workActivityGroupVM);
                        }
                        else
                        {
                            // Fran 15/6/2023: Se tienen en cuenta las estaciones de "Abastecimientos".
                            var getActivitiesByWoiId = await _workActivityViewManager.FindBySpecification(new WorkActivitiesByWorkOrderItemIdSpecification(workActivityGroupVM.Activities.First().WorkOrderItemId, true));
                            if (getActivitiesByWoiId.Succeeded)
                            {
                                var activitiesByWoi = _mapper.Map<List<WorkActivityViewModel>>(getActivitiesByWoiId.Data);
                                activitiesByWoi = activitiesByWoi.OrderBy(a => a.ProductStation.Order).ToList();
                                foreach (var act in activitiesByWoi)
                                {
                                    if (!workActivityGroupVM.Activities.First().isStarted)
                                    {
                                        if (workActivityGroupVM.Activities.First().ProductStation.Order != null && workActivityGroupVM.Activities.First().ProductStation.Order.HasValue)
                                        {
                                            if ((workActivityGroupVM.Activities.First().ProductStation.Order.Value - act.ProductStation.Order.Value) >= 1)
                                            {
                                                if (act.isFinished)
                                                {
                                                    workActivityGroupVM.Activities.First().DisablePlayBtn = "";
                                                    workActivityGroupVM.Activities.First().DisablePauseBtn = "disabled";
                                                    workActivityGroupVM.Activities.First().DisableStopBtn = "disabled";
                                                }
                                                else
                                                {
                                                    workActivityGroupVM.Activities.First().DisablePlayBtn = "disabled";
                                                    workActivityGroupVM.Activities.First().DisablePauseBtn = "disabled";
                                                    workActivityGroupVM.Activities.First().DisableStopBtn = "disabled";
                                                    break;
                                                }
                                            }
                                            else if ((workActivityGroupVM.Activities.First().ProductStation.Order.Value - act.ProductStation.Order.Value) == 0)
                                            {
                                                workActivityGroupVM.Activities.First().DisablePlayBtn = "";
                                                workActivityGroupVM.Activities.First().DisablePauseBtn = "disabled";
                                                workActivityGroupVM.Activities.First().DisableStopBtn = "disabled";
                                            }
                                            else if (workActivityGroupVM.Activities.First().ProductStation.Order.Value == 1)
                                            {
                                                if (act.isFinished)
                                                {
                                                    workActivityGroupVM.Activities.First().DisablePlayBtn = "";
                                                    workActivityGroupVM.Activities.First().DisablePauseBtn = "disabled";
                                                    workActivityGroupVM.Activities.First().DisableStopBtn = "disabled";
                                                }
                                                else
                                                {
                                                    workActivityGroupVM.Activities.First().DisablePlayBtn = "disabled";
                                                    workActivityGroupVM.Activities.First().DisablePauseBtn = "disabled";
                                                    workActivityGroupVM.Activities.First().DisableStopBtn = "disabled";
                                                    break;
                                                }
                                            }
                                            else if ((act.ProductStation.Order.Value - workActivityGroupVM.Activities.First().ProductStation.Order.Value) > 0)
                                            {
                                                workActivityGroupVM.Activities.First().DisablePlayBtn = "";
                                                workActivityGroupVM.Activities.First().DisablePauseBtn = "disabled";
                                                workActivityGroupVM.Activities.First().DisableStopBtn = "disabled";
                                                break;
                                            }
                                            else
                                            {
                                                workActivityGroupVM.Activities.First().DisablePlayBtn = "disabled";
                                                workActivityGroupVM.Activities.First().DisablePauseBtn = "disabled";
                                                workActivityGroupVM.Activities.First().DisableStopBtn = "disabled";
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            workActivityGroupVM.Activities.First().DisablePlayBtn = "disabled";
                                            workActivityGroupVM.Activities.First().DisablePauseBtn = "disabled";
                                            workActivityGroupVM.Activities.First().DisableStopBtn = "disabled";
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (workActivityGroupVM.Activities.First().isFinished)
                                        {
                                            workActivityGroupVM.Activities.First().DisablePlayBtn = "disabled";
                                            workActivityGroupVM.Activities.First().DisablePauseBtn = "disabled";
                                            workActivityGroupVM.Activities.First().DisableStopBtn = "disabled";
                                            break;
                                        }
                                        else
                                        {
                                            workActivityGroupVM.Activities.First().DisablePlayBtn = "disabled";
                                            workActivityGroupVM.Activities.First().DisablePauseBtn = "";
                                            workActivityGroupVM.Activities.First().DisableStopBtn = "";
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;
            }

            return groupsList;
        }

        public async Task<WorkActivityGroupViewModel> validatePreviousStationRecursive(WorkActivityGroupViewModel workActivityGroupVM, List<WorkActivityViewModel> childsActivities = null, bool breakMe = false)
        {
            var listOfActivities = new List<WorkActivityViewModel>();
            if (childsActivities == null)
            {
                //listOfActivities = workActivityGroupVM.Activities.First().ChildsActivities.ToList();
                listOfActivities = workActivityGroupVM.Activities.First().ChildsActivities.Where(childAct => !childAct.WorkOrderItem.Product.ProductFeature.StoredStock).ToList();
            }
            else
            {
                listOfActivities = childsActivities;
            }

            if (listOfActivities.Count > 0)
            {
                foreach (var activityChild in listOfActivities.OrderBy(wa => wa.ChildsActivities.Count).ToList())
                {
                    if (activityChild.ChildsActivities.Count > 0)
                    {
                        await validatePreviousStationRecursive(workActivityGroupVM, activityChild.ChildsActivities.ToList(), breakMe);
                    }
                    else
                    {
                        // Fran 15/6/2023: Se tienen en cuenta las estaciones de "Abastecimientos".
                        var getActivitiesByWoiId = await _workActivityViewManager.FindBySpecification(new WorkActivitiesByWorkOrderItemIdSpecification(activityChild.WorkOrderItemId));
                        if (getActivitiesByWoiId.Succeeded)
                        {
                            var activitiesByWoi = _mapper.Map<List<WorkActivityViewModel>>(getActivitiesByWoiId.Data);
                            activitiesByWoi = activitiesByWoi.OrderBy(a => a.ProductStation.Order).ToList();
                            foreach (var act in activitiesByWoi)
                            {
                                if (!workActivityGroupVM.Activities.First().isStarted)
                                {
                                    var getActivityById = await _workActivityViewManager.FindBySpecification(new WorkActivityWithAllByProductStationIdSpecification(activityChild.Id, activityChild.ProductStationId));
                                    if (getActivityById.Succeeded)
                                    {
                                        var activityChildVM = _mapper.Map<WorkActivityViewModel>(getActivityById.Data.First());
                                        if (activityChildVM.ProductStation.Order != null && activityChildVM.ProductStation.Order.HasValue && act.ProductStation.Order.HasValue)
                                        {
                                            if ((activityChildVM.ProductStation.Order.Value - act.ProductStation.Order.Value) >= 1)
                                            {
                                                if (act.isFinished)
                                                {
                                                    workActivityGroupVM.Activities.First().DisablePlayBtn = "";
                                                    workActivityGroupVM.Activities.First().DisablePauseBtn = "disabled";
                                                    workActivityGroupVM.Activities.First().DisableStopBtn = "disabled";
                                                }
                                                else
                                                {
                                                    workActivityGroupVM.Activities.First().DisablePlayBtn = "disabled";
                                                    workActivityGroupVM.Activities.First().DisablePauseBtn = "disabled";
                                                    workActivityGroupVM.Activities.First().DisableStopBtn = "disabled";
                                                    breakMe = true;
                                                    break;
                                                }
                                            }
                                            else if ((activityChildVM.ProductStation.Order.Value - act.ProductStation.Order.Value) == 0)
                                            {
                                                if (activityChildVM.ProductStation.StationId == workActivityGroupVM.Activities.First().ProductStation.StationId)
                                                {
                                                    workActivityGroupVM.Activities.First().DisablePlayBtn = "";
                                                    workActivityGroupVM.Activities.First().DisablePauseBtn = "disabled";
                                                    workActivityGroupVM.Activities.First().DisableStopBtn = "disabled";
                                                }
                                                else
                                                {
                                                    if (act.isFinished)
                                                    {
                                                        workActivityGroupVM.Activities.First().DisablePlayBtn = "";
                                                        workActivityGroupVM.Activities.First().DisablePauseBtn = "disabled";
                                                        workActivityGroupVM.Activities.First().DisableStopBtn = "disabled";
                                                    }
                                                    else
                                                    {
                                                        workActivityGroupVM.Activities.First().DisablePlayBtn = "disabled";
                                                        workActivityGroupVM.Activities.First().DisablePauseBtn = "disabled";
                                                        workActivityGroupVM.Activities.First().DisableStopBtn = "disabled";
                                                        breakMe = true;
                                                        break;
                                                    }
                                                }

                                            }
                                            else if ((act.ProductStation.Order.Value - activityChildVM.ProductStation.Order.Value) == 1)
                                            {
                                                if (act.isFinished)
                                                {
                                                    workActivityGroupVM.Activities.First().DisablePlayBtn = "";
                                                    workActivityGroupVM.Activities.First().DisablePauseBtn = "disabled";
                                                    workActivityGroupVM.Activities.First().DisableStopBtn = "disabled";
                                                }
                                                else
                                                {
                                                    workActivityGroupVM.Activities.First().DisablePlayBtn = "disabled";
                                                    workActivityGroupVM.Activities.First().DisablePauseBtn = "disabled";
                                                    workActivityGroupVM.Activities.First().DisableStopBtn = "disabled";
                                                    breakMe = true;
                                                    break;
                                                }
                                            }
                                            else if (activityChildVM.ProductStation.Order.Value == 1)
                                            {
                                                if (act.isFinished)
                                                {
                                                    workActivityGroupVM.Activities.First().DisablePlayBtn = "";
                                                    workActivityGroupVM.Activities.First().DisablePauseBtn = "disabled";
                                                    workActivityGroupVM.Activities.First().DisableStopBtn = "disabled";
                                                }
                                                else
                                                {
                                                    workActivityGroupVM.Activities.First().DisablePlayBtn = "disabled";
                                                    workActivityGroupVM.Activities.First().DisablePauseBtn = "disabled";
                                                    workActivityGroupVM.Activities.First().DisableStopBtn = "disabled";
                                                    breakMe = true;
                                                    break;
                                                }
                                            }
                                            else
                                            {
                                                workActivityGroupVM.Activities.First().DisablePlayBtn = "disabled";
                                                workActivityGroupVM.Activities.First().DisablePauseBtn = "disabled";
                                                workActivityGroupVM.Activities.First().DisableStopBtn = "disabled";
                                                breakMe = true;
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            workActivityGroupVM.Activities.First().DisablePlayBtn = "disabled";
                                            workActivityGroupVM.Activities.First().DisablePauseBtn = "disabled";
                                            workActivityGroupVM.Activities.First().DisableStopBtn = "disabled";
                                            breakMe = true;
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    if (workActivityGroupVM.Activities.First().isFinished)
                                    {
                                        workActivityGroupVM.Activities.First().DisablePlayBtn = "disabled";
                                        workActivityGroupVM.Activities.First().DisablePauseBtn = "disabled";
                                        workActivityGroupVM.Activities.First().DisableStopBtn = "disabled";
                                        breakMe = true;
                                        break;
                                    }
                                    else
                                    {
                                        workActivityGroupVM.Activities.First().DisablePlayBtn = "disabled";
                                        workActivityGroupVM.Activities.First().DisablePauseBtn = "";
                                        workActivityGroupVM.Activities.First().DisableStopBtn = "";
                                        breakMe = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    if (breakMe)
                    {
                        break;
                    }
                }
            }
            else
            {
                workActivityGroupVM.Activities.First().DisablePlayBtn = "";
                workActivityGroupVM.Activities.First().DisablePauseBtn = "disabled";
                workActivityGroupVM.Activities.First().DisableStopBtn = "disabled";
            }

            return workActivityGroupVM;
        }

        [Authorize(Policy = Permissions.WorkActivities.Edit)]
        public async Task<JsonResult> GetActivitiesAndChildsToActivate(string strFatherActivitiesIds, int stationId, string initialFathersIds)
        {
            try
            {
                var activitiesViewModel = new WorkActivityRelativeGroupViewModel();
                activitiesViewModel.isOnTopLevel = strFatherActivitiesIds.Equals(initialFathersIds);
                var fatherActivitiesIds = strFatherActivitiesIds.Split(',');
                var responsestation = await _stationViewManager.GetByIdAsync(stationId);
                if (responsestation.Succeeded)
                {
                    activitiesViewModel.isTorno = _mapper.Map<StationViewModel>(responsestation.Data).WorkOrderDisplayType == Stations.Torno;
                }

                activitiesViewModel.InitialFathersIds = initialFathersIds;
                activitiesViewModel.StationId = stationId;
                foreach (var fatherId in fatherActivitiesIds)
                {
                    var fatherActivity = new WorkActivityViewModel();
                    var responsefatheractivity = await _viewManager.FindBySpecification(new WorkActivityWithProductDataSpecification(Convert.ToInt32(fatherId)));
                    if (responsefatheractivity.Succeeded)
                    {
                        fatherActivity = _mapper.Map<WorkActivityViewModel>(responsefatheractivity.Data.FirstOrDefault());
                    }

                    await getDescendents(fatherActivity);
                    activitiesViewModel.FathersActivities.Add(fatherActivity);

                    if (fatherActivity.WorkOrderItem.ProductId.HasValue)
                    {
                        var getWorkActivityById = await _workActivityViewManager.FindBySpecification(new WorkActivityWithAllSpecification(fatherActivity.Id, fatherActivity.WorkOrderItem.ProductId.Value, stationId));
                        if (getWorkActivityById.Succeeded)
                        {
                            var waVMs = _mapper.Map<List<WorkActivityViewModel>>(getWorkActivityById.Data);

                            if (waVMs.Count > 0)
                            {
                                foreach (var waVM in waVMs)
                                {
                                    if (waVM.Id != fatherActivity.Id)
                                    {
                                        activitiesViewModel.FathersActivities.Add(waVM);
                                    }
                                }
                            }
                        }
                    }
                }
                if (activitiesViewModel.FathersActivities.Count > 0)
                {
                    // Ejecuto play action para todos los items seleccionados
                    var fathersActivitiesJsonObject = JsonConvert.SerializeObject(activitiesViewModel.FathersActivities, Formatting.Indented,
                        new JsonSerializerSettings
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });

                    return new JsonResult(new { isValid = true, fathersActivitiesIds = fathersActivitiesJsonObject });

                }
                return new JsonResult(new { isValid = false });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { isValid = false });
                throw;
            }

        }

        public async Task<IActionResult> validatePreviousStationForTorno(string workActivitiesIds, int stationId)
        {
            try
            {
                var hasSuccess = new List<bool>();
                var workActivitiesIdsList = workActivitiesIds.Split(",");
                foreach (var waId in workActivitiesIdsList)
                {
                    var workActivityDTOResponse = await _workActivityViewManager.FindBySpecification(new WorkActivityWithAllByStationIdSpecification(Convert.ToInt32(waId), stationId));
                    if (workActivityDTOResponse.Succeeded)
                    {
                        var workActivityVM = _mapper.Map<WorkActivityViewModel>(workActivityDTOResponse.Data.First());

                        // Fran 15/6/2023: Se tienen en cuenta las estaciones de "Abastecimientos".

                        // Tomo actividades sin tener en cuenta el estado, si no esta habilitado el puesto, la actividad esta creada pero con estado inactiva.
                        var getActivitiesByWoiId = await _workActivityViewManager.FindBySpecification(new WorkActivitiesByWorkOrderItemIdSpecification(workActivityVM.WorkOrderItemId));
                        if (getActivitiesByWoiId.Succeeded)
                        {
                            var activitiesByWoi = _mapper.Map<List<WorkActivityViewModel>>(getActivitiesByWoiId.Data);
                            activitiesByWoi = activitiesByWoi.OrderBy(a => a.ProductStation.Order).ToList();
                            foreach (var act in activitiesByWoi)
                            {
                                if (!workActivityVM.isStarted)
                                {

                                    if (workActivityVM.ProductStation.Order != null)
                                    {
                                        if ((workActivityVM.ProductStation.Order.Value - act.ProductStation.Order.Value) == 1)
                                        {
                                            if (!act.isStarted && !act.isFinished)
                                            {
                                                hasSuccess.Add(false);
                                            }
                                            else if (act.isStarted && act.isFinished)
                                            {
                                                hasSuccess.Add(true);
                                            }
                                            else if (act.isStarted && !act.isFinished)
                                            {
                                                hasSuccess.Add(false);
                                            }
                                        }
                                        else if (workActivityVM.ProductStation.Order.Value == 1)
                                        {
                                            hasSuccess.Add(true);
                                        }
                                        else
                                        {
                                            hasSuccess.Add(true);
                                        }
                                    }
                                    else
                                    {
                                        hasSuccess.Add(false);
                                    }
                                }
                            }
                        }

                    }
                }

                if (hasSuccess.Where(x => x == false).Count() == 0)
                {
                    return new JsonResult(new { isValid = true });
                }
                else
                {
                    return new JsonResult(new { isValid = false });
                }

            }
            catch (Exception ex)
            {
                _notify.Error($"{_localizer["Error"]}: {ex.Message}.");
                return new JsonResult(new { isValid = false });
            }
        }


        private async Task getDescendents(WorkActivityViewModel fatherActivity)
        {
            var responseactivities = await _viewManager.FindBySpecification(new ActivitiesByFatherAndStationSpecification(fatherActivity.WorkOrderItemId));
            if (responseactivities.Succeeded)
            {
                var childsActivities = _mapper.Map<List<WorkActivityViewModel>>(responseactivities.Data);
                foreach (var child in childsActivities)
                {
                    await getDescendents(child);
                }
                fatherActivity.ChildsActivities = childsActivities.GroupBy(x => x.WorkOrderItem.Product.Id).Select(x => x.FirstOrDefault()).ToList();
            }
        }

        [HttpPost]
        public async Task<JsonResult> StopActivities(int workOrderHeaderNumber, int stationId, bool fromShipments)
        {
            try
            {
                StationViewModel stationViewModel = null;
                var filteredactivitiesresponse = await _viewManager.FindBySpecification(new WorkActivitiesByWorkOrderHeaderNumberAndStationIdSpecification(workOrderHeaderNumber, stationId));
                if (filteredactivitiesresponse.Succeeded)
                {
                    //var activitiesViewModel = _mapper.Map<List<WorkActivityViewModel>>(filteredactivitiesresponse.Data.Where(act => act.ToShipments.HasValue ? act.ToShipments.Value == false : act.ToShipments == null).ToList());

                    // Filtrar la informacion de stock según de que tipo de planilla se trate.
                    var getStationById = await _stationViewManager.GetByIdAsync(stationId);
                    if (getStationById.Succeeded)
                    {
                        stationViewModel = _mapper.Map<StationViewModel>(getStationById.Data);
                    }

                    List<WorkActivityViewModel> activitiesViewModel = new List<WorkActivityViewModel>();

                    if (fromShipments && stationViewModel.WorkOrderDisplayType != Stations.Envios)
                    {
                        activitiesViewModel = _mapper.Map<List<WorkActivityViewModel>>(filteredactivitiesresponse.Data.Where(act => act.ToShipments.HasValue && act.ToShipments.Value == true).ToList());
                    }
                    else
                    {
                        activitiesViewModel = _mapper.Map<List<WorkActivityViewModel>>(filteredactivitiesresponse.Data.Where(act => act.ToShipments.HasValue ? act.ToShipments.Value == false : act.ToShipments == null).ToList());
                    }

                    if (stationViewModel != null)
                    {
                        activitiesViewModel = await filterWAInformationByStock(activitiesViewModel, stationViewModel);
                    }

                    if (activitiesViewModel.Count > 0)
                    {
                        foreach (var activity in activitiesViewModel)
                        {
                            var stopActionViewModel = new WorkActionViewModel();
                            stopActionViewModel.ActionName = "stop";
                            stopActionViewModel.WorkActivityId = Convert.ToInt32(activity.Id);
                            stopActionViewModel.StartDate = DateTime.Now;
                            stopActionViewModel.EndingDate = DateTime.Now;
                            var responseaction = await _workActionViewManager.CreateAsync(_mapper.Map<WorkActionDTO>(stopActionViewModel));

                            // Cuando se fuerza el stop desde la grilla de Logistica -> Envios:
                            // Si se trata de una planilla de Envios, entonces le asigno la fecha de regreso a la actividad para poder reflejar el porcentaje de avance y los
                            // estados de los items dentro de cada estacion.
                            if (stationViewModel.WorkOrderDisplayType == Stations.Envios || fromShipments)
                            {
                                if (activity.ComebackDate == null)
                                {
                                    activity.ComebackDate = DateTime.Now;
                                    await _workActivityViewManager.UpdateAsync(_mapper.Map<WorkActivityDTO>(activity));
                                }
                            }
                        }
                    }
                }

                return new JsonResult(new { isValid = true, workOrderDisplayType = stationViewModel.WorkOrderDisplayType });
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return new JsonResult(new { isValid = false });
            }
        }

        // TODO: Optimizar carga de datos.
        public async Task<IActionResult> LoadAllGrouped(DatatableParamsViewModel datatableParams,
             string sSearch_2, string sSearch_3, string sSearch_4, string sSearch_5, string sSearch_6, string sSearch_7,
             string sSearch_9, string sSearch_10, string sSearch_11)
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                var response = await _viewManager.FindBySpecification(new WorkActivityWithAllSpecification(true, false));
                if (response.Succeeded)
                {

                    var viewModel = _mapper.Map<List<WorkActivityViewModel>>(response.Data);

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
                                           Activities = grp.Where(act => act.ToShipments == null).Select(act => act).ToList(),
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
                                           DateToProduction = grp.First().DateToProduction.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                           WOCreateDate = grp.First().WorkOrderItem.WorkOrder.WorkOrderHeader.CreatedOn,
                                           GroupUsers = grp.First().AssignedUsersIds == null ? "" : getUsersStringAsync(grp.First().AssignedUsersIds.Split(','))
                                       }).ToList();

                    foreach (var actGroup in workActivityListGroup)
                    {
                        // Agrupo las actividades de cada puesto para poder obtener correctamente el porcentaje de avance según los agrupamientos en cada puesto.
                        var responsestation = await _stationViewManager.GetByIdAsync(actGroup.StationId);
                        if (responsestation.Succeeded)
                        {
                            var stationViewModel = _mapper.Map<StationViewModel>(responsestation.Data);

                            // Filtrar la informacion según de que tipo de planilla se trate.
                            actGroup.Activities = await filterWAInformationByStock(actGroup.Activities, stationViewModel);

                            switch (stationViewModel.WorkOrderDisplayType)
                            {
                                case Stations.Torno:
                                    removeChilds(actGroup.Activities);
                                    actGroup.Activities = actGroup.Activities.GroupBy(act => act.WorkOrderItem.Product.Code).Select(grp => grp.FirstOrDefault()).ToList();
                                    break;
                                case Stations.Soldadura:
                                    removeChilds(actGroup.Activities);
                                    actGroup.Activities = actGroup.Activities.GroupBy(act => act.WorkOrderItem.Product.Code).Select(grp => grp.FirstOrDefault()).ToList();
                                    break;
                                case Stations.Montaje:
                                    removeChilds(actGroup.Activities);
                                    actGroup.Activities = actGroup.Activities.GroupBy(act => act.WorkOrderItem.Product.Code).Select(grp => grp.FirstOrDefault()).ToList();
                                    break;
                                case Stations.Laser:
                                    actGroup.Activities = actGroup.Activities.GroupBy(act => act.WorkOrderItem.Product.ProductFeature.RawMaterialCode).Select(grp => grp.FirstOrDefault()).ToList();
                                    break;
                                case Stations.Serrucho:
                                    actGroup.Activities = actGroup.Activities.GroupBy(act => act.WorkOrderItem.Product.ProductFeature.RawMaterialCode).Select(grp => grp.FirstOrDefault()).ToList();
                                    break;
                                default:
                                    actGroup.Activities = actGroup.Activities.GroupBy(act => act.WorkOrderItem.Product.ProductFeature.RawMaterialCode).Select(grp => grp.FirstOrDefault()).ToList();
                                    break;
                            }
                        }
                    }

                    // Se ordena primero por Nº OT, luego por Producto y luego por Puesto de trabajo.
                    //workActivityListGroup = workActivityListGroup.OrderBy(wa => wa.WorkOrderHeaderNumber).ThenBy(wa => wa.ProductDescription).ThenBy(wa => wa.StationDescription);

                    // Fran 13/4/2023: Se ordena primero por fecha de creación OT, luego por numero de producto y luego por order de area funcional y abreviatura del puesto de trabajo.
                    workActivityListGroup = workActivityListGroup.OrderByDescending(wa => wa.WOCreateDate).ThenBy(wa => wa.ProductNumber).ThenBy(wa => wa.GroupStation != null ? (wa.GroupStation.FunctionalArea != null ? wa.GroupStation.FunctionalArea.Order : wa.GroupStationId) : wa.GroupStationId).ThenBy(wa => wa.GroupStation.Abbreviation);

                    // Cualquier planilla que no tenga ninguna actividad que mostrar que no se muestre en "Ordenes de trabajo", "Envios" y "Abastecimiento".
                    workActivityListGroup = workActivityListGroup.Where(wag => wag.Activities.Count > 0).ToList();

                    //Nro OT (ID WOH)
                    if (!string.IsNullOrEmpty(sSearch_2))
                    {
                        workActivityListGroup = workActivityListGroup.Where(wa => wa.WorkOrderHeaderNumber
                                                    .ToString().ToLower().Contains(sSearch_2.ToLower()));
                    }
                    // Nro producto (orderDetail)
                    if (!string.IsNullOrEmpty(sSearch_3))
                    {
                        workActivityListGroup = workActivityListGroup.Where(wa => wa.ProductNumber
                                                        .ToString().ToLower().Contains(sSearch_3.ToLower()));
                    }
                    // Descripcion producto
                    if (!string.IsNullOrEmpty(sSearch_4))
                    {
                        workActivityListGroup = workActivityListGroup.Where(wa => wa.ProductDescription
                                                        .ToString().ToLower().Contains(sSearch_4.ToLower()));
                    }
                    // Configuracion
                    if (!string.IsNullOrEmpty(sSearch_5))
                    {

                        workActivityListGroup = workActivityListGroup.Where(wa => !string.IsNullOrEmpty(wa.StructureDescription) &&
                                                        wa.StructureDescription
                                                       .ToString().ToLower().Contains(sSearch_5.ToLower()));
                    }
                    // Puesto
                    if (!string.IsNullOrEmpty(sSearch_6))
                    {
                        workActivityListGroup = workActivityListGroup.Where(wa => wa.StationDescription
                                                        .ToString().ToLower().Contains(sSearch_6.ToLower()));
                    }
                    // Activity State
                    if (!string.IsNullOrEmpty(sSearch_7))
                    {
                        workActivityListGroup = workActivityListGroup.Where(wa => wa.ActivitiesState
                                                        .ToString().ToLower().Contains(sSearch_7.ToLower()));
                    }
                    // DateToProduction
                    if (!string.IsNullOrEmpty(sSearch_9))
                    {
                        workActivityListGroup = workActivityListGroup.Where(wa => wa.DateToProduction != null && wa.DateToProduction
                                                        .ToString().ToLower().Contains(sSearch_9.ToLower()));
                    }
                    // Start Date 
                    if (!string.IsNullOrEmpty(sSearch_10))
                    {
                        workActivityListGroup = workActivityListGroup.Where(wa => wa.StartDate != null && wa.StartDate
                                                        .ToString().ToLower().Contains(sSearch_10.ToLower()));
                    }
                    // End Date
                    if (!string.IsNullOrEmpty(sSearch_11))
                    {
                        workActivityListGroup = workActivityListGroup.Where(wa => wa.EndDate != null && wa.EndDate
                                                         .ToString().ToLower().Contains(sSearch_11.ToLower()));
                    }

                    var sortColumnIndex = datatableParams.iSortCol_0;
                    var sortDirection = datatableParams.sSortDir_0;

                    if (sortColumnIndex == 1)
                    {
                        workActivityListGroup = sortDirection == "asc" ? workActivityListGroup.OrderBy(wa => wa.WorkOrderHeaderNumber) : workActivityListGroup.OrderByDescending(wa => wa.WorkOrderHeaderNumber);
                    }
                    if (sortColumnIndex == 2)
                    {
                        workActivityListGroup = sortDirection == "asc" ? workActivityListGroup.OrderBy(wa => wa.WorkOrderHeaderNumber) : workActivityListGroup.OrderByDescending(wa => wa.WorkOrderHeaderNumber);
                    }
                    if (sortColumnIndex == 3)
                    {
                        workActivityListGroup = sortDirection == "asc" ? workActivityListGroup.OrderBy(wa => wa.ProductNumber) : workActivityListGroup.OrderByDescending(wa => wa.ProductNumber);
                    }
                    if (sortColumnIndex == 4)
                    {
                        workActivityListGroup = sortDirection == "asc" ? workActivityListGroup.OrderBy(wa => wa.ProductDescription) : workActivityListGroup.OrderByDescending(wa => wa.ProductDescription);
                    }
                    if (sortColumnIndex == 5)
                    {
                        workActivityListGroup = sortDirection == "asc" ? workActivityListGroup.OrderBy(wa => wa.StructureDescription) : workActivityListGroup.OrderByDescending(wa => wa.StructureDescription);
                    }
                    if (sortColumnIndex == 6)
                    {
                        workActivityListGroup = sortDirection == "asc" ? workActivityListGroup.OrderBy(wa => wa.StationDescription) : workActivityListGroup.OrderByDescending(wa => wa.StationDescription);
                    }
                    if (sortColumnIndex == 7)
                    {
                        workActivityListGroup = sortDirection == "asc" ? workActivityListGroup.OrderBy(wa => wa.ActivitiesState) : workActivityListGroup.OrderByDescending(wa => wa.ActivitiesState);
                    }
                    if (sortColumnIndex == 8)
                    {
                        workActivityListGroup = sortDirection == "asc" ? workActivityListGroup.OrderBy(wa => wa.ProgressPercentage) : workActivityListGroup.OrderByDescending(wa => wa.ProgressPercentage);
                    }
                    if (sortColumnIndex == 9)
                    {
                        workActivityListGroup = sortDirection == "asc" ? workActivityListGroup.OrderBy(wa => wa.WorkOrderId) : workActivityListGroup.OrderByDescending(wa => wa.WorkOrderId);
                    }
                    if (sortColumnIndex == 10)
                    {
                        workActivityListGroup = sortDirection == "asc" ? workActivityListGroup.OrderBy(wa => wa.StartDate) : workActivityListGroup.OrderByDescending(wa => wa.StartDate);
                    }
                    if (sortColumnIndex == 11)
                    {
                        workActivityListGroup = sortDirection == "asc" ? workActivityListGroup.OrderBy(wa => wa.EndDate) : workActivityListGroup.OrderByDescending(wa => wa.EndDate);
                    }
                    if (sortColumnIndex == 12)
                    {
                        workActivityListGroup = sortDirection == "asc" ? workActivityListGroup.OrderBy(wa => wa.TotalTime) : workActivityListGroup.OrderByDescending(wa => wa.TotalTime);
                    }
                    if (sortColumnIndex == 13)
                    {
                        workActivityListGroup = sortDirection == "asc" ? workActivityListGroup.OrderBy(wa => wa.Quantity) : workActivityListGroup.OrderByDescending(wa => wa.Quantity);
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

                _notify.Information(_localizer["Error retrieving data."]);
                return new JsonResult(new { isValid = false });

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
                    if (stationViewModel.WorkOrderDisplayType == Stations.Serrucho || stationViewModel.WorkOrderDisplayType == Stations.Laser || stationViewModel.WorkOrderDisplayType == Stations.Torno)
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
                    else if (stationViewModel.WorkOrderDisplayType == Stations.Soldadura)
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
                    else if (stationViewModel.WorkOrderDisplayType == Stations.Montaje)
                    {
                        workActivitiesToAdd = workActivitiesVM;
                    }
                    else if (stationViewModel.WorkOrderDisplayType == Stations.Envios)
                    {
                        workActivitiesToAdd = workActivitiesVM;
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

        public async Task<IActionResult> LoadAllByStation(DatatableParamsViewModel datatableParams, int stationId,
            string sSearch_2, string sSearch_3, string sSearch_4, string sSearch_5, string sSearch_6,
            string sSearch_8, string sSearch_9, string sSearch_10)
        {
            try
            {
                var response = await _viewManager.FindBySpecification(new WorkActivityWithAllByStationIdSpecification(stationId, true));
                if (response.Succeeded)
                {
                    IEnumerable<WorkActivityViewModel> waViewModel = (_mapper.Map<List<WorkActivityViewModel>>(response.Data));

                    //Agrupamos por WorkOrder y pero filtrado previamente por stationid
                    IEnumerable<WorkActivityGroupViewModel> workActivityListGroup = waViewModel.GroupBy(x => (x.WorkOrderItem.WorkOrderId))
                                       .Select(grp => new WorkActivityGroupViewModel
                                       {
                                           WorkOrderId = grp.Key,
                                           StationId = stationId,
                                           Quantity = grp.Count(),
                                           isActive = grp.All(x => x.StateActivity),
                                           WorkOrderHeaderNumber = grp.First().WorkOrderItem.WorkOrder.WorkOrderHeader.WorkOrderHeaderNumber,
                                           ProductNumber = grp.First().WorkOrderItem.WorkOrder.OrderDetail.ProductNumber,
                                           ProductDescription = grp.First().WorkOrderItem.WorkOrder.OrderDetail.Product.CodeAndDescription,
                                           StructureDescription = grp.First().WorkOrderItem.WorkOrder.OrderDetail.Structure != null ? grp.First().WorkOrderItem.WorkOrder.OrderDetail.Structure.Description : "",
                                           StationDescription = grp.First().ProductStation.Station.AbreviationDescription,
                                           ProgressPercentage = (grp.Where(x => x.isFinished).Count() * 100 / grp.Count()),
                                           TotalTime = new TimeSpan(grp.Sum(x => x.TotalTime.Value.Ticks)).ToString(@"dd\ hh\:mm\:ss"),
                                           StartDate = grp.Any(z => z.StartDate != null) ? grp.Min(z => z.StartDate).Value.ToString("dd/MM/yyyy HH:mm:ss") : null,
                                           EndDate = grp.Any(z => z.EndingDate == null) ? null : grp.Max(z => z.EndingDate).Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                           ActivitiesState = grp.All(x => x.getState == ActivityStates.Pendiente) ?
                                                                ActivityStates.Pendiente.ToString() : grp.Any(x => x.getState == ActivityStates.EnProceso) ?
                                                                ActivityStates.EnProceso.ToString() : grp.All(x => x.getState == ActivityStates.Finalizado) ?
                                                                ActivityStates.Finalizado.ToString() : ActivityStates.Pendiente.ToString(),
                                           DateToProduction = grp.First().DateToProduction.Value.ToString("dd/MM/yyyy HH:mm:ss"),

                                       }).ToList();

                    //Nro OT (ID WOH)
                    if (!string.IsNullOrEmpty(sSearch_2))
                    {
                        workActivityListGroup = workActivityListGroup.Where(wa => wa.WorkOrderHeaderNumber
                                                    .ToString().ToLower().Contains(sSearch_2.ToLower()));
                    }
                    // Nro producto (orderDetail)
                    if (!string.IsNullOrEmpty(sSearch_3))
                    {
                        workActivityListGroup = workActivityListGroup.Where(wa => wa.ProductNumber
                                                        .ToString().ToLower().Contains(sSearch_3.ToLower()));
                    }
                    // Descripcion producto
                    if (!string.IsNullOrEmpty(sSearch_4))
                    {
                        workActivityListGroup = workActivityListGroup.Where(wa => wa.ProductDescription
                                                        .ToString().ToLower().Contains(sSearch_4.ToLower()));
                    }
                    // Configuracion
                    if (!string.IsNullOrEmpty(sSearch_5))
                    {

                        workActivityListGroup = workActivityListGroup.Where(wa => !string.IsNullOrEmpty(wa.StructureDescription) &&
                                                        wa.StructureDescription
                                                       .ToString().ToLower().Contains(sSearch_5.ToLower()));
                    }
                    // ActivityState
                    if (!string.IsNullOrEmpty(sSearch_6))
                    {
                        workActivityListGroup = workActivityListGroup.Where(wa => wa.ActivitiesState
                                                        .ToString().ToLower().Contains(sSearch_6.ToLower()));
                    }
                    // DateToProduction 
                    if (!string.IsNullOrEmpty(sSearch_8))
                    {
                        workActivityListGroup = workActivityListGroup.Where(wa => wa.DateToProduction != null && wa.DateToProduction
                                                        .ToString().ToLower().Contains(sSearch_8.ToLower()));
                    }

                    // Start Date 
                    if (!string.IsNullOrEmpty(sSearch_9))
                    {
                        workActivityListGroup = workActivityListGroup.Where(wa => wa.StartDate != null && wa.StartDate
                                                        .ToString().ToLower().Contains(sSearch_9.ToLower()));
                    }
                    // End Date
                    if (!string.IsNullOrEmpty(sSearch_10))
                    {
                        workActivityListGroup = workActivityListGroup.Where(wa => wa.EndDate != null && wa.EndDate
                                                         .ToString().ToLower().Contains(sSearch_10.ToLower()));
                    }

                    var sortColumnIndex = Convert.ToInt32(HttpContext.Request.Query["iSortCol_0"]);
                    var sortDirection = HttpContext.Request.Query["sSortDir_0"];

                    if (sortColumnIndex == 1)
                    {
                        workActivityListGroup = sortDirection == "asc" ? workActivityListGroup.OrderBy(wa => wa.WorkOrderId) : workActivityListGroup.OrderByDescending(wa => wa.WorkOrderId);
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

                _notify.Information(_localizer["Error retrieving data."]);
                return new JsonResult(new { isValid = false });
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

        // Método creado por Maxi y Sope -> Si se agrupan actividades, se crea solo acción de inicio para la primera del grupo.
        public async Task<IActionResult> newActionOld(string activitiesIds, string actionName)
        {
            var workActivitiesIdList = activitiesIds.Split(',');
            var activityId = Convert.ToInt32(workActivitiesIdList.First());
            var nowTime = DateTime.Now;
            var lastActionTime = 0;
            try
            {
                // Asignamos fecha de fin a la ultima acción
                var responseActivity = await _viewManager.FindBySpecification(new WorkActivityWithActionByIdSpecification(activityId));
                if (responseActivity.Succeeded)
                {
                    var activityViewModel = _mapper.Map<WorkActivityViewModel>(responseActivity.Data.FirstOrDefault());
                    if (activityViewModel.WorkActions.Count > 0)
                    {
                        var actionList = activityViewModel.WorkActions.ToList();
                        var lastAction = actionList[actionList.Count - 1];
                        lastAction.EndingDate = nowTime;
                        lastActionTime = (int)lastAction.ActionTime.TotalSeconds / workActivitiesIdList.Count();
                        // Si es un grupo de actividades, ajustamos los tiempos de la accion de la primer actividad y creamos las acciones para las restantes del grupo
                        if (workActivitiesIdList.Count() > 1)
                        {
                            await assignActionsAsync(workActivitiesIdList, lastAction);
                        }
                        var responseActionUpdate = await _workActionViewManager.UpdateAsync(_mapper.Map<WorkActionDTO>(lastAction));
                    }
                }

                // Se crea la nueva acción
                var viewModel = new WorkActionViewModel();
                viewModel.WorkActivityId = activityId;
                viewModel.ActionName = actionName;
                viewModel.StartDate = nowTime;
                var responseAction = await _workActionViewManager.CreateAsync(_mapper.Map<WorkActionDTO>(viewModel));
                // Si es un grupo de actividades y la acción es stop, creamos acciones de stop para el grupo
                if (actionName == "stop" && workActivitiesIdList.Count() > 1)
                {
                    await CreateStopActionsAsync(workActivitiesIdList, activityId, nowTime, lastActionTime);
                }
                if (responseAction.Succeeded)
                {
                    _notify.Success(_localizer["Action generated."]);
                    return new JsonResult(new { isValid = true });
                }
                else
                {
                    _notify.Information(_localizer["There was a problem generating the action"]);
                    return new JsonResult(new { isValid = false });
                }

            }
            catch (Exception ex)
            {
                _notify.Error($"{_localizer["Error"]}: {ex.Message}.");
                return new JsonResult(new { isValid = false });
            }
        }

        public async Task<IActionResult> newAction(string activitiesIds, string actionName)
        {
            var workActivitiesIdList = activitiesIds.Split(',');
            var nowTime = DateTime.Now;
            var time = 0;
            var response = true;
            try
            {
                foreach (var id in workActivitiesIdList)
                {
                    var activityId = Convert.ToInt32(id);
                    var responseActivity = await _viewManager.FindBySpecification(new WorkActivityWithActionByIdSpecification(activityId));
                    if (responseActivity.Succeeded)
                    {
                        var activityViewModel = _mapper.Map<WorkActivityViewModel>(responseActivity.Data.FirstOrDefault());
                        if (activityViewModel.WorkActions.Count > 0)
                        {
                            //if (actionName != "stop")
                            //{
                            var actionList = activityViewModel.WorkActions.ToList();
                            var lastAction = actionList[actionList.Count - 1];
                            lastAction.EndingDate = nowTime;
                            time = (int)lastAction.ActionTime.TotalSeconds / workActivitiesIdList.Count();
                            lastAction.EndingDate = lastAction.StartDate.Value.AddSeconds(time);
                            var responseActionUpdate = await _workActionViewManager.UpdateAsync(_mapper.Map<WorkActionDTO>(lastAction));
                            //}
                        }
                    }

                    // Se crea la nueva acción
                    var viewModel = new WorkActionViewModel();
                    viewModel.WorkActivityId = activityId;
                    viewModel.ActionName = actionName;
                    viewModel.StartDate = nowTime;
                    if (actionName == "stop")
                    {
                        viewModel.EndingDate = nowTime;
                    }
                    //else
                    //{
                    //    viewModel.StartDate = nowTime;
                    //}
                    var responseAction = await _workActionViewManager.CreateAsync(_mapper.Map<WorkActionDTO>(viewModel));
                    if (responseAction.Failed)
                    {
                        response = false;
                    }
                }

                if (response)
                {
                    //_notify.Success(_localizer["Action generated."]);
                    return new JsonResult(new { isValid = true });
                }
                else
                {
                    _notify.Information(_localizer["There was a problem generating the action"]);
                    return new JsonResult(new { isValid = false });
                }
            }
            catch (Exception ex)
            {
                _notify.Error($"{_localizer["Error"]}: {ex.Message}.");
                return new JsonResult(new { isValid = false });
            }
        }

        //Solo para manejo de acciones que se agruparon con localStorage
        public async Task<IActionResult> newActionGrupal(string activitiesIds, string actionName, int stationId, string productCode)
        {
            var workActivitiesIdList = activitiesIds.Replace("-", ",").Split(',');
            var nowTime = DateTime.Now;
            var time = 0;
            var response = true;
            try
            {
                // Las acciones fueron creadas todas juntas cuando se lanzaron las actividades
                // Asignamos fecha de fin a todas en forma proporcional
                foreach (var id in workActivitiesIdList)
                {
                    var activityId = Convert.ToInt32(id);
                    var responseActivity = await _viewManager.FindBySpecification(new WorkActivityWithActionByIdSpecification(activityId));
                    if (responseActivity.Succeeded)
                    {
                        var activityViewModel = _mapper.Map<WorkActivityViewModel>(responseActivity.Data.FirstOrDefault());
                        if (activityViewModel.WorkActions.Count > 0)
                        {
                            //if (actionName != "stop")
                            //{
                            var actionList = activityViewModel.WorkActions.ToList();
                            var lastAction = actionList[actionList.Count - 1];
                            lastAction.EndingDate = nowTime;
                            time = (int)lastAction.ActionTime.TotalSeconds / workActivitiesIdList.Count();
                            lastAction.EndingDate = lastAction.StartDate.Value.AddSeconds(time);
                            var responseActionUpdate = await _workActionViewManager.UpdateAsync(_mapper.Map<WorkActionDTO>(lastAction));
                            //}
                        }
                    }

                    //var getProductByCode = _productViewManager.FindBySpecification(new ProductsByCodeSpecification(productCode.ToString().Trim()));
                    //if (getProductByCode.Result.Succeeded)
                    //{
                    //    var productViewModel = _mapper.Map<ProductViewModel>(getProductByCode.Result.Data.First());
                    //    var getWorkActivityById = await _workActivityViewManager.FindBySpecification(new WorkActivityWithAllSpecification(activityId, productViewModel.Id, stationId));
                    //    if (getWorkActivityById.Succeeded)
                    //    {
                    //        var waVMs = _mapper.Map<List<WorkActivityViewModel>>(getWorkActivityById.Data);

                    //        if (waVMs.Count > 0)
                    //        {
                    //            foreach (var waVM in waVMs)
                    //            {
                    //                var viewModel = new WorkActionViewModel();
                    //                viewModel.WorkActivityId = waVM.Id;
                    //                viewModel.ActionName = actionName;
                    //                viewModel.StartDate = nowTime;
                    //                var responseAction = await _workActionViewManager.CreateAsync(_mapper.Map<WorkActionDTO>(viewModel));
                    //                if (responseAction.Failed)
                    //                {
                    //                    response = false;
                    //                }
                    //            }
                    //        }
                    //    }
                    //}

                    // Se crea la nueva acción
                    var viewModel = new WorkActionViewModel();
                    viewModel.WorkActivityId = activityId;
                    viewModel.ActionName = actionName;
                    viewModel.StartDate = nowTime;
                    if (actionName == "stop")
                    {
                        viewModel.EndingDate = nowTime;
                    }
                    var responseAction = await _workActionViewManager.CreateAsync(_mapper.Map<WorkActionDTO>(viewModel));
                    if (responseAction.Failed)
                    {
                        response = false;
                    }
                }
                if (response)
                {
                    // Si la actionName == "stop" tengo que eliminar de la tabla GroupedWorkActions las workActivitiesIdList
                    if (actionName == "stop")
                    {
                        var getGroupedWorkActions = await _groupedWorkActionViewManager.FindBySpecification(new GetGroupedWorkActionsSpecification(activitiesIds.Replace("-", ",")));
                        if (getGroupedWorkActions.Succeeded)
                        {
                            var groupedWorkActionVM = _mapper.Map<GroupedWorkActionViewModel>(getGroupedWorkActions.Data.First());
                            await _groupedWorkActionViewManager.DeleteAsync(groupedWorkActionVM.Id);
                        }
                    }

                    //_notify.Success(_localizer["Action generated."]);
                    return new JsonResult(new { isValid = true });
                }
                else
                {
                    _notify.Information(_localizer["There was a problem generating the action"]);
                    return new JsonResult(new { isValid = false });
                }
            }
            catch (Exception ex)
            {
                _notify.Error($"{_localizer["Error"]}: {ex.Message}.");
                return new JsonResult(new { isValid = false });
            }
        }
        public async Task<IActionResult> newGroupedWorkAction(string workActivitiesIds, int stationId)
        {
            try
            {
                var groupedWorkActionVM = new GroupedWorkActionViewModel();
                groupedWorkActionVM.WorkActivitiesIds = workActivitiesIds;
                groupedWorkActionVM.StationId = stationId;

                var groupedWorkActionDTO = _mapper.Map<GroupedWorkActionDTO>(groupedWorkActionVM);
                var createResponse = await _groupedWorkActionViewManager.CreateAsync(groupedWorkActionDTO);
                if (createResponse.Succeeded)
                {
                    return new JsonResult(new { isValid = true });
                }
                else
                {
                    return new JsonResult(new { isValid = false });
                }
            }
            catch (Exception ex)
            {
                _notify.Error($"{_localizer["Error"]}: {ex.Message}.");
                return new JsonResult(new { isValid = false });
            }
        }

        //Metodo que se encarga de dividir proporcionalmente el tiempo de trabajo de un conjunto de actividades
        public async Task<bool> assignActionsAsync(string[] activitiesIds, WorkActionViewModel lastAction)
        {
            try
            {
                // Ajustar tiempo de la ultima accion de la primer actividad, y crear la accion "replicada" al resto del grupo
                var totalActivities = activitiesIds.Count();
                var individualTimeActivity = AdjustActionTime(lastAction, totalActivities);
                var acumulativeTime = individualTimeActivity;
                foreach (var id in activitiesIds)
                {
                    if (Convert.ToInt32(id) != lastAction.WorkActivityId)
                    {
                        await CreateActionAsync(Convert.ToInt32(id), lastAction, acumulativeTime);
                        acumulativeTime += acumulativeTime;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        //Metodo para la creación de acciones "stop" para un grupo de actividades
        public async Task CreateStopActionsAsync(string[] activitiesIds, int activityIdToExclude, DateTime nowDateTime, int lastActionTime)
        {
            try
            {
                var acum = 1;
                foreach (var id in activitiesIds)
                {

                    if (Convert.ToInt32(id) != activityIdToExclude)
                    {

                        var newStopAction = new WorkActionViewModel();
                        newStopAction.ActionName = "stop";
                        newStopAction.WorkActivityId = Convert.ToInt32(id);
                        newStopAction.StartDate = nowDateTime;
                        newStopAction.StartDate = newStopAction.StartDate.Value.AddSeconds(-(lastActionTime * acum));
                        var responseaction = await _workActionViewManager.CreateAsync(_mapper.Map<WorkActionDTO>(newStopAction));
                        acum++;
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        //Metodo para replicar las acciones de una actividad en las restandes del grupo, de manera escalonada
        public async Task CreateActionAsync(int activityId, WorkActionViewModel action, int time)
        {
            try
            {
                var newAction = new WorkActionViewModel();
                newAction.ActionName = action.ActionName;
                newAction.StartDate = action.StartDate.Value.AddSeconds(time);
                if (action.EndingDate != null)
                {
                    newAction.EndingDate = action.EndingDate.Value.AddSeconds(time);
                }
                newAction.WorkActivityId = activityId;
                var responsecreateaction = await _workActionViewManager.CreateAsync(_mapper.Map<WorkActionDTO>(newAction));
            }
            catch (Exception)
            {

                throw;
            }

        }

        //Metodo para ajustar proporcionalmente el tiempo de una actividad
        public int AdjustActionTime(WorkActionViewModel action, int totalActivities)
        {
            var timeLapseAction = action.EndingDate - action.StartDate;
            var totalTimeAction = Convert.ToInt32(timeLapseAction.Value.TotalSeconds);
            var averageTime = totalTimeAction / totalActivities;
            var timeToRemove = totalTimeAction - averageTime;
            action.EndingDate = action.EndingDate.Value.AddSeconds(-timeToRemove);
            return averageTime;
        }

        public IList<WorkActivityViewModel> clearObjectCycle(IList<WorkActivityViewModel> vmList)
        {
            foreach (var waVM in vmList)
            {
                if (waVM.WorkOrderItem != null)
                {
                    waVM.WorkOrderItem.WorkActivities = null;
                    waVM.WorkOrderItem.WorkOrder.WorkOrderItems = new List<WorkOrderItemViewModel>();
                    waVM.WorkOrderItem.WorkOrder.WorkOrderHeader.WorkOrders = new List<WorkOrderViewModel>();
                }
            }
            return vmList;
        }

        //[Authorize(Policy = Permissions.WorkActivities.Edit)]
        //public async Task<JsonResult> GetActivitiesAndChilds(string strFatherActivitiesIds, int stationId, string initialFathersIds)
        //{
        //    try
        //    {
        //        var activitiesViewModel = new WorkActivityRelativeGroupViewModel();
        //        activitiesViewModel.isOnTopLevel = strFatherActivitiesIds.Equals(initialFathersIds);
        //        var fatherActivitiesIds = strFatherActivitiesIds.Split(',');
        //        var responsestation = await _stationViewManager.GetByIdAsync(stationId);
        //        if (responsestation.Succeeded)
        //        {
        //            activitiesViewModel.isTorno = _mapper.Map<StationViewModel>(responsestation.Data).WorkOrderDisplayType == Stations.Torno;
        //        }

        //        activitiesViewModel.InitialFathersIds = initialFathersIds;
        //        activitiesViewModel.StationId = stationId;
        //        foreach (var fatherId in fatherActivitiesIds)
        //        {
        //            var fatherActivity = new WorkActivityViewModel();
        //            var responsefatheractivity = await _viewManager.FindBySpecification(new WorkActivityWithProductDataSpecification(Convert.ToInt32(fatherId)));
        //            if (responsefatheractivity.Succeeded)
        //            {
        //                fatherActivity = _mapper.Map<WorkActivityViewModel>(responsefatheractivity.Data.FirstOrDefault());
        //            }

        //            await getDescendents(fatherActivity);
        //            activitiesViewModel.FathersActivities.Add(fatherActivity);
        //        }
        //        if (activitiesViewModel.FathersActivities.Count > 0)
        //        {
        //            return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_ManageActivityGroup", activitiesViewModel) });
        //        }
        //        return new JsonResult(new { isValid = false });
        //    }
        //    catch (Exception)
        //    {
        //        var workOrderViewModel = new WorkOrderViewModel();
        //        return new JsonResult(new { isValid = false });
        //        throw;
        //    }

        //}

    }

}

