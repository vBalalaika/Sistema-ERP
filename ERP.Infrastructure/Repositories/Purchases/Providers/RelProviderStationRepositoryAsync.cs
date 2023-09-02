using ERP.Application.Interfaces.Repositories.Purchases.Providers;
using ERP.Domain.Entities.Purchases.Providers;
using ERP.Infrastructure.DbContexts;

namespace ERP.Infrastructure.Repositories.Purchases.Providers
{
    public class RelProviderStationRepositoryAsync : GenericRepository<RelProviderStation>, IRelProviderStationRepository
    {
        public RelProviderStationRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
