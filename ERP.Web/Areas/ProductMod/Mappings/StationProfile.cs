using AutoMapper;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Domain.Entities.ProductMod;
using ERP.Web.Areas.ProductMod.Models;

namespace ERP.Web.Areas.ProductMod.Mappings
{
    internal class StationProfile : Profile
    {
        public StationProfile()
        {
            CreateMap<StationDTO, StationViewModel>().ReverseMap();
            CreateMap<FunctionalArea, FunctionalAreaViewModel>().ReverseMap();
        }
    }
}
