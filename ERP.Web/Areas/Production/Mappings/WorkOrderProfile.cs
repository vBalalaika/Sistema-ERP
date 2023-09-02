using AutoMapper;
using ERP.Application.DTOs.Entities.Production;
using ERP.Domain.Entities.Production;
using ERP.Web.Areas.Production.Models;

namespace ERP.Web.Areas.Production.Mappings
{
    public class WorkOrderProfile : Profile
    {
        public WorkOrderProfile()
        {
            CreateMap<WorkOrderDTO, WorkOrderViewModel>().ReverseMap();
            CreateMap<WorkOrderItem, WorkOrderItemViewModel>().ReverseMap();
        }
    }
}
