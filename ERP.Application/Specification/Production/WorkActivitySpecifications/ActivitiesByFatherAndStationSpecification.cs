using ERP.Domain.Entities.Production;
using ERP.Domain.Entities.ProductMod;
using System.Linq;

namespace ERP.Application.Specification.Production.WorkActivitySpecifications
{
    public class ActivitiesByFatherAndStationSpecification : BaseSpecification<WorkActivity>
    {
        public ActivitiesByFatherAndStationSpecification(int fatherId, int stationId) : base(x => (x.StateActivity == true) && (x.WorkOrderItem.WorkOrderItemOfId == fatherId && x.ProductStation.StationId == stationId))
        {
            AddInclude(x => x.WorkActions.OrderBy(wa => wa.StartDate));
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.Product)}.{nameof(Product.ProductFeature)}");
        }

        public ActivitiesByFatherAndStationSpecification(int fatherId) : base(x => (x.WorkOrderItem.WorkOrderItemOfId == fatherId))
        {
            AddInclude(x => x.WorkActions.OrderBy(wa => wa.StartDate));
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.Product)}.{nameof(Product.ProductFeature)}");
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.Product)}.{nameof(Product.RelProductStations)}.{nameof(RelProductStation.Station)}");
            AddInclude($"{nameof(WorkActivity.ProductStation)}.{nameof(RelProductStation.Station)}");
        }
    }
}
