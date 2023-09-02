using ERP.Domain.Common;
using System;

namespace ERP.Domain.Entities.Logistics.Incomes
{
    public class IncomeState : IAuditableBaseEntity, IConcurrencyToken
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int ConcurrencyToken { get; set; }
    }
}
