using ERP.Web.Areas.ProductMod.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace ERP.Web.Areas.Sales.Models
{
    public class PriceViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public ProductViewModel ProductViewModel { get; set; }
        public int? StructureId { get; set; }
        public StructureViewModel StructureViewModel { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal PriceAll { get; set; }

        public DateTime? PriceAllDate { get; set; }
        public string PriceAllObservations { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal PriceArg { get; set; }

        public DateTime? PriceArgDate { get; set; }
        public string PriceArgObservations { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal PriceMx { get; set; }

        public DateTime? PriceMxDate { get; set; }
        public string PriceMxObservations { get; set; }

        //public DateTime PriceDate { get; set; }

        public int ConcurrencyToken { get; set; }

        /* Only for view model */
        public bool isSpares { get; set; }

        public string CreatedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
    }
}