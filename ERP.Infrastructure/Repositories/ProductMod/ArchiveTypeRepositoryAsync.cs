using ERP.Application.Interfaces.Repositories.ProductMod;
using ERP.Domain.Entities.ProductMod;
using ERP.Infrastructure.DbContexts;

namespace ERP.Infrastructure.Repositories.ProductMod
{
    public class ArchiveTypeRepositoryAsync : GenericRepository<ArchiveType>, IArchiveTypeRepository
    {
        public ArchiveTypeRepositoryAsync(ApplicationDbContext context) : base(context)
        {

        }
    }
}
