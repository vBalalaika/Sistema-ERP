using ERP.Domain.Common;
using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities.ProductMod
{
    public class Structure : IAuditableBaseEntity, IConcurrencyToken
    {
        public string Description { get; set; }
        public Boolean IsBase { get; set; }
        public bool IsStandard { get; set; }


        public Product Product { get; set; }
        public int ProductId { get; set; }

        public StructureVersion LastVersion { get; set; }
        public int LastVersionId { get; set; }

        public SupplyVoltage? SupplyVoltage { get; set; }
        public int? SupplyVoltageId { get; set; }

        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int ConcurrencyToken { get; set; }

        public ICollection<StructureItem> StructureItems { get; set; }
    }
}
