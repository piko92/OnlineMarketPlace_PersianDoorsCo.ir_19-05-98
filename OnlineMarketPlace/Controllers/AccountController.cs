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
using OnlineMarket.Models;
using OnlineMarketPlace.Areas.Identity.Data;
using OnlineMarketPlace.ClassLibraries;
using OnlineMarketPlace.ClassLibraries.Authentication;
using OnlineMarketPlace.ClassLibraries.MessageHandler;
using OnlineMarketPlace.Models.ViewModels;
using OnlineMarketPlace.Repository;

namespace OnlineMarketPlace.Controllers
{
    public class AccountController : Controller
    {
        #region Inject
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;
        DbRepository<OnlineMarketContext, Address, int> _dbAddress;
        private readonly IHostingEnvironment _hostingEnvironment; //returns the hosting environment data
        string contentRootPath;
        OnlineMarketContext _db;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IHostingEnvironment hostingEnvironment,
            DbRepository<OnlineMarketContext, Address, int> dbAddress,
            OnlineMarketContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;

            _dbAddress = dbAddress;
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
                var isNotDuplicate = await _userManager.FindByNameAsync(model.UserName);
                if (isNotDuplicate != null)
                {
                    TempData["msg"] = "این نام کاربری تکراری میباشد، از نام کاربری دیگری استفاده نمایید";
                    return RedirectToAction("Register");
                }
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
                            return RedirectToAction("CheckIdentity", new { newUsername = foundUser.UserName});
                        }
                    }
                }
            }
            return RedirectToAction("Register");
        } 
        #endregion
        #region ConfirmIdentity
        public IActionResult CheckIdentity(string newUsername)
        {
            ViewData["newUser"] = newUsername;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmIdentity(VerifyViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["msg"] = "تکمیل همه موارد ستاره دار الزامی میباشد";
                    return RedirectToAction("Register");
                }
                if (model != null)
                {
                    var newUser = await _userManager.FindByNameAsync(model.UserName);
                    if (newUser != null)
                    {
                        TokenGenerator tokenGenerator = new TokenGenerator(_userManager, _db);
                        int result = await tokenGenerator.ApproveVerificationToken(newUser, model.Token);
                        if (result == 1)
                        {
                            await _userManager.AddToRoleAsync(newUser, "Customer");
                            TempData["msg"] = "ثبت نام شما با موفقیت انجام پذیرفت";
                            return RedirectToAction("Index", "Home");
                        }
                        else if (result == 0)
                        {
                            TempData["msg"] = "کد وارد شده صحیح نمیباشد، جهت تایید در صفحه ثبت نام، گزینه ارسال مجدد کد تایید را انتخاب نمایید ";
                            return RedirectToAction("Register");
                        }
                        else if (result == -2)
                        {
                            TempData["msg"] = "اعتبار کد تایید وارد شده به پایان رسیده، از گزینه ارسال مجدد کد تایید استفاده نمایید";
                            return RedirectToAction("Register");
                        }
                    }
                }
                TempData["msg"] = "در مراجل ثبت نام شما مشکلی رخ داده است";
                return RedirectToAction("Register");
            }
            catch(Exception ex)
            {
                TempData["msg"] = "در مراجل ثبت نام شما مشکلی رخ داده است.";
                return RedirectToAction("Register");
            }
        }

        public ViewResult ReSendVerification() => View();

        public async Task<IActionResult> ReSendVerificationConfirm(string Username)
        {
            if (Username != null)
            {
                var isUser = await _userManager.FindByNameAsync(Username);
                if (isUser != null)
                {
                    if (isUser.PhoneNumberConfirmed == false || isUser.EmailConfirmed == false)
                    {
                        TokenGenerator tokenGenerator = new TokenGenerator(_userManager, _db);
                        var result = await tokenGenerator.SendVerificationToken(isUser);
                        if (result > 0)
                        {
                            return RedirectToAction("CheckIdentity", new { newUsername = isUser.UserName });
                        }
                        TempData["msg"] = "خطایی در ارسال کد تایید رخ داده دوباره تلاش نمایید";
                        return RedirectToAction("ReSendVerification");
                    }
                    else
                    {
                        TempData["msg"] = "این نام کاربری تایید گردیده است، شما میتوانید به حساب کاربری خود ورود نمایید";
                        return RedirectToAction("Login");
                    }
                }
                else
                {
                    TempData["msg"] = "این نام کاربری ثبت نگردیده، ابتدا ثبت نام نمایید";
                    return RedirectToAction("ReSendVerification");
                }
            }
            else
            {
                TempData["msg"] = "هیچ نام کاربری وارد نگردیده است";
                return RedirectToAction("ReSendVerification");
            }
        }

        public ViewResult PasswordRecovery()
        {
            return View();
        }

        public async Task<IActionResult> PasswordRecoveryConfirm(string username)
        {
            if (username == null)
            {
                TempData["msg"] = "ورود نام کاربری الزامی میباشد";
                return RedirectToAction("PasswordRecovery");
            }
            else
            {
                var claimedUser = await _userManager.FindByNameAsync(username);
                if (claimedUser != null)
                {
                    TokenGenerator tokenGenerator = new TokenGenerator(_userManager, _db);
                    var result = await tokenGenerator.ResetPassword(claimedUser);
                    if (result == 1)
                    {
                        var encryptedUserName = CustomizedCryptography.Encrypt(claimedUser.UserName);
                        return RedirectToAction("ResetPasswordCheckToken", new { username = encryptedUserName });
                    }
                }
            }
            return View();
        }

        public ViewResult ResetPasswordCheckToken(string username)
        {
            ViewData["username"] = username;
            return View();
        }

        public async Task<IActionResult> ResetPasswordCheckTokenConfirm(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["msg"] = "تکمیل همه فیلد ها الزامی میباشد";
                return RedirectToAction("PasswordRecovery");
            }
            if (model != null)
            {
                string decryptedUsername = CustomizedCryptography.Decrypt(model.Key);
                if (decryptedUsername != null)
                {
                    var claimedUser = await _userManager.FindByNameAsync(decryptedUsername);
                    if (claimedUser != null)
                    {
                        TokenGenerator tokenGenerator = new TokenGenerator(_userManager, _db);
                        var result = await tokenGenerator.VerifyResetPassword(claimedUser, model.Token, model.Password);
                        if (result == 1)
                        {
                            TempData["msg"] = "کلمه عبور شما با موفقیت تغییر یافت، اکنون میتوانید به حساب کاربری خود وارد گردید";
                            return RedirectToAction("Login");
                        }
                        else if(result == -1)
                        {
                            TempData["msg"] = "کد وارد شده صحیح نمیباشد";
                            var encryptedUserName = CustomizedCryptography.Encrypt(claimedUser.UserName);
                            return RedirectToAction("ResetPasswordCheckToken", new { username = encryptedUserName });
                        }
                    }
                }
            }
            TempData["msg"] = "در مراحل عملیات درخواستی خطایی رخ داده دوباره امتحان کنید";
            return RedirectToAction("PasswordRecovery");
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
        public async Task<IActionResult> UserPanel()
        {
            var foundUser = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewData["addresses"] = _db.Address.Where(x => x.UserId == foundUser.Id).ToList();
            return View();
        }

        [Authorize]
        public async Task<IActionResult> EditProfile(UserPanelViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["msg"] = "تکمیلی فیلدهای ستاره دار الزامی میباشد";
                return RedirectToAction("UserPanel");
            }
            var selectedUser = await _userManager.FindByIdAsync(model.EditUserViewModel.Id);
            if (selectedUser != null)
            {
                selectedUser.FirstName = model.EditUserViewModel.Firstname;
                selectedUser.LastName = model.EditUserViewModel.Lastname;
                selectedUser.NationalCode = model.EditUserViewModel.Nationalcode;
                selectedUser.PhoneNumber = model.EditUserViewModel.Phonenumber;
                //var dob = CustomizeCalendar.PersianToGregorian(model.Dateofbirth_Year, model.Dateofbirth_Month, model.Dateofbirth_Day);
                //selectedUser.DateOfBirth = dob;
                selectedUser.Email = model.EditUserViewModel.Email;

                await _userManager.UpdateAsync(selectedUser);
            }
            return RedirectToAction("UserPanel");
        }

        [Authorize]
        public async Task<IActionResult> ChangePassword(UserPanelViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["msg"] = "تکمیل همه فیلدهای ستاره دار الزامی میباشد";
                return RedirectToAction("UserPanel");
            }
            var foundUser = await _userManager.FindByNameAsync(User.Identity.Name);
            if (foundUser != null)
            {
                var status = await _userManager.ChangePasswordAsync(foundUser, model.ChangePasswordViewModel.CurrentPasword, model.ChangePasswordViewModel.Newpassword);
                if (status.Succeeded)
                {
                    TempData["msg"] = "کلمه عبور شما با موفقیت تغییر یافت، برای ادامه مجددا ورود نمایید";
                    return RedirectToAction("Logout");
                }
            }

            return RedirectToAction("UserPanel");
        }

        [Authorize]
        public async Task<IActionResult> CreateAddress(string Fullname, string MobilePhoneNumber, string PostalCode, string Address)
        {
            if (Fullname == null || MobilePhoneNumber == null ||  Address == null)
            {
                TempData["msg"] = "تکمیل همه فیلدهای الزامی میباشد";
                return RedirectToAction("UserPanel");
            }
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            if (currentUser != null)
            {
                Address address = new Address()
                {
                    RecieverFullName = Fullname,
                    Phone = MobilePhoneNumber,
                    PostalCode = PostalCode,
                    UserAddress = Address,
                    UserId = currentUser.Id
                };
                _dbAddress.Insert(address);
            }

            return RedirectToAction("UserPanel");
        }
      
        public IActionResult RemoveAddress(int id)
        {
            if (id > 0)
            {
                var address = _dbAddress.FindById(id);
                if (address != null)
                {
                    var deleted = _dbAddress.DeleteById(id);
                    return RedirectToAction("UserPanel");
                }
            }
            return RedirectToAction("UserPanel");
        }
        public IActionResult EditAddress(int id)
        {
            if (id > 0)
            {
                var address = _dbAddress.FindById(id);
                if (address != null)
                {
                    
                    return RedirectToAction("UserPanel");
                }
            }
            return RedirectToAction("UserPanel");
        }

        #endregion
    }//end AccountController
}