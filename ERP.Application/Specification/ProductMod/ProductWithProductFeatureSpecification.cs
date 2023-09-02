using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Specification.ProductMod
{
    public class ProductWithProductFeatureSpecification : BaseSpecification<Product>
    {
        public ProductWithProductFeatureSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductFeature);
        }
    }
}
