using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Specification.ProductMod.CategorySpecifications
{
    public class CategoriesForSparesSpecification : BaseSpecification<Category>
    {
        public CategoriesForSparesSpecification(bool spares) : base(c => spares == true ? c.Id == 2 || c.Id == 3 || c.Id == 4 : c.Id == 1)
        {

        }
    }
}
