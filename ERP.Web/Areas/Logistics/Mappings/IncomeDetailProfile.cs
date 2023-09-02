using AutoMapper;
using ERP.Application.DTOs.Entities.Logistics.Incomes;
using ERP.Domain.Entities.Commons;
using ERP.Domain.Entities.Logistics.Incomes;
using ERP.Domain.Entities.ProductMod;
using ERP.Web.Areas.Logistics.Models;
using ERP.Web.Areas.ProductMod.Models;

namespace ERP.Web.Areas.Logistics.Mappings
{
    public class IncomeDetailProfile : Profile
    {
        public IncomeDetailProfile()
        {
            CreateMap<IncomeDetailDTO, IncomeDetailViewModel>().ReverseMap();

            CreateMap<IncomeHeader, IncomeHeaderViewModel>().ReverseMap();
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<Structure, StructureViewModel>().ReverseMap();
            CreateMap<UnitMeasure, UnitMeasureViewModel>().ReverseMap();
            CreateMap<IncomeState, IncomeStateViewModel>().ReverseMap();
        }
    }
}
