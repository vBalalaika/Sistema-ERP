using ERP.Domain.Common;
using ERP.Domain.Entities.Commons;
using ERP.Domain.Entities.Lists;
using System;

namespace ERP.Domain.Entities.Purchases.Providers
{
    public class Subsidiary : IAuditableBaseEntity, IConcurrencyToken
    {
        public int Id { get; set; }

        public string Address { get; set; }
        public int? CountryId { get; set; }
        public Country Country { get; set; }
        //public ICollection<Country> Countries { get; set; }
        public int? StateId { get; set; }
        public State State { get; set; }
        //public ICollection<State> States { get; set; }
        public int? CityId { get; set; }
        public City City { get; set; }
        //public ICollection<City> Cities { get; set; }
        public string PostalCode { get; set; }
        public string SubsidiaryNumber { get; set; }

        public int ProviderId { get; set; }
        public Provider Provider { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int ConcurrencyToken { get; set; }
    }
}
