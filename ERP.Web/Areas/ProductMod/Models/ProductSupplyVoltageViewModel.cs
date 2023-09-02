namespace ERP.Web.Areas.ProductMod.Models
{
    public class ProductSupplyVoltageViewModel
    {
        public SupplyVoltageViewModel SupplyVoltage { get; set; }
        public int SupplyVoltageId { get; set; }


        public int ConcurrencyToken { get; set; }
        public int Id { get; set; }
    }
}
