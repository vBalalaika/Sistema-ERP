using ERP.Domain.Common;
using ERP.Domain.Entities.ProductMod;
using ERP.Domain.Entities.Purchases.Providers;
using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities.Production
{
    public class WorkActivity : IAuditableBaseEntity, IConcurrencyToken
    {
        public int Id { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int ConcurrencyToken { get; set; }


        public int WorkOrderItemId { get; set; }
        public WorkOrderItem WorkOrderItem { get; set; }

        public int ProductStationId { get; set; }
        public RelProductStation ProductStation { get; set; }
        public ICollection<WorkAction> WorkActions { get; set; } = new List<WorkAction>();
        // Assigned users's ids separados por ',' -.-
        public string AssignedUsersIds { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndingDate { get; set; }
        public DateTime? ComebackDate { get; set; }
        public string Observation { get; set; }
        public string NextStation { get; set; }
        public bool StateActivity { get; set; }
        public DateTime? DateToProduction { get; set; }

        // Fran: Envíos
        public bool? ToShipments { get; set; }

        // Fran: Relacion con remito
        //public int? DeliveryNoteDetailId { get; set; }
        //public DeliveryNoteDetail DeliveryNoteDetail { get; set; }

        public int? OutsourcedProviderId { get; set; }
        public Provider OutsourcedProvider { get; set; }

    }
}
