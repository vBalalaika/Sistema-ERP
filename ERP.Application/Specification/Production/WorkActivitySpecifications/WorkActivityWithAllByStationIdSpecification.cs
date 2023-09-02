using ERP.Domain.Entities.Production;
using ERP.Domain.Entities.ProductMod;
using ERP.Domain.Entities.Sales;

namespace ERP.Application.Specification.Production.WorkActivitySpecifications
{
    public class WorkActivityWithAllByStationIdSpecification : BaseSpecification<WorkActivity>
    {
        public WorkActivityWithAllByStationIdSpecification(int stationId) : base(x => x.ProductStation.StationId == stationId)
        {
            AddInclude(x => x.WorkOrderItem);
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.WorkOrder)}.{nameof(WorkOrder.WorkOrderHeader)}");
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.WorkOrder)}.{nameof(WorkOrder.OrderDetail)}.{nameof(OrderDetail.Product)}");
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.WorkOrder)}.{nameof(WorkOrder.OrderDetail)}.{nameof(OrderDetail.Structure)}");
            AddInclude($"{nameof(WorkActivity.ProductStation)}.{nameof(RelProductStation.Station)}");
        }
        public WorkActivityWithAllByStationIdSpecification(int stationId, bool isActive) : base(x => x.ProductStation.StationId == stationId && x.StateActivity == isActive)
        {
            AddInclude(x => x.WorkOrderItem);
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.WorkOrder)}.{nameof(WorkOrder.WorkOrderHeader)}");
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.WorkOrder)}.{nameof(WorkOrder.OrderDetail)}.{nameof(OrderDetail.Product)}");
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.WorkOrder)}.{nameof(WorkOrder.OrderDetail)}.{nameof(OrderDetail.Structure)}");
            AddInclude($"{nameof(WorkActivity.ProductStation)}.{nameof(RelProductStation.Station)}");
        }

        public WorkActivityWithAllByStationIdSpecification(int workActivityId, int stationId) : base(x => x.Id == workActivityId && x.ProductStation.StationId == stationId)
        {
            AddInclude(x => x.WorkOrderItem);
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.WorkOrder)}.{nameof(WorkOrder.WorkOrderHeader)}");
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.WorkOrder)}.{nameof(WorkOrder.OrderDetail)}.{nameof(OrderDetail.Product)}");
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.WorkOrder)}.{nameof(WorkOrder.OrderDetail)}.{nameof(OrderDetail.Structure)}");
            AddInclude($"{nameof(WorkActivity.ProductStation)}.{nameof(RelProductStation.Station)}");
        }
    }
}
