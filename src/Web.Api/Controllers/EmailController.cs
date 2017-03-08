using Server.Api.Helpers;
using Server.Business.Email.Interfaces;
using Server.Business.Helpers.Interfaces;
using Server.Model;
using Server.Model.Constants;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Web.Api.Controllers
{
    public class EmailController : ApiController
    {
        private readonly IEmailOps _email;
        private readonly IConfigManager _config;

        public EmailController(IEmailOps email, IConfigManager config)
        {
            _email = email;
            _config = config;
        }

        [Route("Email/Contact")]
        [HttpGet]
        public async Task<IHttpActionResult> SendContactEmail(string fromName, string fromEmail, bool ccUser, string message)
        {
            try
            {
                var subject = string.Format("Message from {0} to Shree Venkat", fromName);
                var bodyTitle = string.Format("Message from {0}", fromName);

                var template = await _email.GetTemplate("contact-us-template");
                var body = template.Replace("*|MC:TITLE|*", bodyTitle)
                                   .Replace("*|MC:SUBJECT|*", subject)
                                   .Replace("*|MC:EMAILTOBROWSERLINK|*", "http://www.ShreeVenkat.co.uk")
                                   .Replace("{0}", fromName)
                                   .Replace("{1}", fromEmail)
                                   .Replace("{2}", message.Replace("\r\n", "<br/>").Replace("\n", "<br/>").Replace("\r", "<br/>"))
                                   .Replace("*|CURRENT_YEAR|*", DateTime.Now.Year.ToString())
                                   .Replace("*|LIST:COMPANY|*", "www.ShreeVenkat.co.uk");

                var shree = _config.Get<string>(ConfigKeys.SmtpUserName);

                var messageModel = new EmailModel
                                    {
                                        From = shree,
                                        SenderDisplayName = "Shree Venkat",
                                        Recipients = new List<string> { shree },
                                        CcRecipients = (ccUser && !string.IsNullOrWhiteSpace(fromEmail) ? new List<string> { fromEmail } : null),
                                        Subject = subject,
                                        Body = body,
                                        IsHtml = true
                                    };

                await _email.SendEmail(messageModel);
                return Ok();
            }
            catch (Exception ex)
            {
                return WebApiErrorHandler.Throw(ex);
            }
        }
    }
}
