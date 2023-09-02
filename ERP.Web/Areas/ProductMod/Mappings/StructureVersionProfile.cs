using AutoMapper;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Domain.Entities.ProductMod;
using ERP.Web.Areas.ProductMod.Models;

namespace ERP.Web.Areas.ProductMod.Mappings
{
    internal class StructureVersionProfile : Profile
    {
        public StructureVersionProfile()
        {
            CreateMap<StructureVersionDTO, StructureVersionViewModel>().ReverseMap();
            CreateMap<StructureVersion, StructureVersionViewModel>().ReverseMap();
        }
    }
}
