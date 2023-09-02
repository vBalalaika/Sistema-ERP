using AspNetCoreHero.Results;
using AutoMapper;
using ERP.Application.DTOs.Entities.Production;
using ERP.Application.Interfaces.Services.Production;
using ERP.Application.Interfaces.ViewManagers.Production;
using ERP.Application.Specification;
using ERP.Domain.Entities.Production;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Managers.Production
{
    public class WorkActivityViewManager : ViewManager<WorkActivity, WorkActivityDTO>, IWorkActivityViewManager
    {
        private readonly IWorkActivityService _WorkActivityService;
        private readonly IMapper _mapper;

        public WorkActivityViewManager(IWorkActivityService WorkActivityService, IMapper mapper) : base(WorkActivityService, mapper)
        {
            _WorkActivityService = WorkActivityService;
            _mapper = mapper;
        }

        public async Task<Result<bool>> UpdateListAsync(IList<WorkActivityDTO> entityListToUpdate)
        {
            try
            {
                bool result = await _WorkActivityService.UpdateListAsync(entityListToUpdate);
                return Result<bool>.Success(result);
            }
            catch (Exception ex)
            {
                return Result<bool>.Fail(ex.Message);
            }

        }
        public async Task<Result<IReadOnlyList<WorkActivityDTO>>> FindBySpecification(ISpecification<WorkActivity> specification)
        {
            var WorkActivities = await _WorkActivityService.FindWithSpecificationPattern(specification);
            IReadOnlyList<WorkActivityDTO> WorkActivitiesDto = _mapper.Map<IReadOnlyList<WorkActivityDTO>>(WorkActivities);
            return Result<IReadOnlyList<WorkActivityDTO>>.Success(WorkActivitiesDto);
        }
    }
}
