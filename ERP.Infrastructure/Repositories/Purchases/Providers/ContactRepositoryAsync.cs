using ERP.Application.Interfaces.Repositories.Purchases.Providers;
using ERP.Domain.Entities.Purchases.Providers;
using ERP.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Repositories.Purchases.Providers
{
    public class ContactRepositoryAsync : GenericRepository<Contact>, IContactRepository
    {
        public ContactRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<IReadOnlyList<Contact>> GetAll()
        {
            return await _dbContext
              .Set<Contact>()
              .AsNoTracking()
              .ToListAsync();
        }

        public async Task<IReadOnlyList<Contact>> GetByProviderIdAsync(int id)
        {
            var contacts = await _dbContext
                .Set<Contact>()
                .Where(c => c.ProviderId == id)
                .ToListAsync();
            return contacts;
        }
    }
}
