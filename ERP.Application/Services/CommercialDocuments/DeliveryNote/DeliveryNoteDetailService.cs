using AutoMapper;
using ERP.Application.DTOs.Entities.CommercialDocuments.DeliveryNote;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services.CommercialDocuments.DeliveryNote;
using ERP.Domain.Entities.CommercialDocuments.DeliveryNote;

namespace ERP.Application.Services.CommercialDocuments.DeliveryNote
{
    public class DeliveryNoteDetailService : GenericService<DeliveryNoteDetail, DeliveryNoteDetailDTO>, IDeliveryNoteDetailService
    {
        public DeliveryNoteDetailService(IUnitOfWork unitOfWOrk, IMapper mapper) : base(unitOfWOrk, mapper)
        {

        }
    }
}
