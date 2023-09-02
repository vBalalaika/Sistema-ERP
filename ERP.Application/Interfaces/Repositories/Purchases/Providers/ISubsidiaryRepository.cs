using ERP.Domain.Entities.Purchases.Providers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Repositories.Purchases.Providers
{
    public interface ISubsidiaryRepository : IGenericRepository<Subsidiary>
    {
        Task<IReadOnlyList<Subsidiary>> GetAll();
        Task<IReadOnlyList<Subsidiary>> GetByProviderIdAsync(int id);
    }
}
