using ERP.Domain.Common;
using System;

namespace ERP.Domain.Entities.Lists
{
    public class City : IAuditableBaseEntity, IConcurrencyToken
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ZipCode { get; set; }
        public int StateId { get; set; }
        public State State { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int ConcurrencyToken { get; set; }
    }
}
