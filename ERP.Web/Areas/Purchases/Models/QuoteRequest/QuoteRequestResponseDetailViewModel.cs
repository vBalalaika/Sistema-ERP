using ERP.Web.Areas.ProductMod.Models;
using ERP.Web.Areas.Purchases.Models.MissingProduct;

namespace ERP.Web.Areas.Purchases.Models.QuoteRequest
{
    public class QuoteRequestResponseDetailViewModel
    {
        public int Id { get; set; }

        public int? MissingProductId { get; set; }
        public MissingProductViewModel MissingProduct { get; set; }
        public bool Available { get; set; }
        public int? AlternativeProductId { get; set; }
        public ProductViewModel AlternativeProduct { get; set; }
        public decimal OriginalProviderQuantity { get; set; }
        public decimal ProviderQuantity { get; set; }
        public int ProviderUnitMeasureId { get; set; }
        public UnitMeasureViewModel ProviderUnitMeasure { get; set; }
        public decimal OriginalPriceQuantity { get; set; }
        public decimal PriceQuantity { get; set; }
        public int PriceUnitMeasureId { get; set; }
        public UnitMeasureViewModel PriceUnitMeasure { get; set; }
        public decimal UnitPrice { get; set; }
        public int MoneyType { get; set; }
        public decimal Bonus { get; set; }
        public decimal Total { get; set; }
        public int QuoteRequestResponseHeaderId { get; set; }
        public QuoteRequestResponseHeaderViewModel QuoteRequestResponseHeader { get; set; }

        public int ConcurrencyToken { get; set; }

    }
}
