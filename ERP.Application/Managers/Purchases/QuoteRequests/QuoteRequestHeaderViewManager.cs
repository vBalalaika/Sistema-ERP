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
    public class QuoteRequestHeaderViewManager : ViewManager<QuoteRequestHeader, QuoteRequestHeaderDTO>, IQuoteRequestHeaderViewManager
    {
        private readonly IQuoteRequestHeaderService _quoteRequestHeaderService;
        private readonly IMapper _mapper;

        public QuoteRequestHeaderViewManager(IQuoteRequestHeaderService quoteRequestHeaderService, IMapper mapper) : base(quoteRequestHeaderService, mapper)
        {
            _quoteRequestHeaderService = quoteRequestHeaderService;
            _mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<QuoteRequestHeaderDTO>>> FindBySpecification(ISpecification<QuoteRequestHeader> specification)
        {
            var quoteRequests = await _quoteRequestHeaderService.FindWithSpecificationPattern(specification);
            IReadOnlyList<QuoteRequestHeaderDTO> quoteRequestsDTO = _mapper.Map<IReadOnlyList<QuoteRequestHeaderDTO>>(quoteRequests);
            return Result<IReadOnlyList<QuoteRequestHeaderDTO>>.Success(quoteRequestsDTO);
        }
    }
}
