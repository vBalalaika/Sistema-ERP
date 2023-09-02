using AspNetCoreHero.Results;
using AutoMapper;
using ERP.Application.DTOs.Entities.Commons;
using ERP.Application.Interfaces.Services.Commons;
using ERP.Application.Interfaces.ViewManagers.Commons;
using ERP.Application.Specification;
using ERP.Domain.Entities.Commons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Managers.Commons
{
    public class UnitMeasureViewManager : ViewManager<UnitMeasure, UnitMeasureDTO>, IUnitMeasureViewManager
    {
        private readonly IUnitMeasureService _unitmeasureservice;
        private readonly IMapper _mapper;
        public UnitMeasureViewManager(IUnitMeasureService unitmeasureservice, IMapper mapper) : base(unitmeasureservice, mapper)
        {
            _unitmeasureservice = unitmeasureservice;
            _mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<UnitMeasureDTO>>> FindUnitMeasureIsActiveSpecification(ISpecification<UnitMeasure> specification)
        {
            var unitMeasures = await _unitmeasureservice.FindWithSpecificationPattern(specification);
            IReadOnlyList<UnitMeasureDTO> unitMeasuresDto = _mapper.Map<IReadOnlyList<UnitMeasureDTO>>(unitMeasures);
            return Result<IReadOnlyList<UnitMeasureDTO>>.Success(unitMeasuresDto);
        }

        public async Task<Result<IReadOnlyList<UnitMeasureDTO>>> FindBySpecification(ISpecification<UnitMeasure> specification)
        {
            var unitMeasures = await _unitmeasureservice.FindWithSpecificationPattern(specification);
            IReadOnlyList<UnitMeasureDTO> unitMeasuresDto = _mapper.Map<IReadOnlyList<UnitMeasureDTO>>(unitMeasures);
            return Result<IReadOnlyList<UnitMeasureDTO>>.Success(unitMeasuresDto);
        }
    }
}
