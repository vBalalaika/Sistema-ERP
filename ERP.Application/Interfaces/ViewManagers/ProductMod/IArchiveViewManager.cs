using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Application.Specification;
using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Interfaces.ViewManagers.ProductMod
{
    public interface IArchiveViewManager : IViewManager<Archive, ArchiveDTO>
    {
        Task<Result<IReadOnlyList<ArchiveDTO>>> FindBySpecification(ISpecification<Archive> specification);
    }
}
