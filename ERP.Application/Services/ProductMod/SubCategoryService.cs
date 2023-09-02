using AutoMapper;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Application.Exceptions;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services.ProductMod;
using ERP.Application.Specification.ProductMod.SubCategorySpecifications;
using ERP.Domain.Entities.ProductMod;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Services.ProductMod
{
    public class SubCategoryService : GenericService<SubCategory, SubCategoryDTO>, ISubCategoryService
    {
        public SubCategoryService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }

        public override async Task<int> DeleteAsync(int id)
        {
            var response = await _unitOfWork.ProductRepository.FindAsync(new SubCategoryDeleteValidatorSpecification(id));
            IReadOnlyList<ProductDTO> productsBySubCategoryDto = _mapper.Map<IReadOnlyList<ProductDTO>>(response);
            if (productsBySubCategoryDto.Count == 0)
            {
                return await base.DeleteAsync(id);
            }

            throw new ElementNotFoundException("No se puede borrar la sub-categoría, está en uso");
        }
    }
}
