using ERP.Domain.Common;
using System;
#pragma warning disable CS8632
namespace ERP.Domain.Entities.ProductMod
{
    public class SubCategory : IAuditableBaseEntity, IConcurrencyToken
    {
        public string Description { get; set; }
        public Boolean IsActive { get; set; }
        public string? Prefix { get; set; }

        public Category Category { get; set; }
        public int CategoryId { get; set; }

        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int ConcurrencyToken { get; set; }


    }
}
