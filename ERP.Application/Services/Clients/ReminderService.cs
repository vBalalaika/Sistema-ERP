using AutoMapper;
using ERP.Application.DTOs.Entities.Clients;
using ERP.Application.DTOs.Mail;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services.Clients;
using ERP.Application.Interfaces.Shared;
using ERP.Domain.Entities.Clients;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Services.Clients
{
    public class ReminderService : GenericService<Reminder, ReminderDTO>, IReminderService
    {
        private readonly IMailService _mailService;

        public ReminderService(IUnitOfWork unitOfWOrk, IMapper mapper, IMailService mailService) : base(unitOfWOrk, mapper)
        {
            _mailService = mailService;
        }

        public Task<IReadOnlyList<Reminder>> GetByClientId(int id)
        {
            var remindersByClientId = _unitOfWork.ReminderRepository.GetByClientIdAsync(id);
            return remindersByClientId;
        }

        public async Task SendEmailReminderAsync()
        {
            // Obtengo los recordatorios de la fecha actual (se ejecuta un job hangfire a las 4 am todos los días)
            var reminders = await _unitOfWork.ReminderRepository.GetCurrentDateReminders();

            if (reminders.Count > 0)
            {
                string mailBody = "";
                foreach (Reminder reminder in reminders)
                {
                    mailBody += $"<h2 style='color: #007bff;'> Razón social: {reminder.Client.BusinessName} </h2>"
                        + "<ul>"
                        + $"<li> Número de teléfono: {reminder.Client.CellPhoneNumber} </li>"
                        + $"<li> Email: {reminder.Client.Email} </li>"
                        + $"<li> Fecha de contacto: {reminder.ContactDate} </li>"
                        + $"<li style='color: #dc3545;'> Fecha de alerta: {reminder.ReminderDate} </li>"
                        + $"<li> Comentarios: {reminder.Comment} </li>"
                        + "</ul>";
                }

                await _mailService.SendWithTemplateAsync(new MailRequest()
                {
                    // Mail de Diego @simplemak
                    To = "ventas@simplemak.com.ar",
                    Body = mailBody,
                    Subject = "ERP Simplemak"
                });
            }
        }

    }
}
