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
    public class QuoteRequestResponseHeaderViewManager : ViewManager<QuoteRequestResponseHeader, QuoteRequestResponseHeaderDTO>, IQuoteRequestResponseHeaderViewManager
    {
        private readonly IQuoteRequestResponseHeaderService _quoteRequestResponseHeaderService;
        private readonly IMapper _mapper;

        public QuoteRequestResponseHeaderViewManager(IQuoteRequestResponseHeaderService quoteRequestResponseHeaderService, IMapper mapper) : base(quoteRequestResponseHeaderService, mapper)
        {
            _quoteRequestResponseHeaderService = quoteRequestResponseHeaderService;
            _mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<QuoteRequestResponseHeaderDTO>>> FindBySpecification(ISpecification<QuoteRequestResponseHeader> specification)
        {
            var quoteRequestResponses = await _quoteRequestResponseHeaderService.FindWithSpecificationPattern(specification);
            IReadOnlyList<QuoteRequestResponseHeaderDTO> quoteRequestResponsesDTO = _mapper.Map<IReadOnlyList<QuoteRequestResponseHeaderDTO>>(quoteRequestResponses);
            return Result<IReadOnlyList<QuoteRequestResponseHeaderDTO>>.Success(quoteRequestResponsesDTO);
        }
    }
}
