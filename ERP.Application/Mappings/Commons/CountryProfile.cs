using AutoMapper;
using ERP.Application.DTOs.Entities.Commons;
using ERP.Domain.Entities.Commons;

namespace ERP.Application.Mappings.Commons
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            CreateMap<Country, CountryDTO>().ReverseMap();
        }
    }
}
