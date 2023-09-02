using AspNetCoreHero.Results;
using AutoMapper;
using ERP.Application.DTOs.Entities.Production;
using ERP.Application.Exceptions;
using ERP.Application.Interfaces.Services.Production;
using ERP.Application.Interfaces.ViewManagers.Production;
using ERP.Application.Specification;
using ERP.Domain.Entities.Production;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Managers.Production
{
    public class WorkOrderItemViewManager : ViewManager<WorkOrderItem, WorkOrderItemDTO>, IWorkOrderItemViewManager
    {
        private readonly IWorkOrderItemService _WorkOrderItemService;
        private readonly IMapper _mapper;

        public WorkOrderItemViewManager(IWorkOrderItemService WorkOrderItemService, IMapper mapper) : base(WorkOrderItemService, mapper)
        {
            _WorkOrderItemService = WorkOrderItemService;
            _mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<WorkOrderItemDTO>>> FindBySpecification(ISpecification<WorkOrderItem> specification)
        {
            var WOrderItems = await _WorkOrderItemService.FindWithSpecificationPattern(specification);
            IReadOnlyList<WorkOrderItemDTO> WOrderItemsDto = _mapper.Map<IReadOnlyList<WorkOrderItemDTO>>(WOrderItems);
            return Result<IReadOnlyList<WorkOrderItemDTO>>.Success(WOrderItemsDto);
        }

        public Result<int> DeleteByWorkOrderIdAsync(int workOrderId)
        {
            try
            {
                var idDeleted = _WorkOrderItemService.DeleteByWorkOrderIdAsync(workOrderId);
                return Result<int>.Success(idDeleted);
            }
            catch (ElementNotFoundException ex)
            {
                return Result<int>.Fail(ex.Message);
            }
        }
    }
}