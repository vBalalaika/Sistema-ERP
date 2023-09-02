using ERP.Application.DTOs.Entities.Purchases.Providers;
using ERP.Domain.Entities.Purchases.Providers;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Purchases.Providers
{
    public interface IProviderService : IGenericService<Provider, ProviderDTO>
    {
        Task MapContacts(ProviderDTO providerDTO);
        Task MapSubsidiaries(ProviderDTO providerDTO);
        Task MapFinancialInfo(ProviderDTO providerDTO);
    }
}
