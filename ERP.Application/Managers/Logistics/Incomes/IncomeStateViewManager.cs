using AspNetCoreHero.Results;
using AutoMapper;
using ERP.Application.DTOs.Entities.Logistics.Incomes;
using ERP.Application.Interfaces.Services.Logistics.Incomes;
using ERP.Application.Interfaces.ViewManagers.Logistics.Incomes;
using ERP.Application.Specification;
using ERP.Domain.Entities.Logistics.Incomes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Managers.Logistics.Incomes
{
    public class IncomeStateViewManager : ViewManager<IncomeState, IncomeStateDTO>, IIncomeStateViewManager
    {
        private readonly IIncomeStateService _incomeStateService;
        private readonly IMapper _mapper;

        public IncomeStateViewManager(IIncomeStateService incomeStateService, IMapper mapper) : base(incomeStateService, mapper)
        {
            _incomeStateService = incomeStateService;
            _mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<IncomeStateDTO>>> FindBySpecification(ISpecification<IncomeState> specification)
        {
            var incomes = await _incomeStateService.FindWithSpecificationPattern(specification);
            IReadOnlyList<IncomeStateDTO> incomeDtos = _mapper.Map<IReadOnlyList<IncomeStateDTO>>(incomes);
            return Result<IReadOnlyList<IncomeStateDTO>>.Success(incomeDtos);
        }
    }
}
