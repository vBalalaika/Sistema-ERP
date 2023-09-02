using AutoMapper;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Mappings.ProductMod
{
    internal class ProductCutTypeProfile : Profile
    {
        public ProductCutTypeProfile()
        {
            CreateMap<ProductCutType, ProductCutTypeDTO>().ReverseMap();
        }
    }
}
