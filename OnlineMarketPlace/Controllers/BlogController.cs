using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineMarket.Models;
using OnlineMarketPlace.Areas.Identity.Data;
using OnlineMarketPlace.Repository;

namespace OnlineMarketPlace.Controllers
{
    public class BlogController : Controller
    {
        #region Inject
        //Inject DataBase--Start
        DbRepository<OnlineMarketContext, Article, int> dbArticle;
        DbRepository<OnlineMarketContext, ProductAbstract, int> dbProductAbstract;
        DbRepository<OnlineMarketContext, UserArticleReview, int> dbUserArticleReview;
        DbRepository<OnlineMarketContext, Category, int> dbCategory;
        UserManager<ApplicationUser> userManager;
        public BlogController
            (
                DbRepository<OnlineMarketContext, Article, int> _dbArticle,
                DbRepository<OnlineMarketContext, ProductAbstract, int> _dbProductAbstract,
                DbRepository<OnlineMarketContext, UserArticleReview, int> _dbUserArticleReview,
                DbRepository<OnlineMarketContext, Category, int> _dbCategory,
                UserManager<ApplicationUser> _userManager

            )
        {
            dbArticle = _dbArticle;
            dbProductAbstract = _dbProductAbstract;
            dbUserArticleReview = _dbUserArticleReview;
            dbCategory = _dbCategory;
            userManager = _userManager;
        }
        //Inject DataBase--End
        #endregion
        #region Article
        public IActionResult Index()
        {
            ViewData["Article"] = dbArticle.GetAll().OrderByDescending(e => e.WrittenDateTime).ToList(); 
            ViewData["Category"] = dbCategory.GetAll();
            var dbViewModel = dbArticle.GetAll().OrderByDescending(e => e.WrittenDateTime).ToList(); 
            return View(dbViewModel);
        }
        [Route("Blog/{id}/{ArticleTitle}")]
        public IActionResult ShowArticle(int Id)
        {
            ViewData["Article"] = dbArticle.GetAll().OrderByDescending(e=>e.WrittenDateTime).ToList();
            ViewData["Category"] = dbCategory.GetAll();
            var dbViewModel = dbArticle.GetInclude(e=>e.UserArticleReview).Where(e=>e.Id==Id).FirstOrDefault();
            return View(dbViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> InsertArticleComment(int artileId,string name, string email, string comment)
        {
            if (artileId==null || name==null  || comment == null)
            {
             return   Json(new { status = false });
            }
            UserArticleReview userArticleReview = new UserArticleReview()
            {
                FakeUserName = name,
                AnonymousUserEmail = email,
                Comment = comment,
                ArticleId = artileId,
                RegDateTime = DateTime.Now
            };
            if (User.Identity.IsAuthenticated)
            {
                var userId = await userManager.FindByNameAsync(User.Identity.Name);
                userArticleReview.UserId = userId.Id;
            }
            dbUserArticleReview.Insert(userArticleReview);
            return Json(new { status = true });
        }

        #endregion



    }
}