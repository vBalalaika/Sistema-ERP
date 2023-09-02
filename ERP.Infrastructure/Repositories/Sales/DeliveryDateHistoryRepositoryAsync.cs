using ERP.Application.Interfaces.Repositories.Sales;
using ERP.Domain.Entities.Sales;
using ERP.Infrastructure.DbContexts;

namespace ERP.Infrastructure.Repositories.Sales
{
    public class DeliveryDateHistoryRepositoryAsync : GenericRepository<DeliveryDateHistory>, IDeliveryDateHistoryRepository
    {
        public DeliveryDateHistoryRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
