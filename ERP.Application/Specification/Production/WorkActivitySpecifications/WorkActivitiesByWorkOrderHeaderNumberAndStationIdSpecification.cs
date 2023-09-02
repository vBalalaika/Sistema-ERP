using ERP.Domain.Entities.Production;
using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Specification.Production.WorkActivitySpecifications
{
    public class WorkActivitiesByWorkOrderHeaderNumberAndStationIdSpecification : BaseSpecification<WorkActivity>
    {
        public WorkActivitiesByWorkOrderHeaderNumberAndStationIdSpecification(int workOrderHeaderNumber, int stationId) : base(x => (x.WorkOrderItem.WorkOrder.WorkOrderHeader.WorkOrderHeaderNumber == workOrderHeaderNumber && x.ProductStation.StationId == stationId))
        {
            AddInclude(wa => wa.WorkOrderItem);
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.WorkOrder)}.{nameof(WorkOrder.OrderDetail)}");
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.Product)}.{nameof(Product.ProductFeature)}");
        }
    }
}
