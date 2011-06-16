using System;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace alexn.Mail {
    public class EmailSender : IEmailSender, IDisposable {
        private readonly SmtpClient _smtp;

        public EmailSender(SmtpConfiguration configuration) {
            _smtp = new SmtpClient {
                                       Host = configuration.Host,
                                       Port = configuration.Port,
                                       Credentials = configuration.Credentials,
                                       EnableSsl = configuration.EnableSsl,
                                       Timeout = configuration.Timeout
                                   };
        }

        public EmailSender(string smtpHost) : this(smtpHost, 25) {
            Guard.Against.NullOrEmpty(smtpHost, "smtpHost");
        }

        public EmailSender(string smtpHost, int smtpPort)
            : this(new SmtpConfiguration { Host = smtpHost, Port = smtpPort}) {
        }

        public void SendEmail(string from, string to, string subject, string body) {
            var message = BuildMessage(from, to, subject, body);
            SendEmail(message);
        }

        public void SendEmail(MailMessage message) {
            _smtp.Send(message);
        }

        public void SendEmailAsync(string from, string to, string subject, string body) {
            var message = BuildMessage(from, to, subject, body);
            SendEmailAsync(message);
        }

        public void SendEmailAsync(MailMessage message) {
            var task = new Task(() => SendEmail(message));
            task.Start();   
        }

        private static MailMessage BuildMessage(string from, string to, string subject, string body) {
            var message = new MailMessage {
                From = new MailAddress(from),
                Sender = new MailAddress(from),
                Subject = subject,
                SubjectEncoding = Encoding.UTF8,
                Body = body,
                BodyEncoding = Encoding.UTF8,
                IsBodyHtml = true
            };

            var recievers = to.Split(';');
            foreach (var reciever in recievers) {
                message.To.Add(new MailAddress(reciever.Trim()));
            }

            return message;
        }

        public void Dispose() {
            _smtp.Dispose();
        }
    }
}