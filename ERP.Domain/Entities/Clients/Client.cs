using ERP.Domain.Common;
using ERP.Domain.Entities.Commons;
using ERP.Domain.Entities.Lists;
using ERP.Domain.Entities.Sales;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ERP.Domain.Entities.Clients
{

    public class Client : IAuditableBaseEntity, IConcurrencyToken
    {
        // Razón social
        [Required]
        public string BusinessName { get; set; }

        // Nombre de contacto
        public string ContactName { get; set; }

        // Tipo de documento
        public int? DocumentTypeId { get; set; }
        public DocumentType DocumentType { get; set; }

        // Número de documento
        public string DocumentNumber { get; set; }

        // Fecha de nacimiento
        public DateTime? DateOfBirth { get; set; }

        // Celular
        public string CellPhoneNumber { get; set; }

        // Email
        [EmailAddress]
        public string Email { get; set; }

        // Dirección
        public string Address { get; set; }

        // País
        public int CountryId { get; set; }

        public Country Country { get; set; }

        // Provincia
        public int? StateId { get; set; }
        public State State { get; set; }

        // Ciudad
        public int? CityId { get; set; }
        public City City { get; set; }

        // Código postal
        public string PostalCode { get; set; }

        // Estado de operación
        public int? OperationStateId { get; set; }
        public OperationState OperationState { get; set; }

        // Estado
        public bool IsActive { get; set; }

        public string ClientDocument { get; set; }
        public string Comments { get; set; }

        public string IsOnColppy { get; set; }

        public string BranchCompany { get; set; }
        public string SizeCompany { get; set; }
        public string ProductionLevel { get; set; }
        public string IndustryServed { get; set; }

        // Un cliente tiene muchos recordatorios
        public ICollection<Reminder> Reminders { get; set; }
        public ICollection<Communication> Communications { get; set; }
        public ICollection<OrderHeader> OrderHeaders { get; set; }
        public ICollection<SaleOperation> SaleOperations { get; set; }

        #region Propiedades heredadas desde las interfaces

        public int ConcurrencyToken { get; set; }
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }

        #endregion
    }
}
