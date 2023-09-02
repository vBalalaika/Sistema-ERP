using AutoMapper;
using ERP.Application.DTOs.Entities.Lists;
using ERP.Domain.Entities.Lists;

namespace ERP.Application.Mappings.Lists
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<City, CityDTO>().ReverseMap();
        }
    }
}
