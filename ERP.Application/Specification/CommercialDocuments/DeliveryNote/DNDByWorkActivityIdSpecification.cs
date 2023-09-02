using ERP.Domain.Entities.CommercialDocuments.DeliveryNote;

namespace ERP.Application.Specification.CommercialDocuments.DeliveryNote
{
    public class DNDByWorkActivityIdSpecification : BaseSpecification<DeliveryNoteDetail>
    {
        public DNDByWorkActivityIdSpecification(int workActivityId) : base(dnd => dnd.WorkActivityId == workActivityId)
        {
            AddInclude(dnd => dnd.DeliveryNoteHeader);
            AddInclude(dnd => dnd.ProductItem);

            AddInclude($"{nameof(DeliveryNoteDetail.DeliveryNoteHeader)}.{nameof(DeliveryNoteHeader.Provider)}");
            AddInclude($"{nameof(DeliveryNoteDetail.DeliveryNoteHeader)}.{nameof(DeliveryNoteHeader.TransportProvider)}");
        }
    }
}
