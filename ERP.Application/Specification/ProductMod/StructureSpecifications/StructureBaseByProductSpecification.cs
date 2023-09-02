using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Specification.ProductMod.StructureSpecifications
{
    public class StructureBaseByProductSpecification : BaseSpecification<Structure>
    {
        public StructureBaseByProductSpecification(int productId)
            : base(x => x.ProductId == productId && x.IsBase)
        {

        }
    }
}
