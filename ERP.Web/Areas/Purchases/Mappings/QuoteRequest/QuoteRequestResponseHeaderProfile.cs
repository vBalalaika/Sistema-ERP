using AutoMapper;
using ERP.Application.DTOs.Entities.Purchases.QuoteRequests;
using ERP.Domain.Entities.Purchases.Providers;
using ERP.Domain.Entities.Purchases.QuoteRequests;
using ERP.Web.Areas.Purchases.Models.Providers;
using ERP.Web.Areas.Purchases.Models.QuoteRequest;

namespace ERP.Web.Areas.Purchases.Mappings.QuoteRequest
{
    public class QuoteRequestResponseHeaderProfile : Profile
    {
        public QuoteRequestResponseHeaderProfile()
        {
            CreateMap<QuoteRequestResponseHeaderDTO, QuoteRequestResponseHeaderViewModel>().ReverseMap();
            CreateMap<QuoteRequestResponseDetail, QuoteRequestResponseDetailViewModel>().ReverseMap();
            CreateMap<QuoteRequestDetail, QuoteRequestDetailViewModel>().ReverseMap();
            CreateMap<QuoteRequestHeader, QuoteRequestHeaderViewModel>().ReverseMap();
            CreateMap<Provider, ProviderViewModel>().ReverseMap();
        }
    }
}
