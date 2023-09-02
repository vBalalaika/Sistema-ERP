using AutoMapper;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Mappings.ProductMod
{
    internal class OperationProfile : Profile
    {
        public OperationProfile()
        {

            CreateMap<Operation, OperationDTO>().ReverseMap();
        }
    }
}
