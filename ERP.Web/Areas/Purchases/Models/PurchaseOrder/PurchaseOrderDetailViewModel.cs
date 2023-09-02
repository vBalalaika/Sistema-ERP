using ERP.Domain.Entities.Purchases.MissingProducts;
using ERP.Web.Areas.Logistics.Models;
using ERP.Web.Areas.ProductMod.Models;
using ERP.Web.Areas.Purchases.Models.MissingProduct;
using System.ComponentModel.DataAnnotations;

namespace ERP.Web.Areas.Purchases.Models.PurchaseOrder
{
    public class PurchaseOrderDetailViewModel
    {
        public int Id { get; set; }
        public int ConcurrencyToken { get; set; }
        // Se setea por defecto el estado "Comprado", procurar que el Id = 4 sea el estado en cuestión en la base de datos
        public int PurchaseStateId { get; set; } = 4;
        public PurchaseState PurchaseState { get; set; }
        public int PurchaseOrderHeaderId { get; set; }
        public PurchaseOrderHeaderViewModel PurchaseOrderHeader { get; set; }
        public int? MissingProductId { get; set; }
        public MissingProductViewModel MissingProduct { get; set; }
        public int? ProductId { get; set; }
        public ProductViewModel Product { get; set; }
        public decimal OriginalProviderQuantity { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal ProviderQuantity { get; set; }
        public int ProviderUnitMeasureId { get; set; }
        public UnitMeasureViewModel ProviderUnitMeasure { get; set; }
        public decimal OriginalPriceQuantity { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal PriceQuantity { get; set; }
        public int PriceUnitMeasureId { get; set; }
        public UnitMeasureViewModel PriceUnitMeasure { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal UnitPrice { get; set; }
        public int MoneyType { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal Bonus { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal Total { get; set; }
        public decimal PurchasedQuantity { get; set; }

        public int? IncomeDetailId { get; set; }
        public IncomeDetailViewModel IncomeDetail { get; set; }
    }
}
