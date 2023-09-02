using AutoMapper;
using ERP.Application.DTOs.Entities.Clients;
using ERP.Domain.Entities.Clients;
using ERP.Domain.Entities.Sales;
using ERP.Web.Areas.Clients.Models;
using ERP.Web.Areas.Sales.Models;

namespace ERP.Web.Areas.Clients.Mappings
{
    internal class SaleOperationProfile : Profile
    {
        public SaleOperationProfile()
        {
            CreateMap<SaleOperationDTO, SaleOperationViewModel>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailViewModel>().ReverseMap();
            CreateMap<Communication, CommunicationViewModel>().ReverseMap();
        }
    }
}
