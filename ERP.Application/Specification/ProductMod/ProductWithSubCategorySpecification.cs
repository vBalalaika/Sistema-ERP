using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Specification.ProductMod
{
    public class ProductWithSubCategorySpecification : BaseSpecification<Product>
    {

        public ProductWithSubCategorySpecification(bool? InHouse)
            : base(x => (InHouse.Value ? x.ProductFeature.InHouseManufacturing == true : x.ProductFeature.Bought == true))
        {
            AddInclude(x => x.SubCategory);
            AddInclude(x => x.ProductFeature);
            AddInclude(x => x.UnitMeasure);

        }
    }
}
