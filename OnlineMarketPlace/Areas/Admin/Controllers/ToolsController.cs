using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OnlineMarket.Models;
using OnlineMarketPlace.Areas.Identity.Data;
using OnlineMarketPlace.Models.AdminViewModels;
using OnlineMarketPlace.Repository;

namespace OnlineMarketPlace.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperVisor")]

    public class ToolsController : Controller
    {
        #region Inject
        //Inject DataBase--Start
        IHostingEnvironment env = null;

        DbRepository<OnlineMarketContext, ContactUs, int> dbContactUs;
        public ToolsController
            (
                DbRepository<OnlineMarketContext, ContactUs, int> _dbContactUs,
                IHostingEnvironment env
            )
        {
            dbContactUs = _dbContactUs;
            this.env = env;
        }
        //Inject DataBase--End
        #endregion
        #region Contact_us
        //Contact_Us-Start
        public IActionResult ShowComment()
        {
            var dbViewModel = dbContactUs.GetAll();
            return View(dbViewModel);
        }
        public IActionResult DeleteComment(int Id)
        {
            var status = dbContactUs.DeleteById(Id);
            if (status)
            {

            }
            else
            {

            }
            return RedirectToAction("ShowComment");
        }
        public IActionResult ReplyComment(int Id)
        {
            ViewData["dbContactUs"] = dbContactUs.FindById(Id);
            return View();
        }
        //Contact_Us-End
        #endregion
        #region Email
        //Email-Start
        public IActionResult SendEmail()
        {
            return View();
        }
        public IActionResult SendEmailConfirm(EmailViewModel model)
        {
            if (ModelState.IsValid && model.Password == "Qwerty@01234")
            {
                string SenderEmail = model.SenderEmail;
                string Password = model.Password;
                string ReceiverEmail = model.ReceiverEmail;
                string Subject = model.Subject;
                string Content = model.Content;
                MailMessage msg = new MailMessage(SenderEmail, ReceiverEmail);
                msg.Subject = Subject;
                msg.Body = Content;
                if (model.AttachedFiles !=null)
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
                SmtpClient smtpClient = new SmtpClient("mail.persiandoorsco.ir", 587);
                smtpClient.Credentials = new System.Net.NetworkCredential(SenderEmail, Password);
                // smtpClient.EnableSsl = true;
                try
                {
                    smtpClient.Send(msg);
                }
                catch (Exception)
                {
                    return RedirectToAction("SendEmail");
                }

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("SendEmail");
            }
        }
        //Email-End
        #endregion
    }
}
