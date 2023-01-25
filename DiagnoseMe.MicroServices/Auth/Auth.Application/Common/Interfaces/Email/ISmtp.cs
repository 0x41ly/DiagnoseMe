using System.Net.Mail;

namespace Auth.Application.Common.Interfaces.Email;

public interface ISmtp
{
    Task SendEmailAsync(MailAddress to, string subject, string message);
}