using ERP.Domain.Entities.Lists;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Repositories.Lists
{
    public interface ICityRepository : IGenericRepository<City>
    {
        Task<IReadOnlyList<City>> GetCitiesByStateIdAsync(int? stateId);
    }
}
