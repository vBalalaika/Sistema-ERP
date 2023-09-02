using AutoMapper;
using ERP.Application.DTOs.Entities.Sales;
using ERP.Domain.Entities.Sales;

namespace ERP.Application.Mappings.Sales
{
    public class DeliveryDateHistoryProfile : Profile
    {
        public DeliveryDateHistoryProfile()
        {
            CreateMap<DeliveryDateHistory, DeliveryDateHistoryDTO>().ReverseMap();
        }
    }
}
