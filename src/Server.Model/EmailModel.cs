namespace Server.Model
{
    using System.Collections.Generic;
    using System.Net.Mail;

    public class EmailModel
    {
        //filled in by successful POST request
        public int? EmailId { get; set; }

        public string From { get; set; }

        public string SenderDisplayName { get; set; }

        public string ReplyTo { get; set; }

        public List<string> Recipients { get; set; }

        public List<string> CcRecipients { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public bool IsHtml { get; set; }

        public string ApplicationName { get; set; }

        public bool AlternateView { get; set; }

        public List<Attachment> Attachments { get; set; }
    }
}
