using AspNetCoreHero.Results;
using ERP.Application.DTOs.Entities.Commons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.ViewManagers.Commons
{
    public interface IIVAConditionViewManager
    {
        Task<Result<IVAConditionDTO>> GetByIdAsync(int id);
        Task<Result<IReadOnlyList<IVAConditionDTO>>> LoadAll();
    }
}
