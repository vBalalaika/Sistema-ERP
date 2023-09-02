using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Specification.ProductMod.SubCategorySpecifications
{
    public class SubCategoryDeleteValidatorSpecification : BaseSpecification<Product>
    {
        public SubCategoryDeleteValidatorSpecification(int idSubCategory)
          : base(x => x.SubCategoryId == idSubCategory)
        {
        }
    }
}
