using ERP.Application.DTOs.Entities.Purchases.Providers;
using ERP.Domain.Entities.Purchases.Providers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Purchases.Providers
{
    public interface IFinancialInformationService : IGenericService<FinancialInformation, FinancialInformationDTO>
    {
        Task<IReadOnlyList<FinancialInformation>> GetByProviderId(int id);
    }
}
