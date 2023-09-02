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
    public class WorkOrderViewManager : ViewManager<WorkOrder, WorkOrderDTO>, IWorkOrderViewManager
    {
        private readonly IWorkOrderService _WorkOrderService;
        private readonly IMapper _mapper;

        public WorkOrderViewManager(IWorkOrderService WorkOrderService, IMapper mapper) : base(WorkOrderService, mapper)
        {
            _WorkOrderService = WorkOrderService;
            _mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<WorkOrderDTO>>> FindBySpecification(ISpecification<WorkOrder> specification)
        {
            var WOrders = await _WorkOrderService.FindWithSpecificationPattern(specification);
            IReadOnlyList<WorkOrderDTO> WorkOrdersDto = _mapper.Map<IReadOnlyList<WorkOrderDTO>>(WOrders);
            return Result<IReadOnlyList<WorkOrderDTO>>.Success(WorkOrdersDto);
        }
    }
}
