using ERP.Application.Interfaces.Repositories.Sales;
using ERP.Domain.Entities.Sales;
using ERP.Infrastructure.DbContexts;

namespace ERP.Infrastructure.Repositories.Sales
{
    public class OrderHeaderRepositoryAsync : GenericRepository<OrderHeader>, IOrderHeaderRepository
    {
        public OrderHeaderRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
