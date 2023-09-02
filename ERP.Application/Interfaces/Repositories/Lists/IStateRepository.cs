using ERP.Domain.Entities.Lists;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Repositories.Lists
{
    public interface IStateRepository : IGenericRepository<State>
    {
        Task<IReadOnlyList<State>> GetStatesByCountryIdAsync(int countryId);
    }
}
