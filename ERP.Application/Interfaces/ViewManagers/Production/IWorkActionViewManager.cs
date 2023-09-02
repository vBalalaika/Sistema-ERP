﻿using AspNetCoreHero.Results;
using ERP.Application.DTOs.Entities.Production;
using ERP.Application.Specification;
using ERP.Domain.Entities.Production;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.ViewManagers.Production
{
    public interface IWorkActionViewManager : IViewManager<WorkAction, WorkActionDTO>
    {
        Task<Result<IReadOnlyList<WorkActionDTO>>> FindBySpecification(ISpecification<WorkAction> specification);
    }
}
