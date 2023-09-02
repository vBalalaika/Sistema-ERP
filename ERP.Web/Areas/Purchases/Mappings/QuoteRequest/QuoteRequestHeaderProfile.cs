using AutoMapper;
using ERP.Application.DTOs.Entities.Purchases.QuoteRequests;
using ERP.Domain.Entities.Purchases.QuoteRequests;
using ERP.Web.Areas.Purchases.Models.QuoteRequest;

namespace ERP.Web.Areas.Purchases.Mappings.QuoteRequest
{
    public class QuoteRequestHeaderProfile : Profile
    {
        public QuoteRequestHeaderProfile()
        {
            CreateMap<QuoteRequestHeaderDTO, QuoteRequestHeaderViewModel>().ReverseMap();
            CreateMap<RelQuoteRequestProvider, RelQuoteRequestProviderViewModel>().ReverseMap();
            CreateMap<QuoteRequestDetail, QuoteRequestDetailViewModel>().ReverseMap();
            CreateMap<QuoteRequestResponseHeader, QuoteRequestResponseHeaderViewModel>().ReverseMap();
        }
    }
}
