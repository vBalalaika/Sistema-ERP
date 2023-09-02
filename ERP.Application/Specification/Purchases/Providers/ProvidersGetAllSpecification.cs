using ERP.Domain.Entities.Purchases.Providers;

namespace ERP.Application.Specification.Purchases.Providers
{
    public class ProvidersGetAllSpecification : BaseSpecification<Provider>
    {
        public ProvidersGetAllSpecification()
        {

        }

        public ProvidersGetAllSpecification(string searchParameterByCodeOrDescription) : base(prov => (prov.BusinessName.Contains(searchParameterByCodeOrDescription) || prov.Name.Contains(searchParameterByCodeOrDescription)))
        {
            AddInclude(prov => prov.RelProviderProducts);
        }
    }
}
