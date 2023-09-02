using AutoMapper;
using ERP.Application.DTOs.Entities.Sales;
using ERP.Domain.Entities.Sales;

namespace ERP.Application.Mappings.Sales
{
    public class OrderHeaderProfile : Profile
    {
        public OrderHeaderProfile()
        {
            CreateMap<OrderHeader, OrderHeaderDTO>().ReverseMap();
        }
    }
}
