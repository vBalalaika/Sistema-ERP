using ERP.Application.Interfaces.Repositories.Sales;
using ERP.Domain.Entities.Sales;
using ERP.Infrastructure.DbContexts;

namespace ERP.Infrastructure.Repositories.Sales
{
    public class OrderStateRepositoryAsync : GenericRepository<OrderState>, IOrderStateRepository
    {
        public OrderStateRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
