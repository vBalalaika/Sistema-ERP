using ERP.Domain.Entities.Production;

namespace ERP.Application.Specification.Production.WorkOrderItemSpecifications
{
    public class WOIByProductNumberSpecification : BaseSpecification<WorkOrderItem>
    {
        public WOIByProductNumberSpecification(string productNumber) : base(woi => woi.WorkOrder.OrderDetail.ProductNumber.Equals(productNumber.Trim()))
        {
            AddInclude(woi => woi.WorkOrder);
            AddInclude(woi => woi.WorkOrderItemOf);
            AddInclude($"{nameof(WorkOrderItem.WorkOrder)}.{nameof(WorkOrder.OrderDetail)}");

        }
    }
}
