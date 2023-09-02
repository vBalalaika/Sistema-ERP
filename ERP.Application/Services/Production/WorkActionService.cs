using AutoMapper;
using ERP.Application.DTOs.Entities.Production;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services.Production;
using ERP.Domain.Entities.Production;

namespace ERP.Application.Services.Production
{
    public class WorkActionService : GenericService<WorkAction, WorkActionDTO>, IWorkActionService
    {
        public WorkActionService(IUnitOfWork unitOfWOrk, IMapper mapper) : base(unitOfWOrk, mapper)
        {

        }
    }
}
