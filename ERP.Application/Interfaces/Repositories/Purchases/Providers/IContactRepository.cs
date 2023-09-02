using ERP.Domain.Entities.Purchases.Providers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Repositories.Purchases.Providers
{
    public interface IContactRepository : IGenericRepository<Contact>
    {
        Task<IReadOnlyList<Contact>> GetAll();
        Task<IReadOnlyList<Contact>> GetByProviderIdAsync(int id);
    }
}
