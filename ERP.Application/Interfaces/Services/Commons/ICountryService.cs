using ERP.Domain.Entities.Commons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Commons
{
    public interface ICountryService
    {
        Task<Country> GetByIdAsync(int id);
        Task<IReadOnlyList<Country>> GetListAsync();
    }
}
