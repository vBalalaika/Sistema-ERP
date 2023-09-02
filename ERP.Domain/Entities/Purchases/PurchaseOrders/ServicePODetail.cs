using ERP.Domain.Common;
using ERP.Domain.Entities.CommercialDocuments.DeliveryNote;
using ERP.Domain.Entities.Commons;
using ERP.Domain.Entities.ProductMod;
using System;

namespace ERP.Domain.Entities.Purchases.PurchaseOrders
{
    public class ServicePODetail : IAuditableBaseEntity, IConcurrencyToken
    {
        public int Id { get; set; }

        public int ServicePOHeaderId { get; set; }
        public ServicePOHeader ServicePOHeader { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public int? UnitMeasureId { get; set; }
        public UnitMeasure UnitMeasure { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Bonus { get; set; }
        public int? DeliveryNoteDetailId { get; set; }
        public DeliveryNoteDetail DeliveryNoteDetail { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int ConcurrencyToken { get; set; }
    }
}
