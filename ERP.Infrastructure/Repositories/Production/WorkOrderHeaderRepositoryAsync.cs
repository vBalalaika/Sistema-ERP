using ERP.Application.Interfaces.Repositories.Production;
using ERP.Domain.Entities.Production;
using ERP.Infrastructure.DbContexts;

namespace ERP.Infrastructure.Repositories.Production
{
    public class WorkOrderHeaderRepositoryAsync : GenericRepository<WorkOrderHeader>, IWorkOrderHeaderRepository
    {
        public WorkOrderHeaderRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
