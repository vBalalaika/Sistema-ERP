using ERP.Web.Areas.Admin.Models;
using ERP.Web.Areas.Production.Models.WorkActivity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERP.Web.Areas.Production.Models
{
    public class WorkOrderHeaderViewModel
    {
        public int Id { get; set; }
        public int WorkOrderHeaderNumber { get; set; }
        public string Observation { get; set; }
        public DateTime CreatedOn { get; set; }
        public ICollection<WorkOrderViewModel> WorkOrders { get; set; } = new List<WorkOrderViewModel>();
        public List<WorkActivityViewModel> WorkActivities { get; set; } = new List<WorkActivityViewModel>();
        public List<WorkActivityGroupViewModel> WorkActivitiesGroupsByStation { get; set; } = new List<WorkActivityGroupViewModel>();
        public IList<UserViewModel> UsersList { get; set; }
        public SelectList UsersSelectList { get; set; }
        public string StationsIds
        {
            get
            {
                int[] stationsIds = Array.Empty<int>();
                if (this.WorkActivitiesGroupsByStation != null)
                {
                    stationsIds = this.WorkActivitiesGroupsByStation.Select(group => group.GroupStation.Id).ToArray();
                }
                return string.Join(',', stationsIds);
            }
        }

    }
}
