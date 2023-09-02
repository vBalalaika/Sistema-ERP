using ERP.Application.Interfaces.Repositories.ProductMod;
using ERP.Domain.Entities.ProductMod;
using ERP.Infrastructure.DbContexts;

namespace ERP.Infrastructure.Repositories.ProductMod
{
    public class ProductPieceFormatRepositoryAsync : GenericRepository<ProductPieceFormat>, IProductPieceFormatRepository
    {
        public ProductPieceFormatRepositoryAsync(ApplicationDbContext context) : base(context)
        {

        }
    }
}
