using ERP.Application.Interfaces.Repositories.ProductMod;
using ERP.Domain.Entities.ProductMod;
using ERP.Infrastructure.DbContexts;

namespace ERP.Infrastructure.Repositories.ProductMod
{
    public class ProductCutTypeRepositoryAsync : GenericRepository<ProductCutType>, IProductCutTypeRepository
    {
        public ProductCutTypeRepositoryAsync(ApplicationDbContext context) : base(context)
        {

        }

    }
}
