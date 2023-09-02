using ERP.Application.DTOs.Entities.Purchases.PurchaseOrders;
using ERP.Domain.Entities.Purchases.PurchaseOrders;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Purchases.PurchaseOrders
{
    public interface IPurchaseOrderDetailService : IGenericService<PurchaseOrderDetail, PurchaseOrderDetailDTO>
    {
        Task<int> UpdateDataOnReturnItemAsync(int podId);
    }
}
