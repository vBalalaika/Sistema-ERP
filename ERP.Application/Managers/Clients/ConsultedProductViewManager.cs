using AspNetCoreHero.Results;
using AutoMapper;
using ERP.Application.DTOs.Entities.Clients;
using ERP.Application.Interfaces.Services.Clients;
using ERP.Application.Interfaces.ViewManagers.Clients;
using ERP.Application.Specification;
using ERP.Domain.Entities.Clients;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Managers.Clients
{
    public class ConsultedProductViewManager : ViewManager<ConsultedProduct, ConsultedProductDTO>, IConsultedProductViewManager
    {
        private readonly IConsultedProductService _consultedProductService;
        private readonly IMapper _mapper;

        public ConsultedProductViewManager(IConsultedProductService consultedProductService, IMapper mapper) : base(consultedProductService, mapper)
        {
            _consultedProductService = consultedProductService;
            _mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<ConsultedProductDTO>>> FindBySpecification(ISpecification<ConsultedProduct> specification)
        {
            var consultedProducts = await _consultedProductService.FindWithSpecificationPattern(specification);
            IReadOnlyList<ConsultedProductDTO> consultedProductsDto = _mapper.Map<IReadOnlyList<ConsultedProductDTO>>(consultedProducts);
            return Result<IReadOnlyList<ConsultedProductDTO>>.Success(consultedProductsDto);
        }
    }
}
