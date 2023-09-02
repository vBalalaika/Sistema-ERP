using ERP.Application.Interfaces.Repositories.Clients;
using ERP.Domain.Entities.Clients;
using ERP.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Repositories.Clients
{
    public class ReminderRepositoryAsync : GenericRepository<Reminder>, IReminderRepository
    {

        public ReminderRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<IReadOnlyList<Reminder>> GetByClientIdAsync(int id)
        {
            var reminderRepo = await _dbContext
                .Set<Reminder>()
                .Where(r => r.ClientId == id)
                .ToListAsync();
            return reminderRepo;
        }

        public async Task<IReadOnlyList<Reminder>> GetCurrentDateReminders()
        {
            var reminders = await _dbContext
                .Set<Reminder>()
                .Include(r => r.Client)
                .Where(r => r.ReminderCheck == true)
                .Where(r => r.ReminderDate.Value.Date == DateTime.Now.Date)
                .ToListAsync();
            return reminders;
        }
    }
}
