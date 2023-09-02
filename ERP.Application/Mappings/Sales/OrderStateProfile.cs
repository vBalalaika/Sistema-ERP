using AutoMapper;
using ERP.Application.DTOs.Entities.Sales;
using ERP.Domain.Entities.Sales;

namespace ERP.Application.Mappings.Sales
{
    public class OrderStateProfile : Profile
    {
        public OrderStateProfile()
        {
            CreateMap<OrderState, OrderStateDTO>().ReverseMap();
        }
    }
}
