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
    public class OrderDetailViewManager : ViewManager<OrderDetail, OrderDetailDTO>, IOrderDetailViewManager
    {
        private readonly IOrderDetailService _OrderDetailService;
        private readonly IMapper _mapper;

        public OrderDetailViewManager(IOrderDetailService OrderDetailService, IMapper mapper) : base(OrderDetailService, mapper)
        {
            _OrderDetailService = OrderDetailService;
            _mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<OrderDetailDTO>>> FindBySpecification(ISpecification<OrderDetail> specification)
        {
            var Orders = await _OrderDetailService.FindWithSpecificationPattern(specification);
            IReadOnlyList<OrderDetailDTO> OrdersDto = _mapper.Map<IReadOnlyList<OrderDetailDTO>>(Orders);
            return Result<IReadOnlyList<OrderDetailDTO>>.Success(OrdersDto);
        }

        public async Task<Result<int>> UpdateOrderStateAsync(OrderDetailDTO dtoToUpdate, int orderStateId)
        {
            int result = await _OrderDetailService.UpdateOrderState(dtoToUpdate, orderStateId);
            return Result<int>.Success(result);
        }

        //public async Task<Result<int>> UpdateRowOrderFromOrderDetailAsync(int orderDetailId, int i)
        //{
        //    var result = await _OrderDetailService.UpdateRowOrderFromOrderDetail(orderDetailId, i);
        //    return Result<int>.Success(result);
        //}

        //public async Task<Result<int>> UpdateRowOrderDragAndDropAsync(int orderDetailId, int newData)
        //{
        //    var result = await _OrderDetailService.UpdateRowOrderDragAndDrop(orderDetailId, newData);
        //    return Result<int>.Success(result);
        //}

    }
}