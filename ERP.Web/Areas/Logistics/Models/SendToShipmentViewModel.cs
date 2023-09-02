using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Web.Areas.Logistics.Models
{
    public class SendToShipmentViewModel
    {
        public int WorkOrderId { get; set; }
        public int? StationId { get; set; }
        public SelectList Stations { get; set; }
        public int[] WorkOrderItemIds { get; set; }
        public int? ProviderId { get; set; }
        public SelectList Providers { get; set; }
        public bool isSparesOrStock { get; set; }
        public string workOrderIds { get; set; }
    }
}
