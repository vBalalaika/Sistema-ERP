using ERP.Domain.Common;
using ERP.Domain.Entities.Sales;
using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities.Production
{
    public class WorkOrder : IAuditableBaseEntity, IConcurrencyToken
    {
        public int Id { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int ConcurrencyToken { get; set; }
        public string NumberVersionStructure { get; set; }
        public WorkOrderHeader WorkOrderHeader { get; set; }
        public int WorkOrderHeaderId { get; set; }

        public OrderDetail OrderDetail { get; set; }
        public int OrderDetailId { get; set; }

        public ICollection<WorkOrderItem> WorkOrderItems { get; set; } = new List<WorkOrderItem>();

        //Datos que pueden ser calculados, pero se evita logica
        public DateTime? StartDate { get; set; }
        public DateTime? EndingDate { get; set; }
        public DateTime? TotalTime { get; set; }

    }
}
