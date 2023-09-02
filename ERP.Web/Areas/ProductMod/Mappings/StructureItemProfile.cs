using AutoMapper;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Domain.Entities.ProductMod;
using ERP.Web.Areas.ProductMod.Models;

namespace ERP.Web.Areas.ProductMod.Mappings
{
    internal class StructureItemProfile : Profile
    {
        public StructureItemProfile()
        {
            CreateMap<StructureItemDTO, StructureItemViewModel>().ReverseMap();
            CreateMap<StructureItem, StructureItemViewModel>().ReverseMap();
        }
    }
}
