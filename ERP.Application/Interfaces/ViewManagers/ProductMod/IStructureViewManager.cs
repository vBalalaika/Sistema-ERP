using AspNetCoreHero.Results;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Application.Specification;
using ERP.Domain.Entities.ProductMod;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.ViewManagers.ProductMod
{
    public interface IStructureViewManager : IViewManager<Structure, StructureDTO>
    {
        Task<Result<IReadOnlyList<StructureDTO>>> FindBySpecification(ISpecification<Structure> specification);
        Task<Result<IReadOnlyList<ProductDTO>>> GetProducts();
    }
}
