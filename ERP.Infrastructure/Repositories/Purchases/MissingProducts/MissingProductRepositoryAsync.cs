using ERP.Application.Interfaces.Repositories.Purchases.MissingProducts;
using ERP.Domain.Entities.Purchases.MissingProducts;
using ERP.Infrastructure.DbContexts;

namespace ERP.Infrastructure.Repositories.Purchases.MissingProducts
{
    public class MissingProductRepositoryAsync : GenericRepository<MissingProduct>, IMissingProductRepository
    {
        public MissingProductRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
