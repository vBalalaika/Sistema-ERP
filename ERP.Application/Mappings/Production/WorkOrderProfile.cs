using AutoMapper;
using ERP.Application.DTOs.Entities.Production;
using ERP.Domain.Entities.Production;

namespace ERP.Application.Mappings.Production
{
    public class WorkOrderProfile : Profile
    {
        public WorkOrderProfile()
        {
            CreateMap<WorkOrder, WorkOrderDTO>().ReverseMap();
        }
    }
}
