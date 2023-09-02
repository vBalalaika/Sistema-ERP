using ERP.Domain.Common;
using ERP.Domain.Entities.Commons;
using ERP.Domain.Entities.ProductMod;
using System;

namespace ERP.Domain.Entities.Purchases.Providers
{
    public class RelProviderProduct : IAuditableBaseEntity, IConcurrencyToken
    {
        public int ProviderId { get; set; }
        public Provider Provider { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public string Brand { get; set; }

        public string ProviderCode { get; set; }

        public int? UnitMeasureId { get; set; }
        public UnitMeasure? UnitMeasure { get; set; }

        public decimal? Width { get; set; }
        public int? WidthUnitMeasureId { get; set; }
        public UnitMeasure? WidthUnitMeasure { get; set; }

        public decimal? Lenght { get; set; }
        public int? LengthUnitMeasureId { get; set; }
        public UnitMeasure? LengthUnitMeasure { get; set; }

        public decimal? Weight { get; set; }
        public int? WeightUnitMeasureId { get; set; }
        public UnitMeasure? WeightUnitMeasure { get; set; }

        public decimal? Height { get; set; }
        public int? HeightUnitMeasureId { get; set; }
        public UnitMeasure? HeightUnitMeasure { get; set; }

        public decimal? PresentationQuantity { get; set; }
        public int? PresentationUnitMeasureId { get; set; }
        public UnitMeasure PresentationUnitMeasure { get; set; }

        public string Observations { get; set; }

        public decimal DollarPrice { get; set; }
        public decimal PesosPrice { get; set; }
        public int? PriceUnitMeasureId { get; set; }
        public UnitMeasure? PriceUnitMeasure { get; set; }

        public int ConcurrencyToken { get; set; }
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
    }
}
