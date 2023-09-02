using AspNetCoreHero.Results;
using AutoMapper;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Application.Interfaces.Services.ProductMod;
using ERP.Application.Interfaces.ViewManagers.ProductMod;
using ERP.Application.Specification;
using ERP.Application.Specification.ProductMod.SubCategorySpecifications;
using ERP.Domain.Entities.ProductMod;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Managers.ProductMod
{
    public class SubCategoryViewManager : ViewManager<SubCategory, SubCategoryDTO>, ISubCategoryViewManager
    {
        private readonly ISubCategoryService _subcategoryservice;
        private readonly ICategoryService _categoryservice;
        private readonly IMapper _mapper;

        public SubCategoryViewManager(ISubCategoryService subcategoryservice, IMapper mapper, ICategoryService categoryservice) :
           base(subcategoryservice, mapper)
        {
            _subcategoryservice = subcategoryservice;
            _categoryservice = categoryservice;
            _mapper = mapper;
        }
        public async Task<Result<IReadOnlyList<SubCategoryDTO>>> FindBySpecification(ISpecification<SubCategory> specification)
        {
            var subCategories = await _subcategoryservice.FindWithSpecificationPattern(specification);
            IReadOnlyList<SubCategoryDTO> subcategoriesDTO = _mapper.Map<IReadOnlyList<SubCategoryDTO>>(subCategories);
            return Result<IReadOnlyList<SubCategoryDTO>>.Success(subcategoriesDTO);
        }
        public async Task<Result<IReadOnlyList<CategoryDTO>>> GetAllCategories()
        {
            var categories = await _categoryservice.GetListAsync();
            var categoriesDTO = _mapper.Map<IReadOnlyList<CategoryDTO>>(categories);
            return Result<IReadOnlyList<CategoryDTO>>.Success(categoriesDTO);
        }
        public async Task<Result<IReadOnlyList<SubCategoryDTO>>> LoadAllWithCategory()
        {
            var subcategories = await _subcategoryservice.FindWithSpecificationPattern(new SubCategoryWithCategorySpecification());
            var subCategoriesDTO = _mapper.Map<IReadOnlyList<SubCategoryDTO>>(subcategories);
            return Result<IReadOnlyList<SubCategoryDTO>>.Success(subCategoriesDTO);
        }

    }
}
