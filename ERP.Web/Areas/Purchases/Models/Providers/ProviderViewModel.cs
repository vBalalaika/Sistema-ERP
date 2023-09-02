using ERP.Domain.Entities.Commons;
using ERP.Domain.Entities.Lists;
using ERP.Web.Areas.Purchases.Models.QuoteRequest;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ERP.Web.Areas.Purchases.Models.Providers
{
    public class ProviderViewModel
    {
        public int Id { get; set; }
        public int ConcurrencyToken { get; set; }

        public string Name { get; set; }
        public string BusinessName { get; set; }

        public int CountryId { get; set; }
        public SelectList Countries { get; set; }
        public Country Country { get; set; }
        public int? StateId { get; set; }
        public SelectList States { get; set; }
        public State State { get; set; }
        public int? CityId { get; set; }
        public SelectList Cities { get; set; }
        public City City { get; set; }

        public string Address { get; set; }
        public string PostalCode { get; set; }

        public int DocumentTypeId { get; set; }
        public SelectList DocumentTypes { get; set; }
        public DocumentType DocumentType { get; set; }
        public string DocumentNumber { get; set; }

        public int IVAConditionId { get; set; }
        public SelectList IVAConditions { get; set; }
        public IVACondition IVACondition { get; set; }

        public string GrossIncome { get; set; }
        public string WebPage { get; set; }

        public bool IsActive { get; set; }
        public string ProviderType { get; set; }
        public SelectList ProviderTypes { get; set; } = new SelectList(new List<string> { "Productos", "Servicios", "Transporte" });
        public string Observations { get; set; }

        public IList<ContactViewModel> Contacts { get; set; }

        public IList<SubsidiaryViewModel> Subsidiaries { get; set; }

        public IList<FinancialInformationViewModel> FinancialInformations { get; set; }

        public ICollection<RelProviderProductViewModel> RelProviderProducts { get; set; } = new List<RelProviderProductViewModel>();

        public ICollection<RelProviderStationViewModel> RelProviderStations { get; set; } = new List<RelProviderStationViewModel>();

        public ICollection<RelQuoteRequestProviderViewModel> RelQuoteRequestProviders { get; set; } = new List<RelQuoteRequestProviderViewModel>();

        public string getNameOrBussinessName
        {
            get
            {
                if (this.Name != null && this.Name != "")
                {
                    return this.Name;
                }
                else
                {
                    return this.BusinessName;
                }
            }
        }

        public string getBussinessNameOrName
        {
            get
            {
                if (this.BusinessName != null && this.BusinessName != "")
                {
                    return this.BusinessName;
                }
                else
                {
                    return this.Name;
                }
            }
        }

        public bool calledFromAnotherArea { get; set; }

        public ProviderViewModel()
        {
            Contacts = new List<ContactViewModel>();
            Subsidiaries = new List<SubsidiaryViewModel>();
            FinancialInformations = new List<FinancialInformationViewModel>();
        }
    }
}
