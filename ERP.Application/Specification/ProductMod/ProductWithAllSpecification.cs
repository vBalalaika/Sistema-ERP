using ERP.Domain.Entities.ProductMod;
using System.Linq;

namespace ERP.Application.Specification.ProductMod
{
    public class ProductWithAllSpecification : BaseSpecification<Product>
    {
        public ProductWithAllSpecification()
        {
            AddInclude(x => x.ProductPieceTypes);
            AddInclude(x => x.ProductFeature);
            AddInclude(x => x.ProductPieceFormats);
            AddInclude(x => x.ProductSupplyVoltages);
            AddInclude(x => x.RelProductStations.OrderBy(rel => rel.Order));
            AddInclude(x => x.ProductCutTypes);
            AddInclude(x => x.Archives);
            AddInclude(x => x.RelProviderProducts);
            AddInclude($"{nameof(Product.AccessoryProducts)}.{nameof(AccessoryProduct.ProductAccessory)}");
            AddInclude($"{nameof(Product.RelProductStations)}.{nameof(RelProductStation.Station)}");
            AddInclude($"{nameof(Product.SubCategory)}.{nameof(SubCategory.Category)}");
        }

        // For efficient search on select2
        public ProductWithAllSpecification(string searchParameterByCodeOrDescription) : base(prod => (prod.Code.Contains(searchParameterByCodeOrDescription) || prod.Description.Contains(searchParameterByCodeOrDescription)))
        {
            AddInclude(x => x.ProductPieceTypes);
            AddInclude(x => x.ProductFeature);
            AddInclude(x => x.ProductPieceFormats);
            AddInclude(x => x.ProductSupplyVoltages);
            AddInclude(x => x.RelProductStations.OrderBy(rel => rel.Order));
            AddInclude(x => x.ProductCutTypes);
            AddInclude(x => x.Archives);
            AddInclude(x => x.RelProviderProducts);
            AddInclude($"{nameof(Product.AccessoryProducts)}.{nameof(AccessoryProduct.ProductAccessory)}");
            AddInclude($"{nameof(Product.RelProductStations)}.{nameof(RelProductStation.Station)}");
            AddInclude($"{nameof(Product.SubCategory)}.{nameof(SubCategory.Category)}");
        }

        public ProductWithAllSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.UnitMeasure);
            AddInclude(x => x.ProductPieceTypes);
            AddInclude(x => x.ProductFeature);
            AddInclude(x => x.ProductPieceFormats);
            AddInclude(x => x.ProductSupplyVoltages);
            AddInclude(x => x.ProductCutTypes);
            AddInclude(x => x.Archives);
            AddInclude(x => x.Structures);
            AddInclude(x => x.RelProviderProducts);
            AddInclude(x => x.RelProductStations.OrderBy(rel => rel.Order));
            AddInclude($"{nameof(Product.AccessoryProducts)}.{nameof(AccessoryProduct.ProductAccessory)}");
            AddInclude($"{nameof(Product.RelProductStations)}.{nameof(RelProductStation.Station)}");
            AddInclude($"{nameof(Product.SubCategory)}.{nameof(SubCategory.Category)}");
            //AddInclude($"{nameof(Product.Archives)}.{nameof(Archive.ArchiveType)}");
        }
    }
}
