using ERP.Web.Areas.Purchases.Models.Providers;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace ERP.Web.Areas.Purchases.Models.QuoteRequest
{
    public class QuoteRequestResponseHeaderViewModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public int Number { get; set; }

        public int QuoteRequestHeaderId { get; set; }
        public QuoteRequestHeaderViewModel QuoteRequestHeader { get; set; }

        public int ProviderId { get; set; }
        public ProviderViewModel Provider { get; set; }

        public decimal Bonus { get; set; } = 0;

        // 1 => 21%
        // 2 => 10,5%
        // 3 => 27%
        // 4 => 0%
        public int IVA { get; set; }
        public decimal Total { get; set; } = 0;

        public string Comments { get; set; }

        public ICollection<QuoteRequestResponseDetailViewModel> QuoteRequestResponseDetails { get; set; } = new List<QuoteRequestResponseDetailViewModel>();

        public int ConcurrencyToken { get; set; }

        public SelectList AlternativeProducts { get; set; }
    }
}
