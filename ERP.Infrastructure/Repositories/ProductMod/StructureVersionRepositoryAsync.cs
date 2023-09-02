using ERP.Application.Interfaces.Repositories.ProductMod;
using ERP.Domain.Entities.ProductMod;
using ERP.Infrastructure.DbContexts;

namespace ERP.Infrastructure.Repositories.ProductMod
{
    public class StructureVersionRepositoryAsync : GenericRepository<StructureVersion>, IStructureVersionRepository
    {
        public StructureVersionRepositoryAsync(ApplicationDbContext context) : base(context)
        {

        }
    }
}
