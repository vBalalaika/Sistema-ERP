using ERP.Domain.Entities.Production;
using ERP.Domain.Entities.ProductMod;
using System.Linq;

namespace ERP.Application.Specification.Production.WorkActivitySpecifications
{
    public class ActivitiesByWorkOrderAndStationSpecification : BaseSpecification<WorkActivity>
    {
        public ActivitiesByWorkOrderAndStationSpecification(int workOrderId, int stationId) : base(x => (x.StateActivity == true) && (x.WorkOrderItem.WorkOrderId == workOrderId && x.ProductStation.StationId == stationId))
        {
            AddInclude(x => x.OutsourcedProvider);
            AddInclude(x => x.WorkActions.OrderBy(wa => wa.StartDate));
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.Product)}.{nameof(Product.ProductFeature)}");
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.Product)}.{nameof(Product.Archives)}");
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.Product)}.{nameof(Product.RelProductStations)}.{nameof(RelProductStation.Station)}");
            //AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.WorkActivities)}");
            AddInclude(wa => wa.ProductStation);
            AddInclude($"{nameof(WorkActivity.ProductStation)}.{nameof(RelProductStation.Station)}");
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.WorkOrder)}.{nameof(WorkOrder.OrderDetail)}");
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.Product)}.{nameof(Product.SubCategory)}");
        }
    }
}
