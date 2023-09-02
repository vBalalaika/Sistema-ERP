using ERP.Application.Interfaces.Repositories.Clients;
using ERP.Domain.Entities.Clients;
using ERP.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Repositories.Clients
{
    public class SaleOperationRepositoryAsync : GenericRepository<SaleOperation>, ISaleOperationRepository
    {
        public SaleOperationRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<SaleOperation> GetByIdLazyLoadAsync(int id)
        {
            var saleOperationById = await _dbContext
                .Set<SaleOperation>()
                .FirstAsync();
            return saleOperationById;
        }
    }
}
