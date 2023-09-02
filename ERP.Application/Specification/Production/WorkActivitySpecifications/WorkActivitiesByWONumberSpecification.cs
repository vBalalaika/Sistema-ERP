using ERP.Domain.Entities.Production;
using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Specification.Production.WorkActivitySpecifications
{
    public class WorkActivitiesByWONumberSpecification : BaseSpecification<WorkActivity>
    {
        public WorkActivitiesByWONumberSpecification(int WONumber, int stationId) : base(wa => wa.StateActivity == true && wa.WorkOrderItem.WorkOrder.WorkOrderHeader.WorkOrderHeaderNumber == WONumber && wa.ProductStation.StationId == stationId && wa.WorkOrderItem.Product.ProductFeature.StoredStock == false)
        {
            AddInclude(wa => wa.ProductStation);
            AddInclude(wa => wa.WorkOrderItem);
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.WorkOrder)}.{nameof(WorkOrder.WorkOrderHeader)}");
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.Product)}.{nameof(Product.ProductFeature)}");
        }
    }
}
