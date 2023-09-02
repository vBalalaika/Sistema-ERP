using ERP.Domain.Common;
using ERP.Domain.Entities.Commons;
using ERP.Domain.Entities.Lists;
using ERP.Domain.Entities.Purchases.QuoteRequests;
using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities.Purchases.Providers
{
    public class Provider : IAuditableBaseEntity, IConcurrencyToken
    {
        public int Id { get; set; }

        #region Properties
        public string Name { get; set; }
        public string BusinessName { get; set; }
        public int? CountryId { get; set; }
        public Country Country { get; set; }
        public int? StateId { get; set; }
        public State State { get; set; }
        public int? CityId { get; set; }
        public City City { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public int DocumentTypeId { get; set; }
        public DocumentType DocumentType { get; set; }
        public string DocumentNumber { get; set; }
        public int IVAConditionId { get; set; }
        public IVACondition IVACondition { get; set; }
        public string GrossIncome { get; set; }
        public string WebPage { get; set; }
        public bool IsActive { get; set; }
        public string ProviderType { get; set; }
        public string Observations { get; set; }

        public int? ProfitID { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<Subsidiary> Subsidiaries { get; set; }
        public virtual ICollection<FinancialInformation> FinancialInformations { get; set; }

        public ICollection<RelProviderProduct> RelProviderProducts { get; set; } = new List<RelProviderProduct>();

        public ICollection<RelProviderStation> RelProviderStations { get; set; } = new List<RelProviderStation>();

        public ICollection<RelQuoteRequestProvider> RelQuoteRequestProviders { get; set; } = new List<RelQuoteRequestProvider>();
        #endregion

        public string IsOnColppy { get; set; } 

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int ConcurrencyToken { get; set; }
    }
}
