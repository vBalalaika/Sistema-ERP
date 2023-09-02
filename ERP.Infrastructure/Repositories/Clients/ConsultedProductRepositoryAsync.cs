using ERP.Application.Interfaces.Repositories.Clients;
using ERP.Domain.Entities.Clients;
using ERP.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Repositories.Clients
{
    public class ConsultedProductRepositoryAsync : GenericRepository<ConsultedProduct>, IConsultedProductRepository
    {
        public ConsultedProductRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<IReadOnlyList<ConsultedProduct>> GetAll()
        {
            return await _dbContext
              .Set<ConsultedProduct>()
              .AsNoTracking()
              .ToListAsync();
        }
    }
}
