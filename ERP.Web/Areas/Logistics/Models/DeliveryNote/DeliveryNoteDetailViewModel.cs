using ERP.Web.Areas.Production.Models;
using ERP.Web.Areas.ProductMod.Models;
using ERP.Web.Areas.Purchases.Models.PurchaseOrder;
using System.Collections.Generic;

namespace ERP.Web.Areas.Logistics.Models.DeliveryNote
{
    public class DeliveryNoteDetailViewModel
    {
        public int Id { get; set; }
        public int ProductItemId { get; set; }
        public ProductViewModel ProductItem { get; set; }
        public int ProductId { get; set; }
        public ProductViewModel Product { get; set; }
        public int? ConfigurationId { get; set; }
        public StructureViewModel Configuration { get; set; }
        public int Package { get; set; }
        public decimal PackageWeight { get; set; }
        public int Quantity { get; set; }

        public ICollection<WorkActivityViewModel> WorkActivityViewModels { get; set; }
        public int? WorkActivityId { get; set; }
        public WorkActivityViewModel WorkActivity { get; set; }

        public int DeliveryNoteHeaderId { get; set; }
        public DeliveryNoteHeaderViewModel DeliveryNoteHeader { get; set; }
        public int? ServicePODetailId { get; set; }
        public ServicePODetailViewModel ServicePODetail { get; set; }
        public int ConcurrencyToken { get; set; }
    }
}
