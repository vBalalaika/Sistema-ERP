using ERP.Domain.Entities.Purchases.Providers;

namespace ERP.Application.Specification.Purchases.ProviderProduct
{
    public class RelProviderProductGetProvidersByProductIdSpecification : BaseSpecification<RelProviderProduct>
    {
        public RelProviderProductGetProvidersByProductIdSpecification(int id) : base(x => x.ProductId == id)
        {
            AddInclude(x => x.Provider);
            AddInclude(x => x.UnitMeasure);
        }
    }
}