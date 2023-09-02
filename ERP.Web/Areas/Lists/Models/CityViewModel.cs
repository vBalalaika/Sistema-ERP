using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Web.Areas.Lists.Models
{
    public class CityViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ZipCode { get; set; }
        public int StateId { get; set; }
        public SelectList States { get; set; }
        public int ConcurrencyToken { get; set; }
    }
}
