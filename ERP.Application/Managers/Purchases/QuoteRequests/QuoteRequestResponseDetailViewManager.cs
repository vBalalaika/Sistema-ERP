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
    public class QuoteRequestResponseDetailViewManager : ViewManager<QuoteRequestResponseDetail, QuoteRequestResponseDetailDTO>, IQuoteRequestResponseDetailViewManager
    {
        private readonly IQuoteRequestResponseDetailService _quoteRequestResponseDetailService;
        private readonly IMapper _mapper;

        public QuoteRequestResponseDetailViewManager(IQuoteRequestResponseDetailService quoteRequestResponseDetailService, IMapper mapper) : base(quoteRequestResponseDetailService, mapper)
        {
            _quoteRequestResponseDetailService = quoteRequestResponseDetailService;
            _mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<QuoteRequestResponseDetailDTO>>> FindBySpecification(ISpecification<QuoteRequestResponseDetail> specification)
        {
            var quoteRequestResponses = await _quoteRequestResponseDetailService.FindWithSpecificationPattern(specification);
            IReadOnlyList<QuoteRequestResponseDetailDTO> quoteRequestResponsesDTO = _mapper.Map<IReadOnlyList<QuoteRequestResponseDetailDTO>>(quoteRequestResponses);
            return Result<IReadOnlyList<QuoteRequestResponseDetailDTO>>.Success(quoteRequestResponsesDTO);
        }
    }
}
