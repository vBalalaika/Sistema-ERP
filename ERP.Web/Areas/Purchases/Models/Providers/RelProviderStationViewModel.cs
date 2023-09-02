using ERP.Web.Areas.ProductMod.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ERP.Web.Areas.Purchases.Models.Providers
{
    public class RelProviderStationViewModel
    {
        public int Id { get; set; }
        public int ConcurrencyToken { get; set; }

        public int ProviderId { get; set; }
        public ProviderViewModel Provider { get; set; }

        public int StationId { get; set; }
        public StationViewModel Station { get; set; }
        public SelectList Stations { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal DollarPrice { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal PesosPrice { get; set; }

        public int? PriceUnitMeasureId { get; set; }
        public UnitMeasureViewModel PriceUnitMeasure { get; set; }

        public SelectList UnitsMeasure { get; set; }

    }
}
