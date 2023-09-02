using AutoMapper;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Mappings.ProductMod
{
    internal class AccessoryProductProfile : Profile
    {
        public AccessoryProductProfile()
        {
            CreateMap<AccessoryProduct, AccessoryProductDTO>().ReverseMap();
        }
    }
}
