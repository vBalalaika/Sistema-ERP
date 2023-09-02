using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Specification.ProductMod.StructureItemSpecifications
{
    public class StructureItemsByStructureIdWithDataSpecification : BaseSpecification<StructureItem>
    {
        public StructureItemsByStructureIdWithDataSpecification(int idStructure, int version)
            : base(x => x.StructureId == idStructure && x.Version.VersionNumber == version)
        {
            AddInclude($"{nameof(StructureItem.SonProduct)}.{nameof(Product.ProductFeature)}");
            AddInclude($"{nameof(StructureItem.SonProduct)}.{nameof(Product.SubCategory)}");
            AddInclude($"{nameof(StructureItem.SonProduct)}.{nameof(Product.RelProductStations)}.{nameof(RelProductStation.Station)}");

            AddInclude($"{nameof(StructureItem.SonStructure)}.{nameof(Structure.Product)}.{nameof(Product.ProductFeature)}");
            AddInclude($"{nameof(StructureItem.SonStructure)}.{nameof(Structure.Product)}.{nameof(Product.RelProductStations)}.{nameof(RelProductStation.Station)}");
            AddOrderBy(x => x.Order);
        }
    }
}
