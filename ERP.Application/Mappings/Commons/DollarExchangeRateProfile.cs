using AutoMapper;
using ERP.Application.DTOs.Entities.Commons;
using ERP.Domain.Entities.Commons;

namespace ERP.Application.Mappings.Commons
{
    public class DollarExchangeRateProfile : Profile
    {
        public DollarExchangeRateProfile()
        {
            CreateMap<DollarExchangeRate, DollarExchangeRateDTO>().ReverseMap();
        }
    }
}
