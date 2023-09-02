using ERP.Domain.Entities.Production;

namespace ERP.Application.Specification.Production.GroupedWorkActionSpecifications
{
    public class GetGroupedWorkActionsSpecification : BaseSpecification<GroupedWorkAction>
    {
        public GetGroupedWorkActionsSpecification(int stationId) : base(gwa => gwa.StationId == stationId)
        {

        }

        public GetGroupedWorkActionsSpecification(string workActivitiesIds) : base(gwa => gwa.WorkActivitiesIds == workActivitiesIds)
        {

        }
    }
}
