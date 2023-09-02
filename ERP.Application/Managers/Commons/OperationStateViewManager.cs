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
    public class OperationStateViewManager : IOperationStateViewManager
    {

        private readonly IOperationStateService _operationStateService;
        private readonly IMapper _mapper;

        public OperationStateViewManager(IOperationStateService operationStateService, IMapper mapper)
        {
            _operationStateService = operationStateService;
            _mapper = mapper;
        }

        public async Task<Result<OperationStateDTO>> GetByIdAsync(int id)
        {
            OperationState entity = await _operationStateService.GetByIdAsync(id);
            OperationStateDTO entityDto = _mapper.Map<OperationStateDTO>(entity);
            return Result<OperationStateDTO>.Success(entityDto);
        }

        public async Task<Result<IReadOnlyList<OperationStateDTO>>> LoadAll()
        {
            IReadOnlyList<OperationState> entities = await _operationStateService.GetListAsync();
            IReadOnlyList<OperationStateDTO> entitiesDtos = _mapper.Map<IReadOnlyList<OperationStateDTO>>(entities);
            return Result<IReadOnlyList<OperationStateDTO>>.Success(entitiesDtos);
        }

    }
}
