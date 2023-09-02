using AutoMapper;
using ERP.Application.DTOs.Entities.Production;
using ERP.Domain.Entities.Production;

namespace ERP.Application.Mappings.Production
{
    public class WorkActivityProfile : Profile
    {
        public WorkActivityProfile()
        {
            CreateMap<WorkActivity, WorkActivityDTO>().ReverseMap();
        }
    }
}
