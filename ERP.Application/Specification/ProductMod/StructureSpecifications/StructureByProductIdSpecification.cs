using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Specification.ProductMod.StructureSpecifications
{
    public class StructureByProductIdSpecification : BaseSpecification<Structure>
    {
        public StructureByProductIdSpecification(int productId)
            : base(x => x.ProductId == productId)
        {

        }
    }
}
