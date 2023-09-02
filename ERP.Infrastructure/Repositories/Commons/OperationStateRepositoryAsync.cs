using ERP.Application.Interfaces.Repositories.Commons;
using ERP.Domain.Entities.Commons;
using ERP.Infrastructure.DbContexts;

namespace ERP.Infrastructure.Repositories.Commons
{
    public class OperationStateRepositoryAsync : GenericRepository<OperationState>, IOperationStateRepository
    {
        public OperationStateRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
