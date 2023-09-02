using ERP.Domain.Common;
using System;

namespace ERP.Domain.Entities.ProductMod
{
    public class RelProductStation : IAuditableBaseEntity, IConcurrencyToken
    {
        public decimal? Time { get; set; }
        public int? Order { get; set; }
        public decimal? Cost { get; set; }

        public Product Product { get; set; }
        public int ProductId { get; set; }
        public Station Station { get; set; }
        public int StationId { get; set; }

        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int ConcurrencyToken { get; set; }

    }
}
