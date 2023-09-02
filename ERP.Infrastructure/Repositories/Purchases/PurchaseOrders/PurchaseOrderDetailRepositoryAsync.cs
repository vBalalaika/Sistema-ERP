using ERP.Application.Interfaces.Repositories.Purchases.PurchaseOrders;
using ERP.Domain.Entities.Purchases.PurchaseOrders;
using ERP.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Repositories.Purchases.PurchaseOrders
{
    public class PurchaseOrderDetailRepositoryAsync : GenericRepository<PurchaseOrderDetail>, IPurchaseOrderDetailRepository
    {
        public PurchaseOrderDetailRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
        public async Task<IReadOnlyList<PurchaseOrderDetail>> GetAll()
        {
            return await _dbContext
              .Set<PurchaseOrderDetail>()
              .AsNoTracking()
              .ToListAsync();
        }
    }
}
