using AspNetCoreHero.Results;
using AutoMapper;
using ERP.Application.DTOs.Entities.Sales;
using ERP.Application.Interfaces.Services.Sales;
using ERP.Application.Interfaces.ViewManagers.Sales;
using ERP.Application.Specification;
using ERP.Domain.Entities.Sales;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Managers.Sales
{
    public class DeliveryDateHistoryViewManager : ViewManager<DeliveryDateHistory, DeliveryDateHistoryDTO>, IDeliveryDateHistoryViewManager
    {
        private readonly IDeliveryDateHistoryService _deliveryDateHistoryService;
        private readonly IMapper _mapper;

        public DeliveryDateHistoryViewManager(IDeliveryDateHistoryService deliveryDateHistoryService, IMapper mapper) : base(deliveryDateHistoryService, mapper)
        {
            _deliveryDateHistoryService = deliveryDateHistoryService;
            _mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<DeliveryDateHistoryDTO>>> FindBySpecification(ISpecification<DeliveryDateHistory> specification)
        {
            var deliveryDateHistories = await _deliveryDateHistoryService.FindWithSpecificationPattern(specification);
            IReadOnlyList<DeliveryDateHistoryDTO> deliveryDateHistoriesDto = _mapper.Map<IReadOnlyList<DeliveryDateHistoryDTO>>(deliveryDateHistories);
            return Result<IReadOnlyList<DeliveryDateHistoryDTO>>.Success(deliveryDateHistoriesDto);
        }
    }
}
