using ERP.Domain.Common;
using ERP.Domain.Entities.Purchases.Providers;
using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities.Purchases.QuoteRequests
{
    public class QuoteRequestResponseHeader : IAuditableBaseEntity, IConcurrencyToken
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public int Number { get; set; }

        public int QuoteRequestHeaderId { get; set; }
        public QuoteRequestHeader QuoteRequestHeader { get; set; }

        public int ProviderId { get; set; }
        public Provider Provider { get; set; }

        public decimal Bonus { get; set; }

        // 1 => 21%
        // 2 => 10,5%
        // 3 => 27%
        // 4 => 0%
        public int IVA { get; set; }
        public decimal Total { get; set; }

        public string Comments { get; set; }

        public ICollection<QuoteRequestResponseDetail> QuoteRequestResponseDetails { get; set; } = new List<QuoteRequestResponseDetail>();

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int ConcurrencyToken { get; set; }
    }
}
