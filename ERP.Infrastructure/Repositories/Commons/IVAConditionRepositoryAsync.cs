using ERP.Application.Interfaces.Repositories.Commons;
using ERP.Domain.Entities.Commons;
using ERP.Infrastructure.DbContexts;

namespace ERP.Infrastructure.Repositories.Commons
{
    public class IVAConditionRepositoryAsync : GenericRepository<IVACondition>, IIVAConditionRepository
    {
        public IVAConditionRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
