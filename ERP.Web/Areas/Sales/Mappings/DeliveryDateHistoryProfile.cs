using AutoMapper;
using ERP.Application.DTOs.Entities.Sales;
using ERP.Domain.Entities.Sales;
using ERP.Web.Areas.Sales.Models;

namespace ERP.Web.Areas.Sales.Mappings
{
    public class DeliveryDateHistoryProfile : Profile
    {
        public DeliveryDateHistoryProfile()
        {
            CreateMap<DeliveryDateHistoryDTO, DeliveryDateHistoryViewModel>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailViewModel>().ReverseMap();
        }
    }
}
