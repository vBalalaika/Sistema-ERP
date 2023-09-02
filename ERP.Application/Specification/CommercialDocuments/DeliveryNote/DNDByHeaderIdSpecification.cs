using ERP.Domain.Entities.CommercialDocuments.DeliveryNote;

namespace ERP.Application.Specification.CommercialDocuments.DeliveryNote
{
    public class DNDByHeaderIdSpecification : BaseSpecification<DeliveryNoteDetail>
    {
        public DNDByHeaderIdSpecification(int headerId) : base(dnd => dnd.DeliveryNoteHeaderId == headerId)
        {
            AddInclude(dnd => dnd.DeliveryNoteHeader);
        }
    }
}
