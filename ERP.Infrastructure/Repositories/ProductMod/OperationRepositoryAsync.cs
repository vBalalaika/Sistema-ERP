using ERP.Application.Interfaces.Repositories.ProductMod;
using ERP.Domain.Entities.ProductMod;
using ERP.Infrastructure.DbContexts;

namespace ERP.Infrastructure.Repositories.ProductMod
{
    public class OperationRepositoryAsync : GenericRepository<Operation>, IOperationRepository
    {
        public OperationRepositoryAsync(ApplicationDbContext context) : base(context)
        {

        }
    }
}
