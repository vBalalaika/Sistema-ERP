using ERP.Domain.Entities.Purchases.QuoteRequests;

namespace ERP.Application.Specification.Purchases.QuoteRequests
{
    public class QuoteRequestDetailWithAllSpecifications : BaseSpecification<QuoteRequestDetail>
    {
        public QuoteRequestDetailWithAllSpecifications()
        {
            AddInclude(qrd => qrd.QuoteRequestHeader);
            AddInclude(qrd => qrd.Product);
            AddInclude(qrd => qrd.QuantityToOrderUnitMeasure);
            AddInclude(qrd => qrd.ProviderUnitMeasure);
            AddInclude(qrd => qrd.PurchaseState);
            AddInclude($"{nameof(QuoteRequestDetail.QuoteRequestHeader)}.{nameof(QuoteRequestHeader.RelQuoteRequestProviders)}.{nameof(RelQuoteRequestProvider.Provider)}");
            AddInclude($"{nameof(QuoteRequestDetail.QuoteRequestHeader)}.{nameof(QuoteRequestHeader.QuoteRequestResponseHeaders)}");
        }

        public QuoteRequestDetailWithAllSpecifications(int id) : base(qrd => qrd.Id == id)
        {
            AddInclude(qrd => qrd.QuoteRequestHeader);
            AddInclude(qrd => qrd.Product);
            AddInclude(qrd => qrd.QuantityToOrderUnitMeasure);
            AddInclude(qrd => qrd.ProviderUnitMeasure);
            AddInclude(qrd => qrd.PurchaseState);
        }
    }
}
