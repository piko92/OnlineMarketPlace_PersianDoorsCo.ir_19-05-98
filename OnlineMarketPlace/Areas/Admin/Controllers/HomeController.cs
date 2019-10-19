using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
    public class HomeController : Controller
    {
        #region Inject
        //Inject DataBase--Start
        UserManager<ApplicationUser> userManager;
        DbRepository<OnlineMarketContext, ProductFeature, int> dbProductFeature;
        DbRepository<OnlineMarketContext, Category, int> dbCategory;
        DbRepository<OnlineMarketContext, ContactUs, int> dbContactUs;
        DbRepository<OnlineMarketContext, Article, int> dbArticle;
        DbRepository<OnlineMarketContext, Invoice, int> dbInvoice;
        DbRepository<OnlineMarketContext, GeneralPage, int> dbGeneralPage;
        private readonly IHostingEnvironment hostingEnvironment;
        private IConfiguration configuration;
        string contentRootPath;
        public HomeController
            (
                UserManager<ApplicationUser> _userManager,
                DbRepository<OnlineMarketContext, ProductFeature, int> _dbProductFeature,
                DbRepository<OnlineMarketContext, Category, int> _dbCategory,
                DbRepository<OnlineMarketContext, ContactUs, int> _dbContactUs,
                DbRepository<OnlineMarketContext, Article, int> _dbArticle,
                DbRepository<OnlineMarketContext, Invoice, int> _dbInvoice,
                DbRepository<OnlineMarketContext, GeneralPage, int> _dbGeneralPage,
                IHostingEnvironment _hostingEnvironment,
                IConfiguration _configuration
            )
        {
            userManager = _userManager;
            dbProductFeature = _dbProductFeature;
            dbCategory = _dbCategory;
            dbContactUs = _dbContactUs;
            dbArticle = _dbArticle;
            dbInvoice = _dbInvoice;
            dbGeneralPage = _dbGeneralPage;
            hostingEnvironment = _hostingEnvironment;
            contentRootPath = hostingEnvironment.ContentRootPath;
            configuration = _configuration;
        }
        //Inject DataBase--End
        #endregion

        #region Index
        public IActionResult Index(string notification)
        {
            ViewData[" userManagerCount"] = userManager.Users.Where(x => x.Status == true).Count();
            ViewData[" ProductFeatureCount"] = dbProductFeature.GetAll().Where(x => x.Status == true).Count();
            ViewData[" CategoryCount"] = dbCategory.GetAll().Where(x => x.Status == true).Count();
            ViewData[" ContactUsCount"] = dbContactUs.GetAll().Count();
            ViewData[" ArticleCount"] = dbArticle.GetAll().Where(x => x.Status == true).Count();
            ViewData[" PaidInvoiceCount"] = dbInvoice.GetAll().Where(x => x.IsPaid == true).Count();
            if (notification != null)
            {
                ViewData["nvm"] = NotificationHandler.DeserializeMessage(notification);
            }
            return View();
        }
        public IActionResult Editor()
        {
            return View();
        }
        public IActionResult Summernote()
        {
            return View();
        }
        public IActionResult EditorConfirm(string txt1)
        {
            return View();
        }
        #endregion

        #region About

        public IActionResult About(string notification)
        {
            var entity = dbGeneralPage.GetAll().Where(e => e.Title == "AboutUs").FirstOrDefault();
            if (entity == null)
            {
                GeneralPage generalPage = new GeneralPage()
                {
                    Title = "AboutUs"
                };
                dbGeneralPage.Insert(generalPage);
            }
            ViewData["AboutUs"] = dbGeneralPage.GetAll().Where(e => e.Title == "AboutUs").FirstOrDefault();
            return View();
        }
        public IActionResult InsertAboutConfirm(GeneralPageViewModel model)
        {
            string nvm;
            if (ModelState.IsValid == false)
            {
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Wrong_Values, contentRootPath);
                return RedirectToAction("InsertPost", new { notification = nvm });
            }

            byte[] b = null;
            var entity = dbGeneralPage.GetAll().Where(e => e.Title == "AboutUs").FirstOrDefault();
            if (entity != null)
            {
                entity.ContentHtml = model.ContentHtml;
                entity.Summary = model.Summary;
                entity.Status = model.Status;
            }
            if (model.MainImage != null)
            {
                b = new byte[model.MainImage.Length];
                model.MainImage.OpenReadStream().Read(b, 0, (int)model.MainImage.Length);
                entity.MainImage = b;
            }
            try
            {
                dbGeneralPage.Update(entity);
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Success_Update, contentRootPath);
                return RedirectToAction("Index", new { notification = nvm });
            }
            catch (Exception)
            {
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Failed_Operation, contentRootPath);
                return RedirectToAction("Index", new { notification = nvm });
            }

        }
        #endregion

        #region General Page
        public IActionResult ShowGeneralPage(string notification)
        {
            List<GeneralPage> viewModel = dbGeneralPage.GetAll().Where(e => e.Title != "AboutUs").ToList();
            //var viewModel = dbGeneralPage.GetAll() ;
            if (notification != null)
            {
                ViewData["nvm"] = NotificationHandler.DeserializeMessage(notification);
                return View(viewModel);
            }
            return View(viewModel);
        }
        public IActionResult InsertGeneralPage(string notification)
        {
            return View();
        }
        [DisableRequestSizeLimit]
        public async Task<IActionResult> InsertGeneralPageConfirm(GeneralPageViewModel model)
        {
            string nvm;
            if (ModelState.IsValid == false)
            {
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Wrong_Values, contentRootPath);
                return RedirectToAction("ShowGeneralPage", new { notification = nvm });
            }
            GeneralPage generalPage = new GeneralPage()
            {
                Title = model.Title,
                Description = model.Description,
                RegdDateTime = DateTime.Now,
                ContentHtml = model.ContentHtml,
                ShowOrder = model.ShowOrder,
                Status = model.Status
            };
            try
            {
                //Save File in wwwroot
                string folderPath = "";
                string savePath = "";
                folderPath = configuration.GetSection("DefaultPaths").GetSection("PagesFiles").Value;
                savePath = await FileManager.ReadAndSaveFile(contentRootPath, folderPath, model.MainImage);
                generalPage.MainImagePath = savePath;
                if (model.MovieFile != null)
                {
                    folderPath = configuration.GetSection("DefaultPaths").GetSection("PagesFiles").Value;
                    savePath = await FileManager.ReadAndSaveFile(contentRootPath, folderPath, model.MovieFile);
                    generalPage.MoviePath = savePath;
                }
                if (model.DocumentFile != null)
                {
                    folderPath = configuration.GetSection("DefaultPaths").GetSection("PagesFiles").Value;
                    savePath = await FileManager.ReadAndSaveFile(contentRootPath, folderPath, model.DocumentFile);
                    generalPage.DocumentPath = savePath;
                }
                dbGeneralPage.Insert(generalPage);
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Success_Insert, contentRootPath);
                return RedirectToAction("ShowGeneralPage", new { notification = nvm });
            }
            catch (Exception)
            {
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Failed_Insert, contentRootPath);
                return RedirectToAction("ShowGeneralPage", new { notification = nvm });
            }

        }

        public IActionResult DeleteGeneralPage(int Id)
        {
            string nvm;
            var entity = dbGeneralPage.FindById(Id);
            try
            {
                if (entity.MainImagePath != null)
                {
                    bool imgDel = FileManager.DeleteFile(contentRootPath, entity.MainImagePath);
                }
                dbGeneralPage.DeleteById(Id);
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Success_Remove, contentRootPath);
                return Json(nvm);
            }
            catch (Exception)
            {
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Failed_Remove, contentRootPath);
                return Json(nvm);
            }
        }
        public IActionResult EditGeneralPage(int Id)
        {
            ViewData["GeneralPage"] = dbGeneralPage.FindById(Id);
            return View();
        }
        public async Task<IActionResult> EditGeneralPageConfirm(GeneralPageViewModel model)
        {
            string nvm;
            if (ModelState.IsValid == false)
            {
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Wrong_Values, contentRootPath);
                return RedirectToAction("ShowGeneralPage", new { notification = nvm });
            }
            var entity = dbGeneralPage.FindById(model.Id);
            if (entity != null)
            {
                entity.Title = model.Title;
                entity.Description = model.Description;
                entity.RegdDateTime = DateTime.Now;
                entity.ContentHtml = model.ContentHtml;
                entity.ShowOrder = model.ShowOrder;
                entity.Status = model.Status;
            }
            //Delete Old Image then Insert New Image
            string folderPath;
            string savePath;
            if (model.MainImage != null)
            {
                if (entity.MainImagePath != null)
                {
                    bool imgDel = FileManager.DeleteFile(contentRootPath, entity.MainImagePath);
                }
                folderPath = configuration.GetSection("DefaultPaths").GetSection("PagesFiles").Value;
                savePath = await FileManager.ReadAndSaveFile(contentRootPath, folderPath, model.MainImage);
                entity.MainImagePath = savePath;
            }
            if (model.MovieFile != null)
            {
                if (entity.MoviePath != null)
                {
                    bool FileDel = FileManager.DeleteFile(contentRootPath, entity.MoviePath);
                }
                folderPath = configuration.GetSection("DefaultPaths").GetSection("PagesFiles").Value;
                savePath = await FileManager.ReadAndSaveFile(contentRootPath, folderPath, model.MovieFile);
                entity.MoviePath = savePath;
            }
            if (model.DocumentFile != null)
            {
                if (entity.DocumentPath != null)
                {
                    bool FileDel = FileManager.DeleteFile(contentRootPath, entity.DocumentPath);
                }
                folderPath = configuration.GetSection("DefaultPaths").GetSection("PagesFiles").Value;
                savePath = await FileManager.ReadAndSaveFile(contentRootPath, folderPath, model.DocumentFile);
                entity.DocumentPath = savePath;
            }
            try
            {
                dbGeneralPage.Update(entity);
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Success_Update, contentRootPath);
                return RedirectToAction("ShowGeneralPage", new { notification = nvm });
            }
            catch (Exception)
            {
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Failed_Update, contentRootPath);
                return RedirectToAction("ShowGeneralPage", new { notification = nvm });
            }
        }

        public  IActionResult DeleteFileByPath(string filePath)
        {
            string nvm;
            if (filePath==null)
            {
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Failed_Operation, contentRootPath);
                return Json(nvm);
            }
            var generalPage = dbGeneralPage.GetAll();
            try
            {
                var entityMovie = generalPage.Where(e => e.MoviePath == filePath);
                if (entityMovie.Count()>0)
                {
                    var _entityMovie = entityMovie.FirstOrDefault();
                    _entityMovie.MoviePath =null;
                    dbGeneralPage.Update(_entityMovie);
                    bool FileDel = FileManager.DeleteFile(contentRootPath, filePath);
                }
                var entityDocument = generalPage.Where(e => e.DocumentPath == filePath);
                if (entityDocument.Count()>0)
                {
                    var _entityDocument = entityDocument.FirstOrDefault();
                    _entityDocument.DocumentPath = null;
                    dbGeneralPage.Update(_entityDocument);
                    bool FileDel = FileManager.DeleteFile(contentRootPath, filePath);
                }
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Success_Remove, contentRootPath);
                return Json(nvm);
            }
            catch (Exception)
            {
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Failed_Remove, contentRootPath);
                return Json(nvm);
            }
           
        }

        #endregion
    }
}