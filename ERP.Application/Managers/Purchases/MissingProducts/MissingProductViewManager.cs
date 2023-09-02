using AspNetCoreHero.Results;
using AutoMapper;
using ERP.Application.DTOs.Entities.Purchases.MissingProducts;
using ERP.Application.Interfaces.Services.Purchases.MissingProducts;
using ERP.Application.Interfaces.ViewManagers.Purchases.MissingProducts;
using ERP.Application.Specification;
using ERP.Domain.Entities.Purchases.MissingProducts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Managers.Purchases.MissingProducts
{
    public class MissingProductViewManager : ViewManager<MissingProduct, MissingProductDTO>, IMissingProductViewManager
    {
        private readonly IMissingProductService _missingProductService;
        private readonly IMapper _mapper;

        public MissingProductViewManager(IMissingProductService missingProductService, IMapper mapper) : base(missingProductService, mapper)
        {
            _missingProductService = missingProductService;
            _mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<MissingProductDTO>>> FindWithAllSpecification(ISpecification<MissingProduct> specification)
        {
            var missingProducts = await _missingProductService.FindWithSpecificationPattern(specification);
            IReadOnlyList<MissingProductDTO> missingProductDto = _mapper.Map<IReadOnlyList<MissingProductDTO>>(missingProducts);
            return Result<IReadOnlyList<MissingProductDTO>>.Success(missingProductDto);
        }
    }
}
