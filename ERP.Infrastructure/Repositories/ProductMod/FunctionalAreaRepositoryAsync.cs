using ERP.Application.Interfaces.Repositories.ProductMod;
using ERP.Domain.Entities.ProductMod;
using ERP.Infrastructure.DbContexts;

namespace ERP.Infrastructure.Repositories.ProductMod
{
    public class FunctionalAreaRepositoryAsync : GenericRepository<FunctionalArea>, IFunctionalAreaRepository
    {
        public FunctionalAreaRepositoryAsync(ApplicationDbContext context) : base(context)
        {

        }

    }
}