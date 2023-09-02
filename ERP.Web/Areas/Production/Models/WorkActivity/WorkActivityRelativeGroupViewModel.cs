using System.Collections.Generic;
using System.Linq;

namespace ERP.Web.Areas.Production.Models.WorkActivity
{
    public class WorkActivityRelativeGroupViewModel
    {
        public List<WorkActivityViewModel> FathersActivities = new List<WorkActivityViewModel>();
        public List<WorkActivityViewModel> RelativeChilds
        {
            get
            {
                var allActivities = new List<WorkActivityViewModel>();
                if (this.FathersActivities != null)
                {
                    foreach (var father in FathersActivities)
                    {
                        foreach (var child in father.ChildsActivities)
                        {
                            allActivities.Add(child);
                        }
                    }
                }
                return allActivities;
            }
        }
        public bool isTorno { get; set; }
        public bool isOnTopLevel { get; set; }
        public List<WorkActivityViewModel> AllRelativeActivities
        {
            get
            {
                var allActivities = new List<WorkActivityViewModel>();
                if (this.FathersActivities != null)
                {
                    foreach (var father in FathersActivities)
                    {
                        allActivities.Add(father);
                        allActivities.AddRange(getDescendents(father, new List<WorkActivityViewModel>()));
                    }
                }
                return allActivities;
            }
        }
        public List<WorkActivityViewModel> getDescendents(WorkActivityViewModel fatherActivity, List<WorkActivityViewModel> allDescendents)
        {
            foreach (var child in fatherActivity.ChildsActivities)
            {
                allDescendents.Add(child);
                getDescendents(child, allDescendents);
            }

            return allDescendents;
        }
        public List<WorkActivityViewModel> UnfinishedChilds
        {
            get
            {
                var unfinishedChilds = new List<WorkActivityViewModel>();
                foreach (var child in this.RelativeChilds)
                {
                    if (child.ChildsActivities.Any(grandchild => !grandchild.isFinished))
                    {
                        unfinishedChilds.Add(child);
                    }
                }
                return unfinishedChilds;
            }
        }
        public bool CanMakeActions
        {
            get
            {
                return !(!this.isTorno && this.UnfinishedChilds.Count > 0);
            }
        }
        public string UnfinishedChildsIds { get { return string.Join(',', this.UnfinishedChilds.Select(child => child.Id)); } }
        public string DisableNext { get { return this.UnfinishedChilds.Count > 0 ? "" : "disabled"; } }
        public string DisableBack { get { return this.isOnTopLevel ? "disabled" : ""; } }
        public string FathersIds { get { return string.Join(',', this.FathersActivities.Select(father => father.Id)); } }
        public string FathersCodes
        {
            get
            {
                if (FathersActivities.Count > 0)
                {
                    return string.Join(',', FathersActivities.Select(father => father.WorkOrderItem.Product.Code).ToList());

                }
                return null;
            }
        }
        public string GroupIds
        {
            get
            {
                if (this.isTorno)
                {
                    var allactivities = this.AllRelativeActivities.Select(x => x.Id).ToList();
                    return string.Join(',', allactivities);
                }
                var idList = new List<int>();
                foreach (var father in this.FathersActivities)
                {
                    idList.Add(father.Id);
                    foreach (var child in father.ChildsActivities)
                    {
                        idList.Add(child.Id);
                    }

                }
                return string.Join(',', idList);
            }
        }
        public int StationId { get; set; }
        public string InitialFathersIds { get; set; }
        // Primera Accion, de la primera actividad del grupo
        public WorkActionViewModel FirstActionGroup
        {
            get
            {
                if (this.FathersActivities != null)
                {
                    if (this.FathersActivities.FirstOrDefault().WorkActions.Count != 0)
                    {
                        return this.FathersActivities.FirstOrDefault().WorkActions.First();
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
                if (this.FathersActivities != null)
                {
                    if (this.FathersActivities.FirstOrDefault().WorkActions.Count != 0 && this.FathersActivities.FirstOrDefault().WorkActions.Any(action => action.ActionName == "stop") == true)
                        if (this.FathersActivities.FirstOrDefault().WorkActions.Count != 0 && this.FathersActivities.FirstOrDefault().WorkActions.Any(action => action.ActionName == "stop") == true)
                        {
                            return this.FathersActivities.FirstOrDefault().WorkActions.Where(action => action.ActionName == "stop").FirstOrDefault();
                        }
                }
                return null;
            }
        }
    }
}
