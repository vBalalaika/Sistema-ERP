using AutoMapper;
using ERP.Application.DTOs.Entities.Commons;
using ERP.Domain.Entities.Commons;

namespace ERP.Application.Mappings.Commons
{
    internal class UnitMeasureProfile : Profile
    {
        public UnitMeasureProfile()
        {
            CreateMap<UnitMeasure, UnitMeasureDTO>().ReverseMap();
        }
    }
}
