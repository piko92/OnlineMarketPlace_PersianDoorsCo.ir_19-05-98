using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineMarket.Models;
using OnlineMarketPlace.Areas.Identity.Data;
using OnlineMarketPlace.ClassLibraries;
using OnlineMarketPlace.ClassLibraries.Pagination;
using OnlineMarketPlace.Repository;

namespace OnlineMarketPlace.Controllers
{
    //صرفا جهت تست و نمایش قالب ایجاد گردیده
    public class ProductController : Controller
    {
        #region Inject
        //Inject DataBase--Start
        UserManager<ApplicationUser> _userManager;
        DbRepository<OnlineMarketContext, ProductAbstract, int> _dbProduct;
        DbRepository<OnlineMarketContext, Category, int> dbCategory;
        OnlineMarketContext _db;
        public ProductController
            (
                UserManager<ApplicationUser> userManager,
                DbRepository<OnlineMarketContext, ProductAbstract, int> dbProduct,
                DbRepository<OnlineMarketContext, Category, int> _dbCategory,
                OnlineMarketContext db
            )
        {
            _userManager = userManager;
            _dbProduct = dbProduct;
            dbCategory = _dbCategory;
            _db = db;
        }
        //Inject DataBase--End
        #endregion
        public IActionResult Search(
            string name,
            int categoryId = 1,
            int pageNumber = 1, 
            int pageSize = 12
            )
        {
            //تست
            ViewData["dbCategory"] = dbCategory.GetAll().Where(e => e.Status == true).ToList();
            if (name != null)
            {
                var products = _db.ProductAbstract
                    .Include(x => x.Category)
                    .Include(x => x.Brand)
                    .Include(x => x.ProductImage)
                    .Include(x => x.ProductFeature)
                    .Where(x =>
                        x.Name.Contains(name) || x.LatinName.ToLower().Contains(name.ToLower()) ||
                        x.Category.Name.Contains(name) ||
                        x.Brand.Name.Contains(name)
                    )
                    .OrderByDescending(x => x.RegDateTime);

                var finalResult = PagedResult<ProductAbstract>.GetPaged(products, pageNumber, pageSize);
                ViewData["pagenumber"] = pageNumber;
                ViewData["pagesize"] = pageSize;
                ViewData["totalRecords"] = products.Count();
                ViewData["searchedName"] = name;


                var result = finalResult.Results.ToList();
                
                return View(result);
            }
            else
            {
                //********************* need to make STORED PROCEDURE *********************
                //this is just for test
                var products = _db.ProductAbstract
                    .Include(x => x.ProductFeature)
                    .Include(x => x.ProductImage)
                    .Include(x => x.Category)
                    .OrderByDescending(x => x.RegDateTime);

                var finalResult = PagedResult<ProductAbstract>.GetPaged(products, pageNumber, pageSize);
                ViewData["pagenumber"] = pageNumber;
                ViewData["pagesize"] = pageSize;
                ViewData["totalRecords"] = products.Count();
                var result = finalResult.Results.ToList();

                return View(result);
            }
        }

        public IActionResult _PartialSearch(string name)
        {
            if (name != null)
            {
                var foundProduct = _db.ProductAbstract
                    .Include(x => x.Category)
                    .Include(x => x.Brand)
                    .Where(x => x.Name.Contains(name) ||
                        x.LatinName.ToLower().Contains(name.ToLower()) ||
                        x.Category.Name.Contains(name)
                    ).ToList();
                ViewData["foundProduct"] = foundProduct;
                return View();
            }
            return null;
        }//end _PartialSearch


        [Route("Product/{id}/{productName}")]
        public IActionResult SingleProduct(int id)
        {
            try
            {
                string productName;
                if (id > 0)
                {
                    var product = _db.ProductAbstract.Where(x => x.Id == id && x.Status == true)
                        .Include(x => x.ProductFeature)
                        .Include(x => x.ProductImage)
                        .Include(x => x.Category)
                        .Include(x => x.ProductDescription).FirstOrDefault();
                    if (product != null)
                    {
                        productName = Tools.LinkCorrection(product.Name);
                        var relatedProducts = _db.ProductAbstract
                            .Where(x => x.Category == product.Category)
                            .Include(x => x.ProductFeature)
                            .Include(x => x.ProductImage)
                            .Include(x => x.Category)
                            .OrderByDescending(x => x.RegDateTime).Take(8)
                            .Take(8).ToList();

                        ViewData["relatedProducts"] = relatedProducts;
                        return View(product);
                    }
                }
                return RedirectToAction("Search");
            }
            catch (Exception )
            {
                return RedirectToAction("Search");
            }
            
        }//end SingleProduct
    }
}