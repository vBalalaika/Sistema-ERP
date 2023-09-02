using ERP.Domain.Entities.Production;

namespace ERP.Application.Interfaces.Repositories.Production
{
    public interface IWorkOrderItemRepository : IGenericRepository<WorkOrderItem>
    {
        int DeleteByWorkOrderId(int workOrderId);
    }
}