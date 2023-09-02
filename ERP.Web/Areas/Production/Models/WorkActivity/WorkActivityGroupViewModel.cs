using ERP.Web.Areas.ProductMod.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERP.Web.Areas.Production.Models.WorkActivity
{
    public class WorkActivityGroupViewModel
    {
        public int Id { get; set; }
        public int WorkOrderId { get; set; }
        public int StationId { get; set; }
        public int Quantity { get; set; }
        public int FinishedQuantity { get; set; }
        public int WorkOrderHeaderNumber { get; set; }
        public string ProductNumber { get; set; }

        // Fran: Se agrega atributo para mostrar "Repuestos" o "Stock" o "Repuestos y stock" según la combinación.
        public HashSet<string> ProductNumbers { get; set; }
        public string ProductDescription { get; set; }
        public string StructureDescription { get; set; }
        public string StationDescription { get; set; }
        public string UsersIds { get; set; }
        public string[] UsersList { get; set; }
        public SelectList UsersSelectList { get; set; }
        public string GroupUsers { get; set; }

        //public int ProgressPercentage
        //{
        //    get
        //    {
        //        if (this.Activities != null)
        //        {
        //            var finishedQuantity = this.FinishedQuantity;
        //            var sumCount = this.Activities.Count();

        //            if (finishedQuantity > 0)
        //            {
        //                var total = Convert.ToInt32(finishedQuantity * 100 / sumCount);

        //                return total;
        //            }
        //            else
        //                return 0;
        //        }
        //        else
        //            return 0;
        //    }
        //    set { }
        //}

        public int ProgressPercentage
        {
            get
            {
                if (this.Activities != null)
                {
                    var total_finished = 0;
                    var sumCount = this.Activities.Count();
                    foreach (var act in this.Activities)
                    {
                        total_finished += act.WorkActions.Any(item => item.ActionName == "stop") ? 1 : 0;

                    }
                    if (total_finished > 0)
                    {
                        var total = Convert.ToInt32(total_finished * 100 / sumCount);

                        return total;
                    }
                    else
                        return 0;
                }
                else
                    return 0;
            }
            set { }
        }

        //public int TotalPercentage
        //{
        //    get
        //    {
        //        if (this.ActivitiesStartDates.Where(act => act.HasValue).Count() != 0 && this.ActivitiesEndDates.Where(act => act.HasValue).Count() != 0 && this.ActivitiesStartDates.Where(act => act.HasValue).Count() == this.ActivitiesEndDates.Where(act => act.HasValue).Count())
        //        {
        //            return 100;
        //        }
        //        else if (this.ActivitiesStartDates.Where(act => act.HasValue).Count() == 0 && this.ActivitiesEndDates.Where(act => act.HasValue).Count() == 0 && this.ActivitiesStartDates.Where(act => act.HasValue).Count() == this.ActivitiesEndDates.Where(act => act.HasValue).Count())
        //        {
        //            return 0;
        //        }
        //        else
        //        {
        //            return this.ActivitiesEndDates.Where(act => act.HasValue).Count() * 100 / this.Quantity;
        //        }
        //    }
        //}

        //public int ProgressPercentage
        //{
        //    get
        //    {
        //        if (this.Quantity > 0 && this.FinishedQuantity > 0)
        //        {
        //            return Convert.ToInt32(Convert.ToDouble(this.FinishedQuantity) * 100 / this.Quantity);
        //        }
        //        return 0;
        //    }
        //    set { }
        //}

        public List<DateTime?> ActivitiesStartDates { get; set; }
        public List<DateTime?> ActivitiesEndDates { get; set; }
        public string StartDateGroup
        {
            get
            {
                if (this.ActivitiesStartDates != null)
                {
                    return this.ActivitiesStartDates.Any(z => z != null) ? this.ActivitiesStartDates.Min(z => z).Value.ToString("dd/MM/yyyy HH:mm:ss") : null;
                }
                return null;
            }
            set { }
        }
        public string EndDateGroup
        {
            get
            {
                if (this.ActivitiesEndDates != null && this.ActivitiesEndDates.Count() > 0)
                { return this.ActivitiesEndDates.Any(z => z == null) ? null : this.ActivitiesEndDates.Max(z => z).Value.ToString("dd/MM/yyyy HH:mm:ss"); }
                return null;
            }
            set { }
        }
        public string StartDate
        {
            get
            {
                if (this.AllActivities != null)
                {
                    return this.AllActivities.Any(z => z.StartDate != null) ? this.AllActivities.Min(z => z.StartDate).Value.ToString("dd/MM/yyyy HH:mm:ss") : null;
                }
                return null;
            }
            set { }
        }
        public string EndDate
        {
            get
            {
                if (this.AllActivities != null && this.AllActivities.Count() > 0)
                { return this.AllActivities.Any(z => z.EndingDate == null) ? null : this.AllActivities.Max(z => z.EndingDate).Value.ToString("dd/MM/yyyy HH:mm:ss"); }
                return null;
            }
            set { }
        }

        public TimeSpan? TotalTimeGrouped
        {
            get
            {
                var time0 = new TimeSpan(0, 0, 0, 0);

                DateTime startDate = new DateTime();
                DateTime endDate = new DateTime();
                if (this.StartDateGroup != null && this.EndDateGroup != null)
                {
                    startDate = Convert.ToDateTime(this.StartDateGroup);
                    endDate = Convert.ToDateTime(this.EndDateGroup);
                    time0 = (endDate - startDate);
                }
                else if (this.StartDate != null && this.EndDate != null)
                {
                    startDate = Convert.ToDateTime(this.StartDate);
                    endDate = Convert.ToDateTime(this.EndDate);
                    time0 = (endDate - startDate);
                }

                return time0;
            }
        }

        public string DateToProduction { get; set; }
        public DateTime WOCreateDate { get; set; }
        public List<WorkActivityViewModel> Activities { get; set; }
        public List<WorkActivityViewModel> AllActivities
        {
            get
            {
                var allActivities = new List<WorkActivityViewModel>();
                if (this.Activities != null)
                {
                    foreach (var father in this.Activities)
                    {
                        allActivities.Add(father);
                        allActivities.AddRange(GetAllChildsActivities(father, new List<WorkActivityViewModel>()));

                    }
                }
                return allActivities;
            }
        }
        public List<WorkActivityViewModel> GetAllChildsActivities(WorkActivityViewModel father, List<WorkActivityViewModel> allChildActivities)
        {
            foreach (var child in father.ChildsActivities)
            {

                if (child.ChildsActivities.Count > 0)
                {
                    allChildActivities.AddRange(GetAllChildsActivities(child, new List<WorkActivityViewModel>()));
                }
                allChildActivities.Add(child);
            }
            return allChildActivities;
        }
        public List<WorkActivitySubGroupViewModel> SubGroups { get; set; }
        public int GroupStationId { get; set; }
        public StationViewModel GroupStation
        {
            get
            {
                if (this.Activities != null && this.Activities.Count != 0)
                {
                    return this.Activities.First().ProductStation.Station;
                }
                return new StationViewModel();
            }
            set { }
        }
        public bool IsGroupActive
        {
            get
            {
                if (this.Activities != null)
                {
                    return this.Activities.Any(x => x.StateActivity == true);
                }
                return false;
            }
        }
        public string ActivitiesIds
        {
            get
            {
                if (this.Activities != null)
                {
                    var intArray = this.Activities.Select(x => x.Id).ToList();
                    var stringArray = string.Join(",", intArray);
                    return stringArray;
                }
                return null;

            }
        }
        public string RelativeActivitiesIds
        {
            get
            {
                if (this.Activities != null)
                {
                    if (this.Activities.Count() > 0)
                    {
                        var intList = this.Activities.Select(activity => activity.Id).ToList();

                        if (this.Activities.FirstOrDefault().ChildsActivities != null)
                        {
                            intList.AddRange(this.Activities.FirstOrDefault().ChildsActivities.Select(x => x.Id).ToList());
                            var stringArray = string.Join(",", intList);
                            return stringArray;
                        }
                    }
                }
                return null;

            }
        }

        //public string RelativeActivitiesIdsSoldaduraAndMontaje
        //{
        //    get
        //    {
        //        if (this.Activities != null)
        //        {
        //            if (this.Activities.Count() > 0)
        //            {
        //                var intList = this.Activities.Select(activity => activity.Id).ToList();
        //                if (this.Activities.FirstOrDefault().ChildsActivities != null)
        //                {
        //                    intList.AddRange(this.Activities.FirstOrDefault().ChildsActivities.Where(child => child.ProductStation.StationId == this.Activities.FirstOrDefault().ProductStation.StationId).Select(x => x.Id).ToList());
        //                    var stringArray = string.Join(",", intList);
        //                    return stringArray;
        //                }
        //            }
        //        }
        //        return null;
        //    }
        //}

        public string RelativeActivitiesIdsSoldaduraAndMontaje
        {
            get
            {
                if (this.Activities != null)
                {
                    if (this.Activities.Count() > 0)
                    {
                        var intList = this.Activities.Select(activity => activity.Id).ToList();

                        if (this.Activities.FirstOrDefault().ChildsActivities != null)
                        {
                            intList.AddRange(getChildsActivitiesIds(this.Activities.FirstOrDefault().ChildsActivities.Where(child => child.ProductStation.StationId == this.Activities.FirstOrDefault().ProductStation.StationId).ToList(), this.Activities.FirstOrDefault().ProductStation.StationId));

                            //intList.AddRange(this.Activities.FirstOrDefault().ChildsActivities.Where(child => child.ProductStation.StationId == this.Activities.FirstOrDefault().ProductStation.StationId).Select(x => x.Id).ToList());

                            var stringArray = string.Join(",", intList);

                            return stringArray;
                        }
                    }
                }
                return null;

            }
        }

        public static List<int> getChildsActivitiesIds(ICollection<WorkActivityViewModel> childsWorkActivitiesVM, int stationId = 0, List<int> intList = null)
        {
            var intListAux = new List<int>();
            try
            {
                if (intList != null)
                {
                    intListAux = intList;
                }

                foreach (var childAct in childsWorkActivitiesVM)
                {
                    intListAux.Add(childAct.Id);

                    if (childAct.ChildsActivities != null && childAct.ChildsActivities.Count() > 0)
                    {
                        getChildsActivitiesIds(childAct.ChildsActivities.Where(child => child.ProductStation.StationId == stationId).ToList(), stationId, intListAux);
                    }
                }

                return intListAux;
            }
            catch
            {
                return intListAux;
            }
        }

        public string WorkOrdersIds
        {
            get
            {
                if (this.Activities != null)
                {
                    var intArray = this.Activities.GroupBy(x => x.WorkOrderItem.WorkOrderId).Select(x => x.First().WorkOrderItem.WorkOrderId).ToList();
                    var stringArray = string.Join(",", intArray);
                    return stringArray;
                }
                return null;
            }
        }

        // Fran: Se agrega atributo para mostrar los WorkOrderIds agrupados por WorkOrderHeaderNumber y Station
        public HashSet<int> WorkOrderIds { get; set; }

        public int TotalAdvance
        {
            get
            {
                //var total_finished = 0;

                //var sumCount = this.Activities.Count();
                //foreach (var act in this.Activities)
                //{
                //    if (act.isFinished)
                //    {
                //        total_finished += 1;
                //    }
                //}
                //if (total_finished > 0)
                //{
                //    var total = Convert.ToInt32(total_finished * 100 / sumCount);

                //    return total;
                //}
                //else
                //    return 0;

                var sum = 0.0;
                var count = 0;
                if (this.Activities != null)
                {
                    foreach (var activity in this.Activities.Where(wa => wa.ToShipments == null))
                    {
                        count++;
                        //si hay accion de stop para la actividad sumamos 1 
                        if (activity.isFinished)
                        {
                            sum += 1;
                        }
                    }
                }

                if (count > 0)
                    return Convert.ToInt32(sum * 100 / count);
                else
                    return 0;
            }
        }

        public int ShipmentsTotalAdvance
        {
            get
            {
                var sum = 0.0;
                var count = 0;
                if (this.Activities != null)
                {
                    foreach (var activity in this.Activities)
                    {
                        count++;
                        //si hay accion de stop para la actividad sumamos 1 
                        if (activity.isFinished)
                        {
                            sum += 1;
                        }
                    }
                }

                if (count > 0)
                    return Convert.ToInt32(sum * 100 / count);
                else
                    return 0;
            }
        }

        public string TotalTimeGroup
        {
            get
            {
                if (this.Activities != null)
                {
                    if (this.Activities.Count() > 0)
                    {
                        if (this.Activities.FirstOrDefault().WorkActions.Any(x => x.ActionName == "stop") == false)
                        {
                            return null;
                        }
                        else
                        {
                            var timeLapse = new TimeSpan(0, 0, 0, 0);
                            foreach (var activity in Activities)
                            {
                                timeLapse = (TimeSpan)(timeLapse + activity.TotalTime);
                            }
                            return timeLapse.ToString(@"dd\ hh\:mm\:ss");
                        }
                    }
                }

                var time0 = new TimeSpan(0, 0, 0, 0);
                return time0.ToString(@"dd\ hh\:mm\:ss");
            }
        }
        public string TotalTimeRelativeActivities
        {
            get
            {
                if (this.Activities != null)
                {
                    if (this.Activities.Count() > 0)
                    {
                        if (this.Activities.FirstOrDefault().WorkActions.Any(x => x.ActionName == "stop") == false)
                        {
                            return null;
                        }
                        else
                        {
                            var timeLapse = new TimeSpan(0, 0, 0, 0);
                            timeLapse = (TimeSpan)(timeLapse + this.Activities.FirstOrDefault().TotalTime);
                            foreach (var activity in this.Activities.FirstOrDefault().ChildsActivities)
                            {
                                timeLapse = (TimeSpan)(timeLapse + activity.TotalTime);
                            }
                            return timeLapse.ToString(@"dd\ hh\:mm\:ss");
                        }
                    }
                }

                var time0 = new TimeSpan(0, 0, 0, 0);
                return time0.ToString(@"dd\ hh\:mm\:ss");
            }
        }

        // Fran: 14/3/2023
        public string TotalTimeRelativeActivitiesMontajeAndSoldadura
        {
            get
            {
                if (this.Activities != null)
                {
                    if (this.Activities.Count() > 0)
                    {
                        if (this.Activities.FirstOrDefault().WorkActions.Any(x => x.ActionName == "stop") == false)
                        {
                            return null;
                        }
                        else
                        {
                            var timeLapse = new TimeSpan(0, 0, 0, 0);
                            if (this.Activities.FirstOrDefault().WorkActions.Any(workAction => workAction.ActionName == "pause"))
                            {
                                // Se le puso pausa 
                                if (this.Activities.FirstOrDefault().isFinished)
                                {
                                    DateTime? startDateAux = null;
                                    string actionNameAux = "";
                                    //foreach (var workAction in this.Activities.FirstOrDefault().WorkActions.Where(workAction => workAction.ActionName == "play" && workAction.WorkActivityId == this.Activities.FirstOrDefault().Id))
                                    foreach (var workAction in this.Activities.FirstOrDefault().WorkActions.Where(workAction => workAction.WorkActivityId == this.Activities.FirstOrDefault().Id))
                                    {
                                        if (workAction.StartDate != null)
                                        {
                                            if (startDateAux.HasValue && actionNameAux != "pause")
                                            {
                                                timeLapse += (workAction.StartDate.Value - startDateAux.Value);
                                                startDateAux = workAction.StartDate.Value;
                                                actionNameAux = workAction.ActionName;
                                            }
                                            else
                                            {
                                                startDateAux = workAction.StartDate.Value;
                                                actionNameAux = workAction.ActionName;
                                            }
                                        }

                                        //if (workAction.EndingDate != null && workAction.StartDate != null)
                                        //{
                                        //    timeLapse = (TimeSpan)(timeLapse + (workAction.EndingDate.Value - workAction.StartDate.Value));
                                        //}
                                    }
                                }

                            }
                            else if (this.Activities.FirstOrDefault().isFinished)
                            {
                                // Se inicio y se finalizo
                                timeLapse = (TimeSpan)(timeLapse + (this.Activities.FirstOrDefault().EndingDate.Value - this.Activities.FirstOrDefault().StartDate.Value));
                            }

                            return timeLapse.ToString(@"dd\ hh\:mm\:ss");
                        }
                    }
                }

                var time0 = new TimeSpan(0, 0, 0, 0);
                return time0.ToString(@"dd\ hh\:mm\:ss");
            }
        }

        public string TotalTimeAllRelativeActivities
        {
            get
            {
                if (this.AllActivities != null)
                {

                    if (this.StopActionAllRelativeActivities == null)
                    {
                        return null;
                    }
                    else
                    {
                        var timeLapse = new TimeSpan(0, 0, 0, 0);
                        timeLapse = (TimeSpan)(this.StopActionAllRelativeActivities.StartDate - this.FirstActionAllRelativesActivities.StartDate);
                        return timeLapse.ToString(@"dd\ hh\:mm\:ss");
                    }
                }

                var time0 = new TimeSpan(0, 0, 0, 0);
                return time0.ToString(@"dd\ hh\:mm\:ss");
            }
        }
        public string DisableCombine
        {
            get
            {
                return this.TotalTimeRelativeActivities != null ? "disabled" : "";
            }
        }
        public string TotalTime { get; set; }
        public string ActivitiesState { get; set; }
        public bool isActive { get; set; }
        // Primera Accion, de la primera actividad del grupo
        public WorkActionViewModel FirstActionGroup
        {
            get
            {
                if (this.Activities != null)
                {
                    if (this.Activities.Count() > 0)
                    {
                        if (this.Activities.FirstOrDefault().WorkActions.Count != 0)
                        {
                            return this.Activities.FirstOrDefault().WorkActions.First();
                        }
                    }
                }
                return null;

            }
        }
        public WorkActionViewModel FirstActionAllRelativesActivities
        {
            get
            {
                if (this.AllActivities != null)
                {
                    var actions = this.AllActivities.Where(activity => activity.WorkActions.Count > 0).Select(act => act.WorkActions.First()).ToList();
                    if (actions.Count != 0)
                    {
                        return actions.OrderBy(action => action.StartDate ?? DateTime.MaxValue).First();
                    }
                }
                return null;

            }
        }
        //Accion "stop" de la primera actividad del grupo
        public WorkActionViewModel StopActionGroup
        {
            get
            {
                if (this.Activities != null)
                {
                    if (this.Activities.Count() > 0)
                    {
                        if (this.Activities.FirstOrDefault().WorkActions.Count != 0 && this.Activities.FirstOrDefault().WorkActions.Any(action => action.ActionName == "stop") == true)
                        {
                            return this.Activities.FirstOrDefault().WorkActions.Where(action => action.ActionName == "stop").FirstOrDefault();
                        }
                    }

                }
                return null;
            }
        }
        public WorkActionViewModel StopActionAllRelativeActivities
        {
            get
            {
                if (this.AllActivities != null && this.AllActivities.Count() > 0)
                {
                    if (this.AllActivities.All(activity => activity.StopAction != null))
                    {
                        var stopactions = this.AllActivities.Select(activity => activity.StopAction).ToList();
                        return stopactions.OrderByDescending(action => action.StartDate ?? DateTime.MaxValue).First();
                    }
                }
                return null;
            }
        }
    }
}
