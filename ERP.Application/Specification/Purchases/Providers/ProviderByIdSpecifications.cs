
using ERP.Domain.Entities.Purchases.Providers;

namespace ERP.Application.Specification.Purchases.Providers
{
    public class ProviderByIdSpecifications : BaseSpecification<Provider>
    {
        public ProviderByIdSpecifications(int id) : base(p => p.Id == id)
        {
            AddInclude(x => x.Country);
            AddInclude(x => x.State);
            AddInclude(x => x.City);
            AddInclude(x => x.DocumentType);
            AddInclude(x => x.IVACondition);
            AddInclude($"{nameof(Provider.Subsidiaries)}.{nameof(Subsidiary.Country)}");
            AddInclude($"{nameof(Provider.Subsidiaries)}.{nameof(Subsidiary.State)}");
            AddInclude($"{nameof(Provider.Subsidiaries)}.{nameof(Subsidiary.City)}");
        }
    }
}
