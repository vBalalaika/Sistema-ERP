using ERP.Domain.Common;
using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities.Production
{
    public class WorkOrderHeader : IAuditableBaseEntity, IConcurrencyToken
    {
        public int Id { get; set; }
        public int WorkOrderHeaderNumber { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int ConcurrencyToken { get; set; }

        public string Observation { get; set; }
        public ICollection<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();
    }
}
