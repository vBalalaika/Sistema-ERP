using AutoMapper;
using ERP.Application.DTOs.Entities.Clients;
using ERP.Domain.Entities.Clients;

namespace ERP.Application.Mappings.Clients
{
    public class ConsultedProductProfile : Profile
    {
        public ConsultedProductProfile()
        {
            CreateMap<ConsultedProduct, ConsultedProductDTO>().ReverseMap();
        }
    }
}
