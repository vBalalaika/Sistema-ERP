using ERP.Infrastructure.Identity.Models;
using ERP.Web.Areas.Logistics.Models.DeliveryNote;
using ERP.Web.Areas.ProductMod.Models;
using ERP.Web.Areas.Purchases.Models.Providers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERP.Web.Areas.Production.Models
{
    public static class StationDescriptions
    {
        public static readonly string Torno = "P03 - Torno";
        public static readonly string Cincado = "E01 - Despacho a cincado";
        public static readonly string Pintura = "E02 - Despacho a pintura";

    }
    public static class ActivityStates
    {
        public static readonly string Pendiente = "Pendiente";
        public static readonly string EnProceso = "En proceso";
        public static readonly string Finalizado = "Finalizado";

    }
    public static class HtmlColors
    {
        public static readonly string Green = "#77DD77";
        public static readonly string Yellow = "#FFD680";
    }

    public class WorkActivityViewModel
    {
        public ApplicationUser currentUser { get; set; }
        private string StopActionString = "stop";
        //private readonly IStringLocalizer<SharedResource> _localizer;
        public int Id { get; set; }
        public int ConcurrencyToken { get; set; }
        public string LastModifiedBy { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int WorkOrderItemId { get; set; }
        public WorkOrderItemViewModel WorkOrderItem { get; set; }
        // Assigned users's ids separados por ',' -.-
        public string AssignedUsersIds { get; set; }
        public int ProductStationId { get; set; }
        public RelProductStationViewModel ProductStation { get; set; }
        public ICollection<WorkActivityViewModel> ChildsActivities { get; set; } = new List<WorkActivityViewModel>();
        public ICollection<WorkActionViewModel> WorkActions { get; set; } = new List<WorkActionViewModel>();
        public DateTime? StartDate
        {
            get
            {
                if (this.WorkActions.Count > 0 && this.WorkActions.Any(action => action.StartDate != null))
                {
                    return this.WorkActions.Where(action => action.StartDate.HasValue).Select(action => action.StartDate).Min(date => date);
                }
                return null;
            }
            set { }
        }
        public bool haveTornoInRoute
        {
            get
            {
                if (this.WorkOrderItem != null)
                {
                    if (this.WorkOrderItem.WorkActivities != null && this.WorkOrderItem.WorkActivities.Any(activity => activity.NextStation == StationDescriptions.Torno))
                    {
                        return true;
                    }
                    return false;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool haveCincadoInRoute
        {
            get
            {
                if (this.WorkOrderItem != null)
                {
                    if (this.WorkOrderItem.WorkActivities != null && this.WorkOrderItem.WorkActivities.Any(activity => activity.NextStation == StationDescriptions.Cincado))
                    {
                        return true;
                    }
                    return false;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool havePinturaInRoute
        {
            get
            {
                if (this.WorkOrderItem != null)
                {
                    if (this.WorkOrderItem.WorkActivities != null && this.WorkOrderItem.WorkActivities.Any(activity => activity.NextStation == StationDescriptions.Pintura))
                    {
                        return true;
                    }
                    return false;
                }
                else
                {
                    return false;
                }
            }
        }
        public DateTime? EndingDate
        {
            get
            {
                if (this.WorkActions.Any(action => action.ActionName == "stop"))
                {
                    return this.WorkActions.Where(action => action.ActionName == "stop").Select(action => action.StartDate).FirstOrDefault();
                }
                return null;
            }
            set { }
        }
        public DateTime? ComebackDate { get; set; }

        public bool hasActionPlay
        {
            get
            {
                if (this.WorkActions.Any(action => action.ActionName == "play"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool hasActionStop
        {
            get
            {
                if (this.WorkActions.Any(action => action.ActionName == "stop"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public string Observation { get; set; }
        public string NextStation { get; set; }
        public bool StateActivity { get; set; }
        public DateTime? DateToProduction { get; set; }

        //No DB

        public string FilteredStation { get; set; }
        public int IdFilteredStation { get; set; }

        public bool isFinished
        {
            get
            {
                if (this.WorkActions.Count > 0)
                {
                    return this.WorkActions.Any(x => x.ActionName == this.StopActionString);
                }
                return false;
            }
        }
        public bool isStarted
        {
            get
            {
                return this.WorkActions.Count > 0;
            }
        }
        public string getState
        {
            get
            {
                if (this.isStarted && !this.isFinished)
                {
                    return ActivityStates.EnProceso;
                }
                if (this.isFinished)
                {
                    return ActivityStates.Finalizado;
                }
                return ActivityStates.Pendiente;
            }
        }

        public string Color
        {
            get
            {
                if (this.getState == ActivityStates.Pendiente)
                {
                    return "";
                }
                if (this.getState == ActivityStates.EnProceso)
                {
                    return HtmlColors.Yellow;
                }
                return HtmlColors.Green;
            }
        }
        public string TotalTimeString
        {
            get
            {
                return new TimeSpan(this.TotalTime.Value.Ticks).ToString(@"dd\ hh\:mm\:ss");
            }
        }

        public TimeSpan? TotalTime
        {
            get
            {
                var time0 = new TimeSpan(0, 0, 0, 0);

                if (this.LastAction != null)
                {
                    if (this.WorkActions.Any(x => x.ActionName == "stop") == false)
                    {
                        return time0;
                    }
                    else
                    {
                        var timeLapse = new TimeSpan(0, 0, 0, 0);
                        foreach (var action in this.WorkActions)
                        {
                            timeLapse += action.ActionTime;
                        }
                        return timeLapse;
                    }
                }

                return time0;
            }
        }
        public WorkActionViewModel FirstAction
        {
            get
            {
                if (this.WorkActions.Count != 0)
                {
                    return this.WorkActions.First();
                }
                else
                    return null;
            }

        }
        public WorkActionViewModel LastAction
        {
            get
            {
                if (this.WorkActions.Count != 0)
                {
                    return this.WorkActions.Last();
                }
                else
                    return null;
            }

        }
        public WorkActionViewModel StopAction
        {
            get
            {
                if (WorkActions.Count != 0 && WorkActions.Any(action => action.ActionName == "stop") == true)
                {
                    return WorkActions.Where(action => action.ActionName == "stop").FirstOrDefault();
                }
                else
                    return null;
            }
        }
        public string DisablePlay
        {
            get
            {
                if (this.LastAction == null || this.LastAction.ActionName == "pause")
                {
                    return "";
                }
                else
                {
                    return "disabled";
                }
            }
        }
        public string DisablePause
        {
            get
            {
                if (this.LastAction != null && this.LastAction.ActionName == "play")
                {
                    return "";
                }
                else
                {
                    return "disabled";
                }
            }
        }
        public string DisableStop
        {
            get
            {
                if (this.WorkActions.Count != 0 && !this.WorkActions.Any(x => x.ActionName == "stop"))
                {
                    return "";
                }
                else
                {
                    return "disabled";
                }
            }
        }

        // Fran: 6/12/2022 

        public string DisablePlayBtn { get; set; } = null;
        public string DisablePauseBtn { get; set; } = null;
        public string DisableStopBtn { get; set; } = null;

        // Fran: Envíos
        public bool? ToShipments { get; set; }

        // Fran: Relacion con remito
        //public int? DeliveryNoteDetailId { get; set; }
        //public DeliveryNoteDetailViewModel DeliveryNoteDetailViewModel { get; set; }
        public DeliveryNoteHeaderViewModel DeliveryNoteHeaderViewModel { get; set; }

        public int? OutsourcedProviderId { get; set; } = null;
        public ProviderViewModel OutsourcedProvider { get; set; }

        public string PreviousStation { get; set; }
        public string groupedValue { get; set; }
        public DateTime? LastModifiedOn { get; set; }
    }
}
