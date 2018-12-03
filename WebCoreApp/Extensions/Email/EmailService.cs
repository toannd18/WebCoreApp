using MimeKit;
using MimeKit.Text;
using NETCore.MailKit;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCoreApp.Extensions.Email
{
    public class EmailService : IEmailService
    {
        private readonly IMailKitProvider mailKitProvider;

        public EmailService(IMailKitProvider mailKitProvider)
        {
            this.mailKitProvider = mailKitProvider;
        }

        public void SendEmail(string subject, string message, string mailTo, string mailCc, bool isHtml)
        {
            var _to = new string[0];
            var _cc = new string[0];
            if (!string.IsNullOrEmpty(mailTo))
            {
                _to = mailTo.Split(",").Select(x => x.Trim()).ToArray();
            }
            if (!string.IsNullOrEmpty(mailCc))
            {
                _cc = mailCc.Split(",").Select(x => x.Trim()).ToArray();
            }

            var mimeMessage = new MimeMessage();

            mimeMessage.From.Add(new MailboxAddress(mailKitProvider.Options.SenderName, mailKitProvider.Options.SenderEmail));
            foreach (string to in _to)
            {
                mimeMessage.To.Add(MailboxAddress.Parse(to));
            }
            foreach (var cc in _cc)
            {
                mimeMessage.Cc.Add(MailboxAddress.Parse(cc));
            }
            mimeMessage.Subject = subject;
            TextPart body;
            if (isHtml)
            {
                body = new TextPart(TextFormat.Html);
            }
            else
            {
                body = new TextPart(TextFormat.Text);
            }
            body.SetText(Encoding.UTF8, message);

            mimeMessage.Body = body;

            using (var client = mailKitProvider.SmtpClient)
            {
                client.Send(mimeMessage);
            }
        }

        public Task SendAsync(string subject, string message, string mailTo, string mailCc = null, bool isHtml = true)
        {
            return Task.Factory.StartNew(() =>
            {
                SendEmail(subject, message, mailTo, mailCc, isHtml);
            });
        }
    }
}