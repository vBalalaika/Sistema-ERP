using AutoMapper;
using ERP.Application.Constants;
using ERP.Application.DTOs.Entities.Production;
using ERP.Application.DTOs.Entities.Sales;
using ERP.Application.Interfaces.ViewManagers.Production;
using ERP.Application.Interfaces.ViewManagers.ProductMod;
using ERP.Application.Interfaces.ViewManagers.Sales;
using ERP.Application.Specification.Production;
using ERP.Application.Specification.Production.WorkOrderHeaderSpecifications;
using ERP.Application.Specification.ProductMod;
using ERP.Application.Specification.ProductMod.StructureItemSpecifications;
using ERP.Application.Specification.ProductMod.StructureSpecifications;
using ERP.Application.Specification.Sales;
using ERP.Language;
using ERP.Web.Abstractions;
using ERP.Web.Areas.Production.Models;
using ERP.Web.Areas.ProductMod.Models;
using ERP.Web.Areas.Sales.Models;
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
    public class WorkOrderController : BaseController<WorkOrderController>
    {
        private readonly IWorkOrderViewManager _workOrderViewManager;
        private readonly IWorkOrderItemViewManager _workOrderItemViewManager;
        private readonly IWorkOrderHeaderViewManager _workOrderHeaderViewManager;
        private readonly IStationViewManager _stationViewManager;
        private readonly IOrderDetailViewManager _orderDetailViewManager;
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly IStructureItemViewManager _structureitemsViewManager;
        private readonly IStructureViewManager _structureViewManager;
        private readonly IProductViewManager _productViewManager;
        private readonly IWorkActivityViewManager _workActivityViewManager;
        public WorkOrderController(IWorkOrderViewManager viewManager, IStationViewManager stationViewManager,
           IOrderDetailViewManager orderDetailViewManager, IStringLocalizer<SharedResource> localizer, IStructureItemViewManager structureitemsViewManager,
           IStructureViewManager structureViewManager, IProductViewManager productViewManager, IWorkOrderHeaderViewManager workOrderHeaderViewManager,
           IWorkOrderItemViewManager workOrderItemViewManager, IWorkActivityViewManager workActivityViewManager)
        {
            _workOrderViewManager = viewManager;
            _workOrderItemViewManager = workOrderItemViewManager;
            _stationViewManager = stationViewManager;
            _structureitemsViewManager = structureitemsViewManager;
            _structureViewManager = structureViewManager;
            _orderDetailViewManager = orderDetailViewManager;
            _productViewManager = productViewManager;
            _workOrderHeaderViewManager = workOrderHeaderViewManager;
            _workActivityViewManager = workActivityViewManager;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            var model = new WorkOrderViewModel();
            return View(model);
        }

        public async Task<IActionResult> LoadAll()
        {
            var response = await _workOrderViewManager.LoadAll();
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<WorkOrderViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }

        [Authorize(Policy = Permissions.WorkOrders.View)]
        public async Task<JsonResult> OnGetCreateOrEdit(int id = 0)
        {
            if (id == 0)
            {
                var workOrderViewModel = new WorkOrderViewModel();
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", workOrderViewModel) });
            }
            else
            {
                var response = await _workOrderViewManager.GetByIdAsync(id);
                if (response.Succeeded)
                {
                    var workOrderViewModel = _mapper.Map<WorkOrderViewModel>(response.Data);
                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", workOrderViewModel) });
                }
                return null;
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostDeleteItem(int idWorkOrder, int idItem, bool isSparesOrStock = false, string workOrderIds = "")
        {
            if (idWorkOrder != 0)
            {
                try
                {
                    var responseworkorder = await _workOrderViewManager.FindBySpecification(new WorkOrderSpecification(idWorkOrder));
                    if (responseworkorder.Succeeded)
                    {
                        var viewModel = _mapper.Map<WorkOrderViewModel>(responseworkorder.Data.FirstOrDefault());
                        if (viewModel != null)
                        {
                            var levelDeletedItem = viewModel.WorkOrderItems.Where(item => item.Id == idItem).Select(item => item.Level).FirstOrDefault().Split('.');
                            var decendentsList = new List<WorkOrderItemViewModel>();
                            searchDecendents(idItem, viewModel.WorkOrderItems.ToList(), decendentsList);
                            decendentsList.Add(viewModel.WorkOrderItems.ToList().Where(item => item.Id == idItem).FirstOrDefault());
                            //Orden para borrar primero padres y luego hijos, sino tira error
                            decendentsList = decendentsList.OrderByDescending(item => item.Level.Split('.').Count()).ToList();
                            if (decendentsList != null)
                            {
                                foreach (var descendant in decendentsList)
                                {
                                    var responsedelete = await _workOrderItemViewManager.DeleteAsync(descendant.Id);
                                }
                            }

                            await UpdateMajorItems(viewModel, levelDeletedItem);

                            // Hay que refrescar segun _ViewAllPartial o _ViewAllPartialForSparesOrStock
                            if (!isSparesOrStock)
                            {
                                var responsegetworkorder = await _workOrderViewManager.FindBySpecification(new WorkOrderSpecification(idWorkOrder));
                                if (responsegetworkorder.Succeeded)
                                {
                                    var workorderviewModel = _mapper.Map<WorkOrderViewModel>(responsegetworkorder.Data.FirstOrDefault());
                                    if (workorderviewModel != null)
                                    {
                                        foreach (var woi in workorderviewModel.WorkOrderItems)
                                        {
                                            var wActivitiesToAdd = woi.WorkActivities.ToList();
                                            if (wActivitiesToAdd.Count > 0)
                                            {
                                                workorderviewModel.WorkOrderHeader.WorkActivities.AddRange(wActivitiesToAdd);
                                            }
                                        }
                                        var responseproducts = await _productViewManager.LoadAll();
                                        if (responseproducts.Succeeded)
                                        {
                                            var productsViewModel = _mapper.Map<List<ProductViewModel>>(responseproducts.Data);
                                            workorderviewModel.ProductSelectList = new SelectList(productsViewModel, nameof(ProductViewModel.Id), nameof(ProductViewModel.CodeAndDescription), null, null);
                                        }
                                        _notify.Success(_localizer["Work order item deleted."]);
                                        return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_ViewAllPartial", workorderviewModel) });
                                    }
                                }
                            }
                            else
                            {
                                var workOrdersId = workOrderIds.Split(",");
                                var woVMList = new List<WorkOrderViewModel>();
                                foreach (var woId in workOrdersId)
                                {
                                    var responseWO = await _workOrderViewManager.FindBySpecification(new WorkOrderSpecification(Convert.ToInt32(woId)));
                                    if (responseWO.Succeeded)
                                    {
                                        var woVM = _mapper.Map<WorkOrderViewModel>(responseWO.Data.FirstOrDefault());
                                        if (woVM != null)
                                        {
                                            woVMList.Add(woVM);

                                            foreach (var woi in woVM.WorkOrderItems)
                                            {
                                                var wActivitiesToAdd = woi.WorkActivities.ToList();
                                                if (wActivitiesToAdd.Count > 0)
                                                {
                                                    woVM.WorkOrderHeader.WorkActivities.AddRange(wActivitiesToAdd);
                                                }
                                            }
                                        }
                                    }
                                }

                                var responseproducts = await _productViewManager.LoadAll();
                                if (responseproducts.Succeeded)
                                {
                                    var productsViewModel = _mapper.Map<List<ProductViewModel>>(responseproducts.Data);
                                    woVMList.First().ProductSelectList = new SelectList(productsViewModel, nameof(ProductViewModel.Id), nameof(ProductViewModel.CodeAndDescription), null, null);
                                }
                                _notify.Success(_localizer["Work order item deleted."]);
                                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_ViewAllPartialForSparesOrStock", woVMList) });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _notify.Error(ex.Message);
                    return new JsonResult(new { isValid = false });
                }
            }
            return new JsonResult(new { isValid = false });
        }

        private async Task UpdateMajorItems(WorkOrderViewModel viewModel, string[] levelDeletedItem)
        {
            var finalLevelDeleted = levelDeletedItem.Last();
            var levelPrefixDeleted = levelDeletedItem.SkipLast(1).ToList();
            foreach (var item in viewModel.WorkOrderItems.ToList())
            {
                var level = item.Level.Split('.');
                var levelPrefixItem = item.Level.Split('.').Take(levelPrefixDeleted.Count()).ToList();
                var samePrefix = (level.Count() >= levelDeletedItem.Count()) && levelPrefixDeleted.SequenceEqual(levelPrefixItem);
                if (samePrefix && (Convert.ToInt32(level[levelPrefixItem.Count()]) > Convert.ToInt32(finalLevelDeleted)))
                {
                    level[levelPrefixItem.Count()] = (Convert.ToInt32(level[levelPrefixItem.Count()]) - 1).ToString();
                    item.Level = string.Join('.', level);
                    await _workOrderItemViewManager.UpdateAsync(_mapper.Map<WorkOrderItemDTO>(item));
                }

            }
        }

        public void searchDecendents(int fatherId, List<WorkOrderItemViewModel> items, List<WorkOrderItemViewModel> decendents)
        {
            var childsWorkOrderItems = items.Where(item => item.WorkOrderItemOfId == fatherId).ToList();
            decendents.AddRange(childsWorkOrderItems);
            foreach (var child in childsWorkOrderItems)
            {
                searchDecendents(child.Id, items, decendents);
            }

        }

        //[Authorize(Policy = Permissions.WorkOrders.Create)]
        public async Task<JsonResult> OnCreateWorkOrder(string strOrderDetailIds)
        {
            try
            {
                var orderDetailIds = strOrderDetailIds.Split(',');
                var toCreateWorkOrderViewModel = new ToCreateWorkOrderViewModel();
                var responseWorkOrderList = await _workOrderViewManager.LoadAll();
                toCreateWorkOrderViewModel.WorkOrderDate = DateTime.Now;

                foreach (var item in orderDetailIds)
                {
                    int OrderDetailId = Convert.ToInt32(item);
                    var responseOrderDetail = await _orderDetailViewManager.FindBySpecification(new OrderDetailWithAllSpecifications(OrderDetailId));
                    if (responseOrderDetail.Succeeded)
                    {
                        var orderDetail = _mapper.Map<OrderDetailViewModel>(responseOrderDetail.Data.FirstOrDefault());
                        toCreateWorkOrderViewModel.OrderDetailList.Add(orderDetail);
                    }
                }
                var responseStation = await _stationViewManager.LoadAll();
                if (responseStation.Succeeded)
                {
                    var stationListViewModel = _mapper.Map<List<StationViewModel>>(responseStation.Data);
                    toCreateWorkOrderViewModel.StationList = stationListViewModel;
                }

                if (toCreateWorkOrderViewModel.OrderDetailList.All(od => od.Product.SubCategory.CategoryId == 1) && toCreateWorkOrderViewModel.OrderDetailList.Count() > 1)
                {
                    _notify.Warning(_localizer["Attention, you can only send an item with category \"Machines and accessories\" to production."].ToString());
                    return new JsonResult(new { isValid = false });
                }
                else if (toCreateWorkOrderViewModel.OrderDetailList.Any(od => od.Product.SubCategory.CategoryId == 1) && toCreateWorkOrderViewModel.OrderDetailList.Any(od => od.Product.SubCategory.CategoryId != 1))
                {
                    _notify.Warning(_localizer["Attention, you can only send an item with category \"Machines and accessories\" to production."].ToString());
                    return new JsonResult(new { isValid = false });
                }
                else
                {
                    return await this.OnPostCreate(toCreateWorkOrderViewModel);
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);

                return new JsonResult(new { isValid = false });
            }


        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreate(ToCreateWorkOrderViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    WorkOrderHeaderViewModel WorkOrderHeader = new WorkOrderHeaderViewModel();
                    List<WorkOrderViewModel> WorkOrders = new List<WorkOrderViewModel>();
                    foreach (var detail in viewModel.OrderDetailList)
                    {
                        WorkOrderViewModel newWorkOrder = new WorkOrderViewModel();
                        newWorkOrder.OrderNumber = Convert.ToInt32(viewModel.NroWorkOrder);
                        var response = await _orderDetailViewManager.FindBySpecification(new OrderDetailWithAllSpecifications(detail.Id));
                        if (response.Succeeded)
                        {
                            OrderDetailViewModel OrderDetail = _mapper.Map<OrderDetailViewModel>(response.Data.FirstOrDefault());
                            newWorkOrder.OrderDetailId = detail.Id;

                            //WorkOrderItems
                            // Fran: 27/9/2022 Tengo que agregar el FatherWOI para que tenga en cuenta las estaciones del padre al generar las activities.

                            var FatherWOI = new WorkOrderItemViewModel
                            {
                                OrderStateId = 1,
                                ProductId = OrderDetail.ProductId,
                                Quantity = (int)OrderDetail.Quantity,
                                Level = "1",
                            };

                            if (OrderDetail.Structure != null)
                            {
                                newWorkOrder.NumberVersionStructure = OrderDetail.Structure.LastVersion.VersionNumber.ToString();
                                assignLastVersionItems(OrderDetail.Structure);
                                //await getFullWorkOrderItems(newWorkOrder, OrderDetail.Structure, "", new List<WorkOrderItemViewModel>(), FatherWOI, detail.Quantity);
                                await getFullWorkOrderItems(newWorkOrder, OrderDetail.Structure, "", new List<WorkOrderItemViewModel>(), FatherWOI, Convert.ToInt32(detail.Quantity));

                                // Fran: Esta línea genera un WOI extra que es el padre.
                                newWorkOrder.WorkOrderItems.Add(FatherWOI);
                            }
                            else
                            {
                                var WorkOrderItem = new WorkOrderItemViewModel();
                                WorkOrderItem.OrderStateId = 1;
                                WorkOrderItem.ProductId = OrderDetail.ProductId;
                                WorkOrderItem.Quantity = (int)OrderDetail.Quantity;
                                WorkOrderItem.Level = "1";
                                newWorkOrder.WorkOrderItems.Add(WorkOrderItem);
                            }

                            await AssignActivities(newWorkOrder.WorkOrderItems.ToList(), (List<StationViewModel>)viewModel.StationListSelected);
                            var responseworkorderheader = await _workOrderHeaderViewManager.FindBySpecification(new WorkOrderHeaderSpecification());
                            if (responseworkorderheader.Succeeded)
                            {
                                if (responseworkorderheader.Data.Count > 0)
                                {
                                    WorkOrderHeader.WorkOrderHeaderNumber = responseworkorderheader.Data.Max(x => x.WorkOrderHeaderNumber) + 1;
                                }
                                else
                                {
                                    WorkOrderHeader.WorkOrderHeaderNumber = 1;
                                }

                            }
                            WorkOrderHeader.WorkOrders.Add(newWorkOrder);
                        }
                    }
                    var workOrderHeaderDTO = _mapper.Map<WorkOrderHeaderDTO>(WorkOrderHeader);
                    var responseWorkOrderHeader = await _workOrderHeaderViewManager.CreateAsync(workOrderHeaderDTO);


                    if (responseWorkOrderHeader.Succeeded)
                    {
                        //Actualizo los orderDetails para q tengan el id de la workOrder asi podemos usarlo en la grilla. Ver de bajarlo de capa
                        var responseWorkOrderHeaderToUpdate = await _workOrderHeaderViewManager.FindBySpecification(new WorkOrderHeaderWithWOSpecification(responseWorkOrderHeader.Data));
                        if (responseWorkOrderHeaderToUpdate.Succeeded)
                        {
                            WorkOrderHeaderDTO workOrderHeaderVM = responseWorkOrderHeaderToUpdate.Data.FirstOrDefault();
                            foreach (WorkOrderDTO woItem in _mapper.Map<IList<WorkOrderDTO>>(workOrderHeaderVM.WorkOrders))
                            {
                                var responseOrderDetail = await _orderDetailViewManager.FindBySpecification(new OrderDetailEmptySpecification(woItem.OrderDetailId));
                                if (responseOrderDetail.Succeeded)
                                {
                                    OrderDetailDTO odVM = responseOrderDetail.Data.FirstOrDefault();
                                    odVM.WorkOrderId = woItem.Id;
                                    await _orderDetailViewManager.UpdateAsync(odVM);
                                }
                            }
                        }


                        _notify.Success(_localizer["Work Order with ID {0} Created.", responseWorkOrderHeader.Data]);
                        return new JsonResult(new { isValid = true, isSale = false, workOrderId = workOrderHeaderDTO.WorkOrders.FirstOrDefault().Id });
                    }
                    else
                    {
                        _notify.Error(_localizer["There was a problem creating work order."]);
                        return new JsonResult(new { isValid = false });
                    }
                }
                else
                {
                    _notify.Error(_localizer["There was a problem creating work order."]);
                    return new JsonResult(new { isValid = false });
                }
            }
            catch (Exception ex)
            {

                _notify.Error(_localizer["There was a problem creating work order."]);
                return new JsonResult(new { isValid = false });
            }

        }
        public IActionResult LoadWorkOrder(int workorderId = 0)
        {
            if (workorderId != 0)
            {
                var viewModel = new WorkOrderViewModel();
                viewModel.Id = workorderId;
                return View("WorkOrderDetail", viewModel);
            }
            else
            {
                _notify.Error("Work order no encontrada");
                return new JsonResult(new { isValid = false });
            }
        }
        public async Task<IActionResult> LoadWorkOrderData(int workorderId = 0)
        {
            if (workorderId != 0)
            {
                try
                {
                    var responseworkorder = await _workOrderViewManager.FindBySpecification(new WorkOrderSpecification(workorderId));
                    if (responseworkorder.Succeeded)
                    {
                        var viewModel = _mapper.Map<WorkOrderViewModel>(responseworkorder.Data.FirstOrDefault());
                        if (viewModel != null)
                        {
                            foreach (var woi in viewModel.WorkOrderItems)
                            {
                                var wActivitiesToAdd = woi.WorkActivities.ToList();
                                if (wActivitiesToAdd.Count > 0)
                                {
                                    viewModel.WorkOrderHeader.WorkActivities.AddRange(wActivitiesToAdd);
                                }
                            }
                            var responseproducts = await _productViewManager.LoadAll();
                            if (responseproducts.Succeeded)
                            {
                                var productsViewModel = _mapper.Map<List<ProductViewModel>>(responseproducts.Data);
                                viewModel.ProductSelectList = new SelectList(productsViewModel, nameof(ProductViewModel.Id), nameof(ProductViewModel.CodeAndDescription), null, null);
                            }

                            // Fran: Para no mostrar dos veces el padre Level="1"
                            viewModel.WorkOrderItems = viewModel.WorkOrderItems.GroupBy(x => x.Level).Select(y => y.First()).ToList();

                            return PartialView("_ViewAllPartial", viewModel);
                        }
                        else
                        {
                            _notify.Error("Work order no encontrada");
                            return new JsonResult(new { isValid = false });
                        }
                    }
                }

                catch (AutoMapperMappingException ex)
                {
                    _notify.Error(ex.Message);
                    throw;
                }
            }

            return null;
        }
        public IActionResult LoadWorkOrderForSparesOrStock(string workOrderIds)
        {
            if (workOrderIds != "")
            {
                var viewModel = new WorkOrderViewModel();
                ViewBag.workOrderIds = workOrderIds;
                return View("WorkOrderDetail", viewModel);
            }
            else
            {
                _notify.Error("Work order no encontrada");
                return new JsonResult(new { isValid = false });
            }
        }
        public async Task<IActionResult> LoadWorkOrderDataForSparesOrStock(string workOrderIds)
        {
            if (workOrderIds != "")
            {
                try
                {
                    var workOrdersId = workOrderIds.Split(",");
                    var woVMList = new List<WorkOrderViewModel>();
                    foreach (var woId in workOrdersId)
                    {
                        var responseworkorder = await _workOrderViewManager.FindBySpecification(new WorkOrderSpecification(Convert.ToInt32(woId)));
                        if (responseworkorder.Succeeded)
                        {
                            var woVM = _mapper.Map<WorkOrderViewModel>(responseworkorder.Data.FirstOrDefault());
                            if (woVM != null)
                            {
                                woVMList.Add(woVM);

                                foreach (var woi in woVM.WorkOrderItems)
                                {
                                    var wActivitiesToAdd = woi.WorkActivities.ToList();
                                    if (wActivitiesToAdd.Count > 0)
                                    {
                                        woVM.WorkOrderHeader.WorkActivities.AddRange(wActivitiesToAdd);
                                    }
                                }
                            }
                            else
                            {
                                _notify.Error("Work order no encontrada");
                                return new JsonResult(new { isValid = false });
                            }
                        }
                    }

                    var responseproducts = await _productViewManager.LoadAll();
                    if (responseproducts.Succeeded)
                    {
                        var productsViewModel = _mapper.Map<List<ProductViewModel>>(responseproducts.Data);
                        woVMList.First().ProductSelectList = new SelectList(productsViewModel, nameof(ProductViewModel.Id), nameof(ProductViewModel.CodeAndDescription), null, null);
                    }
                    return PartialView("_ViewAllPartialForSparesOrStock", woVMList);

                }
                catch (AutoMapperMappingException ex)
                {
                    throw;
                }
            }

            return null;
        }
        public async Task<IActionResult> AddItem(int productId = 0, int structureId = 0, string maxLevel = "-1", int workOrderId = 0, int quantity = 0)
        {
            if (productId != 0)
            {
                try
                {
                    var workOrderItems = new List<WorkOrderItemViewModel>();
                    var itemLevel = (Convert.ToInt32(maxLevel) + 1).ToString();
                    // Agregamos el item del producto padre
                    var fatherItem = new WorkOrderItemViewModel
                    {
                        OrderStateId = 1,
                        ProductId = productId,
                        Quantity = quantity,
                        Level = itemLevel,
                        WorkOrderId = workOrderId,
                    };
                    if (structureId != 0)
                    {
                        var responsestructure = await _structureViewManager.FindBySpecification(new StructureWithItemsSpecification(structureId));
                        if (responsestructure.Succeeded)
                        {
                            var newWorkOrder = new WorkOrderViewModel();
                            var structureViewModel = _mapper.Map<StructureViewModel>(responsestructure.Data.FirstOrDefault());
                            assignLastVersionItems(structureViewModel);
                            var items = await getFullWorkOrderItems(newWorkOrder, structureViewModel, itemLevel, new List<WorkOrderItemViewModel>(), fatherItem, fatherItem.Quantity);
                            items.Add(fatherItem);
                            await AssignActivities(items, new List<StationViewModel>());
                            workOrderItems.AddRange(items);
                        }
                    }
                    else
                    {
                        workOrderItems.Add(fatherItem);
                        // Se agrega para que se asigne una activity y se pueda cambiar el estado del item nuevo.
                        await AssignActivities(workOrderItems, new List<StationViewModel>());
                    }
                    await SaveItemsAsync(workOrderItems, workOrderId);

                    var responseworkorder = await _workOrderViewManager.FindBySpecification(new WorkOrderSpecification(workOrderId));
                    if (responseworkorder.Succeeded)
                    {
                        var viewModel = _mapper.Map<WorkOrderViewModel>(responseworkorder.Data.FirstOrDefault());
                        if (viewModel != null)
                        {
                            foreach (var woi in viewModel.WorkOrderItems)
                            {
                                var wActivitiesToAdd = woi.WorkActivities.ToList();
                                if (wActivitiesToAdd.Count > 0)
                                {
                                    viewModel.WorkOrderHeader.WorkActivities.AddRange(wActivitiesToAdd);
                                }
                            }
                            var responseproducts = await _productViewManager.LoadAll();
                            if (responseproducts.Succeeded)
                            {
                                var productsViewModel = _mapper.Map<List<ProductViewModel>>(responseproducts.Data);
                                viewModel.ProductSelectList = new SelectList(productsViewModel, nameof(ProductViewModel.Id), nameof(ProductViewModel.CodeAndDescription), null, null);
                            }
                            _notify.Success(_localizer["New item added."]);
                            return PartialView("_ViewAllPartial", viewModel);
                        }
                        else
                        {
                            _notify.Error("Ocurrio un problema al intentar agregar el item.");
                            return new JsonResult(new { isValid = false });
                        }
                    }
                }

                catch (AutoMapperMappingException ex)
                {
                    _notify.Error("Ocurrio un problema al intentar agregar el item." + ex.Message);
                    return new JsonResult(new { isValid = false });
                    throw;
                }
            }

            return null;
        }
        public async Task<JsonResult> ChangeWorkOrderItemState(int workOrderId, int workOrderItemId, bool state, bool isSparesOrStock = false, string workOrderIds = "")
        {
            if (workOrderId != 0)
            {
                try
                {
                    var responseworkorder = await _workOrderViewManager.FindBySpecification(new WorkOrderSpecification(workOrderId));
                    if (responseworkorder.Succeeded)
                    {
                        var viewModel = _mapper.Map<WorkOrderViewModel>(responseworkorder.Data.FirstOrDefault());
                        if (viewModel != null)
                        {
                            var decendentsList = new List<WorkOrderItemViewModel>();
                            searchDecendents(workOrderItemId, viewModel.WorkOrderItems.ToList(), decendentsList);
                            decendentsList.Add(viewModel.WorkOrderItems.ToList().Where(item => item.Id == workOrderItemId).FirstOrDefault());
                            if (decendentsList != null)
                            {
                                foreach (var descendant in decendentsList)
                                {
                                    foreach (var activity in descendant.WorkActivities)
                                    {
                                        activity.StateActivity = state;
                                        if (state)
                                        {
                                            activity.DateToProduction = DateTime.Now;
                                        }
                                        else
                                        {
                                            activity.DateToProduction = null;
                                        }
                                    }
                                    await _workActivityViewManager.UpdateListAsync(_mapper.Map<List<WorkActivityDTO>>(descendant.WorkActivities));
                                }
                            }

                            // Hay que refrescar segun _ViewAllPartial o _ViewAllPartialForSparesOrStock
                            if (!isSparesOrStock)
                            {
                                var responsegetworkorder = await _workOrderViewManager.FindBySpecification(new WorkOrderSpecification(workOrderId));
                                if (responsegetworkorder.Succeeded)
                                {
                                    var workorderviewModel = _mapper.Map<WorkOrderViewModel>(responsegetworkorder.Data.FirstOrDefault());
                                    if (workorderviewModel != null)
                                    {
                                        foreach (var woi in workorderviewModel.WorkOrderItems)
                                        {
                                            var wActivitiesToAdd = woi.WorkActivities.ToList();
                                            if (wActivitiesToAdd.Count > 0)
                                            {
                                                workorderviewModel.WorkOrderHeader.WorkActivities.AddRange(wActivitiesToAdd);
                                            }
                                        }
                                        var responseproducts = await _productViewManager.LoadAll();
                                        if (responseproducts.Succeeded)
                                        {
                                            var productsViewModel = _mapper.Map<List<ProductViewModel>>(responseproducts.Data);
                                            workorderviewModel.ProductSelectList = new SelectList(productsViewModel, nameof(ProductViewModel.Id), nameof(ProductViewModel.CodeAndDescription), null, null);
                                        }
                                        _notify.Success(_localizer["Work order item state changed."]);
                                        return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_ViewAllPartial", workorderviewModel) });
                                    }
                                }
                            }
                            else
                            {
                                var workOrdersId = workOrderIds.Split(",");
                                var woVMList = new List<WorkOrderViewModel>();
                                foreach (var woId in workOrdersId)
                                {
                                    var responseWO = await _workOrderViewManager.FindBySpecification(new WorkOrderSpecification(Convert.ToInt32(woId)));
                                    if (responseWO.Succeeded)
                                    {
                                        var woVM = _mapper.Map<WorkOrderViewModel>(responseWO.Data.FirstOrDefault());
                                        if (woVM != null)
                                        {
                                            woVMList.Add(woVM);

                                            foreach (var woi in woVM.WorkOrderItems)
                                            {
                                                var wActivitiesToAdd = woi.WorkActivities.ToList();
                                                if (wActivitiesToAdd.Count > 0)
                                                {
                                                    woVM.WorkOrderHeader.WorkActivities.AddRange(wActivitiesToAdd);
                                                }
                                            }
                                        }
                                    }
                                }

                                var responseproducts = await _productViewManager.LoadAll();
                                if (responseproducts.Succeeded)
                                {
                                    var productsViewModel = _mapper.Map<List<ProductViewModel>>(responseproducts.Data);
                                    woVMList.First().ProductSelectList = new SelectList(productsViewModel, nameof(ProductViewModel.Id), nameof(ProductViewModel.CodeAndDescription), null, null);
                                }
                                _notify.Success(_localizer["Work order item state changed."]);
                                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_ViewAllPartialForSparesOrStock", woVMList) });
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    _notify.Error(ex.Message);
                    return new JsonResult(new { isValid = false });
                }
            }
            return new JsonResult(new { isValid = false });
        }
        public async Task SaveItemsAsync(List<WorkOrderItemViewModel> items, int workOrderId)
        {
            try
            {
                items = items.OrderBy(item => item.Level.Split('.').Count()).ToList();
                foreach (var item in items)
                {
                    item.WorkOrderId = workOrderId;
                    var responseitem = await _workOrderItemViewManager.CreateAsync(_mapper.Map<WorkOrderItemDTO>(item));
                    item.Id = responseitem.Data;
                    assignWorkOrderItemOfId(items, item);

                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public void assignWorkOrderItemOfId(List<WorkOrderItemViewModel> allItems, WorkOrderItemViewModel posibleFather)
        {
            foreach (var item in allItems)
            {
                if (item.WorkOrderItemOf != null && item.WorkOrderItemOf.Level == posibleFather.Level)
                {
                    item.WorkOrderItemOf = null;
                    item.WorkOrderItemOfId = posibleFather.Id;
                }
            }

        }
        public async Task<List<WorkOrderItemViewModel>> getFullWorkOrderItems(WorkOrderViewModel workOrder, StructureViewModel structure, string levelPrefix, List<WorkOrderItemViewModel> fullWorkOrderItems, WorkOrderItemViewModel WOIFather = null, int fatherQuantity = 0)
        {
            foreach (var item in structure.StructureItems)
            {
                var WOI = new WorkOrderItemViewModel();
                var newPrefix = "";
                if (levelPrefix == "")
                {
                    newPrefix = levelPrefix + item.Order;
                }
                else
                {
                    newPrefix = levelPrefix + "." + item.Order;
                }
                if (fatherQuantity != 0)
                {
                    item.Quantity = item.Quantity * fatherQuantity;
                }
                WOI.Level = newPrefix;
                WOI.StructureId = item.SonStructureId;
                if (item.SonProductId != null)
                {
                    WOI.ProductId = item.SonProductId;
                }
                else
                {
                    WOI.ProductId = item.SonStructure.ProductId;
                }
                WOI.Quantity = item.Quantity;
                WOI.OrderStateId = 1;
                if (WOIFather != null)
                {
                    WOI.WorkOrderItemOf = WOIFather;
                }

                fullWorkOrderItems.Add(WOI);

                if (item.SonStructure != null)
                {
                    var response = await _structureViewManager.FindBySpecification(new StructureWithItemsSpecification(item.SonStructure.Id));
                    if (response.Succeeded)
                    {
                        var viewModel = _mapper.Map<StructureViewModel>(response.Data[0]);
                        assignLastVersionItems(viewModel);
                        await getFullWorkOrderItems(workOrder, viewModel, newPrefix, fullWorkOrderItems, WOI, item.Quantity);
                    }
                }
            }
            workOrder.WorkOrderItems = fullWorkOrderItems;
            return fullWorkOrderItems;
        }

        public async Task<WorkOrderItemViewModel> AssignActivities(List<WorkOrderItemViewModel> workOrderItems, List<StationViewModel>? selectedStations)
        {
            var responstations = await _stationViewManager.LoadAll();
            var stationviewmodel = new List<StationViewModel>();
            if (responstations.Succeeded)
            {
                stationviewmodel = _mapper.Map<List<StationViewModel>>(responstations.Data);
            }
            for (int index = 0; index < workOrderItems.Count; index++)
            {
                var responseProductStations = await _productViewManager.FindBySpecification(new ProductWithRelProductStationsSpecification((int)workOrderItems[index].ProductId));
                if (responseProductStations.Succeeded)
                {
                    var product = _mapper.Map<ProductViewModel>(responseProductStations.Data.FirstOrDefault());
                    var activitiesList = new List<WorkActivityViewModel>();

                    for (int i = 0; i < product.RelProductStations.Count; i++)
                    {
                        var newWorkActivity = new WorkActivityViewModel();
                        //Si no es el último Relproductstation/actividad
                        if (product.RelProductStations.Count != i + 1)
                        {
                            newWorkActivity.NextStation = product.RelProductStations[i + 1].Station.AbreviationDescription;
                        }
                        //Si es la ultima actividad
                        else
                        {
                            // Si tiene padre
                            if (workOrderItems[index].WorkOrderItemOf != null)
                            {
                                var responseProductStationsFather = await _productViewManager.FindBySpecification(new ProductWithRelProductStationsSpecification((int)workOrderItems[index].WorkOrderItemOf.ProductId));
                                var fatherProduct = _mapper.Map<ProductViewModel>(responseProductStationsFather.Data.FirstOrDefault());
                                // Si el padre tiene actividades, asignamos la 1er estacion del padre como nextstation
                                if (fatherProduct.RelProductStations.Count > 0)
                                {
                                    newWorkActivity.NextStation = fatherProduct.RelProductStations[0].Station.AbreviationDescription;
                                }
                            }

                        }
                        newWorkActivity.ProductStationId = product.RelProductStations[i].Id;
                        //siempre es else
                        if (selectedStations.Any(st => st.Id == product.RelProductStations[i].StationId))
                        {
                            newWorkActivity.StateActivity = true;
                            newWorkActivity.DateToProduction = DateTime.Now;
                        }
                        else
                        {
                            newWorkActivity.StateActivity = false;
                        }

                        await assignDefaultUserAsync(newWorkActivity, product.RelProductStations[i].StationId);
                        var activitystation = stationviewmodel.Where(station => station.Id == product.RelProductStations[i].StationId).FirstOrDefault();
                        if (activitystation.WorkOrderDisplayType != null)
                        {
                            activitiesList.Add(newWorkActivity);
                        }

                    }
                    workOrderItems[index].WorkActivities = activitiesList;
                }
            }
            return null;
        }
        // Asignamos a la actividad el usuario por defecto de la estacion
        public async Task assignDefaultUserAsync(WorkActivityViewModel activity, int stationId)
        {
            var responsestation = await _stationViewManager.GetByIdAsync(stationId);
            if (responsestation.Succeeded)
            {
                var stationViewModel = _mapper.Map<StationViewModel>(responsestation.Data);
                activity.AssignedUsersIds = stationViewModel.DefaultUser;
            }
        }
        public async void assignLastVersionItems(StructureViewModel structure)
        {
            var itemsresponse = await _structureitemsViewManager.FindBySpecification(new StructureItemsByStructureIdWithDataSpecification(structure.Id, (int)structure.LastVersion.VersionNumber));
            if (itemsresponse.Succeeded)
            {
                structure.StructureItems = _mapper.Map<List<StructureItemViewModel>>(itemsresponse.Data);
            }
        }
        public async void assignSpecificVersionItems(WorkOrderViewModel workorder)
        {
            var itemsresponse = await _structureitemsViewManager.FindBySpecification(new StructureItemsByStructureIdWithDataSpecification(workorder.OrderDetail.Structure.Id, Convert.ToInt32(workorder.NumberVersionStructure)));
            if (itemsresponse.Succeeded)
            {
                workorder.OrderDetail.Structure.StructureItems = _mapper.Map<List<StructureItemViewModel>>(itemsresponse.Data);
            }
        }
    }
}
