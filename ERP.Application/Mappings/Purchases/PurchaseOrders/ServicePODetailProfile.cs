using AutoMapper;
using ERP.Application.DTOs.Entities.Purchases.PurchaseOrders;
using ERP.Domain.Entities.Purchases.PurchaseOrders;

namespace ERP.Application.Mappings.Purchases.PurchaseOrders
{
    public class ServicePODetailProfile : Profile
    {
        public ServicePODetailProfile()
        {
            CreateMap<ServicePODetail, ServicePODetailDTO>().ReverseMap();
        }
    }
}
