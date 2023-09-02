using ERP.Application.Interfaces.Repositories.Purchases.Providers;
using ERP.Domain.Entities.Purchases.Providers;
using ERP.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Repositories.Purchases.Providers
{
    public class ProviderRepositoryAsync : GenericRepository<Provider>, IProviderRepository
    {
        public ProviderRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public new async Task<IReadOnlyList<Provider>> GetListAsync()
        {
            return await _dbContext
                 .Set<Provider>()
                 .Include(c => c.Country)
                 .Include(s => s.State)
                 .Include(c => c.City)
                 .Include(dt => dt.DocumentType)
                 .Include(iva => iva.IVACondition)
                 .ToListAsync();
        }

    }
}
