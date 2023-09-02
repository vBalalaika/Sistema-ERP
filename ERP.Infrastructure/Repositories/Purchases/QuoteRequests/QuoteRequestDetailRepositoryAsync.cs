using ERP.Application.Interfaces.Repositories.Purchases.QuoteRequests;
using ERP.Domain.Entities.Purchases.QuoteRequests;
using ERP.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Repositories.Purchases.QuoteRequests
{
    public class QuoteRequestDetailRepositoryAsync : GenericRepository<QuoteRequestDetail>, IQuoteRequestDetailRepository
    {
        public QuoteRequestDetailRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<IReadOnlyList<QuoteRequestDetail>> GetAll()
        {
            return await _dbContext
              .Set<QuoteRequestDetail>()
              .AsNoTracking()
              .ToListAsync();
        }
    }
}
