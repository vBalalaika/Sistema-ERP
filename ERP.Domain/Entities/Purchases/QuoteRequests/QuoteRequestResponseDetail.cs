using ERP.Domain.Common;
using ERP.Domain.Entities.Commons;
using ERP.Domain.Entities.ProductMod;
using ERP.Domain.Entities.Purchases.MissingProducts;
using System;

namespace ERP.Domain.Entities.Purchases.QuoteRequests
{
    public class QuoteRequestResponseDetail : IAuditableBaseEntity, IConcurrencyToken
    {
        public int Id { get; set; }

        public int? MissingProductId { get; set; }
        public MissingProduct MissingProduct { get; set; }
        public bool Available { get; set; }
        public int? AlternativeProductId { get; set; }
        public Product AlternativeProduct { get; set; }
        public decimal OriginalProviderQuantity { get; set; }
        public decimal ProviderQuantity { get; set; }
        public int ProviderUnitMeasureId { get; set; }
        public UnitMeasure ProviderUnitMeasure { get; set; }
        public decimal OriginalPriceQuantity { get; set; }
        public decimal PriceQuantity { get; set; }
        public int PriceUnitMeasureId { get; set; }
        public UnitMeasure PriceUnitMeasure { get; set; }
        public decimal UnitPrice { get; set; }
        public int MoneyType { get; set; }
        public decimal Bonus { get; set; }
        public decimal Total { get; set; }
        public int QuoteRequestResponseHeaderId { get; set; }
        public QuoteRequestResponseHeader QuoteRequestResponseHeader { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int ConcurrencyToken { get; set; }

    }
}
