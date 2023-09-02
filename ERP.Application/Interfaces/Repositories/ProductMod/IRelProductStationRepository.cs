using ERP.Domain.Entities.ProductMod;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Repositories.ProductMod
{
    public interface IRelProductStationRepository : IGenericRepository<RelProductStation>
    {
        Task<IList<RelProductStation>> GetAll();
    }
}
