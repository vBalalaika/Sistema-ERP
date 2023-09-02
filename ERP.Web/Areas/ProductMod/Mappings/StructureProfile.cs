using AutoMapper;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Domain.Entities.ProductMod;
using ERP.Web.Areas.ProductMod.Models;

namespace ERP.Web.Areas.ProductMod.Mappings
{
    internal class StructureProfile : Profile
    {
        public StructureProfile()
        {
            CreateMap<StructureDTO, StructureViewModel>().ReverseMap();
            CreateMap<Structure, StructureViewModel>().ReverseMap();
        }
    }
}
