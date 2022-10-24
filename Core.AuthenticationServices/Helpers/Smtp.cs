using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
namespace EmailSender
{
    public partial class Smtp
    {
        private const string _fromMail = "mohammedahmedbadee@gmail.com";
        public static async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var mailMessage = new MailMessage(_fromMail, toEmail);
            mailMessage.Subject = subject;
            mailMessage.Body = message;
            mailMessage.IsBodyHtml = true;
            mailMessage.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(message, null, MediaTypeNames.Text.Html));
            using var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new System.Net.NetworkCredential(_fromMail, "xefqqrjoodfqowzy");
            smtpClient.EnableSsl = true;

            await smtpClient.SendMailAsync(mailMessage);

        }
    }
}