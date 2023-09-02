namespace ERP.Web.Areas.ProductMod.Models
{
    public class UnitMeasureViewModel
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int UnitType { get; set; }
        public string Abbreviation { get; set; }
    }
}
