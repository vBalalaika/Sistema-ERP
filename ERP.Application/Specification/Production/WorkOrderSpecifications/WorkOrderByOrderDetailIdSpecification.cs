using ERP.Domain.Entities.Production;

namespace ERP.Application.Specification.Production.WorkOrderSpecifications
{
    public class WorkOrderByOrderDetailIdSpecification : BaseSpecification<WorkOrder>
    {
        public WorkOrderByOrderDetailIdSpecification(int orderDetailId) : base(wo => wo.OrderDetailId == orderDetailId)
        {
            AddInclude(wo => wo.OrderDetail);
            AddInclude(wo => wo.WorkOrderItems);
        }
    }
}