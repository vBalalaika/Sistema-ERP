using ERP.Domain.Common;
using ERP.Domain.Entities.ProductMod;
using ERP.Domain.Entities.Purchases.MissingProducts;
using ERP.Domain.Entities.Purchases.Providers;
using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities.Purchases.PurchaseOrders
{
    public class ServicePOHeader : IAuditableBaseEntity, IConcurrencyToken
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public int Number { get; set; }
        public int ProviderId { get; set; }
        public Provider Provider { get; set; }
        public string Comments { get; set; }
        public decimal Bonus { get; set; }

        // 1 => 21%
        // 2 => 10,5%
        // 3 => 27%
        public int IVA { get; set; }
        public decimal Total { get; set; }

        public int? StationId { get; set; }
        public Station Station { get; set; }

        public int PurchaseStateId { get; set; }
        public PurchaseState PurchaseState { get; set; }

        public ICollection<ServicePODetail> ServicePODetails { get; set; } = new List<ServicePODetail>();

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int ConcurrencyToken { get; set; }
    }
}
