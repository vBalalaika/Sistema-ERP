using AutoMapper;
using ERP.Application.DTOs.Entities.Sales;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services.Sales;
using ERP.Domain.Entities.Sales;

namespace ERP.Application.Services.Sales
{
    public class DeliveryDateHistoryService : GenericService<DeliveryDateHistory, DeliveryDateHistoryDTO>, IDeliveryDateHistoryService
    {
        public DeliveryDateHistoryService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }
    }
}
