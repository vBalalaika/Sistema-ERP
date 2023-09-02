using ERP.Application.DTOs.Entities.Production;
using ERP.Domain.Entities.Production;

namespace ERP.Application.Interfaces.Services.Production
{
    public interface IWorkOrderHeaderService : IGenericService<WorkOrderHeader, WorkOrderHeaderDTO>
    {
    }
}
