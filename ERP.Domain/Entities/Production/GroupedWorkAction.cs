using ERP.Domain.Common;
using ERP.Domain.Entities.ProductMod;
using System;

namespace ERP.Domain.Entities.Production
{
    public class GroupedWorkAction : IAuditableBaseEntity, IConcurrencyToken
    {
        public int Id { get; set; }
        public string WorkActivitiesIds { get; set; }
        public int StationId { get; set; }
        public Station Station { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int ConcurrencyToken { get; set; }
    }
}
