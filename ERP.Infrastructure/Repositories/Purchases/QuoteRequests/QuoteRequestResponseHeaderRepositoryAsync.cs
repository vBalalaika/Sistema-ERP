using ERP.Application.Interfaces.Repositories.Purchases.QuoteRequests;
using ERP.Domain.Entities.Purchases.QuoteRequests;
using ERP.Infrastructure.DbContexts;

namespace ERP.Infrastructure.Repositories.Purchases.QuoteRequests
{
    public class QuoteRequestResponseHeaderRepositoryAsync : GenericRepository<QuoteRequestResponseHeader>, IQuoteRequestResponseHeaderRepository
    {
        public QuoteRequestResponseHeaderRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
