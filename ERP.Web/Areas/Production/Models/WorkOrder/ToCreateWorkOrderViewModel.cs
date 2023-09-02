using ERP.Web.Areas.ProductMod.Models;
using ERP.Web.Areas.Sales.Models;
using System;
using System.Collections.Generic;

namespace ERP.Web.Areas.Production.Models
{
    public class ToCreateWorkOrderViewModel
    {
        public string NroWorkOrder { get; set; }
        public DateTime WorkOrderDate { get; set; } = new DateTime();


        public IList<OrderDetailViewModel> OrderDetailList { get; set; } = new List<OrderDetailViewModel>();
        public IList<StationViewModel> StationList { get; set; } = new List<StationViewModel>();
        public IList<StationViewModel> StationListSelected { get; set; } = new List<StationViewModel>();
    }
}
