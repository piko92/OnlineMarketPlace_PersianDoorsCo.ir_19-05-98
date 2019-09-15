using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OnlineMarket.Models;
using OnlineMarketPlace.Areas.Identity.Data;
using OnlineMarketPlace.ClassLibraries;
using OnlineMarketPlace.ClassLibraries.NotificationHandler;
using OnlineMarketPlace.Models.AdminViewModels;
using OnlineMarketPlace.Repository;

namespace OnlineMarketPlace.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperVisor")]
    public class GalleryController : Controller
    {
        #region Inject
        //Inject DataBase--Start
        UserManager<ApplicationUser> _userManager;

        DbRepository<OnlineMarketContext, Subject, int> dbSubject;
        DbRepository<OnlineMarketContext, PhotoGallery, int> dbPhotoGallery;
        DbRepository<OnlineMarketContext, ScreenResulation, int> dbScreen;
        DbRepository<OnlineMarketContext, Category, int> dbCategory;
        DbRepository<OnlineMarketContext, Brand, int> dbBrand;
        DbRepository<OnlineMarketContext, TopSlider, int> dbTopSlider;

        private readonly IHostingEnvironment _hostingEnvironment; //returns the hosting environment data
        private IConfiguration _configuration;
        string contentRootPath;
        public GalleryController
            (
                UserManager<ApplicationUser> userManager,
                DbRepository<OnlineMarketContext, Subject, int> _dbSubject,
                DbRepository<OnlineMarketContext, PhotoGallery, int> _dbPhotoGallery,
                DbRepository<OnlineMarketContext, ScreenResulation, int> _dbScreen,
                DbRepository<OnlineMarketContext, Category, int> _dbCategory,
                DbRepository<OnlineMarketContext, Brand, int> _dbBrand,
                DbRepository<OnlineMarketContext, TopSlider, int> _dbTopSlider,
                IHostingEnvironment hostingEnvironment,
                IConfiguration configuration
            )
        {
            _userManager = userManager;

            dbSubject = _dbSubject;
            dbPhotoGallery = _dbPhotoGallery;
            dbScreen = _dbScreen;
            dbCategory = _dbCategory;
            dbBrand = _dbBrand;
            dbTopSlider = _dbTopSlider;

            _hostingEnvironment = hostingEnvironment;
            contentRootPath = _hostingEnvironment.ContentRootPath;
            _configuration = configuration;
        }
        //Inject DataBase--End
        #endregion
        #region Index
        public IActionResult Index()
        {
            return RedirectToAction("ShowPhoto");
        }
        #endregion
        #region Subject
        public IActionResult ShowSubject()
        {
            var dbViewModel = dbSubject.GetInclude(e => e.User);
            return View(dbViewModel);
        }
        public IActionResult InsertSubject()
        {
            return View();
        }
        public IActionResult InsertSubjectConfirm(SubjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                Subject subject = new Subject()
                {
                    Name = model.Name,
                    LatinName = model.LatinName,
                    Description = model.Description,
                    Status = model.Status
                };
                dbSubject.Insert(subject);
                TempData["InsertConfirm"] = "با موفقیت ثبت شد";
                return RedirectToAction("ShowSubject");
            }
            return RedirectToAction("InsertSubject");
        }
        public IActionResult DeleteSubject(int Id)
        {
            var status = dbSubject.DeleteById(Id);
            if (status)
            {

            }
            else
            {

            }
            return RedirectToAction("ShowSubject");
        }
        public IActionResult EditSubject(int Id)
        {
            ViewData["Subject"] = dbSubject.FindById(Id);
            return View();
        }
        public IActionResult EditSubjectConfirm(SubjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = dbSubject.FindById(model.Id);
                entity.Name = model.Name;
                entity.LatinName = model.LatinName;
                entity.Description = model.Description;
                entity.Status = model.Status;

                dbSubject.Update(entity);
                TempData["InsertConfirm"] = "با موفقیت ثبت شد";
                return RedirectToAction("ShowSubject");
            }
            return RedirectToAction("InsertSubject");
        }
        #endregion
        #region Photo
        public IActionResult ShowPhoto()
        {
            var dbViewModel = dbPhotoGallery.GetInclude(e => e.User);
            return View(dbViewModel);
        }
        public IActionResult InsertPhoto()
        {
            ViewData["Subject"] = dbSubject.GetAll();
            return View();
        }
        public IActionResult InsertPhotoConfirm(PhotoGalleryViewModel model)
        {
            if (ModelState.IsValid)
            {
                byte[] b = new byte[model.Image.Length];
                model.Image.OpenReadStream().Read(b, 0, (int)model.Image.Length);
                PhotoGallery photoGallery = new PhotoGallery()
                {
                    Name = model.Name,
                    LatinName = model.LatinName,
                    Status = model.Status,
                    Image=b,
                };
                dbPhotoGallery.Insert(photoGallery);
                TempData["InsertConfirm"] = "با موفقیت ثبت شد";
                return RedirectToAction("ShowPhoto");
            }
            return RedirectToAction("InsertPhoto");
        }
        public IActionResult DeletePhoto(int Id)
        {
            var status = dbPhotoGallery.DeleteById(Id);
            if (status)
            {

            }
            else
            {

            }
            return RedirectToAction("ShowPhoto");
        }
        public IActionResult EditPhoto(int Id)
        {
            ViewData["PhotoGallery"] = dbPhotoGallery.FindById(Id);
            ViewData["Subject"] = dbSubject.GetAll();

            return View();
        }
        public IActionResult EditPhotoConfirm(PhotoGalleryViewModel model)
        {
            var entity = dbPhotoGallery.FindById(model.Id);
            byte[] b = null;
            if (model.Image==null)
            {
                ModelState.Remove("Image");
                b = entity.Image;
            }
            else
            {
                b = new byte[model.Image.Length];
                model.Image.OpenReadStream().Read(b, 0, (int)model.Image.Length);
            }
            if (ModelState.IsValid)
            {
                entity.Name = model.Name;
                entity.LatinName = model.LatinName;
                entity.Status = model.Status;
                entity.SubjectId = model.SubjectId;
                entity.Image = b;

                dbPhotoGallery.Update(entity);
                TempData["InsertConfirm"] = "با موفقیت ثبت شد";
                return RedirectToAction("ShowPhoto");
            }
            return RedirectToAction("InsertPhoto");
        }
        #endregion
        #region Slider
        public ViewResult InsertSliderImage(string notification)
        {
            ViewData["screen"] = dbScreen.GetAll();
            ViewData["category"] = dbCategory.GetAll();
            ViewData["brand"] = dbBrand.GetAll();

            if (notification != null)
            {
                ViewData["nvm"] = NotificationHandler.DeserializeMessage(notification);
                return View();
            }
            return View();
        }

        public async Task<IActionResult> InsertSliderImageConfirm(TopSliderViewModel model)
        {
            string nvm;
            try
            {
                if (!ModelState.IsValid)
                {
                    nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Wrong_Values, contentRootPath);
                    return RedirectToAction("InsertSliderImage", new { notification = nvm });
                }
                if (model != null)
                {
                    var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
                    TopSlider TS = new TopSlider()
                    {
                        Title = model.Title,
                        Summery = model.Summery,
                        Description = model.Description,
                        AltName = model.AltName,
                        Link = model.Link,
                        ScreenResulationId = model.ScreenResulationId > 0 ? model.ScreenResulationId : null,
                        Status = model.Status,
                        Active = model.Active,
                        Priotity = model.Priotity,
                        SetForFuture = model.SetForFuture,
                        HasButton = model.HasButton,
                        UserId = currentUser.Id
                    };
                    if (model.ConnectedCategoryId > 0)
                    {
                        TS.ConnectedCategoryId = model.ConnectedCategoryId;
                    }
                    if (model.ConnectedBrandId > 0)
                    {
                        TS.ConnectedBrandId = model.ConnectedBrandId;
                    }
                    if (model.ConnectedProductId > 0)
                    {
                        TS.ConnectedProductId = model.ConnectedProductId;
                    }
                    if (model.ShowDateTime.ToString().Length > 1 && model.SetForFuture == true)
                    {
                        TS.ShowDateTime = CustomizeCalendar.PersianToGregorian(model.ShowDateTime ?? DateTime.Now);
                    }
                    if (model.ExpireDateTime.ToString().Length > 1 && model.SetForFuture == true)
                    {
                        TS.ExpireDateTime = CustomizeCalendar.PersianToGregorian(model.ExpireDateTime ?? DateTime.Now);
                    }
                    if (model.HasButton && model.ButtonContent != null)
                    {
                        TS.ButtonContent = model.ButtonContent;
                        TS.ButtonLink = model.ButtonLink;
                    }
                    
                    string folderPath = _configuration.GetSection("DefaultPaths").GetSection("SliderImage").Value;
                    string uploads = Path.Combine(contentRootPath, folderPath);
                    bool exists = System.IO.Directory.Exists(uploads);
                    if (!exists)
                    {
                        System.IO.Directory.CreateDirectory(uploads);
                    }

                    var files = HttpContext.Request.Form.Files;
                    foreach (var Image in files)
                    {
                        if (Image != null && Image.Length > 0)
                        {
                            var file = Image;

                            if (file.Length > 0)
                            {
                                var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                                using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                                {
                                    await file.CopyToAsync(fileStream);
                                    TS.ImagePath = folderPath + fileName;
                                }
                            }
                        }
                    }

                    dbTopSlider.Insert(TS); //save record successfully

                    nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Success_Insert, contentRootPath);
                    return RedirectToAction("InsertSliderImage", new { notification = nvm });
                }
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Failed_Insert, contentRootPath);
                return RedirectToAction("InsertSliderImage", new { notification = nvm });
            }
            catch(Exception ex)
            {
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Failed_Operation, contentRootPath);
                return RedirectToAction("InsertSliderImage", new { notification = nvm });
            }
        }//end InsertSliderImageConfirm

        public ViewResult InsertScreenResolution(string notification)
        {
            ViewData["Screen"] = dbScreen.GetAll();
            if (notification != null)
            {
                ViewData["nvm"] = NotificationHandler.DeserializeMessage(notification);
                return View();
            }
            return View();
        }//end InsertScreenResolution

        public IActionResult InsertScreenResolutionConfirm(ScreenResulation model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(null);
                }
                if (model != null)
                {
                    var entityId = dbScreen.Insert(model);
                    var entity = dbScreen.FindById(entityId);
                    return Json(entity);
                }
                return Json(null);
            }
            catch(Exception ex)
            {
                return Json(null);
            }
        }//end InsertScreenResolutionConfirm

        public IActionResult ListOfSliders()
        {
            
            return View();
        }
        #endregion
    }
}