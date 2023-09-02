using AspNetCoreHero.Results;
using AutoMapper;
using ERP.Application.DTOs.Entities.Purchases.Providers;
using ERP.Application.Interfaces.Services.Purchases.Providers;
using ERP.Application.Interfaces.ViewManagers.Purchases.Providers;
using ERP.Application.Specification;
using ERP.Domain.Entities.Purchases.Providers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Managers.Purchases.Providers
{
    public class RelProviderStationViewManager : ViewManager<RelProviderStation, RelProviderStationDTO>, IRelProviderStationViewManager
    {
        private readonly IRelProviderStationService _relProviderStationService;
        private readonly IMapper _mapper;

        public RelProviderStationViewManager(IRelProviderStationService relProviderStationService, IMapper mapper) : base(relProviderStationService, mapper)
        {
            _relProviderStationService = relProviderStationService;
            _mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<RelProviderStationDTO>>> FindBySpecification(ISpecification<RelProviderStation> specification)
        {
            var products = await _relProviderStationService.FindWithSpecificationPattern(specification);
            IReadOnlyList<RelProviderStationDTO> prodDto = _mapper.Map<IReadOnlyList<RelProviderStationDTO>>(products);
            return Result<IReadOnlyList<RelProviderStationDTO>>.Success(prodDto);
        }
    }
}
