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
    public class IncomeHeaderViewManager : ViewManager<IncomeHeader, IncomeHeaderDTO>, IIncomeHeaderViewManager
    {
        private readonly IIncomeHeaderService _incomeHeaderService;
        private readonly IMapper _mapper;

        public IncomeHeaderViewManager(IIncomeHeaderService incomeHeaderService, IMapper mapper) : base(incomeHeaderService, mapper)
        {
            _incomeHeaderService = incomeHeaderService;
            _mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<IncomeHeaderDTO>>> FindBySpecification(ISpecification<IncomeHeader> specification)
        {
            var incomes = await _incomeHeaderService.FindWithSpecificationPattern(specification);
            IReadOnlyList<IncomeHeaderDTO> incomeDtos = _mapper.Map<IReadOnlyList<IncomeHeaderDTO>>(incomes);
            return Result<IReadOnlyList<IncomeHeaderDTO>>.Success(incomeDtos);
        }
    }
}
