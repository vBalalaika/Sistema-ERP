using ERP.Application.Interfaces.Repositories.Purchases.Providers;
using ERP.Domain.Entities.Purchases.Providers;
using ERP.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Repositories.Purchases.Providers
{
    public class SubsidiaryRepositoryAsync : GenericRepository<Subsidiary>, ISubsidiaryRepository
    {
        public SubsidiaryRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<IReadOnlyList<Subsidiary>> GetAll()
        {
            return await _dbContext
            .Set<Subsidiary>()
            .Include(s => s.Country)
            .Include(s => s.State)
            .Include(s => s.City)
            .AsNoTracking()
            .ToListAsync();
        }

        public async Task<IReadOnlyList<Subsidiary>> GetByProviderIdAsync(int id)
        {
            var subsidiaries = await _dbContext
                .Set<Subsidiary>()
                .Include(s => s.Country)
                .Include(s => s.State)
                .Include(s => s.City)
                .Where(s => s.ProviderId == id)
                .ToListAsync();
            return subsidiaries;
        }
    }
}
