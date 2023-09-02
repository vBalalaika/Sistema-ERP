using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Specification.ProductMod.SubCategorySpecifications
{
    public class SubCategoriesByCategoryIdSpecification : BaseSpecification<SubCategory>
    {
        public SubCategoriesByCategoryIdSpecification(int categoryId) : base(sub => sub.CategoryId == categoryId)
        {
            AddInclude(sub => sub.Category);
        }
    }
}
