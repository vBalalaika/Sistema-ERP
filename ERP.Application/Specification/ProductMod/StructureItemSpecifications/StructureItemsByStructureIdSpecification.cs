using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Specification.ProductMod.StructureItemSpecifications
{
    public class StructureItemsByStructureIdSpecification : BaseSpecification<StructureItem>
    {
        public StructureItemsByStructureIdSpecification(int idStructure)
            : base(x => x.StructureId == idStructure)
        {
        }
    }
}
