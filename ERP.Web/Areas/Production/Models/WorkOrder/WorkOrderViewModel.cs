using ERP.Web.Areas.ProductMod.Models;
using ERP.Web.Areas.Sales.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERP.Web.Areas.Production.Models
{
    public class WorkOrderViewModel
    {
        public int Id { get; set; }
        public string NumberVersionStructure { get; set; }
        public string Observation { get; set; }
        public int OrderNumber { get; set; }
        public OrderDetailViewModel OrderDetail { get; set; }
        public int OrderDetailId { get; set; }
        public WorkOrderHeaderViewModel WorkOrderHeader { get; set; }
        public int WorkOrderHeaderId { get; set; }
        public StructureViewModel FullStructure { get; set; }
        public ICollection<WorkOrderItemViewModel> WorkOrderItems { get; set; } = new List<WorkOrderItemViewModel>();
        public SelectList ProductSelectList { get; set; }
        public SelectList StructuresByProduct { get; set; }
        public string maxLevelChilds
        {
            get
            {
                var intLevels = new List<int>();
                var stringLevels = this.WorkOrderItems.Select(item => item.Level.Split('.')[0]);
                foreach (var stringLevel in stringLevels)
                {
                    intLevels.Add(Convert.ToInt32(stringLevel));
                }
                if (intLevels.Count > 0)
                {
                    return intLevels.Max(level => level).ToString();
                }
                return "0";
            }
        }
        public int TotalAdvance
        {
            get
            {
                var sum = 0.0;
                var count = 0;
                foreach (var workorderitem in this.WorkOrderItems.Where(x => x.WorkActivities.Count > 0).GroupBy(x => x.Level).Select(y => y.First()).ToList())
                {
                    foreach (var activity in workorderitem.WorkActivities)
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
        public string GetState
        {
            get
            {
                if (this.TotalAdvance == 100)
                {
                    return ActivityStates.Finalizado;
                }
                foreach (var workorderitem in this.WorkOrderItems.Where(x => x.WorkActivities.Count > 0))
                {
                    foreach (var activity in workorderitem.WorkActivities.Where(x => x.StateActivity == true))
                    {
                        if (activity.StartDate != null)
                        {
                            return ActivityStates.EnProceso;
                        }
                    }
                }

                return ActivityStates.Pendiente;

            }
        }

        //Datos que pueden ser calculados, pero se evita logica
        public DateTime? StartDate
        {
            get
            {
                if (this.WorkOrderItems.Count > 0 && this.WorkOrderItems.Any(workOrderItem => workOrderItem.StartDate != null))
                {
                    return this.WorkOrderItems.Where(workOrderItem => workOrderItem.StartDate.HasValue).Select(workOrderItem => workOrderItem.StartDate).Min(date => date);
                }
                return null;
            }
            set { }
        }
        public DateTime? EndingDate { get; set; }
        public DateTime? TotalTime { get; set; }
    }
}
