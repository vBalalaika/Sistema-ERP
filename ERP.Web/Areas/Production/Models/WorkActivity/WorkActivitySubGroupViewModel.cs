using System.Collections.Generic;

namespace ERP.Web.Areas.Production.Models.WorkActivity
{
    public class WorkActivitySubGroupViewModel
    {
        public int Quantity { get; set; }
        public List<WorkActivityViewModel> Activities { get; set; }
        public List<WorkActivitySubGroupViewModel> SubGroups { get; set; }
    }
}
