using AutoMapper;
using ERP.Application.DTOs.Entities.CommercialDocuments.DeliveryNote;
using ERP.Domain.Entities.CommercialDocuments.DeliveryNote;

namespace ERP.Application.Mappings.CommercialDocuments.DeliveryNote
{
    public class DeliveryNoteDetailProfile : Profile
    {
        public DeliveryNoteDetailProfile()
        {
            CreateMap<DeliveryNoteDetail, DeliveryNoteDetailDTO>().ReverseMap();
        }
    }
}
