using ERP.Application.Interfaces.Repositories.ProductMod;
using ERP.Domain.Entities.ProductMod;
using ERP.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ERP.Infrastructure.Repositories.ProductMod
{
    class RelProductStationRepositoryAsync : GenericRepository<RelProductStation>, IRelProductStationRepository
    {
        public RelProductStationRepositoryAsync(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<IList<RelProductStation>> GetAll()
        {
            return await _dbContext.Set<RelProductStation>().AsNoTracking().ToListAsync();
        }
    }
}
