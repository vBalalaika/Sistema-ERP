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
    public class QuoteRequestDetailViewManager : ViewManager<QuoteRequestDetail, QuoteRequestDetailDTO>, IQuoteRequestDetailViewManager
    {
        private readonly IQuoteRequestDetailService _quoteRequestDetailService;
        private readonly IMapper _mapper;

        public QuoteRequestDetailViewManager(IQuoteRequestDetailService quoteRequestDetailService, IMapper mapper) : base(quoteRequestDetailService, mapper)
        {
            _quoteRequestDetailService = quoteRequestDetailService;
            _mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<QuoteRequestDetailDTO>>> FindBySpecification(ISpecification<QuoteRequestDetail> specification)
        {
            var quoteRequests = await _quoteRequestDetailService.FindWithSpecificationPattern(specification);
            IReadOnlyList<QuoteRequestDetailDTO> quoteRequestsDTO = _mapper.Map<IReadOnlyList<QuoteRequestDetailDTO>>(quoteRequests);
            return Result<IReadOnlyList<QuoteRequestDetailDTO>>.Success(quoteRequestsDTO);
        }
    }
}
