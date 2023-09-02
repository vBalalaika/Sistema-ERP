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
    public class PurchaseOrderDetailViewManager : ViewManager<PurchaseOrderDetail, PurchaseOrderDetailDTO>, IPurchaseOrderDetailViewManager
    {
        private readonly IPurchaseOrderDetailService _purchaseOrderDetailService;
        private readonly IMapper _mapper;

        public PurchaseOrderDetailViewManager(IPurchaseOrderDetailService purchaseOrderDetailService, IMapper mapper) : base(purchaseOrderDetailService, mapper)
        {
            _purchaseOrderDetailService = purchaseOrderDetailService;
            _mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<PurchaseOrderDetailDTO>>> FindBySpecification(ISpecification<PurchaseOrderDetail> specification)
        {
            var purchaseOrders = await _purchaseOrderDetailService.FindWithSpecificationPattern(specification);
            IReadOnlyList<PurchaseOrderDetailDTO> purchaseOrdersDTO = _mapper.Map<IReadOnlyList<PurchaseOrderDetailDTO>>(purchaseOrders);
            return Result<IReadOnlyList<PurchaseOrderDetailDTO>>.Success(purchaseOrdersDTO);
        }

        public async Task<int> UpdateDataOnReturnItem(int podId)
        {
            int result = await _purchaseOrderDetailService.UpdateDataOnReturnItemAsync(podId);
            return result;
        }
    }
}
