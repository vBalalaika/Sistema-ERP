using ERP.Domain.Entities.ProductMod;
using ERP.Domain.Entities.Purchases.Providers;
using ERP.Domain.Entities.Purchases.QuoteRequests;

namespace ERP.Application.Specification.Purchases.QuoteRequests
{
    public class QuoteRequestHeaderWithAllSpecifications : BaseSpecification<QuoteRequestHeader>
    {
        public QuoteRequestHeaderWithAllSpecifications()
        {
            AddInclude(x => x.QuoteRequestDetails);
            AddInclude($"{nameof(QuoteRequestHeader.QuoteRequestDetails)}.{nameof(QuoteRequestDetail.Product)}");
            AddInclude($"{nameof(QuoteRequestHeader.QuoteRequestDetails)}.{nameof(QuoteRequestDetail.QuantityToOrderUnitMeasure)}");
            AddInclude($"{nameof(QuoteRequestHeader.QuoteRequestDetails)}.{nameof(QuoteRequestDetail.ProviderUnitMeasure)}");

            AddInclude($"{nameof(QuoteRequestHeader.RelQuoteRequestProviders)}.{nameof(RelQuoteRequestProvider.Provider)}");

            AddInclude($"{nameof(QuoteRequestHeader.QuoteRequestDetails)}.{nameof(QuoteRequestDetail.Product)}.{nameof(Product.RelProviderProducts)}");
            AddInclude($"{nameof(QuoteRequestHeader.QuoteRequestDetails)}.{nameof(QuoteRequestDetail.Product)}.{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.Provider)}");
            AddInclude($"{nameof(QuoteRequestHeader.QuoteRequestDetails)}.{nameof(QuoteRequestDetail.Product)}.{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.UnitMeasure)}");
            AddInclude($"{nameof(QuoteRequestHeader.QuoteRequestDetails)}.{nameof(QuoteRequestDetail.Product)}.{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.WidthUnitMeasure)}");
            AddInclude($"{nameof(QuoteRequestHeader.QuoteRequestDetails)}.{nameof(QuoteRequestDetail.Product)}.{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.LengthUnitMeasure)}");
            AddInclude($"{nameof(QuoteRequestHeader.QuoteRequestDetails)}.{nameof(QuoteRequestDetail.Product)}.{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.WeightUnitMeasure)}");
            AddInclude($"{nameof(QuoteRequestHeader.QuoteRequestDetails)}.{nameof(QuoteRequestDetail.Product)}.{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.PresentationUnitMeasure)}");
        }

        public QuoteRequestHeaderWithAllSpecifications(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.QuoteRequestDetails);
            AddInclude($"{nameof(QuoteRequestHeader.QuoteRequestDetails)}.{nameof(QuoteRequestDetail.Product)}");
            AddInclude($"{nameof(QuoteRequestHeader.QuoteRequestDetails)}.{nameof(QuoteRequestDetail.MissingProduct)}.{nameof(ERP.Domain.Entities.Purchases.MissingProducts.MissingProduct.Product)}");

            AddInclude($"{nameof(QuoteRequestHeader.RelQuoteRequestProviders)}.{nameof(RelQuoteRequestProvider.Provider)}");

            AddInclude($"{nameof(QuoteRequestHeader.QuoteRequestDetails)}.{nameof(QuoteRequestDetail.Product)}.{nameof(Product.RelProviderProducts)}");
            AddInclude($"{nameof(QuoteRequestHeader.QuoteRequestDetails)}.{nameof(QuoteRequestDetail.Product)}.{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.Provider)}");
            AddInclude($"{nameof(QuoteRequestHeader.QuoteRequestDetails)}.{nameof(QuoteRequestDetail.Product)}.{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.UnitMeasure)}");
            AddInclude($"{nameof(QuoteRequestHeader.QuoteRequestDetails)}.{nameof(QuoteRequestDetail.Product)}.{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.WidthUnitMeasure)}");
            AddInclude($"{nameof(QuoteRequestHeader.QuoteRequestDetails)}.{nameof(QuoteRequestDetail.Product)}.{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.LengthUnitMeasure)}");
            AddInclude($"{nameof(QuoteRequestHeader.QuoteRequestDetails)}.{nameof(QuoteRequestDetail.Product)}.{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.WeightUnitMeasure)}");
            AddInclude($"{nameof(QuoteRequestHeader.QuoteRequestDetails)}.{nameof(QuoteRequestDetail.Product)}.{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.PresentationUnitMeasure)}");

            AddInclude($"{nameof(QuoteRequestHeader.QuoteRequestDetails)}.{nameof(QuoteRequestDetail.Product)}.{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.PriceUnitMeasure)}");
            AddInclude($"{nameof(QuoteRequestHeader.QuoteRequestDetails)}.{nameof(QuoteRequestDetail.ProviderUnitMeasure)}");
            AddInclude($"{nameof(QuoteRequestHeader.QuoteRequestDetails)}.{nameof(QuoteRequestDetail.QuantityToOrderUnitMeasure)}");

        }
    }
}
