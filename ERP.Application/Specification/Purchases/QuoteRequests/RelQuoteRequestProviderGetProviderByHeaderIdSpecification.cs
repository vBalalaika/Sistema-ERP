using ERP.Domain.Entities.Purchases.QuoteRequests;

namespace ERP.Application.Specification.Purchases.QuoteRequests
{
    public class RelQuoteRequestProviderGetProviderByHeaderIdSpecification : BaseSpecification<RelQuoteRequestProvider>
    {
        public RelQuoteRequestProviderGetProviderByHeaderIdSpecification(int headerId) : base(x => x.QuoteRequestHeaderId == headerId)
        {
            AddInclude(x => x.Provider);
        }
    }
}
