using ERP.Domain.Entities.Commons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Commons
{
    public interface IOperationStateService
    {
        Task<OperationState> GetByIdAsync(int id);
        Task<IReadOnlyList<OperationState>> GetListAsync();
    }
}
