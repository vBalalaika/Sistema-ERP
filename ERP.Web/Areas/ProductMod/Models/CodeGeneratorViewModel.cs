using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ERP.Web.Areas.ProductMod.Models
{
    public class CodeGeneratorViewModel
    {

        public SubCategoryViewModel SubCategory { get; set; }
        public int SubCategoryId { get; set; }
        public SelectList SubCategories { get; set; }
        public IList<ProductViewModel> Products { get; set; } = new List<ProductViewModel>();
        public int Quantity { get; set; }
    }
}
