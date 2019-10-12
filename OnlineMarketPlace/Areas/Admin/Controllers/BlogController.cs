using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineMarket.Models;
using OnlineMarketPlace.Areas.Identity.Data;
using OnlineMarketPlace.ClassLibraries.NotificationHandler;
using OnlineMarketPlace.Models.AdminViewModels;
using OnlineMarketPlace.Repository;

namespace OnlineMarketPlace.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperVisor")]

    public class BlogController : Controller
    {
        //Inject DataBase--Start
        DbRepository<OnlineMarketContext, Article, int> dbArticle;
        DbRepository<OnlineMarketContext, ProductAbstract, int> dbProductAbstract;
        DbRepository<OnlineMarketContext, UserArticleReview, int> dbUserArticleReview;
        UserManager<ApplicationUser> userManager;
        private readonly IHostingEnvironment hostingEnvironment;
        string contentRootPath;
        public BlogController
            (
                DbRepository<OnlineMarketContext, Article, int> _dbArticle,
                DbRepository<OnlineMarketContext, ProductAbstract, int> _dbProductAbstract,
                DbRepository<OnlineMarketContext, UserArticleReview, int> _dbUserArticleReview,
                UserManager<ApplicationUser> _userManager,
                IHostingEnvironment _hostingEnvironment
            )
        {
            dbArticle = _dbArticle;
            dbProductAbstract = _dbProductAbstract;
            dbUserArticleReview = _dbUserArticleReview;
            hostingEnvironment = _hostingEnvironment;
            contentRootPath = hostingEnvironment.ContentRootPath;
            userManager = _userManager;
        }
        //Inject DataBase--End
        public IActionResult Index()
        {
            return View();
        }
        //Post-Start
        public IActionResult ShowPost(string notification)
        {
            var dbViewModel = dbArticle.GetInclude(e=>e.User);
            if (notification != null)
            {
                ViewData["nvm"] = NotificationHandler.DeserializeMessage(notification);
                return View(dbViewModel);
            }
            return View(dbViewModel);
        }
        public IActionResult InsertPost(string notification)
        {
            ViewData["Product"] = dbProductAbstract.GetAll();
            if (notification != null)
            {
                ViewData["nvm"] = NotificationHandler.DeserializeMessage(notification);
                return View();
            }
            return View();
        }
        public async Task<IActionResult> InsertPostConfirm(ArticleViewModel model)
        {
            string nvm;
            var currentUser = await userManager.FindByNameAsync(User.Identity.Name);
            byte[] b = null;
            if (ModelState.IsValid == false)
            {
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Wrong_Values, contentRootPath);
                return RedirectToAction("InsertPost", new { notification = nvm });
            }
            if (ModelState.IsValid)
            {
                if (model.MainImage != null)
                {
                    b = new byte[model.MainImage.Length];
                    model.MainImage.OpenReadStream().Read(b, 0, (int)model.MainImage.Length);
                }
                Article article = new Article()
                {
                    Title = model.Title,
                    LatinTitle = model.LatinTitle,
                    Summery = model.Summery,
                    ContentHtml = model.ContentHtml,
                    Status = model.Status,
                    RelatedProductId = model.RelatedProductId,
                    UserId = currentUser.Id,
                    MainImage = b,
                };
                try
                {
                    dbArticle.Insert(article);
                    nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Success_Insert, contentRootPath);
                    return RedirectToAction("ShowPost", new { notification = nvm });
                }
                catch (Exception)
                {

                }

            }
            nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Failed_Operation, contentRootPath);
            return RedirectToAction("ShowPost", new { notification = nvm });
        }
        public IActionResult DeletePost(int Id)
        {
            string nvm;
            try
            {
                var status = dbArticle.DeleteById(Id);
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Success_Remove, contentRootPath);
                return Json(nvm);
            }
            catch (Exception)
            {
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Failed_Remove, contentRootPath);
                return Json(nvm);
            }

        }
        public IActionResult EditPost(int Id)
        {
            ViewData["dbArticle"] = dbArticle.FindById(Id);
            ViewData["Product"] = dbProductAbstract.GetAll();
            return View();
        }
        public async Task<IActionResult> EditPostConfirm(ArticleViewModel model)
        {
            string nvm;
            var currentUser = await userManager.FindByNameAsync(User.Identity.Name);
            byte[] b = null;
            if (ModelState.IsValid == false)
            {
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Wrong_Values, contentRootPath);
                return RedirectToAction("InsertPost", new { notification = nvm });
            }
            if (ModelState.IsValid)
            {
                Article entity = dbArticle.FindById(model.Id);
                if (entity != null)
                {
                    entity.Title = model.Title;
                    entity.LatinTitle = model.LatinTitle;
                    entity.Summery = model.Summery;
                    entity.ContentHtml = model.ContentHtml;
                    entity.Status = model.Status;
                    entity.RelatedProductId = model.RelatedProductId;
                    entity.UserId = currentUser.Id;
                }
                if (model.MainImage != null)
                {
                    b = new byte[model.MainImage.Length];
                    model.MainImage.OpenReadStream().Read(b, 0, (int)model.MainImage.Length);
                    entity.MainImage = b;
                }
                try
                {
                
                    dbArticle.Update(entity);
                    nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Success_Update, contentRootPath);
                    return RedirectToAction("ShowPost", new { notification = nvm });
                }
                catch (Exception)
                {

                }
            }
            nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Failed_Operation, contentRootPath);
            return RedirectToAction("ShowPost", new { notification = nvm });
        }
        //Post-End
        #region ArticleComment
        //Comment-Start
        public IActionResult ShowComment()
        {
            var dbViewModel = dbUserArticleReview.GetInclude(e => e.Article);
            return View(dbViewModel);
        }
        //Comment-End
        #endregion



    }
}