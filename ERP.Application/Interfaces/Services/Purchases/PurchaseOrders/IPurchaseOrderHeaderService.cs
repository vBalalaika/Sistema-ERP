using ERP.Application.DTOs.Entities.Purchases.PurchaseOrders;
using ERP.Domain.Entities.Purchases.PurchaseOrders;

namespace ERP.Application.Interfaces.Services.Purchases.PurchaseOrders
{
    public interface IPurchaseOrderHeaderService : IGenericService<PurchaseOrderHeader, PurchaseOrderHeaderDTO>
    {
    }
}
