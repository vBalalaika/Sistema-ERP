using ERP.Application.Interfaces.Repositories.Purchases.PurchaseOrders;
using ERP.Domain.Entities.Purchases.PurchaseOrders;
using ERP.Infrastructure.DbContexts;

namespace ERP.Infrastructure.Repositories.Purchases.PurchaseOrders
{
    public class ServicePOHeaderRepositoryAsync : GenericRepository<ServicePOHeader>, IServicePOHeaderRepository
    {
        public ServicePOHeaderRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
