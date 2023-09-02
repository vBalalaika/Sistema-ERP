using AutoMapper;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Mappings.ProductMod
{
    internal class CutTypeProfile : Profile
    {
        public CutTypeProfile()
        {
            CreateMap<CutType, CutTypeDTO>().ReverseMap();
        }
    }
}
