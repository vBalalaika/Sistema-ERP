using ERP.Application.Interfaces.Repositories.ProductMod;
using ERP.Domain.Entities.ProductMod;
using ERP.Infrastructure.DbContexts;

namespace ERP.Infrastructure.Repositories.ProductMod
{
    public class ProductSupplyVoltageRepositoryAsync : GenericRepository<ProductSupplyVoltage>, IProductSupplyVoltageRepository
    {
        public ProductSupplyVoltageRepositoryAsync(ApplicationDbContext context) : base(context)
        {

        }
    }
}
