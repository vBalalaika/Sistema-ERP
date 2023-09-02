using ERP.Application.Interfaces.Repositories.Sales;
using ERP.Domain.Entities.Sales;
using ERP.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Repositories.Sales
{
    public class OrderDetailRepositoryAsync : GenericRepository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
        public async Task<IReadOnlyList<OrderDetail>> GetAll()
        {
            return await _dbContext
              .Set<OrderDetail>()
              .AsNoTracking()
              .ToListAsync();
        }
    }
}
