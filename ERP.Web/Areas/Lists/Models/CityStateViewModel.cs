using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Web.Areas.Lists.Models
{
    public class CityStateViewModel
    {
        public int City_Id { get; set; }
        public string City_Name { get; set; }
        public string City_ZipCode { get; set; }
        public int StateId { get; set; }
        public SelectList States { get; set; }
        public int City_ConcurrencyToken { get; set; }

        public int State_Id { get; set; }
        public string State_Name { get; set; }
        public int CountryId { get; set; }
        public SelectList Countries { get; set; }
        public int State_ConcurrencyToken { get; set; }
    }
}
