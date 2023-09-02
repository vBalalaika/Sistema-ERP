using AutoMapper;
using ERP.Application.DTOs.Entities.CommercialDocuments.DeliveryNote;
using ERP.Domain.Entities.CommercialDocuments.DeliveryNote;
using ERP.Domain.Entities.Purchases.Providers;
using ERP.Web.Areas.Logistics.Models.DeliveryNote;
using ERP.Web.Areas.Purchases.Models.Providers;

namespace ERP.Web.Areas.Logistics.Mappings.DeliveryNote
{
    public class DeliveryNoteHeaderProfile : Profile
    {
        public DeliveryNoteHeaderProfile()
        {
            CreateMap<DeliveryNoteHeaderDTO, DeliveryNoteHeaderViewModel>().ReverseMap();

            CreateMap<Provider, ProviderViewModel>().ReverseMap();
            CreateMap<DeliveryNoteDetail, DeliveryNoteDetailViewModel>().ReverseMap();
        }
    }
}
