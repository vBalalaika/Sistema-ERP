using ERP.Domain.Entities.Clients;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Repositories.Clients
{
    public interface IReminderRepository : IGenericRepository<Reminder>
    {
        Task<IReadOnlyList<Reminder>> GetByClientIdAsync(int id);
        Task<IReadOnlyList<Reminder>> GetCurrentDateReminders();
    }
}
