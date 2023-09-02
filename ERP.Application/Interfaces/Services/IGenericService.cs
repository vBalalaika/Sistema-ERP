using ERP.Application.Specification;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services
{
    public interface IGenericService<T, TDTO>
    {
        Task<int> CreateAsync(TDTO entityToCreateDTO);
        Task<T> GetByIdAsync(int id);
        Task<TDTO> GetByIdFullAsync(int id);
        Task<int> UpdateAsync(TDTO entityToUpdate);
        Task<int> DeleteAsync(int id);
        Task<IReadOnlyList<T>> GetListAsync();
        Task<IEnumerable<T>> FindWithSpecificationPattern(ISpecification<T> specification = null);
    }
}
