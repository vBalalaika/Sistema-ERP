using ERP.Domain.Entities.Production;
using System.Linq;

namespace ERP.Application.Specification.Production.WorkActivitySpecifications
{
    public class WorkActivityWithActionByIdSpecification : BaseSpecification<WorkActivity>
    {
        public WorkActivityWithActionByIdSpecification(int activityId) : base(x => x.Id == activityId)
        {
            AddInclude(x => x.WorkActions.OrderBy(wa => wa.StartDate));

        }
    }
}
