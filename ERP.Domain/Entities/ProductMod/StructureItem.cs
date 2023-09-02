using ERP.Domain.Common;
using System;

namespace ERP.Domain.Entities.ProductMod
{
#pragma warning disable CS8632
    public class StructureItem : IAuditableBaseEntity, IConcurrencyToken
    {
        public int Quantity { get; set; }
        public int Order { get; set; }

        public StructureVersion Version { get; set; }
        public int VersionId { get; set; }
        public Product? SonProduct { get; set; }
        public int? SonProductId { get; set; }
        public Structure? SonStructure { get; set; }
        public int? SonStructureId { get; set; }

        public Structure? Structure { get; set; }
        public int? StructureId { get; set; }

        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int ConcurrencyToken { get; set; }
    }
}
