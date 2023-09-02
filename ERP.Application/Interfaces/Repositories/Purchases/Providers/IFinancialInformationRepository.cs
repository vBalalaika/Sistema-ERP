using ERP.Domain.Entities.Purchases.Providers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Repositories.Purchases.Providers
{
    public interface IFinancialInformationRepository : IGenericRepository<FinancialInformation>
    {
        Task<IReadOnlyList<FinancialInformation>> GetAll();
        Task<IReadOnlyList<FinancialInformation>> GetByProviderIdAsync(int id);
    }
}
