using ERP.Application.Interfaces.Repositories.Purchases.QuoteRequests;
using ERP.Domain.Entities.Purchases.QuoteRequests;
using ERP.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Repositories.Purchases.QuoteRequests
{
    public class QuoteRequestResponseDetailRepositoryAsync : GenericRepository<QuoteRequestResponseDetail>, IQuoteRequestResponseDetailRepository
    {
        public QuoteRequestResponseDetailRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<IReadOnlyList<QuoteRequestResponseDetail>> GetAll()
        {
            return await _dbContext
              .Set<QuoteRequestResponseDetail>()
              .AsNoTracking()
              .ToListAsync();
        }
    }
}
