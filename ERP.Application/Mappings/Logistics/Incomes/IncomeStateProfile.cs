using AutoMapper;
using ERP.Application.DTOs.Entities.Logistics.Incomes;
using ERP.Domain.Entities.Logistics.Incomes;

namespace ERP.Application.Mappings.Logistics.Incomes
{
    public class IncomeStateProfile : Profile
    {
        public IncomeStateProfile()
        {
            CreateMap<IncomeState, IncomeStateDTO>().ReverseMap();
        }
    }
}
