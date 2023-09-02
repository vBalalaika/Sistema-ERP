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
    public class RelProviderProductViewManager : ViewManager<RelProviderProduct, RelProviderProductDTO>, IRelProviderProductViewManager
    {
        private readonly IRelProviderProductService _relProviderProductService;
        private readonly IMapper _mapper;

        public RelProviderProductViewManager(IRelProviderProductService relProviderProductService, IMapper mapper) : base(relProviderProductService, mapper)
        {
            _relProviderProductService = relProviderProductService;
            _mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<RelProviderProductDTO>>> FindBySpecification(ISpecification<RelProviderProduct> specification)
        {
            var products = await _relProviderProductService.FindWithSpecificationPattern(specification);
            IReadOnlyList<RelProviderProductDTO> prodDto = _mapper.Map<IReadOnlyList<RelProviderProductDTO>>(products);
            return Result<IReadOnlyList<RelProviderProductDTO>>.Success(prodDto);
        }
    }
}
