using AutoMapper;
using ERP.Application.DTOs.Entities.Purchases.QuoteRequests;
using ERP.Domain.Entities.Commons;
using ERP.Domain.Entities.ProductMod;
using ERP.Domain.Entities.Purchases.QuoteRequests;
using ERP.Web.Areas.ProductMod.Models;
using ERP.Web.Areas.Purchases.Models.MissingProduct;
using ERP.Web.Areas.Purchases.Models.QuoteRequest;

namespace ERP.Web.Areas.Purchases.Mappings.QuoteRequest
{
    public class QuoteRequestDetailProfile : Profile
    {
        public QuoteRequestDetailProfile()
        {
            CreateMap<QuoteRequestDetailDTO, QuoteRequestDetailViewModel>().ReverseMap();
            CreateMap<QuoteRequestHeader, QuoteRequestHeaderViewModel>().ReverseMap();
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<UnitMeasure, UnitMeasureViewModel>().ReverseMap();
            CreateMap<ERP.Domain.Entities.Purchases.MissingProducts.MissingProduct, MissingProductViewModel>().ReverseMap();
            CreateMap<QuoteRequestResponseHeader, QuoteRequestResponseHeaderViewModel>().ReverseMap();
        }
    }
}
