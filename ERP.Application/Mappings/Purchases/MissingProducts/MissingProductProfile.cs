using AutoMapper;
using ERP.Application.DTOs.Entities.Purchases.MissingProducts;
using ERP.Domain.Entities.Purchases.MissingProducts;

namespace ERP.Application.Mappings.Purchases.MissingProducts
{
    public class MissingProductProfile : Profile
    {
        public MissingProductProfile()
        {
            CreateMap<MissingProduct, MissingProductDTO>().ReverseMap();
        }
    }
}
