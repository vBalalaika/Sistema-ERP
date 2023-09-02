using ERP.Domain.Entities.Commons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Commons
{
    public interface IIVAConditionService
    {
        Task<IVACondition> GetByIdAsync(int id);
        Task<IReadOnlyList<IVACondition>> GetListAsync();
    }
}
