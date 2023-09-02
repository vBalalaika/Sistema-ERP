using ERP.Domain.Entities.Purchases.Providers;

namespace ERP.Application.Specification.Purchases.Providers
{
    public class RelProviderStationByProviderAndStationSpecification : BaseSpecification<RelProviderStation>
    {
        public RelProviderStationByProviderAndStationSpecification(int providerId, int stationId) : base(rps => rps.ProviderId == providerId && rps.StationId == stationId)
        {
            AddInclude(rps => rps.PriceUnitMeasure);
        }
    }
}
