using ERP.Web.Areas.ProductMod.Models;

namespace ERP.Web.Areas.Production.Models.WorkAction
{
    public class GroupedWorkActionViewModel
    {
        public int Id { get; set; }
        public string WorkActivitiesIds { get; set; }
        public int StationId { get; set; }
        public StationViewModel Station { get; set; }
        public int ConcurrencyToken { get; set; }
    }
}
