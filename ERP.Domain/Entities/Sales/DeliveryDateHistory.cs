using ERP.Domain.Common;
using System;

namespace ERP.Domain.Entities.Sales
{
    public class DeliveryDateHistory : IAuditableBaseEntity, IConcurrencyToken
    {
        public int Id { get; set; }
        public int OrderDetailId { get; set; }
        public OrderDetail OrderDetail { get; set; }
        public DateTime OldDeliveryDate { get; set; }
        public DateTime NewDeliveryDate { get; set; }
        public DateTime ChangeDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int ConcurrencyToken { get; set; }
    }
}
