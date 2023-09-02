using ERP.Application.Interfaces.Repositories.ProductMod;
using ERP.Domain.Entities.ProductMod;
using ERP.Infrastructure.DbContexts;

namespace ERP.Infrastructure.Repositories.ProductMod
{
    public class StructureItemRepositoryAsync : GenericRepository<StructureItem>, IStructureItemRepository
    {
        public StructureItemRepositoryAsync(ApplicationDbContext context) : base(context)
        {

        }
    }
}