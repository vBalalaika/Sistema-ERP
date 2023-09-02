using AutoMapper;
using ERP.Application.DTOs.Entities.Purchases.PurchaseOrders;
using ERP.Domain.Entities.CommercialDocuments.DeliveryNote;
using ERP.Domain.Entities.Commons;
using ERP.Domain.Entities.ProductMod;
using ERP.Domain.Entities.Purchases.PurchaseOrders;
using ERP.Web.Areas.Logistics.Models.DeliveryNote;
using ERP.Web.Areas.ProductMod.Models;
using ERP.Web.Areas.Purchases.Models.PurchaseOrder;

namespace ERP.Web.Areas.Purchases.Mappings.PurchaseOrder
{
    public class ServicePODetailProfile : Profile
    {
        public ServicePODetailProfile()
        {
            CreateMap<ServicePODetailDTO, ServicePODetailViewModel>().ReverseMap();

            CreateMap<ServicePOHeader, ServicePOHeaderViewModel>().ReverseMap();
            CreateMap<DeliveryNoteDetail, DeliveryNoteDetailViewModel>().ReverseMap();
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<UnitMeasure, UnitMeasureViewModel>().ReverseMap();
        }
    }
}
