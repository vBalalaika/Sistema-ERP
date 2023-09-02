using ERP.Domain.Entities.Clients;
using ERP.Domain.Entities.Sales;
using ERP.Web.Areas.ProductMod.Models;
using ERP.Web.Areas.Sales.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace ERP.Web.Areas.Clients.Models
{
    public class SaleOperationViewModel
    {
        public int ClientId { get; set; }
        public ClientViewModel Client { get; set; }
        public int Id { get; set; }
        public string Operation { get; set; }
        public string State { get; set; }
        public string Comments { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndingDate { get; set; }
        public string StartDateString { get; set; }
        public string EndingDateString { get; set; }
        public int OperationNumber { get; set; }
        public ICollection<OrderDetailViewModel> OrderDetails { get; set; } = new List<OrderDetailViewModel>();
        public ICollection<CommunicationViewModel> Communications { get; set; } = new List<CommunicationViewModel>();
        public ICollection<ProductViewModel> Products{ get; set; } = new List<ProductViewModel>();

        public string View { get; set; }
        
        public int ConcurrencyToken { get; set; }
    }
}
