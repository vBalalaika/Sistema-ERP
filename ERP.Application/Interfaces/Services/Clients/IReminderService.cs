using ERP.Application.DTOs.Entities.Clients;
using ERP.Domain.Entities.Clients;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Clients
{
    public interface IReminderService : IGenericService<Reminder, ReminderDTO>
    {
        Task<IReadOnlyList<Reminder>> GetByClientId(int id);
        Task SendEmailReminderAsync();
    }
}
