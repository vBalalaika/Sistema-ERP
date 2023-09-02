using AutoMapper;
using ERP.Application.DTOs.Entities.Purchases.PurchaseOrders;
using ERP.Domain.Entities.Purchases.PurchaseOrders;
using ERP.Web.Areas.Purchases.Models.PurchaseOrder;

namespace ERP.Web.Areas.Purchases.Mappings.PurchaseOrder
{
    public class ServicePOHeaderProfile : Profile
    {
        public ServicePOHeaderProfile()
        {
            CreateMap<ServicePOHeaderDTO, ServicePOHeaderViewModel>().ReverseMap();

            CreateMap<ServicePODetail, ServicePODetailViewModel>().ReverseMap();
        }
    }
}
