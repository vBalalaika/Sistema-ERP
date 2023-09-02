using ERP.Web.Areas.Purchases.Models.Providers;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace ERP.Web.Areas.Logistics.Models.DeliveryNote
{
    public class DeliveryNoteHeaderViewModel
    {
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public ProviderViewModel Provider { get; set; }
        public SelectList Providers { get; set; }
        public int TransportProviderId { get; set; }
        public ProviderViewModel TransportProvider { get; set; }
        public SelectList TransportProviders { get; set; }
        public DateTime Date { get; set; } = DateTime.Now.Date.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute);
        public string Number { get; set; }
        public ICollection<DeliveryNoteDetailViewModel> DeliveryNoteDetails { get; set; }
        public string Comments { get; set; }
        // Sirve para validar que si se exporto a PDF ya no se puede combinar con otro remito.
        public bool wasExportedToPDF { get; set; }

        public int stationId { get; set; }

        public SelectList deliveryNoteNumbers { get; set; }
        public int ConcurrencyToken { get; set; }
    }
}
