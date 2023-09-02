using ERP.Web.Areas.Logistics.Models.DeliveryNote;
using ERP.Web.Areas.ProductMod.Models;
using System.ComponentModel.DataAnnotations;

namespace ERP.Web.Areas.Purchases.Models.PurchaseOrder
{
    public class ServicePODetailViewModel
    {
        public int Id { get; set; }

        public int ServicePOHeaderId { get; set; }
        public ServicePOHeaderViewModel ServicePOHeader { get; set; }

        public int ProductId { get; set; }
        public ProductViewModel Product { get; set; }
        public int Quantity { get; set; }
        public int? UnitMeasureId { get; set; }
        public UnitMeasureViewModel UnitMeasure { get; set; }
        public decimal UnitPrice { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal Bonus { get; set; }

        public int? DeliveryNoteDetailId { get; set; }
        public DeliveryNoteDetailViewModel DeliveryNoteDetail { get; set; }

        public string CreatedBy { get; set; }
        public int ConcurrencyToken { get; set; }
    }
}
