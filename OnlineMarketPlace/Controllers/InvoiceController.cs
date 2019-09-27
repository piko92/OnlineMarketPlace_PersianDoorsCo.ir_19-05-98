using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OnlineMarket.Models;
using OnlineMarketPlace.Areas.Identity.Data;
using OnlineMarketPlace.Repository;

namespace OnlineMarketPlace.Controllers
{
    public class InvoiceController : Controller
    {
        #region Inject
        //Inject DataBase--Start
        //OnlineMarketContext db;
        readonly UserManager<ApplicationUser> userManager;
        readonly DbRepository<OnlineMarketContext, ProductFeature, int> dbProductFeature;
        readonly DbRepository<OnlineMarketContext, Invoice, int> dbInvoice;
        readonly DbRepository<OnlineMarketContext, InvoiceProduct, int> dbInvoiceProduct;


        public InvoiceController
            (
                UserManager<ApplicationUser> _userManager,
                DbRepository<OnlineMarketContext, ProductFeature, int> _dbProductFeature,
                DbRepository<OnlineMarketContext, Invoice, int> _dbInvoice,
                DbRepository<OnlineMarketContext, InvoiceProduct, int> _dbInvoiceProduct
            )
        {
            userManager = _userManager;
            dbProductFeature = _dbProductFeature;
            dbInvoice = _dbInvoice;
            dbInvoiceProduct = _dbInvoiceProduct;
        }
        //Inject DataBase--End
        #endregion
        #region Index
        //Index--Start
        public IActionResult Index()
        {
            return RedirectToAction("ShowPurchuseCart");
        }
        //Index--End
        #endregion
        #region Show Purchuse Cart
        //Purchuse Cart--Start
        public int InitializeCart(Invoice invoiceUser, ApplicationUser currentUser)
        {
            int cartId;
            if (invoiceUser == null)
            {
                Invoice invoice = new Invoice()
                {
                    CustomerId = currentUser.Id,
                    RegDateTime = DateTime.Now,
                    IsPaid = false,
                    Status = true,
                    Sent = false,
                    Delivered = false
                };
                cartId = dbInvoice.Insert(invoice);
            }
            else
            {
                cartId = invoiceUser.Id;
            }

            return (cartId);
        }
        public async Task<IActionResult> ShowPurchuseCart()
        {
            if (User.Identity.Name == null)
            {
                //Need User Login
                return Json("Login");
                // return RedirectToAction("Login", "Account");
            }
            var currentUser = await userManager.FindByNameAsync(User.Identity.Name);
            var invoiceUser = dbInvoice.GetAll().Where(e => e.CustomerId == currentUser.Id && e.IsPaid == false).FirstOrDefault();
            int cartId = InitializeCart(invoiceUser, currentUser);
            var dbViewModel = dbInvoiceProduct.GetInclude(e => e.ProductFeature, e => e.ProductFeature.ProductAbstract, e=>e.Invoice).Where(e => e.InvoiceId == cartId).ToList();
            return View(dbViewModel);
        }

        public async Task<IActionResult> AddToCart(int productFeatureId)
        {
            var productFeature = dbProductFeature.FindById(productFeatureId);
            if (User.Identity.Name == null)
            {
                //Need User Login
                return Json("Login");
                // return RedirectToAction("Login", "Account");
            }
            var currentUser = await userManager.FindByNameAsync(User.Identity.Name);
            var invoiceUser = dbInvoice.GetAll().Where(e => e.CustomerId == currentUser.Id && e.IsPaid == false).FirstOrDefault();
            int cartId = InitializeCart(invoiceUser, currentUser);
            var invoiceProductUser = dbInvoiceProduct.GetAll().Where(e => e.InvoiceId == cartId && e.ProductFeatureId == productFeatureId).FirstOrDefault();
            if (invoiceProductUser != null)
            {
                return RedirectToAction("ShowPurchuseCart");
            }
            InvoiceProduct invoiceProduct = new InvoiceProduct()
            {
                ProductFeatureId = productFeatureId,
                InvoiceId = cartId,
                Count = 1,
                Status = true,
            };
            dbInvoiceProduct.Insert(invoiceProduct);
            return RedirectToAction("ShowPurchuseCart");
        }
        //Purchuse Cart--Start
        #endregion
    }
}