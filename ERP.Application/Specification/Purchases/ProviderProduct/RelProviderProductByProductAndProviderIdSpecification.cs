using ERP.Domain.Entities.Purchases.Providers;

namespace ERP.Application.Specification.Purchases.ProviderProduct
{
    public class RelProviderProductByProductAndProviderIdSpecification : BaseSpecification<RelProviderProduct>
    {
        public RelProviderProductByProductAndProviderIdSpecification(int ProductId, int ProviderId) : base(rpp => rpp.ProductId == ProductId && rpp.ProviderId == ProviderId)
        {
            AddInclude(rpp => rpp.Product);
        }
    }
}
