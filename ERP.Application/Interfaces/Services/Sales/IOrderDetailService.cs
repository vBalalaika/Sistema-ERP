using ERP.Application.DTOs.Entities.Sales;
using ERP.Domain.Entities.Sales;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Sales
{
    public interface IOrderDetailService : IGenericService<OrderDetail, OrderDetailDTO>
    {
        Task<int> UpdateOrderState(OrderDetailDTO dtoToUpdate, int orderStateId);

        //Task<int> UpdateRowOrderFromOrderDetail(int orderDetailId, int i);

        //Task<int> UpdateRowOrderDragAndDrop(int orderDetailId, int newData);

    }
}