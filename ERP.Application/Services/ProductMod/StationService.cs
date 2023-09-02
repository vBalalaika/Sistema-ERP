using AutoMapper;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Application.Exceptions;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services.ProductMod;
using ERP.Application.Specification.ProductMod.StationSpecifications;
using ERP.Domain.Entities.ProductMod;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Services.ProductMod
{
    public class StationService : GenericService<Station, StationDTO>, IStationService
    {
        public StationService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }
        public override async Task<int> DeleteAsync(int id)
        {
            var response = await base.FindWithSpecificationPattern(new StationDeleteValidatorSpecification(id));
            IReadOnlyList<StationDTO> stationsDto = _mapper.Map<IReadOnlyList<StationDTO>>(response);
            if (stationsDto.Count == 0)
            {
                return await base.DeleteAsync(id);
            }

            throw new ElementNotFoundException("No se puede borrar el puesto de trabajo, está en uso");
        }
    }
}
