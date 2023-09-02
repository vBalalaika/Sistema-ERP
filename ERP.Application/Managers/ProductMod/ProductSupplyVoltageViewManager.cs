using AspNetCoreHero.Results;
using AutoMapper;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Application.Interfaces.Services.ProductMod;
using ERP.Application.Interfaces.ViewManagers.ProductMod;
using ERP.Application.Specification;
using ERP.Domain.Entities.ProductMod;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Managers.ProductMod
{
    public class ProductSupplyVoltageViewManager : ViewManager<ProductSupplyVoltage, ProductSupplyVoltageDTO>, IProductSupplyVoltageViewManager
    {
        private readonly IProductSupplyVoltageService _productSupplyVoltageService;
        private readonly IMapper _mapper;

        public ProductSupplyVoltageViewManager(IProductSupplyVoltageService productSupplyVoltageService, IMapper mapper) : base(productSupplyVoltageService, mapper)
        {
            _productSupplyVoltageService = productSupplyVoltageService;
            _mapper = mapper;
        }
        public async Task<Result<IReadOnlyList<ProductSupplyVoltageDTO>>> FindBySpecification(ISpecification<ProductSupplyVoltage> specification)
        {
            var productSupplyVoltages = await _productSupplyVoltageService.FindWithSpecificationPattern(specification);
            IReadOnlyList<ProductSupplyVoltageDTO> productSupplyVoltagesDTO = _mapper.Map<IReadOnlyList<ProductSupplyVoltageDTO>>(productSupplyVoltages);
            return Result<IReadOnlyList<ProductSupplyVoltageDTO>>.Success(productSupplyVoltagesDTO);
        }
    }
}
