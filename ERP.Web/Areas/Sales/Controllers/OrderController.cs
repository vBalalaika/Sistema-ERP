using AspNetCoreHero.Results;
using ERP.Application.DTOs.Entities.Clients;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Application.DTOs.Entities.Purchases.MissingProducts;
using ERP.Application.DTOs.Entities.Sales;
using ERP.Application.Interfaces.ViewManagers.Clients;
using ERP.Application.Interfaces.ViewManagers.Commons;
using ERP.Application.Interfaces.ViewManagers.Production;
using ERP.Application.Interfaces.ViewManagers.ProductMod;
using ERP.Application.Interfaces.ViewManagers.Purchases.MissingProducts;
using ERP.Application.Interfaces.ViewManagers.Sales;
using ERP.Application.Specification.Clients;
using ERP.Application.Specification.Production.WorkOrderSpecifications;
using ERP.Application.Specification.ProductMod;
using ERP.Application.Specification.ProductMod.StructureItemSpecifications;
using ERP.Application.Specification.ProductMod.StructureSpecifications;
using ERP.Application.Specification.ProductMod.SupplyVoltageSpecifications;
using ERP.Application.Specification.Purchases.MissingProducts;
using ERP.Application.Specification.Sales;
using ERP.Infrastructure.DbContexts;
using ERP.Infrastructure.Identity.Models;
using ERP.Language;
using ERP.Web.Abstractions;
using ERP.Web.Areas.Clients.Models;
using ERP.Web.Areas.ProductMod.Models;
using ERP.Web.Areas.Purchases.Models.MissingProduct;
using ERP.Web.Areas.Sales.Models;
using ERP.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using OfficeOpenXml;
using Rotativa.AspNetCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Web.Areas.Sales.Controllers
{
    [Area("Sales")]
    public class OrderController : BaseController<OrderController>
    {
        private readonly IOrderHeaderViewManager _OrderHeaderViewManager;
        private readonly IOrderDetailViewManager _OrderDetailViewManager;
        private readonly IStringLocalizer<SharedResource> _localizer;

        private readonly IProductViewManager _productViewManager;
        private readonly IClientViewManager _clientViewManager;
        private readonly IDocumentTypeViewManager _documentTypeViewManager;
        private readonly IOperationStateViewManager _operationStateViewManager;
        private readonly ICountryViewManager _countryViewManager;
        private readonly IStructureViewManager _structureViewManager;
        private readonly IProductSupplyVoltageViewManager _productSupplyVoltageViewManager;
        private readonly IWorkOrderViewManager _workOrderViewManager;
        private readonly IWorkOrderItemViewManager _workOrderItemViewManager;
        private readonly IOrderStateViewManager _orderStateViewManager;
        private readonly IStructureItemViewManager _structureItemViewManager;
        private readonly IDeliveryDateHistoryViewManager _deliveryDateHistoryViewManager;
        private readonly IMissingProductViewManager _missingProductViewManager;

        private readonly IPriceViewManager _priceViewManager;

        private readonly ISaleOperationViewManager _saleOperationViewManager;

        private IdentityContext _identityContext;

        private readonly UserManager<ApplicationUser> _userManager;

        private List<ProductDTO> productsToUpdateExistences = new List<ProductDTO>();

        public OrderController(IOrderHeaderViewManager orderHeaderViewManager, IOrderDetailViewManager orderDetailViewManager, IStringLocalizer<SharedResource> localizer, IProductViewManager productViewManager, IClientViewManager clientViewManager, IDocumentTypeViewManager documentTypeViewManager, IOperationStateViewManager operationStateViewManager, ICountryViewManager countryViewManager, IStructureViewManager structureViewManager, IProductSupplyVoltageViewManager productSupplyVoltageViewManager, IWorkOrderViewManager workOrderViewManager, IWorkOrderItemViewManager workOrderItemViewManager, IOrderStateViewManager orderStateViewManager, IStructureItemViewManager structureItemViewManager, IPriceViewManager priceViewManager, ISaleOperationViewManager saleOperationViewManager, IDeliveryDateHistoryViewManager deliveryDateHistoryViewManager, IdentityContext identityContext, UserManager<ApplicationUser> userManager, IMissingProductViewManager missingProductViewManager)
        {
            _OrderHeaderViewManager = orderHeaderViewManager;
            _OrderDetailViewManager = orderDetailViewManager;
            _localizer = localizer;
            _productViewManager = productViewManager;
            _clientViewManager = clientViewManager;
            _documentTypeViewManager = documentTypeViewManager;
            _operationStateViewManager = operationStateViewManager;
            _countryViewManager = countryViewManager;
            _structureViewManager = structureViewManager;
            _productSupplyVoltageViewManager = productSupplyVoltageViewManager;
            _workOrderViewManager = workOrderViewManager;
            _workOrderItemViewManager = workOrderItemViewManager;
            _orderStateViewManager = orderStateViewManager;
            _structureItemViewManager = structureItemViewManager;
            _priceViewManager = priceViewManager;
            _saleOperationViewManager = saleOperationViewManager;
            _identityContext = identityContext;
            _userManager = userManager;
            _deliveryDateHistoryViewManager = deliveryDateHistoryViewManager;
            _missingProductViewManager = missingProductViewManager;
        }

        public IActionResult Index(bool? sale)
        {
            var model = new OrderHeaderViewModel();
            if (sale != null)
            {
                if ((bool)sale)
                {
                    ViewData["OrderTitle"] = _localizer["Sale orders"].ToString();
                    model.isSale = true;
                    ViewBag.onlySpares = false;
                }
                else
                {
                    ViewData["OrderTitle"] = _localizer["Production orders"].ToString();
                    model.isSale = false;
                    ViewBag.onlySpares = false;
                }
            }
            else
            {
                // Repuestos vendidos - Menu Servicio técnico
                ViewData["OrderTitle"] = _localizer["Spare parts sold"].ToString();
                model.isSale = true;
                ViewBag.onlySpares = true;
            }

            return View(model);
        }

        public async Task<IActionResult> ExportToExcel(bool isSale, string columnFilter_2, string columnFilter_3, string columnFilter_4, string columnFilter_5, string columnFilter_6,
            string columnFilter_7, string columnFilter_8, string columnFilter_9, string columnFilter_10, string columnFilter_11, string columnFilter_12, string columnFilter_13, string columnFilter_14,
            string columnFilter_15, string columnFilter_16, string columnFilter_17, string columnFilter_18, string columnFilter_19, bool filter_checkAll, bool filter_checkSimplemak,
            bool filter_checkImported, bool filter_checkMachines, bool filter_checkAccesories, bool filter_checkSpares, bool filter_checkForStock, int colIndexOrder, string colOrderDirection, string dateFromFilterValue, string dateToFilterValue, bool onlySpares)
        {
            try
            {
                IEnumerable<OrderDetailViewModel> Orders = null;
                Result<IReadOnlyList<OrderDetailDTO>> orders = null;
                if (isSale)
                {
                    // For sale orders
                    orders = await _OrderDetailViewManager.FindBySpecification(new OrderDetailWithAllSpecifications(true));
                }
                else
                {
                    // For production orders
                    orders = await _OrderDetailViewManager.FindBySpecification(new OrderDetailWithAllSpecifications(4, true));
                }

                if (orders.Succeeded)
                {
                    var viewModel = _mapper.Map<List<OrderDetailViewModel>>(orders.Data);

                    // Work with new line on Startup.cs which ignore cycles references.
                    Orders = viewModel;

                    // TODO: Eliminar circularReferenceWithHeaders.
                    //Orders = await circularReferenceWithHeaders(viewModel, isSale);

                    HashSet<OrderDetailViewModel> ordersAux = new HashSet<OrderDetailViewModel>();
                    IEnumerable<OrderDetailViewModel> filterList = new List<OrderDetailViewModel>();

                    if (isSale)
                    {
                        Orders = Orders.Where(o => o.OrderHeader != null && o.OrderHeader.Client != null);

                        // Filter orders which belongs to a sale order
                        List<OrderDetailViewModel> orderDetailsAux = new List<OrderDetailViewModel>();
                        orderDetailsAux = Orders.ToList();
                        orderDetailsAux.RemoveAll(od => od.BelongsToASaleOrder == true);

                        Orders = orderDetailsAux;

                        // From date -> To date
                        Orders = Orders.Where(o => o.OrderHeader != null && (o.OrderHeader.OrderDate >= Convert.ToDateTime(dateFromFilterValue) && o.OrderHeader.OrderDate <= Convert.ToDateTime(dateToFilterValue)));

                        //Nº OV/OP
                        if (!string.IsNullOrEmpty(columnFilter_2))
                        {
                            Orders = Orders.Where(o => o.OrderHeader != null && o.OrderHeader.Number.ToString().ToLower().Contains(columnFilter_2.ToLower())).ToList();
                        }
                        // Fecha OV/OP
                        if (!string.IsNullOrEmpty(columnFilter_3))
                        {
                            Orders = Orders.Where(o => o.OrderHeader != null && o.OrderHeader.OrderDate.ToString("dd/MM/yyyy").Contains(columnFilter_3)).ToList();
                        }
                        // Fecha de entrega
                        if (!string.IsNullOrEmpty(columnFilter_4))
                        {
                            Orders = Orders.Where(o => o.DeliveryDate.HasValue && o.DeliveryDate.Value.ToString("dd/MM/yyyy").Contains(columnFilter_4)).ToList();
                        }
                        // Nº prod
                        if (!string.IsNullOrEmpty(columnFilter_5))
                        {
                            Orders = Orders.Where(o => o.ProductNumber.ToString().ToLower().Contains(columnFilter_5.ToLower())).ToList();
                        }
                        // Código de producto
                        if (!string.IsNullOrEmpty(columnFilter_6))
                        {
                            Orders = Orders.Where(o => o.Product != null && o.Product.Code.ToString().ToLower().Contains(columnFilter_6.ToLower())).ToList();
                        }
                        // Descripcion de producto
                        if (!string.IsNullOrEmpty(columnFilter_7))
                        {
                            Orders = Orders.Where(o => o.Product != null && o.Product.Description.ToString().ToLower().Contains(columnFilter_7.ToLower())).ToList();
                        }
                        // Configuracion
                        if (!string.IsNullOrEmpty(columnFilter_8))
                        {
                            Orders = Orders.Where(o => o.Structure != null && o.Structure.Description.ToString().ToLower().Contains(columnFilter_8.ToLower())).ToList();
                        }
                        // Tensión
                        if (!string.IsNullOrEmpty(columnFilter_9))
                        {
                            Orders = Orders.Where(o => o.SupplyVoltage != null && o.SupplyVoltage.Description.ToString().ToLower().Contains(columnFilter_9.ToLower())).ToList();
                        }
                        // Cantidad
                        if (!string.IsNullOrEmpty(columnFilter_10))
                        {
                            Orders = Orders.Where(o => o.Quantity.ToString().ToLower().Contains(columnFilter_10.ToLower())).ToList();
                        }
                        // Cliente
                        if (!string.IsNullOrEmpty(columnFilter_11))
                        {
                            Orders = Orders.Where(o => o.OrderHeader != null && o.OrderHeader.Client != null && o.OrderHeader.Client.BusinessName.ToString().ToLower().Contains(columnFilter_11.ToLower())).ToList();
                        }
                        // País
                        if (!string.IsNullOrEmpty(columnFilter_12))
                        {
                            Orders = Orders.Where(o => o.OrderHeader != null && o.OrderHeader.Client != null && o.OrderHeader.Client.Country != null && o.OrderHeader.Client.Country.Description.ToString().ToLower().Contains(columnFilter_12.ToLower())).ToList();
                        }
                        // Categoria de venta
                        if (!string.IsNullOrEmpty(columnFilter_13))
                        {
                            Orders = Orders.Where(o => o.SaleCategory.Contains(" - ") ? o.SaleCategory.Split()[1].ToString().ToLower().Contains(columnFilter_13.ToLower()) : o.SaleCategory.ToString().ToLower().Contains(columnFilter_13.ToLower())).ToList();
                        }
                        // Familia/Rubro de producto
                        if (!string.IsNullOrEmpty(columnFilter_14))
                        {
                            Orders = Orders.Where(o => o.Product != null && o.Product.SubCategory != null && o.Product.SubCategory.Description.ToString().ToLower().Contains(columnFilter_14.ToLower())).ToList();
                        }
                        // Estado
                        if (!string.IsNullOrEmpty(columnFilter_15))
                        {
                            Orders = Orders.Where(o => o.OrderState != null && o.OrderState.Name.ToString().ToLower().Contains(columnFilter_15.ToLower())).ToList();
                        }
                        // Fecha entregada
                        if (!string.IsNullOrEmpty(columnFilter_17))
                        {
                            Orders = Orders.Where(o => o.DeliveredDate.HasValue && o.DeliveredDate.Value.ToString("dd/MM/yyyy").Contains(columnFilter_17)).ToList();
                        }
                        // Usuario (OrderHeader.User)
                        if (!string.IsNullOrEmpty(columnFilter_18))
                        {
                            Orders = Orders.Where(o => o.OrderHeader.User.ToString().ToLower().Contains(columnFilter_18.ToLower())).ToList();
                        }
                        // Facturado
                        if (!string.IsNullOrEmpty(columnFilter_19))
                        {
                            Orders = Orders.Where(o => o.OrderHeader.Billed == 1 ? "Facturado".ToString().ToLower().Contains(columnFilter_19.ToLower()) : o.OrderHeader.Billed == 2 ? "No facturado".ToString().ToLower().Contains(columnFilter_19.ToLower()) : o.OrderHeader.Billed == 3 ? "Sin cargo".ToString().ToLower().Contains(columnFilter_19.ToLower()) : " - ".ToString().ToLower().Contains(columnFilter_19.ToLower())).ToList();
                        }
                    }
                    else
                    {
                        // From date -> To date
                        Orders = Orders.Where(o => o.OrderHeader != null && (o.OrderHeader.OrderDate >= Convert.ToDateTime(dateFromFilterValue) && o.OrderHeader.OrderDate <= Convert.ToDateTime(dateToFilterValue)));

                        // Filter orders which belongs to a production order
                        List<OrderDetailViewModel> orderDetailsAux = new List<OrderDetailViewModel>();
                        orderDetailsAux = Orders.ToList();
                        orderDetailsAux.RemoveAll(od => od.BelongsToAProductionOrder == true);

                        Orders = orderDetailsAux;

                        //Nº OV/OP
                        if (!string.IsNullOrEmpty(columnFilter_2))
                        {
                            Orders = Orders.Where(o => o.OrderHeader != null && o.OrderHeader.Number.ToString().ToLower().Contains(columnFilter_2.ToLower())).ToList();
                        }
                        // Fecha OV/OP
                        if (!string.IsNullOrEmpty(columnFilter_3))
                        {
                            Orders = Orders.Where(o => o.OrderHeader != null && o.OrderHeader.OrderDate.ToString("dd/MM/yyyy").Contains(columnFilter_3)).ToList();
                        }
                        // Fecha de entrega
                        if (!string.IsNullOrEmpty(columnFilter_4))
                        {
                            Orders = Orders.Where(o => o.DeliveryDate.HasValue && o.DeliveryDate.Value.ToString("dd/MM/yyyy").Contains(columnFilter_4)).ToList();
                        }
                        // Nº prod
                        if (!string.IsNullOrEmpty(columnFilter_5))
                        {
                            Orders = Orders.Where(o => o.ProductNumber.ToString().ToLower().Contains(columnFilter_5.ToLower())).ToList();
                        }
                        // Código de producto
                        if (!string.IsNullOrEmpty(columnFilter_6))
                        {
                            Orders = Orders.Where(o => o.Product != null && o.Product.Code.ToString().ToLower().Contains(columnFilter_6.ToLower())).ToList();
                        }
                        // Descripcion de producto
                        if (!string.IsNullOrEmpty(columnFilter_7))
                        {
                            Orders = Orders.Where(o => o.Product != null && o.Product.Description.ToString().ToLower().Contains(columnFilter_7.ToLower())).ToList();
                        }
                        // Configuracion
                        if (!string.IsNullOrEmpty(columnFilter_8))
                        {
                            Orders = Orders.Where(o => o.Structure != null && o.Structure.Description.ToString().ToLower().Contains(columnFilter_8.ToLower())).ToList();
                        }
                        // Tensión
                        if (!string.IsNullOrEmpty(columnFilter_9))
                        {
                            Orders = Orders.Where(o => o.SupplyVoltage != null && o.SupplyVoltage.Description.ToString().ToLower().Contains(columnFilter_9.ToLower())).ToList();
                        }
                        // Cantidad
                        if (!string.IsNullOrEmpty(columnFilter_10))
                        {
                            Orders = Orders.Where(o => o.Quantity.ToString().ToLower().Contains(columnFilter_10.ToLower())).ToList();
                        }
                        // Cliente
                        if (!string.IsNullOrEmpty(columnFilter_11))
                        {
                            Orders = Orders.Where(o => o.OrderHeader != null && o.OrderHeader.Client != null && o.OrderHeader.Client.BusinessName.ToString().ToLower().Contains(columnFilter_11.ToLower())).ToList();
                        }
                        // Estado
                        if (!string.IsNullOrEmpty(columnFilter_12))
                        {
                            Orders = Orders.Where(o => o.OrderState != null && o.OrderState.Name.ToString().ToLower().Contains(columnFilter_12.ToLower())).ToList();
                        }
                        // Familia/Rubro producto
                        if (!string.IsNullOrEmpty(columnFilter_14))
                        {
                            Orders = Orders.Where(o => o.Product != null && o.Product.SubCategory != null && o.Product.SubCategory.Description.ToString().ToLower().Contains(columnFilter_14.ToLower())).ToList();
                        }
                        // Inicio OT
                        if (!string.IsNullOrEmpty(columnFilter_15))
                        {
                        }
                        // Fecha entregada
                        if (!string.IsNullOrEmpty(columnFilter_16))
                        {
                            Orders = Orders.Where(o => o.DeliveredDate.HasValue && o.DeliveredDate.Value.ToString("dd/MM/yyyy").Contains(columnFilter_16)).ToList();
                        }
                        // Usuario (OrderHeader.User)
                        if (!string.IsNullOrEmpty(columnFilter_17))
                        {
                            Orders = Orders.Where(o => o.OrderHeader.User.ToString().ToLower().Contains(columnFilter_17.ToLower())).ToList();
                        }
                    }

                    if (isSale)
                    {
                        if (filter_checkAll)
                        {
                            if (onlySpares)
                            {
                                filterList = Orders.Where(o => o.Product.SubCategory.CategoryId == 2 || o.Product.SubCategory.CategoryId == 3 || o.Product.SubCategory.CategoryId == 4).ToList();
                                foreach (var od in filterList)
                                {
                                    ordersAux.Add(od);
                                }
                            }
                            else
                            {
                                filterList = Orders.Where(o => o.Product.SubCategory.CategoryId == 1).ToList();
                                foreach (var od in filterList)
                                {
                                    ordersAux.Add(od);
                                }
                            }
                        }
                        else
                        {
                            if (filter_checkSimplemak)
                            {
                                filterList = Orders.Where(o => o.Product.ProductFeature != null && o.Product.ProductFeature.InHouseManufacturing == true).ToList();
                                foreach (var od in filterList)
                                {
                                    ordersAux.Add(od);
                                }
                            }

                            if (filter_checkImported)
                            {
                                filterList = Orders.Where(o => o.Product.ProductFeature != null && o.Product.ProductFeature.Bought == true).ToList();
                                foreach (var od in filterList)
                                {
                                    ordersAux.Add(od);
                                }
                            }

                            if (filter_checkMachines)
                            {
                                filterList = ordersAux.Where(o => o.Product.SubCategory.Prefix.Contains("MC") || o.Product.SubCategory.Prefix.Contains("MI") || o.Product.SubCategory.Prefix.Contains("MP")
                                || o.Product.SubCategory.Prefix.Contains("MV") || o.Product.SubCategory.Prefix.Contains("MA")).ToList();
                                foreach (var od in filterList)
                                {
                                    ordersAux.Add(od);
                                }
                            }
                            else
                            {
                                if (filter_checkAccesories || filter_checkSpares)
                                {
                                    filterList = ordersAux.Where(o => o.Product.SubCategory.Prefix.Contains("MC") || o.Product.SubCategory.Prefix.Contains("MI") || o.Product.SubCategory.Prefix.Contains("MP")
                               || o.Product.SubCategory.Prefix.Contains("MV") || o.Product.SubCategory.Prefix.Contains("MA")).ToList();
                                    foreach (var od in filterList)
                                    {
                                        ordersAux.Remove(od);
                                    }
                                }
                            }
                            if (filter_checkAccesories)
                            {
                                filterList = ordersAux.Where(o => o.Product.SubCategory.Prefix.Contains("AC")).ToList();
                                foreach (var od in filterList)
                                {
                                    ordersAux.Add(od);
                                }
                            }
                            else
                            {
                                if (filter_checkMachines || filter_checkSpares)
                                {
                                    filterList = ordersAux.Where(o => o.Product.SubCategory.Prefix.Contains("AC")).ToList();
                                    foreach (var od in filterList)
                                    {
                                        ordersAux.Remove(od);
                                    }
                                }
                            }
                            if (filter_checkSpares)
                            {
                                filterList = ordersAux.Where(o => o.Product.SubCategory.CategoryId == 2 || o.Product.SubCategory.CategoryId == 3 || o.Product.SubCategory.CategoryId == 4).ToList();
                                foreach (var od in filterList)
                                {
                                    ordersAux.Add(od);
                                }
                            }
                            else
                            {
                                if (filter_checkAccesories || filter_checkMachines)
                                {
                                    filterList = ordersAux.Where(o => o.Product.SubCategory.CategoryId == 2 || o.Product.SubCategory.CategoryId == 3 || o.Product.SubCategory.CategoryId == 4).ToList();
                                    foreach (var od in filterList)
                                    {
                                        ordersAux.Remove(od);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (filter_checkAll)
                        {
                            filterList = Orders.Where(o => o.Product.SubCategory.CategoryId == 1 || o.Product.SubCategory.CategoryId == 2 || o.Product.SubCategory.CategoryId == 3
                        || o.ProductNumber.Substring(0, 2).ToString() == "04").ToList();
                            foreach (var od in filterList)
                            {
                                ordersAux.Add(od);
                            }
                        }
                        else
                        {
                            if (filter_checkSimplemak)
                            {
                                filterList = Orders.Where(o => o.Product.ProductFeature != null && o.Product.ProductFeature.InHouseManufacturing == true).ToList();
                                foreach (var od in filterList)
                                {
                                    ordersAux.Add(od);
                                }
                            }

                            if (filter_checkImported)
                            {
                                filterList = Orders.Where(o => o.Product.ProductFeature != null && o.Product.ProductFeature.Bought == true).ToList();
                                foreach (var od in filterList)
                                {
                                    ordersAux.Add(od);
                                }
                            }

                            if (filter_checkMachines)
                            {
                                filterList = ordersAux.Where(o => o.Product.SubCategory.Prefix.Contains("MC") || o.Product.SubCategory.Prefix.Contains("MI") || o.Product.SubCategory.Prefix.Contains("MP")
                                || o.Product.SubCategory.Prefix.Contains("MV") || o.Product.SubCategory.Prefix.Contains("MA")).ToList();
                                foreach (var od in filterList)
                                {
                                    ordersAux.Add(od);
                                }
                            }
                            else
                            {
                                if (filter_checkAccesories || filter_checkSpares)
                                {
                                    filterList = ordersAux.Where(o => o.Product.SubCategory.Prefix.Contains("MC") || o.Product.SubCategory.Prefix.Contains("MI") || o.Product.SubCategory.Prefix.Contains("MP")
                               || o.Product.SubCategory.Prefix.Contains("MV") || o.Product.SubCategory.Prefix.Contains("MA")).ToList();
                                    foreach (var od in filterList)
                                    {
                                        ordersAux.Remove(od);
                                    }
                                }
                            }
                            if (filter_checkAccesories)
                            {
                                filterList = ordersAux.Where(o => o.Product.SubCategory.Prefix.Contains("AC")).ToList();
                                foreach (var od in filterList)
                                {
                                    ordersAux.Add(od);
                                }
                            }
                            else
                            {
                                if (filter_checkMachines || filter_checkSpares)
                                {
                                    filterList = ordersAux.Where(o => o.Product.SubCategory.Prefix.Contains("AC")).ToList();
                                    foreach (var od in filterList)
                                    {
                                        ordersAux.Remove(od);
                                    }
                                }
                            }
                            if (filter_checkSpares)
                            {
                                filterList = ordersAux.Where(o => o.Product.SubCategory.CategoryId == 2 || o.Product.SubCategory.CategoryId == 3).ToList();
                                foreach (var od in filterList)
                                {
                                    ordersAux.Add(od);
                                }

                                filterList = ordersAux.Where(o => o.ProductNumber.Substring(0, 2).ToString() == "04").ToList();
                                foreach (var od in filterList)
                                {
                                    ordersAux.Remove(od);
                                }
                            }
                            else
                            {
                                if (filter_checkAccesories || filter_checkMachines)
                                {
                                    filterList = ordersAux.Where(o => o.Product.SubCategory.CategoryId == 2 || o.Product.SubCategory.CategoryId == 3).ToList();
                                    foreach (var od in filterList)
                                    {
                                        ordersAux.Remove(od);
                                    }
                                }
                            }

                            if (filter_checkForStock)
                            {
                                filterList = Orders.Where(o => o.ProductNumber.Substring(0, 2).ToString() == "04").ToList();
                                foreach (var od in filterList)
                                {
                                    ordersAux.Add(od);
                                }

                                if (!filter_checkMachines && !filter_checkAccesories && !filter_checkSpares)
                                {
                                    filterList = ordersAux.Where(o => o.ProductNumber.Substring(0, 2).ToString() != "04").ToList();
                                    foreach (var od in filterList)
                                    {
                                        ordersAux.Remove(od);
                                    }
                                }

                                if (filter_checkMachines)
                                {
                                    filterList = ordersAux.Where(o => o.Product.SubCategory.Prefix.Contains("MC") || o.Product.SubCategory.Prefix.Contains("MI") || o.Product.SubCategory.Prefix.Contains("MP")
                               || o.Product.SubCategory.Prefix.Contains("MV") || o.Product.SubCategory.Prefix.Contains("MA")).ToList();
                                    foreach (var od in filterList)
                                    {
                                        ordersAux.Add(od);
                                    }
                                }
                                if (filter_checkAccesories)
                                {
                                    filterList = ordersAux.Where(o => o.Product.SubCategory.Prefix.Contains("AC")).ToList();
                                    foreach (var od in filterList)
                                    {
                                        ordersAux.Add(od);
                                    }
                                }
                                if (filter_checkSpares)
                                {
                                    filterList = ordersAux.Where(o => o.Product.SubCategory.CategoryId == 2 || o.Product.SubCategory.CategoryId == 3 || o.Product.SubCategory.CategoryId == 4).ToList();
                                    foreach (var od in filterList)
                                    {
                                        ordersAux.Add(od);
                                    }
                                }
                            }
                            else
                            {
                                filterList = ordersAux.Where(o => o.ProductNumber.Substring(0, 2).ToString() == "04").ToList();
                                foreach (var od in filterList)
                                {
                                    ordersAux.Remove(od);
                                }
                            }
                        }
                    }

                    Orders = ordersAux;

                    if (colIndexOrder != 0 && colOrderDirection != "")
                    {
                        var sortColumnIndex = colIndexOrder;
                        var sortDirection = colOrderDirection;

                        if (isSale)
                        {
                            if (sortColumnIndex == 2)
                            {
                                Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.OrderHeader.Number) : Orders.OrderByDescending(so => so.OrderHeader.Number);
                            }
                            else if (sortColumnIndex == 3)
                            {
                                Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.OrderHeader.OrderDate) : Orders.OrderByDescending(so => so.OrderHeader.OrderDate);
                            }
                            else if (sortColumnIndex == 4)
                            {
                                Orders = sortDirection == "asc" ? Orders.OrderByDescending(so => so.DeliveryDate.HasValue).ThenBy(so => so.DeliveryDate) : Orders.OrderByDescending(so => so.DeliveryDate.HasValue).ThenByDescending(so => so.DeliveryDate);
                            }
                            else if (sortColumnIndex == 5)
                            {
                                Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.ProductNumber) : Orders.OrderByDescending(so => so.ProductNumber);
                            }
                            else if (sortColumnIndex == 6)
                            {
                                Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.Product != null ? so.Product.Code.ToString() : null) : Orders.OrderByDescending(so => so.Product != null ? so.Product.Code.ToString() : null);
                            }
                            else if (sortColumnIndex == 7)
                            {
                                Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.Product != null ? so.Product.Description.ToString() : null) : Orders.OrderByDescending(so => so.Product != null ? so.Product.Description.ToString() : null);
                            }
                            else if (sortColumnIndex == 8)
                            {
                                Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.Structure != null ? so.Structure.Description.ToString() : null) : Orders.OrderByDescending(so => so.Structure != null ? so.Structure.Description.ToString() : null);
                            }
                            else if (sortColumnIndex == 9)
                            {
                                Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.SupplyVoltage != null ? so.SupplyVoltage.Description.ToString() : null) : Orders.OrderByDescending(so => so.SupplyVoltage != null ? so.SupplyVoltage.Description.ToString() : null);
                            }
                            else if (sortColumnIndex == 10)
                            {
                                Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.Quantity) : Orders.OrderByDescending(so => so.Quantity);
                            }
                            else if (sortColumnIndex == 11)
                            {
                                Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.OrderHeader != null ? (so.OrderHeader.Client != null ? so.OrderHeader.Client.BusinessName.ToString() : null) : null) : Orders.OrderByDescending(so => so.OrderHeader != null ? (so.OrderHeader.Client != null ? so.OrderHeader.Client.BusinessName.ToString() : null) : null);
                            }
                            else if (sortColumnIndex == 12)
                            {
                                Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.OrderHeader != null ? (so.OrderHeader.Client != null ? (so.OrderHeader.Client.Country != null ? so.OrderHeader.Client.Country.Description.ToString() : null) : null) : null) : Orders.OrderByDescending(so => so.OrderHeader != null ? (so.OrderHeader.Client != null ? (so.OrderHeader.Client.Country != null ? so.OrderHeader.Client.Country.Description.ToString() : null) : null) : null);
                            }
                            else if (sortColumnIndex == 13)
                            {
                                Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.SaleCategory) : Orders.OrderByDescending(so => so.SaleCategory);
                            }
                            else if (sortColumnIndex == 14)
                            {
                                Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.Product != null ? (so.Product.SubCategory != null ? so.Product.SubCategory.Description.ToString() : null) : null) : Orders.OrderByDescending(so => so.Product != null ? (so.Product.SubCategory != null ? so.Product.SubCategory.Description.ToString() : null) : null);
                            }
                            else if (sortColumnIndex == 15)
                            {
                                Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.OrderState != null ? so.OrderState.Name.ToString() : null) : Orders.OrderByDescending(so => so.OrderState != null ? so.OrderState.Name.ToString() : null);
                            }
                            else if (sortColumnIndex == 17)
                            {
                                Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.OrderHeader.DeliveredDate != null ? so.OrderHeader.DeliveredDate : null) : Orders.OrderByDescending(so => so.OrderHeader.DeliveredDate != null ? so.OrderHeader.DeliveredDate : null);
                            }
                            else if (sortColumnIndex == 18)
                            {
                                Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.OrderHeader.User) : Orders.OrderByDescending(so => so.OrderHeader.User);
                            }
                            else if (sortColumnIndex == 19)
                            {
                                Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.OrderHeader.Billed == 1 ? "Facturado" : so.OrderHeader.Billed == 2 ? "No facturado" : so.OrderHeader.Billed == 3 ? "Sin cargo" : " - ") : Orders.OrderByDescending(so => so.OrderHeader.Billed == 1 ? "Facturado" : so.OrderHeader.Billed == 2 ? "No facturado" : so.OrderHeader.Billed == 3 ? "Sin cargo" : " - ");
                            }
                        }
                        else
                        {
                            if (sortColumnIndex == 2)
                            {
                                Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.OrderHeader.Number) : Orders.OrderByDescending(so => so.OrderHeader.Number);
                            }
                            else if (sortColumnIndex == 3)
                            {
                                Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.OrderHeader.OrderDate) : Orders.OrderByDescending(so => so.OrderHeader.OrderDate);
                            }
                            else if (sortColumnIndex == 4)
                            {
                                Orders = sortDirection == "asc" ? Orders.OrderByDescending(so => so.DeliveryDate.HasValue).ThenBy(so => so.DeliveryDate) : Orders.OrderByDescending(so => so.DeliveryDate.HasValue).ThenByDescending(so => so.DeliveryDate);
                            }
                            else if (sortColumnIndex == 5)
                            {
                                Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.ProductNumber) : Orders.OrderByDescending(so => so.ProductNumber);
                            }
                            else if (sortColumnIndex == 6)
                            {
                                Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.Product != null ? so.Product.Code.ToString() : null) : Orders.OrderByDescending(so => so.Product != null ? so.Product.Code.ToString() : null);
                            }
                            else if (sortColumnIndex == 7)
                            {
                                Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.Product != null ? so.Product.Description.ToString() : null) : Orders.OrderByDescending(so => so.Product != null ? so.Product.Description.ToString() : null);
                            }
                            else if (sortColumnIndex == 8)
                            {
                                Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.Structure != null ? so.Structure.Description.ToString() : null) : Orders.OrderByDescending(so => so.Structure != null ? so.Structure.Description.ToString() : null);
                            }
                            else if (sortColumnIndex == 9)
                            {
                                Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.SupplyVoltage != null ? so.SupplyVoltage.Description.ToString() : null) : Orders.OrderByDescending(so => so.SupplyVoltage != null ? so.SupplyVoltage.Description.ToString() : null);
                            }
                            else if (sortColumnIndex == 10)
                            {
                                Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.Quantity) : Orders.OrderByDescending(so => so.Quantity);
                            }
                            else if (sortColumnIndex == 11)
                            {
                                Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.OrderHeader != null ? (so.OrderHeader.Client != null ? so.OrderHeader.Client.BusinessName.ToString() : null) : null) : Orders.OrderByDescending(so => so.OrderHeader != null ? (so.OrderHeader.Client != null ? so.OrderHeader.Client.BusinessName.ToString() : null) : null);
                            }
                            else if (sortColumnIndex == 12)
                            {
                                Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.OrderState != null ? so.OrderState.Name.ToString() : null) : Orders.OrderByDescending(so => so.OrderState != null ? so.OrderState.Name.ToString() : null);
                            }
                            else if (sortColumnIndex == 14)
                            {
                                Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.Product != null ? (so.Product.SubCategory != null ? so.Product.SubCategory.Description.ToString() : null) : null) : Orders.OrderByDescending(so => so.Product != null ? (so.Product.SubCategory != null ? so.Product.SubCategory.Description.ToString() : null) : null);
                            }
                            else if (sortColumnIndex == 15)
                            {

                            }
                            else if (sortColumnIndex == 16)
                            {
                                Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.OrderHeader.DeliveredDate != null ? so.OrderHeader.DeliveredDate : null) : Orders.OrderByDescending(so => so.OrderHeader.DeliveredDate != null ? so.OrderHeader.DeliveredDate : null);
                            }
                            else if (sortColumnIndex == 17)
                            {
                                Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.OrderHeader.User) : Orders.OrderByDescending(so => so.OrderHeader.User);
                            }
                        }
                    }

                    return ExportViewModelToExcel(Orders, isSale, onlySpares);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IActionResult ExportViewModelToExcel(IEnumerable<OrderDetailViewModel> viewModel, bool isSale, bool onlySpares)
        {
            try
            {
                var nameForExcelFile = "";
                if (onlySpares)
                {
                    nameForExcelFile = "Ordenes de venta de repuestos_";
                }
                else
                {
                    nameForExcelFile = "Ordenes de venta_";
                }

                var stream = new MemoryStream();
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                if (isSale)
                {
                    using (var xlPackage = new ExcelPackage(stream))
                    {
                        var workSheet = xlPackage.Workbook.Worksheets.Add("Ordenes de venta");

                        workSheet.Cells["A1"].Value = "Nº OV/OP";
                        workSheet.Cells["B1"].Value = _localizer["Date"] + " OV/OP";
                        workSheet.Cells["C1"].Value = _localizer["Delivery date"];
                        workSheet.Cells["D1"].Value = "Nº prod";
                        workSheet.Cells["E1"].Value = _localizer["Code"];
                        workSheet.Cells["F1"].Value = _localizer["Description"];
                        workSheet.Cells["G1"].Value = _localizer["Configuration"];
                        workSheet.Cells["H1"].Value = _localizer["Supply voltage"];
                        workSheet.Cells["I1"].Value = _localizer["Quantity"];
                        workSheet.Cells["J1"].Value = _localizer["Client"];

                        workSheet.Cells["K1"].Value = _localizer["Country"];
                        workSheet.Cells["L1"].Value = _localizer["Sale category"];
                        workSheet.Cells["M1"].Value = _localizer["Family/Heading"];
                        workSheet.Cells["N1"].Value = _localizer["State"];
                        workSheet.Cells["O1"].Value = _localizer["Percentage of completion"];
                        workSheet.Cells["P1"].Value = _localizer["Delivered date"];
                        workSheet.Cells["Q1"].Value = _localizer["User"];
                        workSheet.Cells["R1"].Value = _localizer["Billed"];

                        var row = 2;
                        foreach (var order in viewModel)
                        {
                            if (order.OrderHeader != null)
                            {
                                workSheet.Cells[row, 1].Value = order.OrderHeader.Number;
                                workSheet.Cells[row, 1].Style.Numberformat.Format = "0";
                            }
                            else
                            {
                                workSheet.Cells[row, 1].Value = "";
                            }

                            if (order.OrderHeader != null)
                            {
                                workSheet.Cells[row, 2].Value = order.OrderHeader.OrderDate;
                                workSheet.Cells[row, 2].Style.Numberformat.Format = "dd-MM-yyyy HH:mm";
                            }
                            else
                            {
                                workSheet.Cells[row, 2].Value = "";
                            }

                            if (order.DeliveryDate != null)
                            {
                                workSheet.Cells[row, 3].Value = order.DeliveryDate;
                                workSheet.Cells[row, 3].Style.Numberformat.Format = "dd-MM-yyyy HH:mm";
                            }
                            else
                            {
                                workSheet.Cells[row, 3].Value = "";
                            }

                            workSheet.Cells[row, 4].Value = order.ProductNumber.ToString();

                            if (order.Product != null)
                            {
                                workSheet.Cells[row, 5].Value = order.Product.Code.ToString();
                                workSheet.Cells[row, 6].Value = order.Product.Description.ToString();
                                if (order.Product.SubCategory != null)
                                {
                                    workSheet.Cells[row, 13].Value = order.Product.SubCategory.Description.ToString();
                                }
                                else
                                {
                                    workSheet.Cells[row, 13].Value = "";
                                }
                            }
                            else
                            {
                                workSheet.Cells[row, 5].Value = "";
                                workSheet.Cells[row, 6].Value = "";
                                workSheet.Cells[row, 13].Value = "";
                            }

                            if (order.Structure != null)
                            {
                                workSheet.Cells[row, 7].Value = order.Structure.Description.ToString();
                            }
                            else
                            {
                                workSheet.Cells[row, 7].Value = "";
                            }

                            if (order.SupplyVoltage != null)
                            {
                                workSheet.Cells[row, 8].Value = order.SupplyVoltage.Description.ToString();
                            }
                            else
                            {
                                workSheet.Cells[row, 8].Value = "";
                            }

                            workSheet.Cells[row, 9].Value = order.Quantity;
                            workSheet.Cells[row, 9].Style.Numberformat.Format = "0";

                            if (order.OrderHeader != null)
                            {
                                if (order.OrderHeader.Client != null)
                                {
                                    workSheet.Cells[row, 10].Value = order.OrderHeader.Client.BusinessName.ToString();

                                    if (order.OrderHeader.Client.Country != null)
                                    {
                                        workSheet.Cells[row, 11].Value = order.OrderHeader.Client.Country.Description.ToString();
                                    }
                                    else
                                    {
                                        workSheet.Cells[row, 11].Value = "";
                                    }
                                }
                                else
                                {
                                    workSheet.Cells[row, 10].Value = "";
                                    workSheet.Cells[row, 11].Value = "";
                                }
                            }
                            else
                            {
                                workSheet.Cells[row, 10].Value = "";
                                workSheet.Cells[row, 11].Value = "";
                            }

                            if (order.SaleCategory.Contains(" - "))
                            {
                                workSheet.Cells[row, 12].Value = order.SaleCategory.Split()[1].ToString();
                            }
                            else
                            {
                                workSheet.Cells[row, 12].Value = order.SaleCategory.ToString();
                            }

                            if (order.OrderState != null)
                            {
                                workSheet.Cells[row, 14].Value = order.OrderState.Name.ToString();
                            }
                            else
                            {
                                workSheet.Cells[row, 14].Value = "";
                            }

                            workSheet.Cells[row, 15].Value = order.PercentageOfTotalAdvance;
                            workSheet.Cells[row, 15].Style.Numberformat.Format = "0.00";



                            if (order.DeliveredDate != null)
                            {
                                workSheet.Cells[row, 16].Value = order.DeliveredDate;
                                workSheet.Cells[row, 16].Style.Numberformat.Format = "dd-MM-yyyy HH:mm";
                            }
                            else
                            {
                                workSheet.Cells[row, 16].Value = "";
                            }

                            workSheet.Cells[row, 17].Value = order.OrderHeader.User.ToString();

                            if (order.OrderHeader.Billed == 1)
                            {
                                workSheet.Cells[row, 18].Value = "Facturado";
                            }
                            else if (order.OrderHeader.Billed == 2)
                            {
                                workSheet.Cells[row, 18].Value = "No facturado";
                            }
                            else if (order.OrderHeader.Billed == 3)
                            {
                                workSheet.Cells[row, 18].Value = "Sin cargo";
                            }

                            row++;
                        }

                        workSheet.Cells["A1:R1"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                        workSheet.Cells["R1"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

                        workSheet.Row(1).Style.Font.Bold = true;
                        workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();

                        xlPackage.Workbook.Properties.Title = nameForExcelFile + DateTime.Now.Date.ToString("dd-MM-yyyy");
                        xlPackage.Save();
                    }

                    stream.Position = 0;
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nameForExcelFile + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xlsx");
                }
                else
                {
                    using (var xlPackage = new ExcelPackage(stream))
                    {
                        var workSheet = xlPackage.Workbook.Worksheets.Add("Ordenes de producción");

                        workSheet.Cells["A1"].Value = "Nº OV/OP";
                        workSheet.Cells["B1"].Value = _localizer["Date"] + " OV/OP";
                        workSheet.Cells["C1"].Value = _localizer["Delivery date"];
                        workSheet.Cells["D1"].Value = "Nº prod";
                        workSheet.Cells["E1"].Value = _localizer["Code"];
                        workSheet.Cells["F1"].Value = _localizer["Description"];
                        workSheet.Cells["G1"].Value = _localizer["Configuration"];
                        workSheet.Cells["H1"].Value = _localizer["Supply voltage"];
                        workSheet.Cells["I1"].Value = _localizer["Quantity"];
                        workSheet.Cells["J1"].Value = _localizer["Client"];
                        workSheet.Cells["K1"].Value = _localizer["State"];
                        workSheet.Cells["L1"].Value = _localizer["Percentage of completion"];
                        workSheet.Cells["M1"].Value = _localizer["Family/Heading"];
                        workSheet.Cells["N1"].Value = _localizer["Start"] + " OT";
                        workSheet.Cells["O1"].Value = _localizer["Delivered date"];
                        workSheet.Cells["P1"].Value = _localizer["User"];

                        var row = 2;
                        foreach (var order in viewModel)
                        {
                            if (order.OrderHeader != null)
                            {
                                workSheet.Cells[row, 1].Value = order.OrderHeader.Number;
                                workSheet.Cells[row, 1].Style.Numberformat.Format = "0";
                            }
                            else
                            {
                                workSheet.Cells[row, 1].Value = "";
                            }

                            if (order.OrderHeader != null)
                            {
                                workSheet.Cells[row, 2].Value = order.OrderHeader.OrderDate;
                                workSheet.Cells[row, 2].Style.Numberformat.Format = "dd-MM-yyyy HH:mm";
                            }
                            else
                            {
                                workSheet.Cells[row, 2].Value = "";
                            }

                            if (order.DeliveryDate != null)
                            {
                                workSheet.Cells[row, 3].Value = order.DeliveryDate;
                                workSheet.Cells[row, 3].Style.Numberformat.Format = "dd-MM-yyyy HH:mm";
                            }
                            else
                            {
                                workSheet.Cells[row, 3].Value = "";
                            }

                            workSheet.Cells[row, 4].Value = order.ProductNumber.ToString();

                            if (order.Product != null)
                            {
                                workSheet.Cells[row, 5].Value = order.Product.Code.ToString();
                                workSheet.Cells[row, 6].Value = order.Product.Description.ToString();
                                if (order.Product.SubCategory != null)
                                {
                                    workSheet.Cells[row, 13].Value = order.Product.SubCategory.Description.ToString();
                                }
                                else
                                {
                                    workSheet.Cells[row, 13].Value = "";
                                }
                            }
                            else
                            {
                                workSheet.Cells[row, 5].Value = "";
                                workSheet.Cells[row, 6].Value = "";
                                workSheet.Cells[row, 13].Value = "";
                            }

                            if (order.Structure != null)
                            {
                                workSheet.Cells[row, 7].Value = order.Structure.Description.ToString();
                            }
                            else
                            {
                                workSheet.Cells[row, 7].Value = "";
                            }

                            if (order.SupplyVoltage != null)
                            {
                                workSheet.Cells[row, 8].Value = order.SupplyVoltage.Description.ToString();
                            }
                            else
                            {
                                workSheet.Cells[row, 8].Value = "";
                            }

                            workSheet.Cells[row, 9].Value = order.Quantity;
                            workSheet.Cells[row, 9].Style.Numberformat.Format = "0";

                            if (order.OrderHeader != null)
                            {
                                if (order.OrderHeader.Client != null)
                                {
                                    workSheet.Cells[row, 10].Value = order.OrderHeader.Client.BusinessName.ToString();
                                }
                                else
                                {
                                    workSheet.Cells[row, 10].Value = "";
                                }
                            }
                            else
                            {
                                workSheet.Cells[row, 10].Value = "";
                            }

                            if (order.OrderState != null)
                            {
                                workSheet.Cells[row, 11].Value = order.OrderState.Name.ToString();
                            }
                            else
                            {
                                workSheet.Cells[row, 11].Value = "";
                            }

                            workSheet.Cells[row, 12].Value = order.PercentageOfTotalAdvance;
                            workSheet.Cells[row, 12].Style.Numberformat.Format = "0.00";

                            if (order.StartDate != null)
                            {
                                workSheet.Cells[row, 14].Value = order.StartDate;
                                workSheet.Cells[row, 14].Style.Numberformat.Format = "dd-MM-yyyy HH:mm";
                            }
                            else
                            {
                                workSheet.Cells[row, 14].Value = "";
                            }

                            if (order.DeliveredDate != null)
                            {
                                workSheet.Cells[row, 15].Value = order.DeliveredDate;
                                workSheet.Cells[row, 15].Style.Numberformat.Format = "dd-MM-yyyy HH:mm";
                            }
                            else
                            {
                                workSheet.Cells[row, 15].Value = "";
                            }

                            workSheet.Cells[row, 16].Value = order.OrderHeader.User.ToString();

                            row++;
                        }

                        workSheet.Cells["A1:P1"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                        workSheet.Cells["P1"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

                        workSheet.Row(1).Style.Font.Bold = true;
                        workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();

                        xlPackage.Workbook.Properties.Title = "Ordenes de producción_" + DateTime.Now.Date.ToString("dd-MM-yyyy");
                        xlPackage.Save();
                    }

                    stream.Position = 0;
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Ordenes de producción_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xlsx");
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return null;
            }
        }

        // No se usa mas
        public async Task<List<OrderDetailViewModel>> circularReferenceWithHeaders(List<OrderDetailViewModel> viewModel, bool isSale)
        {
            var usersList = _identityContext.Users.ToList();

            List<OrderDetailViewModel> orderDetailsViewModel = new List<OrderDetailViewModel>();
            if (viewModel.Count > 0)
            {
                foreach (var od in viewModel)
                {
                    OrderDetailViewModel detailViewModel = new OrderDetailViewModel();
                    detailViewModel.Id = od.Id;
                    detailViewModel.ConcurrencyToken = od.ConcurrencyToken;

                    detailViewModel.ProductNumber = od.ProductNumber;

                    detailViewModel.ProductId = od.ProductId;
                    detailViewModel.Product = od.Product;

                    detailViewModel.StructureId = od.StructureId;
                    detailViewModel.Structure = od.Structure;

                    detailViewModel.SupplyVoltageId = od.SupplyVoltageId;
                    detailViewModel.SupplyVoltage = od.SupplyVoltage;
                    //detailViewModel.PercentageOfTotalAdvance = od.PercentageOfTotalAdvance;

                    detailViewModel.DeliveryDate = od.DeliveryDate;
                    detailViewModel.OrderStateId = od.OrderStateId;
                    detailViewModel.OrderState = od.OrderState;

                    this.AssignWorkOrderData(detailViewModel, od);

                    detailViewModel.DeliveredDate = od.DeliveredDate;
                    detailViewModel.SaleCategory = od.SaleCategory;
                    detailViewModel.Quantity = od.Quantity;
                    detailViewModel.UnitPrice = od.UnitPrice;

                    var totalActivities = 0;
                    var totalActivitiesFinished = 0;
                    var isStarted = false;
                    if (od.WorkOrder != null)
                    {
                        detailViewModel.WorkOrder = od.WorkOrder;
                        detailViewModel.WorkOrder.OrderDetail = null;
                        if (od.WorkOrder.WorkOrderItems.Count > 0)
                        {
                            foreach (var woi in od.WorkOrder.WorkOrderItems)
                            {
                                woi.WorkOrder = null;
                                if (woi.WorkActivities.Count > 0)
                                {
                                    totalActivities += woi.WorkActivities.Count();
                                    foreach (var wa in woi.WorkActivities)
                                    {
                                        wa.WorkOrderItem = null;
                                        totalActivitiesFinished += wa.isFinished ? 1 : 0;
                                        if (wa.WorkActions.Count > 0)
                                        {
                                            isStarted = true;
                                            foreach (var wact in wa.WorkActions)
                                            {
                                                wact.WorkActivity = null;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (totalActivitiesFinished > 0)
                    {
                        detailViewModel.PercentageOfTotalAdvance = Convert.ToInt32(totalActivitiesFinished * 100 / totalActivities);
                    }

                    if (isStarted)
                    {
                        // Asigno el estado "En proceso" porque ya se iniciaron actividades para esta orden.
                        var getOrderStateById = await _orderStateViewManager.GetByIdAsync(2);
                        if (getOrderStateById.Succeeded)
                        {
                            var orderStateVM = _mapper.Map<OrderStateViewModel>(getOrderStateById.Data);
                            detailViewModel.OrderStateId = orderStateVM.Id;
                            detailViewModel.OrderState = orderStateVM;
                        }
                    }

                    detailViewModel.WorkOrderId = od.WorkOrderId;
                    //detailViewModel.rowOrder = od.rowOrder;

                    if (od.OrderHeader != null)
                    {
                        OrderHeaderViewModel orderViewModel = new OrderHeaderViewModel();
                        orderViewModel.Id = od.OrderHeader.Id;
                        orderViewModel.ConcurrencyToken = od.OrderHeader.ConcurrencyToken;
                        orderViewModel.ClientId = od.OrderHeader.ClientId;
                        orderViewModel.Client = od.OrderHeader.Client;
                        orderViewModel.OrderDate = od.OrderHeader.OrderDate;
                        orderViewModel.Number = od.OrderHeader.Number;
                        orderViewModel.Taxes = od.OrderHeader.Taxes;
                        orderViewModel.SecureAndFreightCosts = od.OrderHeader.SecureAndFreightCosts;
                        orderViewModel.Bonus = od.OrderHeader.Bonus;
                        orderViewModel.TypeOfFreightAndSecure = od.OrderHeader.TypeOfFreightAndSecure;
                        orderViewModel.PaymentMethod = od.OrderHeader.PaymentMethod;
                        orderViewModel.Packaging = od.OrderHeader.Packaging;
                        orderViewModel.Transport = od.OrderHeader.Transport;
                        orderViewModel.PlaceOfDelivery = od.OrderHeader.PlaceOfDelivery;
                        orderViewModel.OrderTotalPrice = od.OrderHeader.OrderTotalPrice;
                        orderViewModel.ValidOfferDate = od.OrderHeader.ValidOfferDate;
                        orderViewModel.ProductionObservations = od.OrderHeader.ProductionObservations;
                        orderViewModel.SaleObservations = od.OrderHeader.SaleObservations;
                        orderViewModel.DeliveredDate = od.OrderHeader.DeliveredDate;
                        orderViewModel.Billed = od.OrderHeader.Billed;
                        orderViewModel.OrderStateId = od.OrderHeader.OrderStateId;
                        orderViewModel.OrderState = od.OrderHeader.OrderState;
                        if (od.OrderHeader.ClientId != null)
                        {
                            orderViewModel.isSale = true;
                        }
                        else
                        {
                            orderViewModel.isSale = false;
                        }
                        //orderViewModel.isSale = isSale;
                        orderViewModel.OrderDetails = null;

                        detailViewModel.OrderHeader = orderViewModel;

                        // User
                        foreach (var u in usersList)
                        {
                            if (u.Id == od.OrderHeader.User)
                            {
                                detailViewModel.OrderHeader.User = u.FirstName.ToString() + ", " + u.LastName.ToString();
                            }
                        }
                    }

                    orderDetailsViewModel.Add(detailViewModel);
                }

                return orderDetailsViewModel;
            }
            else
            {
                return orderDetailsViewModel;
            }
        }

        // No se usa mas
        public void AssignWorkOrderData(OrderDetailViewModel OrderDetail, OrderDetailViewModel od)
        {
            if (od.CreatedBy != null)
            {
                if (od.WorkOrder == null)
                {
                    //OrderDetail.PercentageOfTotalAdvance = 0;
                    OrderDetail.State = "";
                }
                else
                {
                    if (od.WorkOrder.StartDate == null)
                    {
                        OrderDetail.StartDate = "";
                    }
                    else
                    {
                        OrderDetail.StartDate = od.WorkOrder.StartDate.Value.ToString("dd/MM/yyyy HH:mm:ss");
                    }
                    OrderDetail.State = od.WorkOrder.GetState;
                    //OrderDetail.PercentageOfTotalAdvance = od.WorkOrder.TotalAdvance;
                }
            }
        }

        // No se usa mas
        public List<OrderDetailViewModel> setUsers(List<OrderDetailViewModel> orderDetailViewModels)
        {
            try
            {
                foreach (var vm in orderDetailViewModels)
                {
                    // User
                    var usersList = _identityContext.Users.ToList();
                    foreach (var u in usersList)
                    {
                        if (u.Id == vm.OrderHeader.User)
                        {
                            vm.OrderHeader.User = u.FirstName.ToString() + ", " + u.LastName.ToString();
                        }
                    }
                }

                return orderDetailViewModels;
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return orderDetailViewModels;
            }
        }

        public async Task<IActionResult> LoadAllSales(DatatableParamsViewModel datatableParams,
            string sSearch_2, string sSearch_3, string sSearch_4, string sSearch_5, string sSearch_6, string sSearch_7, string sSearch_8, string sSearch_9,
            string sSearch_10, string sSearch_11, string sSearch_12, string sSearch_13, string sSearch_14, string sSearch_15, string sSearch_16, string sSearch_17, string sSearch_18, string sSearch_19,
            string checkAll, string checkSimplemak, string checkImported, string checkMachines, string checkAccesories, string checkSpares, DateTime dateFromFilterValue, DateTime dateToFilterValue,
            bool onlySpares)
        {
            // For sale orders
            var response = await _OrderDetailViewManager.FindBySpecification(new OrderDetailWithAllSpecifications(true));
            if (response.Succeeded)
            {
                try
                {
                    var viewModel = _mapper.Map<List<OrderDetailViewModel>>(response.Data);

                    // Work with new line on Startup.cs which ignore cycles references.
                    IEnumerable<OrderDetailViewModel> Orders = viewModel;

                    // TODO: Eliminar circularReferenceWithHeaders.
                    //bool isSale = true;
                    //IEnumerable<OrderDetailViewModel> Orders = await circularReferenceWithHeaders(viewModel, isSale);

                    HashSet<OrderDetailViewModel> ordersAux = new HashSet<OrderDetailViewModel>();
                    IEnumerable<OrderDetailViewModel> filterList = new List<OrderDetailViewModel>();

                    Orders = Orders.Where(o => o.OrderHeader != null && o.OrderHeader.Client != null);

                    #region Filters
                    // From date -> To date
                    Orders = Orders.Where(o => o.OrderHeader != null && (o.OrderHeader.OrderDate.Date >= dateFromFilterValue && o.OrderHeader.OrderDate.Date <= dateToFilterValue));

                    // Filter orders which belongs to a sale order
                    List<OrderDetailViewModel> orderDetailsAux = new List<OrderDetailViewModel>();
                    orderDetailsAux = Orders.ToList();
                    orderDetailsAux.RemoveAll(od => od.BelongsToASaleOrder == true);

                    Orders = orderDetailsAux;

                    //Nº OV/OP
                    if (!string.IsNullOrEmpty(sSearch_2))
                    {
                        Orders = Orders.Where(o => o.OrderHeader != null && o.OrderHeader.Number.ToString().ToLower().Contains(sSearch_2.ToLower())).ToList();
                    }
                    // Fecha OV/OP
                    if (!string.IsNullOrEmpty(sSearch_3))
                    {
                        Orders = Orders.Where(o => o.OrderHeader != null && o.OrderHeader.OrderDate.ToString("dd/MM/yyyy").Contains(sSearch_3)).ToList();
                    }
                    // Fecha de entrega
                    if (!string.IsNullOrEmpty(sSearch_4))
                    {
                        Orders = Orders.Where(o => o.DeliveryDate.HasValue && o.DeliveryDate.Value.ToString("dd/MM/yyyy").Contains(sSearch_4)).ToList();
                    }
                    // Nº prod
                    if (!string.IsNullOrEmpty(sSearch_5))
                    {
                        Orders = Orders.Where(o => o.ProductNumber.ToString().ToLower().Contains(sSearch_5.ToLower())).ToList();
                    }
                    // Código de producto
                    if (!string.IsNullOrEmpty(sSearch_6))
                    {
                        Orders = Orders.Where(o => o.Product != null && o.Product.Code.ToString().ToLower().Contains(sSearch_6.ToLower())).ToList();
                    }
                    // Descripcion de producto
                    if (!string.IsNullOrEmpty(sSearch_7))
                    {
                        Orders = Orders.Where(o => o.Product != null && o.Product.Description.ToString().ToLower().Contains(sSearch_7.ToLower())).ToList();
                    }
                    // Configuracion
                    if (!string.IsNullOrEmpty(sSearch_8))
                    {
                        Orders = Orders.Where(o => o.Structure != null && o.Structure.Description.ToString().ToLower().Contains(sSearch_8.ToLower())).ToList();
                    }
                    // Tensión
                    if (!string.IsNullOrEmpty(sSearch_9))
                    {
                        Orders = Orders.Where(o => o.SupplyVoltage != null && o.SupplyVoltage.Description.ToString().ToLower().Contains(sSearch_9.ToLower())).ToList();
                    }
                    // Cantidad
                    if (!string.IsNullOrEmpty(sSearch_10))
                    {
                        Orders = Orders.Where(o => o.Quantity.ToString().ToLower().Contains(sSearch_10.ToLower())).ToList();
                    }
                    // Cliente
                    if (!string.IsNullOrEmpty(sSearch_11))
                    {
                        Orders = Orders.Where(o => o.OrderHeader != null && o.OrderHeader.Client != null && o.OrderHeader.Client.BusinessName.ToString().ToLower().Contains(sSearch_11.ToLower())).ToList();
                    }
                    // País
                    if (!string.IsNullOrEmpty(sSearch_12))
                    {
                        Orders = Orders.Where(o => o.OrderHeader != null && o.OrderHeader.Client != null && o.OrderHeader.Client.Country != null && o.OrderHeader.Client.Country.Description.ToString().ToLower().Contains(sSearch_12.ToLower())).ToList();
                    }
                    // Categoria de venta
                    if (!string.IsNullOrEmpty(sSearch_13))
                    {
                        Orders = Orders.Where(o => o.SaleCategory.Contains(" - ") ? o.SaleCategory.Split()[1].ToString().ToLower().Contains(sSearch_13.ToLower()) : o.SaleCategory.ToString().ToLower().Contains(sSearch_13.ToLower())).ToList();
                    }
                    // Familia/Rubro de producto
                    if (!string.IsNullOrEmpty(sSearch_14))
                    {
                        Orders = Orders.Where(o => o.Product != null && o.Product.SubCategory != null && o.Product.SubCategory.Description.ToString().ToLower().Contains(sSearch_14.ToLower())).ToList();
                    }
                    // Estado
                    if (!string.IsNullOrEmpty(sSearch_15))
                    {
                        Orders = Orders.Where(o => o.OrderState != null && o.OrderState.Name.ToString().ToLower().Contains(sSearch_15.ToLower())).ToList();
                    }
                    // Fecha entregada
                    if (!string.IsNullOrEmpty(sSearch_17))
                    {
                        Orders = Orders.Where(o => o.DeliveredDate.HasValue && o.DeliveredDate.Value.ToString("dd/MM/yyyy").Contains(sSearch_17)).ToList();
                    }
                    // Usuario (OrderHeader.User)
                    if (!string.IsNullOrEmpty(sSearch_18))
                    {
                        Orders = Orders.Where(o => o.OrderHeader.User.ToString().ToLower().Contains(sSearch_18.ToLower())).ToList();
                    }
                    // Facturado
                    if (!string.IsNullOrEmpty(sSearch_19))
                    {
                        Orders = Orders.Where(o => o.OrderHeader.Billed == 1 ? "Facturado".ToString().ToLower().Contains(sSearch_19.ToLower()) : o.OrderHeader.Billed == 2 ? "No facturado".ToString().ToLower().Contains(sSearch_19.ToLower()) : o.OrderHeader.Billed == 3 ? "Sin cargo".ToString().ToLower().Contains(sSearch_19.ToLower()) : " - ".ToString().ToLower().Contains(sSearch_19.ToLower())).ToList();
                    }

                    if (checkAll == "true")
                    {
                        if (onlySpares)
                        {
                            filterList = Orders.Where(o => o.Product.SubCategory.CategoryId == 2 || o.Product.SubCategory.CategoryId == 3 || o.Product.SubCategory.CategoryId == 4).ToList();
                            foreach (var od in filterList)
                            {
                                ordersAux.Add(od);
                            }
                        }
                        else
                        {
                            filterList = Orders.Where(o => o.Product.SubCategory.CategoryId == 1).ToList();
                            foreach (var od in filterList)
                            {
                                ordersAux.Add(od);
                            }
                        }
                    }
                    else
                    {
                        if (checkSimplemak == "true")
                        {
                            filterList = Orders.Where(o => o.Product.ProductFeature != null && o.Product.ProductFeature.InHouseManufacturing == true).ToList();
                            foreach (var od in filterList)
                            {
                                ordersAux.Add(od);
                            }
                        }

                        if (checkImported == "true")
                        {
                            filterList = Orders.Where(o => o.Product.ProductFeature != null && o.Product.ProductFeature.Bought == true).ToList();
                            foreach (var od in filterList)
                            {
                                ordersAux.Add(od);
                            }
                        }

                        if (checkMachines == "true")
                        {
                            filterList = ordersAux.Where(o => o.Product.SubCategory.Prefix.Contains("MC") || o.Product.SubCategory.Prefix.Contains("MI") || o.Product.SubCategory.Prefix.Contains("MP")
                            || o.Product.SubCategory.Prefix.Contains("MV") || o.Product.SubCategory.Prefix.Contains("MA")).ToList();
                            foreach (var od in filterList)
                            {
                                ordersAux.Add(od);
                            }
                        }
                        else
                        {
                            if (checkAccesories == "true" || checkSpares == "true")
                            {
                                filterList = ordersAux.Where(o => o.Product.SubCategory.Prefix.Contains("MC") || o.Product.SubCategory.Prefix.Contains("MI") || o.Product.SubCategory.Prefix.Contains("MP")
                           || o.Product.SubCategory.Prefix.Contains("MV") || o.Product.SubCategory.Prefix.Contains("MA")).ToList();
                                foreach (var od in filterList)
                                {
                                    ordersAux.Remove(od);
                                }
                            }
                        }

                        if (checkAccesories == "true")
                        {
                            filterList = ordersAux.Where(o => o.Product.SubCategory.Prefix.Contains("AC")).ToList();
                            foreach (var od in filterList)
                            {
                                ordersAux.Add(od);
                            }
                        }
                        else
                        {
                            if (checkMachines == "true" || checkSpares == "true")
                            {
                                filterList = ordersAux.Where(o => o.Product.SubCategory.Prefix.Contains("AC")).ToList();
                                foreach (var od in filterList)
                                {
                                    ordersAux.Remove(od);
                                }
                            }
                        }

                        if (checkSpares == "true")
                        {
                            filterList = ordersAux.Where(o => o.Product.SubCategory.CategoryId == 2 || o.Product.SubCategory.CategoryId == 3 || o.Product.SubCategory.CategoryId == 4).ToList();
                            foreach (var od in filterList)
                            {
                                ordersAux.Add(od);
                            }
                        }
                        else
                        {
                            if (checkAccesories == "true" || checkMachines == "true")
                            {
                                filterList = ordersAux.Where(o => o.Product.SubCategory.CategoryId == 2 || o.Product.SubCategory.CategoryId == 3 || o.Product.SubCategory.CategoryId == 4).ToList();
                                foreach (var od in filterList)
                                {
                                    ordersAux.Remove(od);
                                }
                            }
                        }
                    }
                    #endregion

                    #region Order

                    Orders = ordersAux;

                    var sortColumnIndex = datatableParams.iSortCol_0;
                    var sortDirection = datatableParams.sSortDir_0;

                    if (sortColumnIndex == 2)
                    {
                        Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.OrderHeader.Number) : Orders.OrderByDescending(so => so.OrderHeader.Number);
                    }
                    else if (sortColumnIndex == 3)
                    {
                        Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.OrderHeader.OrderDate) : Orders.OrderByDescending(so => so.OrderHeader.OrderDate);
                    }
                    else if (sortColumnIndex == 4)
                    {
                        Orders = sortDirection == "asc" ? Orders.OrderByDescending(so => so.DeliveryDate.HasValue).ThenBy(so => so.DeliveryDate) : Orders.OrderByDescending(so => so.DeliveryDate.HasValue).ThenByDescending(so => so.DeliveryDate);
                    }
                    else if (sortColumnIndex == 5)
                    {
                        Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.ProductNumber) : Orders.OrderByDescending(so => so.ProductNumber);
                    }
                    else if (sortColumnIndex == 6)
                    {
                        Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.Product != null ? so.Product.Code.ToString() : null) : Orders.OrderByDescending(so => so.Product != null ? so.Product.Code.ToString() : null);
                    }
                    else if (sortColumnIndex == 7)
                    {
                        Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.Product != null ? so.Product.Description.ToString() : null) : Orders.OrderByDescending(so => so.Product != null ? so.Product.Description.ToString() : null);
                    }
                    else if (sortColumnIndex == 8)
                    {
                        Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.Structure != null ? so.Structure.Description.ToString() : null) : Orders.OrderByDescending(so => so.Structure != null ? so.Structure.Description.ToString() : null);
                    }
                    else if (sortColumnIndex == 9)
                    {
                        Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.SupplyVoltage != null ? so.SupplyVoltage.Description.ToString() : null) : Orders.OrderByDescending(so => so.SupplyVoltage != null ? so.SupplyVoltage.Description.ToString() : null);
                    }
                    else if (sortColumnIndex == 10)
                    {
                        Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.Quantity) : Orders.OrderByDescending(so => so.Quantity);
                    }
                    else if (sortColumnIndex == 11)
                    {
                        Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.OrderHeader != null ? (so.OrderHeader.Client != null ? so.OrderHeader.Client.BusinessName.ToString() : null) : null) : Orders.OrderByDescending(so => so.OrderHeader != null ? (so.OrderHeader.Client != null ? so.OrderHeader.Client.BusinessName.ToString() : null) : null);
                    }
                    else if (sortColumnIndex == 12)
                    {
                        Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.OrderHeader != null ? (so.OrderHeader.Client != null ? (so.OrderHeader.Client.Country != null ? so.OrderHeader.Client.Country.Description.ToString() : null) : null) : null) : Orders.OrderByDescending(so => so.OrderHeader != null ? (so.OrderHeader.Client != null ? (so.OrderHeader.Client.Country != null ? so.OrderHeader.Client.Country.Description.ToString() : null) : null) : null);
                    }
                    else if (sortColumnIndex == 13)
                    {
                        Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.SaleCategory) : Orders.OrderByDescending(so => so.SaleCategory);
                    }
                    else if (sortColumnIndex == 14)
                    {
                        Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.Product != null ? (so.Product.SubCategory != null ? so.Product.SubCategory.Description.ToString() : null) : null) : Orders.OrderByDescending(so => so.Product != null ? (so.Product.SubCategory != null ? so.Product.SubCategory.Description.ToString() : null) : null);
                    }
                    else if (sortColumnIndex == 15)
                    {
                        Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.OrderState != null ? so.OrderState.Name.ToString() : null) : Orders.OrderByDescending(so => so.OrderState != null ? so.OrderState.Name.ToString() : null);
                    }
                    else if (sortColumnIndex == 17)
                    {
                        Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.OrderHeader.DeliveredDate != null ? so.OrderHeader.DeliveredDate : null) : Orders.OrderByDescending(so => so.OrderHeader.DeliveredDate != null ? so.OrderHeader.DeliveredDate : null);
                    }
                    else if (sortColumnIndex == 18)
                    {
                        Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.OrderHeader.User) : Orders.OrderByDescending(so => so.OrderHeader.User);
                    }
                    else if (sortColumnIndex == 19)
                    {
                        Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.OrderHeader.Billed == 1 ? "Facturado" : so.OrderHeader.Billed == 2 ? "No facturado" : so.OrderHeader.Billed == 3 ? "Sin cargo" : " - ") : Orders.OrderByDescending(so => so.OrderHeader.Billed == 1 ? "Facturado" : so.OrderHeader.Billed == 2 ? "No facturado" : so.OrderHeader.Billed == 3 ? "Sin cargo" : " - ");
                    }
                    #endregion

                    var displayResult = Orders.Skip(datatableParams.iDisplayStart)
                        .Take(datatableParams.iDisplayLength).ToList();

                    var totalRecords = Orders.Count();

                    return Json(new
                    {
                        datatableParams.sEcho,
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
            else
            {
                return null;
            }
        }

        public async Task<IActionResult> LoadAllProduction(DatatableParamsViewModel datatableParams,
            string sSearch_2, string sSearch_3, string sSearch_4, string sSearch_5, string sSearch_6, string sSearch_7, string sSearch_8,
            string sSearch_9, string sSearch_10, string sSearch_11, string sSearch_12, string sSearch_13, string sSearch_14, string sSearch_15, string sSearch_16, string sSearch_17,
            string checkAll, string checkSimplemak, string checkImported, string checkMachines, string checkAccesories, string checkSpares, DateTime dateFromFilterValue, DateTime dateToFilterValue,
            string checkForStock)
        {
            // For production orders
            var response = await _OrderDetailViewManager.FindBySpecification(new OrderDetailWithAllSpecifications(4, true));
            if (response.Succeeded)
            {
                try
                {
                    var viewModel = _mapper.Map<List<OrderDetailViewModel>>(response.Data);

                    // Work with new line on Startup.cs which ignore cycles references.
                    IEnumerable<OrderDetailViewModel> Orders = viewModel;

                    // TODO: Eliminar circularReferenceWithHeaders.
                    //bool isSale = false;
                    //IEnumerable<OrderDetailViewModel> Orders = await circularReferenceWithHeaders(viewModel, isSale);

                    HashSet<OrderDetailViewModel> ordersAux = new HashSet<OrderDetailViewModel>();
                    IEnumerable<OrderDetailViewModel> filterList = new List<OrderDetailViewModel>();

                    // From date -> To date
                    Orders = Orders.Where(o => o.OrderHeader != null && (o.OrderHeader.OrderDate.Date >= dateFromFilterValue && o.OrderHeader.OrderDate.Date <= dateToFilterValue));

                    // Filter orders which belongs to a production order
                    List<OrderDetailViewModel> orderDetailsAux = new List<OrderDetailViewModel>();
                    orderDetailsAux = Orders.ToList();
                    orderDetailsAux.RemoveAll(od => od.BelongsToAProductionOrder == true);

                    Orders = orderDetailsAux;

                    //Nº OV/OP
                    if (!string.IsNullOrEmpty(sSearch_2))
                    {
                        Orders = Orders.Where(o => o.OrderHeader != null && o.OrderHeader.Number.ToString().ToLower().Contains(sSearch_2.ToLower())).ToList();
                    }
                    // Fecha OV/OP
                    if (!string.IsNullOrEmpty(sSearch_3))
                    {
                        Orders = Orders.Where(o => o.OrderHeader != null && o.OrderHeader.OrderDate.ToString("dd/MM/yyyy").Contains(sSearch_3)).ToList();
                    }
                    // Fecha de entrega
                    if (!string.IsNullOrEmpty(sSearch_4))
                    {
                        Orders = Orders.Where(o => o.DeliveryDate.HasValue && o.DeliveryDate.Value.ToString("dd/MM/yyyy").Contains(sSearch_4)).ToList();
                    }
                    // Nº prod
                    if (!string.IsNullOrEmpty(sSearch_5))
                    {
                        Orders = Orders.Where(o => o.ProductNumber.ToString().ToLower().Contains(sSearch_5.ToLower())).ToList();
                    }
                    // Código de producto
                    if (!string.IsNullOrEmpty(sSearch_6))
                    {
                        Orders = Orders.Where(o => o.Product != null && o.Product.Code.ToString().ToLower().Contains(sSearch_6.ToLower())).ToList();
                    }
                    // Descripcion de producto
                    if (!string.IsNullOrEmpty(sSearch_7))
                    {
                        Orders = Orders.Where(o => o.Product != null && o.Product.Description.ToString().ToLower().Contains(sSearch_7.ToLower())).ToList();
                    }
                    // Configuracion
                    if (!string.IsNullOrEmpty(sSearch_8))
                    {
                        Orders = Orders.Where(o => o.Structure != null && o.Structure.Description.ToString().ToLower().Contains(sSearch_8.ToLower())).ToList();
                    }
                    // Tensión
                    if (!string.IsNullOrEmpty(sSearch_9))
                    {
                        Orders = Orders.Where(o => o.SupplyVoltage != null && o.SupplyVoltage.Description.ToString().ToLower().Contains(sSearch_9.ToLower())).ToList();
                    }
                    // Cantidad
                    if (!string.IsNullOrEmpty(sSearch_10))
                    {
                        Orders = Orders.Where(o => o.Quantity.ToString().ToLower().Contains(sSearch_10.ToLower())).ToList();
                    }
                    // Cliente
                    if (!string.IsNullOrEmpty(sSearch_11))
                    {
                        Orders = Orders.Where(o => o.OrderHeader != null && o.OrderHeader.Client != null && o.OrderHeader.Client.BusinessName.ToString().ToLower().Contains(sSearch_11.ToLower())).ToList();
                    }
                    // Estado
                    if (!string.IsNullOrEmpty(sSearch_12))
                    {
                        Orders = Orders.Where(o => o.OrderState != null && o.OrderState.Name.ToString().ToLower().Contains(sSearch_12.ToLower())).ToList();
                    }
                    // Familia/Rubro producto
                    if (!string.IsNullOrEmpty(sSearch_14))
                    {
                        Orders = Orders.Where(o => o.Product != null && o.Product.SubCategory != null && o.Product.SubCategory.Description.ToString().ToLower().Contains(sSearch_14.ToLower())).ToList();
                    }
                    // Inicio OT
                    if (!string.IsNullOrEmpty(sSearch_15))
                    {

                    }
                    // Fecha entregada
                    if (!string.IsNullOrEmpty(sSearch_16))
                    {
                        Orders = Orders.Where(o => o.DeliveredDate.HasValue && o.DeliveredDate.Value.ToString("dd/MM/yyyy").Contains(sSearch_16)).ToList();
                    }
                    // Usuario (OrderHeader.User)
                    if (!string.IsNullOrEmpty(sSearch_17))
                    {
                        Orders = Orders.Where(o => o.OrderHeader.User.ToString().ToLower().Contains(sSearch_17.ToLower())).ToList();
                    }

                    if (checkAll == "true")
                    {
                        filterList = Orders.Where(o => o.Product.SubCategory.CategoryId == 1 || o.Product.SubCategory.CategoryId == 2 || o.Product.SubCategory.CategoryId == 3
                        || o.ProductNumber.Substring(0, 2).ToString() == "04").ToList();
                        foreach (var od in filterList)
                        {
                            ordersAux.Add(od);
                        }
                    }
                    else
                    {
                        if (checkSimplemak == "true")
                        {
                            filterList = Orders.Where(o => o.Product.ProductFeature != null && o.Product.ProductFeature.InHouseManufacturing == true).ToList();
                            foreach (var od in filterList)
                            {
                                ordersAux.Add(od);
                            }
                        }

                        if (checkImported == "true")
                        {
                            filterList = Orders.Where(o => o.Product.ProductFeature != null && o.Product.ProductFeature.Bought == true).ToList();
                            foreach (var od in filterList)
                            {
                                ordersAux.Add(od);
                            }
                        }

                        if (checkMachines == "true")
                        {
                            filterList = ordersAux.Where(o => o.Product.SubCategory.Prefix.Contains("MC") || o.Product.SubCategory.Prefix.Contains("MI") || o.Product.SubCategory.Prefix.Contains("MP")
                            || o.Product.SubCategory.Prefix.Contains("MV") || o.Product.SubCategory.Prefix.Contains("MA")).ToList();
                            foreach (var od in filterList)
                            {
                                ordersAux.Add(od);
                            }
                        }
                        else
                        {
                            if (checkAccesories == "true" || checkSpares == "true")
                            {
                                filterList = ordersAux.Where(o => o.Product.SubCategory.Prefix.Contains("MC") || o.Product.SubCategory.Prefix.Contains("MI") || o.Product.SubCategory.Prefix.Contains("MP")
                           || o.Product.SubCategory.Prefix.Contains("MV") || o.Product.SubCategory.Prefix.Contains("MA")).ToList();
                                foreach (var od in filterList)
                                {
                                    ordersAux.Remove(od);
                                }
                            }
                        }
                        if (checkAccesories == "true")
                        {
                            filterList = ordersAux.Where(o => o.Product.SubCategory.Prefix.Contains("AC")).ToList();
                            foreach (var od in filterList)
                            {
                                ordersAux.Add(od);
                            }
                        }
                        else
                        {
                            if (checkMachines == "true" || checkSpares == "true")
                            {
                                filterList = ordersAux.Where(o => o.Product.SubCategory.Prefix.Contains("AC")).ToList();
                                foreach (var od in filterList)
                                {
                                    ordersAux.Remove(od);
                                }
                            }
                        }
                        if (checkSpares == "true")
                        {
                            filterList = ordersAux.Where(o => o.Product.SubCategory.CategoryId == 2 || o.Product.SubCategory.CategoryId == 3).ToList();
                            foreach (var od in filterList)
                            {
                                ordersAux.Add(od);
                            }

                            filterList = ordersAux.Where(o => o.ProductNumber.Substring(0, 2).ToString() == "04").ToList();
                            foreach (var od in filterList)
                            {
                                ordersAux.Remove(od);
                            }
                        }
                        else
                        {
                            if (checkAccesories == "true" || checkMachines == "true")
                            {
                                filterList = ordersAux.Where(o => o.Product.SubCategory.CategoryId == 2 || o.Product.SubCategory.CategoryId == 3).ToList();
                                foreach (var od in filterList)
                                {
                                    ordersAux.Remove(od);
                                }
                            }
                        }

                        if (checkForStock == "true")
                        {
                            filterList = Orders.Where(o => o.ProductNumber.Substring(0, 2).ToString() == "04").ToList();
                            foreach (var od in filterList)
                            {
                                ordersAux.Add(od);
                            }

                            if (checkMachines == "false" && checkAccesories == "false" && checkSpares == "false")
                            {
                                filterList = ordersAux.Where(o => o.ProductNumber.Substring(0, 2).ToString() != "04").ToList();
                                foreach (var od in filterList)
                                {
                                    ordersAux.Remove(od);
                                }
                            }

                            if (checkMachines == "true")
                            {
                                filterList = ordersAux.Where(o => o.Product.SubCategory.Prefix.Contains("MC") || o.Product.SubCategory.Prefix.Contains("MI") || o.Product.SubCategory.Prefix.Contains("MP")
                           || o.Product.SubCategory.Prefix.Contains("MV") || o.Product.SubCategory.Prefix.Contains("MA")).ToList();
                                foreach (var od in filterList)
                                {
                                    ordersAux.Add(od);
                                }
                            }
                            if (checkAccesories == "true")
                            {
                                filterList = ordersAux.Where(o => o.Product.SubCategory.Prefix.Contains("AC")).ToList();
                                foreach (var od in filterList)
                                {
                                    ordersAux.Add(od);
                                }
                            }
                            if (checkSpares == "true")
                            {
                                filterList = ordersAux.Where(o => o.Product.SubCategory.CategoryId == 2 || o.Product.SubCategory.CategoryId == 3).ToList();
                                foreach (var od in filterList)
                                {
                                    ordersAux.Add(od);
                                }
                            }
                        }
                        else
                        {
                            filterList = ordersAux.Where(o => o.ProductNumber.Substring(0, 2).ToString() == "04").ToList();
                            foreach (var od in filterList)
                            {
                                ordersAux.Remove(od);
                            }
                        }
                    }

                    Orders = ordersAux;

                    var sortColumnIndex = datatableParams.iSortCol_0;
                    var sortDirection = datatableParams.sSortDir_0;

                    if (sortColumnIndex == 2)
                    {
                        Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.OrderHeader.Number) : Orders.OrderByDescending(so => so.OrderHeader.Number);
                    }
                    else if (sortColumnIndex == 3)
                    {
                        Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.OrderHeader.OrderDate) : Orders.OrderByDescending(so => so.OrderHeader.OrderDate);
                    }
                    else if (sortColumnIndex == 4)
                    {
                        Orders = sortDirection == "asc" ? Orders.OrderByDescending(so => so.DeliveryDate.HasValue).ThenBy(so => so.DeliveryDate) : Orders.OrderByDescending(so => so.DeliveryDate.HasValue).ThenByDescending(so => so.DeliveryDate);
                    }
                    else if (sortColumnIndex == 5)
                    {
                        Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.ProductNumber) : Orders.OrderByDescending(so => so.ProductNumber);
                    }
                    else if (sortColumnIndex == 6)
                    {
                        Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.Product != null ? so.Product.Code.ToString() : null) : Orders.OrderByDescending(so => so.Product != null ? so.Product.Code.ToString() : null);
                    }
                    else if (sortColumnIndex == 7)
                    {
                        Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.Product != null ? so.Product.Description.ToString() : null) : Orders.OrderByDescending(so => so.Product != null ? so.Product.Description.ToString() : null);
                    }
                    else if (sortColumnIndex == 8)
                    {
                        Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.Structure != null ? so.Structure.Description.ToString() : null) : Orders.OrderByDescending(so => so.Structure != null ? so.Structure.Description.ToString() : null);
                    }
                    else if (sortColumnIndex == 9)
                    {
                        Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.SupplyVoltage != null ? so.SupplyVoltage.Description.ToString() : null) : Orders.OrderByDescending(so => so.SupplyVoltage != null ? so.SupplyVoltage.Description.ToString() : null);
                    }
                    else if (sortColumnIndex == 10)
                    {
                        Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.Quantity) : Orders.OrderByDescending(so => so.Quantity);
                    }
                    else if (sortColumnIndex == 11)
                    {
                        Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.OrderHeader != null ? (so.OrderHeader.Client != null ? so.OrderHeader.Client.BusinessName.ToString() : null) : null) : Orders.OrderByDescending(so => so.OrderHeader != null ? (so.OrderHeader.Client != null ? so.OrderHeader.Client.BusinessName.ToString() : null) : null);
                    }
                    else if (sortColumnIndex == 12)
                    {
                        Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.OrderState != null ? so.OrderState.Name.ToString() : null) : Orders.OrderByDescending(so => so.OrderState != null ? so.OrderState.Name.ToString() : null);
                    }
                    else if (sortColumnIndex == 14)
                    {
                        Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.Product != null ? (so.Product.SubCategory != null ? so.Product.SubCategory.Description.ToString() : null) : null) : Orders.OrderByDescending(so => so.Product != null ? (so.Product.SubCategory != null ? so.Product.SubCategory.Description.ToString() : null) : null);
                    }
                    else if (sortColumnIndex == 15)
                    {

                    }
                    else if (sortColumnIndex == 16)
                    {
                        Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.OrderHeader.DeliveredDate != null ? so.OrderHeader.DeliveredDate : null) : Orders.OrderByDescending(so => so.OrderHeader.DeliveredDate != null ? so.OrderHeader.DeliveredDate : null);
                    }
                    else if (sortColumnIndex == 17)
                    {
                        Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.OrderHeader.User) : Orders.OrderByDescending(so => so.OrderHeader.User);
                    }

                    //else if (sortColumnIndex == 18)
                    //{
                    //    Orders = sortDirection == "asc" ? Orders.OrderBy(so => so.rowOrder) : Orders.OrderByDescending(so => so.rowOrder);
                    //}

                    var displayResult = Orders.Skip(datatableParams.iDisplayStart)
                        .Take(datatableParams.iDisplayLength).ToList();

                    var totalRecords = Orders.Count();

                    return Json(new
                    {
                        datatableParams.sEcho,
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
            else
            {
                return null;
            }
        }

        public async Task<JsonResult> OnGetCreateOrEdit(bool sale, bool onlySpares, int id = 0)
        {
            try
            {
                if (id == 0)
                {
                    var OrderViewModel = new OrderHeaderViewModel();

                    var clientsResponse = await _clientViewManager.FindBySpecification(new ClientsIsActiveExceptOperationStateDeBajaSpecification());
                    if (clientsResponse.Succeeded)
                    {
                        var clientViewModel = _mapper.Map<List<ClientDTO>>(clientsResponse.Data);
                        OrderViewModel.Clients = new SelectList(clientViewModel, nameof(ClientViewModel.Id), nameof(ClientViewModel.BusinessName), null, null);
                    }

                    OrderViewModel.OrderDate = DateTime.Now.Date.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute);
                    OrderViewModel.isSale = sale;
                    OrderViewModel.onlySpares = onlySpares;

                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", OrderViewModel) });
                }
                // To update Order
                else
                {
                    var orderHeader = await _OrderHeaderViewManager.FindBySpecification(new OrderHeaderWithAllSpecifications(id));
                    if (orderHeader.Succeeded)
                    {
                        var OrderViewModel = _mapper.Map<OrderHeaderViewModel>(orderHeader.Data.FirstOrDefault());

                        var clientsResponse = await _clientViewManager.FindBySpecification(new ClientsIsActiveExceptOperationStateDeBajaSpecification());
                        if (clientsResponse.Succeeded)
                        {
                            var clientViewModel = _mapper.Map<List<ClientDTO>>(clientsResponse.Data);
                            OrderViewModel.Clients = new SelectList(clientViewModel, nameof(ClientViewModel.Id), nameof(ClientViewModel.BusinessName), null, null);
                        }

                        OrderViewModel.TotalPrice = Math.Round(OrderViewModel.TotalPrice, 2);
                        OrderViewModel.Taxes = Math.Round(OrderViewModel.Taxes, 2);
                        OrderViewModel.SecureAndFreightCosts = Math.Round(OrderViewModel.SecureAndFreightCosts, 2);
                        OrderViewModel.Bonus = Math.Round(OrderViewModel.Bonus, 2);
                        OrderViewModel.OrderTotalPrice = Math.Round(OrderViewModel.OrderTotalPrice, 2);

                        if (OrderViewModel.ValidOfferDate.HasValue)
                        {
                            OrderViewModel.ValidOfferDate.Value.ToString("yyyy-MM-dd");
                        }

                        OrderViewModel.isSale = sale;
                        OrderViewModel.onlySpares = onlySpares;

                        // Tengo que traerme el DeliveryDateHistory
                        foreach (var odId in OrderViewModel.OrderDetails.Select(od => od.Id))
                        {
                            var getDeliveryDateHistoryByODId = await _deliveryDateHistoryViewManager.FindBySpecification(new DeliveryDateHistoryByODIdSpecifications(odId));
                            if (getDeliveryDateHistoryByODId.Succeeded)
                            {
                                var deliveryDateHistoryVMs = _mapper.Map<List<DeliveryDateHistoryViewModel>>(getDeliveryDateHistoryByODId.Data.ToList());
                                foreach (var deliveryDateHistoryVM in deliveryDateHistoryVMs)
                                {
                                    var usersList = _identityContext.Users.ToList();

                                    foreach (var user in usersList)
                                    {
                                        if (user.Id == deliveryDateHistoryVM.CreatedBy)
                                        {
                                            deliveryDateHistoryVM.CreatedBy = user.FirstAndLastName.ToString();
                                        }
                                    }

                                    OrderViewModel.deliveryDateHistoryVMs.Add(deliveryDateHistoryVM);
                                }
                            }
                        }
                        // Asignar sale operation
                        if (OrderViewModel.SaleOperationId is not null)
                        {
                            var saleOperationResponse = await _saleOperationViewManager.FindBySpecification(new SaleOperationForSelect2((int)OrderViewModel.SaleOperationId));

                            if (saleOperationResponse.Succeeded)
                            {
                                OrderViewModel.SaleOperation = _mapper.Map<SaleOperationViewModel>(saleOperationResponse.Data.FirstOrDefault());
                            }
                        }

                        return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", OrderViewModel) });
                    }
                }
                return new JsonResult(new { isValid = false });
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return new JsonResult(new { isValid = false });
            }
        }
        public async Task<JsonResult> LoadExistingOperations(int id)
        {
            var saleOperationResponse = await _saleOperationViewManager.FindBySpecification(new SaleOperationByClientId(id, "a"));
            if (saleOperationResponse.Succeeded)
            {
                List<Select2ViewModel> mapSelect2Data = new List<Select2ViewModel>();
                var saleOperationViewModel = _mapper.Map<List<SaleOperationViewModel>>(saleOperationResponse.Data.ToList());

                foreach (var saleOperation in saleOperationViewModel)
                {
                    if (saleOperation.State != "Concretada" && saleOperation.State != "Concretized")
                    {
                        mapSelect2Data.Add(new Select2ViewModel() { Id = saleOperation.Id, Text = saleOperation.Operation });
                    }
                }

                //return new JsonResult(new { saleOperations = mapSelect2Data });
                return Json(saleOperationResponse);
            }
            else
            {
                return null;
            }
        }

        public async Task<JsonResult> LoadProductsSelect2(bool checkSimplemak, bool checkMachines, bool checkAccesories, bool checkSpares, bool checkStockComponents)
        {
            try
            {
                HashSet<ProductDTO> productsAux = new HashSet<ProductDTO>();
                IEnumerable<ProductDTO> filterList = new List<ProductDTO>();

                List<Select2ViewModel> mapProductsOnSelect2 = new List<Select2ViewModel>();
                IEnumerable<ProductDTO> productDTOs = null;

                var productsResponse = await _productViewManager.FindBySpecification(new ProductByCategoryAndInHouseSpecification(null, checkSimplemak));

                if (productsResponse.Succeeded)
                {
                    // check true => Máquinas (categoria "Maquinas y accesorios" con Id = 1 que no tengan subcategoria "Accesorios")
                    // check true => Accesorios (Todo lo que empiece con AC, subcategoria con Id = 19 y Prefix = AC)
                    // check true => Repuestos (si su categoria es "Conjuntos" con Id = 2 o "Piezas individuales" con Id = 3 o "Componentes comprados" con Id = 4)
                    // check true => Componentes para stock (si su categoria es "Conjuntos" con Id = 2 o "Piezas individuales" con Id = 3)

                    if (checkStockComponents)
                    {
                        filterList = productsResponse.Data.Where(p => p.SubCategory.CategoryId == 2 || p.SubCategory.CategoryId == 3).ToList();
                        foreach (var pr in filterList)
                        {
                            productsAux.Add(pr);
                        }

                        productDTOs = productsAux.OrderBy(p => p.Description);
                    }
                    else
                    {
                        if (checkMachines)
                        {
                            filterList = productsResponse.Data.Where(p => p.SubCategory.CategoryId == 1 && p.SubCategoryId != 19).ToList();
                            foreach (var pr in filterList)
                            {
                                productsAux.Add(pr);
                            }
                        }

                        if (checkAccesories)
                        {
                            filterList = productsResponse.Data.Where(p => p.SubCategory.CategoryId == 1 && p.SubCategoryId == 19).ToList();
                            foreach (var pr in filterList)
                            {
                                productsAux.Add(pr);
                            }
                        }

                        if (checkSpares)
                        {
                            filterList = productsResponse.Data.Where(p => p.SubCategory.CategoryId == 2 || p.SubCategory.CategoryId == 3 || p.SubCategory.CategoryId == 4).ToList();
                            foreach (var pr in filterList)
                            {
                                productsAux.Add(pr);
                            }
                        }

                        if (checkMachines || checkAccesories || checkSpares)
                        {
                            productDTOs = productsAux.OrderBy(p => p.Description);
                        }
                        else
                        {
                            productDTOs = productsResponse.Data.OrderBy(p => p.Description);
                        }
                    }

                    if (productDTOs.Count() > 0)
                    {
                        var products = new SelectList((from p in productDTOs.ToList() select new { Id = p.Id, CodeAndDescription = p.Code + " - " + p.Description }), "Id", "CodeAndDescription", null);
                        return new JsonResult(new { isValid = true, products = products });
                    }
                    else
                    {
                        return new JsonResult(new { isValid = false, products = "" });
                    }
                }
                else
                {
                    return new JsonResult(new { isValid = false, products = "" });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return new JsonResult(new { isValid = false, products = "" });
            }
        }

        // Load select2 with ProductNumber OPS finished
        public async Task<JsonResult> LoadStockProducts()
        {
            try
            {
                var getOPsFinished = await _OrderDetailViewManager.FindBySpecification(new OrderDetailWithAllSpecifications(1, true, false));

                if (getOPsFinished.Succeeded)
                {
                    if (getOPsFinished.Data.Count() > 0)
                    {
                        var OpsFinishedVM = _mapper.Map<List<OrderDetailViewModel>>(getOPsFinished.Data.ToList());

                        OpsFinishedVM.RemoveAll(opf => opf.BelongsToAProductionOrder == true);

                        OpsFinishedVM = OpsFinishedVM.Where(od => od.OrderHeader.ClientId == null).ToList();

                        var getAllOrderDetailsWhichBelongsToAProductionOrder = await _OrderDetailViewManager.FindBySpecification(new OrderDetailBelongsToAProductionOrderSpecification(true));
                        if (getAllOrderDetailsWhichBelongsToAProductionOrder.Succeeded)
                        {
                            var detailsBelongsToAProductionOrder = _mapper.Map<List<OrderDetailViewModel>>(getAllOrderDetailsWhichBelongsToAProductionOrder.Data.ToList());

                            foreach (var od in detailsBelongsToAProductionOrder)
                            {
                                OpsFinishedVM.RemoveAll(x => x.ProductNumber == od.ProductNumber);
                            }

                            var stockProducts = new SelectList((from OpFinish in OpsFinishedVM.Where(op => op.Structure != null).OrderBy(op => op.Product.CodeAndDescription).ThenBy(op => op.Structure.Description).ToList() select new { value = OpFinish.Id, text = OpFinish.ProductNumber, grp = OpFinish.Product.CodeAndDescription + " - " + OpFinish.Structure.Description }), "value", "text", null, "grp");
                            return new JsonResult(new { isValid = true, stockProducts = stockProducts });

                        }
                        else
                        {
                            return new JsonResult(new { isValid = false, stockProducts = "" });
                        }

                        //var getAllOrderDetailsWhichBelongToASaleIsTrue = await _OrderDetailViewManager.FindBySpecification(new OrderDetailBelongsToASaleOrderSpecification(true));
                        //if (getAllOrderDetailsWhichBelongToASaleIsTrue.Succeeded)
                        //{
                        //    var odetailsBelongToASale = _mapper.Map<List<OrderDetailViewModel>>(getAllOrderDetailsWhichBelongToASaleIsTrue.Data.ToList());

                        //    foreach (var od in odetailsBelongToASale)
                        //    {
                        //        OpsFinishedVM.RemoveAll(x => x.ProductNumber == od.ProductNumber);
                        //    }

                        //    var stockProducts = new SelectList((from OpFinish in OpsFinishedVM.Where(op => op.Structure != null).OrderBy(op => op.Product.CodeAndDescription).ThenBy(op => op.Structure.Description).ToList() select new { value = OpFinish.Id, text = OpFinish.ProductNumber, grp = OpFinish.Product.CodeAndDescription + " - " + OpFinish.Structure.Description }), "value", "text", null, "grp");
                        //    return new JsonResult(new { isValid = true, stockProducts = stockProducts });

                        //}
                        //else
                        //{
                        //    return new JsonResult(new { isValid = false, stockProducts = "" });
                        //}
                    }
                    else
                    {
                        return new JsonResult(new { isValid = false, stockProducts = "" });
                    }
                }
                else
                {
                    return new JsonResult(new { isValid = false, stockProducts = "" });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return new JsonResult(new { isValid = false, stockProducts = "" });
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(int id, OrderHeaderViewModel OrderHeaderViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool isGeneratedFromMissingProducts = false;

                    if (OrderHeaderViewModel.isSale)
                    {
                        // Si es una orden de venta y el usuario no seleccionó un cliente, se le advierte.
                        if (OrderHeaderViewModel.ClientId == -1)
                        {
                            _notify.Warning(_localizer["You must select a client for sale order."]);
                            return new JsonResult(new { isValid = false, isSale = OrderHeaderViewModel.isSale, renumberProductNumber = false });
                        }


                        if (OrderHeaderViewModel.NewOperation)
                        {

                            var saleOperations = await _saleOperationViewManager.FindBySpecification(new SaleOperationByClientId(OrderHeaderViewModel.ClientId.Value));

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
                                    StartDate = OrderHeaderViewModel.OrderDate,
                                    State = _localizer["In progress"],
                                    ClientId = OrderHeaderViewModel.ClientId.Value,
                                    Operation = operationName,
                                    OperationNumber = operationNumber,
                                    Client = OrderHeaderViewModel.Client,
                                };

                                if (OrderHeaderViewModel.SaleOperationId != null && OrderHeaderViewModel.NewOperation)
                                {
                                    var saleOperationDTO = _mapper.Map<SaleOperationDTO>(saleOperationVM);
                                    var result = await _saleOperationViewManager.CreateAsync(saleOperationDTO);

                                    OrderHeaderViewModel.SaleOperationId = result.Data;
                                }
                                else
                                {
                                    OrderHeaderViewModel.SaleOperation = saleOperationVM;
                                }
                            }
                        }

                        // Si no selecciona una sale operation
                        if (OrderHeaderViewModel.SaleOperationId == null && OrderHeaderViewModel.ExistingOperation)
                        {
                            _notify.Warning(_localizer["You must select a sale operation"]);
                            return new JsonResult(new { isValid = false, isSale = OrderHeaderViewModel.isSale, renumberProductNumber = false });
                        }

                    }

                    if (id == 0)
                    {
                        // Create
                        var detailsOrder = OrderHeaderViewModel.OrderDetails.Where(x => x != null).ToList();
                        if (detailsOrder.Count() > 0)
                        {
                            List<OrderDetailViewModel> detailsOrderSetNullReferences = new List<OrderDetailViewModel>();
                            foreach (var d in detailsOrder)
                            {
                                OrderDetailViewModel od = new OrderDetailViewModel();
                                od = d;
                                if (d.StructureId == -1)
                                {
                                    od.StructureId = null;
                                }
                                if (d.SupplyVoltageId == -1 || d.SupplyVoltageId == null)
                                {
                                    od.SupplyVoltageId = null;
                                }
                                detailsOrderSetNullReferences.Add(od);
                            }

                            OrderHeaderViewModel.OrderDetails = detailsOrderSetNullReferences;

                            var detailsWithQuantityNull = OrderHeaderViewModel.OrderDetails.Where(x => x.Quantity == 0).ToList();
                            if (detailsWithQuantityNull.Count > 0)
                            {
                                _notify.Warning(_localizer["The quantity cannot be zero."]);
                                return new JsonResult(new { isValid = false, isSale = OrderHeaderViewModel.isSale, renumberProductNumber = false });
                            }
                            if (OrderHeaderViewModel.isSale)
                            {
                                var detailsWithPriceZero = OrderHeaderViewModel.OrderDetails.Where(x => x.UnitPrice == 0).ToList();
                                if (detailsWithPriceZero.Count > 0)
                                {
                                    _notify.Warning(_localizer["Unit price cannot be zero."]);
                                    return new JsonResult(new { isValid = false, isSale = OrderHeaderViewModel.isSale, renumberProductNumber = false });
                                }
                            }

                            // Tengo que agregar validación para ProductNumber, para que dos usuarios distintos no creen ordenes con el mismo ProductNumber y advertirle al usuario que el
                            // que se le muestra ya está creado por otro usuario, entonces se crearan ProductNumber += 1

                            if (OrderHeaderViewModel.OrderDetails.All(od => od.BelongsToAProductionOrder != true))
                            {
                                var existsProductNumber = CheckIfExistsProductNumberToSave(OrderHeaderViewModel.OrderDetails);
                                if (existsProductNumber.Result)
                                {
                                    // Si es true -> Quiere decir que existe un ProductNumber igual al que se va a guardar, entonces se advierte...
                                    return new JsonResult(new { isValid = true, isSale = OrderHeaderViewModel.isSale, renumberProductNumber = true });
                                }
                            }
                            else if (OrderHeaderViewModel.OrderDetails.Any(od => od.BelongsToAProductionOrder == true) && OrderHeaderViewModel.OrderDetails.Any(od => od.BelongsToAProductionOrder == false))
                            {
                                var existsProductNumber = CheckIfExistsProductNumberToSave(OrderHeaderViewModel.OrderDetails);
                                if (existsProductNumber.Result)
                                {
                                    // Si es true -> Quiere decir que existe un ProductNumber igual al que se va a guardar, entonces se advierte...
                                    return new JsonResult(new { isValid = true, isSale = OrderHeaderViewModel.isSale, renumberProductNumber = true });
                                }
                            }
                            else if (OrderHeaderViewModel.OrderDetails.All(od => od.BelongsToAProductionOrder == true))
                            {
                                foreach (var od in OrderHeaderViewModel.OrderDetails)
                                {
                                    var getOrderByProductNumber = await _OrderHeaderViewManager.FindBySpecification(new OrderHeaderWithAllSpecifications(od.ProductNumber.Trim()));
                                    if (getOrderByProductNumber.Succeeded)
                                    {
                                        foreach (var oh in getOrderByProductNumber.Data)
                                        {
                                            var orderHeaderVM = _mapper.Map<OrderHeaderViewModel>(oh);
                                            if (orderHeaderVM.ClientId == null)
                                            {
                                                orderHeaderVM.ClientId = OrderHeaderViewModel.ClientId;
                                                var orderHeaderDTO = _mapper.Map<OrderHeaderDTO>(orderHeaderVM);
                                                await _OrderHeaderViewManager.UpdateAsync(orderHeaderDTO);

                                                var orderDetailsVM = orderHeaderVM.OrderDetails;
                                                foreach (var odVM in orderDetailsVM)
                                                {
                                                    if (odVM.Id == od.OrderDetailIdBelongsToASaleOrder)
                                                    {
                                                        odVM.BelongsToASaleOrder = true;
                                                        var orderDetailDTO = _mapper.Map<OrderDetailDTO>(odVM);
                                                        await _OrderDetailViewManager.UpdateAsync(orderDetailDTO);
                                                    }
                                                }
                                            }
                                        }
                                    }

                                }
                            }

                            // Por defecto "Pendiente"
                            OrderHeaderViewModel.OrderStateId = 1;

                            // Por defecto "No facturado"
                            OrderHeaderViewModel.Billed = 2;

                            // Si se genero desde productos faltantes, debo actualizar el estado de los faltantes "A producción"
                            if (OrderHeaderViewModel.OrderDetails.Any(x => x.MissingProductId.HasValue))
                            {
                                isGeneratedFromMissingProducts = true;
                                foreach (var od in OrderHeaderViewModel.OrderDetails)
                                {
                                    if (od.MissingProductId.HasValue)
                                    {
                                        var getMPById = await _missingProductViewManager.FindWithAllSpecification(new MissingProductByIdSpecification(od.MissingProductId.Value));
                                        if (getMPById.Succeeded && getMPById.Data != null)
                                        {
                                            var mpVM = _mapper.Map<MissingProductViewModel>(getMPById.Data.First());
                                            // Seteo el estado "A producción"
                                            mpVM.StateMissingProductId = 7;
                                            var mpDTO = _mapper.Map<MissingProductDTO>(mpVM);
                                            await _missingProductViewManager.UpdateAsync(mpDTO);
                                        }
                                    }
                                }
                            }

                            // Set user
                            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                            OrderHeaderViewModel.User = currentUser.LastName + ", " + currentUser.FirstName;

                            var OrderHeaderDTO = _mapper.Map<OrderHeaderDTO>(OrderHeaderViewModel);
                            var result = await _OrderHeaderViewManager.CreateAsync(OrderHeaderDTO);
                            if (result.Succeeded)
                            {
                                id = result.Data;

                                // Se deja para un futuro retomar el ordenamiento: Se obtiene el mayor rowOrder y se suman segun la cantidad de OrderDetails que se agreguen, pero deberían ordernarse según DeliveryDate.
                                //int rowOrder = 0;
                                //var orderDetailBySpecification = _OrderDetailViewManager.FindBySpecification(new OrderDetailEmptySpecification());
                                //if (orderDetailBySpecification.Result.Succeeded)
                                //{
                                //    DateTime maxDeliveryDate = orderDetailBySpecification.Result.Data.Where(od => od.DeliveryDate.HasValue && od.OrderHeaderId != id).Max(od => od.DeliveryDate.Value);

                                //    var rowOrderBySpecification = _OrderDetailViewManager.FindBySpecification(new OrderDetailGetMaxRowOrderSpecification(maxDeliveryDate));
                                //    if (rowOrderBySpecification.Result.Succeeded)
                                //    {
                                //        rowOrder = rowOrderBySpecification.Result.Data.Where(od => od.rowOrder.HasValue).Max(od => od.rowOrder.Value);
                                //    }

                                //    if (rowOrder != 0)
                                //    {
                                //        foreach (var od in OrderHeaderDTO.OrderDetails)
                                //        {
                                //            rowOrder++;
                                //            await _OrderDetailViewManager.UpdateRowOrderFromOrderDetailAsync(od.Id, rowOrder);
                                //        }
                                //    }

                                //}

                                _notify.Success(_localizer["Order created."]);
                            }
                            else
                            {
                                _notify.Error(result.Message);
                            }
                        }
                        else
                        {
                            _notify.Warning(_localizer["You must add detail for this  order."]);
                            return new JsonResult(new { isValid = false, isSale = OrderHeaderViewModel.isSale, renumberProductNumber = false });
                        }
                    }
                    // For update only observaciones, transporte y fecha de entrega.
                    else
                    {
                        var orderHeaderDto = _mapper.Map<OrderHeaderDTO>(OrderHeaderViewModel);
                        var result = await _OrderHeaderViewManager.UpdateSomePropertiesAsync(orderHeaderDto);
                        if (result.Succeeded) _notify.Information(_localizer["Order updated."]);
                    }

                    return new JsonResult(new { isValid = true, isSale = OrderHeaderViewModel.isSale, renumberProductNumber = false, isGeneratedFromMissingProducts = isGeneratedFromMissingProducts });
                }
                else
                {
                    return new JsonResult(new { isValid = false, isSale = OrderHeaderViewModel.isSale, renumberProductNumber = false });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return new JsonResult(new { isValid = false, isSale = OrderHeaderViewModel.isSale, renumberProductNumber = false });
            }
        }

        /* Método para evento drag & drop */
        //public async Task UpdateRowOrderFromDragAndDrop(string diff)
        //{
        //    try
        //    {
        //        if (diff != null)
        //        {
        //            List<int> orderIds = new List<int>();
        //            String[] diferencias = diff.Split("*");
        //            foreach (var d in diferencias)
        //            {
        //                if (d != "")
        //                {
        //                    var diferencia = d.Split(",");
        //                    int newData = int.Parse(diferencia[0]);
        //                    int oldData = int.Parse(diferencia[1]);

        //                    var getOrderDetailByRowOrder = await _OrderDetailViewManager.FindBySpecification(new OrderDetailByRowOrderSpecification(oldData));

        //                    if (getOrderDetailByRowOrder.Succeeded)
        //                    {
        //                        foreach (var od in getOrderDetailByRowOrder.Data)
        //                        {
        //                            if (!orderIds.Contains(od.Id))
        //                            {
        //                                orderIds.Add(od.Id);
        //                                var orderDetailViewModel = _mapper.Map<OrderDetailViewModel>(od);
        //                                await _OrderDetailViewManager.UpdateRowOrderDragAndDropAsync(od.Id, newData);
        //                            }

        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _notify.Error(ex.Message);
        //    }
        //}

        public async Task<bool> CheckIfExistsProductNumberToSave(IList<OrderDetailViewModel> orderDetailViewModels)
        {
            try
            {
                bool exists = false;
                List<int> productNumbersSM = new List<int>();
                List<int> productNumbersIM = new List<int>();
                List<int> productNumbersSA = new List<int>();
                List<int> productNumbersIA = new List<int>();
                int maxProductNumberSR = 0;
                int maxProductNumberIR = 0;
                int maxProductNumberSCS = 0;

                // Obtengo el maximo ProductNumber segun su categoría (por el prefijo) desde la base de datos.
                var allOrderDetails = await _OrderDetailViewManager.FindBySpecification(new OrderDetailEmptySpecification());
                if (allOrderDetails.Succeeded)
                {
                    if (allOrderDetails.Data.Count > 0)
                    {
                        foreach (var od in allOrderDetails.Data)
                        {
                            if (od.ProductNumber.Substring(0, 2).ToString() == "01")
                            {
                                productNumbersSM.Add(Int32.Parse(od.ProductNumber.Substring(3, 5).ToString()));
                            }
                            else if (od.ProductNumber.Substring(0, 2).ToString() == "11")
                            {
                                productNumbersIM.Add(Int32.Parse(od.ProductNumber.Substring(3, 5).ToString()));
                            }
                            else if (od.ProductNumber.Substring(0, 2).ToString() == "02")
                            {
                                productNumbersSA.Add(Int32.Parse(od.ProductNumber.Substring(3, 5).ToString()));
                            }
                            else if (od.ProductNumber.Substring(0, 2).ToString() == "12")
                            {
                                productNumbersIA.Add(Int32.Parse(od.ProductNumber.Substring(3, 5).ToString()));
                            }
                            else if (od.ProductNumber.Substring(0, 2).ToString() == "03")
                            {
                                if (maxProductNumberSR < Int32.Parse(od.ProductNumber.Substring(3, 5).ToString()))
                                {
                                    maxProductNumberSR = Int32.Parse(od.ProductNumber.Substring(3, 5).ToString());
                                }
                            }
                            else if (od.ProductNumber.Substring(0, 2).ToString() == "13")
                            {
                                if (maxProductNumberIR < Int32.Parse(od.ProductNumber.Substring(3, 5).ToString()))
                                {
                                    maxProductNumberIR = Int32.Parse(od.ProductNumber.Substring(3, 5).ToString());
                                }
                            }
                            else if (od.ProductNumber.Substring(0, 2).ToString() == "04")
                            {
                                if (maxProductNumberSCS < Int32.Parse(od.ProductNumber.Substring(3, 5).ToString()))
                                {
                                    maxProductNumberSCS = Int32.Parse(od.ProductNumber.Substring(3, 5).ToString());
                                }
                            }
                        }

                        foreach (var od in orderDetailViewModels)
                        {
                            if (od.ProductNumber.Substring(0, 2).ToString() == "01")
                            {
                                foreach (var pn in productNumbersSM)
                                {
                                    if (pn == Convert.ToInt32(od.ProductNumber.Substring(3, 5).ToString()))
                                    {
                                        exists = true;
                                    }
                                }
                                return exists;
                            }
                            else if (od.ProductNumber.Substring(0, 2).ToString() == "11")
                            {
                                foreach (var pn in productNumbersIM)
                                {
                                    if (pn == Convert.ToInt32(od.ProductNumber.Substring(3, 5).ToString()))
                                    {
                                        exists = true;
                                    }
                                }
                                return exists;
                            }
                            else if (od.ProductNumber.Substring(0, 2).ToString() == "02")
                            {
                                foreach (var pn in productNumbersSA)
                                {
                                    if (pn == Convert.ToInt32(od.ProductNumber.Substring(3, 5).ToString()))
                                    {
                                        exists = true;
                                    }
                                }
                                return exists;
                            }
                            else if (od.ProductNumber.Substring(0, 2).ToString() == "12")
                            {
                                foreach (var pn in productNumbersIA)
                                {
                                    if (pn == Convert.ToInt32(od.ProductNumber.Substring(3, 5).ToString()))
                                    {
                                        exists = true;
                                    }
                                }
                                return exists;
                            }
                            else if (od.ProductNumber.Substring(0, 2).ToString() == "03")
                            {
                                if (maxProductNumberSR == Convert.ToInt32(od.ProductNumber.Substring(3, 5).ToString()))
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else if (od.ProductNumber.Substring(0, 2).ToString() == "13")
                            {
                                if (maxProductNumberIR == Convert.ToInt32(od.ProductNumber.Substring(3, 5).ToString()))
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else if (od.ProductNumber.Substring(0, 2).ToString() == "04")
                            {
                                if (maxProductNumberSCS == Convert.ToInt32(od.ProductNumber.Substring(3, 5).ToString()))
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

                return false;
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return false;
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetClientDetailsById(int id)
        {
            try
            {
                if (id != -1)
                {
                    var clientsResponse = await _clientViewManager.FindBySpecification(new ClientsIsActiveExceptOperationStateDeBajaSpecification(id));
                    if (clientsResponse.Succeeded)
                    {
                        return new JsonResult(new { isValid = true, data = clientsResponse.Data.FirstOrDefault() });
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
        public async Task<JsonResult> GetProductById(int id)
        {
            try
            {
                if (id != -1)
                {
                    var response = await _productViewManager.FindBySpecification(new ProductWithAllSpecification(id));
                    if (response.Succeeded)
                    {
                        ProductDTO product = response.Data.FirstOrDefault();
                        return new JsonResult(new { isValid = true, product = product });
                    }
                    else
                    {
                        return new JsonResult(new { isValid = false, product = "" });
                    }
                }
                else
                {
                    return new JsonResult(new { isValid = false, product = "" });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return new JsonResult(new { isValid = false, product = "" });
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetPriceForSelectedProduct(int productId, int? structureId)
        {
            try
            {
                if (structureId == 0)
                {
                    structureId = null;
                }

                var pricesResponse = await _priceViewManager.FindBySpecification(new PriceSpecification(productId, structureId));
                if (pricesResponse.Succeeded)
                {
                    if (pricesResponse.Data.Count > 0)
                    {
                        return new JsonResult(new { isValid = true, price = pricesResponse.Data.First() });
                    }
                    else
                    {
                        return new JsonResult(new { isValid = false, price = "" });
                    }
                }
                else
                {
                    return new JsonResult(new { isValid = false, price = "" });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return new JsonResult(new { isValid = false, price = "" });
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

        [HttpGet]
        public async Task<JsonResult> GetSupplyVoltagesByProductId(int id)
        {
            try
            {
                if (id != -1)
                {
                    var productSupplyVoltages = await _productSupplyVoltageViewManager.FindBySpecification(new ProductSupplyVoltageByProductIdSpecification(id));
                    if (productSupplyVoltages.Succeeded)
                    {
                        if (productSupplyVoltages.Data.Count > 0)
                        {
                            var selectList = new SelectList((from psv in productSupplyVoltages.Data.ToList() select new { Id = psv.SupplyVoltage.Id, Description = psv.SupplyVoltage.Description }), "Id", "Description", null);
                            return new JsonResult(new { isValid = true, supplyVoltages = selectList });
                        }
                        else
                        {
                            return new JsonResult(new { isValid = false, supplyVoltages = "" });
                        }
                    }
                    else
                    {
                        return new JsonResult(new { isValid = false, supplyVoltages = "" });
                    }
                }
                else
                {
                    return new JsonResult(new { isValid = false, supplyVoltages = "" });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return new JsonResult(new { isValid = false, supplyVoltages = "" });
            }
        }

        // Setear numero SM
        [HttpGet]
        public async Task<JsonResult> GetLastProductNumberForMachines()
        {
            try
            {
                // Numero que pasa Anto segun la ultima venta de maquinas.
                int maxProductNumber = 0;
                var allOrders = await _OrderHeaderViewManager.FindBySpecification(new OrderHeaderWithAllDetailsSpecification());
                if (allOrders.Succeeded)
                {
                    if (allOrders.Data.Count > 0)
                    {
                        foreach (var o in allOrders.Data)
                        {
                            // Filtro por maquinas
                            var machineOrders = o.OrderDetails.Where(x => x.ProductNumber.Substring(0, 2).ToString() == "01");
                            foreach (var d in machineOrders)
                            {
                                // Itero cada una y valido que ProductNumber es el mayor.
                                if (maxProductNumber < Int32.Parse(d.ProductNumber.Substring(3, 5).ToString()))
                                {
                                    maxProductNumber = Int32.Parse(d.ProductNumber.Substring(3, 5).ToString());
                                }
                            }
                        }
                        if (maxProductNumber != 0)
                        {
                            return new JsonResult(new { maxProductNumber = maxProductNumber });
                        }
                        else
                        {
                            return new JsonResult(new { maxProductNumber = maxProductNumber });
                        }
                    }
                    else
                    {
                        return new JsonResult(new { maxProductNumber = maxProductNumber });
                    }
                }
                else
                {
                    return new JsonResult(new { maxProductNumber = maxProductNumber });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return new JsonResult(new { maxProductNumber = 0 });
            }
        }

        // Setear numero IM
        [HttpGet]
        public async Task<JsonResult> GetLastProductNumberForMachinesImported()
        {
            try
            {
                // Numero que pasa Anto segun la ultima venta de maquinas.
                int maxProductNumber = 0;
                var allOrders = await _OrderHeaderViewManager.FindBySpecification(new OrderHeaderWithAllDetailsSpecification());
                if (allOrders.Succeeded)
                {
                    if (allOrders.Data.Count > 0)
                    {
                        foreach (var o in allOrders.Data)
                        {
                            // Filtro por maquinas
                            var machineOrders = o.OrderDetails.Where(x => x.ProductNumber.Substring(0, 2).ToString() == "11");
                            foreach (var d in machineOrders)
                            {
                                // Itero cada una y valido que ProductNumber es el mayor.
                                if (maxProductNumber < Int32.Parse(d.ProductNumber.Substring(3, 5).ToString()))
                                {
                                    maxProductNumber = Int32.Parse(d.ProductNumber.Substring(3, 5).ToString());
                                }
                            }
                        }
                        if (maxProductNumber != 0)
                        {
                            return new JsonResult(new { maxProductNumber = maxProductNumber });
                        }
                        else
                        {
                            return new JsonResult(new { maxProductNumber = maxProductNumber });
                        }
                    }
                    else
                    {
                        return new JsonResult(new { maxProductNumber = maxProductNumber });
                    }
                }
                else
                {
                    return new JsonResult(new { maxProductNumber = maxProductNumber });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return new JsonResult(new { maxProductNumber = 0 });
            }
        }

        // Setear numero SA
        [HttpGet]
        public async Task<JsonResult> GetLastProductNumberForAccessories()
        {
            try
            {
                // Numero que pasa Anto segun la ultima venta de accesorios.
                int maxProductNumber = 0;
                var allOrders = await _OrderHeaderViewManager.FindBySpecification(new OrderHeaderWithAllDetailsSpecification());
                if (allOrders.Succeeded)
                {
                    if (allOrders.Data.Count > 0)
                    {
                        foreach (var o in allOrders.Data)
                        {
                            // Filtro por accesorios
                            var accessoriesOrders = o.OrderDetails.Where(x => x.ProductNumber.Substring(0, 2).ToString() == "02");
                            foreach (var d in accessoriesOrders)
                            {
                                // Itero cada una y valido que ProductNumber es el mayor.
                                if (maxProductNumber < Int32.Parse(d.ProductNumber.Substring(3, 5).ToString()))
                                {
                                    maxProductNumber = Int32.Parse(d.ProductNumber.Substring(3, 5).ToString());
                                }
                            }
                        }
                        if (maxProductNumber != 0)
                        {
                            return new JsonResult(new { maxProductNumber = maxProductNumber });
                        }
                        else
                        {
                            return new JsonResult(new { maxProductNumber = maxProductNumber });
                        }
                    }
                    else
                    {
                        return new JsonResult(new { maxProductNumber = maxProductNumber });
                    }
                }
                else
                {
                    return new JsonResult(new { maxProductNumber = maxProductNumber });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return new JsonResult(new { maxProductNumber = 0 });
            }
        }

        // Setear numero IA
        [HttpGet]
        public async Task<JsonResult> GetLastProductNumberForAccessoriesImported()
        {
            try
            {
                // Numero que pasa Anto segun la ultima venta de accesorios.
                int maxProductNumber = 0;
                var allOrders = await _OrderHeaderViewManager.FindBySpecification(new OrderHeaderWithAllDetailsSpecification());
                if (allOrders.Succeeded)
                {
                    if (allOrders.Data.Count > 0)
                    {
                        foreach (var o in allOrders.Data)
                        {
                            // Filtro por accesorios
                            var accessoriesOrders = o.OrderDetails.Where(x => x.ProductNumber.Substring(0, 2).ToString() == "12");
                            foreach (var d in accessoriesOrders)
                            {
                                // Itero cada una y valido que ProductNumber es el mayor.
                                if (maxProductNumber < Int32.Parse(d.ProductNumber.Substring(3, 5).ToString()))
                                {
                                    maxProductNumber = Int32.Parse(d.ProductNumber.Substring(3, 5).ToString());
                                }
                            }
                        }
                        if (maxProductNumber != 0)
                        {
                            return new JsonResult(new { maxProductNumber = maxProductNumber });
                        }
                        else
                        {
                            return new JsonResult(new { maxProductNumber = maxProductNumber });
                        }
                    }
                    else
                    {
                        return new JsonResult(new { maxProductNumber = maxProductNumber });
                    }
                }
                else
                {
                    return new JsonResult(new { maxProductNumber = maxProductNumber });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return new JsonResult(new { maxProductNumber = 0 });
            }
        }

        // Setear numero SR
        [HttpGet]
        public async Task<JsonResult> GetLastProductNumberForSpare()
        {
            try
            {
                // Numero que pasa Anto segun la ultima venta de repuestos.
                int maxProductNumber = 0;
                var allOrders = await _OrderHeaderViewManager.FindBySpecification(new OrderHeaderWithAllDetailsSpecification());
                if (allOrders.Succeeded)
                {
                    if (allOrders.Data.Count > 0)
                    {
                        foreach (var o in allOrders.Data)
                        {
                            // Filtro por productos
                            var sparesOrders = o.OrderDetails.Where(x => x.ProductNumber.Substring(0, 2).ToString() == "03");
                            foreach (var d in sparesOrders)
                            {
                                // Itero cada una y valido que ProductNumber es el mayor.
                                if (maxProductNumber < Int32.Parse(d.ProductNumber.Substring(3, 5).ToString()))
                                {
                                    maxProductNumber = Int32.Parse(d.ProductNumber.Substring(3, 5).ToString());
                                }
                            }
                        }
                        if (maxProductNumber != 0)
                        {
                            return new JsonResult(new { maxProductNumber = maxProductNumber });
                        }
                        else
                        {
                            return new JsonResult(new { maxProductNumber = maxProductNumber });
                        }
                    }
                    else
                    {
                        return new JsonResult(new { maxProductNumber = maxProductNumber });
                    }
                }
                else
                {
                    return new JsonResult(new { maxProductNumber = maxProductNumber });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return new JsonResult(new { maxProductNumber = 0 });
            }
        }

        // Setear numero IR
        [HttpGet]
        public async Task<JsonResult> GetLastProductNumberForSpareImported()
        {
            try
            {
                // Numero que pasa Anto segun la ultima venta de repuestos.
                int maxProductNumber = 0;
                var allOrders = await _OrderHeaderViewManager.FindBySpecification(new OrderHeaderWithAllDetailsSpecification());
                if (allOrders.Succeeded)
                {
                    if (allOrders.Data.Count > 0)
                    {
                        foreach (var o in allOrders.Data)
                        {
                            // Filtro por productos
                            var sparesOrders = o.OrderDetails.Where(x => x.ProductNumber.Substring(0, 2).ToString() == "13");
                            foreach (var d in sparesOrders)
                            {
                                // Itero cada una y valido que ProductNumber es el mayor.
                                if (maxProductNumber < Int32.Parse(d.ProductNumber.Substring(3, 5).ToString()))
                                {
                                    maxProductNumber = Int32.Parse(d.ProductNumber.Substring(3, 5).ToString());
                                }
                            }
                        }
                        if (maxProductNumber != 0)
                        {
                            return new JsonResult(new { maxProductNumber = maxProductNumber });
                        }
                        else
                        {
                            return new JsonResult(new { maxProductNumber = maxProductNumber });
                        }
                    }
                    else
                    {
                        return new JsonResult(new { maxProductNumber = maxProductNumber });
                    }
                }
                else
                {
                    return new JsonResult(new { maxProductNumber = maxProductNumber });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return new JsonResult(new { maxProductNumber = 0 });
            }
        }

        // Setear numero SCS
        [HttpGet]
        public async Task<JsonResult> GetLastProductNumberForStockComponents()
        {
            try
            {
                // Numero que pasa Anto segun el ultimo registro de STOCK.
                int maxProductNumber = 0;
                var allOrders = await _OrderHeaderViewManager.FindBySpecification(new OrderHeaderWithAllDetailsSpecification());
                if (allOrders.Succeeded)
                {
                    if (allOrders.Data.Count > 0)
                    {
                        foreach (var o in allOrders.Data)
                        {
                            // Filtro por productos
                            var stockOrders = o.OrderDetails.Where(x => x.ProductNumber.Substring(0, 2).ToString() == "04");
                            foreach (var d in stockOrders)
                            {
                                // Itero cada una y valido que ProductNumber es el mayor.
                                if (maxProductNumber < Int32.Parse(d.ProductNumber.Substring(3, 5).ToString()))
                                {
                                    maxProductNumber = Int32.Parse(d.ProductNumber.Substring(3, 5).ToString());
                                }
                            }
                        }
                        if (maxProductNumber != 0)
                        {
                            return new JsonResult(new { maxProductNumber = maxProductNumber });
                        }
                        else
                        {
                            return new JsonResult(new { maxProductNumber = maxProductNumber });
                        }
                    }
                    else
                    {
                        return new JsonResult(new { maxProductNumber = maxProductNumber });
                    }
                }
                else
                {
                    return new JsonResult(new { maxProductNumber = maxProductNumber });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return new JsonResult(new { maxProductNumber = 0 });
            }
        }

        public IActionResult _PDFReport()
        {
            return View();
        }

        public async Task<IActionResult> ExportToPdf(int orderId, bool isSale)
        {
            try
            {
                if (orderId != 0)
                {
                    ViewBag.isSale = isSale;
                    var response = await _OrderHeaderViewManager.FindBySpecification(new OrderHeaderWithAllSpecifications(orderId));
                    if (response.Succeeded)
                    {
                        var OrderHeadersViewModel = _mapper.Map<List<OrderHeaderViewModel>>(response.Data);

                        foreach (var oh in OrderHeadersViewModel)
                        {
                            if (!isSale)
                            {
                                return new ViewAsPdf("_PDFReportProductionOrder", oh)
                                {
                                    PageSize = Rotativa.AspNetCore.Options.Size.A4,
                                    FileName = "Orden_de_producción_000000" + oh.Number + ".pdf"
                                };
                            }
                            else
                            {
                                return new ViewAsPdf("_PDFReportSaleOrder", oh)
                                {
                                    PageSize = Rotativa.AspNetCore.Options.Size.A4,
                                    FileName = "Orden_de_venta_000000" + oh.Number + ".pdf"
                                };
                            }
                        }
                        return RedirectToAction("Index", new { sale = isSale });
                    }
                    else
                    {
                        return RedirectToAction("Index", new { sale = isSale });
                    }
                }
                else
                {
                    _notify.Warning(_localizer["You must select order to export."]);
                    return RedirectToAction("Index", new { sale = isSale });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return RedirectToAction("Index", new { sale = isSale });
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostDelete(int orderHeaderId, bool isSale)
        {
            // Primero tengo que ver si los OrderDetails asociados al orderHeaderId tienen algun WO, luego ver si
            // este WO tiene WOI, si es así elimino los WOI, luego el WO y por ultimo el OrderHeader.

            var orderDetailsByOrderHeaderId = await _OrderDetailViewManager.FindBySpecification(new OrderDetailByOrderHeaderIdSpecification(orderHeaderId));
            if (orderDetailsByOrderHeaderId.Succeeded)
            {
                foreach (var od in orderDetailsByOrderHeaderId.Data)
                {
                    var workOrderByOrderDetailId = await _workOrderViewManager.FindBySpecification(new WorkOrderByOrderDetailIdSpecification(od.Id));
                    if (workOrderByOrderDetailId.Succeeded)
                    {
                        if (workOrderByOrderDetailId.Data.Count > 0)
                        {
                            // Elimino los WOI del WO perteneciente al OD
                            _workOrderItemViewManager.DeleteByWorkOrderIdAsync(workOrderByOrderDetailId.Data.FirstOrDefault().Id);

                            await _workOrderViewManager.DeleteAsync(workOrderByOrderDetailId.Data.FirstOrDefault().Id);

                        }
                    }
                    else
                    {
                        _notify.Error(workOrderByOrderDetailId.Message);
                        return null;
                    }
                }
            }
            else
            {
                _notify.Error(orderDetailsByOrderHeaderId.Message);
                return null;
            }

            // Delete order complete, OrderHeader
            var deleteCommand = await _OrderHeaderViewManager.DeleteAsync(orderHeaderId);
            if (deleteCommand.Succeeded)
            {

                Result<IReadOnlyList<OrderDetailDTO>> response = null;
                if (isSale)
                {
                    // For sale orders
                    response = await _OrderDetailViewManager.FindBySpecification(new OrderDetailWithAllSpecifications(true));
                }
                else
                {
                    // For production orders
                    response = await _OrderDetailViewManager.FindBySpecification(new OrderDetailWithAllSpecifications(4, true));
                }

                if (response.Succeeded)
                {
                    _notify.Information(_localizer["Order deleted."]);

                    if (isSale)
                    {
                        return new JsonResult(new { isValid = true, isSale = true });
                    }
                    else
                    {
                        return new JsonResult(new { isValid = true, isSale = false });
                    }
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

        [HttpGet]
        public JsonResult ShowAlerts(string inputDetail)
        {
            if (inputDetail == "product_notselected")
            {
                _notify.Warning(_localizer["Please, you must select a product."]);
                return new JsonResult(new { isValid = false });
            }
            else if (inputDetail == "quantity_empty")
            {
                _notify.Warning(_localizer["Quantity input cannot be empty."]);
                return new JsonResult(new { isValid = false });
            }
            else if (inputDetail == "quantity_zero")
            {
                _notify.Warning(_localizer["The quantity cannot be zero."]);
                return new JsonResult(new { isValid = false });
            }
            else if (inputDetail == "not_row_selected")
            {
                _notify.Warning(_localizer["You must select order to export."]);
                return new JsonResult(new { isValid = false });
            }
            else if (inputDetail == "structure_notselected")
            {
                _notify.Warning(_localizer["You must select a configuration."]);
                return new JsonResult(new { isValid = false });
            }
            else if (inputDetail == "structures_empty")
            {
                _notify.Warning(_localizer["The selected product has no configuration."]);
                return new JsonResult(new { isValid = false });
            }

            return null;
        }

        [HttpPost]
        public async Task<JsonResult> UpdateOrderStateAsync(int orderStateId, int? orderId, int? orderDetailId, bool isSale, bool onlySpares, bool isImportedProduct, string strOrderDetailIds)
        {
            try
            {
                if (orderStateId == 0)
                {
                    _notify.Warning(_localizer["Please, you must select a valid state for update this sale order."]);
                    return new JsonResult(new { isValid = false });
                }
                else if (orderDetailId == 0)
                {
                    _notify.Warning(_localizer["Please, you must select an order."]);
                    return new JsonResult(new { isValid = false });
                }
                else
                {
                    if (orderDetailId != null)
                    {
                        if (isSale && !onlySpares && !isImportedProduct && orderStateId == 3)
                        {
                            _notify.Warning(_localizer["Sales order state cannot be updated because it is not an imported product."]);
                            return new JsonResult(new { isValid = false });
                        }
                        else
                        {
                            var orderDetailById = _OrderDetailViewManager.FindBySpecification(new OrderDetailEmptySpecification(orderDetailId.Value));
                            if (orderDetailById.Result.Succeeded)
                            {
                                if (orderDetailById.Result.Data.FirstOrDefault().OrderStateId == 3 && orderStateId >= 4 && orderStateId <= 5)
                                {
                                    return await UpadteOrderStates(orderDetailById.Result.Data.FirstOrDefault(), orderStateId);
                                }
                                else if (orderDetailById.Result.Data.FirstOrDefault().OrderStateId == 3 && orderStateId >= 1 && orderStateId <= 2)
                                {
                                    _notify.Warning(_localizer["The status of the selected order could not be updated. The selected order has already finish."]);
                                    return new JsonResult(new { isValid = false });
                                }
                                else if (orderDetailById.Result.Data.FirstOrDefault().OrderStateId == 3 && orderStateId == 3)
                                {
                                    _notify.Warning(_localizer["The status of the selected order could not be updated. The selected order has already finish."]);
                                    return new JsonResult(new { isValid = false });
                                }
                                else if (orderDetailById.Result.Data.FirstOrDefault().OrderStateId >= 3)
                                {
                                    _notify.Warning(_localizer["The status of the selected order could not be updated. The selected order has already finish."]);
                                    return new JsonResult(new { isValid = false });
                                }
                                else if ((orderDetailById.Result.Data.FirstOrDefault().OrderStateId == 1 || orderDetailById.Result.Data.FirstOrDefault().OrderStateId == 2) && (orderStateId == 4 || orderStateId == 5))
                                {
                                    _notify.Warning(_localizer["The status of the selected order could not be updated. First you have to finish the order."]);
                                    return new JsonResult(new { isValid = false });
                                }
                                else if (orderDetailById.Result.Data.FirstOrDefault().OrderStateId == 1 && orderStateId == 6)
                                {
                                    return await UpadteOrderStates(orderDetailById.Result.Data.FirstOrDefault(), orderStateId);
                                }
                                else if (orderStateId != 6)
                                {
                                    return await UpadteOrderStates(orderDetailById.Result.Data.FirstOrDefault(), orderStateId);
                                }
                                else
                                {
                                    _notify.Warning(_localizer["The status of the selected order could not be updated because it was not pending."]);
                                    return new JsonResult(new { isValid = false });
                                }
                            }
                            else
                            {
                                _notify.Error(_localizer["The status of the selected order could not be updated."]);
                                return new JsonResult(new { isValid = false });
                            }
                        }
                    }
                    else
                    {
                        var hasSucceeded = false;
                        if (strOrderDetailIds != null && strOrderDetailIds != "")
                        {
                            var orderDetailIds = strOrderDetailIds.Split(',');

                            foreach (var odId in orderDetailIds)
                            {
                                // Tomo el Convert.ToInt32(odId) y lo utilizo para setearle su OrderStateId = 5
                                var OrderDetailById = _OrderDetailViewManager.FindBySpecification(new OrderDetailEmptySpecification(Convert.ToInt32(odId)));
                                if (OrderDetailById.Result.Succeeded)
                                {
                                    hasSucceeded = true;
                                    var orderDetailByIdUpdate = await _OrderDetailViewManager.UpdateOrderStateAsync(OrderDetailById.Result.Data.FirstOrDefault(), orderStateId);
                                }
                            }
                        }
                        else
                        {
                            return new JsonResult(new { isValid = false });
                        }

                        if (hasSucceeded)
                        {
                            _notify.Success(_localizer["Order state updated."]);
                            return new JsonResult(new { isValid = true });
                        }
                        else
                        {
                            _notify.Error(_localizer["The status of the selected order could not be updated."]);
                            return new JsonResult(new { isValid = false });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _notify.Error(_localizer["The status of the selected order could not be updated."]);
                return new JsonResult(new { isValid = false });
            }
        }

        public async Task<JsonResult> UpadteOrderStates(OrderDetailDTO orderDetailDTO, int orderStateId)
        {
            var orderDetailByIdUpdate = await _OrderDetailViewManager.UpdateOrderStateAsync(orderDetailDTO, orderStateId);
            if (orderDetailByIdUpdate.Succeeded)
            {
                _notify.Success(_localizer["Order state updated."]);
                return new JsonResult(new { isValid = true });
            }
            else
            {
                _notify.Error(_localizer["The status of the selected order could not be updated."]);
                return new JsonResult(new { isValid = false });
            }
        }

        [HttpPost]
        public async Task<JsonResult> UpdateBilledStateAsync(int billedState, int orderId)
        {
            try
            {
                if (billedState == 0)
                {
                    _notify.Warning(_localizer["Please, you must select a valid state for update this sale order."]);
                    return new JsonResult(new { isValid = false });
                }
                else if (orderId == 0)
                {
                    _notify.Warning(_localizer["Please, you must select sale order."]);
                    return new JsonResult(new { isValid = false });
                }
                else
                {
                    var orderHeaderById = _OrderHeaderViewManager.FindBySpecification(new OrderHeaderByIdSpecification(orderId));
                    if (orderHeaderById.Result.Succeeded)
                    {
                        var orderHeaderByIdUpdate = await _OrderHeaderViewManager.UpdateBilledStateAsync(orderHeaderById.Result.Data.FirstOrDefault(), billedState);
                        if (orderHeaderByIdUpdate.Succeeded)
                        {
                            _notify.Success(_localizer["Order state updated."]);
                            return new JsonResult(new { isValid = true });
                        }
                        else
                        {
                            _notify.Error(_localizer["The status of the selected order could not be updated."]);
                            return new JsonResult(new { isValid = false });
                        }
                    }
                    else
                    {
                        _notify.Error(_localizer["The status of the selected order could not be updated."]);
                        return new JsonResult(new { isValid = false });
                    }
                }
            }
            catch (Exception ex)
            {
                _notify.Error(_localizer["The status of the selected order could not be updated."]);
                return new JsonResult(new { isValid = false });
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetOPDetailsById(int odId)
        {
            try
            {
                if (odId != -1)
                {
                    var getOrderDetailById = await _OrderDetailViewManager.FindBySpecification(new OrderDetailWithAllSpecifications(odId));
                    if (getOrderDetailById.Succeeded)
                    {
                        return new JsonResult(new { isValid = true, orderDetail = getOrderDetailById.Data.FirstOrDefault() });
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

        // To get last version items from structure
        public async Task<StructureViewModel> assignLastVersionItems(StructureViewModel structure)
        {
            var itemsresponse = await _structureItemViewManager.FindBySpecification(new StructureItemsByStructureIdWithDataSpecification(structure.Id, (int)structure.LastVersion.VersionNumber));
            if (itemsresponse.Succeeded)
            {
                structure.StructureItems = _mapper.Map<List<StructureItemViewModel>>(itemsresponse.Data);
            }
            return structure;
        }

        // To get childs structures of father structures
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
                    var response = await _structureViewManager.FindBySpecification(new StructureWithItemsSpecification(item.SonStructure.Id));
                    if (response.Succeeded)
                    {
                        var viewModel = _mapper.Map<StructureViewModel>(response.Data[0]);
                        await assignLastVersionItems(viewModel);
                        await getFullStructure(viewModel, newPrefix, fullstructure, item.Quantity);
                    }
                }
            }
            structure.StructureItems = fullstructure;

            return structure;
        }

        [HttpPost]
        public async Task<JsonResult> DiscountStockFromOrderDetail(int orderDetailId, bool isSale, bool onlySpares)
        {
            try
            {
                var getOrderDetailDTOById = await _OrderDetailViewManager.FindBySpecification(new OrderDetailByIdForStockSpecification(orderDetailId));
                if (getOrderDetailDTOById.Succeeded)
                {
                    var orderDetailVM = _mapper.Map<OrderDetailViewModel>(getOrderDetailDTOById.Data.FirstOrDefault());

                    if (orderDetailVM.OrderStateId >= 3)
                    {
                        _notify.Warning(_localizer["The status of the selected order could not be updated. The selected order has already finish."]);
                        return new JsonResult(new { isValid = false });
                    }
                    else
                    {
                        var getProductById = _productViewManager.FindBySpecification(new ProductByIdSpecification(orderDetailVM.Product.Id));
                        if (getProductById.Result.Succeeded)
                        {
                            var productVM = _mapper.Map<ProductViewModel>(getProductById.Result.Data.FirstOrDefault());
                            if (discountStock(isSale, onlySpares, productVM, orderDetailVM).Result)
                            {
                                return new JsonResult(new { isValid = true });
                            }
                            else
                            {
                                _notify.Warning("No se pudo descontar el stock.");
                                return new JsonResult(new { isValid = false });
                            }
                        }
                        else
                        {
                            _notify.Warning("No se pudo descontar el stock.");
                            return new JsonResult(new { isValid = false });
                        }
                    }
                }
                else
                {
                    _notify.Warning("No se pudo descontar el stock.");
                    return new JsonResult(new { isValid = false });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return new JsonResult(new { isValid = false });
            }
        }

        public async Task<bool> discountStock(bool isSale, bool onlySpares, ProductViewModel productVM, OrderDetailViewModel orderDetailVM = null)
        {
            try
            {
                productsToUpdateExistences = new List<ProductDTO>();

                if (isSale && !onlySpares)
                {
                    // Ventas
                    // Finalizan los importados
                    if (productVM.ProductFeature.Bought)
                    {
                        // Se resta a la existencia actual del producto, la cantidad de la OV
                        //subtractProductExistences(productVM, orderDetailVM.Quantity, null);
                        subtractProductExistences(productVM, Convert.ToInt32(orderDetailVM.Quantity), null);
                    }
                }
                else if (isSale && onlySpares)
                {
                    // Ventas de repuestos: Se muestran todos los que tienen StoredStock activo
                    // Si se mantiene en stock (siempre sucede esto porque en esta seccion se muestran todos los que tienen StoredStock = true):
                    // Se resta a la existencia actual del producto, la cantidad de la OV 
                    //subtractProductExistences(productVM, orderDetailVM.Quantity, null);
                    subtractProductExistences(productVM, Convert.ToInt32(orderDetailVM.Quantity), null);
                }
                else if (!isSale)
                {
                    // Produccion: Se muestran los que no son CT, Maquinas o Accesorios, Conjuntos y los que no tienen StoredStock activo
                    // Finalizan los que no son CT, Componentes para stock, Maquinas, Accesorios y Conjuntos
                    if (productVM.SubCategory.CategoryId != 4 || orderDetailVM.ProductNumber.StartsWith("04") || productVM.SubCategory.CategoryId == 1 || productVM.SubCategory.CategoryId == 2)
                    {
                        // Si es una Pieza individual
                        if (productVM.SubCategory.CategoryId == 3)
                        {
                            // Si se mantiene en stock: Se resta a la existencia actual del producto, la cantidad de la OV 
                            if (productVM.ProductFeature.StoredStock)
                            {
                                //subtractProductExistences(productVM, orderDetailVM.Quantity, null);
                                subtractProductExistences(productVM, Convert.ToInt32(orderDetailVM.Quantity), null);
                            }
                            // Si no se mantiene en stock: Ver stock de su materia prima, restar la existencia equivalente en unidades de la cantidad que figura en la OV
                            else
                            {
                                // Obtengo la materia prima (producto siempre CT) del producto productVM.ProductFeature.RawMaterialCode
                                var getProductByRawMaterialCode = await _productViewManager.FindBySpecification(new ProductByRawMaterialCodeSpecification(productVM.ProductFeature.RawMaterialCode.Trim()));
                                if (getProductByRawMaterialCode.Succeeded && getProductByRawMaterialCode.Data.Count() > 0)
                                {
                                    var rawMaterialProductVM = _mapper.Map<ProductViewModel>(getProductByRawMaterialCode.Data.FirstOrDefault());
                                    //subtractProductExistences(productVM, orderDetailVM.Quantity, rawMaterialProductVM);
                                    subtractProductExistences(productVM, Convert.ToInt32(orderDetailVM.Quantity), rawMaterialProductVM);
                                }
                            }
                        }
                        // Si es una Maquina o Accesorio o Conjunto
                        else if (productVM.SubCategory.CategoryId == 1 || productVM.SubCategory.CategoryId == 2)
                        {
                            // Si se mantiene en stock: Se resta a la existencia actual del producto, la cantidad de la OV 
                            if (productVM.ProductFeature.StoredStock)
                            {
                                //subtractProductExistences(productVM, orderDetailVM.Quantity, null);
                                subtractProductExistences(productVM, Convert.ToInt32(orderDetailVM.Quantity), null);
                            }
                            // Si no se mantiene en stock: Voy por cada uno de sus hijos y procedo segun: Componente comprado, Pieza individual o Conjunto
                            else
                            {
                                // Para Maquinas y Accesorios siempre entra por acá
                                // Llamada recursiva en otro método para esta seccion
                                await subtractSonProductExistences(productVM, orderDetailVM);
                            }
                        }
                    }
                }

                if (productsToUpdateExistences.Count() > 0)
                {
                    var updateExistencesResult = await _productViewManager.UpdateExistences(productsToUpdateExistences);
                    if (updateExistencesResult.Succeeded)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return false;
            }
        }

        public async Task<bool> subtractSonProductExistences(ProductViewModel productVM, OrderDetailViewModel orderDetailVM = null)
        {
            try
            {
                // productVM => Producto padre
                // orderDetailVM.Structure => Estructura del producto padre
                // orderDetailVM.Structure.StructureItems => Hijos de la estructura del padre completa

                orderDetailVM.Structure = await assignLastVersionItems(orderDetailVM.Structure);
                orderDetailVM.Structure = await getFullStructure(orderDetailVM.Structure, "", new List<StructureItemViewModel>());

                foreach (var stItemVM in orderDetailVM.Structure.StructureItems)
                {
                    // Solo itero por los hijos e hijos de hijos en toda la estructura
                    if (stItemVM.SonProductId.HasValue && !stItemVM.SonStructureId.HasValue)
                    {
                        // Si es un Componente comprado: Se resta a la existencia actual del producto, la cantidad de la StructureItem * orderDetailVM.Quantity (OV)
                        // (cantidad de elementos que insume construir la maquina) 
                        if (stItemVM.SonProduct.SubCategory.CategoryId == 4)
                        {
                            //subtractProductExistences(stItemVM.SonProduct, (stItemVM.Quantity * orderDetailVM.Quantity), null);
                            subtractProductExistences(stItemVM.SonProduct, (stItemVM.Quantity * Convert.ToInt32(orderDetailVM.Quantity)), null);
                        }
                        // Si es una Pieza individual
                        else if (stItemVM.SonProduct.SubCategory.CategoryId == 3)
                        {
                            // Si se mantiene en stock: Se resta a la existencia actual del producto, la cantidad de la StructureItem * orderDetailVM.Quantity (OV)
                            // (cantidad de elementos que insume construir la maquina) 
                            if (stItemVM.SonProduct.ProductFeature.StoredStock)
                            {
                                //subtractProductExistences(stItemVM.SonProduct, (stItemVM.Quantity * orderDetailVM.Quantity), null);
                                subtractProductExistences(stItemVM.SonProduct, (stItemVM.Quantity * Convert.ToInt32(orderDetailVM.Quantity)), null);
                            }
                            // Si no se mantiene en stock: Ver stock de su materia prima, restar la existencia equivalente en unidades de la cantidad que figura en la OV
                            else
                            {
                                // Obtengo la materia prima (producto siempre CT) del producto productVM.ProductFeature.RawMaterialCode
                                var getProductByRawMaterialCode = await _productViewManager.FindBySpecification(new ProductByRawMaterialCodeSpecification(stItemVM.SonProduct.ProductFeature.RawMaterialCode.Trim()));
                                if (getProductByRawMaterialCode.Succeeded && getProductByRawMaterialCode.Data.Count() > 0)
                                {
                                    var rawMaterialProductVM = _mapper.Map<ProductViewModel>(getProductByRawMaterialCode.Data.FirstOrDefault());
                                    //subtractProductExistences(stItemVM.SonProduct, (stItemVM.Quantity * orderDetailVM.Quantity), rawMaterialProductVM);
                                    subtractProductExistences(stItemVM.SonProduct, (stItemVM.Quantity * Convert.ToInt32(orderDetailVM.Quantity)), rawMaterialProductVM);
                                }
                            }
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return false;
            }
        }

        public bool subtractProductExistences(ProductViewModel productVM, int quantity, ProductViewModel rawMaterialProductVM = null)
        {
            try
            {
                if (rawMaterialProductVM != null)
                {
                    var rawMaterialProduct = subtractRawMaterial(productVM, quantity, rawMaterialProductVM);
                    if (rawMaterialProduct != null)
                    {
                        var productDTO = _mapper.Map<ProductDTO>(rawMaterialProduct);
                        AddProductToListOfProductExistences(productDTO);
                    }
                }
                else
                {
                    if (productVM.Existence.HasValue)
                    {
                        productVM.Existence = Convert.ToDecimal(productVM.Existence.Value) - Convert.ToInt32(quantity);

                        var productDTO = _mapper.Map<ProductDTO>(productVM);
                        AddProductToListOfProductExistences(productDTO);
                    }
                }

                return true;

            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return false;
            }
        }

        public ProductViewModel subtractRawMaterial(ProductViewModel productVM, int quantity, ProductViewModel rawMaterialProductVM)
        {
            try
            {
                ProductViewModel rawMaterialProduct = null;

                decimal rawMaterialExistences = 0;
                decimal lengthToSubtract = 0;
                decimal surfaceToSubtract = 0;

                decimal stockLength = 0;
                decimal stockWidth = 0;

                var notExist = false;

                // Largo (mm) * Ancho (mm)
                if (productVM.ProductFeature.ComponentLongPiece != "" && productVM.ProductFeature.ComponentWidhtPiece != "")
                {
                    surfaceToSubtract = Convert.ToDecimal(productVM.ProductFeature.ComponentLongPiece) * Convert.ToDecimal(productVM.ProductFeature.ComponentWidhtPiece);
                }
                // Largo (mm)
                else if (productVM.ProductFeature.ComponentLongPiece != "")
                {
                    lengthToSubtract = Convert.ToDecimal(productVM.ProductFeature.ComponentLongPiece);
                }

                // Si la unidad de las existencias es "Barras", "Bobinas" o "Rollos"
                if (rawMaterialProductVM.ExistenceUnitMeasureId == 22 || rawMaterialProductVM.ExistenceUnitMeasureId == 32 || rawMaterialProductVM.ExistenceUnitMeasureId == 23)
                {
                    if (rawMaterialProductVM.Existence.HasValue && rawMaterialProductVM.StockLength.HasValue)
                    {
                        switch (rawMaterialProductVM.StockLengthUnitMeasureId)
                        {
                            // Metros a Milimetros
                            case 2:
                                stockLength = rawMaterialProductVM.StockLength.Value * 1000;
                                break;
                            // Centimetros a Milimetros
                            case 25:
                                stockLength = rawMaterialProductVM.StockLength.Value * 10;
                                break;
                            default:
                                stockLength = rawMaterialProductVM.StockLength.Value;
                                break;
                        }

                        rawMaterialExistences = rawMaterialProductVM.Existence.Value * stockLength;

                        rawMaterialExistences -= lengthToSubtract * quantity;

                        if (productsToUpdateExistences.Count() > 0)
                        {
                            var sameProduct = (from pr in productsToUpdateExistences where pr.Id == rawMaterialProductVM.Id select pr);
                            if (sameProduct.FirstOrDefault() != null)
                            {
                                foreach (var pr in productsToUpdateExistences)
                                {
                                    if (pr.Id == rawMaterialProductVM.Id)
                                    {
                                        pr.Existence = ((pr.Existence * stockLength) - lengthToSubtract * quantity) / stockLength;
                                    }
                                }
                            }
                            else
                            {
                                notExist = true;
                            }

                            if (notExist)
                            {
                                rawMaterialProductVM.Existence = Convert.ToDecimal(rawMaterialExistences / stockLength);
                            }

                        }
                        else
                        {
                            rawMaterialProductVM.Existence = Convert.ToDecimal(rawMaterialExistences / stockLength);
                        }

                        rawMaterialProduct = rawMaterialProductVM;
                    }
                }
                // Si la unidad de las existencias es "Hojas"
                else if (rawMaterialProductVM.ExistenceUnitMeasureId == 31)
                {
                    if (rawMaterialProductVM.Existence.HasValue && rawMaterialProductVM.StockLength.HasValue && rawMaterialProductVM.StockWidth.HasValue)
                    {
                        switch (rawMaterialProductVM.StockLengthUnitMeasureId)
                        {
                            // Metros a Milimetros
                            case 2:
                                stockLength = rawMaterialProductVM.StockLength.Value * 1000;
                                break;
                            // Centimetros a Milimetros
                            case 25:
                                stockLength = rawMaterialProductVM.StockLength.Value * 10;
                                break;
                            default:
                                stockLength = rawMaterialProductVM.StockLength.Value;
                                break;
                        }
                        switch (rawMaterialProductVM.StockWidthUnitMeasureId)
                        {
                            // Metros a Milimetros
                            case 2:
                                stockWidth = rawMaterialProductVM.StockWidth.Value * 1000;
                                break;
                            // Centimetros a Milimetros
                            case 25:
                                stockWidth = rawMaterialProductVM.StockWidth.Value * 10;
                                break;
                            // Milimetros a Milimetros
                            default:
                                stockWidth = rawMaterialProductVM.StockWidth.Value;
                                break;
                        }

                        rawMaterialExistences = rawMaterialProductVM.Existence.Value * stockLength * stockWidth;

                        rawMaterialExistences -= surfaceToSubtract * quantity;

                        if (productsToUpdateExistences.Count() > 0)
                        {
                            var sameProduct = (from pr in productsToUpdateExistences where pr.Id == rawMaterialProductVM.Id select pr);
                            if (sameProduct.FirstOrDefault() != null)
                            {
                                foreach (var pr in productsToUpdateExistences)
                                {
                                    if (pr.Id == rawMaterialProductVM.Id)
                                    {
                                        pr.Existence = ((pr.Existence * stockLength * stockWidth) - surfaceToSubtract * quantity) / (stockLength * stockWidth);
                                    }
                                }
                            }
                            else
                            {
                                notExist = true;
                            }

                            if (notExist)
                            {
                                rawMaterialProductVM.Existence = Convert.ToDecimal(rawMaterialExistences / (stockLength * stockWidth));
                            }
                        }
                        else
                        {
                            rawMaterialProductVM.Existence = Convert.ToDecimal(rawMaterialExistences / (stockLength * stockWidth));
                        }

                        rawMaterialProduct = rawMaterialProductVM;
                    }
                }
                // Si la unidad de las existencias es "Unidades" 
                else if (rawMaterialProductVM.ExistenceUnitMeasureId == 19)
                {
                    if (rawMaterialProductVM.Existence.HasValue)
                    {
                        if (productsToUpdateExistences.Count() > 0)
                        {

                            var sameProduct = (from pr in productsToUpdateExistences where pr.Id == rawMaterialProductVM.Id select pr);
                            if (sameProduct.FirstOrDefault() != null)
                            {
                                foreach (var pr in productsToUpdateExistences)
                                {
                                    if (pr.Id == rawMaterialProductVM.Id)
                                    {
                                        pr.Existence -= quantity;
                                    }
                                }
                            }
                            else
                            {
                                notExist = true;
                            }

                            if (notExist)
                            {
                                rawMaterialProductVM.Existence -= quantity;
                            }
                        }
                        else
                        {
                            rawMaterialProductVM.Existence -= quantity;
                        }

                        rawMaterialProduct = rawMaterialProductVM;
                    }
                }

                return rawMaterialProduct;

            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return null;
            }
        }

        public void AddProductToListOfProductExistences(ProductDTO productDTO)
        {
            if (productsToUpdateExistences.Count() > 0)
            {
                if (!productsToUpdateExistences.Any(pr => pr.Id == productDTO.Id))
                {
                    productsToUpdateExistences.Add(productDTO);
                }
            }
            else
            {
                productsToUpdateExistences.Add(productDTO);
            }
        }

        [HttpPost]
        public async Task<JsonResult> CreateProductionOrderFromMissingProducts(List<int> missingProductsIds)
        {
            var existsPurchasedMissingProduct = false;
            var orderHeaderViewModel = new OrderHeaderViewModel();
            if (missingProductsIds != null)
            {
                if (missingProductsIds.Count > 0)
                {
                    List<MissingProductViewModel> missingProductViewModels = new List<MissingProductViewModel>();
                    foreach (var id in missingProductsIds)
                    {
                        var missingProductDTO = await _missingProductViewManager.FindWithAllSpecification(new MissingProductByIdSpecification(id));
                        if (missingProductDTO.Succeeded)
                        {
                            var missingProductsVM = _mapper.Map<List<MissingProductViewModel>>(missingProductDTO.Data);
                            foreach (var mp in missingProductsVM)
                            {
                                if (mp.Product.SubCategory.Category.Id == 4)
                                {
                                    _notify.Warning(_localizer["You cannot send products from the \"Purchased Components\" category to production."]);
                                    return new JsonResult(new { isValid = false, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", orderHeaderViewModel) });
                                }
                                else
                                {
                                    //if (mp.StateMissingProductId == 1)
                                    if (mp.StateMissingProductId < 4)
                                    {
                                        missingProductViewModels.Add(mp);
                                    }
                                    else
                                    {
                                        existsPurchasedMissingProduct = true;
                                    }
                                }
                            }
                        }
                    }

                    // For create production order
                    orderHeaderViewModel.OrderDate = DateTime.Now.Date.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute);
                    orderHeaderViewModel.isSale = false;
                    orderHeaderViewModel.onlySpares = true;

                    // Tengo que setear los detalles de acuerdo a cada producto faltante en missingProductViewModels
                    foreach (var mp in missingProductViewModels)
                    {
                        var orderDetailVM = new OrderDetailViewModel();

                        int maxProductNumberForStock = 0;
                        var allOrders = await _OrderHeaderViewManager.FindBySpecification(new OrderHeaderWithAllDetailsSpecification());
                        if (allOrders.Succeeded)
                        {
                            if (allOrders.Data.Count > 0)
                            {
                                foreach (var o in allOrders.Data)
                                {
                                    // Filtro por productos
                                    var stockOrders = o.OrderDetails.Where(x => x.ProductNumber.Substring(0, 2).ToString() == "04");
                                    foreach (var d in stockOrders)
                                    {
                                        // Itero cada una y valido que ProductNumber es el mayor.
                                        if (maxProductNumberForStock < Int32.Parse(d.ProductNumber.Substring(3, 5).ToString()))
                                        {
                                            maxProductNumberForStock = Int32.Parse(d.ProductNumber.Substring(3, 5).ToString());
                                        }
                                    }
                                }
                            }
                        }

                        maxProductNumberForStock += 1;
                        orderDetailVM.ProductNumber = "04-" + maxProductNumberForStock.ToString("D5");
                        orderDetailVM.Product = mp.Product;
                        orderDetailVM.ProductId = mp.ProductId;
                        orderDetailVM.MissingProductId = mp.Id;
                        orderDetailVM.StructureId = null;
                        orderDetailVM.SupplyVoltageId = null;
                        //orderDetailVM.OrderStateId = 5;
                        orderDetailVM.OrderStateId = 1;
                        orderDetailVM.SaleCategory = "Stock";
                        orderDetailVM.DeliveryDate = DateTime.Now.AddDays(20);

                        orderDetailVM.Quantity = Convert.ToInt32(mp.QuantityToOrder);

                        orderHeaderViewModel.OrderDetails.Add(orderDetailVM);
                    }

                    if (existsPurchasedMissingProduct)
                    {
                        _notify.Warning(_localizer["Attention, make sure to select products where their status is \"Pending\", \"To quote\" or \"Quoted\"."]);
                        return new JsonResult(new { isValid = false, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", orderHeaderViewModel) });
                    }
                    else
                    {
                        return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", orderHeaderViewModel) });
                    }
                }
                else
                {
                    var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", orderHeaderViewModel);
                    return new JsonResult(new { isValid = false, html = html });
                }
            }
            else
            {
                var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", orderHeaderViewModel);
                return new JsonResult(new { isValid = false, html = html });
            }
        }

    }
}