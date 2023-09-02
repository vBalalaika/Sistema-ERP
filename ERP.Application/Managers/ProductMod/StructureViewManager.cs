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
    public class StructureViewManager : ViewManager<Structure, StructureDTO>, IStructureViewManager
    {
        private readonly IStructureService _structureservice;
        private readonly IProductService _productservice;
        private readonly IMapper _mapper;

        public StructureViewManager(IStructureService structureservice, IProductService productservice, IMapper mapper) : base(structureservice, mapper)
        {
            _structureservice = structureservice;
            _productservice = productservice;
            _mapper = mapper;
        }
        public async Task<Result<IReadOnlyList<StructureDTO>>> FindBySpecification(ISpecification<Structure> specification)
        {
            var structures = await _structureservice.FindWithSpecificationPattern(specification);
            IReadOnlyList<StructureDTO> structureDTO = _mapper.Map<IReadOnlyList<StructureDTO>>(structures);
            return Result<IReadOnlyList<StructureDTO>>.Success(structureDTO);
        }
        public async Task<Result<IReadOnlyList<ProductDTO>>> GetProducts()
        {
            var products = await _productservice.GetListAsync();
            var productsDTO = _mapper.Map<IReadOnlyList<ProductDTO>>(products);
            return Result<IReadOnlyList<ProductDTO>>.Success(productsDTO);
        }
    }
}
