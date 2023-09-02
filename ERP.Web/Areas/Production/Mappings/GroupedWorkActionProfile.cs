using AutoMapper;
using ERP.Application.DTOs.Entities.Production;
using ERP.Web.Areas.Production.Models.WorkAction;

namespace ERP.Web.Areas.Production.Mappings
{
    public class GroupedWorkActionProfile : Profile
    {
        public GroupedWorkActionProfile()
        {
            CreateMap<GroupedWorkActionDTO, GroupedWorkActionViewModel>().ReverseMap();
        }
    }
}
