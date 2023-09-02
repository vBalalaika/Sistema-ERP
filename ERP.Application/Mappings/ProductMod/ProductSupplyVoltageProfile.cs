using AutoMapper;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Mappings.ProductMod
{
    internal class ProductSupplyVoltageProfile : Profile
    {
        public ProductSupplyVoltageProfile()
        {
            CreateMap<ProductSupplyVoltage, ProductSupplyVoltageDTO>().ReverseMap();
        }
    }
}
