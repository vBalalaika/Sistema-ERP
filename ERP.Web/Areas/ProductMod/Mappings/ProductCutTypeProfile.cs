using AutoMapper;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Web.Areas.ProductMod.Models;

namespace ERP.Web.Areas.ProductMod.Mappings
{
    internal class ProductCutTypeProfile : Profile
    {
        public ProductCutTypeProfile()
        {
            CreateMap<ProductCutTypeDTO, ProductCutTypeViewModel>().ReverseMap();
        }
    }
}
