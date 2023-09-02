using ERP.Domain.Entities.Purchases.MissingProducts;
using ERP.Domain.Entities.Purchases.QuoteRequests;

namespace ERP.Application.Specification.Purchases.QuoteRequests
{
    public class QuoteRequestResponseDetailWithAllSpecifications : BaseSpecification<QuoteRequestResponseDetail>
    {
        public QuoteRequestResponseDetailWithAllSpecifications()
        {
            AddInclude(qrrd => qrrd.QuoteRequestResponseHeader);
            AddInclude(qrrd => qrrd.MissingProduct);
            AddInclude(qrrd => qrrd.AlternativeProduct);
            AddInclude(qrrd => qrrd.PriceUnitMeasure);
            AddInclude(qrrd => qrrd.ProviderUnitMeasure);
            AddInclude($"{nameof(QuoteRequestResponseDetail.QuoteRequestResponseHeader)}.{nameof(QuoteRequestResponseHeader.QuoteRequestHeader)}");
            AddInclude($"{nameof(QuoteRequestResponseDetail.QuoteRequestResponseHeader)}.{nameof(QuoteRequestResponseHeader.Provider)}");
            AddInclude($"{nameof(QuoteRequestResponseDetail.MissingProduct)}.{nameof(MissingProduct.Product)}");
        }

        public QuoteRequestResponseDetailWithAllSpecifications(int id) : base(qrrd => qrrd.Id == id)
        {
            AddInclude(qrrd => qrrd.QuoteRequestResponseHeader);
            AddInclude(qrrd => qrrd.MissingProduct);
            AddInclude(qrrd => qrrd.AlternativeProduct);
            AddInclude(qrrd => qrrd.PriceUnitMeasure);
            AddInclude(qrrd => qrrd.ProviderUnitMeasure);
            AddInclude($"{nameof(QuoteRequestResponseDetail.QuoteRequestResponseHeader)}.{nameof(QuoteRequestResponseHeader.QuoteRequestHeader)}.{nameof(QuoteRequestHeader.QuoteRequestDetails)}");
            AddInclude($"{nameof(QuoteRequestResponseDetail.QuoteRequestResponseHeader)}.{nameof(QuoteRequestResponseHeader.Provider)}");
            AddInclude($"{nameof(QuoteRequestResponseDetail.MissingProduct)}.{nameof(MissingProduct.Product)}");
        }
    }
}
