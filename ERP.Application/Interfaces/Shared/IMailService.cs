using ERP.Application.DTOs.Mail;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Shared
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request);
        Task SendWithTemplateAsync(MailRequest request);
    }
}