using AutoMapper;
using ERP.Application.DTOs.Entities.Sales;
using ERP.Web.Areas.Sales.Models;

namespace ERP.Web.Areas.Sales.Mappings
{
    internal class OrderStateProfile : Profile
    {
        public OrderStateProfile()
        {
            CreateMap<OrderStateDTO, OrderStateViewModel>().ReverseMap();
        }
    }
}
