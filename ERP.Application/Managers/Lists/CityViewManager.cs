using AspNetCoreHero.Results;
using AutoMapper;
using ERP.Application.DTOs.Entities.Lists;
using ERP.Application.Interfaces.Services.Lists;
using ERP.Application.Interfaces.ViewManagers.Lists;
using ERP.Application.Specification;
using ERP.Domain.Entities.Lists;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Managers.Lists
{
    public class CityViewManager : ViewManager<City, CityDTO>, ICityViewManager
    {
        private readonly ICityService _cityservice;
        private readonly IMapper _mapper;

        public CityViewManager(ICityService cityservice, IMapper mapper, IStateService stateService) : base(cityservice, mapper)
        {
            _cityservice = cityservice;
            _mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<CityDTO>>> FindWithSpecification(ISpecification<City> specification)
        {
            var cities = await _cityservice.FindWithSpecificationPattern(specification);
            IReadOnlyList<CityDTO> cityDTOs = _mapper.Map<IReadOnlyList<CityDTO>>(cities);
            return Result<IReadOnlyList<CityDTO>>.Success(cityDTOs);
        }

        public async Task<IReadOnlyList<City>> GetCitiesByStateIdAsync(int? stateId)
        {
            var citiesByStateId = await _cityservice.GetCitiesByStateIdAsync(stateId);
            return citiesByStateId;
        }
    }
}
