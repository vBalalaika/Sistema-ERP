using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Specification.ProductMod
{
    public class ProductsExcludingSelfSpecification : BaseSpecification<Product>
    {
        public ProductsExcludingSelfSpecification(int productId)
            : base()
        {
        }
    }
}
