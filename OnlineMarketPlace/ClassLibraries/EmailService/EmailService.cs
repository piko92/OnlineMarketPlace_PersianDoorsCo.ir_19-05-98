using OnlineMarketPlace.Models.AdminViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace OnlineMarketPlace.ClassLibraries.EmailService
{
    public static class EmailService
    {
        public static bool Send(EmailViewModel model, string smtpAdress, int? smtpPort)
        {
            string SenderEmail = model.SenderEmail;
            string Password = model.Password;
            string ReceiverEmail = model.ReceiverEmail;
            string Subject = model.Subject;
            string Content = model.Content;
            MailMessage msg = new MailMessage(SenderEmail, ReceiverEmail);
            msg.Subject = Subject;
            msg.Body = Content;
            if (model.AttachedFiles != null)
            {
                foreach (var item in model.AttachedFiles)
                {
                    MemoryStream ms = null;
                    using (ms = new MemoryStream())
                    {
                        item.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        Attachment att = new Attachment(new MemoryStream(fileBytes), item.FileName);
                        msg.Attachments.Add(att);
                    }
                }
            }
            //msg.IsBodyHtml = true;
            //msg.Priority = MailPriority.High;
            if (smtpAdress==null || smtpPort==null)
            {
                smtpAdress = "mail.persiandoorsco.ir";
                smtpPort = 587;
            }
            SmtpClient smtpClient = new SmtpClient(smtpAdress, smtpPort.Value);
            smtpClient.Credentials = new System.Net.NetworkCredential(SenderEmail, Password);
            // smtpClient.EnableSsl = true;
            try
            {
                smtpClient.Send(msg);
                return true;
            }
            catch (Exception)
            {
                return false;
            }


        }
    }
}
