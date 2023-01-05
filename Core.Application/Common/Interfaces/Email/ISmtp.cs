using System.Net.Mail;

namespace Core.Application.Common.Interfaces.Email;

public interface ISmtp
{
    Task SendEmailAsync(MailAddress to, string subject, string message);
}