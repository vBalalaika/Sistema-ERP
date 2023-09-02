using AutoMapper;
using ERP.Application.DTOs.Entities.Production;
using ERP.Domain.Entities.Production;
using ERP.Web.Areas.Production.Models;

namespace ERP.Web.Areas.Production.Mappings
{
    public class WorkOrderItemProfile : Profile
    {
        public WorkOrderItemProfile()
        {
            CreateMap<WorkOrderItemDTO, WorkOrderItemViewModel>().ReverseMap();
            CreateMap<WorkActivity, WorkActivityViewModel>().ReverseMap();
        }
    }
}
