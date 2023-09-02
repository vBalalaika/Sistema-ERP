using ERP.Domain.Entities.Production;
using ERP.Domain.Entities.Sales;

namespace ERP.Application.Specification.Production.WorkOrderSpecifications
{
    public class WorkOrderWithHeaderSpecification : BaseSpecification<WorkOrder>
    {
        public WorkOrderWithHeaderSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.WorkOrderHeader);
            AddInclude($"{nameof(WorkOrder.OrderDetail)}.{nameof(OrderDetail.Product)}");
            AddInclude($"{nameof(WorkOrder.OrderDetail)}.{nameof(OrderDetail.Structure)}");
        }
    }
}
