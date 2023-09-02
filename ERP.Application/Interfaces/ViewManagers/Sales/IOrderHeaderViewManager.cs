using AspNetCoreHero.Results;
using ERP.Application.DTOs.Entities.Sales;
using ERP.Application.Specification;
using ERP.Domain.Entities.Sales;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.ViewManagers.Sales
{
    public interface IOrderHeaderViewManager : IViewManager<OrderHeader, OrderHeaderDTO>
    {
        Task<Result<IReadOnlyList<OrderHeaderDTO>>> FindBySpecification(ISpecification<OrderHeader> specification);

        Task<Result<int>> UpdateOrderStateAsync(OrderHeaderDTO dtoToUpdate, int orderStateId);

        Task<Result<int>> UpdateBilledStateAsync(OrderHeaderDTO dtoToUpdate, int billedState);

        Task<Result<int>> UpdateSomePropertiesAsync(OrderHeaderDTO dtoToUpdate);
    }
}
