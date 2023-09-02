using ERP.Domain.Entities.Purchases.Providers;
using System.ComponentModel.DataAnnotations;

namespace ERP.Web.Areas.Purchases.Models.Providers
{
    public class ContactViewModel
    {
        public int Id { get; set; }
        public int ConcurrencyToken { get; set; }

        public string Name { get; set; }
        public string Charge { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string SubsidiaryNumber { get; set; }

        public int ProviderId { get; set; }
        public Provider Provider { get; set; }
    }
}
