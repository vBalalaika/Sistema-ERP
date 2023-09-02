using ERP.Web.Areas.ProductMod.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ERP.Web.Areas.Purchases.Models.Providers
{
    public class RelProviderProductViewModel
    {
        public int Id { get; set; }
        public int ConcurrencyToken { get; set; }

        public int ProviderId { get; set; }
        public ProviderViewModel Provider { get; set; }

        public int ProductId { get; set; }
        public SelectList Products { get; set; }
        public ProductViewModel Product { get; set; }

        public string Brand { get; set; }
        public string ProviderCode { get; set; }

        [Required(ErrorMessage = "Unit measure value must be completed.")]
        public int UnitMeasureId { get; set; }
        public UnitMeasureViewModel UnitMeasure { get; set; }
        public SelectList UnitsMeasure { get; set; }

        // WidthUnitMeasure
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal? Width { get; set; }
        public int? WidthUnitMeasureId { get; set; }
        public UnitMeasureViewModel WidthUnitMeasure { get; set; }

        // LenghtUnitMeasure
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal? Lenght { get; set; }
        public int? LengthUnitMeasureId { get; set; }
        public UnitMeasureViewModel LengthUnitMeasure { get; set; }

        // WeightUnitMeasure
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal? Weight { get; set; }
        public int? WeightUnitMeasureId { get; set; }
        public UnitMeasureViewModel WeightUnitMeasure { get; set; }

        // WeightUnitMeasure
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal? Height { get; set; }
        public int? HeightUnitMeasureId { get; set; }
        public UnitMeasureViewModel HeightUnitMeasure { get; set; }

        // PresentationUnitMeasure
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal? PresentationQuantity { get; set; }
        public int? PresentationUnitMeasureId { get; set; }
        public UnitMeasureViewModel PresentationUnitMeasure { get; set; }

        public string Observations { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal DollarPrice { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal PesosPrice { get; set; }

        public int? PriceUnitMeasureId { get; set; }
        public UnitMeasureViewModel PriceUnitMeasure { get; set; }

        public bool viewDetails { get; set; }
        public bool editFromAllProductsProviders { get; set; }

    }
}
