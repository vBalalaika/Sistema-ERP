using ERP.Application.DTOs.Entities.Clients;
using ERP.Domain.Entities.Clients;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Clients
{
    public interface ISaleOperationService : IGenericService<SaleOperation, SaleOperationDTO>
    {
        Task<SaleOperation> GetByIdLazyLoad(int id);
    }
}
