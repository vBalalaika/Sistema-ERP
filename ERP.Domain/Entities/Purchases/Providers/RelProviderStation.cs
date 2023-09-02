using ERP.Domain.Common;
using ERP.Domain.Entities.Commons;
using ERP.Domain.Entities.ProductMod;
using System;

namespace ERP.Domain.Entities.Purchases.Providers
{
    public class RelProviderStation : IAuditableBaseEntity, IConcurrencyToken
    {
        public int Id { get; set; }

        public int ProviderId { get; set; }
        public Provider Provider { get; set; }

        public int StationId { get; set; }
        public Station Station { get; set; }

        public decimal DollarPrice { get; set; }
        public decimal PesosPrice { get; set; }
        public int? PriceUnitMeasureId { get; set; }
        public UnitMeasure PriceUnitMeasure { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int ConcurrencyToken { get; set; }
    }
}
