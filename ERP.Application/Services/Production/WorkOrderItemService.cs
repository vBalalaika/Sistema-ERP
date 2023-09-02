using AutoMapper;
using ERP.Application.DTOs.Entities.Production;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services.Production;
using ERP.Domain.Entities.Production;

namespace ERP.Application.Services.Production
{
    public class WorkOrderItemService : GenericService<WorkOrderItem, WorkOrderItemDTO>, IWorkOrderItemService
    {
        public WorkOrderItemService(IUnitOfWork unitOfWOrk, IMapper mapper) : base(unitOfWOrk, mapper)
        {
        }

        public int DeleteByWorkOrderIdAsync(int workOrderId)
        {
            return _unitOfWork.WorkOrderItemRepository.DeleteByWorkOrderId(workOrderId);
        }
    }
}