using ERP.Application.Interfaces.Repositories.Sales;
using ERP.Domain.Entities.Sales;
using ERP.Infrastructure.DbContexts;

namespace ERP.Infrastructure.Repositories.Sales
{
    public class PriceRepositoryAsync : GenericRepository<Price>, IPriceRepository
    {
        public PriceRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
