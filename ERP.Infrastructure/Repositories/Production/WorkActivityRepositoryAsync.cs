using ERP.Application.Interfaces.Repositories.Production;
using ERP.Domain.Entities.Production;
using ERP.Infrastructure.DbContexts;

namespace ERP.Infrastructure.Repositories.Production
{
    public class WorkActivityRepositoryAsync : GenericRepository<WorkActivity>, IWorkActivityRepository
    {
        public WorkActivityRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
