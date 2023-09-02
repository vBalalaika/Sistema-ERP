using ERP.Domain.Entities.Purchases.PurchaseOrders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Repositories.Purchases.PurchaseOrders
{
    public interface IServicePODetailRepository : IGenericRepository<ServicePODetail>
    {
        Task<IReadOnlyList<ServicePODetail>> GetAll();
    }
}
