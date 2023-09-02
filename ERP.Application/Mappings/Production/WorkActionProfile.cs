using AutoMapper;
using ERP.Application.DTOs.Entities.Production;
using ERP.Domain.Entities.Production;

namespace ERP.Application.Mappings.Production
{
    public class WorkActionProfile : Profile
    {
        public WorkActionProfile()
        {
            CreateMap<WorkAction, WorkActionDTO>().ReverseMap();
        }
    }
}
