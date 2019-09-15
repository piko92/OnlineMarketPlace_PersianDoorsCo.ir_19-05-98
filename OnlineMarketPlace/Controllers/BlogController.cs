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
        public BlogController
            (
                DbRepository<OnlineMarketContext, Article, int> _dbArticle,
                DbRepository<OnlineMarketContext, ProductAbstract, int> _dbProductAbstract
            )
        {
            dbArticle = _dbArticle;
            dbProductAbstract = _dbProductAbstract;
        }
        //Inject DataBase--End
        #endregion
        #region Injext
        public IActionResult Index()
        {
            var dbViewModel=  dbArticle.GetAll();
            return View(dbViewModel);
            //return View();
        }
        #endregion

    }
}