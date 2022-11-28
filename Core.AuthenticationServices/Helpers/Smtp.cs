using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using Core.Shared.Settings;

namespace EmailSender
{
    public partial class Smtp
    {
        
        public static async Task SendEmailAsync(MailAddress to, string subject, string message, MailSettings mailSettings)
        {
            var from = new MailAddress(mailSettings.Mail,MailSettings.DisplayName);
            var mailMessage = new MailMessage(from, to);
            mailMessage.Subject = subject;
            mailMessage.Body = message;
            mailMessage.IsBodyHtml = true;
            mailMessage.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(message, null, MediaTypeNames.Text.Html));
            using var smtpClient = new SmtpClient(MailSettings.Host, MailSettings.Port);
            smtpClient.Credentials = new System.Net.NetworkCredential(mailSettings.Mail, mailSettings.Password);
            smtpClient.EnableSsl = true;

            await smtpClient.SendMailAsync(mailMessage);

        }
    }
}