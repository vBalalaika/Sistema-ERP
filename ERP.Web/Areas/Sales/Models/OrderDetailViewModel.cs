using ERP.Web.Areas.Clients.Models;
using ERP.Web.Areas.Production.Models;
using ERP.Web.Areas.ProductMod.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace ERP.Web.Areas.Sales.Models
{
    public class OrderDetailViewModel
    {
        public int Id { get; set; }
        public int ConcurrencyToken { get; set; }
        public string CreatedBy { get; set; }

        // Numero de venta (Número identificatorio de venta, es un número de 5 digitos que ya están utilizando. 01-00000)
        // Antes IdentificationNumber
        public string ProductNumber { get; set; }

        public int ProductId { get; set; }
        public ProductViewModel Product { get; set; }
        public SelectList Products { get; set; }

        // Si el producto tiene configuraciones, mostrar cuales son para seleccionar una. (Buscar por ProductId en tabla Structures)
        public int? StructureId { get; set; }

        public StructureViewModel Structure { get; set; }
        public SelectList Structures { get; set; }

        // Si el producto elegido tiene tensiones, mostrar cuales son para seleccionar una. (Buscar por ProductId en tabla ProductSupplyVoltages)
        public int? SupplyVoltageId { get; set; }

        public SupplyVoltageViewModel SupplyVoltage { get; set; }
        public SelectList SupplyVoltages { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public int OrderStateId { get; set; }
        public OrderStateViewModel OrderState { get; set; }
        public string State { get; set; }

        //Teniendo en cuenta todos los puestos por los que tiene que pasar el producto para estar completo
        public decimal PercentageOfTotalAdvance { get; set; }

        //Avance detallado?

        public DateTime? DeliveredDate { get; set; }

        // Familia-rubro producto
        //public int? SubCategoryId { get; set; }
        //public SubCategoryViewModel SubCategory { get; set; }

        // Categoria de venta
        // Venta:
        //      - Si la categoria del producto seleccionado es "Máquinas y accesorios" y su sub-categoria NO ES "Accesorios" -> Venta = "Máquinas"
        //      - Si la categoria del producto seleccionado es "Máquinas y accesorios" y su sub-categoria ES "Accesorios" -> Venta = "Accesorios"
        //      - Si la categoria del producto seleccionado es "Conjuntos" o "Piezas individuales" o "Componentes comprados" -> Venta = "Repuestos"
        // Simplemak - Máquinas 01-
        // Importadas - Máquinas 11-
        // Simplemak - Accesorios 02-
        // Importadas - Accesorios 12-
        // Simplemak - Repuestos 03-
        // Importadas - Repuestos 13-
        // Antes Identification
        public string StartDate { get; set; }

        public string SaleCategory { get; set; }

        //public int Quantity { get; set; }
        public decimal Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public int OrderHeaderId { get; set; }
        public OrderHeaderViewModel OrderHeader { get; set; }

        public int? WorkOrderId { get; set; }
        public WorkOrderViewModel WorkOrder { get; set; }

        //public bool BelongToASale { get; set; } = false;

        public bool BelongsToASaleOrder { get; set; } = false;
        public bool BelongsToAProductionOrder { get; set; } = false;
        public int OrderDetailIdBelongsToASaleOrder { get; set; } = 0;

        // For reorder rows on DataTable
        //public int? rowOrder { get; set; }

        public int SaleOperationId { get; set; }
        public SaleOperationViewModel SaleOperation { get; set; }

        public int? MissingProductId { get; set; }
    }
}