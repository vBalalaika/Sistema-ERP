using ERP.Domain.Entities.Production;

namespace ERP.Application.Specification.Production.WorkActionSpecifications
{
    public class WorkActionsByActivityIdSpecification : BaseSpecification<WorkAction>
    {
        public WorkActionsByActivityIdSpecification(int activityId) : base(x => x.WorkActivityId == activityId)
        {

        }
    }
}
