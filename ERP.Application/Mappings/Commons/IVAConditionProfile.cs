using AutoMapper;
using ERP.Application.DTOs.Entities.Commons;
using ERP.Domain.Entities.Commons;

namespace ERP.Application.Mappings.Commons
{
    public class IVAConditionProfile : Profile
    {
        public IVAConditionProfile()
        {
            CreateMap<IVACondition, IVAConditionDTO>().ReverseMap();
        }
    }
}
