using ERP.Domain.Common;
using ERP.Domain.Entities.Commons;
using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities.Lists
{
    public class State : IAuditableBaseEntity, IConcurrencyToken
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public ICollection<City> Cities { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int ConcurrencyToken { get; set; }
    }
}
