using AspNetCoreHero.Results;
using AutoMapper;
using ERP.Application.DTOs.Entities.Purchases.MissingProducts;
using ERP.Application.Interfaces.Services.Purchases.MissingProducts;
using ERP.Application.Interfaces.ViewManagers.Purchases.MissingProducts;
using ERP.Domain.Entities.Purchases.MissingProducts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Managers.Purchases.MissingProducts
{
    public class PurchaseStateViewManager : IPurchaseStateViewManager
    {

        private readonly IPurchaseStateService _stateMissingProductService;
        private readonly IMapper _mapper;

        public PurchaseStateViewManager(IPurchaseStateService stateMissingProductService, IMapper mapper)
        {
            _stateMissingProductService = stateMissingProductService;
            _mapper = mapper;
        }

        public async Task<Result<PurchaseStateDTO>> GetByIdAsync(int id)
        {
            PurchaseState entity = await _stateMissingProductService.GetByIdAsync(id);
            PurchaseStateDTO entityDto = _mapper.Map<PurchaseStateDTO>(entity);
            return Result<PurchaseStateDTO>.Success(entityDto);
        }

        public async Task<Result<IReadOnlyList<PurchaseStateDTO>>> LoadAll()
        {
            IReadOnlyList<PurchaseState> entities = await _stateMissingProductService.GetListAsync();
            IReadOnlyList<PurchaseStateDTO> entitiesDtos = _mapper.Map<IReadOnlyList<PurchaseStateDTO>>(entities);
            return Result<IReadOnlyList<PurchaseStateDTO>>.Success(entitiesDtos);
        }
    }
}
