using ERP.Domain.Entities.Purchases.MissingProducts;
using ERP.Web.Areas.ProductMod.Models;
using ERP.Web.Areas.Purchases.Models.MissingProduct;

namespace ERP.Web.Areas.Purchases.Models.QuoteRequest
{
    public class QuoteRequestDetailViewModel
    {
        public int Id { get; set; }
        public int ConcurrencyToken { get; set; }

        public int QuoteRequestHeaderId { get; set; }
        public QuoteRequestHeaderViewModel QuoteRequestHeader { get; set; }

        public int ProductId { get; set; }
        public ProductViewModel Product { get; set; }

        public decimal QuantityToOrder { get; set; }
        public int? QuantityToOrderUnitMeasureId { get; set; }
        public UnitMeasureViewModel QuantityToOrderUnitMeasure { get; set; }

        public decimal Quantity { get; set; }
        public int? ProviderUnitMeasureId { get; set; }
        public UnitMeasureViewModel ProviderUnitMeasure { get; set; }

        public int? MissingProductId { get; set; }
        public MissingProductViewModel MissingProduct { get; set; }

        // Se setea por defecto el estado "A cotizar", procurar que el Id = 2 sea el estado en cuestión en la base de datos
        public int PurchaseStateId { get; set; } = 2;
        public PurchaseState PurchaseState { get; set; }
    }
}
