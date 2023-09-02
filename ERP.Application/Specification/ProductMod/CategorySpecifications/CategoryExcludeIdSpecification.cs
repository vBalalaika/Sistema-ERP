using ERP.Domain.Entities.ProductMod;
using System.Linq;

namespace ERP.Application.Specification.ProductMod.CategorySpecifications
{
    public class CategoryExcludeIdSpecification : BaseSpecification<Category>
    {
        public CategoryExcludeIdSpecification(int idCategory)
           : base(x => x.SubCategories.Any(sub => sub.CategoryId != idCategory))
        {
            //AddInclude(x => x.SubCategories);
        }
    }
}
