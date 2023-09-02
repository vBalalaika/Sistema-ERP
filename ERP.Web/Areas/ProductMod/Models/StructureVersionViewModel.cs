using System;

namespace ERP.Web.Areas.ProductMod.Models
{
    public class StructureVersionViewModel
    {
        public int Id { get; set; }
        public int VersionNumber { get; set; }
        public string Comment { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
