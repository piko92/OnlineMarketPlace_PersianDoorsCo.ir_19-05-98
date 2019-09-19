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
        private readonly IHostingEnvironment hostingEnvironment;
        string contentRootPath;
        public HomeController
            (
                UserManager<ApplicationUser> _userManager,
                DbRepository<OnlineMarketContext, ProductFeature, int> _dbProductFeature,
                DbRepository<OnlineMarketContext, Category, int> _dbCategory,
                DbRepository<OnlineMarketContext, ContactUs, int> _dbContactUs,
                DbRepository<OnlineMarketContext, Article, int> _dbArticle,
               IHostingEnvironment _hostingEnvironment
            )
        {
            userManager = _userManager;
            dbProductFeature = _dbProductFeature;
            dbCategory = _dbCategory;
            dbContactUs = _dbContactUs;
            dbArticle = _dbArticle;
            hostingEnvironment = _hostingEnvironment;
            contentRootPath = hostingEnvironment.ContentRootPath;
        }
        //Inject DataBase--End
        #endregion
        public IActionResult Index(string notification)
        {
            ViewData[" userManagerCount"] = userManager.Users.Where(x=>x.Status==true).Count();
            ViewData[" ProductFeatureCount"] = dbProductFeature.GetAll().Where(x => x.Status = true).Count();
            ViewData[" CategoryCount"] = dbCategory.GetAll().Where(x => x.Status = true).Count();
            ViewData[" ContactUsCount"] = dbContactUs.GetAll().Where(x => x.Status = true).Count();
            ViewData[" ArticleCount"] = dbArticle.GetAll().Where(x => x.Status = true).Count();
            if (notification != null)
            {
                ViewData["nvm"] = NotificationHandler.DeserializeMessage(notification);
            }
            return View();
        }
    }
}