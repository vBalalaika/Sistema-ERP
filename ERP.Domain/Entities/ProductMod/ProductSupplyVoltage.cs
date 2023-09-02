using ERP.Domain.Common;
using System;

namespace ERP.Domain.Entities.ProductMod
{
    public class ProductSupplyVoltage : IAuditableBaseEntity, IConcurrencyToken
    {
        public Product Product { get; set; }
        public int ProductId { get; set; }

        public SupplyVoltage SupplyVoltage { get; set; }
        public int SupplyVoltageId { get; set; }

        public int ConcurrencyToken { get; set; }
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
    }
}
