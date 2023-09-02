namespace ERP.Web.Areas.Logistics.Models
{
    public class IncomeStateViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        public int ConcurrencyToken { get; set; }
    }
}