using ERP.Domain.Common;
using System;

namespace ERP.Domain.Entities.Production
{
    public class WorkAction : IAuditableBaseEntity, IConcurrencyToken
    {
        public int Id { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int ConcurrencyToken { get; set; }

        public int WorkActivityId { get; set; }
        public WorkActivity WorkActivity { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndingDate { get; set; }
        public string ActionName { get; set; }


    }
}
