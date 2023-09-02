using AutoMapper;
using ERP.Application.DTOs.Entities.Production;
using ERP.Web.Areas.Production.Models;

namespace ERP.Web.Areas.Production.Mappings
{
    public class WorkActionProfile : Profile
    {
        public WorkActionProfile()
        {
            CreateMap<WorkActionDTO, WorkActionViewModel>().ReverseMap();
        }
    }
}
