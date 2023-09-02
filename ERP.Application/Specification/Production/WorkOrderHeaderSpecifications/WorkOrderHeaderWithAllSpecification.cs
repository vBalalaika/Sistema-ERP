using ERP.Domain.Entities.Production;
using ERP.Domain.Entities.ProductMod;
using ERP.Domain.Entities.Sales;

namespace ERP.Application.Specification.Production.WorkOrderHeaderSpecifications
{
    public class WorkOrderHeaderWithAllSpecification : BaseSpecification<WorkOrderHeader>
    {

        public WorkOrderHeaderWithAllSpecification()
        {
            AddInclude(x => x.WorkOrders);
            AddInclude($"{nameof(WorkOrderHeader.WorkOrders)}.{nameof(WorkOrder.WorkOrderItems)}." +
                        $"{nameof(WorkOrderItem.WorkActivities)}.{nameof(WorkActivity.ProductStation)}.{nameof(RelProductStation.Station)}");
            AddInclude($"{nameof(WorkOrderHeader.WorkOrders)}.{nameof(WorkOrder.OrderDetail)}.{nameof(OrderDetail.Product)}");

        }
        public WorkOrderHeaderWithAllSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.WorkOrders);
            AddInclude($"{nameof(WorkOrderHeader.WorkOrders)}.{nameof(WorkOrder.WorkOrderItems)}." +
                        $"{nameof(WorkOrderItem.WorkActivities)}.{nameof(WorkActivity.ProductStation)}.{nameof(RelProductStation.Station)}");
            AddInclude($"{nameof(WorkOrderHeader.WorkOrders)}.{nameof(WorkOrder.OrderDetail)}.{nameof(OrderDetail.Product)}");

        }
    }
}
