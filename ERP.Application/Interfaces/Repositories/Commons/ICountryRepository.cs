using ERP.Domain.Entities.Commons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Repositories.Commons
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
        Task<IReadOnlyList<Country>> GetListActiveAsync();
    }
}
