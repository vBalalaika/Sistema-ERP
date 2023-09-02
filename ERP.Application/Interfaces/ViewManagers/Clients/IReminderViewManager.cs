using AspNetCoreHero.Results;
using ERP.Application.DTOs.Entities.Clients;
using ERP.Application.Specification;
using ERP.Domain.Entities.Clients;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.ViewManagers.Clients
{
    public interface IReminderViewManager : IViewManager<Reminder, ReminderDTO>
    {
        Task<IReadOnlyList<Reminder>> GetByClientId(int id);
        Task<Result<IReadOnlyList<ReminderDTO>>> FindBySpecification(ISpecification<Reminder> specification);
    }
}
