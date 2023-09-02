using AspNetCoreHero.Results;
using ERP.Application.DTOs.Entities.Purchases.PurchaseOrders;
using ERP.Application.Specification;
using ERP.Domain.Entities.Purchases.PurchaseOrders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.ViewManagers.Purchases.PurchaseOrders
{
    public interface IServicePODetailViewManager : IViewManager<ServicePODetail, ServicePODetailDTO>
    {
        Task<Result<IReadOnlyList<ServicePODetailDTO>>> FindBySpecification(ISpecification<ServicePODetail> specification);
    }
}
