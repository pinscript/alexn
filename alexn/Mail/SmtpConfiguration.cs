using System.Net;

namespace alexn.Mail {
    public class SmtpConfiguration {
        public string Host { get; set; }
        public int Port { get; set; }
        public ICredentialsByHost Credentials { get; set; }
        public bool EnableSsl { get; set; }
        public int Timeout { get; set; }
    }
}