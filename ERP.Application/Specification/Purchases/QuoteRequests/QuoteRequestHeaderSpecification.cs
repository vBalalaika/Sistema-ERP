using ERP.Domain.Entities.Purchases.QuoteRequests;

namespace ERP.Application.Specification.Purchases.QuoteRequests
{
    public class QuoteRequestHeaderSpecification : BaseSpecification<QuoteRequestHeader>
    {
        public QuoteRequestHeaderSpecification()
        {

        }

        public QuoteRequestHeaderSpecification(int id) : base(qrh => qrh.Id == id)
        {
            AddInclude(x => x.QuoteRequestDetails);
            AddInclude(x => x.RelQuoteRequestProviders);
        }
    }
}
