using ERP.Domain.Entities.Purchases.Providers;

namespace ERP.Web.Areas.Purchases.Models.Providers
{
    public class FinancialInformationViewModel
    {
        public int Id { get; set; }
        public int ConcurrencyToken { get; set; }

        public string Bank { get; set; }
        public string AccountType { get; set; }
        public string AccountNumber { get; set; }
        public string CBU { get; set; }
        public string Observations { get; set; }

        public int ProviderId { get; set; }
        public Provider Provider { get; set; }
    }
}
