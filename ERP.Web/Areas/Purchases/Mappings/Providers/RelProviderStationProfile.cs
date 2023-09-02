using AutoMapper;
using ERP.Application.DTOs.Entities.Purchases.Providers;
using ERP.Domain.Entities.ProductMod;
using ERP.Domain.Entities.Purchases.Providers;
using ERP.Web.Areas.ProductMod.Models;
using ERP.Web.Areas.Purchases.Models.Providers;

namespace ERP.Web.Areas.Purchases.Mappings.Providers
{
    public class RelProviderStationProfile : Profile
    {
        public RelProviderStationProfile()
        {
            CreateMap<RelProviderStationDTO, RelProviderStationViewModel>().ReverseMap();
            CreateMap<Station, StationViewModel>().ReverseMap();
            CreateMap<Provider, ProviderViewModel>().ReverseMap();
        }
    }
}
