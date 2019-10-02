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
        #region Inject
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
        #endregion
        #region Register
        public IActionResult Register() => View();
        #endregion

        public async Task<IActionResult> RegisterConfirm(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser NewUser = new ApplicationUser()
                {
                    Email = model.Email,
                    UserName = model.Email
                };
                var status = await _userManager.CreateAsync(NewUser, model.Password);
                if (status.Succeeded)
                {
                    return RedirectToAction("Login");
                }
            }
            return RedirectToAction("Register");
        }

        #region Login
        public IActionResult Login() => View();

        public async Task<IActionResult> LoginConfirm(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Login");
            }
            var foundUser = await _userManager.FindByNameAsync(model.UserName);
            if (foundUser != null)
            {
                var status = await _signInManager.PasswordSignInAsync(foundUser, model.Password, model.RememberMe, false);
                if (status.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Login");
        }
        #endregion
        #region LogOut
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }//end AccountController
}