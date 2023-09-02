using AspNetCoreHero.Results;
using AutoMapper;
using ERP.Application.DTOs.Entities.Purchases.PurchaseOrders;
using ERP.Application.Interfaces.Services.Purchases.PurchaseOrders;
using ERP.Application.Interfaces.ViewManagers.Purchases.PurchaseOrders;
using ERP.Application.Specification;
using ERP.Domain.Entities.Purchases.PurchaseOrders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Managers.Purchases.PurchaseOrders
{
    public class PurchaseOrderHeaderViewManager : ViewManager<PurchaseOrderHeader, PurchaseOrderHeaderDTO>, IPurchaseOrderHeaderViewManager
    {
        private readonly IPurchaseOrderHeaderService _purchaseOrderHeaderService;
        private readonly IMapper _mapper;

        public PurchaseOrderHeaderViewManager(IPurchaseOrderHeaderService purchaseOrderHeaderService, IMapper mapper) : base(purchaseOrderHeaderService, mapper)
        {
            _purchaseOrderHeaderService = purchaseOrderHeaderService;
            _mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<PurchaseOrderHeaderDTO>>> FindBySpecification(ISpecification<PurchaseOrderHeader> specification)
        {
            var purchaseOrders = await _purchaseOrderHeaderService.FindWithSpecificationPattern(specification);
            IReadOnlyList<PurchaseOrderHeaderDTO> purchaseOrdersDTO = _mapper.Map<IReadOnlyList<PurchaseOrderHeaderDTO>>(purchaseOrders);
            return Result<IReadOnlyList<PurchaseOrderHeaderDTO>>.Success(purchaseOrdersDTO);
        }
    }
}
