using ERP.Domain.Entities.Purchases.MissingProducts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Purchases.MissingProducts
{
    public interface IPurchaseStateService
    {
        Task<PurchaseState> GetByIdAsync(int id);
        Task<IReadOnlyList<PurchaseState>> GetListAsync();
    }
}
