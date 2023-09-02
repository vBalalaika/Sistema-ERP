using ERP.Domain.Entities.ProductMod;
using System.Linq;

namespace ERP.Application.Specification.ProductMod.CategorySpecifications
{
    public class CategoryDeleteValidatorSpecification : BaseSpecification<Category>
    {
        public CategoryDeleteValidatorSpecification(int idCategory)
           : base(x => x.SubCategories.Any(sub => sub.CategoryId == idCategory))
        {
            AddInclude(x => x.SubCategories);
        }
    }
}
