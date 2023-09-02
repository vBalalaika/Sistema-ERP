namespace ERP.Domain.Entities.Commons
{
    public class DocumentType
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
