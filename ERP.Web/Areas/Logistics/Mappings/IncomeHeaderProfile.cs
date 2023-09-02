using AutoMapper;
using ERP.Application.DTOs.Entities.Logistics.Incomes;
using ERP.Domain.Entities.Logistics.Incomes;
using ERP.Domain.Entities.ProductMod;
using ERP.Domain.Entities.Purchases.Providers;
using ERP.Web.Areas.Logistics.Models;
using ERP.Web.Areas.ProductMod.Models;
using ERP.Web.Areas.Purchases.Models.Providers;

namespace ERP.Web.Areas.Logistics.Mappings
{
    public class IncomeHeaderProfile : Profile
    {
        public IncomeHeaderProfile()
        {
            CreateMap<IncomeHeaderDTO, IncomeHeaderViewModel>().ReverseMap();

            CreateMap<Provider, ProviderViewModel>().ReverseMap();
            CreateMap<Station, StationViewModel>().ReverseMap();
            CreateMap<IncomeDetail, IncomeDetailViewModel>().ReverseMap();
        }
    }
}
