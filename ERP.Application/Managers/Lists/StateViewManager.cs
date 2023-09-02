using AspNetCoreHero.Results;
using AutoMapper;
using ERP.Application.DTOs.Entities.Lists;
using ERP.Application.Interfaces.Services.Commons;
using ERP.Application.Interfaces.Services.Lists;
using ERP.Application.Interfaces.ViewManagers.Lists;
using ERP.Application.Specification;
using ERP.Domain.Entities.Lists;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Managers.Lists
{
    public class StateViewManager : ViewManager<State, StateDTO>, IStateViewManager
    {
        private readonly IStateService _stateservice;
        private readonly IMapper _mapper;

        public StateViewManager(IStateService stateservice, IMapper mapper, ICountryService countryService) : base(stateservice, mapper)
        {
            _stateservice = stateservice;
            _mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<StateDTO>>> FindBySpecification(ISpecification<State> specification)
        {
            var states = await _stateservice.FindWithSpecificationPattern(specification);
            IReadOnlyList<StateDTO> stateDTOs = _mapper.Map<IReadOnlyList<StateDTO>>(states);
            return Result<IReadOnlyList<StateDTO>>.Success(stateDTOs);
        }

        public async Task<IReadOnlyList<State>> GetStatesByCountryIdAsync(int countryId)
        {
            var statesByCountryId = await _stateservice.GetStatesByCountryIdAsync(countryId);
            return statesByCountryId;
        }
    }
}
