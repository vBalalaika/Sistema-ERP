using AspNetCoreHero.Results;
using AutoMapper;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Application.Interfaces.Services.ProductMod;
using ERP.Application.Interfaces.ViewManagers.ProductMod;
using ERP.Domain.Entities.ProductMod;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Managers.ProductMod
{
    public class FunctionalAreaViewManager : ViewManager<FunctionalArea, FunctionalAreaDTO>, IFunctionalAreaViewManager
    {
        private readonly IFunctionalAreaService _functionalareaservice;
        private readonly IMapper _mapper;

        public FunctionalAreaViewManager(IFunctionalAreaService functionalareaservice, IMapper mapper) :
           base(functionalareaservice, mapper)
        {
            _functionalareaservice = functionalareaservice;
            _mapper = mapper;
        }
        public async Task<Result<IReadOnlyList<FunctionalAreaDTO>>> GetAllFunctionalAreas()
        {
            var functionalAreas = await _functionalareaservice.GetListAsync();
            var functionalAreasDTO = _mapper.Map<IReadOnlyList<FunctionalAreaDTO>>(functionalAreas);
            return Result<IReadOnlyList<FunctionalAreaDTO>>.Success(functionalAreasDTO);
        }

    }
}
