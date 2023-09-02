using AutoMapper;
using ERP.Application.DTOs.Entities.Purchases.QuoteRequests;
using ERP.Domain.Entities.Purchases.Providers;
using ERP.Domain.Entities.Purchases.QuoteRequests;
using ERP.Web.Areas.Purchases.Models.Providers;
using ERP.Web.Areas.Purchases.Models.QuoteRequest;

namespace ERP.Web.Areas.Purchases.Mappings.QuoteRequest
{
    internal class RelQuoteRequestProviderProfile : Profile
    {
        public RelQuoteRequestProviderProfile()
        {
            CreateMap<RelQuoteRequestProviderDTO, RelQuoteRequestProviderViewModel>().ReverseMap();
            CreateMap<QuoteRequestHeader, QuoteRequestHeaderViewModel>().ReverseMap();
            CreateMap<Provider, ProviderViewModel>().ReverseMap();
        }
    }
}
