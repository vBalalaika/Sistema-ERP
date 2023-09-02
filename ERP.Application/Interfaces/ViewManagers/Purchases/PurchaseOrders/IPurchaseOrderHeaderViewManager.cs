using AspNetCoreHero.Results;
using ERP.Application.DTOs.Entities.Purchases.PurchaseOrders;
using ERP.Application.Specification;
using ERP.Domain.Entities.Purchases.PurchaseOrders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.ViewManagers.Purchases.PurchaseOrders
{
    public interface IPurchaseOrderHeaderViewManager : IViewManager<PurchaseOrderHeader, PurchaseOrderHeaderDTO>
    {
        Task<Result<IReadOnlyList<PurchaseOrderHeaderDTO>>> FindBySpecification(ISpecification<PurchaseOrderHeader> specification);
    }
}
