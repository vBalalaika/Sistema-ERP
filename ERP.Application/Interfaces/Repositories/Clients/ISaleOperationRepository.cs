using ERP.Domain.Entities.Clients;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Repositories.Clients
{
    public interface ISaleOperationRepository : IGenericRepository<SaleOperation>
    {
        Task<SaleOperation> GetByIdLazyLoadAsync(int id);

    }
}
