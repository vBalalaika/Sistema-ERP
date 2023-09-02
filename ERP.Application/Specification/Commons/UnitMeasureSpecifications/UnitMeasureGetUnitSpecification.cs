using ERP.Domain.Entities.Commons;

namespace ERP.Application.Specification.Commons.UnitMeasureSpecifications
{
    public class UnitMeasureGetUnitSpecification : BaseSpecification<UnitMeasure>
    {
        public UnitMeasureGetUnitSpecification() : base(um => um.Code == 7)
        {

        }
    }
}
