using ERP.Application.DTOs.Entities.Sales;
using ERP.Domain.Entities.Sales;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Sales
{
    public interface IOrderHeaderService : IGenericService<OrderHeader, OrderHeaderDTO>
    {
        Task<int> UpdateOrderState(OrderHeaderDTO dtoToUpdate, int orderStateId);
        Task<int> UpdateBilledState(OrderHeaderDTO dtoToUpdate, int billedState);
        Task<int> UpdateSomeProperties(OrderHeaderDTO dtoToUpdate);
    }
}
