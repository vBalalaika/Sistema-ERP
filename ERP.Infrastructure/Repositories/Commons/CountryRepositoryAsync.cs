using ERP.Application.Interfaces.Repositories.Commons;
using ERP.Domain.Entities.Commons;
using ERP.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Repositories.Commons
{
    public class CountryRepositoryAsync : GenericRepository<Country>, ICountryRepository
    {
        public CountryRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<IReadOnlyList<Country>> GetListActiveAsync()
        {
            var countrysActive = await _dbContext
                .Set<Country>()
                .Where(c => c.IsActive == true)
                .OrderBy(c => c.Denomination)
                .ToListAsync();
            return countrysActive;
        }
    }
}
