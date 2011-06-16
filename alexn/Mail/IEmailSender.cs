using System.Net.Mail;

namespace alexn.Mail {
    public interface IEmailSender {
        void SendEmail(string from, string to, string subject, string body);
        void SendEmail(MailMessage message);
        void SendEmailAsync(string from, string to, string subject, string body);
        void SendEmailAsync(MailMessage message);
    }
}