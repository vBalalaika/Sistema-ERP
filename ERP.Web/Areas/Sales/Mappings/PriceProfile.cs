using AutoMapper;
using ERP.Application.DTOs.Entities.Sales;
using ERP.Domain.Entities.ProductMod;
using ERP.Domain.Entities.Purchases.Providers;
using ERP.Domain.Entities.Sales;
using ERP.Web.Areas.ProductMod.Models;
using ERP.Web.Areas.Purchases.Models.Providers;
using ERP.Web.Areas.Sales.Models;

namespace ERP.Web.Areas.Sales.Mappings
{
    internal class PriceProfile : Profile
    {
        public PriceProfile()
        {
            CreateMap<PriceDTO, PriceViewModel>()
                .ForMember(dest => dest.ProductViewModel, opt => opt.MapFrom(src => src.Product))
                .ForMember(dest => dest.StructureViewModel, opt => opt.MapFrom(src => src.Structure))
                .ReverseMap();
            CreateMap<Price, PriceViewModel>()
                .ForMember(dest => dest.ProductViewModel, opt => opt.MapFrom(src => src.Product))
                .ForMember(dest => dest.StructureViewModel, opt => opt.MapFrom(src => src.Structure))
                .ReverseMap();
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<Structure, StructureViewModel>().ReverseMap();
            CreateMap<RelProviderProduct, RelProviderProductViewModel>().ReverseMap();
        }
    }
}
