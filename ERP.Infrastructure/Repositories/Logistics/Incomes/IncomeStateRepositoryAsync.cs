using ERP.Application.Interfaces.Repositories.Logistics.Incomes;
using ERP.Domain.Entities.Logistics.Incomes;
using ERP.Infrastructure.DbContexts;

namespace ERP.Infrastructure.Repositories.Logistics.Incomes
{
    public class IncomeStateRepositoryAsync : GenericRepository<IncomeState>, IIncomeStateRepository
    {
        public IncomeStateRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
