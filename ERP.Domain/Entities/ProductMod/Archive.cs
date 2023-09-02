using ERP.Domain.Common;
using System;

namespace ERP.Domain.Entities.ProductMod
{
    public class Archive : IAuditableBaseEntity, IConcurrencyToken
    {
        public string PathUrl { get; set; }

        public Product Product { get; set; }
        public int ProductId { get; set; }

        public ArchiveType? ArchiveType { get; set; }
        public int? ArchiveTypeId { get; set; }

        public int ConcurrencyToken { get; set; }
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
    }
}
