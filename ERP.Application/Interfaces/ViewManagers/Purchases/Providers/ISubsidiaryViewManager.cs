using ERP.Application.DTOs.Entities.Purchases.Providers;
using ERP.Domain.Entities.Purchases.Providers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.ViewManagers.Purchases.Providers
{
    public interface ISubsidiaryViewManager : IViewManager<Subsidiary, SubsidiaryDTO>
    {
        Task<IReadOnlyList<Subsidiary>> GetSubsidiariesByProviderId(int id);
    }
}
