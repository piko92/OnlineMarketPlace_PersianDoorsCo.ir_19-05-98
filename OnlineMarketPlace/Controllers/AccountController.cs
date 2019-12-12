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
        DbRepository<OnlineMarketContext, Invoice, int> dbInvoice;
        private readonly IHostingEnvironment _hostingEnvironment; //returns the hosting environment data
        string contentRootPath;
        OnlineMarketContext _db;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IHostingEnvironment hostingEnvironment,
            DbRepository<OnlineMarketContext, Address, int> dbAddress,
             DbRepository<OnlineMarketContext, Invoice, int> _dbInvoice,
        OnlineMarketContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;

            _dbAddress = dbAddress;
            dbInvoice = _dbInvoice;
            _hostingEnvironment = hostingEnvironment;
            contentRootPath = _hostingEnvironment.ContentRootPath;//returns the root path of the website

        }//end AccountController(Constructor)
        #endregion
        #region Register
        public IActionResult Register(string msg)
        {
            if (msg != null)
            {
                TempData["msg"] = msg;
                return View();
            }
            return View();
        }
        public async Task<IActionResult> RegisterConfirm(RegisterViewModel model)
        {
            string message = null;
            if (ModelState.IsValid)
            {
                var isNotDuplicate = await _userManager.FindByNameAsync(model.UserName);
                if (isNotDuplicate != null)
                {
                    message = "این نام کاربری تکراری میباشد، از نام کاربری دیگری استفاده نمایید";
                    return RedirectToAction("Register", new { msg = message });
                }
                ApplicationUser NewUser = new ApplicationUser()
                {
                    UserName = model.UserName
                };
                var status = await _userManager.CreateAsync(NewUser, model.Password);
                if (status.Succeeded)
                {
                    await _userManager.AddToRoleAsync(NewUser, "Customer");
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
            message = "طی مراحل ثبت نام خطایی رخ داده، لطفا دوباره تلاش نمایید";
            return RedirectToAction("Register", new { msg = message });
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
            string message = null;
            try
            {
                if (!ModelState.IsValid)
                {
                    message = "تکمیل همه موارد ستاره دار الزامی میباشد";
                    return RedirectToAction("Register", new { msg = message });
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
                            message = "ثبت نام شما با موفقیت انجام پذیرفت";
                            return RedirectToAction("Index", "Home", new { msg = message });
                        }
                        else if (result == 0)
                        {
                            message = "کد وارد شده صحیح نمیباشد، جهت تایید در صفحه ثبت نام، گزینه ارسال مجدد کد تایید را انتخاب نمایید ";
                            return RedirectToAction("Register", new { msg = message });
                        }
                        else if (result == -2)
                        {
                            message = "اعتبار کد تایید وارد شده به پایان رسیده، از گزینه ارسال مجدد کد تایید استفاده نمایید";
                            return RedirectToAction("Register", new { msg = message });
                        }
                    }
                }
                message = "در مراجل ثبت نام شما مشکلی رخ داده است";
                return RedirectToAction("Register", new { msg = message });
            }
            catch(Exception ex)
            {
                message = "در مراجل ثبت نام شما مشکلی رخ داده است.";
                return RedirectToAction("Register", new { msg = message });
            }
        }

        public ViewResult ReSendVerification(string msg)
        {
            if (msg != null)
            {
                TempData["msg"] = msg;
                return View();
            }
            return View();
        }

        public async Task<IActionResult> ReSendVerificationConfirm(string Username)
        {
            string message = null;
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
                        message = "خطایی در ارسال کد تایید رخ داده دوباره تلاش نمایید";
                        return RedirectToAction("ReSendVerification", new { msg = message });
                    }
                    else
                    {
                        message = "این نام کاربری تایید گردیده است، شما میتوانید به حساب کاربری خود ورود نمایید";
                        return RedirectToAction("Login", new { msg = message });
                    }
                }
                else
                {
                    message = "این نام کاربری ثبت نگردیده، ابتدا ثبت نام نمایید";
                    return RedirectToAction("ReSendVerification", new { msg = message });
                }
            }
            else
            {
                message = "هیچ نام کاربری وارد نگردیده است";
                return RedirectToAction("ReSendVerification", new { msg = message });
            }
        }

        public ViewResult PasswordRecovery(string msg)
        {
            if (msg != null)
            {
                TempData["msg"] = msg;
                return View();
            }
            return View();
        }

        public async Task<IActionResult> PasswordRecoveryConfirm(string username)
        {
            string message = null;
            if (username == null)
            {
                message = "ورود نام کاربری الزامی میباشد";
                return RedirectToAction("PasswordRecovery", new { msg = message });
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
            string message = null;
            if (!ModelState.IsValid)
            {
                message = "تکمیل همه فیلد ها الزامی میباشد";
                return RedirectToAction("PasswordRecovery", new { msg = message });
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
                            message = "کلمه عبور شما با موفقیت تغییر یافت، اکنون میتوانید به حساب کاربری خود وارد گردید";
                            return RedirectToAction("Login", new { msg = message });
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
            message = "در مراحل عملیات درخواستی خطایی رخ داده دوباره امتحان کنید";
            return RedirectToAction("PasswordRecovery", new { msg = message });
        }
        #endregion
        #region Login
        public IActionResult Login(string msg)
        {
            if (msg != null)
            {
                TempData["msg"] = msg;
                return View();
            }
            return View();
        }

        public async Task<IActionResult> LoginConfirm(LoginViewModel model)
        {
            string message = null;
            try
            {
                if (!ModelState.IsValid)
                {
                    message = "تکمیل تمامی فیلدهای ستاره دار الزامیست";
                    return RedirectToAction("Login", new { msg = message });
                }
                var foundUser = await _userManager.FindByNameAsync(model.UserName);

                if (foundUser != null)
                {
                    var isUserName_Number = double.TryParse(foundUser.UserName, out double r) ? r : 0;

                    if (isUserName_Number > 0) //PhoneNumber is UserName
                    {
                        if (foundUser.PhoneNumberConfirmed == false)
                        {
                            message = "شماره همراه شما تایید نگردیده است";
                            return RedirectToAction("Login", new { msg = message });
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
                            message = "ایمیل شما تایید نگردیده است";
                            return RedirectToAction("Login", new { msg = message });
                        }
                        var status = await _signInManager.PasswordSignInAsync(foundUser, model.Password, model.RememberMe, false);
                        if (status.Succeeded)
                        {
                            message = "شما با موفقیت وارد حساب کاربری خود شدید";
                            return RedirectToAction("Index", "Home", new { msg = message });
                        }
                    }

                }
                message = "در مراحل ورود خطایی رخ داده، لطفا دوباره تلاش نمایید";
                return RedirectToAction("Login", new { msg = message });
            }
            catch(Exception ex)
            {
                message = "در مراحل ورود خطایی رخ داده، لطفا دوباره تلاش نمایید";
                return RedirectToAction("Login", new { msg = message });
            }
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
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewData["addresses"] = _db.Address.Where(x => x.UserId == currentUser.Id).ToList();
            ViewData["userInvoice"] = dbInvoice.GetInclude(e=>e.InvoiceProduct).Where(e=>e.CustomerId== currentUser.Id && e.IsPaid==true).ToList();
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

        public IActionResult EditAddress(UserPanelViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["msg"] = "تکمیل فیلدهای ستاره دار الزامی میباشد";
                return RedirectToAction("UserPanel");
            }
            if (model.AddressViewModel.Id > 0)
            {
                var address = _dbAddress.FindById(model.AddressViewModel.Id);
                if (address != null)
                {
                    address.RecieverFullName = model.AddressViewModel.Fullname;
                    address.Phone = model.AddressViewModel.MobilePhoneNumber;
                    address.PostalCode = model.AddressViewModel.PostalCode;
                    address.UserAddress = model.AddressViewModel.Address;

                    _dbAddress.Update(address);

                    return RedirectToAction("UserPanel");
                }
            }
            return RedirectToAction("UserPanel");
        }

        public JsonResult GetUserAddress(int id)
        {
            if (id > 0)
            {
                var address = _dbAddress.FindById(id);
                if (address != null)
                {
                    var result = new
                    {
                        status = true,
                        id = address.Id,
                        name = address.RecieverFullName,
                        phone = address.Phone,
                        postalCode = address.PostalCode,
                        address = address.UserAddress
                    };
                    return Json(result);
                }
            }
            return Json(new { status = false });
        }
        #endregion

        
    }//end AccountController
}