using ERP.Domain.Entities.Purchases.MissingProducts;
using ERP.Web.Areas.ProductMod.Models;
using ERP.Web.Areas.Purchases.Models.Providers;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;

namespace ERP.Web.Areas.Purchases.Models.MissingProduct
{
    public class MissingProductViewModel
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;

        public int? ProviderId { get; set; }
        public ProviderViewModel Provider { get; set; }
        public SelectList Providers { get; set; }

        public int ProductId { get; set; }
        public ProductViewModel Product { get; set; }

        public SelectList Products { get; set; }
        public SelectList UnitMeasures { get; set; }
        public SelectList StockUnitMeasures { get; set; }

        // Se setea por defecto el estado "Pendiente", procurar que el Id = 1 sea el estado en cuestión en la base de datos
        public int? StateMissingProductId { get; set; } = 1;
        public PurchaseState StateMissingProduct { get; set; }
        public SelectList StateMissingProducts { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal QuantityToOrder { get; set; }
        public int? QuantityToOrderUnitMeasureId { get; set; }
        public UnitMeasureViewModel QuantityToOrderUnitMeasure { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal Quantity { get; set; }
        public decimal StorageQuantity
        {
            get
            {
                if (Decimal.Compare(this.Quantity, 0) == 0)
                {
                    return 1;
                }
                else
                {
                    return this.Quantity;
                }
            }
        }

        public DateTime CreatedOn { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public int? StorageUnitMeasureId { get; set; }
        public UnitMeasureViewModel StorageUnitMeasure { get; set; }

        public string Comments { get; set; }
        public string UserName { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal PurchasedQuantity { get; set; }
        public int ConcurrencyToken { get; set; }

        public string idsForUpdateMassiveComments { get; set; } = null;

    }
}
