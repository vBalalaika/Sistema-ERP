using AutoMapper;
using ERP.Application.DTOs.Entities.Purchases.PurchaseOrders;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services.Purchases.PurchaseOrders;
using ERP.Domain.Entities.Purchases.PurchaseOrders;

namespace ERP.Application.Services.Purchases.PurchaseOrders
{
    public class ServicePODetailService : GenericService<ServicePODetail, ServicePODetailDTO>, IServicePODetailService
    {
        public ServicePODetailService(IUnitOfWork unitOfWOrk, IMapper mapper) : base(unitOfWOrk, mapper)
        {

        }
    }
}
