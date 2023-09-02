using AutoMapper;
using ERP.Application.DTOs.Entities.Lists;
using ERP.Domain.Entities.Lists;
using ERP.Web.Areas.Lists.Models;

namespace ERP.Web.Areas.Lists.Mappings
{
    internal class StateProfile : Profile
    {
        public StateProfile()
        {
            CreateMap<StateDTO, StateViewModel>().ReverseMap();
            CreateMap<State, StateViewModel>().ReverseMap();
        }
    }
}
