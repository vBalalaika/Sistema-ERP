using ERP.Domain.Entities.ProductMod;
using ERP.Application.Interfaces.Repositories.ProductMod;
using ERP.Infrastructure.DbContexts;

namespace ERP.Infrastructure.Repositories.ProductMod
{
    public class SupplyVoltageRepositoryAsync : GenericRepository<SupplyVoltage>, ISupplyVoltageRepository
    {
        public SupplyVoltageRepositoryAsync(ApplicationDbContext context) : base(context)
        {

        }
    }
}
