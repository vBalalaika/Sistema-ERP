using AspNetCoreHero.Results;
using AutoMapper;
using ERP.Application.DTOs.Entities.Commons;
using ERP.Application.Interfaces.Services.Commons;
using ERP.Application.Interfaces.ViewManagers.Commons;
using ERP.Domain.Entities.Commons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Managers.Commons
{
    public class IVAConditionViewManager : IIVAConditionViewManager
    {
        private readonly IIVAConditionService _ivaConditionService;
        private readonly IMapper _mapper;

        public IVAConditionViewManager(IIVAConditionService ivaConditionService, IMapper mapper)
        {
            _ivaConditionService = ivaConditionService;
            _mapper = mapper;
        }

        public async Task<Result<IVAConditionDTO>> GetByIdAsync(int id)
        {
            IVACondition entity = await _ivaConditionService.GetByIdAsync(id);
            IVAConditionDTO entityDto = _mapper.Map<IVAConditionDTO>(entity);
            return Result<IVAConditionDTO>.Success(entityDto);
        }

        public async Task<Result<IReadOnlyList<IVAConditionDTO>>> LoadAll()
        {
            IReadOnlyList<IVACondition> entities = await _ivaConditionService.GetListAsync();
            IReadOnlyList<IVAConditionDTO> entitiesDtos = _mapper.Map<IReadOnlyList<IVAConditionDTO>>(entities);
            return Result<IReadOnlyList<IVAConditionDTO>>.Success(entitiesDtos);
        }
    }
}
