using AutoMapper;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Domain.Entities.ProductMod;
using ERP.Web.Areas.ProductMod.Models;

namespace ERP.Web.Areas.ProductMod.Mappings
{
    internal class RelProductStationProfile : Profile
    {
        public RelProductStationProfile()
        {
            CreateMap<RelProductStationDTO, RelProductStationViewModel>().ReverseMap();
            CreateMap<Station, StationViewModel>().ReverseMap();
        }
    }
}
