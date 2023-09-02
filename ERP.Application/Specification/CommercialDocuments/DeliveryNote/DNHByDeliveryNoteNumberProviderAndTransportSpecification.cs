using ERP.Domain.Entities.CommercialDocuments.DeliveryNote;

namespace ERP.Application.Specification.CommercialDocuments.DeliveryNote
{
    public class DNHByDeliveryNoteNumberProviderAndTransportSpecification : BaseSpecification<DeliveryNoteHeader>
    {
        public DNHByDeliveryNoteNumberProviderAndTransportSpecification(int providerId, int transportProviderId, string deliveryNoteNumber) : base(dnh => dnh.ProviderId == providerId && dnh.TransportProviderId == transportProviderId && dnh.Number.Trim().Equals(deliveryNoteNumber.Trim()))
        {

        }
    }
}
