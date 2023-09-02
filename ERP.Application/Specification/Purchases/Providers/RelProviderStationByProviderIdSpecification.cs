using ERP.Domain.Entities.Purchases.Providers;

namespace ERP.Application.Specification.Purchases.Providers
{
    public class RelProviderStationByProviderIdSpecification : BaseSpecification<RelProviderStation>
    {
        public RelProviderStationByProviderIdSpecification(int providerId) : base(rps => rps.ProviderId == providerId)
        {
            AddInclude(rps => rps.Station);
            AddInclude(rps => rps.Provider);
        }
    }
}
