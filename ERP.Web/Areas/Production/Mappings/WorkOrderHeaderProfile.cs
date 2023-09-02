using AutoMapper;
using ERP.Application.DTOs.Entities.Production;
using ERP.Domain.Entities.Production;
using ERP.Domain.Entities.ProductMod;
using ERP.Domain.Entities.Sales;
using ERP.Web.Areas.Production.Models;
using ERP.Web.Areas.ProductMod.Models;
using ERP.Web.Areas.Sales.Models;

namespace ERP.Web.Areas.Production.Mappings
{
    public class WorkOrderHeaderProfile : Profile
    {
        public WorkOrderHeaderProfile()
        {
            CreateMap<WorkOrderHeaderDTO, WorkOrderHeaderViewModel>().ReverseMap();
            CreateMap<WorkOrderHeader, WorkOrderHeaderViewModel>().ReverseMap();
            CreateMap<WorkOrder, WorkOrderViewModel>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailViewModel>().ReverseMap();
            CreateMap<WorkOrderItem, WorkOrderItemViewModel>().ReverseMap();
            CreateMap<WorkActivity, WorkActivityViewModel>().ReverseMap();
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<RelProductStation, RelProductStationViewModel>().ReverseMap();
            CreateMap<Station, StationViewModel>().ReverseMap();

        }
    }
}
