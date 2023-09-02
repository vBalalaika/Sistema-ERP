using ERP.Domain.Entities.Production;

namespace ERP.Application.Specification.Production.WorkOrderItemSpecifications
{
    public class WorkOrderItemByWorkOrderIdSpecification : BaseSpecification<WorkOrderItem>
    {
        public WorkOrderItemByWorkOrderIdSpecification(int workOrderId) : base(woi => woi.WorkOrderId == workOrderId)
        {
        }
    }
}