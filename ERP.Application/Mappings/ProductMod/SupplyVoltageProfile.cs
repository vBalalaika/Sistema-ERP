using AutoMapper;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Mappings.ProductMod
{
    internal class SupplyVoltageProfile : Profile
    {
        public SupplyVoltageProfile()
        {
            CreateMap<SupplyVoltage, SupplyVoltageDTO>().ReverseMap();
        }
    }
}
