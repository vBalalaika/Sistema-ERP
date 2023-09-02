using AspNetCoreHero.Results;
using ERP.Application.DTOs.Entities.Sales;
using ERP.Application.Specification;
using ERP.Domain.Entities.Sales;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.ViewManagers.Sales
{
    public interface IOrderDetailViewManager : IViewManager<OrderDetail, OrderDetailDTO>
    {
        Task<Result<IReadOnlyList<OrderDetailDTO>>> FindBySpecification(ISpecification<OrderDetail> specification);

        Task<Result<int>> UpdateOrderStateAsync(OrderDetailDTO dtoToUpdate, int orderStateId);

        //Task<Result<int>> UpdateRowOrderFromOrderDetailAsync(int orderDetailId, int i);

        //Task<Result<int>> UpdateRowOrderDragAndDropAsync(int orderDetailId, int newData);

    }
}