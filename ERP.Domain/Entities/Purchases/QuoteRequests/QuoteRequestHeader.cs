using ERP.Domain.Common;
using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities.Purchases.QuoteRequests
{
    public class QuoteRequestHeader : IAuditableBaseEntity, IConcurrencyToken
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int ConcurrencyToken { get; set; }

        public DateTime Date { get; set; }
        public string Comments { get; set; }
        public DateTime? ResponseDate { get; set; }

        public ICollection<QuoteRequestDetail> QuoteRequestDetails { get; set; } = new List<QuoteRequestDetail>();

        public ICollection<RelQuoteRequestProvider> RelQuoteRequestProviders { get; set; } = new List<RelQuoteRequestProvider>();

        public virtual List<QuoteRequestResponseHeader> QuoteRequestResponseHeaders { get; set; }
    }
}
