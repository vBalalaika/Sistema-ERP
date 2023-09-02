using ERP.Application.Interfaces.Repositories.Logistics.Incomes;
using ERP.Domain.Entities.Logistics.Incomes;
using ERP.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Repositories.Logistics.Incomes
{
    public class IncomeDetailRepositoryAsync : GenericRepository<IncomeDetail>, IIncomeDetailRepository
    {
        public IncomeDetailRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<IReadOnlyList<IncomeDetail>> GetAll()
        {
            return await _dbContext
              .Set<IncomeDetail>()
              .AsNoTracking()
              .ToListAsync();
        }
    }
}
