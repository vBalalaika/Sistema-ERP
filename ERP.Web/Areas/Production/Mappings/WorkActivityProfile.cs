using AutoMapper;
using ERP.Application.DTOs.Entities.Production;
using ERP.Domain.Entities.Production;
using ERP.Web.Areas.Production.Models;

namespace ERP.Web.Areas.Production.Mappings
{
    public class WorkActivityProfile : Profile
    {
        public WorkActivityProfile()
        {
            CreateMap<WorkActivityDTO, WorkActivityViewModel>().ReverseMap();
            CreateMap<WorkAction, WorkActionViewModel>().ReverseMap();
            CreateMap<WorkOrderItem, WorkOrderItemViewModel>().ReverseMap();
        }
    }
}
