using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Specification.ProductMod.StructureSpecifications
{
    public class StructureWithItemsSpecification : BaseSpecification<Structure>
    {
        public StructureWithItemsSpecification(int structureID)
            : base(x => x.Id == structureID)
        {

            AddInclude(x => x.Product);
            AddInclude(x => x.LastVersion);
            AddInclude(x => x.SupplyVoltage);

            AddInclude($"{nameof(Structure.StructureItems)}.{nameof(StructureItem.SonProduct)}.{nameof(Product.SubCategory)}");
            AddInclude($"{nameof(Structure.StructureItems)}.{nameof(StructureItem.SonProduct)}.{nameof(Product.ProductFeature)}");
        }
        public StructureWithItemsSpecification(int productId, bool isStandard)
            : base(x => x.ProductId == productId && x.IsStandard == isStandard)
        {

            AddInclude(x => x.Product);
            AddInclude(x => x.LastVersion);
            AddInclude(x => x.SupplyVoltage);
        }
    }
}


