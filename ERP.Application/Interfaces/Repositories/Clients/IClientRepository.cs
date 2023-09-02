using ERP.Domain.Entities.Clients;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Repositories.Clients
{
    public interface IClientRepository : IGenericRepository<Client>
    {
        Task<Client> GetByIdLazyLoadAsync(int id);
    }
}
