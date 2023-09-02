using ERP.Domain.Entities.ProductMod;
using System.Linq;

namespace ERP.Application.Specification.ProductMod
{
    public class ProductByCategoryAndInHouseSpecification : BaseSpecification<Product>
    {
        //TODO: mejorar ifs
        public ProductByCategoryAndInHouseSpecification(int? CategoryId, bool? InHouse)
            : base(x => (CategoryId != null ? x.SubCategory.CategoryId == CategoryId : x.Id != null) && (InHouse != null ? InHouse.Value ? x.ProductFeature.InHouseManufacturing == true : x.ProductFeature.Bought == true : x.Id != null))
        {
            AddInclude(x => x.ProductFeature);
            AddInclude(x => x.SubCategory);
            AddInclude(x => x.UnitMeasure);
            AddInclude(x => x.Archives);
            AddInclude(x => x.Structures);
            AddInclude(x => x.RelProductStations.OrderBy(rel => rel.Order));
            AddInclude(x => x.RelProviderProducts);
            AddInclude($"{nameof(Product.RelProductStations)}.{nameof(RelProductStation.Station)}");
            AddInclude($"{nameof(Product.SubCategory)}.{nameof(SubCategory.Category)}");
        }
    }
}
