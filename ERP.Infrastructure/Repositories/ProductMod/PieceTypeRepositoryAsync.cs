using ERP.Application.Interfaces.Repositories.ProductMod;
using ERP.Domain.Entities.ProductMod;
using ERP.Infrastructure.DbContexts;

namespace ERP.Infrastructure.Repositories.ProductMod
{
    public class PieceTypeRepositoryAsync : GenericRepository<PieceType>, IPieceTypeRepository
    {
        public PieceTypeRepositoryAsync(ApplicationDbContext context) : base(context)
        {

        }
    }
}
