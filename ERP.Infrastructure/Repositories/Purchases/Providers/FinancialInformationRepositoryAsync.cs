using ERP.Application.Interfaces.Repositories.Purchases.Providers;
using ERP.Domain.Entities.Purchases.Providers;
using ERP.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Repositories.Purchases.Providers
{
    public class FinancialInformationRepositoryAsync : GenericRepository<FinancialInformation>, IFinancialInformationRepository
    {
        public FinancialInformationRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<IReadOnlyList<FinancialInformation>> GetAll()
        {
            return await _dbContext
           .Set<FinancialInformation>()
           .AsNoTracking()
           .ToListAsync();
        }

        public async Task<IReadOnlyList<FinancialInformation>> GetByProviderIdAsync(int id)
        {
            var financialInfo = await _dbContext
                .Set<FinancialInformation>()
                .Where(fi => fi.ProviderId == id)
                .ToListAsync();
            return financialInfo;
        }
    }
}
