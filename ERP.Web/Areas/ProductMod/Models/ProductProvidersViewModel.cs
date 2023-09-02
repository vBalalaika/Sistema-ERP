using ERP.Web.Areas.Purchases.Models.Providers;
using System.Collections.Generic;

namespace ERP.Web.Areas.ProductMod.Models
{
    public class ProductProvidersViewModel
    {
        public ProductViewModel Product { get; set; }
        public IEnumerable<RelProviderProductViewModel> Providers { get; set; }
    }
}
