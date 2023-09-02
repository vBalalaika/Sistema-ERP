using ERP.Application.Interfaces.Repositories.Production;
using ERP.Domain.Entities.Production;
using ERP.Infrastructure.DbContexts;

namespace ERP.Infrastructure.Repositories.Production
{
    public class WorkActionRepositoryAsync : GenericRepository<WorkAction>, IWorkActionRepository
    {
        public WorkActionRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
