using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineMarket.Models;
using OnlineMarketPlace.Areas.Identity.Data;
using OnlineMarketPlace.Repository;

namespace OnlineMarketPlace.Controllers
{
    public class BlogController : Controller
    {
        #region Injext
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
        #endregion
        #region Article
        public IActionResult Index()
        {
            ViewData["Article"] = dbArticle.GetAll();
            var dbViewModel=  dbArticle.GetAll();
            return View(dbViewModel);
        }
        public IActionResult ShowArticle(int Id)
        {
            ViewData["Article"]= dbArticle.GetAll();
            var dbViewModel = dbArticle.FindById(Id);
            return View(dbViewModel);
        }
        #endregion



    }
}