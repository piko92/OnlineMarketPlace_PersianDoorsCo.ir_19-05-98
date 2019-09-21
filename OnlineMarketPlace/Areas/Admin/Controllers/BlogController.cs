using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineMarket.Models;
using OnlineMarketPlace.Areas.Identity.Data;
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
        public BlogController
            (
                DbRepository<OnlineMarketContext, Article, int> _dbArticle,
                DbRepository<OnlineMarketContext, ProductAbstract, int> _dbProductAbstract,
             DbRepository<OnlineMarketContext, UserArticleReview, int> _dbUserArticleReview
            )
        {
            dbArticle = _dbArticle;
            dbProductAbstract = _dbProductAbstract;
            dbUserArticleReview = _dbUserArticleReview;

        }
        //Inject DataBase--End
        public IActionResult Index()
        {
            return View();
        }
        //Post-Start
        public IActionResult ShowPost()
        {
            var dbViewModel = dbArticle.GetAll();
            return View(dbViewModel);
        }
        public IActionResult InsertPost()
        {
            ViewData["Product"] = dbProductAbstract.GetAll();
            return View();
        }
        public IActionResult InsertPostConfirm(ArticleViewModel model)
        {
            byte[] b=null;
            if (ModelState.IsValid)
            {
                if (model.MainImage!=null)
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
                    RelatedProductId=model.RelatedProductId,
                    MainImage=b,
                };
                dbArticle.Insert(article);
                TempData["InsertConfirm"] = "مقاله با موفقیت ثبت شد";
                return RedirectToAction("ShowPost");
            }
            return RedirectToAction("InsertPost");
        }
        public IActionResult DeletePost(int Id)
        {
            var status = dbArticle.DeleteById(Id);
            if (status)
            {

            }
            else
            {

            }
            return RedirectToAction("ShowPost");
        }
        public IActionResult EditPost(int Id)
        {
            ViewData["Article"] = dbArticle.GetAll();
            ViewData["ProductAbstract"] = dbProductAbstract.GetAll();
            return View();
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