using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Specification.ProductMod
{
    public class ProductWithSparesForPricesSpecification : BaseSpecification<Product>
    {
        public ProductWithSparesForPricesSpecification(bool spares) : base(p => spares == true ? p.SubCategory.CategoryId == 2 || p.SubCategory.CategoryId == 3 || p.SubCategory.CategoryId == 4 : p.SubCategory.CategoryId == 1)
        {
            AddInclude(x => x.SubCategory);
            AddInclude($"{nameof(Product.SubCategory)}.{nameof(SubCategory.Category)}");
        }
    }
}
