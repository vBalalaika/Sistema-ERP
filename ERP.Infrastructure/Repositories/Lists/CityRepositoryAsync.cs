using ERP.Application.Interfaces.Repositories.Lists;
using ERP.Domain.Entities.Lists;
using ERP.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Repositories.Lists
{
    public class CityRepositoryAsync : GenericRepository<City>, ICityRepository
    {
        public CityRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
        public async Task<IReadOnlyList<City>> GetCitiesByStateIdAsync(int? stateId)
        {
            if (stateId != null)
            {
                var cityRepo = await _dbContext
                    .Cities
                    .Where(c => c.StateId == stateId)
                    .OrderBy(c => c.Name)
                    .ToListAsync();

                return cityRepo;
            }
            else
            {
                return null;
            }

        }

    }
}
