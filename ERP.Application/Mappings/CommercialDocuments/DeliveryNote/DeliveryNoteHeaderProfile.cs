using AutoMapper;
using ERP.Application.DTOs.Entities.CommercialDocuments.DeliveryNote;
using ERP.Domain.Entities.CommercialDocuments.DeliveryNote;

namespace ERP.Application.Mappings.CommercialDocuments.DeliveryNote
{
    public class DeliveryNoteHeaderProfile : Profile
    {
        public DeliveryNoteHeaderProfile()
        {
            CreateMap<DeliveryNoteHeader, DeliveryNoteHeaderDTO>().ReverseMap();
        }
    }
}
