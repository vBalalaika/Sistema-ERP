using AutoMapper;
using ERP.Application.DTOs.Entities.Production;
using ERP.Domain.Entities.Production;

namespace ERP.Application.Mappings.Production
{
    public class GroupedWorkActionProfile : Profile
    {
        public GroupedWorkActionProfile()
        {
            CreateMap<GroupedWorkAction, GroupedWorkActionDTO>().ReverseMap();
        }
    }
}
