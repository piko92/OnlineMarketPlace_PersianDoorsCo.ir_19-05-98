using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnlineMarketPlace.Areas.Identity.Data;
using OnlineMarketPlace.ClassLibraries.MessageHandler;
using OnlineMarketPlace.Models.ViewModels;

namespace OnlineMarketPlace.Controllers
{
    public class AccountController : Controller
    {
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;
        private readonly IHostingEnvironment _hostingEnvironment; //returns the hosting environment data
        string contentRootPath;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IHostingEnvironment hostingEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;

            _hostingEnvironment = hostingEnvironment;
            contentRootPath = _hostingEnvironment.ContentRootPath;//returns the root path of the website

        }//end AccountController(Constructor)

        //public IActionResult Register() => View();
        //public async Task<IActionResult> RegisterConfirm(RegisterViewModel model)
        //{
        //    if (model != null)
        //    {
        //        ApplicationUser NewUser = new ApplicationUser()
        //        {
        //            Email = model.Email,
        //            UserName = model.Email
        //        };
        //        var status = await _userManager.CreateAsync(NewUser, model.Password);
        //        if (status.Succeeded)
        //        {
        //            TempData["message"] = messages.Find(x => x.Title == "RegisterSuccess").Content;
        //        }
        //        return RedirectToAction("Index", "Home");
        //    }
        //    TempData["message"] = messages.Find(x => x.Title == "NotCompletedFields").Content;
        //    return RedirectToAction("Register");
        //}//end RegisterConfirm

        //public ViewResult Login() => View();
        //public async Task<IActionResult> LoginConfirm(LoginViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return RedirectToAction("Login");
        //    }
        //    else
        //    {
        //        var foundUser = await _userManager.FindByNameAsync(model.UserName);
        //        if (foundUser != null)
        //        {
        //            var status = await _signInManager.PasswordSignInAsync(foundUser, model.Password, model.RememberMe, false);
        //            if (status.Succeeded)
        //            {
        //                TempData["message"] = messages.Find(x => x.Title == "LoginSuccess").Content;
        //                return RedirectToAction("Index", "Home");
        //            }
        //            else
        //            {
        //                TempData["message"] = messages.Find(x => x.Title == "LoginWrongValues").Content;
        //                return RedirectToAction("Login");
        //            }
        //        }
        //        TempData["message"] = messages.Find(x => x.Title == "UserNotFound").Content;
        //        return RedirectToAction("Login");
        //    }
        //    TempData["message"] = messages.Find(x => x.Title == "LoginEmptyValues").Content;
        //    return RedirectToAction("Login");

        //}//end LoginConfirm

        //public async Task<IActionResult> Logout()
        //{
        //    await _signInManager.SignOutAsync();
        //    return RedirectToAction("Index", "Home");
        //}

    }//end AccountController
}