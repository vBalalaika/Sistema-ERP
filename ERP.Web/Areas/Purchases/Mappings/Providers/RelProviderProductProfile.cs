using AutoMapper;
using ERP.Application.DTOs.Entities.Purchases.Providers;
using ERP.Domain.Entities.ProductMod;
using ERP.Domain.Entities.Purchases.Providers;
using ERP.Web.Areas.ProductMod.Models;
using ERP.Web.Areas.Purchases.Models.Providers;

namespace ERP.Web.Areas.Purchases.Mappings.Providers
{
    internal class RelProviderProductProfile : Profile
    {
        public RelProviderProductProfile()
        {
            CreateMap<RelProviderProductDTO, RelProviderProductViewModel>().ReverseMap();
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<Provider, ProviderViewModel>().ReverseMap();
        }
    }
}
