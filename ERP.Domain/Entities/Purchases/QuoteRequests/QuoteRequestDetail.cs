using ERP.Domain.Common;
using ERP.Domain.Entities.Commons;
using ERP.Domain.Entities.ProductMod;
using ERP.Domain.Entities.Purchases.MissingProducts;
using System;

namespace ERP.Domain.Entities.Purchases.QuoteRequests
{
    public class QuoteRequestDetail : IAuditableBaseEntity, IConcurrencyToken
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int ConcurrencyToken { get; set; }

        public int QuoteRequestHeaderId { get; set; }
        public QuoteRequestHeader QuoteRequestHeader { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public decimal QuantityToOrder { get; set; }
        public int? QuantityToOrderUnitMeasureId { get; set; }
        public UnitMeasure QuantityToOrderUnitMeasure { get; set; }
        public decimal Quantity { get; set; }
        public int? ProviderUnitMeasureId { get; set; }
        public UnitMeasure ProviderUnitMeasure { get; set; }

        public int? MissingProductId { get; set; }
        public MissingProduct MissingProduct { get; set; }

        public int PurchaseStateId { get; set; }
        public PurchaseState PurchaseState { get; set; }
    }
}
