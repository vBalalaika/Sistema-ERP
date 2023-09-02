using ERP.Web.Areas.ProductMod.Models;
using ERP.Web.Areas.Purchases.Models.Providers;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace ERP.Web.Areas.Logistics.Models
{
    public class IncomeHeaderViewModel
    {
        public DateTime IncomeDate { get; set; }
        public int ProviderId { get; set; }
        public ProviderViewModel Provider { get; set; }
        public SelectList Providers { get; set; }
        public int TransportProviderId { get; set; }
        public ProviderViewModel TransportProvider { get; set; }
        public SelectList TransportProviders { get; set; }
        public string DeliveryNoteNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public int? ExternalProcessStationId { get; set; }
        public StationViewModel ExternalProcessStation { get; set; }
        public SelectList ExternalProcessStations { get; set; }
        public string Comments { get; set; }
        public bool OwnTransport { get; set; } = false;

        public ICollection<IncomeDetailViewModel> IncomeDetails { get; set; } = new List<IncomeDetailViewModel>();

        public int Id { get; set; }
        public int ConcurrencyToken { get; set; }
        public DateTime LastModifiedOn { get; set; }
    }
}
