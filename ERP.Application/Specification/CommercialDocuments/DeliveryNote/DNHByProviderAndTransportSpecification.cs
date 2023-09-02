using ERP.Domain.Entities.CommercialDocuments.DeliveryNote;

namespace ERP.Application.Specification.CommercialDocuments.DeliveryNote
{
    public class DNHByProviderAndTransportSpecification : BaseSpecification<DeliveryNoteHeader>
    {
        public DNHByProviderAndTransportSpecification(int providerId, int transportProviderId) : base(dnh => dnh.ProviderId == providerId && dnh.TransportProviderId == transportProviderId)
        {

        }
    }
}
