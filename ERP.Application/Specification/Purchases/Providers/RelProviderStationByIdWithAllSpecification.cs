using ERP.Domain.Entities.Purchases.Providers;

namespace ERP.Application.Specification.Purchases.Providers
{
    public class RelProviderStationByIdWithAllSpecification : BaseSpecification<RelProviderStation>
    {
        public RelProviderStationByIdWithAllSpecification(int id) : base(rps => rps.Id == id)
        {
            AddInclude(rps => rps.Station);
            AddInclude(rps => rps.Provider);
        }
    }
}
