using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineMarket.Models;
using OnlineMarketPlace.Areas.Identity.Data;
using OnlineMarketPlace.Models.AdminViewModels;
using OnlineMarketPlace.Repository;

namespace OnlineMarketPlace.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        //Inject DataBase--Start
        //Call DataBase Function From Repository
        DbRepository<OnlineMarketContext, Brand, int> dbBrand;
        DbRepository<OnlineMarketContext, Category, int> dbCategory;
        DbRepository<OnlineMarketContext, ProductAbstract, int> dbProductAbstract;
        public ProductController
            (
                DbRepository<OnlineMarketContext, Brand, int> _dbBrand,
                DbRepository<OnlineMarketContext, Category, int> _dbCategory,
                DbRepository<OnlineMarketContext, ProductAbstract, int> _dbProductAbstract
            )
        {
            dbBrand = _dbBrand;
            dbCategory = _dbCategory;
            dbProductAbstract = _dbProductAbstract;
        }
        //Inject DataBase--End
        public IActionResult Index()
        {
            return View();
        }
        //Category--Start
        public IActionResult ShowCategory()
        {
            return View(dbCategory.GetAll());
        }
        public IActionResult InsertCategory()
        {
            return View();
        }
        public IActionResult InsertConfirmCategory(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                Category category = new Category()
                {
                    Name = model.Name,
                    Description = model.Description
                };
                dbCategory.Insert(category);
                TempData["InsertConfirm"] = "دسته بندی با موفقیت ثبت شد";
                return RedirectToAction("ShowCategory");
            }
            return RedirectToAction("InsertCategory");

        }
        //Category--End
        //Brand--Start
        public IActionResult ShowBrand()
        {
            return View(dbBrand.GetAll());
        }
        public IActionResult InsertBrand()
        {
            return View();
        }
        public IActionResult InsertBrand(BrandViewModel model)
        {
            if (ModelState.IsValid)
            {
                Brand brand = new Brand()
                {
                    Name = model.Name,
                    Description = model.Description
                };
                dbBrand.Insert(brand);
                TempData["InsertConfirm"] = "برند با موفقیت ثبت شد";
                return RedirectToAction("ShowBrand");
            }
            return RedirectToAction("InsertBrand");

        }
        //Brand--End
        //Product--Start
        public IActionResult ShowProduct()
        {
            return View(dbProductAbstract.GetAll());
        }
        public IActionResult InsertProduct()
        {
            return View();
        }
        public IActionResult InsertProductConfirm(BrandViewModel model)
        {
            if (ModelState.IsValid)
            {
                ProductAbstract productAbstract = new ProductAbstract()
                {
                    Name = model.Name,
                    
                };
                dbProductAbstract.Insert(productAbstract);
                TempData["InsertConfirm"] = "محصول با موفقیت ثبت شد";
                return RedirectToAction("ShowProduct");
            }
            return RedirectToAction("InsertProduct");

        }
        //Brand--End
    }
}