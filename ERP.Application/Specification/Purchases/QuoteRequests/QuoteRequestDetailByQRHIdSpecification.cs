using ERP.Domain.Entities.Purchases.MissingProducts;
using ERP.Domain.Entities.Purchases.QuoteRequests;

namespace ERP.Application.Specification.Purchases.QuoteRequests
{
    public class QuoteRequestDetailByQRHIdSpecification : BaseSpecification<QuoteRequestDetail>
    {
        public QuoteRequestDetailByQRHIdSpecification(int qrhId) : base(qrd => qrd.QuoteRequestHeaderId == qrhId)
        {
            AddInclude(qrd => qrd.QuoteRequestHeader);
            AddInclude(qrd => qrd.MissingProduct);
            AddInclude($"{nameof(QuoteRequestDetail.MissingProduct)}.{nameof(MissingProduct.Product)}");
        }
    }
}
