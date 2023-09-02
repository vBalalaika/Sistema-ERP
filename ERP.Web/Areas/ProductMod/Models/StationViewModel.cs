using Microsoft.AspNetCore.Mvc.Rendering;
namespace ERP.Web.Areas.ProductMod.Models
{
    public class StationViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string Abbreviation { get; set; }
        public string WorkOrderDisplayType { get; set; }
        public SelectList FunctionalAreas { get; set; }
        public int FunctionalAreaId { get; set; }
        public FunctionalAreaViewModel FunctionalArea { get; set; }
        public string DefaultUser { get; set; }
        public string Users { get; set; }
        public string[] UsersIds { get; set; }
        public SelectList UsersSelectList { get; set; }
        public int ConcurrencyToken { get; set; }

        public string AbreviationDescription
        {
            get
            {
                return this.Abbreviation + " - " + this.Description;
            }
        }
    }
}
