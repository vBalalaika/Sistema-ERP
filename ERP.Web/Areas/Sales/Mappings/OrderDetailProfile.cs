using AutoMapper;
using ERP.Application.DTOs.Entities.Sales;
using ERP.Domain.Entities.ProductMod;
using ERP.Domain.Entities.Sales;
using ERP.Web.Areas.ProductMod.Models;
using ERP.Web.Areas.Sales.Models;

namespace ERP.Web.Areas.Sales.Mappings
{
    internal class OrderDetailProfile : Profile
    {
        public OrderDetailProfile()
        {
            CreateMap<OrderDetailDTO, OrderDetailViewModel>().ReverseMap();
            CreateMap<OrderHeader, OrderHeaderViewModel>().ReverseMap();

            CreateMap<OrderState, OrderStateViewModel>().ReverseMap();
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<Structure, StructureViewModel>().ReverseMap();
            CreateMap<SupplyVoltage, SupplyVoltageViewModel>().ReverseMap();
        }
    }
}
