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
    public class StructureItemViewManager : ViewManager<StructureItem, StructureItemDTO>, IStructureItemViewManager
    {
        private readonly IStructureItemService _structureitemservice;
        private readonly IMapper _mapper;

        public StructureItemViewManager(IStructureItemService structureitemservice, IMapper mapper) : base(structureitemservice, mapper)
        {
            _structureitemservice = structureitemservice;
            _mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<StructureItemDTO>>> FindBySpecification(ISpecification<StructureItem> specification)
        {
            var structureitems = await _structureitemservice.FindWithSpecificationPattern(specification);
            IReadOnlyList<StructureItemDTO> structureitemsDTO = _mapper.Map<IReadOnlyList<StructureItemDTO>>(structureitems);
            return Result<IReadOnlyList<StructureItemDTO>>.Success(structureitemsDTO);
        }
    }
}
