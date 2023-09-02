using AspNetCoreHero.Results;
using AutoMapper;
using ERP.Application.DTOs.Entities.Production;
using ERP.Application.Interfaces.Services.Production;
using ERP.Application.Interfaces.ViewManagers.Production;
using ERP.Application.Specification;
using ERP.Domain.Entities.Production;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Managers.Production
{
    public class GroupedWorkActionViewManager : ViewManager<GroupedWorkAction, GroupedWorkActionDTO>, IGroupedWorkActionViewManager
    {
        private readonly IGroupedWorkActionService _groupedWorkActionService;
        private readonly IMapper _mapper;

        public GroupedWorkActionViewManager(IGroupedWorkActionService groupedWorkActionService, IMapper mapper) : base(groupedWorkActionService, mapper)
        {
            _groupedWorkActionService = groupedWorkActionService;
            _mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<GroupedWorkActionDTO>>> FindBySpecification(ISpecification<GroupedWorkAction> specification)
        {
            var gWActions = await _groupedWorkActionService.FindWithSpecificationPattern(specification);
            IReadOnlyList<GroupedWorkActionDTO> gWorkActionsDto = _mapper.Map<IReadOnlyList<GroupedWorkActionDTO>>(gWActions);
            return Result<IReadOnlyList<GroupedWorkActionDTO>>.Success(gWorkActionsDto);
        }
    }
}
