using AutoMapper;
using ERP.Application.DTOs.Entities.Production;
using ERP.Domain.Entities.Production;

namespace ERP.Application.Mappings.Production
{
    public class WorkOrderHeaderProfile : Profile
    {
        public WorkOrderHeaderProfile()
        {
            CreateMap<WorkOrderHeader, WorkOrderHeaderDTO>().ReverseMap();
        }
    }
}
