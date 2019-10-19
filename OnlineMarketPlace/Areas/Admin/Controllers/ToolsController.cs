using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OnlineMarket.Models;
using OnlineMarketPlace.Areas.Identity.Data;
using OnlineMarketPlace.ClassLibraries;
using OnlineMarketPlace.ClassLibraries.NotificationHandler;
using OnlineMarketPlace.ClassLibraries.SMSService.SMSIR;
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
        DbRepository<OnlineMarketContext, Setting, int> dbSetting;
        OnlineMarketContext _db;
        private IConfiguration _configuration;
        string contentRootPath;
        public ToolsController
            (
                DbRepository<OnlineMarketContext, ContactUs, int> _dbContactUs,
                DbRepository<OnlineMarketContext, Setting, int> _dbSetting,
                IHostingEnvironment env,
                IConfiguration configuration,
                OnlineMarketContext db
            )
        {
            dbContactUs = _dbContactUs;
            dbSetting = _dbSetting;
            this.env = env;
            _configuration = configuration;
            contentRootPath = env.ContentRootPath;
            _db = db;
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
            string nvm;
            try
            {
                var status = dbContactUs.DeleteById(Id);
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Success_Remove, contentRootPath);
                return Json(nvm);
            }
            catch (Exception)
            {
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Failed_Remove, contentRootPath);
                return Json(nvm);
            }

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
        #region Setting
        [Authorize(Roles = "Admin")]
        public IActionResult Setting(string notification)
        {
            if (notification != null)
            {
                ViewData["nvm"] = NotificationHandler.DeserializeMessage(notification);
                return View();
            }
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult EditSetting(SettingViewModel model)
        {
            string nvm;
            if (model != null)
            {
                var isNotEmpty = dbSetting.GetAll();
                if (isNotEmpty.Count == 0)
                {
                    Setting setting = new Setting()
                    {
                        SMSApiAddress = model.SMSApiAddress,
                        SMSApiNumber = model.SMSApiNumber,
                        SMSUsername = model.SMSUsername,
                        AdminEmail = model.AdminEmail,
                        EmailPort = model.EmailPort,
                        EmailProtocol = model.EmailProtocol,
                        EmailServiceProvider = model.EmailServiceProvider,
                        BaseCurrencyId = model.BaseCurrencyId

                    };
                    dbSetting.Insert(setting);
                    nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Success_Update, contentRootPath);
                    return RedirectToAction("Setting", new { notification = nvm });
                }

                var foundSetting = dbSetting.FindById(model.Id);
                if (foundSetting != null)
                {
                    foundSetting.AdminEmail = model.AdminEmail;
                    foundSetting.AdminEmailPassword = model.AdminEmailPassword;
                    foundSetting.EmailServiceProvider = model.EmailServiceProvider;
                    foundSetting.EmailProtocol = model.EmailProtocol;
                    foundSetting.EmailPort = model.EmailPort;
                    foundSetting.SMSApiAddress = model.SMSApiAddress;
                    foundSetting.SMSApiNumber = model.SMSApiNumber;
                    foundSetting.SMSUsername = model.SMSUsername;
                    foundSetting.BaseCurrencyId = model.BaseCurrencyId;

                    var status = dbSetting.Update(foundSetting);
                    nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Success_Update, contentRootPath);
                    return RedirectToAction("Setting", new { notification = nvm });
                }
            }
            nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Failed_Update, contentRootPath);
            return RedirectToAction("Setting", new { notification = nvm });
        }

        public ViewResult FillTables(string notification)
        {
            if (notification != null)
            {
                ViewData["nvm"] = NotificationHandler.DeserializeMessage(notification);
                return View();
            }
            return View();
        }

        public async Task<IActionResult> FillAdditionalTables(AdditionalTablesViewModel model)
        {
            string nvm;
            bool countryOK = false, provinceOK = false, cityOK = false;
            try
            {
                if (ModelState.IsValid)
                {
                    string folderPath = _configuration.GetSection("DefaultPaths").GetSection("DataSet").Value;
                    if (model.Country != null)
                    {
                        var tempFile = await FileManager.ReadAndSaveFile(contentRootPath, folderPath, model.Country);
                        var combinedPath = Path.Combine(contentRootPath, tempFile);
                        var renderedCountyFile = ReadExcelFiles.Read(combinedPath);
                        List<Country> countries = new List<Country>();
                        for (int i = 0; i < renderedCountyFile.Count; i++)
                        {
                            Country country = new Country()
                            {
                                Name = renderedCountyFile[i][0]
                            };
                            countries.Add(country);
                        }
                        if (model.RewriteCountry == true)
                        {
                            _db.Country.RemoveRange(_db.Country.ToList());
                            _db.SaveChanges();
                        }
                        countries = countries.OrderBy(x => x.Name).ToList();
                        _db.Country.AddRange(countries);
                        var result = _db.SaveChanges();
                        var deleted = FileManager.DeleteFile(contentRootPath, tempFile);
                        if (result == 1)
                        {
                            countryOK = true;
                        }
                    }//end Country

                    if (model.Province != null)
                    {
                        var tempFile = await FileManager.ReadAndSaveFile(contentRootPath, folderPath, model.Province);
                        var combinedPath = Path.Combine(contentRootPath, tempFile);
                        var renderedProvinceFile = ReadExcelFiles.Read(combinedPath);
                        List<Province> provinces = new List<Province>();
                        for (int i = 0; i < renderedProvinceFile.Count; i++)
                        {
                            Province province = new Province()
                            {
                                Name = renderedProvinceFile[i][0]
                            };
                            provinces.Add(province);
                        }
                        if (model.RewriteProvince == true)
                        {
                            _db.Province.RemoveRange(_db.Province.ToList());
                            _db.SaveChanges();
                        }
                        provinces = provinces.OrderBy(x => x.Name).ToList();
                        _db.Province.AddRange(provinces);
                        var result = _db.SaveChanges();
                        var deleted = FileManager.DeleteFile(contentRootPath, tempFile);
                        if (result == 1)
                        {
                            provinceOK = true;
                        }
                    }//end Province

                    if (model.City != null)
                    {
                        var tempFile = await FileManager.ReadAndSaveFile(contentRootPath, folderPath, model.City);
                        var combinedPath = Path.Combine(contentRootPath, tempFile);
                        var renderedCityFile = ReadExcelFiles.Read(combinedPath);
                        List<City> cities = new List<City>();
                        for (int i = 0; i < renderedCityFile.Count; i++)
                        {
                            City city = new City()
                            {
                                Name = renderedCityFile[i][0]
                            };
                            cities.Add(city);
                        }
                        if (model.RewriteCity == true)
                        {
                            _db.City.RemoveRange(_db.City.ToList());
                            _db.SaveChanges();
                        }
                        cities = cities.OrderBy(x => x.Name).ToList();
                        _db.City.AddRange(cities);
                        var result = _db.SaveChanges();
                        var deleted = FileManager.DeleteFile(contentRootPath, tempFile);
                        if (result == 1)
                        {
                            cityOK = true;
                        }
                    }//end City

                    if (countryOK == true && provinceOK == true && cityOK == true)
                    {
                        nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Success_Insert, contentRootPath);
                        return RedirectToAction("FillTables", new { notification = nvm }); //successful insert
                    }
                }
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Failed_Insert, contentRootPath);
                return RedirectToAction("FillTables", new { notification = nvm }); //failed insert
            }
            catch (Exception ex)
            {
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Failed_Operation, contentRootPath);
                return RedirectToAction("FillTables", new { notification = nvm }); //failed insert
            }
        }
        #endregion
        #region SmsIr
        public IActionResult SendSms(string smsText, string phoneNumber)
        {
            return View();
        }
        public IActionResult SendSmsConfirm(string smsText, string phoneNumber)
        {
            // string ApiKey = "ae868fb306a4d18320668fb";
            // string SecurityCode = "PersianDoorsCo!@#12345";
            // string lineNumber = "30004747474480";
            var setting = dbSetting.GetAll().FirstOrDefault();
            string ApiKey = setting.SMSApiAddress;
            string SecurityCode = setting.SMSUsername;
            string lineNumber = setting.SMSApiNumber;
            //متن پیامک
            string message = smsText;
            //شماره مقصد
            string mobileNumber = phoneNumber;
            var result = SmsIrService.SendSms(ApiKey, SecurityCode, lineNumber, message, mobileNumber);
            return Json(result);
        }
        #endregion
    }
}
