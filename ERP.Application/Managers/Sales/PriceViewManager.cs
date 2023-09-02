using AspNetCoreHero.Results;
using AutoMapper;
using ERP.Application.DTOs.Entities.Sales;
using ERP.Application.Interfaces.Services.Sales;
using ERP.Application.Interfaces.ViewManagers.Sales;
using ERP.Application.Specification;
using ERP.Domain.Entities.Sales;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Managers.Sales
{
    public class PriceViewManager : ViewManager<Price, PriceDTO>, IPriceViewManager
    {
        private readonly IPriceService _priceService;
        private readonly IMapper _mapper;

        public PriceViewManager(IPriceService priceService, IMapper mapper) : base(priceService, mapper)
        {
            _priceService = priceService;
            _mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<PriceDTO>>> FindBySpecification(ISpecification<Price> specification)
        {
            var prices = await _priceService.FindWithSpecificationPattern(specification);
            IReadOnlyList<PriceDTO> priceDTOs = _mapper.Map<IReadOnlyList<PriceDTO>>(prices);
            return Result<IReadOnlyList<PriceDTO>>.Success(priceDTOs);
        }
    }
}
