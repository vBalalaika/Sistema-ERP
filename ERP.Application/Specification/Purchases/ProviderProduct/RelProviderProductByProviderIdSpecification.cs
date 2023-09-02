using ERP.Domain.Entities.Purchases.Providers;

namespace ERP.Application.Specification.Purchases.ProviderProduct
{
    public class RelProviderProductByProviderIdSpecification : BaseSpecification<RelProviderProduct>
    {

        public RelProviderProductByProviderIdSpecification(int id) : base(x => x.ProviderId == id)
        {
            AddInclude(x => x.Provider);
            AddInclude(x => x.Product);
            AddInclude(x => x.UnitMeasure);
            AddInclude(x => x.WidthUnitMeasure);
            AddInclude(x => x.LengthUnitMeasure);
            AddInclude(x => x.WeightUnitMeasure);
            AddInclude(x => x.PresentationUnitMeasure);
            AddInclude(x => x.PriceUnitMeasure);
            AddInclude(x => x.HeightUnitMeasure);
            AddInclude($"{nameof(RelProviderProduct.Provider)}.{nameof(Provider.Country)}");
            AddInclude($"{nameof(RelProviderProduct.Provider)}.{nameof(Provider.State)}");
            AddInclude($"{nameof(RelProviderProduct.Provider)}.{nameof(Provider.City)}");
            AddInclude($"{nameof(RelProviderProduct.Provider)}.{nameof(Provider.DocumentType)}");
            AddInclude($"{nameof(RelProviderProduct.Provider)}.{nameof(Provider.IVACondition)}");
        }
    }
}
