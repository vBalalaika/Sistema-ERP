using ERP.Domain.Entities.Commons;

namespace ERP.Application.Specification.Commons.UnitMeasureSpecifications
{
    public class UnitMeasureSpecification : BaseSpecification<UnitMeasure>
    {
        public UnitMeasureSpecification()
        {

        }

        public UnitMeasureSpecification(int id) : base(um => um.Id == id)
        {

        }
    }
}
