using System.ComponentModel.DataAnnotations;

namespace ERP.Web.Areas.ProductMod.Models
{
    public class RelProductStationViewModel
    {
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal? Time { get; set; }
        public int? Order { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal? Cost { get; set; }


        public StationViewModel Station { get; set; }
        public int StationId { get; set; }



        public int Id { get; set; }
        public int ConcurrencyToken { get; set; }
    }
}
