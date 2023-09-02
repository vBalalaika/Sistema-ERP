using AspNetCoreHero.Results;
using ERP.Application.DTOs.Entities.Purchases.Providers;
using ERP.Application.Specification;
using ERP.Domain.Entities.Purchases.Providers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.ViewManagers.Purchases.Providers
{
    public interface IRelProviderProductViewManager : IViewManager<RelProviderProduct, RelProviderProductDTO>
    {
        Task<Result<IReadOnlyList<RelProviderProductDTO>>> FindBySpecification(ISpecification<RelProviderProduct> specification);
    }
}
