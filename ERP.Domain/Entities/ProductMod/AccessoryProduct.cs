using ERP.Domain.Common;
using System;
namespace ERP.Domain.Entities.ProductMod
{
    public class AccessoryProduct : IAuditableBaseEntity, IConcurrencyToken
    {
        public Product Product { get; set; }
        public int IdProduct { get; set; }
        public Product ProductAccessory { get; set; }
        public int IdProductAccessory { get; set; }

        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int ConcurrencyToken { get; set; }
    }
}
