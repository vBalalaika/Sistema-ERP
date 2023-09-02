using ERP.Application.Interfaces.Repositories.ProductMod;
using ERP.Domain.Entities.ProductMod;
using ERP.Infrastructure.DbContexts;

namespace ERP.Infrastructure.Repositories.ProductMod
{
    public class ProductFeatureRepositoryAsync : GenericRepository<ProductFeature>, IProductFeatureRepository
    {
        public ProductFeatureRepositoryAsync(ApplicationDbContext context) : base(context)
        {

        }
    }
}
