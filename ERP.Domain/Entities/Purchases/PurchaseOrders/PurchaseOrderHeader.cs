using ERP.Domain.Common;
using ERP.Domain.Entities.Purchases.Providers;
using ERP.Domain.Entities.Purchases.QuoteRequests;
using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities.Purchases.PurchaseOrders
{
    public class PurchaseOrderHeader : IAuditableBaseEntity, IConcurrencyToken
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Comments { get; set; }
        public int ProviderId { get; set; }
        public Provider Provider { get; set; }
        public decimal Bonus { get; set; }
        // 1 => 21%
        // 2 => 10,5%
        // 3 => 27%
        // 4 => 0%
        public int IVA { get; set; }
        public decimal Total { get; set; }
        public ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; } = new List<PurchaseOrderDetail>();
        public int? QuoteRequestResponseHeaderId { get; set; }
        public QuoteRequestResponseHeader QuoteRequestResponseHeader { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int ConcurrencyToken { get; set; }
    }
}
