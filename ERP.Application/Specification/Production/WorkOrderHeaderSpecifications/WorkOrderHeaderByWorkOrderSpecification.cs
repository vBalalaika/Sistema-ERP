using ERP.Domain.Entities.Production;
using ERP.Domain.Entities.ProductMod;
using ERP.Domain.Entities.Sales;

namespace ERP.Application.Specification.Production.WorkOrderHeaderSpecifications
{
    public class WorkOrderHeaderByWorkOrderSpecification : BaseSpecification<WorkOrderHeader>
    {

        public WorkOrderHeaderByWorkOrderSpecification()
        {
            AddInclude(x => x.WorkOrders);

        }
        public WorkOrderHeaderByWorkOrderSpecification(int idWorkOrder) : base(x => x.WorkOrders.Contains(new WorkOrder { Id = idWorkOrder }))
        {
            AddInclude(x => x.WorkOrders);
            AddInclude($"{nameof(WorkOrderHeader.WorkOrders)}.{nameof(WorkOrder.WorkOrderItems)}." +
                        $"{nameof(WorkOrderItem.WorkActivities)}.{nameof(WorkActivity.ProductStation)}.{nameof(RelProductStation.Station)}.{nameof(Station.FunctionalArea)}");
            AddInclude($"{nameof(WorkOrderHeader.WorkOrders)}.{nameof(WorkOrder.OrderDetail)}.{nameof(OrderDetail.Product)}");
            AddInclude($"{nameof(WorkOrderHeader.WorkOrders)}.{nameof(WorkOrder.OrderDetail)}.{nameof(OrderDetail.Structure)}");
            AddInclude($"{nameof(WorkOrderHeader.WorkOrders)}.{nameof(WorkOrder.WorkOrderItems)}.{nameof(WorkOrderItem.WorkActivities)}.{nameof(WorkActivity.WorkActions)}");

            AddInclude($"{nameof(WorkOrderHeader.WorkOrders)}.{nameof(WorkOrder.WorkOrderItems)}.{nameof(WorkOrderItem.Product)}");
            AddInclude($"{nameof(WorkOrderHeader.WorkOrders)}.{nameof(WorkOrder.WorkOrderItems)}.{nameof(WorkOrderItem.Product)}.{nameof(Product.ProductFeature)}");
        }
    }
}
