using AutoMapper;
using ERP.Application.DTOs.Entities.Clients;
using ERP.Domain.Entities.Clients;
using ERP.Web.Areas.Clients.Models;

namespace ERP.Web.Areas.Clients.Mappings
{
    internal class CommunicationProfile : Profile
    {
        public CommunicationProfile()
        {
            CreateMap<CommunicationDTO, CommunicationViewModel>().ReverseMap();
            CreateMap<ConsultedProduct, ConsultedProductViewModel>().ReverseMap();
            CreateMap<SaleOperation, SaleOperationViewModel>().ReverseMap();
        }
    }
}
