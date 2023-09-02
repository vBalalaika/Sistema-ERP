using ERP.Domain.Entities.ProductMod;
using System.Linq;


namespace ERP.Application.Specification.ProductMod.StructureSpecifications
{
    public class StructureDeleteValidatorSpecification : BaseSpecification<Structure>
    {
        public StructureDeleteValidatorSpecification(int idStructure)
              : base(x => x.StructureItems.Any(item => item.SonStructureId == idStructure))
        {
        }
    }
}
