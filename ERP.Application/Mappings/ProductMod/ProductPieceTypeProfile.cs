using AutoMapper;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Mappings.ProductMod
{
    internal class ProductPieceTypeProfile : Profile
    {
        public ProductPieceTypeProfile()
        {
            CreateMap<ProductPieceType, ProductPieceTypeDTO>().ReverseMap();
        }

    }
}
