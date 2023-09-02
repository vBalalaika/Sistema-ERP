using ERP.Domain.Common;
using System;
#pragma warning disable CS8632
namespace ERP.Domain.Entities.ProductMod
{
    public class StructureVersion : IAuditableBaseEntity, IConcurrencyToken
    {
        public int VersionNumber { get; set; }
        public string? Comment { get; set; }

        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int ConcurrencyToken { get; set; }
    }
}
