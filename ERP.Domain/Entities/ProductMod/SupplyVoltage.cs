using ERP.Domain.Common;
using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities.ProductMod
{
    public class SupplyVoltage : IAuditableBaseEntity, IConcurrencyToken
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Boolean IsActive { get; set; }


        public int ConcurrencyToken { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }

        public ICollection<ProductSupplyVoltage> ProductSupplyVoltage { get; set; }
    }
}
