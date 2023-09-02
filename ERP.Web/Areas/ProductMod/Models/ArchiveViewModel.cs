namespace ERP.Web.Areas.ProductMod.Models
{
    public class ArchiveViewModel
    {
        public string PathUrl { get; set; }

        public ArchiveTypeViewModel? ArchiveType { get; set; }
        public int? ArchiveTypeId { get; set; }

        public int ConcurrencyToken { get; set; }
        public int Id { get; set; }

        public int ProductId { get; set; }
    }
}
