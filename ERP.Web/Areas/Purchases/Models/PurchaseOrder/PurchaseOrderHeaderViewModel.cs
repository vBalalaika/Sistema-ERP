using ERP.Web.Areas.Purchases.Models.Providers;
using ERP.Web.Areas.Purchases.Models.QuoteRequest;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace ERP.Web.Areas.Purchases.Models.PurchaseOrder
{
    public class PurchaseOrderHeaderViewModel
    {
        public int Id { get; set; }
        public int ConcurrencyToken { get; set; }
        public DateTime Date { get; set; }
        public string Comments { get; set; }
        public int ProviderId { get; set; }
        public ProviderViewModel Provider { get; set; }
        public SelectList Providers { get; set; }
        public decimal Bonus { get; set; }
        // 1 => 21%
        // 2 => 10,5%
        // 3 => 27%
        // 4 => 0%
        public int IVA { get; set; }
        public decimal Total { get; set; }
        public int Number { get; set; }
        public IList<PurchaseOrderDetailViewModel> PurchaseOrderDetails { get; set; } = new List<PurchaseOrderDetailViewModel>();
        public int? QuoteRequestResponseHeaderId { get; set; }
        public QuoteRequestResponseHeaderViewModel QuoteRequestResponseHeader { get; set; }
        public bool isGenerateMissingProducts { get; set; } = false;
        public SelectList AlternativeProducts { get; set; }
        public List<int> missingProductsIds { get; set; }
    }
}
