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
    public class ServicePODetailViewManager : ViewManager<ServicePODetail, ServicePODetailDTO>, IServicePODetailViewManager
    {
        private readonly IServicePODetailService _servicePODetailService;
        private readonly IMapper _mapper;

        public ServicePODetailViewManager(IServicePODetailService servicePODetailService, IMapper mapper) : base(servicePODetailService, mapper)
        {
            _servicePODetailService = servicePODetailService;
            _mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<ServicePODetailDTO>>> FindBySpecification(ISpecification<ServicePODetail> specification)
        {
            var servicePOrders = await _servicePODetailService.FindWithSpecificationPattern(specification);
            IReadOnlyList<ServicePODetailDTO> servicePOrdersDTO = _mapper.Map<IReadOnlyList<ServicePODetailDTO>>(servicePOrders);
            return Result<IReadOnlyList<ServicePODetailDTO>>.Success(servicePOrdersDTO);
        }
    }
}
