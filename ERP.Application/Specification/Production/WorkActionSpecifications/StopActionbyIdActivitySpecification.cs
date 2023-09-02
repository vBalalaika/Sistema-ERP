using ERP.Domain.Entities.Production;

namespace ERP.Application.Specification.Production.WorkActionSpecifications
{
    public class StopActionbyIdActivitySpecification : BaseSpecification<WorkAction>
    {
        public StopActionbyIdActivitySpecification(int activityId) : base(x => x.WorkActivityId == activityId && x.ActionName == "stop")
        {

        }
    }
}
