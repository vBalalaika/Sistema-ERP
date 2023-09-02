using AutoMapper;
using ERP.Application.DTOs.Entities.Production;
using ERP.Domain.Entities.Production;

namespace ERP.Application.Mappings.Production
{
    public class WorkOrderItemProfile : Profile
    {
        public WorkOrderItemProfile()
        {
            CreateMap<WorkOrderItem, WorkOrderItemDTO>().ReverseMap();
        }
    }
}
