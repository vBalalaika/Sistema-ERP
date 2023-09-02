using ERP.Domain.Entities.Sales;

namespace ERP.Application.Specification.Sales
{
    public class PriceByStructureIdSpecification : BaseSpecification<Price>
    {
        public PriceByStructureIdSpecification(int structureId) : base(pr => pr.StructureId.HasValue && pr.StructureId.Value == structureId)
        {

        }
    }
}
