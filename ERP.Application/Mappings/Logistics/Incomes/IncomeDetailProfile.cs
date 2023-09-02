using AutoMapper;
using ERP.Application.DTOs.Entities.Logistics.Incomes;
using ERP.Domain.Entities.Logistics.Incomes;

namespace ERP.Application.Mappings.Logistics.Incomes
{
    public class IncomeDetailProfile : Profile
    {
        public IncomeDetailProfile()
        {
            CreateMap<IncomeDetail, IncomeDetailDTO>().ReverseMap();
        }
    }
}
