using ERP.Domain.Entities.Purchases.QuoteRequests;

namespace ERP.Application.Specification.Purchases.QuoteRequests
{
    public class QuoteRequestResponseDetailByQRRHIdSpecification : BaseSpecification<QuoteRequestResponseDetail>
    {
        public QuoteRequestResponseDetailByQRRHIdSpecification(int qrrhId) : base(qrrd => qrrd.QuoteRequestResponseHeaderId == qrrhId)
        {
            AddInclude(qrrd => qrrd.QuoteRequestResponseHeader);
        }
    }
}
