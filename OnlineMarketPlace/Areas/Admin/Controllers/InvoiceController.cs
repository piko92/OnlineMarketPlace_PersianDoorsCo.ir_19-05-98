using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OnlineMarket.Models;
using OnlineMarketPlace.Areas.Identity.Data;
using OnlineMarketPlace.ClassLibraries.NotificationHandler;
using OnlineMarketPlace.Models.AdminViewModels;
using OnlineMarketPlace.Repository;

namespace OnlineMarketPlace.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperVisor")]
    public class InvoiceController : Controller
    {
        #region Inject
        readonly UserManager<ApplicationUser> userManager;
        readonly DbRepository<OnlineMarketContext, ProductFeature, int> dbProductFeature;
        readonly DbRepository<OnlineMarketContext, Invoice, int> dbInvoice;
        readonly DbRepository<OnlineMarketContext, InvoiceProduct, int> dbInvoiceProduct;
        private readonly IHostingEnvironment hostingEnvironment;
        string contentRootPath;


        public InvoiceController
            (
                UserManager<ApplicationUser> _userManager,
                DbRepository<OnlineMarketContext, ProductFeature, int> _dbProductFeature,
                DbRepository<OnlineMarketContext, Invoice, int> _dbInvoice,
                DbRepository<OnlineMarketContext, InvoiceProduct, int> _dbInvoiceProduct,
                IHostingEnvironment _hostingEnvironment


            )
        {
            userManager = _userManager;
            dbProductFeature = _dbProductFeature;
            dbInvoice = _dbInvoice;
            dbInvoiceProduct = _dbInvoiceProduct;
            hostingEnvironment = _hostingEnvironment;
            contentRootPath = hostingEnvironment.ContentRootPath;

        }
        #endregion
        #region Index
        public IActionResult Index()
        {
            return View();
        }
        #endregion
        #region Purchuse Cart
        public IActionResult ShowNotPaidPurchuseCart(string notification)
        {
            var dbViewModel = dbInvoice.GetInclude(
               e => e.InvoiceProduct,
               e => e.Customer)
               .Where(e => e.IsPaid == false && e.InvoiceProduct.Count() > 0).ToList(); ;
            //ViewData["dbInvoiceProduct"] = dbInvoiceProduct.GetInclude(
            //    e => e.ProductFeature,
            //    e => e.ProductFeature.ProductAbstract,
            //    e => e.ProductFeature.ProductAbstract.ProductImage,
            //    e => e.Invoice)
            //    .ToList();
            if (notification != null)
            {
                ViewData["nvm"] = NotificationHandler.DeserializeMessage(notification);
                return View(dbViewModel);
            }
            return View(dbViewModel);
        }
        public IActionResult ShowPaidPurchuseCart(string notification)
        {
            var dbViewModel = dbInvoice.GetInclude(
                e => e.InvoiceProduct,
                e => e.Customer)
                .Where(e => e.IsPaid == true).ToList();
            if (notification != null)
            {
                ViewData["nvm"] = NotificationHandler.DeserializeMessage(notification);
                return View(dbViewModel);
            }
            return View(dbViewModel);
        }
        public IActionResult ShowPurchuseCartDetails(int id)
        {
            var dbViewModel = dbInvoiceProduct.GetInclude(
               e => e.ProductFeature.ProductAbstract.ProductImage,
               e => e.Invoice.Customer).Where(e => e.InvoiceId == id)
               .ToList();
            return View(dbViewModel);
        }
        public IActionResult EditPurchuseCartStatus(int id)
        {
            var dbinvoice = dbInvoice.GetInclude(
                e => e.InvoiceProduct,
                e => e.Customer)
                .Where(e => e.Id == id).FirstOrDefault();
           // var dbinvoice = dbInvoice.FindById(id);
            ViewData["PurchuseCart"] = dbinvoice;
            return View();
        }
        public IActionResult EditPurchuseCartStatusConfirm(EditPurchuseCartStatusViewModel model)
        {
            string nvm;
            var entity = dbInvoice.FindById(model.Id);
            if (entity!=null)
            {
                entity.IsPaid = model.IsPaid;
                entity.Approved = model.Approved;
                entity.Sent = model.Sent;
                entity.Delivered = model.Delivered;
            }
            try
            {
                dbInvoice.Update(entity);
                if (model.IsPaid)
                {
                    nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Success_Update, contentRootPath);
                    return RedirectToAction("ShowPaidPurchuseCart", new { notification = nvm });
                }
                else
                {
                    nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Success_Update, contentRootPath);
                    return RedirectToAction("ShowNotPaidPurchuseCart", new { notification = nvm });
                }
                
            }
            catch (Exception)
            {

            }
            nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Failed_Operation, contentRootPath);
            return RedirectToAction("ShowNotPaidPurchuseCart", new { notification = nvm });
        }
        public IActionResult DeletePurchuseCart(int id)
        {
            string nvm;

            var entity = dbInvoice.FindById(id);
            if (entity != null)
            {
                var InvoiceProductList = dbInvoiceProduct.GetAll().Where(e => e.InvoiceId == id).ToList();
                try
                {
                    foreach (var item in InvoiceProductList)
                    {
                        dbInvoiceProduct.DeleteById(item.Id);
                    }
                    dbInvoice.DeleteById(id);
                    
                    nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Success_Remove, contentRootPath);
                    return RedirectToAction("ShowNotPaidPurchuseCart", new { notification = nvm });
                }
                catch (Exception)
                {
                    nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Failed_Remove, contentRootPath);
                    return RedirectToAction("ShowNotPaidPurchuseCart", new { notification = nvm });
                }
            }
            nvm = NotificationHandler.SerializeMessage<string>(NotificationHandler.Failed_Remove, contentRootPath);
            return RedirectToAction("ShowNotPaidPurchuseCart", new { notification = nvm });
        }


        #endregion

    }
}