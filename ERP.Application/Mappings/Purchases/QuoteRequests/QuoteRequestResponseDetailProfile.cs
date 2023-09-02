using AutoMapper;
using ERP.Application.DTOs.Entities.Purchases.QuoteRequests;
using ERP.Domain.Entities.Purchases.QuoteRequests;

namespace ERP.Application.Mappings.Purchases.QuoteRequests
{
    public class QuoteRequestResponseDetailProfile : Profile
    {
        public QuoteRequestResponseDetailProfile()
        {
            CreateMap<QuoteRequestResponseDetail, QuoteRequestResponseDetailDTO>().ReverseMap();
        }
    }
}
