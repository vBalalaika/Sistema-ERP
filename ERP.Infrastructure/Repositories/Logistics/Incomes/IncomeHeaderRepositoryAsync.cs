using ERP.Application.Interfaces.Repositories.Logistics.Incomes;
using ERP.Domain.Entities.Logistics.Incomes;
using ERP.Infrastructure.DbContexts;

namespace ERP.Infrastructure.Repositories.Logistics.Incomes
{
    public class IncomeHeaderRepositoryAsync : GenericRepository<IncomeHeader>, IIncomeHeaderRepository
    {
        public IncomeHeaderRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
