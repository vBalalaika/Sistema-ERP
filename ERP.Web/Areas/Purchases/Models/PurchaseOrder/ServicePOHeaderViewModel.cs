using ERP.Domain.Entities.Purchases.MissingProducts;
using ERP.Web.Areas.ProductMod.Models;
using ERP.Web.Areas.Purchases.Models.Providers;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace ERP.Web.Areas.Purchases.Models.PurchaseOrder
{
    public class ServicePOHeaderViewModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public int Number { get; set; }
        public int ProviderId { get; set; }
        public ProviderViewModel Provider { get; set; }
        public SelectList Providers { get; set; }
        public SelectList Products { get; set; }

        public string Comments { get; set; }
        public decimal Bonus { get; set; } = 0;

        // 1 => 21%
        // 2 => 10,5%
        // 3 => 27%
        public int IVA { get; set; } = 1;
        public decimal Total { get; set; } = 0;

        public int? StationId { get; set; }
        public StationViewModel Station { get; set; }

        // ¿Con que estado inicial por defecto se setea? ¿Tiene estado?
        public int PurchaseStateId { get; set; } = 1;
        public PurchaseState PurchaseState { get; set; }

        public ICollection<ServicePODetailViewModel> ServicePODetails { get; set; } = new List<ServicePODetailViewModel>();

        public string CreatedBy { get; set; }
        public int ConcurrencyToken { get; set; }
    }
}
