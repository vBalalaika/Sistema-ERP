using AutoMapper;
using ERP.Application.DTOs.Entities.Purchases.PurchaseOrders;
using ERP.Domain.Entities.Commons;
using ERP.Domain.Entities.ProductMod;
using ERP.Domain.Entities.Purchases.PurchaseOrders;
using ERP.Web.Areas.ProductMod.Models;
using ERP.Web.Areas.Purchases.Models.MissingProduct;
using ERP.Web.Areas.Purchases.Models.PurchaseOrder;

namespace ERP.Web.Areas.Purchases.Mappings.PurchaseOrder
{
    public class PurchaseOrderDetailProfile : Profile
    {
        public PurchaseOrderDetailProfile()
        {
            CreateMap<PurchaseOrderDetailDTO, PurchaseOrderDetailViewModel>().ReverseMap();

            CreateMap<PurchaseOrderHeader, PurchaseOrderHeaderViewModel>().ReverseMap();
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<ERP.Domain.Entities.Purchases.MissingProducts.MissingProduct, MissingProductViewModel>().ReverseMap();
            CreateMap<UnitMeasure, UnitMeasureViewModel>().ReverseMap();
        }
    }
}
