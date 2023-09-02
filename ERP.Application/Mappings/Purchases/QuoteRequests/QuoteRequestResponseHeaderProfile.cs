using AutoMapper;
using ERP.Application.DTOs.Entities.Purchases.QuoteRequests;
using ERP.Domain.Entities.Purchases.QuoteRequests;

namespace ERP.Application.Mappings.Purchases.QuoteRequests
{
    public class QuoteRequestResponseHeaderProfile : Profile
    {
        public QuoteRequestResponseHeaderProfile()
        {
            CreateMap<QuoteRequestResponseHeader, QuoteRequestResponseHeaderDTO>().ReverseMap();
        }
    }
}
