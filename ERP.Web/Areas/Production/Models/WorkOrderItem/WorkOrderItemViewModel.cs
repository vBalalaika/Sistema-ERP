using ERP.Web.Areas.ProductMod.Models;
using ERP.Web.Areas.Sales.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERP.Web.Areas.Production.Models
{

    public class WorkOrderItemViewModel
    {
        public int Id { get; set; }
        public int ConcurrencyToken { get; set; }
        public string Level { get; set; }
        public int SpacingPixels
        {
            get
            {
                return (this.Level.Split('.').Length - 1) * 8;
            }
        }
        public int Quantity { get; set; }
        public int? ProductId { get; set; }
        public ProductViewModel Product { get; set; }
        public int? StructureId { get; set; }
        public StructureViewModel Structure { get; set; }

        public int WorkOrderId { get; set; }
        public WorkOrderViewModel WorkOrder { get; set; }

        public int? WorkOrderItemOfId { get; set; }
        public WorkOrderItemViewModel? WorkOrderItemOf { get; set; }

        public int OrderStateId { get; set; }
        public OrderStateViewModel OrderState { get; set; }

        public ICollection<WorkActivityViewModel> WorkActivities { get; set; } = new List<WorkActivityViewModel>();

        public bool isActive
        {
            get
            {
                if (this.WorkActivities != null)
                {
                    return (this.WorkActivities.Count > 0 && this.WorkActivities.Any(activity => activity.StateActivity == true));
                }
                else
                {
                    return false;
                }
            }
        }


        public string getState
        {
            get
            {
                if (this.WorkActivities != null)
                {
                    if (this.WorkActivities.All(activity => activity.getState == ActivityStates.Pendiente))
                    {
                        return ActivityStates.Pendiente;
                    }
                    if (this.WorkActivities.Any(activity => activity.getState == ActivityStates.EnProceso))
                    {
                        return ActivityStates.EnProceso;
                    }
                    return ActivityStates.Finalizado;
                }
                else
                {
                    return ActivityStates.EnProceso;
                }


            }
        }
        public DateTime? StartDate
        {
            get
            {
                if (this.WorkActivities != null)
                {
                    if (this.WorkActivities.Count > 0 && this.WorkActivities.Any(activity => activity.StartDate != null))
                    {
                        return this.WorkActivities.Where(activity => activity.StartDate.HasValue).Select(activity => activity.StartDate).Min(date => date);
                    }
                    return null;
                }
                else
                {
                    return null;
                }
            }
            set { }
        }

        public bool toShipments
        {
            get
            {
                if (this.WorkActivities != null)
                {
                    return (this.WorkActivities.Count > 0 && this.WorkActivities.Any(activity => activity.ToShipments.HasValue && activity.ToShipments.Value == true));
                }
                else
                {
                    return false;
                }
            }
        }

        public string StationOutsourced
        {
            get
            {
                var stationOutsourced = "";
                if (this.toShipments)
                {
                    if (this.WorkActivities.Count > 0)
                    {
                        foreach (var wa in this.WorkActivities)
                        {
                            if (wa.ToShipments.HasValue && wa.ToShipments.Value)
                            {
                                if (wa.OutsourcedProvider != null)
                                {
                                    stationOutsourced += wa.ProductStation.Station.Abbreviation.ToString() + " " + wa.OutsourcedProvider.getBussinessNameOrName.ToString();
                                }
                                else
                                {
                                    stationOutsourced += wa.ProductStation.Station.Abbreviation.ToString();
                                }

                                stationOutsourced += " - ";
                            }

                        }
                    }

                    if (stationOutsourced.Trim().StartsWith("-"))
                    {
                        stationOutsourced = stationOutsourced.Trim().Substring(2);
                    }

                    if (stationOutsourced.Trim().EndsWith("-"))
                    {
                        stationOutsourced = stationOutsourced.Trim().Substring(0, stationOutsourced.Length - 2);
                    }

                    return stationOutsourced;
                }
                else
                {
                    return stationOutsourced;
                }
            }
        }

        public bool AllStationsOutsourced
        {
            get
            {
                int count = 0;
                if (this.WorkActivities.Count > 0)
                {
                    foreach (var wa in this.WorkActivities)
                    {
                        if (wa.ToShipments.HasValue && wa.ToShipments.Value)
                        {
                            count += 1;
                        }
                    }

                    if (count == this.WorkActivities.Count())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

    }
}
