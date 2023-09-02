using ERP.Domain.Entities.Sales;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Sales
{
    public interface IOrderStateService
    {
        Task<OrderState> GetByIdAsync(int id);
        Task<IReadOnlyList<OrderState>> GetListAsync();
    }
}
