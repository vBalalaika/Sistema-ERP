using ERP.Application.Interfaces.Repositories.Production;
using ERP.Domain.Entities.Production;
using ERP.Infrastructure.DbContexts;

namespace ERP.Infrastructure.Repositories.Production
{
    public class WorkOrderRepositoryAsync : GenericRepository<WorkOrder>, IWorkOrderRepository
    {
        public WorkOrderRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
