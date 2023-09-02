using ERP.Domain.Common;
using ERP.Domain.Entities.Production;
using ERP.Domain.Entities.ProductMod;
using System;

namespace ERP.Domain.Entities.CommercialDocuments.DeliveryNote
{
    public class DeliveryNoteDetail : IAuditableBaseEntity, IConcurrencyToken
    {
        public int Id { get; set; }
        public int ProductItemId { get; set; }
        public Product ProductItem { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int? ConfigurationId { get; set; }
        public Structure Configuration { get; set; }
        public int Package { get; set; }
        public decimal PackageWeight { get; set; }
        public int Quantity { get; set; }

        //public ICollection<WorkActivity> WorkActivities { get; set; }
        public int? WorkActivityId { get; set; }
        public WorkActivity WorkActivity { get; set; }

        public int DeliveryNoteHeaderId { get; set; }
        public DeliveryNoteHeader DeliveryNoteHeader { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int ConcurrencyToken { get; set; }
    }
}
