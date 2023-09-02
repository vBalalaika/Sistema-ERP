using ERP.Application.Interfaces.Repositories.ProductMod;
using ERP.Domain.Entities.ProductMod;
using ERP.Infrastructure.DbContexts;

namespace ERP.Infrastructure.Repositories.ProductMod
{
    public class PieceFormatRepositoryAsync : GenericRepository<PieceFormat>, IPieceFormatRepository
    {
        public PieceFormatRepositoryAsync(ApplicationDbContext context) : base(context)
        {

        }
    }
}
