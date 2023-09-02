using ERP.Domain.Entities.ProductMod;
using ERP.Domain.Entities.Purchases.MissingProducts;
using ERP.Domain.Entities.Purchases.Providers;
using ERP.Domain.Entities.Purchases.QuoteRequests;

namespace ERP.Application.Specification.Purchases.QuoteRequests
{
    public class QuoteRequestResponseDetailByProductIdSpecifications : BaseSpecification<QuoteRequestResponseDetail>
    {
        public QuoteRequestResponseDetailByProductIdSpecifications(int productId) : base(qrrd => qrrd.MissingProduct.ProductId == productId)
        {
            AddInclude(qrrd => qrrd.QuoteRequestResponseHeader);
            AddInclude(qrrd => qrrd.MissingProduct);
            AddInclude(qrrd => qrrd.PriceUnitMeasure);
            AddInclude(qrrd => qrrd.ProviderUnitMeasure);
            AddInclude($"{nameof(QuoteRequestResponseDetail.QuoteRequestResponseHeader)}.{nameof(QuoteRequestResponseHeader.QuoteRequestHeader)}");
            AddInclude($"{nameof(QuoteRequestResponseDetail.QuoteRequestResponseHeader)}.{nameof(QuoteRequestResponseHeader.Provider)}");

            AddInclude($"{nameof(QuoteRequestResponseDetail.MissingProduct)}.{nameof(MissingProduct.Product)}.{nameof(Product.UnitMeasure)}");
            AddInclude($"{nameof(QuoteRequestResponseDetail.MissingProduct)}.{nameof(MissingProduct.Product)}.{nameof(Product.RelProviderProducts)}");
            AddInclude($"{nameof(QuoteRequestResponseDetail.MissingProduct)}.{nameof(MissingProduct.Product)}.{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.Provider)}");
            AddInclude($"{nameof(QuoteRequestResponseDetail.MissingProduct)}.{nameof(MissingProduct.Product)}.{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.UnitMeasure)}");
            AddInclude($"{nameof(QuoteRequestResponseDetail.MissingProduct)}.{nameof(MissingProduct.Product)}.{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.WidthUnitMeasure)}");
            AddInclude($"{nameof(QuoteRequestResponseDetail.MissingProduct)}.{nameof(MissingProduct.Product)}.{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.LengthUnitMeasure)}");
            AddInclude($"{nameof(QuoteRequestResponseDetail.MissingProduct)}.{nameof(MissingProduct.Product)}.{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.WeightUnitMeasure)}");
            AddInclude($"{nameof(QuoteRequestResponseDetail.MissingProduct)}.{nameof(MissingProduct.Product)}.{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.PresentationUnitMeasure)}");
            AddInclude($"{nameof(QuoteRequestResponseDetail.MissingProduct)}.{nameof(MissingProduct.Product)}.{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.PriceUnitMeasure)}");
            AddInclude($"{nameof(QuoteRequestResponseDetail.MissingProduct)}.{nameof(MissingProduct.Product)}.{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.HeightUnitMeasure)}");
        }
    }
}
