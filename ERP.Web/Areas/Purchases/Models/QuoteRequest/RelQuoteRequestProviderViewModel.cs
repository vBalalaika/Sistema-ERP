using ERP.Web.Areas.Purchases.Models.Providers;

namespace ERP.Web.Areas.Purchases.Models.QuoteRequest
{
    public class RelQuoteRequestProviderViewModel
    {
        public int Id { get; set; }
        public int ConcurrencyToken { get; set; }

        public int QuoteRequestHeaderId { get; set; }
        public QuoteRequestHeaderViewModel QuoteRequestHeader { get; set; }

        public int ProviderId { get; set; }
        public ProviderViewModel Provider { get; set; }
    }
}
