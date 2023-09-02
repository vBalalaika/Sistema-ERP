using AspNetCoreHero.Results;
using ERP.Application.DTOs.Entities.Clients;
using ERP.Application.Specification;
using ERP.Domain.Entities.Clients;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.ViewManagers.Clients
{
    public interface ISaleOperationViewManager : IViewManager<SaleOperation, SaleOperationDTO>
    {
        Task<Result<IReadOnlyList<SaleOperationDTO>>> FindBySpecification(ISpecification<SaleOperation> specification);

        Task<Result<SaleOperationDTO>> GetByIdLazyLoad(int id);
    }
}
