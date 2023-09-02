using AutoMapper;
using ERP.Application.DTOs.Entities.Production;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services.Production;
using ERP.Domain.Entities.Production;

namespace ERP.Application.Services.Production
{
    public class WorkOrderHeaderService : GenericService<WorkOrderHeader, WorkOrderHeaderDTO>, IWorkOrderHeaderService
    {
        public WorkOrderHeaderService(IUnitOfWork unitOfWOrk, IMapper mapper) : base(unitOfWOrk, mapper)
        {

        }
    }

}
