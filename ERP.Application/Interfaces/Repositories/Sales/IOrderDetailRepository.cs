using ERP.Domain.Entities.Sales;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Repositories.Sales
{
    public interface IOrderDetailRepository : IGenericRepository<OrderDetail>
    {
        Task<IReadOnlyList<OrderDetail>> GetAll();
    }
}
