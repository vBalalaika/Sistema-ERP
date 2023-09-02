using AutoMapper;
using ERP.Application.DTOs.Entities.Lists;
using ERP.Domain.Entities.Lists;
using ERP.Web.Areas.Lists.Models;

namespace ERP.Web.Areas.Lists.Mappings
{
    public class CityStateProfile : Profile
    {
        public CityStateProfile()
        {
            CreateMap<CityDTO, CityStateViewModel>()
                .ForMember(source => source.City_Id, opt => opt.MapFrom(dest => dest.Id))
                .ForMember(source => source.City_Name, opt => opt.MapFrom(dest => dest.Name))
                .ForMember(source => source.City_ZipCode, opt => opt.MapFrom(dest => dest.ZipCode))
                .ForMember(source => source.City_ConcurrencyToken, opt => opt.MapFrom(dest => dest.ConcurrencyToken))
                .ReverseMap();
            CreateMap<City, CityStateViewModel>()
                .ForMember(source => source.City_Id, opt => opt.MapFrom(dest => dest.Id))
                .ForMember(source => source.City_Name, opt => opt.MapFrom(dest => dest.Name))
                .ForMember(source => source.City_ZipCode, opt => opt.MapFrom(dest => dest.ZipCode))
                .ForMember(source => source.City_ConcurrencyToken, opt => opt.MapFrom(dest => dest.ConcurrencyToken))
                .ReverseMap();

            CreateMap<StateDTO, CityStateViewModel>()
                .ForMember(source => source.State_Id, opt => opt.MapFrom(dest => dest.Id))
                .ForMember(source => source.State_Name, opt => opt.MapFrom(dest => dest.Name))
                .ForMember(source => source.State_ConcurrencyToken, opt => opt.MapFrom(dest => dest.ConcurrencyToken))
                .ReverseMap();
            CreateMap<State, CityStateViewModel>()
                .ForMember(source => source.State_Id, opt => opt.MapFrom(dest => dest.Id))
                .ForMember(source => source.State_Name, opt => opt.MapFrom(dest => dest.Name))
                .ForMember(source => source.State_ConcurrencyToken, opt => opt.MapFrom(dest => dest.ConcurrencyToken))
                .ReverseMap();
        }
    }
}
