using AutoMapper;
using ERP.Application.DTOs.Entities.Lists;
using ERP.Domain.Entities.Lists;
using ERP.Web.Areas.Lists.Models;

namespace ERP.Web.Areas.Lists.Mappings
{
    internal class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<CityDTO, CityViewModel>().ReverseMap();
            CreateMap<City, CityViewModel>().ReverseMap();
        }
    }
}
