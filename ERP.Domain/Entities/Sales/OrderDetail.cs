using ERP.Domain.Common;
using ERP.Domain.Entities.Production;
using ERP.Domain.Entities.ProductMod;
using System;

namespace ERP.Domain.Entities.Sales
{
    public class OrderDetail : IAuditableBaseEntity, IConcurrencyToken
    {
        public int Id { get; set; }

        // Numero de venta
        public string ProductNumber { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        // Configuración
        public int? StructureId { get; set; }

        public Structure Structure { get; set; }

        // Tensión
        public int? SupplyVoltageId { get; set; }

        public SupplyVoltage SupplyVoltage { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public int OrderStateId { get; set; }
        public OrderState OrderState { get; set; }

        //Teniendo en cuenta todos los puestos por los que tiene que pasar el producto para estar completo
        public decimal PercentageOfTotalAdvance { get; set; }

        //Avance detallado?

        public DateTime? DeliveredDate { get; set; }

        // Familia-rubro producto
        //public int? SubCategoryId { get; set; }
        //public SubCategory SubCategory { get; set; }

        // Categoria de venta
        public string SaleCategory { get; set; }

        //public int Quantity { get; set; }
        public decimal Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public int OrderHeaderId { get; set; }
        public OrderHeader OrderHeader { get; set; }

        //WorkOrder
        public int? WorkOrderId { get; set; }

        public WorkOrder WorkOrder { get; set; }

        //public bool BelongToASale { get; set; } = false;

        public bool BelongsToASaleOrder { get; set; } = false;
        public bool BelongsToAProductionOrder { get; set; } = false;

        public int? MissingProductId { get; set; }

        // For reorder rows on DataTable
        //public int? rowOrder { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int ConcurrencyToken { get; set; }
    }
}