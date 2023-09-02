using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace ERP.Web.Areas.Purchases.Models.QuoteRequest
{
    public class QuoteRequestHeaderViewModel
    {
        public int Id { get; set; }
        public int ConcurrencyToken { get; set; }
        public DateTime Date { get; set; }
        public string Comments { get; set; }

        public SelectList Products { get; set; }

        public DateTime? ResponseDate { get; set; }
        public SelectList Providers { get; set; }
        public ICollection<QuoteRequestDetailViewModel> QuoteRequestDetails { get; set; } = new List<QuoteRequestDetailViewModel>();
        public ICollection<RelQuoteRequestProviderViewModel> RelQuoteRequestProviders { get; set; } = new List<RelQuoteRequestProviderViewModel>();

        public virtual List<QuoteRequestResponseHeaderViewModel> QuoteRequestResponseHeaders { get; set; }

        public int Number { get; set; }

        public bool isGenerateMissingProducts { get; set; } = false;
        public string getRelQuoteRequestProviders
        {
            get
            {
                string providers = "";
                int count = 0;
                foreach (var rpp in this.RelQuoteRequestProviders)
                {
                    if (rpp.Provider != null)
                    {
                        providers += rpp.Provider.getBussinessNameOrName;
                        count += 1;
                        if (count < this.RelQuoteRequestProviders.Count)
                        {
                            providers += ", ";
                        }
                    }
                }
                return providers;
            }
        }

        public string getQuoteRequestResponseNumbers
        {
            get
            {
                string getQuoteRequestResponseNumbers = "";
                int count = 0;
                if (this.QuoteRequestResponseHeaders != null)
                {
                    foreach (var qrrh in this.QuoteRequestResponseHeaders)
                    {
                        getQuoteRequestResponseNumbers += qrrh.Id;
                        count += 1;
                        if (count < this.QuoteRequestResponseHeaders.Count)
                        {
                            getQuoteRequestResponseNumbers += ", ";
                        }
                    }
                }

                return getQuoteRequestResponseNumbers;
            }
        }

        // Only for PDF report
        public string ProviderNameOrBussinessName { get; set; }
        public string ProviderDocumentTypeDescription { get; set; }
        public string ProviderDocumentNumber { get; set; }
        public string ProviderIVAConditionDescription { get; set; }
        public string ProviderAddress { get; set; }
        public string ProviderPostalCode { get; set; }
        public string ProviderCity { get; set; }
        public string ProviderState { get; set; }
        public string ProviderCountry { get; set; }
    }
}
