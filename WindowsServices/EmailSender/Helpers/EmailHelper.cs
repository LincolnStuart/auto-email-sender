using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using EmailSender.Models;
using EmailSender.Repositories;

namespace EmailSender.Helpers
{
    public class EmailHelper
    {
        public static async Task SendMail(Email email, Config config, TemplateData templateData, EmailRepository emailRepo)
        {
            try
            {
                using (var smtpClient = BuildSmtpClient(email, config))
                using (var message = BuildMailMessage(email, config, templateData))
                {
                    var emails = email.To.Trim().Split(";");
                    foreach (var e in emails)
                    {
                        message.To.Add(new MailAddress(e.Trim()));
                    }
                    foreach (var a in email.Attachments)
                    {
                        message.Attachments.Add(new Attachment(new MemoryStream(a.Bytes), a.Name));
                    }
                    await smtpClient.SendMailAsync(message);
                }
            }
            catch (Exception e)
            {
                emailRepo.UpdateError(email.Id, e.Message);
                throw e;
            }
        }

        private static SmtpClient BuildSmtpClient(Email email, Config config)
        {
            return new SmtpClient
            {
                Host = config.Host,
                Port = config.Port,
                EnableSsl = config.EnableSsl,
                Credentials = new NetworkCredential(config.FromAddress, config.Password)
            };
        }

        private static MailMessage BuildMailMessage(Email email, Config config, TemplateData templateData)
        {
            return new MailMessage()
            {
                From = new MailAddress(config.FromAddress),
                Subject = email.Subject,
                Body = email.UsingTemplate ? TemplateHelper.BuildBody(email.Body, templateData) : email.Body,
                IsBodyHtml = true
            };
        }

    }
}
