using ERP.Domain.Common;
using ERP.Domain.Entities.CommercialDocuments.DeliveryNote;
using ERP.Domain.Entities.Commons;
using ERP.Domain.Entities.ProductMod;
using ERP.Domain.Entities.Purchases.MissingProducts;
using ERP.Domain.Entities.Purchases.PurchaseOrders;
using System;

namespace ERP.Domain.Entities.Logistics.Incomes
{
    public class IncomeDetail : IAuditableBaseEntity, IConcurrencyToken
    {
        public int IncomeProductId { get; set; }
        public Product IncomeProduct { get; set; }
        public decimal Quantity { get; set; }
        public int UnitId { get; set; }
        public UnitMeasure Unit { get; set; }
        public string BatchNumber { get; set; }
        public int? OTNumber { get; set; } = null;
        public string ProductNumber { get; set; }
        public int? FatherProductId { get; set; }
        public Product FatherProduct { get; set; }
        public int? FatherStructureId { get; set; }
        public Structure FatherStructure { get; set; }
        public int? OCNumber { get; set; } = null;
        public decimal Weight { get; set; } = 0;
        public int IncomeStateId { get; set; }
        public IncomeState IncomeState { get; set; }
        public int IncomeHeaderId { get; set; }
        public IncomeHeader IncomeHeader { get; set; }
        public DateTime? IncomeDate { get; set; }
        public string Reception { get; set; }
        public string NextStation { get; set; }
        public PurchaseOrderDetail PurchaseOrderDetail { get; set; }
        public int? DeliveryNoteHeaderId { get; set; }
        public DeliveryNoteHeader DeliveryNoteHeader { get; set; }
        public int? MissingProductId { get; set; }
        public MissingProduct MissingProduct { get; set; }

        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int ConcurrencyToken { get; set; }
    }
}
