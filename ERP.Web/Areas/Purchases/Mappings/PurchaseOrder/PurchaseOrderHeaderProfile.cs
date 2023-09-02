using AutoMapper;
using ERP.Application.DTOs.Entities.Purchases.PurchaseOrders;
using ERP.Domain.Entities.Purchases.Providers;
using ERP.Domain.Entities.Purchases.PurchaseOrders;
using ERP.Domain.Entities.Purchases.QuoteRequests;
using ERP.Web.Areas.Purchases.Models.Providers;
using ERP.Web.Areas.Purchases.Models.PurchaseOrder;
using ERP.Web.Areas.Purchases.Models.QuoteRequest;

namespace ERP.Web.Areas.Purchases.Mappings.PurchaseOrder
{
    public class PurchaseOrderHeaderProfile : Profile
    {
        public PurchaseOrderHeaderProfile()
        {
            CreateMap<PurchaseOrderHeaderDTO, PurchaseOrderHeaderViewModel>().ReverseMap();
            CreateMap<PurchaseOrderDetail, PurchaseOrderDetailViewModel>().ReverseMap();

            CreateMap<QuoteRequestHeader, QuoteRequestHeaderViewModel>().ReverseMap();
            CreateMap<Provider, ProviderViewModel>().ReverseMap();
        }
    }
}
