using AutoMapper;
using ERP.Application.DTOs.Entities.Purchases.QuoteRequests;
using ERP.Domain.Entities.Purchases.QuoteRequests;

namespace ERP.Application.Mappings.Purchases.QuoteRequests
{
    public class QuoteRequestDetailProfile : Profile
    {
        public QuoteRequestDetailProfile()
        {
            CreateMap<QuoteRequestDetail, QuoteRequestDetailDTO>().ReverseMap();
        }
    }
}
