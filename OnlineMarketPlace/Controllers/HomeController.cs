using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineMarket.Models;
using OnlineMarketPlace.Areas.Identity.Data;
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
        public HomeController
            (
                 UserManager<ApplicationUser> _userManager,
                DbRepository<OnlineMarketContext, ContactUs, int> _dbContactUs
            )
        {
            dbContactUs = _dbContactUs;
            userManager = _userManager;
        }
        //Inject DataBase--End
        #endregion
        #region Index
        public IActionResult Index() //Home Page
        {
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

    }
}