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
    public class WorkOrderHeaderViewManager : ViewManager<WorkOrderHeader, WorkOrderHeaderDTO>, IWorkOrderHeaderViewManager
    {
        private readonly IWorkOrderHeaderService _WorkOrderHeaderService;
        private readonly IMapper _mapper;

        public WorkOrderHeaderViewManager(IWorkOrderHeaderService WorkOrderHeaderService, IMapper mapper) : base(WorkOrderHeaderService, mapper)
        {
            _WorkOrderHeaderService = WorkOrderHeaderService;
            _mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<WorkOrderHeaderDTO>>> FindBySpecification(ISpecification<WorkOrderHeader> specification)
        {
            var WOHeaders = await _WorkOrderHeaderService.FindWithSpecificationPattern(specification);
            IReadOnlyList<WorkOrderHeaderDTO> WorkOrderHeadersDto = _mapper.Map<IReadOnlyList<WorkOrderHeaderDTO>>(WOHeaders);
            return Result<IReadOnlyList<WorkOrderHeaderDTO>>.Success(WorkOrderHeadersDto);
        }
    }
}
