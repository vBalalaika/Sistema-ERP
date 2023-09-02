using AutoMapper;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Mappings.ProductMod
{
    internal class StructureVersionProfile : Profile
    {
        public StructureVersionProfile()
        {
            CreateMap<StructureVersion, StructureVersionDTO>().ReverseMap();
        }
    }
}
