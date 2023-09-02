using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Specification.ProductMod.StructureSpecifications
{
    public class StructureStandarByProductSpecification : BaseSpecification<Structure>
    {
        public StructureStandarByProductSpecification(int productId)
            : base(x => x.ProductId == productId && x.IsStandard)
        {

        }
    }
}
