using ERP.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Repositories
{
    public interface ILogRepository
    {
        Task<List<AuditLogResponse>> GetAuditLogsAsync(string userId);

        Task<List<AuditLogResponse>> GetAllAuditLogsAsync(string tableName, string type, string oldValue, string newValue, string affectedColumn);

        Task AddLogAsync(string action, string userId);
    }
}