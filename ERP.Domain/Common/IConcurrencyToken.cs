namespace ERP.Domain.Common
{
    public interface IConcurrencyToken
    {
        public int ConcurrencyToken { get; set; }
    }
}