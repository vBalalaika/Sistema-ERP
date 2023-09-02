using AspNetCoreHero.Results;
using ERP.Application.DTOs.Entities.Commons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.ViewManagers.Commons
{
    public interface IOperationStateViewManager
    {
        Task<Result<OperationStateDTO>> GetByIdAsync(int id);
        Task<Result<IReadOnlyList<OperationStateDTO>>> LoadAll();
    }
}
