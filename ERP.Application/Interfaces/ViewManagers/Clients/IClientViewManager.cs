using AspNetCoreHero.Results;
using ERP.Application.DTOs.Entities.Clients;
using ERP.Application.Specification;
using ERP.Domain.Entities.Clients;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.ViewManagers.Clients
{
    public interface IClientViewManager : IViewManager<Client, ClientDTO>
    {
        Task<Result<ClientDTO>> GetByIdLazyLoad(int id);

        Task<Result<int>> DeactivateAsync(int id);

        Task<Result<IReadOnlyList<ClientDTO>>> FindBySpecification(ISpecification<Client> specification);
    }
}
