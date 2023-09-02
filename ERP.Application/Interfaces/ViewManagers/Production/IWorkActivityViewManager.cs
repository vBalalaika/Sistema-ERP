using AspNetCoreHero.Results;
using ERP.Application.DTOs.Entities.Production;
using ERP.Application.Specification;
using ERP.Domain.Entities.Production;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.ViewManagers.Production
{
    public interface IWorkActivityViewManager : IViewManager<WorkActivity, WorkActivityDTO>
    {
        Task<Result<bool>> UpdateListAsync(IList<WorkActivityDTO> entityListToUpdate);
        Task<Result<IReadOnlyList<WorkActivityDTO>>> FindBySpecification(ISpecification<WorkActivity> specification);
    }
}
