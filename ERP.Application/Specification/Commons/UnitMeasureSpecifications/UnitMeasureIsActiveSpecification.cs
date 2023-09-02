using ERP.Domain.Entities.Commons;

namespace ERP.Application.Specification.Commons.UnitMeasureSpecifications
{
    public class UnitMeasureIsActiveSpecification : BaseSpecification<UnitMeasure>
    {
        public UnitMeasureIsActiveSpecification() : base(um => um.IsActive == true)
        {
            AddOrderByDescending(x => x.Description);
        }
    }
}
