
using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Specification.ProductMod.SubCategorySpecifications
{
    public class SubCategoriesByPrefixSpecification : BaseSpecification<SubCategory>
    {
        public SubCategoriesByPrefixSpecification(string prefix) :
            base(x => x.Prefix.Equals(prefix))
        {

        }
    }
}
