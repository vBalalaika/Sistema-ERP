using AspNetCoreHero.Results;
using ERP.Application.DTOs.Entities.Commons;
using ERP.Application.Specification;
using ERP.Domain.Entities.Commons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.ViewManagers.Commons
{
    public interface IUnitMeasureViewManager : IViewManager<UnitMeasure, UnitMeasureDTO>
    {
        Task<Result<IReadOnlyList<UnitMeasureDTO>>> FindUnitMeasureIsActiveSpecification(ISpecification<UnitMeasure> specification);
        Task<Result<IReadOnlyList<UnitMeasureDTO>>> FindBySpecification(ISpecification<UnitMeasure> specification);
    }
}
