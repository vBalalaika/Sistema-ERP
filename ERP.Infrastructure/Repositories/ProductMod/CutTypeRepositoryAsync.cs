using ERP.Application.Interfaces.Repositories.ProductMod;
using ERP.Domain.Entities.ProductMod;
using ERP.Infrastructure.DbContexts;

namespace ERP.Infrastructure.Repositories.ProductMod
{
    public class CutTypeRepositoryAsync : GenericRepository<CutType>, ICutTypeRepository
    {
        public CutTypeRepositoryAsync(ApplicationDbContext context) : base(context)
        {

        }
    }
}