using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineMarket.Models;
using OnlineMarketPlace.Areas.Identity.Data;
using OnlineMarketPlace.ClassLibraries;
using OnlineMarketPlace.ClassLibraries.Authentication;
using OnlineMarketPlace.Models.ViewModels;
using OnlineMarketPlace.Repository;

namespace OnlineMarketPlace.Controllers
{
    public class HomeController : Controller
    {
        #region Inject
        //Inject DataBase--Start
        UserManager<ApplicationUser> userManager;
        DbRepository<OnlineMarketContext, ContactUs, int> dbContactUs;
        PhoneNumberTokenProvider<ApplicationUser> _phoneNumberToken;
        OnlineMarketContext _db;

        private readonly IHostingEnvironment _hostingEnvironment; //returns the hosting environment data
        string contentRootPath;

        public HomeController
            (
                UserManager<ApplicationUser> _userManager,
                DbRepository<OnlineMarketContext, ContactUs, int> _dbContactUs,
                OnlineMarketContext db,
                IHostingEnvironment hostingEnvironment
            )
        {
            userManager = _userManager;
            dbContactUs = _dbContactUs;
            _db = db;

            _hostingEnvironment = hostingEnvironment;
            contentRootPath = _hostingEnvironment.ContentRootPath;
        }
        //Inject DataBase--End
        #endregion
        #region Index
        public IActionResult Index() //Home Page
        {
            //هشت محصول جدید
            List<ProductAbstract> LatestProducts = new List<ProductAbstract>();
            if (_db.ProductAbstract.Count() > 0)
            {
                LatestProducts = _db.ProductAbstract
                 .Include(x => x.ProductFeature)
                 .Include(x => x.ProductImage)
                 .Include(x => x.Category)
                 .OrderByDescending(x => x.RegDateTime).Take(8).ToList();
            }
            
            ViewData["LatestProducts"] = LatestProducts;
            return View();
        }
        [HttpPost]
        public IActionResult GetPartialProduct(int Id)
        {
            if (Id > 0)
            {
                var product = _db.ProductAbstract.Where(x => x.Id == Id && x.Status == true)
                    .Include(x => x.ProductImage)
                    .Include(x => x.Category)
                    .Include(x => x.ProductFeature)
                    .Include(x => x.ProductDescription).FirstOrDefault();
                if (product != null)
                {
                    return View(product);
                }
            }
            return View();
        }
        #endregion
        #region ContactUs
        public IActionResult ContactUs()
        {
            return View();
        }
        public async Task<IActionResult> InsertContactUsConfirm(ContactUsViewModel model)
        {
            if (ModelState.IsValid)
            {
                ContactUs contactUs = new ContactUs()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Comment = model.Comment,
                    RegdDateTime = DateTime.Now,
                };
                if (User.Identity.IsAuthenticated)
                {
                    ApplicationUser user = await userManager.FindByNameAsync(User.Identity.Name);
                    contactUs.UserId = user.Id;
                }
                try
                {
                    dbContactUs.Insert(contactUs);
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return RedirectToAction("ContactUs");
                }
            }
            return RedirectToAction("ContactUs");
        }

        #endregion

        #region About
        public IActionResult About()
        {
            return View();
        }
        #endregion

        #region PageNotFound
        [Route("[action]")]
        public ViewResult PageNotFound() => View(); //Independant layout
        #endregion


        public async Task<IActionResult> Test()
        {
            
            var r = ReadExcelFiles.Read(contentRootPath + @"\wwwroot\data-seeds\Province_iran.xlsx");
            
            return View();
        }
    }
}