using AspNetCoreHero.Results;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Domain.Entities.ProductMod;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.ViewManagers.ProductMod
{
    public interface IFunctionalAreaViewManager : IViewManager<FunctionalArea, FunctionalAreaDTO>
    {
        Task<Result<IReadOnlyList<FunctionalAreaDTO>>> GetAllFunctionalAreas();
    }
}