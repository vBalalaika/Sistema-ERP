using ERP.Domain.Entities.Clients;

namespace ERP.Web.Areas.Clients.Models
{
    public class ConsultedProductViewModel
    {
        public int Id { get; set; }
        public int CommunicationId { get; set; }
        public Communication Communication { get; set; }
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductDescription { get; set; }
        public int StructureId { get; set; }
        public string StructureDescription { get; set; }
        public int FunctionalityId { get; set; }
        public string Functionality { get; set; }
        public int PieceTypeId { get; set; }
        public string PieceType { get; set; }

        public int ConcurrencyToken { get; set; }
    }
}
