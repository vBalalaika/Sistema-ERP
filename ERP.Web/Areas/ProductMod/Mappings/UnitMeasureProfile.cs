using AutoMapper;
using ERP.Application.DTOs.Entities.Commons;
using ERP.Web.Areas.ProductMod.Models;

namespace ERP.Web.Areas.ProductMod.Mappings
{
    internal class UnitMeasureProfile : Profile
    {
        public UnitMeasureProfile()
        {
            CreateMap<UnitMeasureDTO, UnitMeasureViewModel>().ReverseMap();
        }
    }
}
