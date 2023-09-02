using ERP.Domain.Common;
using ERP.Domain.Entities.Purchases.Providers;
using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities.CommercialDocuments.DeliveryNote
{
    public class DeliveryNoteHeader : IAuditableBaseEntity, IConcurrencyToken
    {
        public int Id { get; set; }

        public int ProviderId { get; set; }
        public Provider Provider { get; set; }
        public int TransportProviderId { get; set; }
        public Provider TransportProvider { get; set; }
        public DateTime Date { get; set; }
        public string Number { get; set; }
        public ICollection<DeliveryNoteDetail> DeliveryNoteDetails { get; set; }
        public string Comments { get; set; }

        // Sirve para validar que si se exporto a PDF ya no se puede combinar con otro remito.
        public bool wasExportedToPDF { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int ConcurrencyToken { get; set; }
    }
}
