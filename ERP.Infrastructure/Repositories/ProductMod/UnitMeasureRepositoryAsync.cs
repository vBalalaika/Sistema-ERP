using ERP.Application.Interfaces.Repositories.Commons;
using ERP.Domain.Entities.Commons;
using ERP.Infrastructure.DbContexts;

namespace ERP.Infrastructure.Repositories.ProductMod
{
    public class UnitMeasureRepositoryAsync : GenericRepository<UnitMeasure>, IUnitMeasureRepository
    {
        public UnitMeasureRepositoryAsync(ApplicationDbContext context) : base(context)
        {

        }
    }
}
