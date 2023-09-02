using ERP.Web.Areas.Production.Models.WorkActivity;
using ERP.Web.Areas.ProductMod.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERP.Web.Areas.Production.Models
{
    public class WorkActivitiesViewModel
    {
        public string urlIds
        {
            get
            {
                string url = "";
                WorkOrderViewModel last = this.WorkOrders.Last();
                foreach (var wo in this.WorkOrders)
                {
                    if (wo != null)
                    {
                        if (wo.Equals(last))
                            url += wo.Id;
                        else
                            url += wo.Id + ",";
                    }
                }
                return url;
            }
        }
        public string workOrderIdsString
        {
            get
            {
                string ids = "";
                WorkOrderViewModel last = this.WorkOrders.Last();
                IList<int> intIds = new List<int>();
                foreach (var wo in this.WorkOrders)
                {
                    if (last != null)
                    {
                        if (!intIds.Contains(wo.WorkOrderHeader.WorkOrderHeaderNumber))
                        {
                            intIds.Add(wo.WorkOrderHeader.WorkOrderHeaderNumber);
                        }

                    }
                }
                ids = string.Join("-", intIds);
                return ids;
            }
        }
        public string workOrdersProductsNumbers
        {
            get
            {
                List<string> productNumbers = new List<string>();
                foreach (var wo in this.WorkOrders)
                {
                    productNumbers.Add(wo.OrderDetail.ProductNumber);

                }
                return string.Join(',', productNumbers);

            }
        }
        public string workOrdersProductsNames
        {
            get
            {
                List<string> productNumbers = new List<string>();
                foreach (var wo in this.WorkOrders)
                {
                    productNumbers.Add(wo.OrderDetail.Product.CodeAndDescription);

                }
                return string.Join(',', productNumbers);

            }
        }

        public string workOrdersStructuresNames
        {
            get
            {
                List<string> structureNames = new List<string>();
                foreach (var wo in this.WorkOrders)
                {
                    if (wo.OrderDetail.Structure != null)
                    {
                        structureNames.Add(wo.OrderDetail.Structure.Description);
                    }
                }
                return string.Join(',', structureNames);

            }
        }

        public int TotalAdvance
        {
            get
            {
                if (this.ActivitiesGroups != null)
                {
                    var total_finished = 0.0;

                    var sumCount = this.ActivitiesGroups.Count();
                    foreach (var group in this.ActivitiesGroups)
                    {
                        total_finished += group.Activities.Any(item => item.isFinished) ? 1 : 0;
                    }

                    if (total_finished > 0)
                    {
                        return Convert.ToInt32(total_finished * 100 / sumCount);
                    }
                    else
                        return 0;
                }
                else
                    return 0;
            }
        }

        public int ShipmentsTotalAdvance
        {
            get
            {
                if (this.ActivitiesGroups != null)
                {
                    var total_finished = 0.0;
                    var sumCount = 0;

                    foreach (var group in this.ActivitiesGroups)
                    {
                        foreach (var subGroup in group.SubGroups)
                        {
                            foreach (var activity in subGroup.Activities.Where(wa => !wa.WorkOrderItem.Product.ProductFeature.StoredStock && wa.ToShipments == null))
                            {
                                if (subGroup.Activities.First().Id == activity.Id)
                                {
                                    sumCount++;
                                    if (activity.isFinished)
                                    {
                                        total_finished += 1;
                                    }
                                }
                            }
                        }
                    }

                    //foreach (var group in this.ActivitiesGroups)
                    //{
                    //    foreach (var item in group.Activities.Where(wa => !wa.WorkOrderItem.Product.ProductFeature.StoredStock && wa.ToShipments == null))
                    //    {
                    //        sumCount++;
                    //        if (item.isFinished)
                    //        {
                    //            total_finished += 1;
                    //        }
                    //    }
                    //}

                    if (total_finished > 0)
                    {
                        return Convert.ToInt32(total_finished * 100 / sumCount);
                    }
                    else
                        return 0;
                }
                else
                    return 0;
            }
        }

        public string StartDate
        {
            get
            {
                return this.ActivitiesGroups.Any(z => z.StartDate != null) ? this.ActivitiesGroups.Min(z => z.StartDate) : null;
            }
        }
        public string EndDate
        {
            get
            {
                //Maxi y sope
                //if (this.ActivitiesGroups.All(group => group.EndDate != null))
                //{
                //    return this.ActivitiesGroups.Max(z => z.EndDate);
                //}
                //return null;
                // Fran
                if (this.TotalAdvance == 100)
                {
                    return this.ActivitiesGroups.Max(z => z.EndDate);
                }
                else
                {
                    return null;
                }
            }
        }
        public string ShippingEndDate
        {
            get
            {
                return this.ActivitiesGroups.All(z => z.EndDate != null) ? this.ActivitiesGroups.Max(z => z.EndDate) : null;
            }
        }
        public string TotalTime
        {
            get
            {
                if (this.StartDate != null && this.EndDate != null)
                {
                    DateTime startDate = DateTime.ParseExact(this.StartDate, "dd/MM/yyyy HH:mm:ss",
                                   System.Globalization.CultureInfo.InvariantCulture);
                    DateTime endDate = DateTime.ParseExact(this.EndDate, "dd/MM/yyyy HH:mm:ss",
                              System.Globalization.CultureInfo.InvariantCulture);
                    TimeSpan totalTime = endDate - startDate;
                    return totalTime.ToString(@"dd\ hh\:mm\:ss");
                }
                else if (this.StartDate != null && this.ShippingEndDate != null)
                {
                    DateTime startDate = DateTime.ParseExact(this.StartDate, "dd/MM/yyyy HH:mm:ss",
                        System.Globalization.CultureInfo.InvariantCulture);
                    DateTime endDate = DateTime.ParseExact(this.ShippingEndDate, "dd/MM/yyyy HH:mm:ss",
                              System.Globalization.CultureInfo.InvariantCulture);
                    TimeSpan totalTime = endDate - startDate;
                    return totalTime.ToString(@"dd\ hh\:mm\:ss");
                }
                return null;
            }
        }
        public StationViewModel Station { get; set; }
        public List<WorkActivityGroupViewModel> ActivitiesGroups = new List<WorkActivityGroupViewModel>();
        public List<WorkActivityViewModel> WorkActivities { get; set; } = new List<WorkActivityViewModel>();
        public List<WorkOrderViewModel> WorkOrders { get; set; } = new List<WorkOrderViewModel>();
    }

    public class TreeTable
    {
        public int tt_key { get; set; }
        public int tt_parent { get; set; }
        public string ProductNumber { get; set; }
        public string ProductDescription { get; set; }
        public int Quantity { get; set; }
        public string RawMaterialDescription { get; set; }
        public string Width { get; set; }
        public string Length { get; set; }
        //public IList<ArchiveViewModel> Archives { get; set; } = new List<ArchiveViewModel>();
        public string Archives { get; set; }
        public string NextStation { get; set; }
        public string RoadMap { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string Time { get; set; }
        public string ActionButtons { get; set; }
        public string relativeActivitiesIds { get; set; }
    }
}
