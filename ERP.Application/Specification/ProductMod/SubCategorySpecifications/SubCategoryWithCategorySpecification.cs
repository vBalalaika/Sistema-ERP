using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Specification.ProductMod.SubCategorySpecifications
{
    class SubCategoryWithCategorySpecification : BaseSpecification<SubCategory>
    {
        public SubCategoryWithCategorySpecification()
        {
            AddInclude(x => x.Category);
        }
        public SubCategoryWithCategorySpecification(int idSubCategory)
          : base(x => x.Id == idSubCategory)
        {
            AddInclude(x => x.Category);
        }
    }
}
