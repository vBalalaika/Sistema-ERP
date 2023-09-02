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
    public class WorkActionViewManager : ViewManager<WorkAction, WorkActionDTO>, IWorkActionViewManager
    {
        private readonly IWorkActionService _WorkActionService;
        private readonly IMapper _mapper;

        public WorkActionViewManager(IWorkActionService WorkActionService, IMapper mapper) : base(WorkActionService, mapper)
        {
            _WorkActionService = WorkActionService;
            _mapper = mapper;
        }


        public async Task<Result<IReadOnlyList<WorkActionDTO>>> FindBySpecification(ISpecification<WorkAction> specification)
        {
            var WActions = await _WorkActionService.FindWithSpecificationPattern(specification);
            IReadOnlyList<WorkActionDTO> WorkActionsDto = _mapper.Map<IReadOnlyList<WorkActionDTO>>(WActions);
            return Result<IReadOnlyList<WorkActionDTO>>.Success(WorkActionsDto);
        }
    }
}
