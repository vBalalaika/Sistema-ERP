using ERP.Application.Interfaces.Repositories.Commons;
using ERP.Domain.Entities.Commons;
using ERP.Infrastructure.DbContexts;

namespace ERP.Infrastructure.Repositories.Commons
{
    public class DollarExchangeRateRepositoryAsync : GenericRepository<DollarExchangeRate>, IDollarExchangeRateRepository
    {
        public DollarExchangeRateRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
