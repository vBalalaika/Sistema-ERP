using ERP.Application.Interfaces.Repositories.Clients;
using ERP.Domain.Entities.Clients;
using ERP.Infrastructure.DbContexts;

namespace ERP.Infrastructure.Repositories.Clients
{
    public class CommunicationRepositoryAsync : GenericRepository<Communication>, ICommunicationRepository
    {
        public CommunicationRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
