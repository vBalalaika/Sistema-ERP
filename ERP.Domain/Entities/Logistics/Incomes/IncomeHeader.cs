using ERP.Domain.Common;
using ERP.Domain.Entities.ProductMod;
using ERP.Domain.Entities.Purchases.Providers;
using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities.Logistics.Incomes
{
    public class IncomeHeader : IAuditableBaseEntity, IConcurrencyToken
    {
        public DateTime IncomeDate { get; set; }
        public int ProviderId { get; set; }
        public Provider Provider { get; set; }
        public int TransportProviderId { get; set; }
        public Provider TransportProvider { get; set; }
        public string DeliveryNoteNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public int? ExternalProcessStationId { get; set; }
        public Station ExternalProcessStation { get; set; }
        public string Comments { get; set; }
        public bool OwnTransport { get; set; } = false;

        public ICollection<IncomeDetail> IncomeDetails { get; set; } = new List<IncomeDetail>();

        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int ConcurrencyToken { get; set; }
    }
}
