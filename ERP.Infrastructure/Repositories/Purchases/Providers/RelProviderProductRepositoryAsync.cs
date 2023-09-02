using ERP.Application.Interfaces.Repositories.Purchases.Providers;
using ERP.Domain.Entities.Purchases.Providers;
using ERP.Infrastructure.DbContexts;

namespace ERP.Infrastructure.Repositories.Purchases.Providers
{
    public class RelProviderProductRepositoryAsync : GenericRepository<RelProviderProduct>, IRelProviderProductRepository
    {
        public RelProviderProductRepositoryAsync(ApplicationDbContext context) : base(context)
        {

        }
    }
}
