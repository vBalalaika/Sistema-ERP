using AspNetCoreHero.Results;
using ERP.Application.DTOs.Entities.Clients;
using ERP.Application.Specification;
using ERP.Domain.Entities.Clients;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.ViewManagers.Clients
{
    public interface IConsultedProductViewManager : IViewManager<ConsultedProduct, ConsultedProductDTO>
    {
        Task<Result<IReadOnlyList<ConsultedProductDTO>>> FindBySpecification(ISpecification<ConsultedProduct> specification);
    }
}
