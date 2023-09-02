using ERP.Domain.Entities.Production;
using ERP.Domain.Entities.ProductMod;
using ERP.Domain.Entities.Sales;

namespace ERP.Application.Specification.Production
{
    public class WorkOrderSpecification : BaseSpecification<WorkOrder>
    {
        public WorkOrderSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.WorkOrderHeader);
            AddInclude($"{nameof(WorkOrder.OrderDetail)}.{nameof(OrderDetail.Product)}");
            AddInclude($"{nameof(WorkOrder.OrderDetail)}.{nameof(OrderDetail.Structure)}");
            AddInclude($"{nameof(WorkOrder.WorkOrderItems)}.{nameof(WorkOrderItem.WorkActivities)}.{nameof(WorkActivity.ProductStation)}.{nameof(RelProductStation.Station)}");
            AddInclude($"{nameof(WorkOrder.WorkOrderItems)}.{nameof(WorkOrderItem.Product)}.{nameof(Product.ProductFeature)}");
            AddInclude($"{nameof(WorkOrder.WorkOrderItems)}.{nameof(WorkOrderItem.Product)}.{nameof(Product.UnitMeasure)}");
            AddInclude($"{nameof(WorkOrder.WorkOrderItems)}.{nameof(WorkOrderItem.Product)}.{nameof(Product.SubCategory)}.{nameof(SubCategory.Category)}");
            AddInclude($"{nameof(WorkOrder.WorkOrderItems)}.{nameof(WorkOrderItem.Structure)}.{nameof(Structure.Product)}.{nameof(Product.ProductFeature)}");
            AddInclude($"{nameof(WorkOrder.WorkOrderItems)}.{nameof(WorkOrderItem.WorkActivities)}.{nameof(WorkActivity.WorkActions)}");

            AddInclude($"{nameof(WorkOrder.WorkOrderItems)}.{nameof(WorkOrderItem.Product)}.{nameof(Product.RelProductStations)}.{nameof(RelProductStation.Station)}");

            AddInclude($"{nameof(WorkOrder.WorkOrderItems)}.{nameof(WorkOrderItem.WorkActivities)}.{nameof(WorkActivity.OutsourcedProvider)}");

            AddInclude($"{nameof(WorkOrder.WorkOrderItems)}.{nameof(WorkOrderItem.WorkOrderItemOf)}.{nameof(WorkOrderItem.Product)}.{nameof(Product.ProductFeature)}");

        }
    }
}
