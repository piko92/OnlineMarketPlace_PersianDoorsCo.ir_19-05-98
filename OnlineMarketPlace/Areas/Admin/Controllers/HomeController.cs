using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineMarket.Models;
using OnlineMarketPlace.Areas.Identity.Data;
using OnlineMarketPlace.ClassLibraries.NotificationHandler;
using OnlineMarketPlace.Models.AdminViewModels;
using OnlineMarketPlace.Repository;

namespace OnlineMarketPlace.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperVisor")]
    public class HomeController : Controller
    {
        #region Inject
        //Inject DataBase--Start
        UserManager<ApplicationUser> userManager;
        DbRepository<OnlineMarketContext, ProductFeature, int> dbProductFeature;
        DbRepository<OnlineMarketContext, Category, int> dbCategory;
        DbRepository<OnlineMarketContext, ContactUs, int> dbContactUs;
        DbRepository<OnlineMarketContext, Article, int> dbArticle;
        DbRepository<OnlineMarketContext, Invoice, int> dbInvoice;
        DbRepository<OnlineMarketContext, GeneralPage, int> dbGeneralPage;
        private readonly IHostingEnvironment hostingEnvironment;
        string contentRootPath;
        public HomeController
            (
                UserManager<ApplicationUser> _userManager,
                DbRepository<OnlineMarketContext, ProductFeature, int> _dbProductFeature,
                DbRepository<OnlineMarketContext, Category, int> _dbCategory,
                DbRepository<OnlineMarketContext, ContactUs, int> _dbContactUs,
                DbRepository<OnlineMarketContext, Article, int> _dbArticle,
                DbRepository<OnlineMarketContext, Invoice, int> _dbInvoice,
                DbRepository<OnlineMarketContext, GeneralPage, int> _dbGeneralPage,
                IHostingEnvironment _hostingEnvironment
            )
        {
            userManager = _userManager;
            dbProductFeature = _dbProductFeature;
            dbCategory = _dbCategory;
            dbContactUs = _dbContactUs;
            dbArticle = _dbArticle;
            dbInvoice = _dbInvoice;
            dbGeneralPage = _dbGeneralPage;
            hostingEnvironment = _hostingEnvironment;
            contentRootPath = hostingEnvironment.ContentRootPath;
        }
        //Inject DataBase--End
        #endregion
        #region Index
        public IActionResult Index(string notification)
        {
            ViewData[" userManagerCount"] = userManager.Users.Where(x => x.Status == true).Count();
            ViewData[" ProductFeatureCount"] = dbProductFeature.GetAll().Where(x => x.Status == true).Count();
            ViewData[" CategoryCount"] = dbCategory.GetAll().Where(x => x.Status == true).Count();
            ViewData[" ContactUsCount"] = dbContactUs.GetAll().Where(x => x.Status == true).Count();
            ViewData[" ArticleCount"] = dbArticle.GetAll().Where(x => x.Status == true).Count();
            ViewData[" PaidInvoiceCount"] = dbInvoice.GetAll().Where(x => x.IsPaid == true).Count();
            if (notification != null)
            {
                ViewData["nvm"] = NotificationHandler.DeserializeMessage(notification);
            }
            return View();
        }
        #endregion
        public IActionResult About(string notification)
        {
            var entity = dbGeneralPage.GetAll().Where(e => e.Title == "AboutUs").FirstOrDefault();
            if (entity==null)
            {
                GeneralPage generalPage = new GeneralPage()
                {
                    Title = "AboutUs"
                };
                dbGeneralPage.Insert(generalPage);
            }
            ViewData["AboutUs"] = dbGeneralPage.GetAll().Where(e => e.Title == "AboutUs").FirstOrDefault(); ;
            return View();
        }
        public IActionResult InsertAboutConfirm(GeneralPageViewModel model)
        {
            string nvm;
            if (ModelState.IsValid==false)
            {
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Wrong_Values, contentRootPath);
                return RedirectToAction("InsertPost", new { notification = nvm });
            }
            
            byte[] b = null;
            var entity = dbGeneralPage.GetAll().Where(e => e.Title == "AboutUs").FirstOrDefault();
            if (entity != null)
            {
                entity.ContentHtml = model.ContentHtml;
                entity.Summary = model.Summary;
                entity.Status = model.Status;
            }
            if (model.MainImage != null)
            {
                b = new byte[model.MainImage.Length];
                model.MainImage.OpenReadStream().Read(b, 0, (int)model.MainImage.Length);
                entity.MainImage = b;
            }
            try
            {
                dbGeneralPage.Update(entity);
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Success_Update, contentRootPath);
                return RedirectToAction("Index", new { notification = nvm });
            }
            catch (Exception)
            {
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Failed_Operation, contentRootPath);
                return RedirectToAction("Index", new { notification = nvm });
            }
           
        }
    }
}