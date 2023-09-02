using AutoMapper;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Domain.Entities.ProductMod;


namespace ERP.Application.Mappings.ProductMod
{
    internal class FunctionalAreaProfile : Profile
    {
        public FunctionalAreaProfile()
        {
            CreateMap<FunctionalArea, FunctionalAreaDTO>().ReverseMap();
        }
    }

}

