using AutoMapper;
using ERP.Application.DTOs.Entities.Commons;
using ERP.Domain.Entities.Commons;

namespace ERP.Application.Mappings.Commons
{
    public class OperationStateProfile : Profile
    {
        public OperationStateProfile()
        {
            CreateMap<OperationState, OperationStateDTO>().ReverseMap();
        }
    }
}
