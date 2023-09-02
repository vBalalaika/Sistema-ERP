using System;

namespace ERP.Web.Areas.Sales.Models
{
    public class DeliveryDateHistoryViewModel
    {
        public int Id { get; set; }
        public int OrderDetailId { get; set; }
        public OrderDetailViewModel OrderDetail { get; set; }
        public DateTime OldDeliveryDate { get; set; }
        public DateTime NewDeliveryDate { get; set; }
        public DateTime ChangeDate { get; set; }
        public string CreatedBy { get; set; }
        public int ConcurrencyToken { get; set; }
    }
}
