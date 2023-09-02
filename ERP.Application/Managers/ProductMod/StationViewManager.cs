using AspNetCoreHero.Results;
using AutoMapper;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Application.Interfaces.Services.ProductMod;
using ERP.Application.Interfaces.ViewManagers.ProductMod;
using ERP.Domain.Entities.ProductMod;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.Application.Specification.ProductMod.StationSpecifications;

namespace ERP.Application.Managers.ProductMod
{
    public class StationViewManager : ViewManager<Station, StationDTO>, IStationViewManager
    {
        private readonly IStationService _stationservice;
        private readonly IMapper _mapper;

        public StationViewManager(IStationService stationservice, IMapper mapper) : base(stationservice, mapper)
        {
            _stationservice = stationservice;
            _mapper = mapper;
        }
        public async Task<Result<IReadOnlyList<StationDTO>>> LoadAllWithFunctionalArea()
        {
            var stations = await _stationservice.FindWithSpecificationPattern(new StationWithFunctionalAreaSpecification());
            var stationsDTO = _mapper.Map<IReadOnlyList<StationDTO>>(stations);
            return Result<IReadOnlyList<StationDTO>>.Success(stationsDTO);
        }
    }
}
