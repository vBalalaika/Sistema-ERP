using ERP.Application.Interfaces.Repositories.Production;
using ERP.Domain.Entities.Production;
using ERP.Infrastructure.DbContexts;

namespace ERP.Infrastructure.Repositories.Production
{
    public class GroupedWorkActionRepositoryAsync : GenericRepository<GroupedWorkAction>, IGroupedWorkActionRepository
    {
        public GroupedWorkActionRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
