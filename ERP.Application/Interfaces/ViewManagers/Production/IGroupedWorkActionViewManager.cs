using AspNetCoreHero.Results;
using ERP.Application.DTOs.Entities.Production;
using ERP.Application.Specification;
using ERP.Domain.Entities.Production;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.ViewManagers.Production
{
    public interface IGroupedWorkActionViewManager : IViewManager<GroupedWorkAction, GroupedWorkActionDTO>
    {
        Task<Result<IReadOnlyList<GroupedWorkActionDTO>>> FindBySpecification(ISpecification<GroupedWorkAction> specification);
    }
}
