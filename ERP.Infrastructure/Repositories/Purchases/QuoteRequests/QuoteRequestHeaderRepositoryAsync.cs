using ERP.Application.Interfaces.Repositories.Purchases.QuoteRequests;
using ERP.Domain.Entities.Purchases.QuoteRequests;
using ERP.Infrastructure.DbContexts;

namespace ERP.Infrastructure.Repositories.Purchases.QuoteRequests
{
    public class QuoteRequestHeaderRepositoryAsync : GenericRepository<QuoteRequestHeader>, IQuoteRequestHeaderRepository
    {
        public QuoteRequestHeaderRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
