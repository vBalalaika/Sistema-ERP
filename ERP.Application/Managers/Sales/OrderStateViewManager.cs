using AspNetCoreHero.Results;
using AutoMapper;
using ERP.Application.DTOs.Entities.Sales;
using ERP.Application.Interfaces.Services.Sales;
using ERP.Application.Interfaces.ViewManagers.Sales;
using ERP.Domain.Entities.Sales;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Managers.Sales
{
    public class OrderStateViewManager : IOrderStateViewManager
    {
        private readonly IOrderStateService _OrderStateService;
        private readonly IMapper _mapper;

        public OrderStateViewManager(IOrderStateService OrderStateService, IMapper mapper)
        {
            _OrderStateService = OrderStateService;
            _mapper = mapper;
        }

        public async Task<Result<OrderStateDTO>> GetByIdAsync(int id)
        {
            OrderState entity = await _OrderStateService.GetByIdAsync(id);
            OrderStateDTO entityDto = _mapper.Map<OrderStateDTO>(entity);
            return Result<OrderStateDTO>.Success(entityDto);
        }

        public async Task<Result<IReadOnlyList<OrderStateDTO>>> LoadAll()
        {
            IReadOnlyList<OrderState> entities = await _OrderStateService.GetListAsync();
            IReadOnlyList<OrderStateDTO> entitiesDtos = _mapper.Map<IReadOnlyList<OrderStateDTO>>(entities);
            return Result<IReadOnlyList<OrderStateDTO>>.Success(entitiesDtos);
        }
    }
}
