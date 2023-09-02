
namespace ERP.Web.Areas.ProductMod.Models
{
    public class OperationViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int ConcurrencyToken { get; set; }
    }
}
