using AspNetCoreHero.Results;
using AutoMapper;
using ERP.Application.DTOs.Entities.Clients;
using ERP.Application.Interfaces.Services.Clients;
using ERP.Application.Interfaces.ViewManagers.Clients;
using ERP.Application.Specification;
using ERP.Domain.Entities.Clients;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Managers.Clients
{
    public class ReminderViewManager : ViewManager<Reminder, ReminderDTO>, IReminderViewManager
    {
        private readonly IReminderService _reminderservice;
        private readonly IMapper _mapper;

        public ReminderViewManager(IReminderService reminderservice, IMapper mapper) : base(reminderservice, mapper)
        {
            _reminderservice = reminderservice;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<Reminder>> GetByClientId(int id)
        {
            var remindersByClient = await _reminderservice.GetByClientId(id);
            var remindersDtoByClient = _mapper.Map<IReadOnlyList<ReminderDTO>>(remindersByClient);
            return remindersDtoByClient;
        }

        public async Task<Result<IReadOnlyList<ReminderDTO>>> FindBySpecification(ISpecification<Reminder> specification)
        {
            var reminders = await _reminderservice.FindWithSpecificationPattern(specification);
            IReadOnlyList<ReminderDTO> reminderDTOs = _mapper.Map<IReadOnlyList<ReminderDTO>>(reminders);
            return Result<IReadOnlyList<ReminderDTO>>.Success(reminderDTOs);
        }
    }
}
