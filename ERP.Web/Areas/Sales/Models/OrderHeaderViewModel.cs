using ERP.Web.Areas.Clients.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace ERP.Web.Areas.Sales.Models
{
    public class OrderHeaderViewModel
    {
        public int Id { get; set; }
        public int ConcurrencyToken { get; set; }

        public int? ClientId { get; set; }
        public ClientViewModel Client { get; set; }
        public SelectList Clients { get; set; }
        public DateTime OrderDate { get; set; }
        public int Number { get; set; }
        public decimal Taxes { get; set; }
        public decimal SecureAndFreightCosts { get; set; }
        public decimal Bonus { get; set; }
        public string TypeOfFreightAndSecure { get; set; }
        public string PaymentMethod { get; set; }
        public string Packaging { get; set; }
        public string Transport { get; set; }
        public string PlaceOfDelivery { get; set; }
        public decimal OrderTotalPrice { get; set; }

        public decimal TotalPrice { get; set; }
        public DateTime? ValidOfferDate { get; set; }
        // Observaciones para producción
        public string ProductionObservations { get; set; }
        // Observaciones para ventas
        public string SaleObservations { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public int OrderStateId { get; set; }
        public OrderStateViewModel OrderState { get; set; }
        public IList<OrderDetailViewModel> OrderDetails { get; set; } = new List<OrderDetailViewModel>();

        public int Billed { get; set; }

        public string User { get; set; }

        public SelectList Products { get; set; }
        // To distinct sale order than production order
        public bool isSale { get; set; }
        public bool onlySpares { get; set; }
        public bool NewOperation { get; set; }
        public bool ExistingOperation { get; set; }
        public SaleOperationViewModel SaleOperation { get; set; }
        public int? SaleOperationId { get; set; }
        public IList<DeliveryDateHistoryViewModel> deliveryDateHistoryVMs { get; set; } = new List<DeliveryDateHistoryViewModel>();
    }
}
