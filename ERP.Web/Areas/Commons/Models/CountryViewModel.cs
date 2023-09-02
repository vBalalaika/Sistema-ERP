namespace ERP.Web.Areas.Commons.Models
{
    public class CountryViewModel
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Denomination { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
