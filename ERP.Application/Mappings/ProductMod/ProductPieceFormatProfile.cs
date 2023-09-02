using AutoMapper;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Mappings.ProductMod
{
    internal class ProductPieceFormatProfile : Profile
    {
        public ProductPieceFormatProfile()
        {
            CreateMap<ProductPieceFormat, ProductPieceFormatDTO>().ReverseMap();
        }
    }
}
