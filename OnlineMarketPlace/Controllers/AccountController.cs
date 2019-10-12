using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnlineMarketPlace.Areas.Identity.Data;
using OnlineMarketPlace.ClassLibraries;
using OnlineMarketPlace.ClassLibraries.Authentication;
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
        OnlineMarketContext _db;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IHostingEnvironment hostingEnvironment,
            OnlineMarketContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;

            _hostingEnvironment = hostingEnvironment;
            contentRootPath = _hostingEnvironment.ContentRootPath;//returns the root path of the website

        }//end AccountController(Constructor)
        #endregion
        #region Register
        public IActionResult Register() => View();
        public async Task<IActionResult> RegisterConfirm(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser NewUser = new ApplicationUser()
                {
                    UserName = model.UserName
                };
                var status = await _userManager.CreateAsync(NewUser, model.Password);
                if (status.Succeeded)
                {
                    TokenGenerator tokenGenerator = new TokenGenerator(_userManager, _db);
                    var foundUser = await _userManager.FindByNameAsync(NewUser.UserName);
                    if (foundUser != null && (foundUser.PhoneNumberConfirmed == false || foundUser.EmailConfirmed == false))
                    {
                        var result = await tokenGenerator.SendVerificationToken(foundUser);
                        if (result > 0)
                        {
                            HttpContext.Session.SetString("newRegUser", foundUser.Id);

                            return RedirectToAction("CheckIdentity");
                        }
                    }
                }
            }
            return RedirectToAction("Register");
        }
        #endregion
        #region ConfirmIdentity
        public IActionResult CheckIdentity() => View();
        public async Task<IActionResult> ConfirmIdentity(string token)
        {
            var newUserId = HttpContext.Session.GetString("newRegUser");
            if (newUserId != null)
            {
                var newUser = await _userManager.FindByIdAsync(newUserId);
                if (newUser != null)
                {
                    TokenGenerator tokenGenerator = new TokenGenerator(_userManager, _db);
                    var r = await tokenGenerator.ApproveVerificationToken(newUser, token);
                    TempData["msg"] = "ثبت نام شما با موفقیت انجام پذیرفت";
                    return RedirectToAction("Index", "Home");
                }
            }
            TempData["msg"] = "در مراجل ثبت نام شما مشکلی رخ داده است.";
            return RedirectToAction("Register");
        }
        #endregion
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
                var isUserName_Number = double.TryParse(foundUser.UserName, out double r) ? r : 0;

                if (isUserName_Number > 0) //PhoneNumber is UserName
                {
                    if (foundUser.PhoneNumberConfirmed == false)
                    {
                        TempData["msg"] = "شماره همراه شما تایید نگردیده است";
                        return RedirectToAction("Login");
                    }
                    var status = await _signInManager.PasswordSignInAsync(foundUser, model.Password, model.RememberMe, false);
                    if (status.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }

                }
                else                       //Email is UserName
                {
                    if (foundUser.EmailConfirmed == false)
                    {
                        TempData["msg"] = "ایمیل شما تایید نگردیده است";
                        return RedirectToAction("Login");
                    }
                    var status = await _signInManager.PasswordSignInAsync(foundUser, model.Password, model.RememberMe, false);
                    if (status.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
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
        #region Panel

        [Authorize]
        public IActionResult UserPanel()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> EditProfile(EditUserViewModel model)
        {
            var selectedUser = await _userManager.FindByIdAsync(model.Id);
            if (selectedUser != null)
            {
                selectedUser.FirstName = model.Firstname;
                selectedUser.LastName = model.Lastname;
                selectedUser.NationalCode = model.Nationalcode;
                selectedUser.PhoneNumber = model.Phonenumber;
                //var dob = CustomizeCalendar.PersianToGregorian(model.Dateofbirth_Year, model.Dateofbirth_Month, model.Dateofbirth_Day);
                //selectedUser.DateOfBirth = dob;
                selectedUser.Email = model.Email;

                await _userManager.UpdateAsync(selectedUser);
            }
            return RedirectToAction("UserPanel");
        }

        #endregion
    }//end AccountController
}