using ERP.Application.Interfaces.Repositories.Purchases.PurchaseOrders;
using ERP.Domain.Entities.Purchases.PurchaseOrders;
using ERP.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Repositories.Purchases.PurchaseOrders
{
    public class ServicePODetailRepositoryAsync : GenericRepository<ServicePODetail>, IServicePODetailRepository
    {
        public ServicePODetailRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<IReadOnlyList<ServicePODetail>> GetAll()
        {
            return await _dbContext
              .Set<ServicePODetail>()
              .AsNoTracking()
              .ToListAsync();
        }
    }
}
