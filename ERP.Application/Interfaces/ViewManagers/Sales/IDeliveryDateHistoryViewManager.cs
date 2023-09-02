using AspNetCoreHero.Results;
using ERP.Application.DTOs.Entities.Sales;
using ERP.Application.Specification;
using ERP.Domain.Entities.Sales;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.ViewManagers.Sales
{
    public interface IDeliveryDateHistoryViewManager : IViewManager<DeliveryDateHistory, DeliveryDateHistoryDTO>
    {
        Task<Result<IReadOnlyList<DeliveryDateHistoryDTO>>> FindBySpecification(ISpecification<DeliveryDateHistory> specification);
    }
}
