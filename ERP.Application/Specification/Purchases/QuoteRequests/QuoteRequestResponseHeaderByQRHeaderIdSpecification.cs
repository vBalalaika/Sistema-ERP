using ERP.Domain.Entities.Purchases.MissingProducts;
using ERP.Domain.Entities.Purchases.QuoteRequests;

namespace ERP.Application.Specification.Purchases.QuoteRequests
{
    public class QuoteRequestResponseHeaderByQRHeaderIdSpecification : BaseSpecification<QuoteRequestResponseHeader>
    {
        public QuoteRequestResponseHeaderByQRHeaderIdSpecification(int qrhId) : base(qrrh => qrrh.QuoteRequestHeaderId == qrhId)
        {
            AddInclude(qrrh => qrrh.Provider);
            AddInclude(qrrh => qrrh.QuoteRequestResponseDetails);
            AddInclude($"{nameof(QuoteRequestResponseHeader.QuoteRequestResponseDetails)}.{nameof(QuoteRequestResponseDetail.MissingProduct)}.{nameof(MissingProduct.Product)}");
            AddInclude($"{nameof(QuoteRequestResponseHeader.QuoteRequestResponseDetails)}.{nameof(QuoteRequestResponseDetail.AlternativeProduct)}");
            AddInclude($"{nameof(QuoteRequestResponseHeader.QuoteRequestResponseDetails)}.{nameof(QuoteRequestResponseDetail.ProviderUnitMeasure)}");
            AddInclude($"{nameof(QuoteRequestResponseHeader.QuoteRequestResponseDetails)}.{nameof(QuoteRequestResponseDetail.PriceUnitMeasure)}");
        }
    }
}
