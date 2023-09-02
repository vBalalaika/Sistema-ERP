using AutoMapper;
using ERP.Application.DTOs.Entities.Sales;
using ERP.Domain.Entities.Sales;

namespace ERP.Application.Mappings.Sales
{
    public class PriceProfile : Profile
    {
        public PriceProfile()
        {
            CreateMap<Price, PriceDTO>().ReverseMap();
        }
    }
}
