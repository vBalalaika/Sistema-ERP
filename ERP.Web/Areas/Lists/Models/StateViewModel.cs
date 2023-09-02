using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Web.Areas.Lists.Models
{
    public class StateViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public SelectList Countries { get; set; }
        public int ConcurrencyToken { get; set; }
    }
}
