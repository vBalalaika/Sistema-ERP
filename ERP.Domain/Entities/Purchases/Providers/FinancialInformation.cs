using ERP.Domain.Common;
using System;

namespace ERP.Domain.Entities.Purchases.Providers
{
    public class FinancialInformation : IAuditableBaseEntity, IConcurrencyToken
    {
        public int Id { get; set; }

        public string Bank { get; set; }
        public string AccountType { get; set; }
        public string AccountNumber { get; set; }
        public string CBU { get; set; }
        public string Observations { get; set; }

        public int ProviderId { get; set; }
        public Provider Provider { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int ConcurrencyToken { get; set; }
    }
}
