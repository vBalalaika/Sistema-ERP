using ERP.Application.DTOs.Entities.Clients;
using ERP.Domain.Entities.Clients;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Clients
{
    public interface IClientService : IGenericService<Client, ClientDTO>
    {
        Task<Client> GetByIdLazyLoad(int id);

        Task<int> DeactivateAsync(int id);

    }
}
