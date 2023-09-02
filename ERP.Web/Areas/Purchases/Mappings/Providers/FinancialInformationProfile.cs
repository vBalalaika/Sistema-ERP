using AutoMapper;
using ERP.Application.DTOs.Entities.Purchases.Providers;
using ERP.Web.Areas.Purchases.Models.Providers;

namespace ERP.Web.Areas.Purchases.Mappings.Providers
{
    internal class FinancialInformationProfile : Profile
    {
        public FinancialInformationProfile()
        {
            CreateMap<FinancialInformationDTO, FinancialInformationViewModel>().ReverseMap();
        }
    }
}
