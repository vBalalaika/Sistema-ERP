using ERP.Application.Interfaces.Repositories.Purchases.MissingProducts;
using ERP.Domain.Entities.Purchases.MissingProducts;
using ERP.Infrastructure.DbContexts;

namespace ERP.Infrastructure.Repositories.Purchases.MissingProducts
{
    public class StateMissingProductRepositoryAsync : GenericRepository<PurchaseState>, IPurchaseStateRepository
    {
        public StateMissingProductRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
