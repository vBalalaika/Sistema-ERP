using AspNetCoreHero.Results;
using ERP.Application.DTOs.Entities.Sales;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.ViewManagers.Sales
{
    public interface IOrderStateViewManager
    {
        Task<Result<OrderStateDTO>> GetByIdAsync(int id);
        Task<Result<IReadOnlyList<OrderStateDTO>>> LoadAll();
    }
}
