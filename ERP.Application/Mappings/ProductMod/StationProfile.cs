using AutoMapper;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Mappings.ProductMod
{
    internal class StationProfile : Profile
    {
        public StationProfile()
        {
            CreateMap<Station, StationDTO>().ReverseMap();
        }
    }
}
