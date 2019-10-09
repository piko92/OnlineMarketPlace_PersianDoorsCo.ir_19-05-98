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
        DbRepository<OnlineMarketContext, ProductPrice, int> dbProductPrice;

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
                DbRepository<OnlineMarketContext, ProductPrice, int> _dbProductPrice,
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
            dbProductPrice = _dbProductPrice;
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
            catch (Exception)
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
            catch (Exception)
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
                return RedirectToAction("EditFeature", new { model.Id, notification = nvm });
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
                return RedirectToAction("EditFeature", new { model.Id, notification = nvm });
            }
        }
        //Feature--End
        #endregion
        #region Product
        //Product--Start
        public IActionResult ShowProduct(string notification)
        {
            var dbViewModel = dbProductAbstract.GetInclude(e => e.Brand, e => e.Category, e => e.ProductImage, e => e.ProductFeature);
            if (notification != null)
            {
                ViewData["nvm"] = NotificationHandler.DeserializeMessage(notification);
                return View(dbViewModel);
            }
            return View(dbViewModel);
        }
        public IActionResult InsertProduct(string notification)
        {
            ViewData["Brand"] = dbBrand.GetAll();
            ViewData["Category"] = dbCategory.GetAll();
            if (notification != null)
            {
                ViewData["nvm"] = NotificationHandler.DeserializeMessage(notification);
                return View();
            }
            return View();
        }
        public async Task<IActionResult> InsertProductConfirm(ProductViewModel model)
        {
            string nvm;
            var currentUser = await userManager.FindByNameAsync(User.Identity.Name);
            if (ModelState.IsValid == false)
            {
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Wrong_Values, contentRootPath);
                return RedirectToAction("ShowProduct", new { notification = nvm });
            }
            try
            {
                ProductAbstract productAbstract = new ProductAbstract()
                {
                    Name = model.Name,
                    BrandId = model.BrandI,
                    CategoryId = model.CategoryId,
                    Status = model.Status,
                    UserId = currentUser.Id,
                    BasePrice = model.BasePrice
                };
                int id = dbProductAbstract.Insert(productAbstract);

                ProductFeature productFeature = new ProductFeature()
                {
                    ProductAbstractId = id,
                    ProductCode = model.Code,
                    Status = model.Status,
                    UserId = currentUser.Id,
                };
                dbProductFeature.Insert(productFeature);

                //Insert Main Image - Start
                if (model.MainImage != null)
                {
                    ProductImage productImage = new ProductImage()
                    {
                        ProductId = id,
                        UserId = currentUser.Id,
                        GrayScale = false,
                        Compressed = false,
                        IsMainImage = true
                    };

                    string folderPath = _configuration.GetSection("DefaultPaths").GetSection("ProductImage").Value;

                    string savePath = await FileManager.SaveImageInDirectory(contentRootPath, folderPath, model.MainImage, true, id);
                    string thumnailSavePath = FileManager.SaveThumbnail(savePath, contentRootPath, folderPath, ImageFormat.Png, true, id);

                    productImage.ImagePath = savePath;
                    productImage.ImageThumbnailPath = thumnailSavePath;

                    dbProductImage.Insert(productImage);
                }
                //Insert Main Image - End
                //Insert Other Images -Start
                if (model.img != null)
                {
                    foreach (var item in model.img)
                    {
                        ProductImage productImage = new ProductImage()
                        {
                            ProductId = id,
                            UserId = currentUser.Id,
                            GrayScale = false,
                            Compressed = false
                        };

                        string folderPath = _configuration.GetSection("DefaultPaths").GetSection("ProductImage").Value;

                        string savePath = await FileManager.SaveImageInDirectory(contentRootPath, folderPath, item, true, id);
                        string thumnailSavePath = FileManager.SaveThumbnail(savePath, contentRootPath, folderPath, ImageFormat.Png, true, id);

                        productImage.ImagePath = savePath;
                        productImage.ImageThumbnailPath = thumnailSavePath;

                        dbProductImage.Insert(productImage);
                    }
                }
                //Insert Other Images -End
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Success_Insert, contentRootPath);
                return RedirectToAction("ShowProduct", new { notification = nvm });
            }
            catch (Exception)
            {
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Failed_Insert, contentRootPath);
                return RedirectToAction("ShowProduct", new { notification = nvm });
            }

        }
        public IActionResult DeleteProduct(int Id)
        {
            string nvm;
            try
            {
                var entity = dbProductAbstract.FindById(Id);
                if (entity != null)
                {
                    var ProductImageFiles = _db.ProductImage.Where(x => x.ProductId == entity.Id).ToList();
                    var ProductAbstractDeleted = dbProductAbstract.DeleteById(Id);
                    if (true)
                    {
                        if (ProductImageFiles.Count > 0)
                        {
                            foreach (var item in ProductImageFiles)
                            {
                                //delete related image files:
                                bool imgDel = FileManager.DeleteFile(contentRootPath, item.ImagePath);
                                bool thumbnailImgDel = FileManager.DeleteFile(contentRootPath, item.ImageThumbnailPath);
                            }
                            bool dirDel = FileManager.DeleteDirectory(contentRootPath, ProductImageFiles.FirstOrDefault().ImagePath);
                        }
                        nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Success_Remove, contentRootPath);
                        return RedirectToAction("ShowProduct", new { notification = nvm });
                    }
                }
            }
            catch (Exception)
            {

            }
            nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Failed_Remove, contentRootPath);
            return RedirectToAction("ShowProduct", new { notification = nvm });
        }
        public IActionResult EditProduct(int Id)
        {
            ViewData["Brand"] = dbBrand.GetAll();
            ViewData["Category"] = dbCategory.GetAll();
            ViewData["ProductAbstract"] = dbProductAbstract.GetIncludeById(Id, e => e.Brand, e => e.Category, e => e.ProductImage, e => e.ProductFeature);
            //ViewData["ProductFeature"]  = dbProductFeature.GetAll().Where(x => x.ProductAbstractId == Id).FirstOrDefault();
            return View();
        }
        public async Task<IActionResult> EditProductConfirm(ProductViewModel model)
        {
            string nvm;
            int id = model.Id;
            var currentUser = await userManager.FindByNameAsync(User.Identity.Name);
            if (ModelState.IsValid == false)
            {
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Wrong_Values, contentRootPath);
                return RedirectToAction("ShowProduct", new { notification = nvm });
            }
            try
            {
                var entityProductAbstract = dbProductAbstract.FindById(model.Id);
                if (entityProductAbstract != null)
                {
                    entityProductAbstract.Name = model.Name;
                    entityProductAbstract.BrandId = model.BrandI;
                    entityProductAbstract.CategoryId = model.CategoryId;
                    entityProductAbstract.Status = model.Status;
                    entityProductAbstract.UserId = currentUser.Id;
                    entityProductAbstract.BasePrice = model.BasePrice;
                }
                var entityProductFeature = dbProductFeature.GetAll().Where(e => e.ProductAbstractId == id).FirstOrDefault();
                if (entityProductFeature != null)
                {
                    entityProductFeature.ProductAbstractId = id;
                    entityProductFeature.ProductCode = model.Code;
                    entityProductFeature.Status = model.Status;
                    entityProductFeature.UserId = currentUser.Id;
                }

                //Insert Main Image - Start
                if (model.MainImage != null)
                {
                    var mainImage = entityProductAbstract.ProductImage.Where(e => e.ProductId == id).Where(e => e.IsMainImage == true).FirstOrDefault();
                    if (mainImage != null)
                    {
                        bool status = dbProductImage.DeleteById(mainImage.Id);
                        if (status)
                        {
                            bool imgDel = FileManager.DeleteFile(contentRootPath, mainImage.ImagePath);
                            ProductImage productImage = new ProductImage()
                            {
                                ProductId = id,
                                UserId = currentUser.Id,
                                GrayScale = false,
                                Compressed = false,
                                IsMainImage = true
                            };
                            string folderPath = _configuration.GetSection("DefaultPaths").GetSection("ProductImage").Value;
                            string savePath = await FileManager.SaveImageInDirectory(contentRootPath, folderPath, model.MainImage, true, id);
                            string thumnailSavePath = FileManager.SaveThumbnail(savePath, contentRootPath, folderPath, ImageFormat.Png, true, id);
                            productImage.ImagePath = savePath;
                            productImage.ImageThumbnailPath = thumnailSavePath;
                            dbProductImage.Insert(productImage);
                        }
                    }
                }
                //Insert Main Image - End
                //Insert Other Images -Start
                if (model.img != null)
                {
                    foreach (var item in model.img)
                    {
                        ProductImage productImage = new ProductImage()
                        {
                            ProductId = id,
                            UserId = currentUser.Id,
                            GrayScale = false,
                            Compressed = false
                        };

                        string folderPath = _configuration.GetSection("DefaultPaths").GetSection("ProductImage").Value;

                        string savePath = await FileManager.SaveImageInDirectory(contentRootPath, folderPath, item, true, id);
                        string thumnailSavePath = FileManager.SaveThumbnail(savePath, contentRootPath, folderPath, ImageFormat.Png, true, id);

                        productImage.ImagePath = savePath;
                        productImage.ImageThumbnailPath = thumnailSavePath;

                        dbProductImage.Insert(productImage);
                    }
                }
                //Insert Other Images -End
                dbProductAbstract.Update(entityProductAbstract);
                dbProductFeature.Update(entityProductFeature);
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Success_Update, contentRootPath);
                return RedirectToAction("ShowProduct", new { notification = nvm });

            }
            catch (Exception)
            {
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Failed_Update, contentRootPath);
                return RedirectToAction("ShowProduct", new { notification = nvm });
            }

        }
        public string DeleteProductImage(int Id)
        {
            var entity = dbProductImage.FindById(Id);
            if (entity != null)
            {
                try
                {
                    bool status = dbProductImage.DeleteById(Id);
                    dbProductImage.Save();
                    if (status)
                    {
                        bool imgDel = FileManager.DeleteFile(contentRootPath, entity.ImagePath);
                        return "true";
                    }
                }
                catch (Exception)
                {
                    return "false";
                }
            }
            return "false";
        }
        //Product--End
        #endregion
        #region Product Update Count and Price
        //Update Count and Price Of Products -Start
        public IActionResult UpdateProductInfo(string notification)
        {
            var dbViewModel = dbProductAbstract.GetInclude(e => e.Brand, e => e.Category, e => e.ProductImage, e => e.ProductFeature);
            if (notification != null)
            {
                ViewData["nvm"] = NotificationHandler.DeserializeMessage(notification);
                return View(dbViewModel);
            }
            return View(dbViewModel);
        }
        public string EditProductCount(int Id, string basePrice, string count)
        {
            var entityProductAbstract = dbProductAbstract.FindById(Id);
            if (entityProductAbstract != null)
            {
                try
                {
                    var entityProductFeature = dbProductFeature.GetAll().Where(e => e.ProductAbstractId == Id).FirstOrDefault();
                    entityProductAbstract.BasePrice = decimal.Parse(basePrice);
                    entityProductFeature.Count = int.Parse(count);
                    dbProductAbstract.Update(entityProductAbstract);
                    dbProductFeature.Update(entityProductFeature);
                    return "true";
                }
                catch (Exception)
                {
                    return "false";
                }
            }
            return "false";
        }
        //Update Count and Price Of Products -Start
        #endregion
        #region Product Additional Feature
        public IActionResult ShowProductFeature(string notification)
        {
            var dbViewModel = dbProductAbstract.GetInclude(e => e.ProductFeature);
            ViewData["ProductAdditionalFeatures"] = dbProductAdditionalFeatures.GetAll();
            ViewData["AdditionalFeatures"] = dbAdditionalFeatures.GetAll();
            if (notification != null)
            {
                ViewData["nvm"] = NotificationHandler.DeserializeMessage(notification);
                return View(dbViewModel);
            }
            return View(dbViewModel);
        }
        public IActionResult EditProductFeature(int Id)
        {
            ViewData["ProductAbstract"] = dbProductAbstract.GetIncludeById(Id, e => e.Brand, e => e.Category, e => e.ProductImage, e => e.ProductFeature);
            ViewData["AdditionalFeatures"] = dbAdditionalFeatures.GetAll().Where(e => e.Status == true).ToList();
            var idProductFeature = dbProductAbstract.FindById(Id).ProductFeature.Where(e => e.ProductAbstractId == Id).FirstOrDefault().Id;
            ViewData["ProductAdditionalFeatures"] = dbProductAdditionalFeatures.GetAll().Where(e => e.ProductFeatureId == idProductFeature).ToList();
            return View();
        }
        public IActionResult EditProductFeatureConfirm(ProductFeatureViewModel model)
        {
            string nvm;
            if (ModelState.IsValid != true)
            {
                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Wrong_Values, contentRootPath);
                return RedirectToAction("ShowProductFeature", new { notification = nvm });
            }
            try
            {
                var ProductFeature = dbProductFeature.FindById(model.ProductFeatureId);
                //update
                var pAdditionalFeatures = dbProductAdditionalFeatures.GetAll().Where(e => e.ProductFeatureId == model.ProductFeatureId);
                for (int i = 0; i < model.FeatureData.Count; i++)
                {
                    var featureData = model.FeatureData[i];
                    var additionalFeatureId = model.AdditionalFeatureId[i];
                    var entity = pAdditionalFeatures.Where(e => e.AdditionalFeaturesId == model.AdditionalFeatureId[i]).FirstOrDefault();
                    if (entity != null)
                    {
                        entity.AdditionalFeaturesId = additionalFeatureId;
                        entity.ProductFeatureId = model.ProductFeatureId;
                        entity.Value = featureData;
                        entity.Status = true;
                        dbProductAdditionalFeatures.Update(entity);
                    }
                    else
                    {
                        //insert
                        ProductAdditionalFeatures productAdditionalFeatures = new ProductAdditionalFeatures()
                        {
                            AdditionalFeaturesId = additionalFeatureId,
                            ProductFeatureId = model.ProductFeatureId,
                            Value = featureData,
                            Status = true,
                        };
                        dbProductAdditionalFeatures.Insert(productAdditionalFeatures);
                    }
                }

                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Success_Update, contentRootPath);
                return RedirectToAction("ShowProductFeature", new { notification = nvm });
            }
            catch (Exception)
            {

                nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Failed_Update, contentRootPath);
                return RedirectToAction("ShowProductFeature", new { notification = nvm });
            }

        }

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