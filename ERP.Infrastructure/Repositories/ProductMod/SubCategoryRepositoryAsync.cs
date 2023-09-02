using ERP.Application.Interfaces.Repositories.ProductMod;
using ERP.Domain.Entities.ProductMod;
using ERP.Infrastructure.DbContexts;

namespace ERP.Infrastructure.Repositories.ProductMod
{
    public class SubCategoryRepositoryAsync : GenericRepository<SubCategory>, ISubCategoryRepository
    {
        public SubCategoryRepositoryAsync(ApplicationDbContext context) : base(context)
        {

        }

    }
}
