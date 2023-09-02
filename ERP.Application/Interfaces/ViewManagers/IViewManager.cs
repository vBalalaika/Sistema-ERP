using AspNetCoreHero.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.ViewManagers
{
    public interface IViewManager<T, DTO> where T : class
    {
        Task<Result<IReadOnlyList<DTO>>> LoadAll();
        Task<Result<DTO>> GetByIdAsync(int id);
        Task<Result<int>> CreateAsync(DTO dtoToCreate);
        Task<Result<int>> UpdateAsync(DTO dtoToUpdate);
        Task<Result<int>> DeleteAsync(int id);
    }
}
