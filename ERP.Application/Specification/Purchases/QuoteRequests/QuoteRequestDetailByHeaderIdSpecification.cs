using ERP.Domain.Entities.Purchases.QuoteRequests;

namespace ERP.Application.Specification.Purchases.QuoteRequests
{
    public class QuoteRequestDetailByHeaderIdSpecification : BaseSpecification<QuoteRequestDetail>
    {
        public QuoteRequestDetailByHeaderIdSpecification(int headerId) : base(x => x.QuoteRequestHeaderId == headerId)
        {
            AddInclude(x => x.Product);
            AddInclude(x => x.ProviderUnitMeasure);
        }
    }
}
