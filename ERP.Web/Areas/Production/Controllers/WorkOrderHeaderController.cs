using ERP.Application.Constants;
using ERP.Application.DTOs.Entities.Production;
using ERP.Application.Interfaces.ViewManagers.Production;
using ERP.Application.Interfaces.ViewManagers.ProductMod;
using ERP.Application.Specification.Production.WorkActivitySpecifications;
using ERP.Application.Specification.Production.WorkOrderHeaderSpecifications;
using ERP.Application.Specification.Production.WorkOrderItemSpecifications;
using ERP.Infrastructure.DbContexts;
using ERP.Language;
using ERP.Web.Abstractions;
using ERP.Web.Areas.Admin.Models;
using ERP.Web.Areas.Production.Models;
using ERP.Web.Areas.Production.Models.WorkActivity;
using ERP.Web.Areas.ProductMod.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Web.Areas.Production.Controllers
{
    [Area("Production")]
    public class WorkOrderHeaderController : BaseController<WorkOrderHeaderController>
    {
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly IWorkOrderHeaderViewManager _workOrderHeaderViewManager;
        private readonly IWorkActivityViewManager _workActivityViewManager;
        private readonly IStationViewManager _stationViewManager;
        private readonly IWorkOrderItemViewManager _workOrderItemViewManager;
        private readonly IWorkActionViewManager _workActionViewManager;
        private IdentityContext _identityContext;

        public WorkOrderHeaderController(IStringLocalizer<SharedResource> localizer, IWorkOrderHeaderViewManager workOrderHeaderViewManager, IWorkActivityViewManager workActivityViewManager, IStationViewManager stationViewManager, IWorkOrderItemViewManager workOrderItemViewManager, IWorkActionViewManager workActionViewManager, IdentityContext identityContext)
        {
            _localizer = localizer;
            _workOrderHeaderViewManager = workOrderHeaderViewManager;
            _workActivityViewManager = workActivityViewManager;
            _stationViewManager = stationViewManager;
            _workOrderItemViewManager = workOrderItemViewManager;
            _workActionViewManager = workActionViewManager;
            _identityContext = identityContext;
        }

        [Authorize(Policy = Permissions.WorkOrders.View)]
        public async Task<JsonResult> OnGetWorkOrderHeader(int idWorkOrder = 0)
        {
            try
            {
                var response = await _workOrderHeaderViewManager.FindBySpecification(new WorkOrderHeaderByWorkOrderSpecification(idWorkOrder));
                if (response.Succeeded)
                {
                    var workOrderHeaderViewModel = _mapper.Map<WorkOrderHeaderViewModel>(response.Data.FirstOrDefault());
                    foreach (var wo in workOrderHeaderViewModel.WorkOrders)
                    {
                        foreach (var woi in wo.WorkOrderItems)
                        {
                            if (woi.WorkActivities.Any())
                            {
                                workOrderHeaderViewModel.WorkActivities.AddRange(woi.WorkActivities);
                            }
                        }
                    }
                    var usersViewModel = _mapper.Map<List<UserViewModel>>(_identityContext.Users.Where(user => user.IsActive == true).ToList());

                    workOrderHeaderViewModel.UsersSelectList = new SelectList(usersViewModel, nameof(UserViewModel.Id), nameof(UserViewModel.FirstAndLastName), null, null);

                    //var waListGroup = workOrderHeaderViewModel.WorkActivities.GroupBy(x => x.ProductStation.Station.Id).Select(grp => new WorkActivityGroupViewModel
                    //{
                    //    Activities = grp.Select(x => x).ToList(),
                    //    UsersList = grp.First().AssignedUsersIds == null ? new string[] { } : grp.First().AssignedUsersIds.Split(','),
                    //    GroupStationId = grp.First().ProductStation.Station.Id,
                    //    UsersSelectList = StationUserSelectList(grp.First().ProductStation.Station.Users),
                    //    FinishedQuantity = (grp.Where(x => x.isFinished).Count()),
                    //    Quantity = grp.Count(),
                    //});

                    var waListGroup = workOrderHeaderViewModel.WorkActivities.GroupBy(x => x.ProductStation.Station.Id).Select(grp => new WorkActivityGroupViewModel
                    {
                        Activities = grp.Select(x => x).ToList(),
                        UsersList = grp.All(x => x.AssignedUsersIds == null) ? new string[] { } : grp.Where(x => x.AssignedUsersIds != null && x.LastModifiedOn.HasValue).OrderByDescending(x => x.LastModifiedOn.Value).Select(x => x.AssignedUsersIds.Split(',')).First(),
                        GroupStationId = grp.First().ProductStation.Station.Id,
                        UsersSelectList = StationUserSelectList(grp.First().ProductStation.Station.Users),
                        FinishedQuantity = (grp.Where(x => x.isFinished).Count()),
                        Quantity = grp.Count(),
                    });

                    workOrderHeaderViewModel.WorkActivitiesGroupsByStation = waListGroup.ToList();

                    foreach (var actGroup in workOrderHeaderViewModel.WorkActivitiesGroupsByStation)
                    {
                        // Agrupo las actividades de cada puesto para poder obtener correctamente el porcentaje de avance según los agrupamientos en cada puesto.
                        var responsestation = await _stationViewManager.GetByIdAsync(actGroup.GroupStationId);
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
                                case Stations.Envios:
                                    actGroup.Activities = actGroup.Activities.GroupBy(act => act.WorkOrderItem.Product.Code).Select(grp => grp.FirstOrDefault()).ToList();
                                    break;
                                case Stations.Abastecimientos:
                                    actGroup.Activities = actGroup.Activities.GroupBy(act => act.WorkOrderItem.Product.Code).Select(grp => grp.FirstOrDefault()).ToList();
                                    break;
                                default:
                                    actGroup.Activities = actGroup.Activities.GroupBy(act => act.WorkOrderItem.Product.ProductFeature.RawMaterialCode).Select(grp => grp.FirstOrDefault()).ToList();
                                    break;
                            }
                        }
                    }

                    // Para ordenar el listado de puestos de trabajo segun area funcional y abreviatura del puesto.
                    workOrderHeaderViewModel.WorkActivitiesGroupsByStation = workOrderHeaderViewModel.WorkActivitiesGroupsByStation.OrderBy(wgbs => wgbs.GroupStation != null ? wgbs.GroupStation.FunctionalArea.Order : wgbs.GroupStationId).ThenBy(wgbs => wgbs.GroupStation.Abbreviation).ToList();

                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_ViewDetails", workOrderHeaderViewModel) });
                }
                return null;
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);

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
                    else if (stationViewModel.WorkOrderDisplayType == Stations.Abastecimientos)
                    {
                        workActivitiesToAdd = workActivitiesVM;
                    }
                }

                if (workActivitiesToAdd.Count != 0)
                {
                    workActivitiesVM = workActivitiesToAdd;
                }

                return workActivitiesVM;
            }
            catch
            {
                _notify.Error("No se pudo filtrar la información de las actividades respecto a los datos de stock.");
                return workActivitiesVM;
            }
        }

        public async Task assignUsersToGroupActivitiesAsync(WorkActivityGroupViewModel group, int workOrderHeaderId)
        {
            var responseactivities = await _workActivityViewManager.FindBySpecification(new ActivitiesByWorkOrderHeaderAndStationSpecification(workOrderHeaderId, group.GroupStationId));
            if (responseactivities.Succeeded)
            {
                var activitiesViewModel = _mapper.Map<List<WorkActivityViewModel>>(responseactivities.Data);
                if (activitiesViewModel.Count > 0)
                {
                    var concatUsersIds = "";
                    if (group.UsersList != null)
                    {
                        concatUsersIds = string.Join(',', group.UsersList);
                    }
                    activitiesViewModel.FirstOrDefault().AssignedUsersIds = concatUsersIds;
                    await _workActivityViewManager.UpdateAsync(_mapper.Map<WorkActivityDTO>(activitiesViewModel.FirstOrDefault()));
                    //foreach (var activity in activitiesViewModel)
                    //{
                    //    activity.AssignedUsersIds = concatUsersIds;
                    //    await _workActivityViewManager.UpdateAsync(_mapper.Map<WorkActivityDTO>(activity));
                    //}
                }

            }

        }
        public SelectList StationUserSelectList(string stationUsersIds)
        {
            var stationUsersViewModel = new List<UserViewModel>();
            if (stationUsersIds != null)
            {
                var idsArray = stationUsersIds.Split(',');
                var usersViewModel = _mapper.Map<List<UserViewModel>>(_identityContext.Users.Where(user => user.IsActive == true).ToList());

                foreach (var userId in idsArray)
                {
                    var user = usersViewModel.Select(user => user).Where(user => user.Id.Equals(userId)).FirstOrDefault();
                    if (user != null)
                    {
                        stationUsersViewModel.Add(user);
                    }
                }
            }
            return new SelectList(stationUsersViewModel, nameof(UserViewModel.Id), nameof(UserViewModel.FirstAndLastName), null, null);
        }

        [HttpPost]
        public async Task<JsonResult> AssignUsersToActivities(int id, WorkOrderHeaderViewModel workOrderHeaderViewModel)
        {
            try
            {
                foreach (var group in workOrderHeaderViewModel.WorkActivitiesGroupsByStation)
                {
                    await assignUsersToGroupActivitiesAsync(group, workOrderHeaderViewModel.Id);
                }
                _notify.Success(_localizer["Users assigned."]);
                var html = await _viewRenderer.RenderViewToStringAsync("_ViewDetails", workOrderHeaderViewModel);
                return new JsonResult(new { isValid = true, html = html });
            }
            catch (Exception)
            {
                _notify.Error(_localizer["Error while saving."]);
                var html = await _viewRenderer.RenderViewToStringAsync("_ViewDetails", workOrderHeaderViewModel);
                return new JsonResult(new { isValid = false, html = html });
                throw;
            }
        }

        [HttpPost]
        public async Task<JsonResult> setActivitiesActiveByStation(int workOrderHeaderId = 0, int stationId = 0)
        {
            try
            {
                List<WorkActivityViewModel> workActivitiesToUpdate = new List<WorkActivityViewModel>();
                var response = await _workOrderHeaderViewManager.FindBySpecification(new WorkOrderHeaderWithActivitiesSpecification(workOrderHeaderId));
                if (response.Succeeded)
                {
                    var workOrderHeaderViewModel = _mapper.Map<WorkOrderHeaderViewModel>(response.Data.FirstOrDefault());

                    foreach (WorkActivityViewModel item in workOrderHeaderViewModel.WorkOrders
                                                    .SelectMany(x => x.WorkOrderItems)
                                                    .SelectMany(z => z.WorkActivities)
                                                    .Where(y => y.StateActivity == false && y.ProductStation.StationId == stationId)
                                                    .ToList())
                    {
                        item.StateActivity = true;
                        item.DateToProduction = DateTime.Now;
                        workActivitiesToUpdate.Add(item);
                    }

                    var responseUpdate = await _workActivityViewManager.UpdateListAsync(_mapper.Map<IList<WorkActivityDTO>>(workActivitiesToUpdate));
                    if (responseUpdate.Succeeded)
                    {
                        _notify.Success(_localizer["Se envío a producir al puesto seleccionado."]);
                        return new JsonResult(new { isValid = true });
                    }
                }
                _notify.Error(_localizer["No se pudo completar la transacción"]);
                return new JsonResult(new { isValid = false });
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);

                return new JsonResult(new { isValid = false });
            }

        }

        [HttpPost]
        public async Task<JsonResult> setActivitiesActiveByStationMassive(int workOrderHeaderId, List<int> stationIds)
        {
            try
            {
                List<WorkActivityViewModel> workActivitiesToUpdate = new List<WorkActivityViewModel>();
                var response = await _workOrderHeaderViewManager.FindBySpecification(new WorkOrderHeaderWithActivitiesSpecification(workOrderHeaderId));
                if (response.Succeeded)
                {
                    var workOrderHeaderViewModel = _mapper.Map<WorkOrderHeaderViewModel>(response.Data.FirstOrDefault());

                    var workActivities = new List<WorkActivityViewModel>();
                    foreach (var stationId in stationIds)
                    {
                        foreach (var wa in workOrderHeaderViewModel.WorkOrders.SelectMany(x => x.WorkOrderItems).SelectMany(z => z.WorkActivities).Where(y => y.StateActivity == false).ToList())
                        {
                            if (wa.ProductStation.StationId == stationId)
                            {
                                wa.StateActivity = true;
                                wa.DateToProduction = DateTime.Now;
                                workActivitiesToUpdate.Add(wa);
                            }
                        }
                    }

                    var responseUpdate = await _workActivityViewManager.UpdateListAsync(_mapper.Map<IList<WorkActivityDTO>>(workActivitiesToUpdate));
                    if (responseUpdate.Succeeded)
                    {
                        _notify.Success(_localizer["All stations have been sent to produce."]);
                        return new JsonResult(new { isValid = true });
                    }
                }
                _notify.Error(_localizer["No se pudo completar la transacción"]);
                return new JsonResult(new { isValid = false });
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);

                return new JsonResult(new { isValid = false });
            }

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

    }
}
