using ERP.Application.DTOs.Entities.Lists;
using ERP.Domain.Entities.Lists;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Lists
{
    public interface IStateService : IGenericService<State, StateDTO>
    {
        Task<IReadOnlyList<State>> GetStatesByCountryIdAsync(int countryId);
    }
}
