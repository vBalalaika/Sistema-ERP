using AutoMapper;
using ERP.Application.DTOs.Entities.Production;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services.Production;
using ERP.Domain.Entities.Production;

namespace ERP.Application.Services.Production
{
    public class GroupedWorkActionService : GenericService<GroupedWorkAction, GroupedWorkActionDTO>, IGroupedWorkActionService
    {
        public GroupedWorkActionService(IUnitOfWork unitOfWOrk, IMapper mapper) : base(unitOfWOrk, mapper)
        {

        }
    }
}
