using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Specification.ProductMod
{
    public class RawMaterialProductsSpecification : BaseSpecification<Product>
    {
        public RawMaterialProductsSpecification(int categoryExcluded) :
          base(x => x.SubCategory.CategoryId != categoryExcluded)
        {

        }
    }
}
