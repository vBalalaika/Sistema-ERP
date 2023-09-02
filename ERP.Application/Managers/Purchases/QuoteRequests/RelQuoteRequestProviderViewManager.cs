using AspNetCoreHero.Results;
using AutoMapper;
using ERP.Application.DTOs.Entities.Purchases.QuoteRequests;
using ERP.Application.Interfaces.Services.Purchases.QuoteRequests;
using ERP.Application.Interfaces.ViewManagers.Purchases.QuoteRequests;
using ERP.Application.Specification;
using ERP.Domain.Entities.Purchases.QuoteRequests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Managers.Purchases.QuoteRequests
{
    public class RelQuoteRequestProviderViewManager : ViewManager<RelQuoteRequestProvider, RelQuoteRequestProviderDTO>, IRelQuoteRequestProviderViewManager
    {
        private readonly IRelQuoteRequestProviderService _relQuoteRequestProviderService;
        private readonly IMapper _mapper;

        public RelQuoteRequestProviderViewManager(IRelQuoteRequestProviderService relQuoteRequestProviderService, IMapper mapper) : base(relQuoteRequestProviderService, mapper)
        {
            _relQuoteRequestProviderService = relQuoteRequestProviderService;
            _mapper = mapper;
        }
        public async Task<Result<IReadOnlyList<RelQuoteRequestProviderDTO>>> FindBySpecification(ISpecification<RelQuoteRequestProvider> specification)
        {
            var relQuoteRequestProviders = await _relQuoteRequestProviderService.FindWithSpecificationPattern(specification);
            IReadOnlyList<RelQuoteRequestProviderDTO> relQuoteRequestProvidersDTO = _mapper.Map<IReadOnlyList<RelQuoteRequestProviderDTO>>(relQuoteRequestProviders);
            return Result<IReadOnlyList<RelQuoteRequestProviderDTO>>.Success(relQuoteRequestProvidersDTO);
        }
    }
}
