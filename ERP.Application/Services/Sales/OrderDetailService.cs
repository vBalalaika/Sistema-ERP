using AutoMapper;
using ERP.Application.DTOs.Entities.Sales;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services.Sales;
using ERP.Domain.Entities.Sales;
using System.Threading.Tasks;

namespace ERP.Application.Services.Sales
{
    public class OrderDetailService : GenericService<OrderDetail, OrderDetailDTO>, IOrderDetailService
    {
        public OrderDetailService(IUnitOfWork unitOfWOrk, IMapper mapper) : base(unitOfWOrk, mapper)
        {
        }

        public async Task<int> UpdateOrderState(OrderDetailDTO dtoToUpdate, int orderStateId)
        {
            OrderDetail entity = MapToEntity(dtoToUpdate);

            entity.OrderStateId = orderStateId;

            _unitOfWork.GetRepository<OrderDetail>().Update(entity);

            await _unitOfWork.Commit();
            return entity.Id;
        }

        //public async Task<int> UpdateRowOrderFromOrderDetail(int orderDetailId, int i)
        //{
        //    var orderDetail = GetByIdAsync(orderDetailId);

        //    orderDetail.Result.rowOrder = i;

        //    _unitOfWork.GetRepository<OrderDetail>().Update(orderDetail.Result);

        //    await _unitOfWork.Commit();
        //    return orderDetail.Id;

        //}

        //public async Task<int> UpdateRowOrderDragAndDrop(int orderDetailId, int newData)
        //{
        //    var orderDetail = GetByIdAsync(orderDetailId);

        //    orderDetail.Result.rowOrder = newData;

        //    _unitOfWork.GetRepository<OrderDetail>().Update(orderDetail.Result);

        //    await _unitOfWork.Commit();
        //    return orderDetail.Id;

        //}

    }
}