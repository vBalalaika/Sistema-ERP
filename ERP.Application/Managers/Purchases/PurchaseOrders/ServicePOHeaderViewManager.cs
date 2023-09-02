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
    public class ServicePOHeaderViewManager : ViewManager<ServicePOHeader, ServicePOHeaderDTO>, IServicePOHeaderViewManager
    {
        private readonly IServicePOHeaderService _servicePOHeaderService;
        private readonly IMapper _mapper;

        public ServicePOHeaderViewManager(IServicePOHeaderService servicePOHeaderService, IMapper mapper) : base(servicePOHeaderService, mapper)
        {
            _servicePOHeaderService = servicePOHeaderService;
            _mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<ServicePOHeaderDTO>>> FindBySpecification(ISpecification<ServicePOHeader> specification)
        {
            var servicePOrders = await _servicePOHeaderService.FindWithSpecificationPattern(specification);
            IReadOnlyList<ServicePOHeaderDTO> servicePOrdersDTO = _mapper.Map<IReadOnlyList<ServicePOHeaderDTO>>(servicePOrders);
            return Result<IReadOnlyList<ServicePOHeaderDTO>>.Success(servicePOrdersDTO);
        }
    }
}
