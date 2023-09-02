using AutoMapper;
using ERP.Application.DTOs.Entities.Clients;
using ERP.Domain.Entities.Clients;

namespace ERP.Application.Mappings.Clients
{
    public class SaleOperationProfile : Profile
    {
        public SaleOperationProfile()
        {
            CreateMap<SaleOperation, SaleOperationDTO>().ReverseMap();
        }
    }
}
