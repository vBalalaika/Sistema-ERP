using AutoMapper;
using ERP.Application.Constants;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Application.Interfaces.ViewManagers.Production;
using ERP.Application.Interfaces.ViewManagers.ProductMod;
using ERP.Application.Interfaces.ViewManagers.Sales;
using ERP.Application.Specification.Production.WorkOrderItemSpecifications;
using ERP.Application.Specification.ProductMod;
using ERP.Application.Specification.ProductMod.StructureItemSpecifications;
using ERP.Application.Specification.ProductMod.StructureSpecifications;
using ERP.Application.Specification.ProductMod.SupplyVoltageSpecifications;
using ERP.Application.Specification.Sales;
using ERP.Language;
using ERP.Web.Abstractions;
using ERP.Web.Areas.ProductMod.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Web.Areas.ProductMod.Controllers
{
    [Area("ProductMod")]
    public class StructureController : BaseController<StructureController>
    {
        private readonly IStructureViewManager _viewManager;
        private readonly IStructureVersionViewManager _structureversionviewManager;
        private readonly IStructureItemViewManager _structureitemsViewManager;
        private readonly IProductViewManager _productViewManager;
        private readonly IProductSupplyVoltageViewManager _productSupplyVoltageViewManager;
        private readonly IWorkOrderItemViewManager _workOrderItemViewManager;
        private readonly IOrderDetailViewManager _orderDetailViewManager;
        private readonly IPriceViewManager _priceViewManager;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public StructureController(IStructureViewManager viewManager, IStructureVersionViewManager structureversionviewManager, IStructureItemViewManager structureitemsViewManager, IProductViewManager productViewManager, IProductSupplyVoltageViewManager productSupplyVoltageViewManager, IWorkOrderItemViewManager workOrderItemViewManager, IOrderDetailViewManager orderDetailViewManager, IStringLocalizer<SharedResource> localizer, IPriceViewManager priceViewManager)
        {
            _viewManager = viewManager;
            _structureversionviewManager = structureversionviewManager;
            _structureitemsViewManager = structureitemsViewManager;
            _productViewManager = productViewManager;
            _productSupplyVoltageViewManager = productSupplyVoltageViewManager;
            _workOrderItemViewManager = workOrderItemViewManager;
            _orderDetailViewManager = orderDetailViewManager;
            _localizer = localizer;
            _priceViewManager = priceViewManager;
        }

        public async Task<IActionResult> Index(int productId)
        {
            var viewModel = new ProductStructureViewModel();
            await populateAllData(viewModel, productId);
            return View(viewModel);
        }

        public async Task<IActionResult> LoadAll(int productId = 0)
        {
            if (productId != 0)
            {
                try
                {
                    var viewModel = new ProductStructureViewModel();
                    await populateAllData(viewModel, productId);
                    return PartialView("_ViewAll", viewModel);
                }
                catch (AutoMapperMappingException ex)
                {
                    throw;
                }
            }

            return null;
        }

        [Authorize(Policy = Permissions.Structure.View)]
        public async Task<IActionResult> LoadProduct(int productId = 0, int structureId = 0, bool isStandard = false)
        {
            if (productId != 0)
            {
                try
                {
                    var viewModel = new ProductStructureViewModel();
                    await populateAllData(viewModel, productId, structureId, isStandard);
                    return View("Structure", viewModel);
                }
                catch (AutoMapperMappingException ex)
                {
                    throw;
                }
            }

            return null;
        }

        public async Task<IActionResult> LoadStructure(int productId = 0, int structureId = 0)
        {
            if (productId != 0)
            {
                try
                {
                    var viewModel = new ProductStructureViewModel();
                    await populateAllData(viewModel, productId, structureId);
                    return PartialView("_ViewStructure", viewModel);
                }
                catch (AutoMapperMappingException ex)
                {
                    throw;
                }
            }

            return null;
        }

        [Authorize(Policy = Permissions.Structure.View)]
        public async Task<JsonResult> OnGetCreateOrEdit(int structureId = 0, int productId = 0, string viewToRender = "")
        {
            try
            {
                if (structureId == 0)
                {
                    var structureViewModel = new StructureViewModel();
                    structureViewModel.ProductId = productId;
                    structureViewModel.IsStandard = true;
                    structureViewModel.viewToRender = viewToRender;
                    await populateStructureData(structureViewModel, productId);
                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", structureViewModel) });
                }
                else
                {
                    var response = await _viewManager.FindBySpecification(new StructureWithItemsSpecification(structureId));
                    if (response.Succeeded)
                    {
                        var structureViewModel = _mapper.Map<StructureViewModel>(response.Data.FirstOrDefault());
                        structureViewModel.viewToRender = viewToRender;
                        assignLastVersionItems(structureViewModel);
                        await populateStructureData(structureViewModel, productId);
                        return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", structureViewModel) });
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(new { isValid = false });
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(int id, StructureViewModel structure, string viewToRender = "")
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var canAddOrEdit = true;
                    if (structure.IsBase)
                    {
                        var baseStructureId = await validateCanBase(structure.ProductId);
                        if (baseStructureId != 0 && baseStructureId != structure.Id)
                        {
                            canAddOrEdit = false;
                        }
                    }
                    if (structure.IsStandard)
                    {
                        var standardStructureId = await validateCanStandard(structure.ProductId);
                        if (standardStructureId != 0 && standardStructureId != structure.Id)
                        {
                            canAddOrEdit = false;
                        }
                    }

                    if (canAddOrEdit)
                    {
                        await assignVersion(structure);
                        if (id == 0)
                        {
                            structure.Product = null;
                            clearStructureItemsSonProductAndSonStructure(structure);
                            var structureDTO = _mapper.Map<StructureDTO>(structure);
                            var result = await _viewManager.CreateAsync(structureDTO);
                            if (result.Succeeded)
                            {
                                structure.Id = result.Data;

                                _notify.Success(_localizer["Structure created.", result.Data]);
                            }
                            else _notify.Error(result.Message);
                        }
                        else
                        {
                            var structureDTO = _mapper.Map<StructureDTO>(structure);
                            var result = await _viewManager.UpdateAsync(structureDTO);
                            if (result.Succeeded) _notify.Information(_localizer["Structure update.", result.Data]);
                        }

                        var viewModel = new ProductStructureViewModel();
                        await populateAllData(viewModel, structure.ProductId, structure.Id);
                        var html = await _viewRenderer.RenderViewToStringAsync(viewToRender, viewModel);
                        return new JsonResult(new { isValid = true, html = html });
                    }
                    else
                    {
                        _notify.Error(_localizer["The product already has a base/standard structure"]);
                        await populateStructureData(structure, structure.ProductId);
                        var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", structure);
                        return new JsonResult(new { isValid = false, html = html });
                    }
                }
                else
                {
                    await populateStructureData(structure, structure.ProductId);
                    var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", structure);
                    return new JsonResult(new { isValid = false, html = html });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", structure);
                return new JsonResult(new { isValid = false, html = html });
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostDelete(int structureId, int productId)
        {
            try
            {
                // Agregar validacion para OrderDetail -> Structure
                // Tengo que verificar que la estructura que se quiere eliminar no posee OT relacionadas, si no posee la elimino, sino advierto al usuario que debe primero eliminar la OT.
                var woisResponse = await _workOrderItemViewManager.FindBySpecification(new WorkOrderItemByStructureIdAndProductIdSpecification(structureId, productId));
                var ordersResponse = await _orderDetailViewManager.FindBySpecification(new OrderDetailByStructureIdSpecification(structureId));
                var pricesResponse = await _priceViewManager.FindBySpecification(new PriceByStructureIdSpecification(structureId));
                if (woisResponse.Succeeded && ordersResponse.Succeeded && pricesResponse.Succeeded)
                {
                    if (ordersResponse.Data.Count() == 0)
                    {
                        if (woisResponse.Data.Count() == 0)
                        {
                            if (pricesResponse.Data.Count() == 0)
                            {
                                var deleteCommand = await _viewManager.DeleteAsync(structureId);
                                if (deleteCommand.Succeeded)
                                {
                                    var viewModel = new ProductStructureViewModel();
                                    viewModel.SelectedStructure = null;
                                    _notify.Information(_localizer["Structure deleted.", structureId]);
                                    await populateAllData(viewModel, productId);
                                    var html = await _viewRenderer.RenderViewToStringAsync("Structure", viewModel);
                                    return new JsonResult(new { isValid = true, html = html });
                                }
                                else
                                {
                                    _notify.Error(deleteCommand.Message);
                                    return new JsonResult(new { isValid = false, html = "" });
                                }
                            }
                            else
                            {
                                _notify.Information(_localizer["The structure you want to delete has a price assigned to it, please delete this price first, and then delete the structure."]);
                                return new JsonResult(new { isValid = false, html = "" });
                            }
                        }
                        else
                        {
                            _notify.Information(_localizer["The structure you want to delete has one or more work orders associated with it, please first delete these orders and then delete this structure."]);
                            return new JsonResult(new { isValid = false, html = "" });
                        }
                    }
                    else
                    {
                        _notify.Information(_localizer["The structure you want to delete has one or more orders associated with it, first delete these orders and then delete the structure."]);
                        return new JsonResult(new { isValid = false, html = "" });
                    }
                }
                else
                {
                    return new JsonResult(new { isValid = false, html = "" });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return new JsonResult(new { isValid = false, html = "" });
            }
        }

        public async Task<JsonResult> LoopControl(int structureId, int structureToAddId)
        {
            var structureviewmodel = new StructureViewModel();
            var responseStructure = await _viewManager.FindBySpecification(new StructureWithItemsSpecification(structureToAddId));
            if (responseStructure.Succeeded)
            {
                structureviewmodel = _mapper.Map<StructureViewModel>(responseStructure.Data[0]);
                assignLastVersionItems(structureviewmodel);
                await getFullStructure(structureviewmodel, "", new List<StructureItemViewModel>());
            }
            var canAdd = !structureviewmodel.StructureItems.Any(item => item.SonStructureId == structureId);
            if (structureId == structureToAddId)
            {
                canAdd = false;
            }
            return new JsonResult(new { canAdd = canAdd });
        }

        public async Task<StructureViewModel> getFullStructure(StructureViewModel structure, string levelPrefix, List<StructureItemViewModel> fullstructure, int fatherQuantity = 0)
        {
            foreach (var item in structure.StructureItems)
            {
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
                item.Level = newPrefix;
                fullstructure.Add(item);
                if (item.SonStructure != null)
                {
                    var response = await _viewManager.FindBySpecification(new StructureWithItemsSpecification(item.SonStructure.Id));
                    if (response.Succeeded)
                    {
                        var viewModel = _mapper.Map<StructureViewModel>(response.Data[0]);
                        assignLastVersionItems(viewModel);
                        await getFullStructure(viewModel, newPrefix, fullstructure, item.Quantity);
                    }
                }
            }
            structure.StructureItems = fullstructure;

            return structure;
        }

        public async Task<int> validateCanBase(int productId)
        {
            var response = await _viewManager.FindBySpecification(new StructureBaseByProductSpecification(productId));
            if (response.Succeeded)
            {
                if (response.Data.Count != 0)
                {
                    return response.Data.First().Id;
                }
            }
            return 0;
        }

        public async Task<int> validateCanStandard(int productId)
        {
            var response = await _viewManager.FindBySpecification(new StructureStandarByProductSpecification(productId));
            if (response.Succeeded)
            {
                if (response.Data.Count != 0)
                {
                    return response.Data.First().Id;
                }
            }
            return 0;
        }

        public void clearStructureItemsSonProductAndSonStructure(StructureViewModel structure)
        {
            if (structure.StructureItems != null)
            {
                foreach (var item in structure.StructureItems)
                {
                    item.SonProduct = null;
                    item.SonStructure = null;
                }
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

        public async Task<int> assignVersion(StructureViewModel structure)
        {
            var version = new StructureVersionViewModel();
            version.Comment = "Versionado automatico desde ERP Web";
            if (structure.LastVersion == null)
            {
                version.VersionNumber = 1;
            }
            else
            {
                version.VersionNumber = (int)(structure.LastVersion.VersionNumber + 1);
            }
            var versionresponse = await _structureversionviewManager.CreateAsync(_mapper.Map<StructureVersionDTO>(version));
            var versionid = 0;
            if (versionresponse.Succeeded)
            {
                versionid = versionresponse.Data;
            }
            structure.LastVersionId = versionid;
            return versionid;
        }

        public async Task<JsonResult> GetStructuresByProduct(int productId)
        {
            var structures = await _viewManager.FindBySpecification(new StructureByProductIdSpecification(productId));
            var structuresviewmodel = _mapper.Map<List<StructureViewModel>>(structures.Data);
            if (structures.Succeeded)
            {
                return new JsonResult(new { isValid = true, structures = structuresviewmodel });
            }
            else
            {
                return null;
            }
        }

        public async Task<JsonResult> GetSupplyVoltages()
        {
            var supplyvoltageResponse = await _productViewManager.GetSupplyVoltages();
            if (supplyvoltageResponse.Succeeded)
            {
                var supplyvoltageViewModel = _mapper.Map<List<SupplyVoltageDTO>>(supplyvoltageResponse.Data);
                return new JsonResult(new { isValid = true, voltages = supplyvoltageViewModel });
            }
            else
            {
                return null;
            }
        }

        public async Task<ProductStructureViewModel> populateAllData(ProductStructureViewModel viewModel, int productId = 0, int structureId = 0, bool isStandard = false)
        {
            if (productId != 0)
            {
                // Carga de producto y sus estructuras para el combo
                var response = await _productViewManager.FindBySpecification(new ProductWithStructuresSpecification(productId));
                if (response.Succeeded)
                {
                    viewModel.Product = _mapper.Map<ProductViewModel>(response.Data.FirstOrDefault());
                    var structuresViewModel = _mapper.Map<List<StructureViewModel>>(viewModel.Product.Structures);
                    viewModel.Structures = structuresViewModel;

                    if (isStandard)
                    {
                        if (structuresViewModel.Any(x => x.IsStandard))
                        {
                            var structureStandardResponse = await _viewManager.FindBySpecification(new StructureWithItemsSpecification(productId, true));
                            if (structureStandardResponse.Succeeded)
                            {
                                var structureStandardviewmodel = _mapper.Map<StructureViewModel>(structureStandardResponse.Data.FirstOrDefault());
                                assignLastVersionItems(structureStandardviewmodel);
                                viewModel.SelectedStructure = structureStandardviewmodel;
                                await getFullStructure(viewModel.SelectedStructure, "", new List<StructureItemViewModel>());
                            }
                        }
                    }
                }
            }
            if (structureId != 0)
            {
                var responseStructure = await _viewManager.FindBySpecification(new StructureWithItemsSpecification(structureId));
                if (responseStructure.Succeeded)
                {
                    var structureviewmodel = _mapper.Map<StructureViewModel>(responseStructure.Data.FirstOrDefault());
                    assignLastVersionItems(structureviewmodel);
                    viewModel.SelectedStructure = structureviewmodel;
                    await getFullStructure(viewModel.SelectedStructure, "", new List<StructureItemViewModel>());
                }
            }
            return viewModel;
        }

        public async Task<StructureViewModel> populateStructureData(StructureViewModel viewModel, int productId = 0)
        {
            if (productId != 0)
            {
                var productsResponse = await _productViewManager.FindBySpecification(new ProductsExcludingSelfSpecification(productId));
                if (productsResponse.Succeeded)
                {
                    var productsviewmodel = _mapper.Map<List<ProductViewModel>>(productsResponse.Data);
                    viewModel.ProductsList = productsviewmodel.OrderBy(p => p.Description).ToList();
                }
                var response = await _productViewManager.FindBySpecification(new ProductWithStructuresSpecification(productId));
                if (response.Succeeded)
                {
                    viewModel.Product = _mapper.Map<ProductViewModel>(response.Data[0]);
                }
                var productSupplyVoltages = await _productSupplyVoltageViewManager.FindBySpecification(new ProductSupplyVoltageByProductIdSpecification(productId));
                if (productSupplyVoltages.Succeeded)
                {
                    if (productSupplyVoltages.Data.Count > 0)
                    {
                        viewModel.SupplyVoltages = new SelectList((from psv in productSupplyVoltages.Data.ToList() select new { Id = psv.SupplyVoltage.Id, Description = psv.SupplyVoltage.Description }), "Id", "Description", null);
                    }
                }
            }

            return viewModel;
        }
    }
}