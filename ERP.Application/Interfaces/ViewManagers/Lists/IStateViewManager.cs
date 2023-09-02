using AspNetCoreHero.Results;
using ERP.Application.DTOs.Entities.Lists;
using ERP.Application.Specification;
using ERP.Domain.Entities.Lists;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.ViewManagers.Lists
{
    public interface IStateViewManager : IViewManager<State, StateDTO>
    {
        Task<Result<IReadOnlyList<StateDTO>>> FindBySpecification(ISpecification<State> specification);
        Task<IReadOnlyList<State>> GetStatesByCountryIdAsync(int countryId);
    }
}
