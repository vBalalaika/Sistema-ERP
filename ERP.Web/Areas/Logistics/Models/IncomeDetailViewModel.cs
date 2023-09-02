using ERP.Web.Areas.Logistics.Models.DeliveryNote;
using ERP.Web.Areas.ProductMod.Models;
using ERP.Web.Areas.Purchases.Models.MissingProduct;
using ERP.Web.Areas.Purchases.Models.PurchaseOrder;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace ERP.Web.Areas.Logistics.Models
{
    public class IncomeDetailViewModel
    {
        public int IncomeProductId { get; set; }
        public ProductViewModel IncomeProduct { get; set; }
        public SelectList IncomeProducts { get; set; }
        public decimal Quantity { get; set; }
        public int UnitId { get; set; }
        public UnitMeasureViewModel Unit { get; set; }
        public SelectList Units { get; set; }
        public string BatchNumber { get; set; }
        public int? OTNumber { get; set; } = null;
        public string ProductNumber { get; set; }
        public int? FatherProductId { get; set; }
        public ProductViewModel FatherProduct { get; set; }
        public SelectList FatherProducts { get; set; }
        public int? FatherStructureId { get; set; }
        public StructureViewModel FatherStructure { get; set; }
        public SelectList FatherStructures { get; set; }
        public int? OCNumber { get; set; } = null;
        public decimal Weight { get; set; } = 0;
        public int IncomeStateId { get; set; }
        public IncomeStateViewModel IncomeState { get; set; }
        public SelectList IncomeStates { get; set; }
        public int IncomeHeaderId { get; set; }
        public IncomeHeaderViewModel IncomeHeader { get; set; }
        public DateTime? IncomeDate { get; set; }
        public string Reception { get; set; } = "R01";
        public string NextStation { get; set; }
        public PurchaseOrderDetailViewModel PurchaseOrderDetail { get; set; }
        public int? DeliveryNoteHeaderId { get; set; }
        public DeliveryNoteHeaderViewModel DeliveryNoteHeader { get; set; }
        public int? MissingProductId { get; set; }
        public MissingProductViewModel MissingProduct { get; set; }

        public int Id { get; set; }
        public int ConcurrencyToken { get; set; }
        public string CreatedBy { get; set; }
    }
}
