using AutoMapper;
using ERP.Application.DTOs.Entities.Purchases.MissingProducts;
using ERP.Web.Areas.Purchases.Models.MissingProduct;

namespace ERP.Web.Areas.Purchases.Mappings.MissingProduct
{
    internal class StateMissingProductProfile : Profile
    {
        public StateMissingProductProfile()
        {
            CreateMap<PurchaseStateDTO, StateMissingProductViewModel>().ReverseMap();
        }
    }
}
