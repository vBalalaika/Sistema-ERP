using AutoMapper;
using ERP.Application.DTOs.Entities.Logistics.Incomes;
using ERP.Domain.Entities.Logistics.Incomes;

namespace ERP.Application.Mappings.Logistics.Incomes
{
    public class IncomeHeaderProfile : Profile
    {
        public IncomeHeaderProfile()
        {
            CreateMap<IncomeHeader, IncomeHeaderDTO>().ReverseMap();
        }
    }
}
