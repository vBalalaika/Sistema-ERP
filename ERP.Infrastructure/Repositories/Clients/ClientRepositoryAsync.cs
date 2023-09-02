using ERP.Application.Interfaces.Repositories.Clients;
using ERP.Domain.Entities.Clients;
using ERP.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Repositories.Clients
{
    public class ClientRepositoryAsync : GenericRepository<Client>, IClientRepository
    {
        public ClientRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public new async Task<IReadOnlyList<Client>> GetListAsync()
        {
            return await _dbContext
                 .Set<Client>()
                 .Include(c => c.Country)
                 .Include(os => os.OperationState)
                 .Include(dt => dt.DocumentType)
                 .Include(s => s.State)
                 .Include(c => c.City)
                 .ToListAsync();
        }

        public async Task<Client> GetByIdLazyLoadAsync(int id)
        {
            var clientById = await _dbContext
                .Set<Client>()
                .Include(c => c.Country)
                .Where(c => c.Id == id)
                .FirstAsync();
            return clientById;
        }

    }
}
