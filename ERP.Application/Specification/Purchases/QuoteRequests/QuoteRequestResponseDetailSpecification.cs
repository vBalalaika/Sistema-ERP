using ERP.Domain.Entities.ProductMod;
using ERP.Domain.Entities.Purchases.MissingProducts;
using ERP.Domain.Entities.Purchases.QuoteRequests;

namespace ERP.Application.Specification.Purchases.QuoteRequests
{
    public class QuoteRequestResponseDetailSpecification : BaseSpecification<QuoteRequestResponseDetail>
    {
        public QuoteRequestResponseDetailSpecification()
        {
            AddInclude(qrrd => qrrd.QuoteRequestResponseHeader);
            AddInclude(qrrd => qrrd.MissingProduct);
            AddInclude(qrrd => qrrd.ProviderUnitMeasure);
            AddInclude(qrrd => qrrd.PriceUnitMeasure);

            AddInclude($"{nameof(QuoteRequestResponseDetail.QuoteRequestResponseHeader)}.{nameof(QuoteRequestResponseHeader.Provider)}");
            AddInclude($"{nameof(QuoteRequestResponseDetail.MissingProduct)}.{nameof(MissingProduct.Product)}.{nameof(Product.SubCategory)}");
            AddInclude($"{nameof(QuoteRequestResponseDetail.MissingProduct)}.{nameof(MissingProduct.Product)}.{nameof(Product.UnitMeasure)}");
        }
    }
}
