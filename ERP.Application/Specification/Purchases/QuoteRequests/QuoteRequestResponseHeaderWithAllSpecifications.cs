using ERP.Domain.Entities.Purchases.MissingProducts;
using ERP.Domain.Entities.Purchases.QuoteRequests;

namespace ERP.Application.Specification.Purchases.QuoteRequests
{
    public class QuoteRequestResponseHeaderWithAllSpecifications : BaseSpecification<QuoteRequestResponseHeader>
    {
        public QuoteRequestResponseHeaderWithAllSpecifications(int id) : base(qrrh => qrrh.Id == id)
        {
            AddInclude(qrrh => qrrh.QuoteRequestHeader);
            AddInclude(qrrh => qrrh.Provider);
            AddInclude(qrrh => qrrh.QuoteRequestResponseDetails);
            AddInclude($"{nameof(QuoteRequestResponseHeader.QuoteRequestResponseDetails)}.{nameof(QuoteRequestResponseDetail.MissingProduct)}.{nameof(MissingProduct.Product)}");
            AddInclude($"{nameof(QuoteRequestResponseHeader.QuoteRequestResponseDetails)}.{nameof(QuoteRequestResponseDetail.AlternativeProduct)}");
            AddInclude($"{nameof(QuoteRequestResponseHeader.QuoteRequestResponseDetails)}.{nameof(QuoteRequestResponseDetail.PriceUnitMeasure)}");
            AddInclude($"{nameof(QuoteRequestResponseHeader.QuoteRequestResponseDetails)}.{nameof(QuoteRequestResponseDetail.ProviderUnitMeasure)}");
        }
    }
}
