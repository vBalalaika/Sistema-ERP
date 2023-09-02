using AutoMapper;
using ERP.Application.DTOs.Entities.Lists;
using ERP.Domain.Entities.Lists;

namespace ERP.Application.Mappings.Lists
{
    public class StateProfile : Profile
    {
        public StateProfile()
        {
            CreateMap<State, StateDTO>().ReverseMap();
        }
    }
}
