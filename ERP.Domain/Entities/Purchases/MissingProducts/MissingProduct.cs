using ERP.Domain.Common;
using ERP.Domain.Entities.Commons;
using ERP.Domain.Entities.ProductMod;
using ERP.Domain.Entities.Purchases.Providers;
using System;

namespace ERP.Domain.Entities.Purchases.MissingProducts
{
    public class MissingProduct : IAuditableBaseEntity, IConcurrencyToken
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public int? ProviderId { get; set; }
        public Provider Provider { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int? StateMissingProductId { get; set; }
        public PurchaseState StateMissingProduct { get; set; }

        public decimal QuantityToOrder { get; set; }
        public int? QuantityToOrderUnitMeasureId { get; set; }
        public UnitMeasure QuantityToOrderUnitMeasure { get; set; }

        public decimal Quantity { get; set; }
        public int? StorageUnitMeasureId { get; set; }
        public UnitMeasure StorageUnitMeasure { get; set; }

        public string Comments { get; set; }
        public string UserName { get; set; }

        public decimal PurchasedQuantity { get; set; }

        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int ConcurrencyToken { get; set; }

    }
}
