using ERP.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace ERP.Domain.Entities.Purchases.Providers
{
    public class Contact : IAuditableBaseEntity, IConcurrencyToken
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Charge { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }

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
