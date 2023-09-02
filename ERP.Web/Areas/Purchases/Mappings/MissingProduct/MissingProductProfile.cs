using AutoMapper;
using ERP.Application.DTOs.Entities.Purchases.MissingProducts;
using ERP.Web.Areas.Purchases.Models.MissingProduct;

namespace ERP.Web.Areas.Purchases.Mappings.MissingProduct
{
    internal class MissingProductProfile : Profile
    {
        public MissingProductProfile()
        {
            CreateMap<MissingProductDTO, MissingProductViewModel>().ReverseMap();
        }
    }
}
