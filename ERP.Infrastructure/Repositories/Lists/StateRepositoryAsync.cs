using ERP.Application.Interfaces.Repositories.Lists;
using ERP.Domain.Entities.Lists;
using ERP.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Repositories.Lists
{
    public class StateRepositoryAsync : GenericRepository<State>, IStateRepository
    {
        public StateRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<IReadOnlyList<State>> GetStatesByCountryIdAsync(int countryId)
        {
            var stateRepo = await _dbContext
                .States
                .Where(s => s.CountryId == countryId)
                .OrderBy(s => s.Name)
                .ToListAsync();

            return stateRepo;
        }
    }
}
