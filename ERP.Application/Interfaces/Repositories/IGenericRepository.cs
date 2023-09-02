using ERP.Application.Specification;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Repositories
{
    public interface IGenericRepository<T>
    {
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdFullAsync(int id);
        Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize);
        Task<T> CreateAsync(T entity);
        void Update(T entity);
        Task DeleteAsync(int id);
        Task<IReadOnlyList<T>> GetListAsync();

        Task<IQueryable<T>> FindAsync(ISpecification<T> specification = null);
    }
}
