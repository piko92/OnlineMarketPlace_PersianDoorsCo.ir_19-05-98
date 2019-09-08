using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OnlineMarket.Models;
using OnlineMarketPlace.Areas.Identity.Data;
using OnlineMarketPlace.ClassLibraries;
using OnlineMarketPlace.ClassLibraries.NotificationHandler;
using OnlineMarketPlace.Models.ViewModels;
using OnlineMarketPlace.Repository;

namespace OnlineMarketPlace.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperVisor")]
    public class AccountController : Controller
    {
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;
        RoleManager<IdentityRole> _roleManager;
        OnlineMarketContext _db;
        DbRepository<OnlineMarketContext, UserModified, int> _dbUserModified;
        DbRepository<OnlineMarketContext, UserImage, int> _dbUserImage;

        private readonly IHostingEnvironment _hostingEnvironment; //returns the hosting environment data
        string contentRootPath;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            OnlineMarketContext db,
            IHostingEnvironment hostingEnvironment,
            DbRepository<OnlineMarketContext, UserModified, int> dbUserModified)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _db = db;

            _dbUserModified = dbUserModified;

            _hostingEnvironment = hostingEnvironment;
            contentRootPath = _hostingEnvironment.ContentRootPath;//returns the root path of the website
        }

        [AllowAnonymous]
        //[Route("[action]")]
        public ViewResult Signin(string notification)
        {
            if (notification != null)
            {
                ViewData["nvm"] = NotificationHandler.DeserializeMessage(notification);
            }
            return View();
        }//end Signin

        //احتیاج به کپچا دارد
        [AllowAnonymous]
        public async Task<IActionResult> SigninConfirm(SigninViewModel model)
        {
            string nvm;
            if (!ModelState.IsValid)
            {
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Wrong_Values, contentRootPath);
                return RedirectToAction("Signin", new { notification = nvm });
            }
            else
            {
                var claimedUser = await _userManager.FindByNameAsync(model.Username);
                if (claimedUser != null)
                {
                    var status = await _signInManager.PasswordSignInAsync(claimedUser, model.Password, model.IsRemember, false);
                    if (status.Succeeded)
                    {
                        nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Success_Login, contentRootPath);
                        return RedirectToAction("Index", "Home", new { notification = nvm }); //successful signin
                    }
                    else
                    {
                        nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Failed_Operation, contentRootPath);
                        return RedirectToAction("Signin", new { notification = nvm });
                    }
                }
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Wrong_Values, contentRootPath); //wrong username or password
                return RedirectToAction("Signin", new { notification = nvm });
            }
        }//end SigninConfirm
        //Initialize User--Start
        [AllowAnonymous]
        public async Task<IActionResult> InitializeUser()
        {
            string nvm;
            //making identity roles
            string[] RolesName = { "Admin", "SuperVisor", "Customer" };
            foreach (var item in RolesName)
            {
                if (await _roleManager.RoleExistsAsync(item) == false)
                {
                    IdentityRole role = new IdentityRole(item);
                    await _roleManager.CreateAsync(role);
                }
            }

            //check if Admin account exist or not ?! if not, create
            var AdminExist = await _userManager.FindByNameAsync("admin@admin.com");
            if (AdminExist == null)
            {
                ApplicationUser admin = new ApplicationUser()
                {
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com",
                    FirstName = "Saeed",
                    LastName = "Panahi",
                    Gendre = 1,
                    DateOfBirth = new DateTime(1990, 1, 20),
                    SpecialUser = true,
                    Rank = 1000,
                    NationalCode = "2283332211"
                };
                var status = await _userManager.CreateAsync(admin, "Qwerty@01234");
                if (status.Succeeded)
                {
                    await _userManager.AddToRoleAsync(admin, "Admin");
                }
            }
            nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Success_Insert, contentRootPath);
            return RedirectToAction("Signup", new { notification = nvm });
        }
        //Initialize User--End

        public async Task<IActionResult> Signout()
        {
            await _signInManager.SignOutAsync();
            return RedirectPermanent("/Home/Index");
        }//end Signout

        public ViewResult Signup(string notification)
        {
            if (notification != null)
            {
                ViewData["nvm"] = NotificationHandler.DeserializeMessage(notification);
            }
            return View();
        }//end Signup

        public async Task<IActionResult> SignupConfirm(SignupViewModel model, List<IFormFile> img)
        {

            string nvm;
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Require_Login, contentRootPath);
                    return RedirectToAction("Signin", new { notification = nvm });
                }
                if (!ModelState.IsValid)
                {
                    nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Wrong_Values, contentRootPath);
                    return RedirectToAction("Signup", new { notification = nvm });
                }
                else
                {
                    if (model != null)
                    {

                        var chkUser = await _userManager.FindByNameAsync(model.Username);
                        ApplicationUser chkEmail;
                        if (model.Email != null)
                        {
                            chkEmail = await _userManager.FindByEmailAsync(model.Email);
                        }
                        else
                        {
                            chkEmail = null;
                        }
                        if (chkUser == null && chkEmail == null)
                        {


                            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
                            var greDate = CustomizeCalendar.PersianToGregorian(model.Dateofbirth ?? DateTime.Now);

                            //create new user
                            ApplicationUser newUser = new ApplicationUser()
                            {
                                FirstName = model.Firstname,
                                LastName = model.Lastname,
                                UserName = model.Username,
                                Email = model.Email,
                                PhoneNumber = model.Phonenumber,
                                SpecialUser = model.Specialuser,
                                Status = model.Status,
                                DefinedByUser = currentUser,
                                DateOfBirth = greDate,
                                Rank = 1,
                                NationalCode = model.Nationalcode
                            };
                            if (model.Gender == true)
                            {
                                newUser.Gendre = 1; //male
                            }
                            else if (model.Gender == false)
                            {
                                newUser.Gendre = 2; //female
                            }
                            else
                            {
                                newUser.Gendre = null; //not specified
                            }

                            var user_status = await _userManager.CreateAsync(newUser, model.Password);
                            if (user_status.Succeeded)
                            {
                                await _userManager.AddToRoleAsync(newUser, "SuperVisor");

                                //Add image of user in 'UserImage' table, seperately
                                if (img.Count > 0)
                                {
                                    var newAddedUser = await _userManager.FindByNameAsync(newUser.UserName);
                                    if (newAddedUser != null)
                                    {
                                        try
                                        {
                                            UserImage userImage = new UserImage()
                                            {
                                                UserId = newAddedUser.Id,
                                                Caption = newAddedUser.LastName + "_" + DateTime.Now
                                            };
                                            img.ForEach(x =>
                                            {
                                                if (x != null)
                                                {
                                                    byte[] b = new byte[x.Length];
                                                    x.OpenReadStream().Read(b, 0, b.Length);
                                                    userImage.Image = b;

                                                    //Make Thumbnail
                                                    MemoryStream mem1 = new MemoryStream(b);
                                                    Image img2 = Image.FromStream(mem1);
                                                    Bitmap bmp = new Bitmap(img2, 120, 120);
                                                    MemoryStream mem2 = new MemoryStream();
                                                    bmp.Save(mem2, System.Drawing.Imaging.ImageFormat.Jpeg);
                                                    userImage.ImageThumbnail = mem2.ToArray();
                                                }
                                            });
                                            _db.UserImage.Add(userImage);
                                            _db.SaveChanges();
                                        }
                                        catch (Exception ex)
                                        {
                                            nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Failed_Insert_Image, contentRootPath);
                                            return RedirectToAction("Signup", new { notification = nvm });
                                        }
                                    }
                                }

                                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Success_Insert, contentRootPath);
                                return RedirectToAction("Signup", new { notification = nvm });
                            }
                            nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Failed_Insert, contentRootPath);
                            return RedirectToAction("Signup", new { notification = nvm });
                        }
                        else
                        {
                            nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Failed_Insert, contentRootPath);
                            return RedirectToAction("Signup", new { notification = nvm });
                        }
                    }
                    else
                    {
                        nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Failed_Operation, contentRootPath);
                        return RedirectToAction("Signup", new { notification = nvm });
                    }
                }
            }
            catch (Exception ex)
            {
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Failed_Operation, contentRootPath);
                return RedirectToAction("Signup", new { notification = nvm });
            }
        }//end SignupConfirm

        public ViewResult UserList(string notification)
        {
            var users = _db.Users.Include(x => x.UserImage).ToList();
            if (notification != null)
            {
                ViewData["nvm"] = NotificationHandler.DeserializeMessage(notification);
            }
            return View(users);
        }//end UserList

        public async Task<IActionResult> EditUser(string UserId, string notification)
        {
            var selectedUser = await _userManager.FindByIdAsync(UserId);
            if (selectedUser != null)
            {
                if (notification != null)
                {
                    ViewData["nvm"] = NotificationHandler.DeserializeMessage(notification);
                }
                var user = _db.Users.Where(x => x.Id == selectedUser.Id).Include(x => x.UserImage).FirstOrDefault();
                ViewData["selectedUser"] = user;
                return View();
            }
            var nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Failed_Update, contentRootPath);
            return RedirectToAction("Signin", new { notification = nvm });
        }//end EditUser

        public async Task<IActionResult> EditUserConfirm(SignupViewModel model, List<IFormFile> img, string Id)
        {
            string nvm;
            var user = await _userManager.FindByIdAsync(Id);
            var userModifier = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Record_Not_Exist, contentRootPath);
                return RedirectToAction("EditUser", new { notification = nvm });
            }
            //Get Backup From The User Into 'UserModified' Table
            var userJSONForBackup = JsonConvert.SerializeObject(user);
            
            UserModified UM = new UserModified()
            {
                LastUserBackupJson = userJSONForBackup,
                ModifedByUserId = userModifier.Id,
                UserId = user.Id,
                Comment = "Edited by " + userModifier.UserName
            };
            _dbUserModified.Insert(UM);

            var greDate = CustomizeCalendar.PersianToGregorian(model.Dateofbirth ?? DateTime.Now);
            using (var transaction = _db.Database.BeginTransaction())
            {
                user.FirstName = model.Firstname;
                user.LastName = model.Lastname;
                user.Email = model.Email;
                user.PhoneNumber = model.Phonenumber;
                user.SpecialUser = model.Specialuser;
                user.Status = model.Status;
                user.DateOfBirth = greDate;
                if (model.Gender == true)
                {
                    user.Gendre = 1; //male
                }
                else if (model.Gender == false)
                {
                    user.Gendre = 2; //female
                }
                else
                {
                    user.Gendre = null; //not specified
                }
                //--------------------------------
                //disable the last images of the user
                var doesUserHaveImage = _db.UserImage.Where(x => x.UserId == Id).ToList();
                if (doesUserHaveImage.Count > 0)
                {
                    foreach (var item in doesUserHaveImage)
                    {
                        var userOldImage = _db.UserImage.FirstOrDefault();
                        if (userOldImage != null)
                        {
                            userOldImage.Status = false;
                        }
                        _dbUserImage.Update(userOldImage); //needs repository
                    }
                }
                //--------------------------------
                //add new image of the user
                UserImage userImage = new UserImage()
                {
                    UserId = Id,
                    Caption = model.Lastname + "_" + DateTime.Now
                };
                img.ForEach(x =>
                {
                    if (x != null)
                    {
                        byte[] b = new byte[x.Length];
                        x.OpenReadStream().Read(b, 0, b.Length);
                        userImage.Image = b;

                        //Make Thumbnail
                        MemoryStream mem1 = new MemoryStream(b);
                        Image img2 = Image.FromStream(mem1);
                        Bitmap bmp = new Bitmap(img2, 120, 120);
                        MemoryStream mem2 = new MemoryStream();
                        bmp.Save(mem2, System.Drawing.Imaging.ImageFormat.Jpeg);
                        userImage.ImageThumbnail = mem2.ToArray();
                    }
                });
                _dbUserImage.Insert(userImage);
                //--------------------------------
                var status = await _userManager.UpdateAsync(user);
                if (status.Succeeded)
                {
                    transaction.Commit(); //saves the database

                    nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Success_Insert, contentRootPath);
                    return RedirectToAction("EditUser", new { notification = nvm });
                }
            }

            return View();
        }
    }
}