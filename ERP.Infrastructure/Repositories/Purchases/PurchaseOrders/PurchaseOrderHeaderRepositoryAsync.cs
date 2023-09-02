using ERP.Application.Interfaces.Repositories.Purchases.PurchaseOrders;
using ERP.Domain.Entities.Purchases.PurchaseOrders;
using ERP.Infrastructure.DbContexts;

namespace ERP.Infrastructure.Repositories.Purchases.PurchaseOrders
{
    public class PurchaseOrderHeaderRepositoryAsync : GenericRepository<PurchaseOrderHeader>, IPurchaseOrderHeaderRepository
    {
        public PurchaseOrderHeaderRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
