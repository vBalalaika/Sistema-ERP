using ERP.Domain.Entities.ProductMod;
using ERP.Domain.Entities.Purchases.Providers;

namespace ERP.Application.Specification.Purchases.ProviderProduct
{
    public class RelProviderProductWithAllSpecification : BaseSpecification<RelProviderProduct>
    {
        public RelProviderProductWithAllSpecification()
        {
            AddInclude(x => x.Product);
            AddInclude(x => x.Provider);
            AddInclude(x => x.UnitMeasure);
            AddInclude(x => x.PriceUnitMeasure);
            AddInclude($"{nameof(RelProviderProduct.Product)}.{nameof(Product.SubCategory)}");
        }

        public RelProviderProductWithAllSpecification(int id) : base(rpp => rpp.Id == id)
        {
            AddInclude(x => x.Product);
            AddInclude(x => x.Provider);
            AddInclude(x => x.UnitMeasure);
            AddInclude(x => x.PriceUnitMeasure);
            AddInclude($"{nameof(RelProviderProduct.Product)}.{nameof(Product.SubCategory)}");
        }
    }
}
