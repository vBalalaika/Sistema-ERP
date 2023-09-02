using AutoMapper;
using ERP.Application.DTOs.Entities.Lists;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services.Lists;
using ERP.Domain.Entities.Lists;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Services.Lists
{
    public class StateService : GenericService<State, StateDTO>, IStateService
    {
        public StateService(IUnitOfWork unitOfWOrk, IMapper mapper) : base(unitOfWOrk, mapper)
        {

        }

        public Task<IReadOnlyList<State>> GetStatesByCountryIdAsync(int countryId)
        {
            var statesByCountryId = _unitOfWork.StateRepository.GetStatesByCountryIdAsync(countryId);
            return statesByCountryId;
        }
    }
}
