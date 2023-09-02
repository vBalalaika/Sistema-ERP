using AutoMapper;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Mappings.ProductMod
{
    internal class StructureItemProfile : Profile
    {
        public StructureItemProfile()
        {
            CreateMap<StructureItem, StructureItemDTO>().ReverseMap();
        }
    }
}
