using alexn.Mail;
using NUnit.Framework;

namespace alexn.Test.Mail {
    [TestFixture]
    public class EmailSenderIntegrationTest {
        [Test]
        public void TestSend() {
            using(var sender = new EmailSender("91.201.60.2", 26)) {
                //sender.SendEmail("alexander.nyquist@mediaanalys.se", "alexander.nyquist@mediaanalys.se", "Testar", "Går detta?");
            }
        }

        [Test]
        public void TestSendToMultiple() {
            using (var sender = new EmailSender("91.201.60.2", 26)) {
                //sender.SendEmail("alexander.nyquist@mediaanalys.se", "alexander.nyquist@mediaanalys.se;nyquist.alexander@gmail.com", "Testar", "Går detta?");
            }
        }
    }
}
