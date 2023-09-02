using AspNetCoreHero.Results;
using AutoMapper;
using ERP.Application.DTOs.Entities.Sales;
using ERP.Application.Interfaces.Services.Sales;
using ERP.Application.Interfaces.ViewManagers.Sales;
using ERP.Application.Specification;
using ERP.Domain.Entities.Sales;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Managers.Sales
{
    public class OrderHeaderViewManager : ViewManager<OrderHeader, OrderHeaderDTO>, IOrderHeaderViewManager
    {
        private readonly IOrderHeaderService _OrderHeaderService;
        private readonly IMapper _mapper;

        public OrderHeaderViewManager(IOrderHeaderService OrderHeaderService, IMapper mapper) : base(OrderHeaderService, mapper)
        {
            _OrderHeaderService = OrderHeaderService;
            _mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<OrderHeaderDTO>>> FindBySpecification(ISpecification<OrderHeader> specification)
        {
            var Orders = await _OrderHeaderService.FindWithSpecificationPattern(specification);
            IReadOnlyList<OrderHeaderDTO> OrdersDto = _mapper.Map<IReadOnlyList<OrderHeaderDTO>>(Orders);
            return Result<IReadOnlyList<OrderHeaderDTO>>.Success(OrdersDto);
        }

        public async Task<Result<int>> UpdateOrderStateAsync(OrderHeaderDTO dtoToUpdate, int orderStateId)
        {
            int result = await _OrderHeaderService.UpdateOrderState(dtoToUpdate, orderStateId);
            return Result<int>.Success(result);
        }

        public async Task<Result<int>> UpdateBilledStateAsync(OrderHeaderDTO dtoToUpdate, int billedState)
        {
            int result = await _OrderHeaderService.UpdateBilledState(dtoToUpdate, billedState);
            return Result<int>.Success(result);
        }

        public async Task<Result<int>> UpdateSomePropertiesAsync(OrderHeaderDTO dtoToUpdate)
        {
            return Result<int>.Success(await _OrderHeaderService.UpdateSomeProperties(dtoToUpdate));
        }
    }
}
