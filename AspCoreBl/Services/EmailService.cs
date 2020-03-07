using AspCoreBl.Misc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AspCoreBl.Services
{
    public class EmailService
    {
        private EmailSettings _settings;
        public EmailService(IOptions<EmailSettings> settings)
        {
            _settings = settings.Value;
        }
        public EmailService(EmailSettings settings)
        {
            _settings = settings;
        }
        public async Task SendMailAsync(List<MailAddress> toAddrs, List<MailAddress> ccAddrs, List<MailAddress> bccAddrs, string subject, string bodyText, List<string> attachmentFilepaths)
        {
            var mailServerIp = _settings.MailServerIp;
            var systemEmailSenderEmail = _settings.SystemEmailSenderEmail;
            var systemEmailSenderUsername = _settings.SystemEmailSenderUsername;
            var systemEmailSenderPassword = _settings.SystemEmailSenderPassword;
            var systemEmailSenderName = _settings.SystemEmailSenderName;
            var port = _settings.OutPortNo;

            var objMail = new MailMessage();

            if (toAddrs == null || toAddrs.Count == 0)
                throw new AppException("No 'to email address' found to send email");

            foreach (var add in toAddrs)
                objMail.To.Add(add);

            if (ccAddrs != null && ccAddrs.Count > 0)
                foreach (var add in ccAddrs)
                    objMail.CC.Add(add);

            if (bccAddrs != null && bccAddrs.Count > 0)
                foreach (var add in bccAddrs)
                    objMail.Bcc.Add(add);

            objMail.From = new MailAddress(systemEmailSenderEmail, systemEmailSenderName);

            objMail.IsBodyHtml = true;

            objMail.Priority = MailPriority.Normal;

            if (attachmentFilepaths != null && attachmentFilepaths.Count > 0)
                foreach (var path in attachmentFilepaths)
                    objMail.Attachments.Add(new Attachment(path));

            objMail.Subject = subject;

            objMail.Body = bodyText;

            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(systemEmailSenderEmail, systemEmailSenderPassword);

            await client.SendMailAsync(objMail);
        }
    }

    public class EmailSettings
    {
        public string MailServerIp { get; set; }
        public string SystemEmailSenderEmail { get; set; }
        public string SystemEmailSenderUsername { get; set; }
        public string SystemEmailSenderPassword { get; set; }
        public string SystemEmailSenderName { get; set; }
        public int OutPortNo { get; set; }
    }
}
