using AutoMapper;
using ERP.Application.DTOs.Entities.Lists;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services.Lists;
using ERP.Domain.Entities.Lists;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Services.Lists
{
    public class CityService : GenericService<City, CityDTO>, ICityService
    {
        public CityService(IUnitOfWork unitOfWOrk, IMapper mapper) : base(unitOfWOrk, mapper)
        {

        }

        public Task<IReadOnlyList<City>> GetCitiesByStateIdAsync(int? stateId)
        {
            var citiesByStateId = _unitOfWork.CityRepository.GetCitiesByStateIdAsync(stateId);
            return citiesByStateId;
        }
    }
}
