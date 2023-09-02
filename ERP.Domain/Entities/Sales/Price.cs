using ERP.Domain.Common;
using ERP.Domain.Entities.ProductMod;
using System;

namespace ERP.Domain.Entities.Sales
{
    public class Price : IAuditableBaseEntity, IConcurrencyToken
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int? StructureId { get; set; }
        public Structure Structure { get; set; }

        public decimal PriceAll { get; set; }
        public DateTime? PriceAllDate { get; set; }
        public string PriceAllObservations { get; set; }

        public decimal PriceArg { get; set; }
        public DateTime? PriceArgDate { get; set; }
        public string PriceArgObservations { get; set; }

        public decimal PriceMx { get; set; }
        public DateTime? PriceMxDate { get; set; }
        public string PriceMxObservations { get; set; }

        //public DateTime PriceDate { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int ConcurrencyToken { get; set; }
    }
}