using ERP.Application.DTOs.Entities.Purchases.Providers;
using ERP.Domain.Entities.Purchases.Providers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.ViewManagers.Purchases.Providers
{
    public interface IFinancialInformationViewManager : IViewManager<FinancialInformation, FinancialInformationDTO>
    {
        Task<IReadOnlyList<FinancialInformation>> GetFinancialInfosByProviderId(int id);
    }
}
