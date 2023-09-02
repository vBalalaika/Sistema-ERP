using ERP.Application.Interfaces.Repositories.ProductMod;
using ERP.Domain.Entities.ProductMod;
using ERP.Infrastructure.DbContexts;

namespace ERP.Infrastructure.Repositories.ProductMod
{
    public class CategoryRepositoryAsync : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepositoryAsync(ApplicationDbContext context) : base(context)
        {

        }

    }
}
