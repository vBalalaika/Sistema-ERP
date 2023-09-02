using AutoMapper;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Application.Exceptions;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services.ProductMod;
using ERP.Application.Specification.ProductMod.CategorySpecifications;
using ERP.Domain.Entities.ProductMod;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Services.ProductMod
{
    public class CategoryService : GenericService<Category, CategoryDTO>, ICategoryService
    {
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }

        public override async Task<int> DeleteAsync(int id)
        {
            var response = await base.FindWithSpecificationPattern(new CategoryDeleteValidatorSpecification(id));
            IReadOnlyList<CategoryDTO> categoriesDto = _mapper.Map<IReadOnlyList<CategoryDTO>>(response);
            if (categoriesDto.Count == 0)
            {
                return await base.DeleteAsync(id);
            }

            throw new ElementNotFoundException("No se puede borrar la categoría, está en uso");
        }
    }
}
