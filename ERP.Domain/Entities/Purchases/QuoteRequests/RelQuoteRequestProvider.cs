using ERP.Domain.Common;
using ERP.Domain.Entities.Purchases.Providers;
using System;

namespace ERP.Domain.Entities.Purchases.QuoteRequests
{
    public class RelQuoteRequestProvider : IAuditableBaseEntity, IConcurrencyToken
    {
        public int Id { get; set; }

        public int QuoteRequestHeaderId { get; set; }
        public QuoteRequestHeader QuoteRequestHeader { get; set; }
        public int ProviderId { get; set; }
        public Provider Provider { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int ConcurrencyToken { get; set; }
    }
}
