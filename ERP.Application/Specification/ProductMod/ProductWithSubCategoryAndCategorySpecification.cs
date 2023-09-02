using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Specification.ProductMod
{
    public class ProductWithSubCategoryAndCategorySpecification : BaseSpecification<Product>
    {
        public ProductWithSubCategoryAndCategorySpecification(int productId) : base(pr => pr.Id == productId)
        {
            AddInclude(pr => pr.SubCategory);
            AddInclude($"{nameof(Product.SubCategory)}.{nameof(SubCategory.Category)}");
        }
    }
}
