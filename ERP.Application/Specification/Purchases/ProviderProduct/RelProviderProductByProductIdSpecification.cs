using ERP.Domain.Entities.Purchases.Providers;

namespace ERP.Application.Specification.Purchases.ProviderProduct
{
    public class RelProviderProductByProductIdSpecification : BaseSpecification<RelProviderProduct>
    {
        public RelProviderProductByProductIdSpecification(int? id) : base(x => x.ProductId == id)
        {
            AddInclude(x => x.Provider);
            AddInclude(x => x.Product);
            AddInclude(x => x.UnitMeasure);
        }
    }
}
