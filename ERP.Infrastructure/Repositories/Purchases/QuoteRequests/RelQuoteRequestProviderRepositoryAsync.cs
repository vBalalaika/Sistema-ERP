using ERP.Application.Interfaces.Repositories.Purchases.QuoteRequests;
using ERP.Domain.Entities.Purchases.QuoteRequests;
using ERP.Infrastructure.DbContexts;

namespace ERP.Infrastructure.Repositories.Purchases.QuoteRequests
{
    public class RelQuoteRequestProviderRepositoryAsync : GenericRepository<RelQuoteRequestProvider>, IRelQuoteRequestProviderRepository
    {
        public RelQuoteRequestProviderRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
