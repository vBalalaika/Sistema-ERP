using AutoMapper;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Mappings.ProductMod
{
    public class ArchiveTypeProfile : Profile
    {
        public ArchiveTypeProfile()
        {
            CreateMap<ArchiveType, ArchiveTypeDTO>().ReverseMap();
        }
    }
}
