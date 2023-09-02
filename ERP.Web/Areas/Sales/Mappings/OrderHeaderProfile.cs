using AutoMapper;
using ERP.Application.DTOs.Entities.Sales;
using ERP.Domain.Entities.Clients;
using ERP.Domain.Entities.Sales;
using ERP.Web.Areas.Clients.Models;
using ERP.Web.Areas.Sales.Models;

namespace ERP.Web.Areas.Sales.Mappings
{
    internal class OrderHeaderProfile : Profile
    {
        public OrderHeaderProfile()
        {
            CreateMap<OrderHeaderDTO, OrderHeaderViewModel>()
                .ForMember(oh => oh.deliveryDateHistoryVMs, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<OrderDetail, OrderDetailViewModel>().ReverseMap();

            CreateMap<Client, ClientViewModel>().ReverseMap();
            CreateMap<OrderState, OrderStateViewModel>().ReverseMap();
        }
    }
}
