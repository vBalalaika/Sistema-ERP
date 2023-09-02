using AspNetCoreHero.Results;
using ERP.Application.DTOs.Entities.Lists;
using ERP.Application.Specification;
using ERP.Domain.Entities.Lists;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.ViewManagers.Lists
{
    public interface ICityViewManager : IViewManager<City, CityDTO>
    {
        Task<Result<IReadOnlyList<CityDTO>>> FindWithSpecification(ISpecification<City> specification);
        Task<IReadOnlyList<City>> GetCitiesByStateIdAsync(int? stateId);
    }
}
