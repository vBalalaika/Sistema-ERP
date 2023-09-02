using AspNetCoreHero.Results;
using ERP.Application.DTOs.Entities.Logistics.Incomes;
using ERP.Application.Specification;
using ERP.Domain.Entities.Logistics.Incomes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.ViewManagers.Logistics.Incomes
{
    public interface IIncomeStateViewManager : IViewManager<IncomeState, IncomeStateDTO>
    {
        Task<Result<IReadOnlyList<IncomeStateDTO>>> FindBySpecification(ISpecification<IncomeState> specification);
    }
}
