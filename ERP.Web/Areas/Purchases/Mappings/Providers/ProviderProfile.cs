using AutoMapper;
using ERP.Application.DTOs.Entities.Purchases.Providers;
using ERP.Domain.Entities.Purchases.Providers;
using ERP.Domain.Entities.Purchases.QuoteRequests;
using ERP.Web.Areas.Purchases.Models.Providers;
using ERP.Web.Areas.Purchases.Models.QuoteRequest;

namespace ERP.Web.Areas.Purchases.Mappings.Providers
{
    internal class ProviderProfile : Profile
    {
        public ProviderProfile()
        {
            CreateMap<ProviderDTO, ProviderViewModel>().ReverseMap();
            CreateMap<Contact, ContactViewModel>()
                .ForMember(c => c.ProviderId, opt => opt.Ignore())
                .ForMember(c => c.Provider, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<Subsidiary, SubsidiaryViewModel>()
                .ForMember(c => c.ProviderId, opt => opt.Ignore())
                .ForMember(c => c.Provider, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<FinancialInformation, FinancialInformationViewModel>()
                .ForMember(c => c.ProviderId, opt => opt.Ignore())
                .ForMember(c => c.Provider, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<RelProviderProduct, RelProviderProductViewModel>().ReverseMap();
            CreateMap<RelProviderStation, RelProviderStationViewModel>().ReverseMap();
            CreateMap<RelQuoteRequestProvider, RelQuoteRequestProviderViewModel>().ReverseMap();
        }
    }
}
