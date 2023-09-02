using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace ERP.Web.Areas.ProductMod.Models
{
    public class StructureViewModel
    {
        public IList<ProductViewModel> ProductsList { get; set; } = new List<ProductViewModel>();
        public SelectList Structures { get; set; }
        public IList<StructureItemViewModel> StructureItems { get; set; }
        public string Description { get; set; }
        public Boolean IsBase { get; set; }
        public bool IsStandard { get; set; }
        public string viewToRender { get; set; }
        public int Id { get; set; }
        public int ConcurrencyToken { get; set; }

        public StructureVersionViewModel LastVersion { get; set; }
        public int LastVersionId { get; set; }
        public SelectList SupplyVoltages { get; set; }
        public SupplyVoltageViewModel? SupplyVoltage { get; set; }
        public int? SupplyVoltageId { get; set; }

        public ProductViewModel Product { get; set; }
        public int ProductId { get; set; }

    }
}
