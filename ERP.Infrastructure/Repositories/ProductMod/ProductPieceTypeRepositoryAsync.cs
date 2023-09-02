using ERP.Application.Interfaces.Repositories.ProductMod;
using ERP.Domain.Entities.ProductMod;
using ERP.Infrastructure.DbContexts;

namespace ERP.Infrastructure.Repositories.ProductMod
{
    public class ProductPieceTypeRepositoryAsync : GenericRepository<ProductPieceType>, IProductPieceTypeRepository
    {
        public ProductPieceTypeRepositoryAsync(ApplicationDbContext context) : base(context)
        {

        }
    }
}
