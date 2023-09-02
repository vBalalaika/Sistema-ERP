using ERP.Domain.Common;
using System;

namespace ERP.Domain.Entities.ProductMod
{
#pragma warning disable CS8632// nullable enable,sino tira warning en linea 17,18,etc..
    public class Company : IAuditableBaseEntity, IConcurrencyToken
    {

        public string BusinessName { get; set; }

        //public int riId {get;set;} Tabla?
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? PostalCode { get; set; }
        public int CUIT { get; set; }
        public int GrossIncome { get; set; }
        public DateTime ActivityStartDate { get; set; }
        public string? PhoneNumber { get; set; }
        public string Email { get; set; }
        public string? Web { get; set; }
        public string? Facebook { get; set; }
        public string? Skype { get; set; }
        public Boolean IsActive { get; set; }
        //public Image Logo { get; set; } Como guardarlo?
        //public string tmId { get; set; } tipado?Tabla?
        public bool IsPerceptionAgent { get; set; }
        public int Admin { get; set; }
        public int IdClientAuto { get; set; }
        public int IdSupplierAuto { get; set; }//CodProAuto, pro=Proveedor/producto?
        public string? Subjet { get; set; }
        public string? Body { get; set; }
        public bool CostHasIVA { get; set; }
        public string? Eslogan { get; set; }
        public bool UseAFIPTesting { get; set; }
        public int? IdProvinceAuto { get; set; }//CodProvAuto, prov=Proveedor/provincia?
        public bool? DispatchUpdatedCtaCte { get; set; }
        public int IVADefault { get; set; } //Tipado?



        public int ConcurrencyToken { get; set; }
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
    }
}
