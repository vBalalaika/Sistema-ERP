using Microsoft.AspNetCore.Mvc.Rendering;
namespace ERP.Web.Areas.ProductMod.Models
{
    public class SubCategoryViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Prefix { get; set; }
        public bool IsActive { get; set; }
        public SelectList Categories { get; set; }
        public int CategoryId { get; set; }
        public CategoryViewModel Category { get; set; }
        public int ConcurrencyToken { get; set; }
    }
}
