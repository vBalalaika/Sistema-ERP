using ERP.Domain.Entities.Commons;
using ERP.Domain.Entities.Lists;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;

namespace ERP.Web.Areas.Clients.Models
{
    public class ClientViewModel
    {
        // Razón social
        public string BusinessName { get; set; }

        // Nombre de contacto
        public string ContactName { get; set; }

        // Tipo de documento
        public int? DocumentTypeId { get; set; }
        public SelectList DocumentTypes { get; set; }
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
        public SelectList Countries { get; set; }
        public Country Country { get; set; }

        // Provincia
        public int? StateId { get; set; }
        public SelectList States { get; set; }
        public State State { get; set; }

        // Ciudad
        public int? CityId { get; set; }
        public SelectList Cities { get; set; }
        public City City { get; set; }

        // Código postal
        public string PostalCode { get; set; }

        // Estado de operación
        public int? OperationStateId { get; set; }
        public SelectList OperationStates { get; set; }
        public OperationState OperationState { get; set; }

        // Estado
        public bool IsActive { get; set; }

        public bool Bought { get; set; } = false;

        public string ClientDocument { get; set; }
        public string Comments { get; set; }

        public int Id { get; set; }
        public int ConcurrencyToken { get; set; }

        public string BranchCompany { get; set; }
        public string SizeCompany { get; set; }
        public string ProductionLevel { get; set; }
        public string IndustryServed { get; set; }

        public bool calledFromAnotherArea { get; set; }

    }
}
