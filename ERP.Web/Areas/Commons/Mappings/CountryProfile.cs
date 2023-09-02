using AutoMapper;
using ERP.Application.DTOs.Entities.Commons;
using ERP.Web.Areas.Commons.Models;

namespace ERP.Web.Areas.Commons.Mappings
{
    internal class CountryProfile : Profile
    {
        public CountryProfile()
        {
            CreateMap<CountryDTO, CountryViewModel>().ReverseMap();
        }
    }
}
