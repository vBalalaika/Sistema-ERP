using AspNetCoreHero.Results;
using AutoMapper;
using ERP.Application.DTOs.Entities.Commons;
using ERP.Application.Interfaces.Services.Commons;
using ERP.Application.Interfaces.ViewManagers.Commons;
using ERP.Application.Specification;
using ERP.Domain.Entities.Commons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Managers.Commons
{
    public class DollarExchangeRateViewManager : ViewManager<DollarExchangeRate, DollarExchangeRateDTO>, IDollarExchangeRateViewManager
    {
        private readonly IDollarExchangeRateService _dollarExchangeRateService;
        private readonly IMapper _mapper;

        public DollarExchangeRateViewManager(IDollarExchangeRateService dollarExchangeRateService, IMapper mapper) : base(dollarExchangeRateService, mapper)
        {
            _dollarExchangeRateService = dollarExchangeRateService;
            _mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<DollarExchangeRateDTO>>> FindBySpecification(ISpecification<DollarExchangeRate> specification)
        {
            var dollarExchange = await _dollarExchangeRateService.FindWithSpecificationPattern(specification);
            IReadOnlyList<DollarExchangeRateDTO> dollarExchangeDto = _mapper.Map<IReadOnlyList<DollarExchangeRateDTO>>(dollarExchange);
            return Result<IReadOnlyList<DollarExchangeRateDTO>>.Success(dollarExchangeDto);
        }
    }
}
