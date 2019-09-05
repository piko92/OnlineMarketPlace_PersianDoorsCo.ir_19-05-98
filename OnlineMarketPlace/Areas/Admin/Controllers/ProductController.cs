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
        //Call DataBase Function From Repository
        DbRepository<OnlineMarketContext, Material, int> dbMaterial;
        DbRepository<OnlineMarketContext, Category, int> dbCategory;
        public ProductController
            (
                DbRepository<OnlineMarketContext, Material, int> _dbMaterial,
                DbRepository<OnlineMarketContext, Category, int> _dbCategory
            )
        {
            dbMaterial = _dbMaterial;
            dbCategory = _dbCategory;
        }


        public IActionResult Index()
        {
            return View(dbMaterial.GetAll());
        }
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
                TempData["InsertConfirm"]= "دسته بندی با موفقیت ثبت شد";
                return RedirectToAction("ShowCategory");
            }
                return RedirectToAction("InsertCategory");
            
        }
    }
}