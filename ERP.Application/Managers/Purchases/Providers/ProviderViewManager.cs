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
    public class ProviderViewManager : ViewManager<Provider, ProviderDTO>, IProviderViewManager
    {
        private readonly IProviderService _providerService;
        private readonly IMapper _mapper;

        public ProviderViewManager(IProviderService providerService, IMapper mapper) : base(providerService, mapper)
        {
            _providerService = providerService;
            _mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<ProviderDTO>>> FindBySpecification(ISpecification<Provider> specification)
        {
            var providers = await _providerService.FindWithSpecificationPattern(specification);
            IReadOnlyList<ProviderDTO> providerDTOs = _mapper.Map<IReadOnlyList<ProviderDTO>>(providers);
            return Result<IReadOnlyList<ProviderDTO>>.Success(providerDTOs);
        }

    }
}
