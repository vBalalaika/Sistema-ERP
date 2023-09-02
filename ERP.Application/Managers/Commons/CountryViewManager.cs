using AspNetCoreHero.Results;
using AutoMapper;
using ERP.Application.DTOs.Entities.Commons;
using ERP.Application.Interfaces.Services.Commons;
using ERP.Application.Interfaces.ViewManagers.Commons;
using ERP.Domain.Entities.Commons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Managers.Commons
{
    public class CountryViewManager : ICountryViewManager
    {
        private readonly ICountryService _countryservice;
        private readonly IMapper _mapper;

        public CountryViewManager(ICountryService countryservice, IMapper mapper)
        {
            _countryservice = countryservice;
            _mapper = mapper;
        }

        public async Task<Result<CountryDTO>> GetByIdAsync(int id)
        {
            Country entity = await _countryservice.GetByIdAsync(id);
            CountryDTO entityDto = _mapper.Map<CountryDTO>(entity);
            return Result<CountryDTO>.Success(entityDto);
        }

        public async Task<Result<IReadOnlyList<CountryDTO>>> LoadAll()
        {
            IReadOnlyList<Country> entities = await _countryservice.GetListAsync();
            IReadOnlyList<CountryDTO> entitiesDtos = _mapper.Map<IReadOnlyList<CountryDTO>>(entities);
            return Result<IReadOnlyList<CountryDTO>>.Success(entitiesDtos);
        }
    }
}
