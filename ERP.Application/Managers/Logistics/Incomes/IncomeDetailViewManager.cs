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
    public class IncomeDetailViewManager : ViewManager<IncomeDetail, IncomeDetailDTO>, IIncomeDetailViewManager
    {
        private readonly IIncomeDetailService _incomeDetailService;
        private readonly IMapper _mapper;

        public IncomeDetailViewManager(IIncomeDetailService incomeDetailService, IMapper mapper) : base(incomeDetailService, mapper)
        {
            _incomeDetailService = incomeDetailService;
            _mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<IncomeDetailDTO>>> FindBySpecification(ISpecification<IncomeDetail> specification)
        {
            var incomes = await _incomeDetailService.FindWithSpecificationPattern(specification);
            IReadOnlyList<IncomeDetailDTO> incomeDetailDTOs = _mapper.Map<IReadOnlyList<IncomeDetailDTO>>(incomes);
            return Result<IReadOnlyList<IncomeDetailDTO>>.Success(incomeDetailDTOs);
        }
    }
}
