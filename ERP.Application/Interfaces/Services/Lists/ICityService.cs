using ERP.Application.DTOs.Entities.Lists;
using ERP.Domain.Entities.Lists;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Lists
{
    public interface ICityService : IGenericService<City, CityDTO>
    {
        Task<IReadOnlyList<City>> GetCitiesByStateIdAsync(int? stateId);
    }
}
