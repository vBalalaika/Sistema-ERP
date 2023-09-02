namespace ERP.Web.Areas.ProductMod.Models
{
    public class AccessoryProductViewModel
    {
        public ProductViewModel ProductAccessory { get; set; }
        public int IdProductAccessory { get; set; }
        public int ConcurrencyToken { get; set; }

        public int Id { get; set; }
    }
}
