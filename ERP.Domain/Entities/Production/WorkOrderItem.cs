using ERP.Domain.Common;
using ERP.Domain.Entities.ProductMod;
using ERP.Domain.Entities.Sales;
using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities.Production
{
    public class WorkOrderItem : IAuditableBaseEntity, IConcurrencyToken
    {
        public int Id { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int ConcurrencyToken { get; set; }

        public int Quantity { get; set; }
        public string Level { get; set; }
        public int? ProductId { get; set; }
        public Product Product { get; set; }
        public int? StructureId { get; set; }
        public Structure Structure { get; set; }

        public int WorkOrderId { get; set; }
        public WorkOrder WorkOrder { get; set; }

        public int? WorkOrderItemOfId { get; set; }
        public WorkOrderItem? WorkOrderItemOf { get; set; }

        public ICollection<WorkOrderItem> ChildsWorkOrderItems { get; set; }

        public int OrderStateId { get; set; }
        public OrderState OrderState { get; set; }

        public ICollection<WorkActivity> WorkActivities { get; set; } = new List<WorkActivity>();


    }
}
