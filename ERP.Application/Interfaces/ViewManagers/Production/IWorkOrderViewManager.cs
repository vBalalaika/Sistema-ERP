using AspNetCoreHero.Results;
using ERP.Application.DTOs.Entities.Production;
using ERP.Application.Specification;
using ERP.Domain.Entities.Production;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.ViewManagers.Production
{
    public interface IWorkOrderViewManager : IViewManager<WorkOrder, WorkOrderDTO>
    {
        Task<Result<IReadOnlyList<WorkOrderDTO>>> FindBySpecification(ISpecification<WorkOrder> specification);
    }
}
