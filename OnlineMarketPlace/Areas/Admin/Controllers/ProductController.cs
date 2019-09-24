using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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
        DbRepository<OnlineMarketContext, ProductImage, int> dbProductImage;

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
                DbRepository<OnlineMarketContext, ProductImage, int> _dbProductImage,
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
            dbProductImage = _dbProductImage;
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
        public IActionResult ShowCategory(string notification)
        {
            var dbViewModel = dbCategory.GetAll();
            if (notification != null)
            {
                ViewData["nvm"] = NotificationHandler.DeserializeMessage(notification);
                return View(dbViewModel);
            }
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
                        TotalVisit = 0,
                        Priority = model.Priority,
                        Status = model.Status
                    };
                    var isParentExist = dbCategory.FindById(model.ParentId);
                    if (isParentExist != null)
                    {
                        category.ParentId = model.ParentId;
                    }
                    else
                    {
                        category.ParentId = null;
                    }
                    //Insert Image
                    string folderPath = _configuration.GetSection("DefaultPaths").GetSection("CategoryImage").Value;
                    var savePath = await FileManager.SaveImageInDirectory(contentRootPath, folderPath, model.Image1);

                    category.ImagePath = savePath;
                    dbCategory.Insert(category);// Insert

                    nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Success_Insert, contentRootPath);
                    return RedirectToAction("InsertCategory", new { notification = nvm });
                }
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Failed_Insert, contentRootPath);
                return RedirectToAction("InsertCategory", new { notification = nvm });
            }
            catch (Exception ex)
            {
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Failed_Operation, contentRootPath);
                return RedirectToAction("InsertCategory", new { notification = nvm });
            }
        }
        public IActionResult DeleteCategory(int Id)
        {
            string nvm;
            var entity = dbCategory.FindById(Id);
            if (entity != null)
            {
                try
                {
                    bool status = dbCategory.DeleteById(Id);
                    dbCategory.Save();
                    if (status)
                    {
                        if (entity.ImagePath != null)
                        {
                            bool imgDel = FileManager.DeleteFile(contentRootPath, entity.ImagePath);
                        }
                        nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Success_Remove, contentRootPath);
                        return RedirectToAction("ShowCategory", new { notification = nvm });
                    }
                }
                catch (Exception)
                {
                    nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Failed_Remove, contentRootPath);
                    return RedirectToAction("ShowCategory", new { notification = nvm });
                }

            }
            nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Failed_Operation, contentRootPath);
            return RedirectToAction("ShowCategory", new { notification = nvm });
        }
        public IActionResult EditCategory(int Id)
        {
            ViewData["Category"] = dbCategory.FindById(Id);
            ViewData["dbCategory"] = dbCategory.GetAll();
            return View();
        }
        public async Task<IActionResult> EditCategoryConfirm(CategoryViewModel model)
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
                    var entity = dbCategory.FindById(model.Id);

                    entity.Name = model.Name;
                    entity.LatinName = model.LatinName;
                    entity.Description = model.Description;
                    entity.UserId = currentUser.Id;
                    entity.AliasName = model.AliasName;
                    entity.TitleAltName = model.TitleAltName;
                    entity.OrderedCount = 0;
                    entity.TotalVisit = 0;
                    entity.Priority = model.Priority;
                    entity.Status = model.Status;
                    entity.ShowInFooter = model.ShowInFooter;
                    entity.ShowInMenu = model.ShowInMenu;


                    var isParentExist = dbCategory.FindById(model.ParentId);
                    if (isParentExist != null)
                    {
                        entity.ParentId = model.ParentId;
                    }
                    else
                    {
                        entity.ParentId = null;
                    }
                    //Insert Image
                    if (model.Image1 != null)
                    {
                        if (entity.ImagePath != null)
                        {
                            bool imgDel = FileManager.DeleteFile(contentRootPath, entity.ImagePath);
                        }
                        string folderPath = _configuration.GetSection("DefaultPaths").GetSection("CategoryImage").Value;
                        var savePath = await FileManager.SaveImageInDirectory(contentRootPath, folderPath, model.Image1);
                        entity.ImagePath = savePath;
                    }


                    dbCategory.Update(entity);
                    nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Success_Update, contentRootPath);
                    return RedirectToAction("ShowCategory", new { notification = nvm });
                }
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Failed_Update, contentRootPath);
                return RedirectToAction("ShowCategory", new { notification = nvm });
            }
            catch (Exception ex)
            {
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Failed_Operation, contentRootPath);
                return RedirectToAction("InsertCategory", new { notification = nvm });
            }
        }
        //Category--End
        #endregion
        #region Brand
        //Brand--Start
        public IActionResult ShowBrand(string notification)
        {
            var dbViewModel = dbBrand.GetAll();
            if (notification != null)
            {
                ViewData["nvm"] = NotificationHandler.DeserializeMessage(notification);
                return View(dbViewModel);
            }
            return View(dbViewModel);
        }
        public IActionResult InsertBrand(string notification)
        {
            if (notification != null)
            {
                ViewData["nvm"] = NotificationHandler.DeserializeMessage(notification);
                return View();
            }
            return View();
        }
        public async Task<IActionResult> InsertBrandConfirm(BrandViewModel model)
        {
            string nvm;
            var currentUser = await userManager.FindByNameAsync(User.Identity.Name);
            try
            {
                if (!ModelState.IsValid)
                {
                    nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Wrong_Values, contentRootPath);
                    return RedirectToAction("InsertBrand", new { notification = nvm });
                }
                if (model != null)
                {
                    Brand brand = new Brand()
                    {
                        Name = model.Name,
                        LatinName = model.LatinName,
                        Description = model.Description,
                        UserId = currentUser.Id,
                        Status = model.Status
                    };

                    dbBrand.Insert(brand);
                    nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Success_Insert, contentRootPath);
                    return RedirectToAction("ShowBrand", new { notification = nvm });
                }
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Failed_Insert, contentRootPath);
                return RedirectToAction("ShowBrand", new { notification = nvm });
            }
            catch (Exception)
            {
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Failed_Operation, contentRootPath);
                return RedirectToAction("InsertBrand", new { notification = nvm });
            }
        }
        public IActionResult DeleteBrand(int Id)
        {
            string nvm;
            var entity = dbBrand.FindById(Id);
            if (entity != null)
            {
                try
                {
                    bool status = dbBrand.DeleteById(Id);
                    dbBrand.Save();
                    if (status)
                    {
                        nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Success_Remove, contentRootPath);
                        return RedirectToAction("ShowBrand", new { notification = nvm });
                    }
                }
                catch (Exception)
                {
                    nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Failed_Remove, contentRootPath);
                    return RedirectToAction("ShowBrand", new { notification = nvm });
                }

            }
            nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Failed_Operation, contentRootPath);
            return RedirectToAction("ShowBrand", new { notification = nvm });
        }
        public IActionResult EditBrand(int Id)
        {
            ViewData["Brand"] = dbBrand.FindById(Id);
            return View();
        }
        public async Task<IActionResult> EditBrandConfirm(BrandViewModel model)
        {
            string nvm;
            var currentUser = await userManager.FindByNameAsync(User.Identity.Name);
            try
            {
                if (!ModelState.IsValid)
                {
                    nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Wrong_Values, contentRootPath);
                    return RedirectToAction("InsertBrand", new { notification = nvm });
                }
                if (model != null)
                {
                    var entity = dbBrand.FindById(model.Id);

                    entity.Name = model.Name;
                    entity.LatinName = model.LatinName;
                    entity.Description = model.Description;
                    entity.UserId = currentUser.Id;
                    entity.Status = model.Status;

                    dbBrand.Update(entity);
                    nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Success_Update, contentRootPath);
                    return RedirectToAction("ShowBrand", new { notification = nvm });
                }
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Failed_Update, contentRootPath);
                return RedirectToAction("ShowBrand", new { notification = nvm });
            }
            catch (Exception)
            {
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Failed_Operation, contentRootPath);
                return RedirectToAction("InsertBrand", new { notification = nvm });
            }
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
                return RedirectToAction("EditFeature", new { Id = model.Id, notification = nvm });
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
        public async Task<IActionResult> InsertProductConfirm(ProductViewModel model)
        {
            var currentUser = await userManager.FindByNameAsync(User.Identity.Name);
            if (ModelState.IsValid)
            {
                ProductAbstract productAbstract = new ProductAbstract()
                {
                    Name = model.Name,
                    BrandId = model.BrandI,
                    CategoryId = model.CategoryId,
                    Status = model.Status,
                    UserId = currentUser.Id
                };
                int id = dbProductAbstract.Insert(productAbstract);

                //List<ProductImage> productImages = new List<ProductImage>();
                foreach (var item in model.img)
                {
                    ProductImage productImage = new ProductImage()
                    {
                        ProductId = id,
                        UserId = currentUser.Id,
                        GrayScale = false,
                        Compressed = false
                    };

                    //Insert Image
                    string folderPath = _configuration.GetSection("DefaultPaths").GetSection("ProductImage").Value;

                    string savePath = await FileManager.SaveImageInDirectory(contentRootPath, folderPath, item, true, id);
                    string thumnailSavePath = FileManager.SaveThumbnail(savePath, contentRootPath, folderPath, ImageFormat.Png, true, id);

                    productImage.ImagePath = savePath;
                    productImage.ImageThumbnailPath = thumnailSavePath;

                    dbProductImage.Insert(productImage);
                }

                TempData["InsertConfirm"] = "محصول با موفقیت ثبت شد";
                return RedirectToAction("ShowProduct");
            }
            return RedirectToAction("InsertProduct");
        }
        public IActionResult DeleteProduct(int Id)
        {
            var entity = dbProductAbstract.FindById(Id);
            if (entity != null)
            {
                //...
                //rest of the codes...
                //-----------------------------
                //delete related image files:
                var thisEntityFiles = _db.ProductImage.Where(x => x.ProductId == entity.Id).ToList();
                if (thisEntityFiles.Count > 0)
                {
                    foreach (var item in thisEntityFiles)
                    {
                        bool imgDel = FileManager.DeleteFile(contentRootPath, item.ImagePath);
                        bool thumbnailImgDel = FileManager.DeleteFile(contentRootPath, item.ImageThumbnailPath);
                    }
                }
                //حذف "دسته ای" در ریپازیتوری موجود نبود
                _db.ProductImage.RemoveRange(thisEntityFiles);
                _db.SaveChanges();
            }
            var entityDeleted = dbProductAbstract.DeleteById(Id);
            if (entityDeleted)
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