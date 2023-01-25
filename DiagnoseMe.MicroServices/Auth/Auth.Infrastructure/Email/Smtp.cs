using System.Net.Mail;
using System.Net.Mime;
using Auth.Application.Common.Interfaces.Email;
using Auth.Application.Settings;
using Microsoft.Extensions.Options;

namespace Auth.Infrastructure.Email;

public class Smtp : ISmtp
{
    
    private readonly MailSettings _mailSittings;
    public Smtp(
        IOptions<MailSettings> mailSettings
    )
    {
        _mailSittings = mailSettings.Value;
    }
    public async Task SendEmailAsync(MailAddress to, string subject, string message)
    {
        var from = new MailAddress(_mailSittings.Mail,_mailSittings.DisplayName);
        var mailMessage = new MailMessage(from, to);
        mailMessage.Subject = subject;
        mailMessage.Body = message;
        mailMessage.IsBodyHtml = true;
        mailMessage.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(message, null, MediaTypeNames.Text.Html));
        using var smtpClient = new SmtpClient(_mailSittings.Host, _mailSittings.Port);
        smtpClient.Credentials = new System.Net.NetworkCredential(_mailSittings.Mail, _mailSittings.Password);
        smtpClient.EnableSsl = true;
        await smtpClient.SendMailAsync(mailMessage);
    }
}