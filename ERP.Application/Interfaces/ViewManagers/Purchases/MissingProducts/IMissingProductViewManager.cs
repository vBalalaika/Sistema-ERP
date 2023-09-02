using AspNetCoreHero.Results;
using ERP.Application.DTOs.Entities.Purchases.MissingProducts;
using ERP.Application.Specification;
using ERP.Domain.Entities.Purchases.MissingProducts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.ViewManagers.Purchases.MissingProducts
{
    public interface IMissingProductViewManager : IViewManager<MissingProduct, MissingProductDTO>
    {
        Task<Result<IReadOnlyList<MissingProductDTO>>> FindWithAllSpecification(ISpecification<MissingProduct> specification);
    }
}
