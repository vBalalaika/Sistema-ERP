﻿using AspNetCoreHero.Results;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Application.Specification;
using ERP.Domain.Entities.ProductMod;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.ViewManagers.ProductMod
{
    public interface IProductSupplyVoltageViewManager : IViewManager<ProductSupplyVoltage, ProductSupplyVoltageDTO>
    {
        Task<Result<IReadOnlyList<ProductSupplyVoltageDTO>>> FindBySpecification(ISpecification<ProductSupplyVoltage> specification);
    }
}
