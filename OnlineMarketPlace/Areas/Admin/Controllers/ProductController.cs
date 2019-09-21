using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OnlineMarket.Models;
using OnlineMarketPlace.Areas.Identity.Data;
using OnlineMarketPlace.ClassLibraries;
using OnlineMarketPlace.ClassLibraries.NotificationHandler;
using OnlineMarketPlace.Models.AdminViewModels;
using OnlineMarketPlace.Repository;
using OnlineMarketPlace.Repository.Extension;

namespace OnlineMarketPlace.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperVisor")]

    public class ProductController : Controller
    {
        #region Inject
        //Inject DataBase--Start
        OnlineMarketContext _db;
        UserManager<ApplicationUser> userManager;
        DbRepository<OnlineMarketContext, Brand, int> dbBrand;
        DbRepository<OnlineMarketContext, Category, int> dbCategory;
        //DBRepositoryEx<OnlineMarketContext, Category, string> dbCategoryEx;

        DbRepository<OnlineMarketContext, ProductAbstract, int> dbProductAbstract;
        DbRepository<OnlineMarketContext, AdditionalFeatures, int> dbAdditionalFeatures;
        DbRepository<OnlineMarketContext, ProductAdditionalFeatures, int> dbProductAdditionalFeatures;
        DbRepository<OnlineMarketContext, ProductFeature, int> dbProductFeature;

        private readonly IHostingEnvironment hostingEnvironment;
        private IConfiguration _configuration;
        string contentRootPath;
        public ProductController
            (
                UserManager<ApplicationUser> _userManager,
                DbRepository<OnlineMarketContext, Brand, int> _dbBrand,
                DbRepository<OnlineMarketContext, Category, int> _dbCategory,
                DbRepository<OnlineMarketContext, ProductAbstract, int> _dbProductAbstract,
                DbRepository<OnlineMarketContext, AdditionalFeatures, int> _dbAdditionalFeatures,
                DbRepository<OnlineMarketContext, ProductAdditionalFeatures, int> _dbProductAdditionalFeatures,
                DbRepository<OnlineMarketContext, ProductFeature, int> _dbProductFeature,
                //DBRepositoryEx<OnlineMarketContext, Category, string> _dbCategoryEx,
                IHostingEnvironment _hostingEnvironment,
                IConfiguration configuration,
                OnlineMarketContext db
            )
        {
            userManager = _userManager;
            dbBrand = _dbBrand;
            dbCategory = _dbCategory;
            dbProductAbstract = _dbProductAbstract;
            dbAdditionalFeatures = _dbAdditionalFeatures;
            dbProductAdditionalFeatures = _dbProductAdditionalFeatures;
            dbProductFeature = _dbProductFeature;
            //dbCategoryEx = _dbCategoryEx;
            _db = db;

            hostingEnvironment = _hostingEnvironment;
            contentRootPath = hostingEnvironment.ContentRootPath;
            _configuration = configuration;
        }
        //Inject DataBase--End
        #endregion
        #region Index
        public IActionResult Index()
        {
            return View();
        }
        #endregion
        #region Category
        //Category--Start
        public IActionResult ShowCategory()
        {
            var dbViewModel = dbCategory.GetAll();
            return View(dbViewModel);
        }
        public IActionResult InsertCategory(string notification)
        {
            ViewData["Category"] = dbCategory.GetAll();
            if (notification != null)
            {
                ViewData["nvm"] = NotificationHandler.DeserializeMessage(notification);
                return View();
            }
            return View();
        }
        public async Task<IActionResult> InsertCategoryConfirm(CategoryViewModel model)
        {
            string nvm;
            var currentUser = await userManager.FindByNameAsync(User.Identity.Name);
            try
            {
                if (!ModelState.IsValid)
                {
                    nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Wrong_Values, contentRootPath);
                    return RedirectToAction("InsertCategory", new { notification = nvm });
                }
                if (model != null)
                {
                    //check if the 'Name' field value does exist in DB, returns error to user
                    //var isExist = dbCategoryEx.FindExactName(model.Name);
                    var isExist = _db.Category.Where(x => x.Name.ToLower() == model.Name.ToLower()).ToList();
                    if (isExist.Count > 0)
                    {
                        nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.DuplicatedValue, contentRootPath);
                        return RedirectToAction("InsertCategory", new { notification = nvm });
                    }
                    Category category = new Category()
                    {
                        Name = model.Name,
                        LatinName = model.LatinName,
                        Description = model.Description,
                        UserId = currentUser.Id,
                        AliasName = model.AliasName,
                        TitleAltName = model.TitleAltName,
                        OrderedCount = 0,
                        TotalVisit = 0
                    };
                    var isParentExist = dbCategory.FindById(model.ParentId);
                    if (isParentExist != null)
                    {
                        category.ParentId = model.ParentId;
                    }
                    else
                    {
                        category.ParentId = 0;
                    }

                    //Insert Image
                    var files = HttpContext.Request.Form.Files;
                    foreach (var Image in files)
                    {
                        if (Image != null && Image.Length > 0)
                        {
                            var savePath = await FileCopier.SaveImageInDirectory("DefaultPaths", "CategoryImage", Image);
                            category.ImagePath = savePath;
                        }
                    }

                    dbCategory.Insert(category);
                    TempData["InsertConfirm"] = "دسته بندی با موفقیت ثبت شد";

                    nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Success_Insert, contentRootPath);
                    return RedirectToAction("InsertCategory", new { notification = nvm });
                }
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Failed_Insert, contentRootPath);
                return RedirectToAction("InsertCategory", new { notification = nvm });
            }
            catch(Exception ex)
            {
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Failed_Operation, contentRootPath);
                return RedirectToAction("InsertCategory", new { notification = nvm });
            }
        }
        //Category--End
        #endregion
        #region Brand
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
        #endregion
        #region Feature
        //Feature--Start
        public IActionResult ShowFeature(string notification)
        {
            var dbViewModel = dbAdditionalFeatures.GetAll();
            if (notification != null)
            {
                ViewData["nvm"] = NotificationHandler.DeserializeMessage(notification);
                return View(dbViewModel);
            }
            return View(dbViewModel);
        }
        public IActionResult InsertFeature(string notification)
        {
            if (notification != null)
            {
                ViewData["nvm"] = NotificationHandler.DeserializeMessage(notification);
                return View();
            }
            return View();
        }
        public async Task<IActionResult> InsertFeatureConfirm(AdditionalFeaturesViewModel model)
        {
            ApplicationUser user = null;
            string nvm;
            if (User.Identity.IsAuthenticated)
            {
                user = await userManager.FindByNameAsync(User.Identity.Name);
                model.UserId = user.Id;
            }
            if (!ModelState.IsValid)
            {
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Wrong_Values, contentRootPath);
                return RedirectToAction("InsertFeature", new { notification = nvm });
            }
            try
            {
                AdditionalFeatures additionalFeatures = new AdditionalFeatures()
                {
                    Name = model.Name,
                    LatinName = model.LatinName,
                    Description = model.Description,
                    UserId = model.UserId,
                    RegDateTime = DateTime.Now,
                    Status = model.Status,
                };
                dbAdditionalFeatures.Insert(additionalFeatures);
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Success_Insert, contentRootPath);
                return RedirectToAction("InsertFeature", new { notification = nvm });
            }
            catch (Exception)
            {
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Failed_Operation, contentRootPath);
                return RedirectToAction("InsertFeature", new { notification = nvm });
            }
        }
        public IActionResult DeleteFeature(int Id)
        {
            var status = dbAdditionalFeatures.DeleteById(Id);
            string nvm;
            if (status)
            {
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Success_Remove, contentRootPath);
                return RedirectToAction("ShowFeature", new { notification = nvm });
            }
            nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Failed_Operation, contentRootPath);
            return RedirectToAction("ShowFeature", new { notification = nvm });

        }
        public IActionResult EditFeature(int Id, string notification)
        {
            ViewData["Feature"] = dbAdditionalFeatures.FindById(Id);
            if (notification != null)
            {
                ViewData["nvm"] = NotificationHandler.DeserializeMessage(notification);
                return View();
            }
            return View();
        }
        public async Task<IActionResult> EditFeatureConfirm(AdditionalFeaturesViewModel model)
        {
            ApplicationUser user = null;
            string nvm;
            if (User.Identity.IsAuthenticated)
            {
                user = await userManager.FindByNameAsync(User.Identity.Name);
                model.UserId = user.Id;
            }
            if (!ModelState.IsValid)
            {
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Wrong_Values, contentRootPath);
                return RedirectToAction("EditFeature", new {Id=model.Id, notification = nvm });
            }

            try
            {
                var entity = dbAdditionalFeatures.FindById(model.Id);
                entity.Name = model.Name;
                entity.LatinName = model.LatinName;
                entity.Description = model.Description;
                entity.Status = model.Status;
                entity.UserId = model.UserId;

                dbAdditionalFeatures.Update(entity);

                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Success_Update, contentRootPath);
                return RedirectToAction("ShowFeature", new { notification = nvm });
            }
            catch (Exception)
            {
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Failed_Operation, contentRootPath);
                return RedirectToAction("EditFeature", new { Id = model.Id, notification = nvm });
            }
        }
        //Feature--End
        #endregion
        #region Product
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
        //Product--End
        #endregion
        #region Test
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
        #endregion
    }
}