using AutoMapper;
using ERP.Domain.Entities.ProductMod;
using ERP.Web.Areas.ProductMod.Models;

namespace ERP.Web.Areas.ProductMod.Mappings
{
    public class ArchiveTypeProfile : Profile
    {
        public ArchiveTypeProfile()
        {
            CreateMap<ArchiveType, ArchiveTypeViewModel>()
                .ReverseMap();
        }
    }
}
