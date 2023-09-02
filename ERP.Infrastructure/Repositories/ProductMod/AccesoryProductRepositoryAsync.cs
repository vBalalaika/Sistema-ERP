using ERP.Domain.Entities.ProductMod;
using ERP.Application.Interfaces.Repositories.ProductMod;
using ERP.Infrastructure.DbContexts;

namespace ERP.Infrastructure.Repositories.ProductMod
{
    public class AccesoryProductRepositoryAsync : GenericRepository<AccessoryProduct>, IAccesoryProductRepository
    {
        public AccesoryProductRepositoryAsync(ApplicationDbContext context) : base(context)
        {

        }
    }
}
