using ERP.Application.Interfaces.Repositories.ProductMod;
using ERP.Domain.Entities.ProductMod;
using ERP.Infrastructure.DbContexts;

namespace ERP.Infrastructure.Repositories.ProductMod
{
    public class StationRepositoryAsync : GenericRepository<Station>, IStationRepository
    {
        public StationRepositoryAsync(ApplicationDbContext context) : base(context)
        {

        }
    }
}
