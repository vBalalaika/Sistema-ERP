using AutoMapper;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Domain.Entities.Commons;
using ERP.Domain.Entities.ProductMod;
using ERP.Domain.Entities.Purchases.Providers;
using ERP.Web.Areas.ProductMod.Models;
using ERP.Web.Areas.Purchases.Models.Providers;

namespace ERP.Web.Areas.ProductMod.Mappings
{
    internal class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductDTO, ProductViewModel>().ReverseMap();
            CreateMap<SubCategory, SubCategoryViewModel>().ReverseMap();
            CreateMap<UnitMeasure, UnitMeasureViewModel>().ReverseMap();
            CreateMap<SupplyVoltage, SupplyVoltageViewModel>().ReverseMap();
            CreateMap<RelProductStation, RelProductStationViewModel>().ReverseMap();
            CreateMap<RelProviderProduct, RelProviderProductViewModel>().ReverseMap();
            CreateMap<ProductSupplyVoltage, ProductSupplyVoltageViewModel>().ReverseMap();
            CreateMap<ProductPieceType, ProductPieceTypeViewModel>().ReverseMap();
            CreateMap<ProductPieceFormat, ProductPieceFormatViewModel>().ReverseMap();
            CreateMap<ProductFeature, ProductFeatureViewModel>().ReverseMap();
            CreateMap<ProductCutType, ProductCutTypeViewModel>().ReverseMap();
            CreateMap<PieceType, PieceTypeViewModel>().ReverseMap();
            CreateMap<PieceFormat, PieceFormatViewModel>().ReverseMap();
            CreateMap<CutType, CutTypeViewModel>().ReverseMap();
            CreateMap<Archive, ArchiveViewModel>().ReverseMap();
            CreateMap<AccessoryProduct, AccessoryProductViewModel>().ReverseMap();
            CreateMap<Operation, OperationViewModel>().ReverseMap();
            CreateMap<Product, ProductViewModel>().ReverseMap().ForMember(pr => pr.AccessoryProductsOf, opt => opt.Ignore());

        }
    }
}