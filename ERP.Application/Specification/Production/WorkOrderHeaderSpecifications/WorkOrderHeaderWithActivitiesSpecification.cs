using ERP.Domain.Entities.Production;
using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Specification.Production.WorkOrderHeaderSpecifications
{
    public class WorkOrderHeaderWithActivitiesSpecification : BaseSpecification<WorkOrderHeader>
    {

        public WorkOrderHeaderWithActivitiesSpecification()
        {
            AddInclude(x => x.WorkOrders);
            AddInclude($"{nameof(WorkOrderHeader.WorkOrders)}.{nameof(WorkOrder.WorkOrderItems)}." +
                        $"{nameof(WorkOrderItem.WorkActivities)}.{nameof(WorkActivity.ProductStation)}.{nameof(RelProductStation.Station)}");


        }
        public WorkOrderHeaderWithActivitiesSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.WorkOrders);
            AddInclude($"{nameof(WorkOrderHeader.WorkOrders)}.{nameof(WorkOrder.WorkOrderItems)}." +
                        $"{nameof(WorkOrderItem.WorkActivities)}.{nameof(WorkActivity.ProductStation)}.{nameof(RelProductStation.Station)}");


        }
    }
}
