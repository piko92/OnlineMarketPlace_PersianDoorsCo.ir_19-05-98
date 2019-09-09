﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
            //var dbViewModel = dbCategory.GetAll();
            var dbViewModel = dbCategory.GetInclude(e => e.Field);
            return View(dbViewModel);
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
        public IActionResult InsertBrandConfirm(BrandViewModel model)
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
            var dbViewModel = dbProductAbstract.GetInclude(e => e.Brand, e => e.Category, e => e.ProductImage);
            return View(dbViewModel);
        }
        public IActionResult InsertProduct()
        {
            ViewData["Brand"] = dbBrand.GetAll();
            ViewData["Category"] = dbCategory.GetAll();
            return View();
        }
        public IActionResult InsertProductConfirm(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                ProductAbstract productAbstract = new ProductAbstract()
                {
                    Name = model.Name,
                    BrandId = model.BrandI,
                    CategoryId = model.CategoryId,
                    Status = model.Status,

                };
                List<ProductImage> productImages = new List<ProductImage>();
                foreach (var item in model.img)
                {
                    byte[] b = new byte[item.Length];
                    item.OpenReadStream().Read(b, 0, (int)item.Length);
                    //Thumbnail
                    MemoryStream mem1 = new MemoryStream(b);
                    Image img = Image.FromStream(mem1);
                    Bitmap bmp = new Bitmap(img, 300, 300);
                    MemoryStream mem2 = new MemoryStream();
                    bmp.Save(mem2, System.Drawing.Imaging.ImageFormat.Jpeg);
                    ProductImage productImage = new ProductImage()
                    {
                        Image = b,
                        ImageThumbnail = mem2.ToArray()
                    };

                    productImages.Add(productImage);
                }
                productAbstract.ProductImage = productImages;
                dbProductAbstract.Insert(productAbstract);
                TempData["InsertConfirm"] = "محصول با موفقیت ثبت شد";
                return RedirectToAction("ShowProduct");
            }
            return RedirectToAction("InsertProduct");
        }
        public IActionResult DeleteProduct(int Id)
        {
            var status = dbProductAbstract.DeleteById(Id);
            if (status)
            {

            }
            else
            {

            }
            return RedirectToAction("ShowProduct");
        }
        public IActionResult EditProduct(int Id)
        {
            ViewData["Brand"] = dbBrand.GetAll();
            ViewData["Category"] = dbCategory.GetAll();
            ViewData["ProductAbstract"] = dbProductAbstract.GetIncludeById(Id, e => e.Brand, e => e.Category, e => e.ProductImage);
            //var dbViewModel = dbProductAbstract.GetInclude(e => e.Brand, e => e.Category, e => e.ProductImage);
            return View();
        }
        public IActionResult EditProductConfirm(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                ProductAbstract productAbstract = new ProductAbstract()
                {
                    Name = model.Name,
                    BrandId = model.BrandI,
                    CategoryId = model.CategoryId,
                    Status = model.Status,

                };
                List<ProductImage> productImages = new List<ProductImage>();
                foreach (var item in model.img)
                {
                    byte[] b = new byte[item.Length];
                    item.OpenReadStream().Read(b, 0, (int)item.Length);
                    //Thumbnail
                    MemoryStream mem1 = new MemoryStream(b);
                    Image img = Image.FromStream(mem1);
                    Bitmap bmp = new Bitmap(img, 300, 300);
                    MemoryStream mem2 = new MemoryStream();
                    bmp.Save(mem2, System.Drawing.Imaging.ImageFormat.Jpeg);
                    ProductImage productImage = new ProductImage()
                    {
                        Image = b,
                        ImageThumbnail = mem2.ToArray()
                    };

                    productImages.Add(productImage);
                }
                productAbstract.ProductImage = productImages;
                dbProductAbstract.Insert(productAbstract);
                TempData["InsertConfirm"] = "محصول با موفقیت ثبت شد";
                return RedirectToAction("ShowProduct");
            }
            return RedirectToAction("InsertProduct");
        }
        
        //Brand--End
        //Test--Start
        public IActionResult FindById(int id)
        {
            var entity = dbProductAbstract.FindById(id);
            if (entity != null)
            {
                return Json(new { errMsg = entity.Name.ToString() });
            }
            else
            {
                return Json(new { errMsg = false });
            }
        }
        public IActionResult DeleteById(int id)
        {
            var status = dbProductAbstract.DeleteById(id);
            return Json(new { errMsg = status });
        }
        public IActionResult UpdateEntity(int id, string name)
        {
            var entity = dbProductAbstract.FindById(id);
            if (entity != null)
            {
                entity.Name = name;
                var status = dbProductAbstract.Update(entity);
                return Json(new { errMsg = status });
            }
            else
            {
                return Json(new { errMsg = false });
            }
        }
        //Test--End
    }
}