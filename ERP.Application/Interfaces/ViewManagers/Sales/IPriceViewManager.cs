using AspNetCoreHero.Results;
using ERP.Application.DTOs.Entities.Sales;
using ERP.Application.Specification;
using ERP.Domain.Entities.Sales;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.ViewManagers.Sales
{
    public interface IPriceViewManager : IViewManager<Price, PriceDTO>
    {
        Task<Result<IReadOnlyList<PriceDTO>>> FindBySpecification(ISpecification<Price> specification);
    }
}
