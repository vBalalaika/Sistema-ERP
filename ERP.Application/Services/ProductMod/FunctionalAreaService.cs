using AutoMapper;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Application.Exceptions;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services.ProductMod;
using ERP.Application.Specification.ProductMod.FunctionalAreaSpecifications;
using ERP.Domain.Entities.ProductMod;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Services.ProductMod
{
    public class FunctionalAreaService : GenericService<FunctionalArea, FunctionalAreaDTO>, IFunctionalAreaService
    {
        public FunctionalAreaService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }
        public override async Task<int> DeleteAsync(int id)
        {
            var response = await base.FindWithSpecificationPattern(new FunctionalAreaDeleteValidatorSpecification(id));
            IReadOnlyList<FunctionalAreaDTO> functionalareasDto = _mapper.Map<IReadOnlyList<FunctionalAreaDTO>>(response);
            if (functionalareasDto.Count == 0)
            {
                return await base.DeleteAsync(id);
            }

            throw new ElementNotFoundException("No se puede borrar el área functional, está en uso");
        }
    }
}
