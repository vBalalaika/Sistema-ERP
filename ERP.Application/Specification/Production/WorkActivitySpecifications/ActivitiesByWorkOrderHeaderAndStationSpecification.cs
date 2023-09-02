using ERP.Domain.Entities.Production;

namespace ERP.Application.Specification.Production.WorkActivitySpecifications
{
    public class ActivitiesByWorkOrderHeaderAndStationSpecification : BaseSpecification<WorkActivity>
    {
        public ActivitiesByWorkOrderHeaderAndStationSpecification(int workOrderHeaderId, int stationId) : base(x => (x.WorkOrderItem.WorkOrder.WorkOrderHeaderId == workOrderHeaderId && x.ProductStation.StationId == stationId))
        {

        }
    }
}