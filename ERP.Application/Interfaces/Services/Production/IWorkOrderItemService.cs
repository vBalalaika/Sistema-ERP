using ERP.Application.DTOs.Entities.Production;
using ERP.Domain.Entities.Production;

namespace ERP.Application.Interfaces.Services.Production
{
    public interface IWorkOrderItemService : IGenericService<WorkOrderItem, WorkOrderItemDTO>
    {
        int DeleteByWorkOrderIdAsync(int workOrderId);
    }
}