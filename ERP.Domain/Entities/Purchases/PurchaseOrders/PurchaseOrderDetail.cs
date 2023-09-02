using ERP.Domain.Common;
using ERP.Domain.Entities.Commons;
using ERP.Domain.Entities.Logistics.Incomes;
using ERP.Domain.Entities.ProductMod;
using ERP.Domain.Entities.Purchases.MissingProducts;
using System;

namespace ERP.Domain.Entities.Purchases.PurchaseOrders
{
    public class PurchaseOrderDetail : IAuditableBaseEntity, IConcurrencyToken
    {
        public int Id { get; set; }
        public int PurchaseOrderHeaderId { get; set; }
        public PurchaseOrderHeader PurchaseOrderHeader { get; set; }
        public int? MissingProductId { get; set; }
        public MissingProduct MissingProduct { get; set; }
        public int? ProductId { get; set; }
        public Product Product { get; set; }
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
        public decimal PurchasedQuantity { get; set; }
        public int PurchaseStateId { get; set; }
        public PurchaseState PurchaseState { get; set; }
        public int? IncomeDetailId { get; set; }
        public IncomeDetail IncomeDetail { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int ConcurrencyToken { get; set; }
    }
}
