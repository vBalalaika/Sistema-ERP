using AutoMapper;
using ERP.Application.DTOs.Entities.Purchases.Providers;
using ERP.Domain.Entities.Commons;
using ERP.Domain.Entities.Lists;
using ERP.Web.Areas.Commons.Models;
using ERP.Web.Areas.Lists.Models;
using ERP.Web.Areas.Purchases.Models.Providers;

namespace ERP.Web.Areas.Purchases.Mappings.Providers
{
    internal class SubsidiaryProfile : Profile
    {
        public SubsidiaryProfile()
        {
            CreateMap<SubsidiaryDTO, SubsidiaryViewModel>()
                .ReverseMap();
            CreateMap<Country, CountryViewModel>()
                .ReverseMap();
            CreateMap<City, CityViewModel>()
                .ReverseMap();
            CreateMap<State, StateViewModel>()
                .ReverseMap();
        }
    }
}
