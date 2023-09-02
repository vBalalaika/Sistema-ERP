using AspNetCoreHero.Results;
using AutoMapper;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Application.Interfaces.Services.ProductMod;
using ERP.Application.Interfaces.ViewManagers.ProductMod;
using ERP.Application.Specification;
using ERP.Domain.Entities.ProductMod;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Managers.ProductMod
{
    public class CategoryViewManager : ViewManager<Category, CategoryDTO>, ICategoryViewManager
    {
        private readonly ICategoryService _categoryservice;
        private readonly IMapper _mapper;

        public CategoryViewManager(ICategoryService categoryservice, IMapper mapper) :
           base(categoryservice, mapper)
        {
            _categoryservice = categoryservice;
            _mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<CategoryDTO>>> FindBySpecification(ISpecification<Category> specification)
        {
            var categories = await _categoryservice.FindWithSpecificationPattern(specification);
            IReadOnlyList<CategoryDTO> categoriesDTO = _mapper.Map<IReadOnlyList<CategoryDTO>>(categories);
            return Result<IReadOnlyList<CategoryDTO>>.Success(categoriesDTO);
        }
    }
}
