using AspNetCoreHero.Results;
using ERP.Application.DTOs.Entities.Purchases.MissingProducts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.ViewManagers.Purchases.MissingProducts
{
    public interface IPurchaseStateViewManager
    {
        Task<Result<PurchaseStateDTO>> GetByIdAsync(int id);
        Task<Result<IReadOnlyList<PurchaseStateDTO>>> LoadAll();
    }
}
