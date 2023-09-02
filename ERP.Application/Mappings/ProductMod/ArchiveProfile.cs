using AutoMapper;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Mappings.ProductMod
{
    public class ArchiveProfile : Profile
    {
        public ArchiveProfile()
        {
            CreateMap<Archive, ArchiveDTO>().ReverseMap();
        }
    }
}
