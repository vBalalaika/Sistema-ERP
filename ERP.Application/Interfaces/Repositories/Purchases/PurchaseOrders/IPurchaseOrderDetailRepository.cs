using ERP.Domain.Entities.Purchases.PurchaseOrders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Repositories.Purchases.PurchaseOrders
{
    public interface IPurchaseOrderDetailRepository : IGenericRepository<PurchaseOrderDetail>
    {
        Task<IReadOnlyList<PurchaseOrderDetail>> GetAll();
    }
}
