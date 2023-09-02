using ERP.Domain.Entities.Clients;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Repositories.Clients
{
    public interface IConsultedProductRepository : IGenericRepository<ConsultedProduct>
    {
        Task<IReadOnlyList<ConsultedProduct>> GetAll();
    }
}
