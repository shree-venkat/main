using System.Threading.Tasks;
using Server.Business.Email.Interfaces;
using Server.Business.Helpers.Interfaces;
using Server.Model;
using System.Net.Mail;
using System.Linq;
using Server.Model.Constants;

namespace Server.Business.Email
{
    public class EmailOps : IEmailOps
    {
        private readonly IConfigManager _config;
        private readonly IEmbeddedResource _resource;

        public EmailOps(IConfigManager config, IEmbeddedResource resource)
        {
            _config = config;
            _resource = resource;
        }

        public async Task<string> GetTemplate(string templateName)
        {
            var template = await _resource.GetContents("Server.Business.Resources." + templateName + ".txt");
            return template;
        }

        public async Task SendEmail(EmailModel model)
        {
            var host = _config.Get<string>(ConfigKeys.SmtpHost);
            var port = _config.Get<int>(ConfigKeys.SmtpPort);
            var userName = _config.Get<string>(ConfigKeys.SmtpUserName);
            var password = _config.Get<string>(ConfigKeys.SmtpPassword);
            var ssl = _config.Get<bool>(ConfigKeys.SmtpEnableSsl);

            var smtpClient = new SmtpClient(host, port);
            using (var mailMessage = new MailMessage(new MailAddress(model.From, model.SenderDisplayName), new MailAddress(model.Recipients.FirstOrDefault())))
            {
                mailMessage.Subject = model.Subject;
                //var emailBody = EmailTemplate.Replace("{0}", title).Replace("{1}", body);
                AlternateView item = AlternateView.CreateAlternateViewFromString(model.Body, null, "text/html");
                mailMessage.AlternateViews.Add(item);
                mailMessage.IsBodyHtml = true;

                if (model.CcRecipients != null && model.CcRecipients.Any())
                {
                    model.CcRecipients.ForEach(mailMessage.CC.Add);
                }
                if (!string.IsNullOrWhiteSpace(model.ReplyTo))
                {
                    mailMessage.ReplyToList.Add(model.ReplyTo);
                }
                if (model.Attachments != null && model.Attachments.Any())
                {
                    model.Attachments.ForEach(mailMessage.Attachments.Add);
                }

                smtpClient.UseDefaultCredentials = true;
                smtpClient.Credentials = new System.Net.NetworkCredential(userName, password);
                smtpClient.EnableSsl = ssl;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Timeout = 300000;

                await smtpClient.SendMailAsync(mailMessage);
            }
        }
    }
}