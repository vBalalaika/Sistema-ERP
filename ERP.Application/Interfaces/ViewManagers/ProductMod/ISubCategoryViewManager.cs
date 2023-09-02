using AspNetCoreHero.Results;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Application.Specification;
using ERP.Domain.Entities.ProductMod;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.ViewManagers.ProductMod
{
    public interface ISubCategoryViewManager : IViewManager<SubCategory, SubCategoryDTO>
    {
        Task<Result<IReadOnlyList<SubCategoryDTO>>> FindBySpecification(ISpecification<SubCategory> specification);
        Task<Result<IReadOnlyList<CategoryDTO>>> GetAllCategories();
        Task<Result<IReadOnlyList<SubCategoryDTO>>> LoadAllWithCategory();
    }
}
